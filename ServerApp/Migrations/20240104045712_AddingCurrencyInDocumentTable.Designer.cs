﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServerApp;

#nullable disable

namespace ServerApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240104045712_AddingCurrencyInDocumentTable")]
    partial class AddingCurrencyInDocumentTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ServerApp.Models.Category", b =>
                {
                    b.Property<long>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("category_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.HasKey("CategoryId");

                    b.ToTable("category");
                });

            modelBuilder.Entity("ServerApp.Models.Client", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("client_id");

                    b.Property<int>("Balance")
                        .HasColumnType("int")
                        .HasColumnName("balance");

                    b.Property<string>("ClientVAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("client_vat");

                    b.Property<string>("CompanyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("company_type");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int")
                        .HasColumnName("currency_id");

                    b.Property<int>("RegistrationNumber")
                        .HasColumnType("int")
                        .HasColumnName("registration_number");

                    b.HasKey("ClientId");

                    b.HasIndex("ClientVAT")
                        .IsUnique();

                    b.HasIndex("CurrencyId");

                    b.ToTable("client");
                });

            modelBuilder.Entity("ServerApp.Models.Currency", b =>
                {
                    b.Property<int>("CurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("currency_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CurrencyId"));

                    b.Property<string>("CurrencyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("currency_name");

                    b.HasKey("CurrencyId");

                    b.ToTable("currency");
                });

            modelBuilder.Entity("ServerApp.Models.Document", b =>
                {
                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("tenant_id");

                    b.Property<string>("ClientId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("client_id");

                    b.Property<string>("DocumentId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("document_id");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("currency");

                    b.HasKey("TenantId", "ClientId", "DocumentId");

                    b.HasIndex("ClientId");

                    b.ToTable("document");
                });

            modelBuilder.Entity("ServerApp.Models.Product", b =>
                {
                    b.Property<string>("ProductCode")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("product_code");

                    b.HasKey("ProductCode");

                    b.ToTable("product");
                });

            modelBuilder.Entity("ServerApp.Models.Tenant", b =>
                {
                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("tenant_id");

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int")
                        .HasColumnName("account_number");

                    b.HasKey("TenantId");

                    b.ToTable("tenant");
                });

            modelBuilder.Entity("ServerApp.Models.Transaction", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("client_id");

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("tenant_id");

                    b.Property<string>("DocumentId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("document_id");

                    b.Property<long>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("transaction_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TransactionId"));

                    b.Property<int>("Amount")
                        .HasColumnType("int")
                        .HasColumnName("amount");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.HasKey("ClientId", "TenantId", "DocumentId", "TransactionId");

                    b.HasIndex("TenantId", "ClientId", "DocumentId");

                    b.ToTable("transaction");
                });

            modelBuilder.Entity("ServerApp.Models.Client", b =>
                {
                    b.HasOne("ServerApp.Models.Currency", "Currency")
                        .WithMany("Clients")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("ServerApp.Models.Document", b =>
                {
                    b.HasOne("ServerApp.Models.Client", null)
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ServerApp.Models.Tenant", null)
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ServerApp.Models.Transaction", b =>
                {
                    b.HasOne("ServerApp.Models.Document", null)
                        .WithMany()
                        .HasForeignKey("TenantId", "ClientId", "DocumentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ServerApp.Models.Currency", b =>
                {
                    b.Navigation("Clients");
                });
#pragma warning restore 612, 618
        }
    }
}
