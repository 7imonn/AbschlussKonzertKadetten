using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbschlussKonzertKadetten.Models
{
    public class ViewModelOrder
    {
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Bemerkung { get; set; }
        public int TicketE { get; set; }
        public int  TicketK { get; set; }
        public int TicketKK { get; set; }
    }
}
