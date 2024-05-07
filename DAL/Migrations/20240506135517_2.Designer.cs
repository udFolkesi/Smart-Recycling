﻿// <auto-generated />
using System;
using DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(SmartRecyclingDbContext))]
    [Migration("20240506135517_2")]
    partial class _2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CORE.Models.CollectionPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Fullness")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Volume")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CollectionPoint");
                });

            modelBuilder.Entity("CORE.Models.CollectionPointComposition", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("MaxVolume")
                        .HasColumnType("int");

                    b.Property<string>("TrashType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CollectionPointComposition");
                });

            modelBuilder.Entity("CORE.Models.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CollectionPointID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("TrashType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CollectionPointID");

                    b.HasIndex("UserID");

                    b.ToTable("Operation");
                });

            modelBuilder.Entity("CORE.Models.RecyclingPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecyclingType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Workload")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RecyclingPoint");
                });

            modelBuilder.Entity("CORE.Models.RecyclingPointStatistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Collected")
                        .HasColumnType("int");

                    b.Property<string>("Period")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Recycled")
                        .HasColumnType("int");

                    b.Property<int>("RecyclingPointID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecyclingPointID");

                    b.ToTable("RecyclingPointStatistics");
                });

            modelBuilder.Entity("CORE.Models.Transportation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CollectionPointID")
                        .HasColumnType("int");

                    b.Property<int>("RecyclingPointID")
                        .HasColumnType("int");

                    b.Property<string>("TrashType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CollectionPointID");

                    b.HasIndex("RecyclingPointID");

                    b.ToTable("Transportation");
                });

            modelBuilder.Entity("CORE.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CORE.Models.UserStatistics", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Bonuses")
                        .HasColumnType("int");

                    b.Property<int>("Recycled")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserStatistics");
                });

            modelBuilder.Entity("CORE.Models.CollectionPointComposition", b =>
                {
                    b.HasOne("CORE.Models.CollectionPoint", "CollectionPoint")
                        .WithOne("CollectionPointComposition")
                        .HasForeignKey("CORE.Models.CollectionPointComposition", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CollectionPoint");
                });

            modelBuilder.Entity("CORE.Models.Operation", b =>
                {
                    b.HasOne("CORE.Models.CollectionPoint", "CollectionPoint")
                        .WithMany("Operations")
                        .HasForeignKey("CollectionPointID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CORE.Models.User", "User")
                        .WithMany("Operations")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CollectionPoint");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CORE.Models.RecyclingPointStatistics", b =>
                {
                    b.HasOne("CORE.Models.RecyclingPoint", "RecyclingPoint")
                        .WithMany("RecyclingPointStatistics")
                        .HasForeignKey("RecyclingPointID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RecyclingPoint");
                });

            modelBuilder.Entity("CORE.Models.Transportation", b =>
                {
                    b.HasOne("CORE.Models.CollectionPoint", "CollectionPoint")
                        .WithMany("Transportations")
                        .HasForeignKey("CollectionPointID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CORE.Models.RecyclingPoint", "RecyclingPoint")
                        .WithMany("Transportation")
                        .HasForeignKey("RecyclingPointID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CollectionPoint");

                    b.Navigation("RecyclingPoint");
                });

            modelBuilder.Entity("CORE.Models.UserStatistics", b =>
                {
                    b.HasOne("CORE.Models.User", "User")
                        .WithOne("UserStatistics")
                        .HasForeignKey("CORE.Models.UserStatistics", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CORE.Models.CollectionPoint", b =>
                {
                    b.Navigation("CollectionPointComposition")
                        .IsRequired();

                    b.Navigation("Operations");

                    b.Navigation("Transportations");
                });

            modelBuilder.Entity("CORE.Models.RecyclingPoint", b =>
                {
                    b.Navigation("RecyclingPointStatistics");

                    b.Navigation("Transportation");
                });

            modelBuilder.Entity("CORE.Models.User", b =>
                {
                    b.Navigation("Operations");

                    b.Navigation("UserStatistics");
                });
#pragma warning restore 612, 618
        }
    }
}
