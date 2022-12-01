﻿// <auto-generated />
using System;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EntityLayer.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdentityNo")
                        .HasColumnType("int");

                    b.Property<int>("IdentityNoVerified")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("EntityLayer.PaymentLog", b =>
                {
                    b.Property<string>("orderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("authCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("extraData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hostReference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("installmentCount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentSystem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("responseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("responseHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("responseMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("rnd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("saleDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("totalAmount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("vPosName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("orderId");

                    b.ToTable("PaymentLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
