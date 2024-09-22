using DomenskiSloj;
using SlojPodataka;
using SlojPodataka.Interfejsi;
using System.Data;
using System.Data.SqlClient;

namespace AplikacioniSloj
{
    public class clsKorisnikServis
    {
        private readonly IKorisnikRepo _repoKorisnik;

        // Konstruktor
        public clsKorisnikServis(IKorisnikRepo repoKorisnik)
        {
            _repoKorisnik = repoKorisnik;
        }

        // Metod za prikaz svih korisnika
        public DataSet Prikazi()
        {
            return _repoKorisnik.PrikaziSveKorisnike();
        }

        public DataSet Prikazi(string prezime)
        {
            return _repoKorisnik.PronadjiKorisnikaPoPrezimenu(prezime);
        }

        // Metod za prikaz svih korisnika sa računima
        public DataSet PrikaziKorisnikeSaRacunima()
        {
            return _repoKorisnik.PrikaziKorisnikeSaRacunima();
        }

        // Metod za kreiranje novog korisnika
        public bool DodajKorisnika(clsKorisnik korisnik)
        {
            return _repoKorisnik.KreirajKorisnika(korisnik);
        }

        // Metod za brisanje korisnika na osnovu JMBG-a
        public bool ObrisiKorisnika(string jmbg)
        {
            return _repoKorisnik.ObrisiKorisnika(jmbg);
        }

        // Metod za izmenu postojećeg korisnika na osnovu JMBG-a
        public bool IzmeniKorisnika(string JMBG, clsKorisnik noviKorisnik)
        {
            return _repoKorisnik.IzmeniKorisnika(JMBG, noviKorisnik);
        }

        // Metod za pronalaženje korisnika na osnovu JMBG-a
        public clsKorisnik PronadjiPoJMBG(string jmbg)
        {
            return _repoKorisnik.PronadjiPoJMBG(jmbg);
        }

        // Metod za pronalaženje korisnika na osnovu email-a
        public clsKorisnik PronadjiPoEmailu(string email)
        {
            return _repoKorisnik.PronadjiPoEmail(email);
        }
    }
}