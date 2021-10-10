﻿// <auto-generated />
using SammBotNET.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SammBotNET.Migrations.CommandDBMigrations
{
    [DbContext(typeof(CommandDB))]
    [Migration("20210216083530_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("BaldiBotNET.Database.CustomCommand", b =>
                {
                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<ulong>("authorID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("reply")
                        .HasColumnType("TEXT");

                    b.HasKey("name");

                    b.ToTable("CustomCommand");
                });
#pragma warning restore 612, 618
        }
    }
}
