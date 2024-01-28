﻿// <auto-generated />
using System;
using Billing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Billing.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240128145449_BillsTable")]
    partial class BillsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Billing.Models.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("Billing.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BillId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<long>("UniqueCode")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Product 1",
                            Price = 999,
                            UniqueCode = 1234567890123L
                        },
                        new
                        {
                            Id = 2,
                            Name = "Product 2",
                            Price = 999,
                            UniqueCode = 9876543210987L
                        },
                        new
                        {
                            Id = 3,
                            Name = "Product 3",
                            Price = 999,
                            UniqueCode = 1112223334445L
                        });
                });

            modelBuilder.Entity("Billing.Models.Product", b =>
                {
                    b.HasOne("Billing.Models.Bill", null)
                        .WithMany("products")
                        .HasForeignKey("BillId");
                });

            modelBuilder.Entity("Billing.Models.Bill", b =>
                {
                    b.Navigation("products");
                });
#pragma warning restore 612, 618
        }
    }
}
