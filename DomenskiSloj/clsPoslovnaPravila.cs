
using SlojPodataka;
using SlojPodataka.Interfejsi;
using System.Data;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;

namespace DomenskiSloj
{
    public class clsPoslovnaPravila
    {
        private readonly IKorisnikRepo _repoKorisnik;
        private readonly IUplataRepo _repoUplata;
        private readonly string _putanjaMinimalnaStarost = @"D:\Programs\Visual Studio\Projects\StedniRacun\XMLOgranicenja\MinimalnaStarost.xml";
        private readonly string _putanjaMaksimalnaUplata = @"D:\Programs\Visual Studio\Projects\StedniRacun\XMLOgranicenja\MaksimalnaUplata.xml";

        // Konstruktor
        public clsPoslovnaPravila(IKorisnikRepo repoKorisnik, IUplataRepo repoUplata)
        {
            _repoKorisnik = repoKorisnik;
            _repoUplata = repoUplata;
        }

        // Metod za proveru minimalne starosti korisnika
        public bool ProveraStarosti(string jmbgKorisnika)
        {
            var korisnik = _repoKorisnik.PronadjiPoJMBG(jmbgKorisnika);

            if (korisnik == null)
                return false;

            int minimalnaStarost;

            if (!File.Exists(_putanjaMinimalnaStarost))
            {
                throw new FileNotFoundException("File not found", _putanjaMinimalnaStarost);
            }

            minimalnaStarost = UčitajMinimalnuStarostIzXml();

            return korisnik.Godine >= minimalnaStarost;
        }

        private int UčitajMinimalnuStarostIzXml()
        {
            XDocument xml = XDocument.Load(_putanjaMinimalnaStarost);
            var element = xml.Element("poslovnaPravila")?.Element("minimalnaStarost");

            if (element == null)
            {
                throw new InvalidOperationException("XML does not contain 'minimalnaStarost' element.");
            }

            return int.Parse(element.Value);
        }

        // Metod za generisanje nasumičnog 18-cifrenog broja koji predstavlja broj računa
        public string GenerisiBrojRacuna()
        {
            Random r = new Random();
            string brojRacuna = "";

            for (int i = 0; i < 18; i++)
            {
                brojRacuna += r.Next(0, 10).ToString();
            }
            return brojRacuna;
        }

        // Metod za proveru maksimalne uplate
        public bool ProveraMaksimalneUplate(decimal iznosUplate)
        {
            if (!File.Exists(_putanjaMaksimalnaUplata))
            {
                throw new FileNotFoundException("File not found", _putanjaMaksimalnaUplata);
            }

            decimal maksimalnaUplata = UčitajMaksimalnuUplatuIzXml();
            return iznosUplate <= maksimalnaUplata;
        }

        private decimal UčitajMaksimalnuUplatuIzXml()
        {
            XDocument xml = XDocument.Load(_putanjaMaksimalnaUplata);
            var element = xml.Element("poslovnaPravila")?.Element("maksimalnaUplata");

            if (element == null)
            {
                throw new InvalidOperationException("XML does not contain 'maksimalnaUplata' element.");
            }

            return decimal.Parse(element.Value);
        }
    }
}