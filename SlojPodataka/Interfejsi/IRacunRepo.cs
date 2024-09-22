using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IRacunRepo
    {
        // Metoda za izlistavanje svih računa sa njihovim ID-jem, imenom i prezimenom korisnika, i stanjem na računu.
        DataSet PrikaziSveRacune();

        // Metoda za prikaz stanja računa korisnika na osnovu JMBG-a.
        DataSet PrikaziStanjeRacunaKorisnika(string jmbgKorisnika);

        // Metoda za brisanje računa na osnovu ID-a računa.
        bool ObrisiRacun(int idRacuna);
    }
}
