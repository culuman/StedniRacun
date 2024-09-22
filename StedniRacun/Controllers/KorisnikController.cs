using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using AplikacioniSloj;
using SlojPodataka;
using System.Data;

namespace PrezentacioniSloj.Controllers
{
    public class KorisnikController : Controller
    {

        private readonly clsKorisnikServis _korisnikServis;
        private readonly clsUplataServis _uplataServis;
        private readonly clsRacunServis _racunServis;
        private readonly clsZahtevServis _zahtevServis;

        public KorisnikController(clsKorisnikServis korisnikServis, clsUplataServis uplataServis, clsRacunServis racunServis, clsZahtevServis zahtevServis)
        {
            _korisnikServis = korisnikServis;
            _uplataServis = uplataServis;
            _racunServis = racunServis;
            _zahtevServis = zahtevServis;
        }
        public IActionResult KorisnikPocetna()
        {
            return View();
        }

        public IActionResult KorisnikProfil()
        {
            // Dobijanje podataka iz sesije
            var jmbg = HttpContext.Session.GetString("JMBG");
            var ime = HttpContext.Session.GetString("Ime");
            var prezime = HttpContext.Session.GetString("Prezime");
            var lozinka = HttpContext.Session.GetString("Lozinka");
            var godine = HttpContext.Session.GetInt32("Godine");
            var email = HttpContext.Session.GetString("Email");
            var tipKorisnika = HttpContext.Session.GetString("TipKorisnika");

            // Kreiranje modela sa podacima iz sesije
            var model = new RegistracijaModel
            {
                JMBG = jmbg,
                Ime = ime,
                Prezime = prezime,
                Email = email,
                Lozinka = lozinka,
                Godine = (int)godine,
                TipKorisnika = tipKorisnika

            };

            return View(model);
        }

        public IActionResult KorisnikZahtev()
        {
            // Pretpostavimo da dobijamo JMBG korisnika iz sesije
            string jmbgKorisnika = HttpContext.Session.GetString("JMBG");

            // Dobijamo sve zahteve
            DataSet sviZahtevi = _zahtevServis.PrikaziPoKorisniku(jmbgKorisnika);

            return View(sviZahtevi);
        }

        [HttpPost]
        public IActionResult DodajZahtev()
        {
            
            string jmbgKorisnika = HttpContext.Session.GetString("JMBG");

            if (!string.IsNullOrEmpty(jmbgKorisnika))
            {
                if(_zahtevServis.OtvoriZahtev(jmbgKorisnika)) TempData["SuccessMessage"] = "Zahtev uspešno kreiran!";
                else TempData["ErrorMessage"] = "Kreiranje zahteva nije uspelo!";
                return RedirectToAction("KorisnikZahtev");
            }
            return RedirectToAction("KorisnikZahtev");
        }

        [HttpPost]
        public IActionResult IzmeniPodatke(RegistracijaModel model, string action)
        {

            if (action == "izmeni")
            {
                // Uzimanje JMBG korisnika iz sesije
                var jmbgIzSesije = HttpContext.Session.GetString("JMBG");

                if (!string.IsNullOrEmpty(jmbgIzSesije))
                {
                    clsKorisnik korisnik = new clsKorisnik();
                    korisnik.Jmbg = model.JMBG;
                    korisnik.Ime = model.Ime;
                    korisnik.Prezime = model.Prezime;
                    korisnik.Email = model.Email;
                    korisnik.Lozinka = model.Lozinka;
                    korisnik.Godine = model.Godine;
                    korisnik.TipKorisnika = model.TipKorisnika;

                    if (_korisnikServis.IzmeniKorisnika(jmbgIzSesije, korisnik))
                        return RedirectToAction("KorisnikPocetna");
                    return View();
                }
                return View();

            }

            else if (action == "obrisi")
            {   
                var jmbg = HttpContext.Session.GetString("JMBG");

                if (!string.IsNullOrEmpty(jmbg))
                {
                    if (_korisnikServis.ObrisiKorisnika(jmbg))
                        return RedirectToAction("Pocetna", "Home");
                    return View();
                }
                return View();
            }
            return View();
        }


        public IActionResult KorisnikStampa()
        {
            var jmbg = HttpContext.Session.GetString("JMBG");
            if (!string.IsNullOrEmpty(jmbg))
            {
                DataSet dataSet = _racunServis.PrikaziStanjeRacuna(jmbg);
                if (dataSet != null)
                    return View("KorisnikStampa", dataSet);
                else return RedirectToAction("KorisnikPocetna");
            }
            return RedirectToAction("KorisnikPocetna");
        }

        public IActionResult KorisnikUplata()
        {
            // Pretpostavimo da dobijamo JMBG korisnika iz sesije
            string jmbgKorisnika = HttpContext.Session.GetString("JMBG");

            // Dobijamo sve zahteve
            DataSet sveUplate = _uplataServis.PrikaziUplateZaKorisnika(jmbgKorisnika);

            DataSet sviRacuni = _racunServis.PrikaziStanjeRacuna(jmbgKorisnika);

            ViewBag.BrojeviRacuna = sviRacuni.Tables[0].AsEnumerable()
                    .Select(row => new {
                        IDRacuna = row.Field<int>("IDRacuna"),
                        BrojRacuna = row.Field<string>("BrojRacuna")
                    }).ToList();

            return View(sveUplate);
        }

        [HttpPost]
        public IActionResult DodajUplatu(int idRacuna, decimal iznos)
        {

            string jmbgKorisnika = HttpContext.Session.GetString("JMBG");

            if (!string.IsNullOrEmpty(jmbgKorisnika))
            {
                if (idRacuna != 0 && iznos != 0 && _uplataServis.KreirajUplatu(jmbgKorisnika, idRacuna, iznos))
                {
                    TempData["SuccessMessage"] = "Zahtev za uplatu uspešno poslat!";
                    return RedirectToAction("KorisnikUplata");
                }
                else
                {
                    TempData["ErrorMessage"] = "Uplata neuspešna.";
                    return RedirectToAction("KorisnikUplata");
                }
            }
            return RedirectToAction("KorisnikUplata");
        }
    }
}
