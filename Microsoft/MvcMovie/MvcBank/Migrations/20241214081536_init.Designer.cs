﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcBank.Data;

#nullable disable

namespace MvcBank.Migrations
{
    [DbContext(typeof(BankDbContext))]
    [Migration("20241214081536_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MvcBank.Models.Bank", b =>
                {
                    b.Property<Guid>("BankId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BankId");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("MvcBank.Models.Branch", b =>
                {
                    b.Property<Guid>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BankId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BranchId");

                    b.HasIndex("BankId");

                    b.ToTable("Branch");
                });

            modelBuilder.Entity("MvcBank.Models.Branch", b =>
                {
                    b.HasOne("MvcBank.Models.Bank", "Bank")
                        .WithMany("Branch")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("MvcBank.Models.Bank", b =>
                {
                    b.Navigation("Branch");
                });
#pragma warning restore 612, 618
        }
    }
}