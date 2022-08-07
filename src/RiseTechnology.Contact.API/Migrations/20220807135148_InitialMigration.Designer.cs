﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RiseTechnology.Contact.API.Context;

namespace RiseTechnology.Contact.API.Migrations
{
    [DbContext(typeof(ContactContext))]
    [Migration("20220807135148_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RiseTechnology.Contact.API.Context.DbEntities.ContactInformation", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ContactType")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PersonUUID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UUID");

                    b.HasIndex("PersonUUID");

                    b.ToTable("ContactInformations", "RiseContact");
                });

            modelBuilder.Entity("RiseTechnology.Contact.API.Context.DbEntities.Person", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UUID");

                    b.ToTable("Persons", "RiseContact");
                });

            modelBuilder.Entity("RiseTechnology.Contact.API.Context.DbEntities.ContactInformation", b =>
                {
                    b.HasOne("RiseTechnology.Contact.API.Context.DbEntities.Person", "Person")
                        .WithMany("ContactInformations")
                        .HasForeignKey("PersonUUID");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("RiseTechnology.Contact.API.Context.DbEntities.Person", b =>
                {
                    b.Navigation("ContactInformations");
                });
#pragma warning restore 612, 618
        }
    }
}