﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WardrobeOnline.DAL;

#nullable disable

namespace WardrobeOnline.DAL.Migrations
{
    [DbContext(typeof(WardrobeContext))]
    partial class WardrobeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Cloth", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Rating")
                        .HasColumnType("integer");

                    b.Property<string>("Size")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Clothes");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.ClothHasMaterials", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("ClothID")
                        .HasColumnType("integer");

                    b.Property<int>("MaterialID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("ClothID");

                    b.HasIndex("MaterialID");

                    b.ToTable("ClothHasMaterials");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Material", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Person", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Photo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("ClothID")
                        .HasColumnType("integer");

                    b.Property<string>("HashCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDBStored")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Value")
                        .HasColumnType("bytea");

                    b.HasKey("ID");

                    b.HasIndex("ClothID");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Physique", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Force")
                        .HasColumnType("integer");

                    b.Property<int>("Growth")
                        .HasColumnType("integer");

                    b.Property<int>("PersonID")
                        .HasColumnType("integer");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("PersonID");

                    b.ToTable("Physiques");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Season", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Set", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PhysiqueID")
                        .HasColumnType("integer");

                    b.Property<int>("SeasonID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("PhysiqueID");

                    b.HasIndex("SeasonID");

                    b.ToTable("Sets");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.SetHasClothes", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("ClothID")
                        .HasColumnType("integer");

                    b.Property<int>("SetID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("ClothID");

                    b.HasIndex("SetID");

                    b.ToTable("SetHasClothes");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.ClothHasMaterials", b =>
                {
                    b.HasOne("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Cloth", "Cloth")
                        .WithMany("ClothHasMaterials")
                        .HasForeignKey("ClothID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Material", "Material")
                        .WithMany("ClothHasMaterials")
                        .HasForeignKey("MaterialID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cloth");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Photo", b =>
                {
                    b.HasOne("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Cloth", "Cloth")
                        .WithMany("Photos")
                        .HasForeignKey("ClothID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cloth");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Physique", b =>
                {
                    b.HasOne("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Person", "Person")
                        .WithMany("Physiques")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Set", b =>
                {
                    b.HasOne("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Physique", "Physique")
                        .WithMany("Sets")
                        .HasForeignKey("PhysiqueID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Season", "Season")
                        .WithMany("Sets")
                        .HasForeignKey("SeasonID")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Physique");

                    b.Navigation("Season");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.SetHasClothes", b =>
                {
                    b.HasOne("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Cloth", "Cloth")
                        .WithMany("SetClothes")
                        .HasForeignKey("ClothID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Set", "Set")
                        .WithMany("SetHasClothes")
                        .HasForeignKey("SetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cloth");

                    b.Navigation("Set");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Cloth", b =>
                {
                    b.Navigation("ClothHasMaterials");

                    b.Navigation("Photos");

                    b.Navigation("SetClothes");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Material", b =>
                {
                    b.Navigation("ClothHasMaterials");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Person", b =>
                {
                    b.Navigation("Physiques");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Physique", b =>
                {
                    b.Navigation("Sets");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Season", b =>
                {
                    b.Navigation("Sets");
                });

            modelBuilder.Entity("UnitedSystems.CommonLibrary.WardrobeOnline.Entities.DB.Set", b =>
                {
                    b.Navigation("SetHasClothes");
                });
#pragma warning restore 612, 618
        }
    }
}
