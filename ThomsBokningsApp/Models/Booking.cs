using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ThomsBokningsApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int BoatNumber { get; set; }
        public string Description { get; set; }
        public string WeekDay { get; set; }
        public int WeekNumber { get; set; }
        public bool Available { get; set; }
        public virtual int? PersonId { get; set; }
        public virtual Person Persons { get; set; }
    }
}
