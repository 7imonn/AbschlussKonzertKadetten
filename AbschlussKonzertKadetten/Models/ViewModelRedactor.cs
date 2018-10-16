using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AbschlussKonzertKadetten.Models
{
    public class ViewModelRedactor
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
