using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersenyNyilvantarto.Models
{
    public class FeladatController
    {
        static MySqlConnection sqlConnection;

        private static void BuildConnection()
        {
            string connectionString = "SERVER = localhost;" +
                                      "DATABASE= tesztverseny;" +
                                      "UID = root;" +
                                      "PASSWORD =;" +
                                      "SSL MODE= none;";
            sqlConnection = new MySqlConnection();
            sqlConnection.ConnectionString = connectionString;
        }
        public string FeladatTorlese(int id)
        {
            BuildConnection();
            sqlConnection.Open();
            string sql = "DELETE FROM feladat WHERE Id = @id";
            MySqlCommand cmd = new MySqlCommand(sql, sqlConnection);
            cmd.Parameters.AddWithValue("@id", id);
            int sorokSzama = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return sorokSzama > 0 ? "Sikeres törlés" : "Hiba a törlés során";
        }
        public string FeladatModositasa(Feladat modositando)
        {
            BuildConnection();
            sqlConnection.Open();
            string sql = "UPDATE feladat SET Id = @Id, Szoveg = @Szoveg, Valasz1 = @Valasz1, Valasz2 = @Valasz2 , Valasz3= @Valasz3, Helyes= @Helyes , Pont = @Pont  WHERE Id = @id";
            MySqlCommand cmd = new MySqlCommand(sql, sqlConnection);
            cmd.Parameters.AddWithValue("@Id", modositando.Id);
            cmd.Parameters.AddWithValue("@Szoveg", modositando.Szoveg);
            cmd.Parameters.AddWithValue("@Valasz1", modositando.Valasz1);
            cmd.Parameters.AddWithValue("@Valasz2", modositando.Valasz2);
            cmd.Parameters.AddWithValue("@Valasz3", modositando.Valasz3);
            cmd.Parameters.AddWithValue("@Helyes", modositando.HelyesValasz);
            cmd.Parameters.AddWithValue("@Pont", modositando.Pont);
            int sorokSzama = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return sorokSzama > 0 ? "Sikeres módosítás" : "Hiba a módosítás során";
        }

        public string FeladatFelvitele(Feladat rogzitendo)
        {
            BuildConnection();
            sqlConnection.Open();
            string sql = "INSERT INTO feladat(Id, Szoveg, Valasz1,Valasz2,Valasz3,Valasz4,Helyes,Pont) VALUES (@Id,@Szoveg,@Valasz1,@Valasz2,@Valasz3,@Valasz4,@Helyes,@Pont)";
            MySqlCommand cmd = new MySqlCommand(sql, sqlConnection);
            cmd.Parameters.AddWithValue("@Id", rogzitendo.Id);
            cmd.Parameters.AddWithValue("@Szoveg", rogzitendo.Szoveg);
            cmd.Parameters.AddWithValue("@Valasz1", rogzitendo.Valasz1);
            cmd.Parameters.AddWithValue("@Valasz2", rogzitendo.Valasz2);
            cmd.Parameters.AddWithValue("@Valasz3", rogzitendo.Valasz3);
            cmd.Parameters.AddWithValue("@Valasz4", rogzitendo.Valasz4);
            cmd.Parameters.AddWithValue("@Helyes", rogzitendo.HelyesValasz);
            cmd.Parameters.AddWithValue("@Pont", rogzitendo.Pont);
            int sorokSzama = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return sorokSzama > 0 ? "Sikeres felvitel" : "Hiba a felvitel során";
        }
        public List<Feladat> FeladatokListaja()
        {
            List<Feladat> feladatLista = new List<Feladat>();
            try
            {
                BuildConnection();
                sqlConnection.Open();
                string sql = "SELECT * FROM feladat";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = sqlConnection;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Feladat feladat = new Feladat();
                    feladat.Id = reader.GetInt32("Id");
                    feladat.Szoveg = reader.GetString("Szoveg");
                    feladat.Valasz1 = reader.GetString("Valasz1");
                    feladat.Valasz2 = reader.GetString("Valasz2");
                    feladat.Valasz3 = reader.GetString("Valasz3");
                    feladat.Valasz4 = reader.GetString("Valasz4");
                    feladat.HelyesValasz = reader.GetInt32("Helyes");
                    feladat.Pont = reader.GetInt32("Pont");
                    feladatLista.Add(feladat);
                }
                sqlConnection.Close();
                return feladatLista;
            }
            catch (Exception ex)
            {
                throw new Exception("Hiba az adatbázis lekérdezés során: " + ex.Message);
                //Feladat hiba = new Feladat()
                //{
                //    Id = 0,
                //    Szoveg = ex.Message
                //};
                //eloadoLista.Add(hiba);

            }
        }
        




        }
}
