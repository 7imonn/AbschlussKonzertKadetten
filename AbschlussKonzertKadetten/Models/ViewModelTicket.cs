using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AbschlussKonzertKadetten.Models
{
    public class ViewModelTicket
    {
        //[Required]
        public string Type { get; set; }
        //[Required]
        public int Quantity { get; set; }
        //[Required]
        public string Day { get; set; }

    }
}
