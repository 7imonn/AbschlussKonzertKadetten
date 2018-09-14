using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbschlussKonzertKadetten.Models
{
    public class TicketOrder
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public DateTime Day { get; set; }
        public int Quantity { get; set; }
    }
}
