﻿// <auto-generated />
using System;
using CompanyContext.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CompanyContext.Infrastructure.Migrations.Migrate
{
    [DbContext(typeof(CompanyDbContext))]
    [Migration("20230717135109_Demo")]
    partial class Demo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CompanyContext.Core.Aggregate.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("CompanyContext.Core.Aggregate.CompanyType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("TypeIcon")
                        .HasMaxLength(500)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("Type");
                });

            modelBuilder.Entity("CompanyContext.Core.Aggregate.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("CompanyContext.Core.Entity.Specialization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyTypeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("SpecializationIcon")
                        .HasMaxLength(500)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompanyTypeId");

                    b.ToTable("Specialization");
                });

            modelBuilder.Entity("CompanyContext.Core.Entity.SubCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategory");
                });

            modelBuilder.Entity("CompanyContext.Core.Interface.IArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SpecializationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("SpecializationId");

                    b.HasIndex("TypeId");

                    b.ToTable("Area");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IArea");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("CompanyContext.Core.ValueObject.CategoryArea", b =>
                {
                    b.HasBaseType("CompanyContext.Core.Interface.IArea");

                    b.HasDiscriminator().HasValue("CategoryArea");
                });

            modelBuilder.Entity("CompanyContext.Core.ValueObject.ServiceArea", b =>
                {
                    b.HasBaseType("CompanyContext.Core.Interface.IArea");

                    b.HasDiscriminator().HasValue("ServiceArea");
                });

            modelBuilder.Entity("CompanyContext.Core.Aggregate.Service", b =>
                {
                    b.OwnsMany("CompanyContext.Core.ValueObject.Requirement", "Requires", b1 =>
                        {
                            b1.Property<Guid>("ServiceId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("CategoryId")
                                .HasColumnType("uuid");

                            b1.Property<Guid?>("SubCategoryId")
                                .HasColumnType("uuid");

                            b1.HasKey("ServiceId", "Id");

                            b1.HasIndex("CategoryId");

                            b1.HasIndex("SubCategoryId");

                            b1.ToTable("Requirement");

                            b1.HasOne("CompanyContext.Core.Aggregate.Category", null)
                                .WithMany()
                                .HasForeignKey("CategoryId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("ServiceId");

                            b1.HasOne("CompanyContext.Core.Entity.SubCategory", null)
                                .WithMany()
                                .HasForeignKey("SubCategoryId");
                        });

                    b.Navigation("Requires");
                });

            modelBuilder.Entity("CompanyContext.Core.Entity.Specialization", b =>
                {
                    b.HasOne("CompanyContext.Core.Aggregate.CompanyType", null)
                        .WithMany("Specializations")
                        .HasForeignKey("CompanyTypeId");
                });

            modelBuilder.Entity("CompanyContext.Core.Entity.SubCategory", b =>
                {
                    b.HasOne("CompanyContext.Core.Aggregate.Category", null)
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("CompanyContext.Core.Interface.IArea", b =>
                {
                    b.HasOne("CompanyContext.Core.Aggregate.Category", null)
                        .WithMany("Areas")
                        .HasForeignKey("CategoryId");

                    b.HasOne("CompanyContext.Core.Aggregate.Service", null)
                        .WithMany("Areas")
                        .HasForeignKey("ServiceId");

                    b.HasOne("CompanyContext.Core.Entity.Specialization", null)
                        .WithMany()
                        .HasForeignKey("SpecializationId");

                    b.HasOne("CompanyContext.Core.Aggregate.CompanyType", null)
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CompanyContext.Core.Aggregate.Category", b =>
                {
                    b.Navigation("Areas");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("CompanyContext.Core.Aggregate.CompanyType", b =>
                {
                    b.Navigation("Specializations");
                });

            modelBuilder.Entity("CompanyContext.Core.Aggregate.Service", b =>
                {
                    b.Navigation("Areas");
                });
#pragma warning restore 612, 618
        }
    }
}
