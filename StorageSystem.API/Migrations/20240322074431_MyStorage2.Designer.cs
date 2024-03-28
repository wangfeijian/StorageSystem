﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StorageSystem.API.Context;

#nullable disable

namespace StorageSystem.API.Migrations
{
    [DbContext(typeof(StorageDbContext))]
    [Migration("20240322074431_MyStorage2")]
    partial class MyStorage2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4");

            modelBuilder.Entity("StorageSystem.Shared.Context.StorageDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("TEXT");

                    b.Property<bool>("InFinish")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("InStorageDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("MaterielDetail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MaterielSN")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StorageName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WbsNo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("StorageDetails");

                    b.HasDiscriminator<string>("Discriminator").HasValue("StorageDetail");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("StorageSystem.Shared.Context.StorageStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StorageName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("StorageStatuses");
                });

            modelBuilder.Entity("StorageSystem.Shared.Context.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StorageSystem.Shared.Context.StorageOutDetail", b =>
                {
                    b.HasBaseType("StorageSystem.Shared.Context.StorageDetail");

                    b.Property<bool>("OutFinish")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OutStorageDate")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("StorageOutDetail");
                });
#pragma warning restore 612, 618
        }
    }
}