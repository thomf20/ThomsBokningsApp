using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThomsBokningsApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ThomsBokningsApp
{
    internal class MyDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = tcp:thomsbokningsapp.database.windows.net, 1433; Initial Catalog = BookingDB; Persist Security Info = False; User ID = thomadmin; Password = Systemutvecklare22; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
