﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SammBotNET.Database;

namespace SammBotNET.Migrations.EmotionalSupportDBMigrations
{
    [DbContext(typeof(EmotionalSupportDB))]
    [Migration("20211009225335_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("SammBotNET.Database.EmotionalSupport", b =>
                {
                    b.Property<string>("SupportMessage")
                        .HasColumnType("TEXT");

                    b.HasKey("SupportMessage");

                    b.ToTable("EmotionalSupport");
                });
#pragma warning restore 612, 618
        }
    }
}
