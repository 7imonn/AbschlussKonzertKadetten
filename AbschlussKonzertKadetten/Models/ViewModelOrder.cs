using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AbschlussKonzertKadetten.Models
{
    public class ViewModelOrder
    {
        [EmailAddress]
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Bemerkung { get; set; }
        public int TicketESa { get; set; }
        public int TicketKSa { get; set; }
        public int TicketKKSa { get; set; }
        public int TicketESo { get; set; }
        public int TicketKSo { get; set; }
        public int TicketKKSo { get; set; }
    }
}
