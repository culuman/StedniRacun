using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Interfejsi;
using DomenskiSloj;

namespace AplikacioniSloj
{
    public class clsZahtevServis
    {
        private IZahtevRepo _repo;
        private clsPoslovnaPravila _poslovnaPravila;

        // Konstruktor
        public clsZahtevServis(IZahtevRepo repo, clsPoslovnaPravila poslovnaPravila)
        {
            _repo = repo;
            _poslovnaPravila = poslovnaPravila;
        }

        // Metoda za prikaz svih zahteva
        public DataSet Prikazi()
        {
            return _repo.PrikaziZahteveZaOtvaranje();
        }

        // Metoda za prikaz zahteva po korisniku na osnovu JMBG-a
        public DataSet PrikaziPoKorisniku(string jmbgKorisnika)
        {
            return _repo.PrikaziZahtevePoKorisniku(jmbgKorisnika);
        }

        // Metoda za otvaranje novog zahteva
        public bool OtvoriZahtev(string jmbgKorisnika)
        {
            // Proveri minimalnu starost korisnika
            if (!_poslovnaPravila.ProveraStarosti(jmbgKorisnika))
            {
                return false; // Korisnik nije ispunio uslove minimalne starosti
            }

            // Ako su uslovi ispunjeni, otvori novi zahtev
            return _repo.OtvoriZahtev(jmbgKorisnika);
        }

        // Metoda za odobravanje zahteva
        public bool OdobriZahtev(int idZahteva)
        {
            // Odobri zahtev i kreiraj račun
            string brojRacuna = _poslovnaPravila.GenerisiBrojRacuna();
            return _repo.OdobriZahtev(idZahteva, brojRacuna);
        }
        public bool Odbij(int IDZahteva)
        {
            return _repo.OdbijZahtev(IDZahteva);
        }
    }
}
