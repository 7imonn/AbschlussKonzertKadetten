﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Entity;
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

            //builder.Entity<TicketOrder>()
            //    .HasOne(m => m.Order)
            //    .WithMany(ma => ma.TicketOrders)
            //    .HasForeignKey(m => m.OrderId);

            //builder.Entity<TicketOrder>()
            //    .HasOne(m => m.Ticket)
            //    .WithMany(ma => ma.TicketOrders)
            //    .HasForeignKey(a => a.TicketId);

            builder.Entity<Clients>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<Kadett>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<Orders>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<TicketOrder>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<User>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Redactor>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<FormularActive>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Kadett> Kadett { get; set; }
        public DbSet<Redactor> Redactor { get; set; }
        public DbSet<TicketOrder> TicketOrders { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<FormularActive> FormularActive { get; set; }

    }
}
