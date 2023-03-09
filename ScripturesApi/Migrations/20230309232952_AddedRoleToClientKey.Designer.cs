﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScripturesApi.Domain;

#nullable disable

namespace ScripturesApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230309232952_AddedRoleToClientKey")]
    partial class AddedRoleToClientKey
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ScripturesApi.Domain.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FullTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Heading")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("ScripturesApi.Domain.Entities.Chapter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("ScripturesApi.Domain.Entities.ClientKey", b =>
                {
                    b.Property<Guid>("ApiKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClientKeyRole")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("ApiKey");

                    b.ToTable("ClientKeys");
                });

            modelBuilder.Entity("ScripturesApi.Domain.Entities.IpLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ClientKeyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ip")
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("RequestUri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RequestedAtUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClientKeyId");

                    b.ToTable("IpLogs");
                });

            modelBuilder.Entity("ScripturesApi.Domain.Entities.Verse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChapterId")
                        .HasColumnType("int");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.ToTable("Verses");
                });

            modelBuilder.Entity("ScripturesApi.Domain.Entities.Chapter", b =>
                {
                    b.HasOne("ScripturesApi.Domain.Entities.Book", "Book")
                        .WithMany("Chapters")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("ScripturesApi.Domain.Entities.IpLog", b =>
                {
                    b.HasOne("ScripturesApi.Domain.Entities.ClientKey", "ClientKey")
                        .WithMany("IpLogs")
                        .HasForeignKey("ClientKeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientKey");
                });

            modelBuilder.Entity("ScripturesApi.Domain.Entities.Verse", b =>
                {
                    b.HasOne("ScripturesApi.Domain.Entities.Chapter", "Chapter")
                        .WithMany("Verses")
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chapter");
                });

            modelBuilder.Entity("ScripturesApi.Domain.Entities.Book", b =>
                {
                    b.Navigation("Chapters");
                });

            modelBuilder.Entity("ScripturesApi.Domain.Entities.Chapter", b =>
                {
                    b.Navigation("Verses");
                });

            modelBuilder.Entity("ScripturesApi.Domain.Entities.ClientKey", b =>
                {
                    b.Navigation("IpLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
