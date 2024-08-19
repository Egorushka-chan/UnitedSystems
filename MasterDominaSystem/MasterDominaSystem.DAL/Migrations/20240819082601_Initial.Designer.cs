﻿// <auto-generated />
using System;
using MasterDominaSystem.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MasterDominaSystem.DAL.Migrations
{
    [DbContext(typeof(MasterContext))]
    [Migration("20240819082601_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MasterDominaSystem.DAL.Reports.ReportCloth", b =>
                {
                    b.Property<int>("GeneralID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GeneralID"));

                    b.Property<string>("ClothDescription")
                        .HasColumnType("text");

                    b.Property<int>("ClothID")
                        .HasColumnType("integer");

                    b.Property<string>("ClothName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ClothRating")
                        .HasColumnType("integer");

                    b.Property<string>("ClothSize")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MaterialDescription")
                        .HasColumnType("text");

                    b.Property<int?>("MaterialID")
                        .HasColumnType("integer");

                    b.Property<string>("MaterialName")
                        .HasColumnType("text");

                    b.Property<string>("PhotoHashCode")
                        .HasColumnType("text");

                    b.Property<int?>("PhotoID")
                        .HasColumnType("integer");

                    b.Property<string>("PhotoName")
                        .HasColumnType("text");

                    b.HasKey("GeneralID");

                    b.HasIndex("ClothID", "MaterialID", "PhotoID");

                    b.ToTable("ReportCloths");
                });

            modelBuilder.Entity("MasterDominaSystem.DAL.Reports.ReportPerson", b =>
                {
                    b.Property<int>("GeneralID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GeneralID"));

                    b.Property<string>("ClothDescription")
                        .HasColumnType("text");

                    b.Property<int?>("ClothID")
                        .HasColumnType("integer");

                    b.Property<string>("ClothName")
                        .HasColumnType("text");

                    b.Property<int?>("ClothRating")
                        .HasColumnType("integer");

                    b.Property<string>("ClothSize")
                        .HasColumnType("text");

                    b.Property<int>("PersonID")
                        .HasColumnType("integer");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PersonType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhotoHashCode")
                        .HasColumnType("text");

                    b.Property<int?>("PhotoID")
                        .HasColumnType("integer");

                    b.Property<string>("PhotoName")
                        .HasColumnType("text");

                    b.Property<string>("PhysiqueDescription")
                        .HasColumnType("text");

                    b.Property<int?>("PhysiqueForce")
                        .HasColumnType("integer");

                    b.Property<int?>("PhysiqueGrowth")
                        .HasColumnType("integer");

                    b.Property<int?>("PhysiqueID")
                        .HasColumnType("integer");

                    b.Property<int?>("PhysiqueWeight")
                        .HasColumnType("integer");

                    b.Property<int?>("SeasonID")
                        .HasColumnType("integer");

                    b.Property<string>("SeasonName")
                        .HasColumnType("text");

                    b.Property<string>("SetDescription")
                        .HasColumnType("text");

                    b.Property<int?>("SetID")
                        .HasColumnType("integer");

                    b.Property<string>("SetName")
                        .HasColumnType("text");

                    b.HasKey("GeneralID");

                    b.HasIndex("PersonID", "PhysiqueID", "SetID", "SeasonID", "ClothID", "PhotoID");

                    b.ToTable("ReportPersons");
                });
#pragma warning restore 612, 618
        }
    }
}
