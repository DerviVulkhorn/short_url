﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShortLink.Entity;

#nullable disable

namespace ShortLink.Migrations
{
    [DbContext(typeof(EntityDb))]
    [Migration("20230911081813_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ShortLink.Models.Code", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int?>("OriginalLinkid")
                        .HasColumnType("integer");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("countMove")
                        .HasColumnType("integer");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.HasIndex("OriginalLinkid");

                    b.ToTable("codes");
                });

            modelBuilder.Entity("ShortLink.Models.OriginalLink", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int?>("UsersId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("UsersId");

                    b.ToTable("originalLinks");
                });

            modelBuilder.Entity("ShortLink.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("ShortLink.Models.Code", b =>
                {
                    b.HasOne("ShortLink.Models.OriginalLink", null)
                        .WithMany("code")
                        .HasForeignKey("OriginalLinkid");
                });

            modelBuilder.Entity("ShortLink.Models.OriginalLink", b =>
                {
                    b.HasOne("ShortLink.Models.Users", null)
                        .WithMany("OriginalLink")
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("ShortLink.Models.OriginalLink", b =>
                {
                    b.Navigation("code");
                });

            modelBuilder.Entity("ShortLink.Models.Users", b =>
                {
                    b.Navigation("OriginalLink");
                });
#pragma warning restore 612, 618
        }
    }
}
