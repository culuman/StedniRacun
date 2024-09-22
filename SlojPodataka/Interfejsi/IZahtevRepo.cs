using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IZahtevRepo
    {
        // Metoda za prikaz svih zahteva za otvaranje.
        DataSet PrikaziZahteveZaOtvaranje();

        // Metoda za prikaz zahteva po korisniku na osnovu JMBG-a.
        DataSet PrikaziZahtevePoKorisniku(string jmbgKorisnika);

        // Metoda za otvaranje zahteva na osnovu JMBG-a korisnika.
        bool OtvoriZahtev(string jmbgKorisnika);

        // Metoda za odobravanje zahteva.
        bool OdobriZahtev(int idZahteva, string brojRacuna);

        bool OdbijZahtev(int idZahteva);
    }
}
