﻿// <auto-generated />
using KomByd.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KomByd.Migrations.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20181231162233_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("KomByd.Migrations.Models.StopRepo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("StopName");

                    b.Property<string>("StopNumbers");

                    b.Property<string>("VehiclesList");

                    b.HasKey("Id");

                    b.ToTable("Stops");
                });
#pragma warning restore 612, 618
        }
    }
}
