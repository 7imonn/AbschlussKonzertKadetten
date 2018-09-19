using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AbschlussKonzertKadetten.Models
{
    public class ViewModelOrder
    {
        //[Required]
        //[EmailAddress]
        public string Email { get; set; }
        //[StringLength(50)]
        public string ClientLastName { get; set; }
        //[StringLength(50)]
        public string ClientFirstName { get; set; }
        //[StringLength(100)]
        public string Bemerkung { get; set; }
        public string KadettLastName { get; set; }
        public string KadettFirstName { get; set; }
        public bool KadettInKader { get; set; }
        //[Required]
        public List<ViewModelTicket> Tickets { get; set; }
    }
}
