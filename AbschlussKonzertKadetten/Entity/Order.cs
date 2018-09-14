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
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Bemerkung { get; set; }
        public DateTime OrderDate { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public virtual ICollection<TicketOrder> TicketOrders { get; set; }

    }
}
