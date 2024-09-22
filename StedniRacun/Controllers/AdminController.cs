using AplikacioniSloj;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlojPodataka;
using System.Data;

namespace PrezentacioniSloj.Controllers
{
    public class AdminController : Controller
    {
        private readonly clsKorisnikServis _korisnikServis;
        private readonly clsRacunServis _racunServis;
        private readonly clsZahtevServis _zahtevServis;
        private readonly clsUplataServis _uplataServis;

        public AdminController(clsKorisnikServis korisnikServis, clsRacunServis racunServis, clsZahtevServis zahtevServis, clsUplataServis uplataServis)
        {
            _korisnikServis = korisnikServis;
            _racunServis = racunServis;
            _zahtevServis = zahtevServis;
            _uplataServis = uplataServis;
        }

        public IActionResult AdminPocetna()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminPregledKorisnika(string prezime)
        {
            DataSet rezultat;

            if (!string.IsNullOrEmpty(prezime))
            {
                rezultat = _korisnikServis.Prikazi(prezime);
            }
            else
            {
                rezultat = _korisnikServis.Prikazi();
            }

            return View(rezultat);
        }

        public IActionResult AdminPregledZahteva()
        {
            DataSet zahtevi = _zahtevServis.Prikazi();

            return View(zahtevi);
        }

        public IActionResult AdminPregledUplata()
        {
            DataSet uplate = _uplataServis.PrikaziUplateNaCekanju();

            return View(uplate);
        }

        public IActionResult AdminPregledKorisnikaDetalji()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IzmeniKorisnika(string? email, string? jmbg, string? action)
        {
            
            if (action == "izmeni" && !string.IsNullOrEmpty(email))
            {
                clsKorisnik korisnik = _korisnikServis.PronadjiPoEmailu(email);
                return View("AdminPregledKorisnikaDetalji", korisnik);
            }
            if (action == "obrisi" && !string.IsNullOrEmpty(jmbg))
            {
                if(_korisnikServis.ObrisiKorisnika(jmbg)) TempData["SuccessMessage"] = "Korisnik uspešno obrisan!";
                else TempData["ErrorMessage"] = "Neuspešno brisanje korisnika!";
            }

            return RedirectToAction("AdminPregledKorisnika");
        }

        [HttpPost]
        public IActionResult IzmeniPodatke(string action, clsKorisnik model, string JMBG)
        {
            if (action == "izmeni")
            {
                if (_korisnikServis.IzmeniKorisnika(JMBG, model)) TempData["SuccessMessage"] = "Korisnik uspešno izmenjen!";
                else TempData["ErrorMessage"] = "Neuspešna izmena korisnika!";

                return RedirectToAction("AdminPocetna"); 
            }

            return View(); 
        }


        public IActionResult AdminStampaRacuna()
        {
            DataSet dataSet = _racunServis.Prikazi();
                if (dataSet != null)
                    return View("AdminStampaRacuna", dataSet);
                else return RedirectToAction("AdminPocetna");
        }


        [HttpPost]
        public IActionResult UpravljajZahtevima(int IDZahteva, string action)
        {
            if (action == "odobri")
            {
                if (_zahtevServis.OdobriZahtev(IDZahteva)) TempData["SuccessMessage"] = "Zahtev uspešno odobren!";
                else TempData["ErrorMessage"] = "Neuspešno odobravanje zahteva!";
            }
            else if (action == "odbij")
            {
                if (_zahtevServis.Odbij(IDZahteva)) TempData["SuccessMessage"] = "Zahtev uspešno odbijen!";
                else TempData["ErrorMessage"] = "Neuspešno odbijanje zahteva!";
            }

            return RedirectToAction("AdminPregledZahteva");
        }

        [HttpPost]
        public IActionResult UpravljajUplatama(int IDUplate, string action)
        {
            if (action == "odobri")
            {
                if (_uplataServis.OdobriUplatu(IDUplate)) TempData["SuccessMessage"] = "Uplata uspešno odobrena!";
                else TempData["ErrorMessage"] = "Neuspešno odobravanje uplate!";
            }
            else if (action == "odbij")
            {
                if (_uplataServis.Odbij(IDUplate)) TempData["SuccessMessage"] = "Uplata uspešno odbijena!";
                else TempData["ErrorMessage"] = "Neuspešno odbijanje uplate!";
            }

            return RedirectToAction("AdminPregledUplata");
        }
    }
}
