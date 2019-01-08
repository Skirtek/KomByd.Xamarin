﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KomByd.Repository.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190108224016_AddLinesList")]
    partial class AddLinesList
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("KomByd.Migrations.Models.Line", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DirectionsList");

                    b.Property<string>("LineNumber");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Lines");
                });

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
