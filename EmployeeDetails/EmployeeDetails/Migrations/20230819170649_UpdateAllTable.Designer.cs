﻿// <auto-generated />
using EmployeeDetails.DbCon;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeDetails.Migrations
{
    [DbContext(typeof(DbConnectionContext))]
    [Migration("20230819170649_UpdateAllTable")]
    partial class UpdateAllTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EmployeeDetails.Models.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityId"), 1L, 1);

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("City_StateId")
                        .HasColumnType("int");

                    b.HasKey("CityId");

                    b.HasIndex("City_StateId");

                    b.ToTable("CityTable");
                });

            modelBuilder.Entity("EmployeeDetails.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"), 1L, 1);

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("CountryId");

                    b.ToTable("CountryTable");
                });

            modelBuilder.Entity("EmployeeDetails.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("BSC")
                        .HasColumnType("bit");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("HSC")
                        .HasColumnType("bit");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MSC")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("SSC")
                        .HasColumnType("bit");

                    b.Property<int>("cityId")
                        .HasColumnType("int");

                    b.Property<int>("countryId")
                        .HasColumnType("int");

                    b.Property<int>("stateId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.HasIndex("cityId");

                    b.HasIndex("countryId");

                    b.HasIndex("stateId");

                    b.ToTable("EmployeeTable");
                });

            modelBuilder.Entity("EmployeeDetails.Models.State", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StateId"), 1L, 1);

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("State_CountryId")
                        .HasColumnType("int");

                    b.HasKey("StateId");

                    b.HasIndex("State_CountryId");

                    b.ToTable("StateTable");
                });

            modelBuilder.Entity("EmployeeDetails.Models.City", b =>
                {
                    b.HasOne("EmployeeDetails.Models.State", "City_state")
                        .WithMany()
                        .HasForeignKey("City_StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City_state");
                });

            modelBuilder.Entity("EmployeeDetails.Models.Employee", b =>
                {
                    b.HasOne("EmployeeDetails.Models.City", "Employee_City")
                        .WithMany()
                        .HasForeignKey("cityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeDetails.Models.Country", "Employee_country")
                        .WithMany()
                        .HasForeignKey("countryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeDetails.Models.State", "Employee_State")
                        .WithMany()
                        .HasForeignKey("stateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee_City");

                    b.Navigation("Employee_State");

                    b.Navigation("Employee_country");
                });

            modelBuilder.Entity("EmployeeDetails.Models.State", b =>
                {
                    b.HasOne("EmployeeDetails.Models.Country", "State_country")
                        .WithMany()
                        .HasForeignKey("State_CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State_country");
                });
#pragma warning restore 612, 618
        }
    }
}
