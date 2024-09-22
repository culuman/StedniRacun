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
    public class clsRacunServis
    {
        private IRacunRepo _repo;

        // Konstruktor
        public clsRacunServis(IRacunRepo repo)
        {
            _repo = repo;
        }

        // Metod za prikaz svih računa
        public DataSet Prikazi()
        {
            return _repo.PrikaziSveRacune();
        }

        // Metod za prikaz stanja računa korisnika
        public DataSet PrikaziStanjeRacuna(string jmbgKorisnika)
        {
            return _repo.PrikaziStanjeRacunaKorisnika(jmbgKorisnika);
        }

        // Metod za brisanje računa
        public bool ObrisiRacun(int idRacuna)
        {
            return _repo.ObrisiRacun(idRacuna);
        }
    }
}
