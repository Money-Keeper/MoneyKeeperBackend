﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoneyKeeper.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MoneyKeeper.Data.Migrations
{
    [DbContext(typeof(MoneyKeeperContext))]
    partial class MoneyKeeperContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MoneyKeeper.Domain.Data.Models.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<char>("Symbol")
                        .HasColumnType("character(1)");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("MoneyKeeper.Domain.Data.Models.Expense", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("MoneyKeeper.Domain.Data.Models.Expense", b =>
                {
                    b.HasOne("MoneyKeeper.Domain.Data.Models.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });
#pragma warning restore 612, 618
        }
    }
}