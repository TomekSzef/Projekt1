﻿// <auto-generated />
using AutoWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AutoWebApp.Migrations
{
    [DbContext(typeof(AutoWebAppContext))]
    [Migration("20240215113204_readdorders")]
    partial class readdorders
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AutoWebApp.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"));

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("AutoWebApp.Models.SparePart", b =>
                {
                    b.Property<int>("PartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PartID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("VehicleModel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PartID");

                    b.ToTable("SparePart", (string)null);
                });

            modelBuilder.Entity("AutoWebApp.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NIP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            Email = "admin@admin",
                            FirstName = "Admin",
                            IsAdmin = true,
                            LastName = "Admin",
                            NIP = "",
                            Password = "admin"
                        });
                });

            modelBuilder.Entity("OrderSparePart", b =>
                {
                    b.Property<int>("OrdersOrderID")
                        .HasColumnType("int");

                    b.Property<int>("SparePartsPartID")
                        .HasColumnType("int");

                    b.HasKey("OrdersOrderID", "SparePartsPartID");

                    b.HasIndex("SparePartsPartID");

                    b.ToTable("OrderSparePart");
                });

            modelBuilder.Entity("OrderSparePart", b =>
                {
                    b.HasOne("AutoWebApp.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoWebApp.Models.SparePart", null)
                        .WithMany()
                        .HasForeignKey("SparePartsPartID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}