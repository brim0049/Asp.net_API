﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api_travailPratique.Models;

#nullable disable

namespace api_travailPratique.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20220416201304_add")]
    partial class add
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("api_travailPratique.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Profil")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Solde")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("api_travailPratique.Models.Facture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("VendeurId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("VendeurId");

                    b.ToTable("Factures");
                });

            modelBuilder.Entity("api_travailPratique.Models.Produit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NomProduit")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Quantite")
                        .HasColumnType("int");

                    b.Property<int?>("VendeurId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VendeurId");

                    b.ToTable("Produits");
                });

            modelBuilder.Entity("api_travailPratique.Models.Vendeur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Profil")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Solde")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Vendeurs");
                });

            modelBuilder.Entity("ClientProduit", b =>
                {
                    b.Property<int>("ClientsId")
                        .HasColumnType("int");

                    b.Property<int>("ProduitsId")
                        .HasColumnType("int");

                    b.HasKey("ClientsId", "ProduitsId");

                    b.HasIndex("ProduitsId");

                    b.ToTable("ClientProduit");
                });

            modelBuilder.Entity("api_travailPratique.Models.Facture", b =>
                {
                    b.HasOne("api_travailPratique.Models.Client", "Client")
                        .WithMany("Factures")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api_travailPratique.Models.Vendeur", "Vendeur")
                        .WithMany("Factures")
                        .HasForeignKey("VendeurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Vendeur");
                });

            modelBuilder.Entity("api_travailPratique.Models.Produit", b =>
                {
                    b.HasOne("api_travailPratique.Models.Vendeur", "Vendeur")
                        .WithMany("Produits")
                        .HasForeignKey("VendeurId");

                    b.Navigation("Vendeur");
                });

            modelBuilder.Entity("ClientProduit", b =>
                {
                    b.HasOne("api_travailPratique.Models.Client", null)
                        .WithMany()
                        .HasForeignKey("ClientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api_travailPratique.Models.Produit", null)
                        .WithMany()
                        .HasForeignKey("ProduitsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api_travailPratique.Models.Client", b =>
                {
                    b.Navigation("Factures");
                });

            modelBuilder.Entity("api_travailPratique.Models.Vendeur", b =>
                {
                    b.Navigation("Factures");

                    b.Navigation("Produits");
                });
#pragma warning restore 612, 618
        }
    }
}
