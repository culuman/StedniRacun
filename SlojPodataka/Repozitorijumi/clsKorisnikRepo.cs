using SlojPodataka;
using SlojPodataka.Interfejsi;
using System.Data;
using System.Data.SqlClient;

namespace SlojPodataka.Repozitorijumi
{
    public class clsKorisnikRepo : IKorisnikRepo
    {
        //polje konekcije
        private string _stringKonekcije;

        //konstruktor
        //dobija se string konekcije pri pozivanju
        public clsKorisnikRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        public DataSet PrikaziSveKorisnike()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("PrikaziSveKorisnike", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        //izlistava sve korisnike koji nisu admin i daje njihov jmbg, ime, prezime, e-mail
        //i preko join-a daje stanje sa njihovih racuna
        public DataSet PrikaziKorisnikeSaRacunima()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("PrikaziKorisnikeSaRacunima", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }


        public bool KreirajKorisnika(clsKorisnik objKreirajKorisnika)
        {
            //promenljiva za proveru unosa podataka
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("KreirajKorisnika", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = objKreirajKorisnika.Jmbg;
            Komanda.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = objKreirajKorisnika.Ime;
            Komanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = objKreirajKorisnika.Prezime;
            Komanda.Parameters.Add("@Email", SqlDbType.NVarChar).Value = objKreirajKorisnika.Email;
            Komanda.Parameters.Add("@Lozinka", SqlDbType.NVarChar).Value = objKreirajKorisnika.Lozinka;
            Komanda.Parameters.Add("@Godine", SqlDbType.Int).Value = objKreirajKorisnika.Godine;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //vraca true ako je uspesno
            return (proveraUnosa > 0);
        }

        public bool ObrisiKorisnika(string jmbg)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("ObrisiKorisnika", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = jmbg;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);

        }

      
        public bool IzmeniKorisnika(string JMBG, clsKorisnik objNoviKorisnik)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("IzmeniKorisnika", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = JMBG;
            Komanda.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = objNoviKorisnik.Ime;
            Komanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = objNoviKorisnik.Prezime;
            Komanda.Parameters.Add("@Email", SqlDbType.NVarChar).Value = objNoviKorisnik.Email;
            Komanda.Parameters.Add("@Lozinka", SqlDbType.NVarChar).Value = objNoviKorisnik.Lozinka;
            Komanda.Parameters.Add("@Godine", SqlDbType.Int).Value = objNoviKorisnik.Godine;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }

        public clsKorisnik PronadjiPoJMBG(string jmbg)
        {
            using (SqlConnection Veza = new SqlConnection(_stringKonekcije))
            {

                Veza.Open();
                SqlCommand Komanda = new SqlCommand("PronadjiKorisnikaPoJMBG", Veza);
                Komanda.CommandType = CommandType.StoredProcedure;
                Komanda.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = jmbg;

                using (SqlDataReader Reader = Komanda.ExecuteReader())
                {
                    if (Reader.Read())
                    {
                        return MapirajRedUObjekat(Reader);
                    }
                    else
                    {
                        return null; // Nema pronađenog korisnika sa datim JMBG-om
                    }
                }
            }
        }

        public clsKorisnik PronadjiPoEmail(string email)
        {
            using (SqlConnection Veza = new SqlConnection(_stringKonekcije))
            {
                
                Veza.Open();
                SqlCommand Komanda = new SqlCommand("PronadjiKorisnikaPoEmailu", Veza);
                Komanda.CommandType = CommandType.StoredProcedure;
                Komanda.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;

                using (SqlDataReader Reader = Komanda.ExecuteReader())
                {
                    if (Reader.Read())
                    {
                        return MapirajRedUObjekat(Reader);
                    }
                    else
                    {
                        return null; // Nema pronađenog korisnika sa datim email-om
                    }
                }
            }
        }

        public DataSet PronadjiKorisnikaPoPrezimenu(string Prezime)
        {

            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("PronadjiKorisnikaPoPrezimenu", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = Prezime;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        private clsKorisnik MapirajRedUObjekat(SqlDataReader reader)
        {
            return new clsKorisnik
            {
                Jmbg = reader["JMBG"].ToString(),
                Ime = reader["Ime"].ToString(),
                Prezime = reader["Prezime"].ToString(),
                Email = reader["Email"].ToString(),
                Lozinka = reader["Lozinka"].ToString(),
                Godine = (int)reader["Godine"],
                TipKorisnika = reader["TipKorisnika"].ToString()
            };
        }

    }
}