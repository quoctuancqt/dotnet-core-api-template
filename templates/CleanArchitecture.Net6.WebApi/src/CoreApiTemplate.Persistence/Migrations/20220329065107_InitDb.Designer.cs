﻿// <auto-generated />
using System;
using CoreApiTemplate.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoreApiTemplate.Persistence.Migrations
{
    [DbContext(typeof(TodoContext))]
    [Migration("20220329065107_InitDb")]
    partial class InitDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CoreApiTemplate.Domain.Entities.ToDoItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ToDoItems");
                });

            modelBuilder.Entity("EntityAudit", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("AuditMessage")
                        .HasColumnType("text");

                    b.Property<string>("SaveChangesAuditId")
                        .HasColumnType("text");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SaveChangesAuditId");

                    b.ToTable("EntityAudits");
                });

            modelBuilder.Entity("SaveChangesAudit", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Succeeded")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("SaveChangesAudits");
                });

            modelBuilder.Entity("EntityAudit", b =>
                {
                    b.HasOne("SaveChangesAudit", "SaveChangesAudit")
                        .WithMany("EntityAudits")
                        .HasForeignKey("SaveChangesAuditId");

                    b.Navigation("SaveChangesAudit");
                });

            modelBuilder.Entity("SaveChangesAudit", b =>
                {
                    b.Navigation("EntityAudits");
                });
#pragma warning restore 612, 618
        }
    }
}
