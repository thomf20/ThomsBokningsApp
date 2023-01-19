using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using ThomsBokningsApp.Models;
using Dapper;
using System.Security.Cryptography.X509Certificates;

namespace ThomsBokningsApp
{
    public class GetDapperData
    {
        public static void BestBoat()
        {
            string connString = "Server = tcp:thomsbokningsapp.database.windows.net, 1433; Initial Catalog = BookingDB; Persist Security Info = False; User ID = thomadmin; Password = Systemutvecklare22; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";

            string sql = "SELECT Bookings.Available, Bookings.BoatNumber, COUNT(Available) AS AntalBokningar FROM dbo.Bookings WHERE Available = 0 Group By Available, BoatNumber ORDER BY AntalBokningar desc";

            var bestBoat = new List<Booking>();
            {
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    bestBoat = connection.Query<Booking>(sql).ToList();
                    connection.Close();
                }
                Console.WriteLine("Mest bokade båt högst upp");
                foreach(var b in bestBoat)
                {
                    Console.WriteLine($"Båt nummer: {b.BoatNumber}");
                }
            }
        }
        public static void MostBookedDay()
        {
            string connString = "Server = tcp:thomsbokningsapp.database.windows.net, 1433; Initial Catalog = BookingDB; Persist Security Info = False; User ID = thomadmin; Password = Systemutvecklare22; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";

            string sql = "SELECT WeekDay, COUNT(WeekDay) AS TimesBooked FROM dbo.Bookings WHERE Available = 0 Group By WeekDay ORDER BY WeekDay desc";

            var bestBoat = new List<Booking>();
            {
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    bestBoat = connection.Query<Booking>(sql).ToList();
                    connection.Close();
                }
                Console.WriteLine("Populäraste dagen högst upp: ");
                foreach (var b in bestBoat)
                {
                    Console.WriteLine($"{b.WeekDay}");
                }
            }
        }
        public static void MostPopularGroupAmount()
        {
            string connString = "Server = tcp:thomsbokningsapp.database.windows.net, 1433; Initial Catalog = BookingDB; Persist Security Info = False; User ID = thomadmin; Password = Systemutvecklare22; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";

            string sql = "SELECT Persons.NumberOfPeople, COUNT(NumberOfPeople) AS Antal FROM dbo.Persons GROUP BY Persons.NumberOfPeople ORDER BY Antal desc";

            var popularGroups = new List<Person>();
            {
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    popularGroups = connection.Query<Person>(sql).ToList();
                    connection.Close();
                }
                Console.WriteLine("Populäraste gruppantal högst upp: ");
                foreach (var p in popularGroups)
                {
                    Console.WriteLine($"{p.NumberOfPeople}");
                }
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
