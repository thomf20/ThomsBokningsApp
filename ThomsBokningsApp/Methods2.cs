using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThomsBokningsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace ThomsBokningsApp
{
    public class Methods2
    {
        public static void BoatValues()
        {
            Console.WriteLine("Välkommen till adminsidan, där du kan lägga till en båt.");
            Console.WriteLine("OBS! Du kan endast lägga till en båt åt gången.");
            Console.WriteLine("Båtnummer: ");
            var boatNr = int.Parse(Console.ReadLine());
            Console.WriteLine("Beskrivning: ");
            var description = Console.ReadLine();
            var weekDays = new List<string> { "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag" };
            var available = true;
            for (int i = 1; i < 53; i++)
            {
                foreach(var w in weekDays)
                {
                    AddBoat(boatNr, description, w, i, available);
                }
            }

        }
        public static void AddBoat(int boatNr, string description, string weekDays, int weekNr, bool available)
        {
            using (var db = new MyDBContext())
            {
                var newBooking = new Booking()
                {
                    BoatNumber = boatNr,
                    Description = description,
                    WeekDay = weekDays,
                    WeekNumber = weekNr,
                    Available = available
                };
                var bookingList = db.Bookings;
                bookingList.Add(newBooking);
                db.SaveChanges();
            }
        }
        public static void BookBoat(int personId)
        {
            Console.WriteLine("Veckonummer: ");
            int weekNr = int.Parse(Console.ReadLine());
            Console.WriteLine("Veckodag:");
            string weekDay = Console.ReadLine();
            Console.WriteLine("Båtnummer: ");
            var boatNr = int.Parse(Console.ReadLine());

            using (var db = new MyDBContext())
            {
                var booking = (from t in db.Bookings
                               where t.WeekNumber == weekNr && t.WeekDay == weekDay && t.BoatNumber == boatNr
                               select t).SingleOrDefault();

                booking.PersonId = personId;
                booking.Available = false;
                db.SaveChanges();
                Console.Clear();
                Console.WriteLine("Lyckad bokning");
                Console.ReadKey();
                Console.Clear();
            }

        }
        public static void PrintWeekValues()
        {
            using (var db = new MyDBContext())
            {
                Console.WriteLine("Ange vecka (1-52):  ");
                var weekNr = int.Parse(Console.ReadLine());
                Console.WriteLine("Ange båtnummer (1-2): ");
                var boatNr = int.Parse(Console.ReadLine());

                PrintWeeks(weekNr, boatNr);
            }
        }
        public static void PrintWeeks(int weekNr, int boatNr)
        {
            using (var db = new MyDBContext())
            {
                var bookings = db.Bookings;
                var check = bookings.Where(bookings => bookings.WeekNumber == weekNr && bookings.BoatNumber == boatNr);
                Console.WriteLine("Vecka: " + weekNr);
                foreach (var c in check)
                {
                    Console.Write(c.WeekDay);
                    if (c.Available == true)
                    {
                        Console.WriteLine(" - Ledig    ");
                    }
                    else
                    {
                        Console.WriteLine($" - Bokad    ");
                    }
                }
                Console.ReadKey();
                Console.Clear();
            }
        }
        enum Menu
        {
            Boka_fiskebåt = 1,
            Visa_Översikt,
            Queries,
            Admin = 5,
            Avsluta = 9
        }
        public static void StartPage()
        {
            bool run = true;
            while (run)
            {
                Console.WriteLine("Välkommen till bokningen av Thoms Fiskebåtar.\nVälj i menyn: ");
                foreach (int i in Enum.GetValues(typeof(Menu)))
                {
                    Console.WriteLine($"{i}. {Enum.GetName(typeof(Menu), i).Replace('_', ' ')}");
                }
                Menu menu = (Menu)99;
                int nr;
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                {
                    menu = (Menu)nr;
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Fel inmatning");
                }
                switch (menu)
                {
                    case Menu.Boka_fiskebåt:
                        CreatePerson();
                        break;
                    case Menu.Visa_Översikt:
                        PrintWeekValues();
                        break;
                    case Menu.Queries:
                        GetDapperData.BestBoat();
                        Console.WriteLine("------------------------------");
                        GetDapperData.MostBookedDay();
                        Console.WriteLine("------------------------------");
                        GetDapperData.MostPopularGroupAmount();
                        break;
                    case Menu.Admin:
                        BoatValues();
                        break;
                    case Menu.Avsluta:
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Fel inmatning, försök igen.");
                        break;
                }

            }
        }//Lades in första gången manuellt.
        public static void CreateBooking()
        {
            var weekDays = new List<string> { "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag" };
            for (int i = 0; i < 52; i++)
            {
                foreach (var w in weekDays)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        using (var db = new MyDBContext())
                        {
                            var newBooking = new Booking()
                            {
                                BoatNumber = j + 1,
                                Description = "Fiskeutrustning riktat mot alla arter. Inklusive ekolod, håv, 15 hk motor",
                                WeekDay = w,
                                WeekNumber = i + 1,
                                Available = true,
                                PersonId = null
                            };
                            var bookingList = db.Bookings;
                            bookingList.Add(newBooking);
                            db.SaveChanges();
                            Console.Clear();
                            Console.WriteLine("Du har lagt till en båt.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                }
            }
        }
        public static void CreatePerson()
        {
            using (var db = new MyDBContext())
            {
                Console.WriteLine("Namn: ");
                var name = Console.ReadLine();
                Console.WriteLine("Antal personer i båten: ");
                var antal = int.Parse(Console.ReadLine());

                PersonValues(name, antal);
            }

        }
        public static void PersonValues(string name, int antal)
        {
           using (var db = new MyDBContext())
           {
                var newPerson = new Person()
                {
                    Name = name,
                    NumberOfPeople = antal
                };
                var personList = db.Persons;
                personList.Add(newPerson);
                db.SaveChanges();
                BookBoat(newPerson.Id);
           }
        }
    }
}

