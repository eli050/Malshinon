using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Malshinon.DB
{
    public class MySQLData
    {
        static string connectionString = "Server=localhos;Database=malshinondb;User=root;Port=3306;";
        public MySqlConnection? connection;
        public void Connect()
        {
            var conn = new MySqlConnection(connectionString);
            connection = conn;
            try
            {
                connection.Open();
                Console.WriteLine("Connected to MySQL data base successfully");
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"error conncting to MySQL database: {ex.Message}");
            }

        }
        public MySqlConnection? GetConnection()
        {
            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error from connection: {ex.Message}");
                return null;
            }
        }
        public void CloseConnection(MySqlConnection conn)
        {
            conn.Close();
        }
    }
}