using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.DB;
using Malshinon.Models;
using MySql.Data.MySqlClient;

namespace Malshinon.DAL
{
    public class PeopleDAL
    {
        private MySQLData _mySQL;
        public PeopleDAL(MySQLData mySQL)
        {
            _mySQL = mySQL;
        }
        public People SearchPersonById(int id)
        {
            MySqlConnection? conn = null;
            try
            {
                conn = _mySQL.GetConnection();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM people WHERE people.id = '{id}';", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    
                    int ID = reader.GetInt32("id");
                    string firstName = reader.GetString("first_name");
                    string lastName = reader.GetString("last_name");
                    string secretCode = reader.GetString("secret_code");
                    string type = reader.GetString("type");
                    int numReport = reader.GetInt32("num_report");
                    int numMentions = reader.GetInt32("num_mentions");
                    People person = new People(firstName, lastName, secretCode, type, numReport, numMentions, ID);
                    return person;
                    
                }
                else
                {
                    return null;
                }
             
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
