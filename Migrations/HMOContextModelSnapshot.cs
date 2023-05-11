﻿// <auto-generated />
using System;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(HMOContext))]
    partial class HMOContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entity.Corona_details", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("posutuve_result")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("recovery_date")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.ToTable("Corona_Details");
                });

            modelBuilder.Entity("Entity.Patient1", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Tz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("birth_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("id_Corona_Detail")
                        .HasColumnType("int");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mobile_phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("number")
                        .HasColumnType("int");

                    b.Property<string>("street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("id_Corona_Detail");

                    b.ToTable("Patient1");
                });

            modelBuilder.Entity("Entity.Vaccines", b =>
                {
                    b.Property<int>("id_vec")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_vec"));

                    b.Property<int>("id_p")
                        .HasColumnType("int");

                    b.Property<string>("manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("veccine_date")
                        .HasColumnType("datetime2");

                    b.HasKey("id_vec");

                    b.HasIndex("id_p");

                    b.ToTable("Vaccines");
                });

            modelBuilder.Entity("Entity.Patient1", b =>
                {
                    b.HasOne("Entity.Corona_details", "Corona_Detail")
                        .WithMany()
                        .HasForeignKey("id_Corona_Detail");

                    b.Navigation("Corona_Detail");
                });

            modelBuilder.Entity("Entity.Vaccines", b =>
                {
                    b.HasOne("Entity.Patient1", "Patient1")
                        .WithMany("Vaccines")
                        .HasForeignKey("id_p")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient1");
                });

            modelBuilder.Entity("Entity.Patient1", b =>
                {
                    b.Navigation("Vaccines");
                });
#pragma warning restore 612, 618
        }
    }
}
