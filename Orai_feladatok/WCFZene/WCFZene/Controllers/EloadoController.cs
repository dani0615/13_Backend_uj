using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFZene.Models;

namespace WCFZene.Controllers
{
    public class EloadoController
    {
        public static MySqlConnection SQLConnection;
        private static void BuildConnection()
        {
            string connectionString = "SERVER = localhost;" +
                          "DATABASE= zene;" +
                          "UID = root;" +
                          "PASSWORD =;" +
                          "SSL MODE= none;";
            SQLConnection = new MySqlConnection();
            SQLConnection.ConnectionString = connectionString;

        }

        public string EloadoTorlese(int id)
        {
            BuildConnection();
            SQLConnection.Open();
            string sql = "DELETE from eloado WHERE Id =@id  ";
            MySqlCommand cmd = new MySqlCommand(sql, SQLConnection);
            cmd.Parameters.AddWithValue("@id", id);

            int erintettSorok = cmd.ExecuteNonQuery();
            SQLConnection.Close();
            return erintettSorok > 0 ? "Sikeres törlés" : "Hiba a törlés során";
        }

        public string EloadoModositas(Eloado modositando)
        {
            BuildConnection();
            SQLConnection.Open();
            string sql = "UPDATE  eloado SET Nev = @nev, Nemzetiseg = @nemzetiseg, Szolo = @szolo WHERE Id =@id  ";
            MySqlCommand cmd = new MySqlCommand(sql, SQLConnection);
            cmd.Parameters.AddWithValue("@nev", modositando.Nev);
            cmd.Parameters.AddWithValue("@nemzetiseg", modositando.Nemzetiseg);
            cmd.Parameters.AddWithValue("@szolo", modositando.Szolo);
            cmd.Parameters.AddWithValue("@id", modositando.Id);
            int erintettSorok = cmd.ExecuteNonQuery();
            SQLConnection.Close();
            return erintettSorok > 0 ? "Sikeres Módosítás" : "Sikertelen Módosítás";
        }

        public string EloadoFelvitele(Eloado rogzitendo)
        {
            if (rogzitendo == null)
            {
                return "Hiba: Az eloado objektum nem lehet null.";
            }
            BuildConnection();
            SQLConnection.Open();
            string sql = "INSERT INTO eloado (Nev, Nemzetiseg, Szolo) VALUES (@nev, @nemzetiseg, @szolo)";
            MySqlCommand cmd = new MySqlCommand(sql, SQLConnection);
            cmd.Parameters.AddWithValue("@nev", rogzitendo.Nev);
            cmd.Parameters.AddWithValue("@nemzetiseg", rogzitendo.Nemzetiseg);
            cmd.Parameters.AddWithValue("@szolo", rogzitendo.Szolo);
            int erintettSorok = cmd.ExecuteNonQuery();
            SQLConnection.Close();
            return erintettSorok > 0 ? "Sikeres rögzítés" : "Sikertelen rögzítés";
        }

        public List<Eloado> EloadokListaja()
        {
            List<Eloado> eloadoLista = new List<Eloado>();
            MySqlDataReader reader = null;
            try
            {
                BuildConnection();
                SQLConnection.Open();
                string sql = "SELECT * FROM eloado";
                MySqlCommand cmd = new MySqlCommand(sql, SQLConnection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Eloado e = new Eloado
                    {
                        Id = reader.GetInt32("Id"),
                        Nev = reader.GetString("Nev"),
                        Nemzetiseg = reader.IsDBNull(reader.GetOrdinal("Nemzetiseg")) ? null : reader.GetString("Nemzetiseg"),
                        Szolo = reader.GetBoolean("Szolo")
                    };
                    eloadoLista.Add(e);
                }
                return eloadoLista;
            }
            catch (Exception ex)
            {
                Eloado hiba = new Eloado
                {
                    Id = 0,
                    Nev = ex.Message
                };
                eloadoLista.Add(hiba);
                throw;


            }
        }

    }
}