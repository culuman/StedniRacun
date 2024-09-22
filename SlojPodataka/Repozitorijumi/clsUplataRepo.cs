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
    public class clsUplataRepo : IUplataRepo
    {
        //polje konekcije
        private string _stringKonekcije;

        //konstruktor
        //dobija se string konekcije pri pozivanju
        public clsUplataRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        public DataSet PrikaziUplatePoKorisnicima()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("PrikaziUplatePoKorisnicima", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet PrikaziUplateNaCekanju()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("PrikaziUplateNaCekanju", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet PrikaziUplateZaKorisnika(string jmbgKorisnika)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("PrikaziUplateZaKorisnika", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@JMBGKorisnika", SqlDbType.NVarChar).Value = jmbgKorisnika;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public bool KreirajUplatu(string jmbgKorisnika, int idRacuna, decimal iznos)
        {
            //promenljiva za proveru uspesnosti unosa 
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("KreirajUplatu", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@JMBGKorisnika", SqlDbType.NVarChar).Value = jmbgKorisnika;
            Komanda.Parameters.Add("@IDRacuna", SqlDbType.Int).Value = idRacuna;
            Komanda.Parameters.Add("@Iznos", SqlDbType.Decimal).Value = iznos;
            DateTime datum = DateTime.Now;
            Komanda.Parameters.Add("@Datum", SqlDbType.Date).Value = datum.Date;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //vraca true ako je uspesno
            return (proveraUnosa > 0);
        }

        public bool OdobriUplatu(int idUplate)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("OdobriUplatuIDodajNaRacun", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDUplate", SqlDbType.Int).Value = idUplate;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //vraca true ako je uspesno
            return (proveraUnosa > 0);
        }

        public bool OdbijUplatu(int IDUplate)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("OdbijUplatu", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDUplate", SqlDbType.Int).Value = IDUplate;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }
    }
}
