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
    public class clsZahtevRepo : IZahtevRepo
    {
        //polje konekcije
        private string _stringKonekcije;

        //konstruktor
        //dobija se string konekcije pri pozivanju
        public clsZahtevRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        public DataSet PrikaziZahteveZaOtvaranje()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("PrikaziZahteveZaOtvaranje", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet PrikaziZahtevePoKorisniku(string jmbgKorisnika)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("PrikaziZahtevePoKorisniku", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@JMBGKorisnika", SqlDbType.VarChar).Value = jmbgKorisnika;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }



        public bool OtvoriZahtev(string jmbgKorisnika)
        {
            //promenljiva za proveru uspesnosti unosa 
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("OtvoriZahtev", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@JMBGKorisnika", SqlDbType.NVarChar).Value = jmbgKorisnika;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //vraca true ako je uspesno
            return (proveraUnosa > 0);
        }

        

        public bool OdobriZahtev(int idZahteva, string brojRacuna)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("OdobriZahtevIKreirajRacun", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDZahteva", SqlDbType.VarChar).Value = idZahteva;
            Komanda.Parameters.Add("@BrojRacuna", SqlDbType.VarChar).Value = brojRacuna;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //vraca true ako je uspesno
            return (proveraUnosa > 0);
        }

        public bool OdbijZahtev(int IDZahteva)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("OdbijZahtev", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDZahteva", SqlDbType.Int).Value = IDZahteva;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }
    }
}
