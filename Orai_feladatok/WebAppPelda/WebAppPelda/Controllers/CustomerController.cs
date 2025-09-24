using MySql.Data.MySqlClient;
using WebAppPelda.Models;

namespace WebAppPelda.Controllers
{
    public class CustomerController
    {

        //adatbázizból olvasás
        public static MySqlConnection SQLConnection;
        public static void BuildConnection()
        {
            //MYSQL connection létrehozása
            //Adatbázis lekérdezés
            //Eredmény feldolgozása
            string connectionString = "SERVER = localhost;" +
                         "DATABASE= uzlet;" +
                         "UID = root;" +
                         "PASSWORD =;" +
                         "SSL MODE= none;";
            SQLConnection = new MySqlConnection();
            SQLConnection.ConnectionString = connectionString;
        }

        public List<Customer> GetCustomersFromDatabase() 
        {
            BuildConnection();
            SQLConnection.Open();
            string sql = "SELECT * FROM customer";
            MySqlCommand command = new MySqlCommand(sql, SQLConnection);
            MySqlDataReader reader = command.ExecuteReader();
            List<Customer> customers = new List<Customer>();
            while (reader.Read())
            {
                Customer customer = new Customer();
                customer.Id = Convert.ToInt32(reader["id"]);
                customer.Name = reader["name"].ToString();
                customer.Phone = reader["phone"].ToString();
                customer.Score = Convert.ToInt32(reader["score"]);
                customers.Add(customer);

            }
            SQLConnection.Close();
            return customers;
        }
        
           

        public List<Customer> GetCustomersFromFile()
        {
            //return new List<Customer>()
            //{
            //    new Customer() { Id = 1, Name = "Kiss János", Phone = "+36201234567", Score = 100 },
            //    new Customer() { Id = 2, Name = "Nagy Péter", Phone = "+36207654321", Score = 200 },
            //    new Customer() { Id = 3, Name = "Tóth Anna", Phone = "+36203456789", Score = 150 },
            //};


            //Szöveges állományból olvasás
            List<Customer> customers = new List<Customer>();
            string[] sorok = File.ReadAllLines("CustomersData.txt").Skip(1).ToArray();
            foreach (string sor in sorok)
            {
                string[] mezok = sor.Split(';');
                Customer customer = new Customer()
                {
                    Id = Convert.ToInt32(mezok[0]),
                    Name = mezok[1],
                    Phone = mezok[2],
                    Score = Convert.ToInt32(mezok[3])
                };
                customers.Add(customer);
            }
            return customers;
        }
    }
}
