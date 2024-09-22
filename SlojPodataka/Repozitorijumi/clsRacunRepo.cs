using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Interfejsi;

namespace SlojPodataka.Repozitorijumi
{
    public class clsRacunRepo : IRacunRepo
    {
        //polje konekcije
        private string _stringKonekcije;

        //konstruktor
        //pri pozivanju dobija se string konekcije
        public clsRacunRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        // izlistava sve racune i prikazuje detalje
        public DataSet PrikaziSveRacune()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("PrikaziSveRacune", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }


        public DataSet PrikaziStanjeRacunaKorisnika(string jmbgKorisnika)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("PrikaziStanjeRacunaKorisnika", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@JMBGKorisnika", SqlDbType.NVarChar).Value = jmbgKorisnika;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }


        public bool ObrisiRacun(int idRacuna)
        {
            //promenljiva za proveru uspesnosti unosa 
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("ObrisiRacun", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDRacuna", SqlDbType.Int).Value = idRacuna;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //vraca true ako je uspesno
            return (proveraUnosa > 0);
        }

    }
}
