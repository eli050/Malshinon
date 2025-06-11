using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.Models;
using Malshinon.DB;

namespace Malshinon.DAL.Controlers
{
    static public class Management
    {
        static private MySQLData _MySQL = new MySQLData();
        static private PeopleDAL peopleDAL = new PeopleDAL(_MySQL);
        static private IntelReportsDAL reportsDAL = new IntelReportsDAL(_MySQL);
        static public void Control()
        {
            
            Console.WriteLine("Enter your secret name: ");
            string secretNameR = Console.ReadLine()!;
            People reporter = LogIn(secretNameR);
            if (reporter == null)
            {
                Console.WriteLine("You do not exist in the system, let's register.");
                Console.WriteLine("Enter your first name: ");
                string firstName = Console.ReadLine()!;
                Console.WriteLine("Enter your last name: ");
                string lastName = Console.ReadLine()!;
                string typeReporter = "reporter";
                reporter = SignIn(firstName, lastName, secretNameR, typeReporter);
                Console.WriteLine("Great, now you're registered in the system, let's start reporting.");
            }
            else
            {
                if(reporter.NumMentions > 0)
                {
                    reporter.Type = "both";
                }
                Console.WriteLine("Great, you're in the system, let's start reporting.");
            }
            Console.WriteLine("Enter the secret name of the\n " +
                "person you want to report.");
            string secretNameT = Console.ReadLine()!;
            People Target = LogIn(secretNameT);
            if(Target == null)
            {
                Console.WriteLine("The above person does not exist in the system, start registration.");
                Console.WriteLine("Enter first name: ");
                string firstName = Console.ReadLine()!;
                Console.WriteLine("Enter last name: ");
                string lastName = Console.ReadLine()!;
                string typeTarget = "target";
                Target = SignIn(firstName, lastName, secretNameR, typeTarget);
                Console.WriteLine("Great, now that it's in the system, you can continue with the report.");
            }
            else
            {
                if(Target.NumReport > 0)
                {
                    Target.Type = "both";
                }
            }
            reporter.IncNumReport();
            Target.IncNumMentions();
            if(Target.NumMentions > 20)
            {
                Console.WriteLine($"⚠️ FYI‼️ The person {Target.FirstName} {Target.LastName} is dangerous target");
            }
            if (reporter.NumReport > 10 && AVG100Reports(reporter.Id) && reporter.Type != "both")
            {
                reporter.Type = "potential_agent";
            }
            Console.WriteLine("Enter a report message: ");
            string text = Console.ReadLine()!;
            MakeMessege(reporter.Id, Target.Id, text);
            Update(Target);
            Update(reporter);
            

        }
        static private People LogIn(string secretName)
        {
            People person = peopleDAL.SearchPersonBySecretCode(secretName);
            return person;
        }
        static private People SignIn(string FirstName, string LastName, string SecretName, string Type)
        {
            People person = new People(FirstName, LastName, SecretName, Type);
            person = peopleDAL.InsertPerson(person);
            return person;

        }
        static private void MakeMessege(int ReporterId, int TargetId, string Text)
        {
            IntelReports intelReports = new IntelReports(ReporterId,TargetId,Text);
            reportsDAL.CreateIntelReport(intelReports);

        }
        static private People Update(People people)
        {
            People person = peopleDAL.UpdatePerson(people);
            return person;
        }
        static private bool AVG100Reports(int reporterId)
        {
            List<IntelReports> intels = reportsDAL.SearchByReporterId(reporterId);
            int counter = 0;
            int sum = 0;
            foreach(IntelReports intel in intels)
            {
                counter++;
                sum += intel.Text.Length;
            }
            bool AVG = sum / counter >= 100;
            return AVG;
        }

    }
}
