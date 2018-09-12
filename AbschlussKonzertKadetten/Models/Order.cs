using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AbschlussKonzertKadetten.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Bemerkung { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        [ForeignKey("Id")]
        public virtual Ticket Tickets { get; set; }
        [Required]
        [ForeignKey("Id")]
        public virtual Client Clients { get; set; }
    }
}
