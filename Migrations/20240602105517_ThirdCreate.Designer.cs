﻿// <auto-generated />
using Market.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Market.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20240602105517_ThirdCreate")]
    partial class ThirdCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Market.Models.Group", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ProductName");

                    b.HasKey("ID")
                        .HasName("GroupID");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[ProductName] IS NOT NULL");

                    b.ToTable("ProductGroups", (string)null);
                });

            modelBuilder.Entity("Market.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Description");

                    b.Property<int>("GroupID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ProductName");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("Price");

                    b.HasKey("ID")
                        .HasName("ProductID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Market.Models.Store", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Count")
                        .HasColumnType("int")
                        .HasColumnName("ProductCount");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("StorageName");

                    b.HasKey("ID")
                        .HasName("StoreID");

                    b.ToTable("Storage", (string)null);
                });

            modelBuilder.Entity("ProductStore", b =>
                {
                    b.Property<int>("ProductsID")
                        .HasColumnType("int");

                    b.Property<int>("StoresID")
                        .HasColumnType("int");

                    b.HasKey("ProductsID", "StoresID");

                    b.HasIndex("StoresID");

                    b.ToTable("StorageName", (string)null);
                });

            modelBuilder.Entity("Market.Models.Product", b =>
                {
                    b.HasOne("Market.Models.Group", "Group")
                        .WithMany("Products")
                        .HasForeignKey("ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("GroupToProduct");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("ProductStore", b =>
                {
                    b.HasOne("Market.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Market.Models.Store", null)
                        .WithMany()
                        .HasForeignKey("StoresID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Market.Models.Group", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
