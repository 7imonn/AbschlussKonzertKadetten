﻿// <auto-generated />
using System;
using AbschlussKonzertKadetten.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AbschlussKonzertKadetten.Migrations
{
    [DbContext(typeof(KadettenContext))]
    [Migration("20191125173627_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AbschlussKonzertKadetten.Entity.FormularActive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.HasKey("Id");

                    b.ToTable("FormularActive");
                });

            modelBuilder.Entity("AbschlussKonzertKadetten.Entity.Kadett", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<bool>("KadettInKader");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Kadett");
                });

            modelBuilder.Entity("AbschlussKonzertKadetten.Entity.Redactor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Redactor");
                });

            modelBuilder.Entity("AbschlussKonzertKadetten.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("AbschlussKonzertKadetten.Models.Clients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("AbschlussKonzertKadetten.Models.Orders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bemerkung");

                    b.Property<int>("ClientId");

                    b.Property<int>("KadettId");

                    b.Property<DateTime>("OrderDate");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("KadettId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AbschlussKonzertKadetten.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Price");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("AbschlussKonzertKadetten.Models.TicketOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Day");

                    b.Property<int>("OrderId");

                    b.Property<int>("Quantity");

                    b.Property<int>("TicketId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketOrders");
                });

            modelBuilder.Entity("AbschlussKonzertKadetten.Models.Orders", b =>
                {
                    b.HasOne("AbschlussKonzertKadetten.Models.Clients", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AbschlussKonzertKadetten.Entity.Kadett", "Kadett")
                        .WithMany()
                        .HasForeignKey("KadettId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AbschlussKonzertKadetten.Models.TicketOrder", b =>
                {
                    b.HasOne("AbschlussKonzertKadetten.Models.Orders", "Order")
                        .WithMany("TicketOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AbschlussKonzertKadetten.Models.Ticket", "Ticket")
                        .WithMany("TicketOrders")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
