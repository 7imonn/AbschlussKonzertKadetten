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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TicketOrder>().HasKey(x => new { x.OrderId, x.TicketId });

            builder.Entity<TicketOrder>()
                .HasOne(m => m.Order)
                .WithMany(ma => ma.TicketOrders)
                .HasForeignKey(m => m.OrderId);

            builder.Entity<TicketOrder>()
                .HasOne(m => m.Ticket)
                .WithMany(ma => ma.TicketOrders)
                .HasForeignKey(a => a.TicketId);
        }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<TicketOrder> TicketOrders { get; set; }

    }
}
