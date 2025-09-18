using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeneszamok.Models;

namespace Zeneszamok.Controllers
{
    public class LemezController
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

            public List<Lemez> LemezekListaja()
            {
                List<Lemez> lemezLista = new List<Lemez>();
                BuildConnection();
                SQLConnection.Open();
                string sql = "SELECT * FROM lemez";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = SQLConnection;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Lemez e = new Lemez();
                    e.Id = reader.GetInt32("id");
                    e.Cim = reader.GetString("Cim");
                    e.KiadasEve = reader.GetInt32("KiadasEve");
                    e.Kiado = reader.GetString("Kiado");
                    lemezLista.Add(e);
                    //A beolvasott adatok feldolgozása
                }
                SQLConnection.Close();
                return lemezLista;
            }

        public string LemezFelvitele(Lemez rogzitendo)
        {
            BuildConnection();
            SQLConnection.Open();
            string sql = "INSERT INTO lemez (Id, Cim, KiadasEve,Kiado) VALUES (@id, @Cim, @KiadasEve,@Kiado)";
            MySqlCommand cmd = new MySqlCommand(sql, SQLConnection);
            cmd.Parameters.AddWithValue("@id", rogzitendo.Id);
            cmd.Parameters.AddWithValue("@Cim", rogzitendo.Cim);
            cmd.Parameters.AddWithValue("@KiadasEve", rogzitendo.KiadasEve);
            cmd.Parameters.AddWithValue("@Kiado", rogzitendo.Kiado);
            int erintettSorok = cmd.ExecuteNonQuery();
            SQLConnection.Close();
            return erintettSorok > 0 ? "Sikeres rögzítés" : "Sikertelen rögzítés";
        }

        public string LemezModositas(Lemez modositando)
        {
            BuildConnection();
            SQLConnection.Open();
            string sql = "UPDATE  lemez SET Id = @id, Cim = @Cim, KiadasEve = @KiadasEve , Kiado=@Kiado WHERE Id =@id";
            MySqlCommand cmd = new MySqlCommand(sql, SQLConnection);
            cmd.Parameters.AddWithValue("@id", modositando.Id);
            cmd.Parameters.AddWithValue("@CIm", modositando.Cim);
            cmd.Parameters.AddWithValue("@KiadasEve", modositando.KiadasEve);
            cmd.Parameters.AddWithValue("@Kiado", modositando.Kiado);
            int erintettSorok = cmd.ExecuteNonQuery();
            SQLConnection.Close();
            return erintettSorok > 0 ? "Sikeres Módosítás" : "Sikertelen Módosítás";
        }

        public string LemezTorlese(int id)
        {
            BuildConnection();
            SQLConnection.Open();
            string sql = "DELETE from lemez WHERE Id =@id ";
            MySqlCommand cmd = new MySqlCommand(sql, SQLConnection);
            cmd.Parameters.AddWithValue("@id", id);

            int erintettSorok = cmd.ExecuteNonQuery();
            SQLConnection.Close();
            return erintettSorok > 0 ? "Sikeres törlés" : "Hiba a törlés során";
        }

    }
    }

