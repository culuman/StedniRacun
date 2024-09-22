using AplikacioniSloj;
using Microsoft.AspNetCore.Mvc;
using SlojPodataka;

public class NalogController : Controller
{
    private readonly clsKorisnikServis _korisnikServis;

    public NalogController(clsKorisnikServis korisnikServis)
    {
        _korisnikServis = korisnikServis;
    }

    [HttpGet]
    public ActionResult Registracija()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Registracija(RegistracijaModel model)
    {
        if (ModelState.IsValid)
        {
            bool uspesnaRegistracija = _korisnikServis.DodajKorisnika(new clsKorisnik
            {
                Jmbg = model.JMBG,
                Ime = model.Ime,
                Prezime = model.Prezime,
                Email = model.Email,
                Lozinka = model.Lozinka,
                Godine = model.Godine
            });

            if (uspesnaRegistracija)
            {
                // Ako je registracija uspešna, preusmerite korisnika na odgovarajući view ili akciju
                TempData["SuccessMessage"] = "Uspešno ste se registrovali!";
                return RedirectToAction("Prijava");
            }
            else
            {
                // Ako registracija nije uspela, možete dodati odgovarajuću logiku ili poruku
                ModelState.AddModelError(string.Empty, "Registracija nije uspešna, proverite unete podatke i pokušajte ponovo.");
            }
        }

        // Ako ModelState nije validan, vraća se isti view sa postojećim podacima
        return View(model);
    }

    [HttpGet]
    public ActionResult Prijava()
    {
        return View();
    }
    [HttpPost]
    public ActionResult Prijava(PrijavaModel model)
    {
        if (ModelState.IsValid)
        {
            // Pozovite metodu iz servisa koja proverava korisničke podatke
            var prijavljeniKorisnik = _korisnikServis.PronadjiPoEmailu(model.Email);

            if (prijavljeniKorisnik != null)
            {
                // Ako je pronađen korisnik sa datim emailom, proverite lozinku
                if (prijavljeniKorisnik.Lozinka == model.Lozinka)
                {
                    // Lozinka je ispravna, postavite korisničke podatke u sesiju
                    HttpContext.Session.SetString("JMBG", prijavljeniKorisnik.Jmbg);
                    HttpContext.Session.SetString("Ime", prijavljeniKorisnik.Ime);
                    HttpContext.Session.SetString("Prezime", prijavljeniKorisnik.Prezime);
                    HttpContext.Session.SetString("Email", prijavljeniKorisnik.Email);
                    HttpContext.Session.SetString("Lozinka", prijavljeniKorisnik.Lozinka);
                    HttpContext.Session.SetInt32("Godine", prijavljeniKorisnik.Godine);
                    HttpContext.Session.SetString("TipKorisnika", prijavljeniKorisnik.TipKorisnika);

                    // Redirekcija na odgovarajući view u zavisnosti od tipa korisnika
                    if (prijavljeniKorisnik.TipKorisnika == "admin")
                    {
                        return RedirectToAction("AdminPocetna", "Admin");
                    }
                    else if (prijavljeniKorisnik.TipKorisnika == "fizicko lice")
                    {
                        return RedirectToAction("KorisnikPocetna", "Korisnik");
                    }
                }
                else
                {
                    // Pogrešna lozinka
                    ModelState.AddModelError(string.Empty, "Pogrešna lozinka");
                }
            }
            else
            {
                // Nema korisnika sa datom e-poštom
                ModelState.AddModelError(string.Empty, "Nema korisnika sa tom email adresom!");
            }
        }

        // Ako ModelState nije validan, vraća se isti view sa postojećim podacima
        return View(model);
    }

}
