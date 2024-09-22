using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IKorisnikRepo
    {
        // Metoda za prikaz svih korisnika
        DataSet PrikaziSveKorisnike();

        // Metoda za prikaz svih korisnika sa računima
        DataSet PrikaziKorisnikeSaRacunima();

        // Metoda za kreiranje novog korisnika
        bool KreirajKorisnika(clsKorisnik objKreirajKorisnika);

        // Metoda za brisanje korisnika na osnovu JMBG-a
        bool ObrisiKorisnika(string JMBG);

        // Metoda za izmenu postojećeg korisnika na osnovu JMBG-a
        bool IzmeniKorisnika(string JMBG, clsKorisnik objNoviKorisnik);

        // Metoda za pronalaženje korisnika na osnovu JMBG-a
        clsKorisnik PronadjiPoJMBG(string jmbg);

        // Metoda za pronalaženje korisnika na osnovu email-a
        clsKorisnik PronadjiPoEmail(string email);

        DataSet PronadjiKorisnikaPoPrezimenu(string Prezime);
    }
}
