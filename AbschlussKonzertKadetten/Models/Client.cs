using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AbschlussKonzertKadetten.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string ForeName { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
