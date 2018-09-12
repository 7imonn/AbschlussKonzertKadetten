using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Models;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;

namespace AbschlussKonzertKadetten.Context
{
    public class KadettenContext : DbContext
    {
        public KadettenContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TicketOrder>().HasKey(sc => new { sc.OrderId, sc.TicketId });
        }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<TicketOrder> TicketOrders { get; set; }

    }
}
