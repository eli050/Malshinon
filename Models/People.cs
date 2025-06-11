using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;

namespace Malshinon.Models
{
    public class People
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string SecretCode { get; private set; }
        private string _type;
        public string Type 
        {
            get
            {
                return _type;
            }
            set 
            {
                string[] types = new string[] { "reporter", "target", "both", "potential_agent" };
                if (types.Contains(value))
                {
                    _type = value;
                }
            } 
        }
        public int NumReport { get; private set; }
        public int NumMentions { get; private set; }

        public People(string firstname, string lastName, 
            string secretCode, string type,
            int numReport, int numMentions, int id = 0)
        {
            FirstName = firstname;
            LastName = lastName;
            SecretCode = secretCode;
            _type = type;
            NumReport = numReport;
            NumMentions = numMentions;
            Id = id;
        }
        public void IncNumReport()
        {
            NumReport++;
        }
        public void IncNumMentions()
        {
            NumMentions++;
        }
        public static People CreateReader(MySqlDataReader reader)
        {
            reader.Read();
            int ID = reader.GetInt32("id");
            string firstName = reader.GetString("first_name");
            string lastName = reader.GetString("last_name");
            string SecretCode = reader.GetString("secret_name");
            string type = reader.GetString("type");
            int numReport = reader.GetInt32("num_reports");
            int numMentions = reader.GetInt32("num_mentions");
            People person = new People(firstName, lastName, SecretCode, type, numReport, numMentions, ID);
            return person;
        }
    }
}
