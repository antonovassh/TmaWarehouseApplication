﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TmaWarehouse.Data;

#nullable disable

namespace TmaWarehouse.Migrations;

[DbContext(typeof(TmaWarehouseDbContext))]
[Migration("20240317163653_23")]
partial class _23
{
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "6.0.28")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

        modelBuilder.Entity("Application.Models.Item", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<string>("ContactPerson")
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("GroupId")
                    .HasColumnType("int");

                b.Property<int>("MeasurementId")
                    .HasColumnType("int");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Photo")
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("Price")
                    .HasColumnType("int");

                b.Property<int>("Quantity")
                    .HasColumnType("int");

                b.Property<string>("Status")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("StorageLocation")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("GroupId");

                b.HasIndex("MeasurementId");

                b.ToTable("Items");
            });

        modelBuilder.Entity("Application.Models.ItemModel.ItemGroup", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("ItemGroups");
            });

        modelBuilder.Entity("Application.Models.ItemModel.ItemMeasurement", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("ItemMeasurements");
            });

        modelBuilder.Entity("Application.Models.Request.Request", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<string>("Comment")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("EmployeeName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("ItemId")
                    .HasColumnType("int");

                b.Property<int>("MeasurementId")
                    .HasColumnType("int");

                b.Property<int>("Quantity")
                    .HasColumnType("int");

                b.Property<string>("Status")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("MeasurementId");

                b.ToTable("Requests");
            });

        modelBuilder.Entity("Application.Models.Item", b =>
            {
                b.HasOne("Application.Models.ItemModel.ItemGroup", "Group")
                    .WithMany()
                    .HasForeignKey("GroupId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Application.Models.ItemModel.ItemMeasurement", "Measurement")
                    .WithMany()
                    .HasForeignKey("MeasurementId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Group");

                b.Navigation("Measurement");
            });

        modelBuilder.Entity("Application.Models.Request.Request", b =>
            {
                b.HasOne("Application.Models.ItemModel.ItemMeasurement", "Measurement")
                    .WithMany()
                    .HasForeignKey("MeasurementId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Measurement");
            });
#pragma warning restore 612, 618
    }
}
