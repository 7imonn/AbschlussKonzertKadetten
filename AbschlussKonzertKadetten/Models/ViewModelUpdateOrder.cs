using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbschlussKonzertKadetten.Models
{
    public class ViewModelUpdateOrder
    {
        public string Email { get; set; }
        public List<ViewModelTicket> Tickets { get; set; }
    }
}
