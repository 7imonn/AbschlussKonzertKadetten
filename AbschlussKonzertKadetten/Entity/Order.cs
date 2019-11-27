using AbschlussKonzertKadetten.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbschlussKonzertKadetten.Models
{
    public class Orders
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Bemerkung { get; set; }
        public DateTime OrderDate { get; set; }
        public int ClientId { get; set; }
        public Clients Client { get; set; }
        public int KadettId { get; set; }
        public Kadett Kadett { get; set; }
        public virtual ICollection<TicketOrder> TicketOrders { get; set; }

    }
}
