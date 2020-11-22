﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using erthsobesapi;

namespace erthsobesapi.Migrations
{
    [DbContext(typeof(OrdersContext))]
    partial class OrdersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("erthsobesapi.Model.Attachment", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("erthsobesapi.Model.Order", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityByDefaultColumn();

                    b.Property<long>("attachment_id")
                        .HasColumnType("bigint");

                    b.Property<decimal>("cost")
                        .HasColumnType("numeric");

                    b.Property<string>("email")
                        .HasColumnType("text");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("text");

                    b.Property<Guid>("product_id")
                        .HasColumnType("uuid");

                    b.Property<string>("type")
                        .HasColumnType("text");

                    b.Property<string>("value")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Order_info");
                });
#pragma warning restore 612, 618
        }
    }
}
