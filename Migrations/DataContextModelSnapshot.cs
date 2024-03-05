﻿// <auto-generated />
using System;
using AutoRainAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AutoRainAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AutoRainAPI.Models.Device", b =>
                {
                    b.Property<string>("SerialNumber")
                        .HasColumnType("text")
                        .HasColumnName("serial_number");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("password");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("salt");

                    b.Property<Guid?>("UserId")
                        .IsRequired()
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("SerialNumber");

                    b.HasIndex("UserId");

                    b.ToTable("devices");
                });

            modelBuilder.Entity("AutoRainAPI.Models.DeviceData", b =>
                {
                    b.Property<Guid>("devices_data_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("devices_data_id");

                    b.Property<int>("SoilMoisture")
                        .HasColumnType("integer")
                        .HasColumnName("soil_moisture");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime>("date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<string>("serial_number")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("serial_number");

                    b.HasKey("devices_data_id");

                    b.ToTable("devices_data");
                });

            modelBuilder.Entity("AutoRainAPI.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("password");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("salt");

                    b.HasKey("UserId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("AutoRainAPI.Models.Device", b =>
                {
                    b.HasOne("AutoRainAPI.Models.User", "User")
                        .WithMany("Devices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AutoRainAPI.Models.User", b =>
                {
                    b.Navigation("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}
