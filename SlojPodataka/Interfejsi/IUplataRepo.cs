using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IUplataRepo
    {
        // Metoda za prikaz uplata po korisnicima.
        DataSet PrikaziUplatePoKorisnicima();

        // Metoda za prikaz uplata koje su na čekanju.
        DataSet PrikaziUplateNaCekanju();

        // Metoda za prikaz uplata za određenog korisnika na osnovu JMBG-a.
        DataSet PrikaziUplateZaKorisnika(string jmbgKorisnika);

        // Metoda za kreiranje nove uplate za određenog korisnika i račun.
        bool KreirajUplatu(string jmbgKorisnika, int idRacuna, decimal iznos);

        // Metoda za odobravanje uplate.
        bool OdobriUplatu(int idUplate);

        bool OdbijUplatu(int idUplate);
    }
}
