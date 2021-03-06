// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SammBotNET.Database;

namespace SammBotNET.Migrations.PeoneImagesDBMigrations
{
    [DbContext(typeof(PeoneImagesDB))]
    [Migration("20211029183247_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("SammBotNET.Database.PeoneImage", b =>
                {
                    b.Property<string>("TwitterUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("TwitterUrl");

                    b.ToTable("PeoneImage");
                });
#pragma warning restore 612, 618
        }
    }
}
