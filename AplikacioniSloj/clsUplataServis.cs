using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Interfejsi;
using DomenskiSloj;
using SlojPodataka;

namespace AplikacioniSloj
{
    public class clsUplataServis
    {
        private readonly IUplataRepo _repoUplata;
        private readonly clsPoslovnaPravila _poslovnaPravila;

        // Konstruktor prima repozitorijum za uplate i instancu poslovnih pravila
        public clsUplataServis(IUplataRepo repoUplata, clsPoslovnaPravila poslovnaPravila)
        {
            _repoUplata = repoUplata;
            _poslovnaPravila = poslovnaPravila;
        }

        // Metod za kreiranje nove uplate
        public bool KreirajUplatu(string jmbgKorisnika, int idRacuna, decimal iznos)
        {
            // Provera maksimalne uplate
            if (!_poslovnaPravila.ProveraMaksimalneUplate(iznos))
            {
                throw new InvalidOperationException("Iznos uplate premašuje dozvoljeni maksimum.");
            }

            // Kreiranje nove uplate kroz repozitorijum
            return _repoUplata.KreirajUplatu(jmbgKorisnika, idRacuna, iznos);
        }

        // Metod za odobravanje uplate
        public bool OdobriUplatu(int idUplate)
        {
            return _repoUplata.OdobriUplatu(idUplate);
        }

        // Metod za prikaz uplata po korisnicima
        public DataSet PrikaziUplatePoKorisnicima()
        {
            return _repoUplata.PrikaziUplatePoKorisnicima();
        }

        // Metod za prikaz uplata koje su na čekanju
        public DataSet PrikaziUplateNaCekanju()
        {
            return _repoUplata.PrikaziUplateNaCekanju();
        }

        // Metod za prikaz uplata za određenog korisnika
        public DataSet PrikaziUplateZaKorisnika(string jmbgKorisnika)
        {
            return _repoUplata.PrikaziUplateZaKorisnika(jmbgKorisnika);
        }
        public bool Odbij(int IDUplate)
        {
            return _repoUplata.OdbijUplatu(IDUplate);
        }
    }
}
