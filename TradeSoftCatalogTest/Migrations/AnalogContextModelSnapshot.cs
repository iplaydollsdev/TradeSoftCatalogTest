﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TradeSoftCatalogTest.Migrations
{
    [DbContext(typeof(AnalogContext))]
    partial class AnalogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TradeSoftCatalogTest.MVVM.Model.AnalogModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Article1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Article2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacturer1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacturer2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrustLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("analogs", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}