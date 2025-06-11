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
                    People person = People.CreateReader(reader);
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
            finally
            {
                if (conn != null)
                {
                    _mySQL.CloseConnection(conn);
                }
            }
        }
        public People SearchPersonBySecretCode(string secretCode)
        {
            MySqlConnection? conn = null;
            try
            {
                conn = _mySQL.GetConnection();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM people WHERE people.secret_name = '{secretCode}';", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    People person = People.CreateReader(reader); 
                    return person;

                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    _mySQL.CloseConnection(conn);
                }
            }
        }
        public People InsertPerson(People person)
        {
            MySqlConnection? conn = null;
            try
            {
                conn = _mySQL.GetConnection();
                string query = $"INSERT INTO people(first_name,last_name,secret_name,type,num_reports,num_mentions)" +
                    $"VALUES('{person.FirstName}','{person.LastName}','{person.SecretCode}','{person.Type}','{person.NumReport}','{person.NumMentions}');";
                MySqlCommand cmd = new MySqlCommand(query,conn);
                cmd.ExecuteNonQuery();
                People people = SearchPersonBySecretCode(person.SecretCode);
                return people;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    _mySQL.CloseConnection(conn);
                }
            }
        }
        public People UpdatePerson(People person)
        {
            MySqlConnection? conn = null;
            try
            {
                conn = _mySQL.GetConnection();
                string query = $"UPDATE people SET people.type = '{person.Type}',people.num_reports = '{person.NumReport}',people.num_mentions = '{person.NumMentions}' WHERE people.id = {person.Id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                return person;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    _mySQL.CloseConnection(conn);
                }
            }
        }
    }
}
