﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TonyWebApplication;

namespace Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("Entidades.Caminhao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<long>("AnoFabricacao")
                        .HasColumnType("INTEGER");

                    b.Property<long>("AnoModelo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Modelo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Observacoes")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Caminhoes");
                });
#pragma warning restore 612, 618
        }
    }
}
