﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniInventoryManagementSystem.DbCon;

#nullable disable

namespace MiniInventoryManagementSystem.Migrations
{
    [DbContext(typeof(DbConnectionContext))]
    [Migration("20230811171919_Create Sales Tables")]
    partial class CreateSalesTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MiniInventoryManagementSystem.Models.Catagory", b =>
                {
                    b.Property<int>("CatagoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CatagoryId"), 1L, 1);

                    b.Property<string>("CatagoryName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("CatagoryId");

                    b.ToTable("CatagoryTable");
                });

            modelBuilder.Entity("MiniInventoryManagementSystem.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<string>("CustomerAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("CustomerId");

                    b.ToTable("CustomerTable");
                });

            modelBuilder.Entity("MiniInventoryManagementSystem.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Product_CatagoryId")
                        .HasColumnType("int")
                        .HasColumnName("CatagoryId");

                    b.HasKey("ProductId");

                    b.HasIndex("Product_CatagoryId");

                    b.ToTable("ProductTable");
                });

            modelBuilder.Entity("MiniInventoryManagementSystem.Models.Sales", b =>
                {
                    b.Property<int>("SalesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalesId"), 1L, 1);

                    b.Property<string>("SalesDate")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Sales_CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerId");

                    b.Property<int>("Sales_SalesManId")
                        .HasColumnType("int")
                        .HasColumnName("SalesManID");

                    b.HasKey("SalesId");

                    b.HasIndex("Sales_CustomerId");

                    b.HasIndex("Sales_SalesManId");

                    b.ToTable("SalesTable");
                });

            modelBuilder.Entity("MiniInventoryManagementSystem.Models.SalesMan", b =>
                {
                    b.Property<int>("SalesManId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalesManId"), 1L, 1);

                    b.Property<string>("SalesManAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SalesManDOB")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SalesManDesignation")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SalesManEmail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("SalesManJoiningDate")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SalesManName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SalesManPhone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SalesManSalary")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("SalesManId");

                    b.ToTable("SalesManTable");
                });

            modelBuilder.Entity("MiniInventoryManagementSystem.Models.Supplyer", b =>
                {
                    b.Property<int>("SupplyerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplyerId"), 1L, 1);

                    b.Property<string>("SupplyerAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SupplyerName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SupplyerPhone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("SupplyerId");

                    b.ToTable("SupplyerTable");
                });

            modelBuilder.Entity("MiniInventoryManagementSystem.Models.Product", b =>
                {
                    b.HasOne("MiniInventoryManagementSystem.Models.Catagory", "Product_Catagory")
                        .WithMany()
                        .HasForeignKey("Product_CatagoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product_Catagory");
                });

            modelBuilder.Entity("MiniInventoryManagementSystem.Models.Sales", b =>
                {
                    b.HasOne("MiniInventoryManagementSystem.Models.Customer", "Sales_Customer")
                        .WithMany()
                        .HasForeignKey("Sales_CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniInventoryManagementSystem.Models.SalesMan", "Sales_SalesMan")
                        .WithMany()
                        .HasForeignKey("Sales_SalesManId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sales_Customer");

                    b.Navigation("Sales_SalesMan");
                });
#pragma warning restore 612, 618
        }
    }
}
