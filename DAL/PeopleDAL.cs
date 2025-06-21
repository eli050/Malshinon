using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.DB;
using Malshinon.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

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
                    $"VALUES('{person.FirstName}','{person.LastName}','{person.SecretCode}','{person.Type}',{person.NumReport},{person.NumMentions});";
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
                string query = $"UPDATE people SET people.type = '{person.Type}',people.num_reports = {person.NumReport},people.num_mentions = {person.NumMentions} WHERE people.id = {person.Id};";
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
        public List<AgentReports> GetSumAndLenOfAgentReports()
        {
            MySqlConnection? conn = null;
            try
            {
                conn = _mySQL.GetConnection();
                string query = "SELECT people.type, people.secret_name, people.id," +
                    " COUNT(intel_reports.reporter_id) AS sum_reprts," +
                    " AVG( CHAR_LENGTH(intel_reports.text) )" +
                    " AS avg_len FROM people JOIN intel_reports " +
                    "ON intel_reports.reporter_id = people.id " +
                    "GROUP BY people.secret_name, people.id, " +
                    "intel_reports.text HAVING people.type = 'potential_agent';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<AgentReports> agents = new List<AgentReports>();
                    while (reader.Read())
                    {
                        agents.Add(new AgentReports
                        {
                            secretName = reader.GetString("secret_name"),
                            numReports = reader.GetInt32("sum_reprts"),
                            AVGLen = reader.GetDouble("avg_len")

                        });
                        
                    }
                    return agents;
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
    }
}
