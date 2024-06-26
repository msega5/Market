﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Reflection.Emit;

namespace Market.Models
{
    public class StoreContext : DbContext
    {
        public DbSet<Store> Store { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Group> Group { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=gb;Integrated Security=True;TrustServerCertificate=True;Trusted_Connection=True;")
                .UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity
                        .ToTable("Products");
                entity
                        .HasKey(x => x.ID).HasName("ProductID");
                entity
                        .HasIndex(x => x.Name).IsUnique();

                entity
                        .Property(e => e.Name)
                        .HasColumnName("ProductName")
                        .HasMaxLength(255)
                        .IsRequired();

                entity
                        .Property(e => e.Description)
                        .HasColumnName("Description")
                        .HasMaxLength(255)
                        .IsRequired();
                entity
                        .Property(e => e.Price)
                        .HasColumnName("Price")
                        .IsRequired();

                entity
                        .HasOne(x => x.Group)
                        .WithMany(c => c.Products)
                        .HasForeignKey(x => x.ID)
                        .HasConstraintName("GroupToProduct");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("ProductGroups");
                entity.HasKey(x => x.ID).HasName("GroupID");
                entity.HasIndex(x => x.Name).IsUnique();
                entity
                        .Property(e => e.Name)
                        .HasColumnName("ProductName")
                        .HasMaxLength(255);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Storage");
                entity.HasKey(x => x.ID).HasName("StoreID");
                entity
                        .Property(e => e.Name)
                        .HasColumnName("StorageName");
                entity
                        .Property(e => e.Count)
                        .HasColumnName("ProductCount");
              
                entity
                        .HasMany(x=>x.Products)
                        .WithMany(m => m.Stores)
                        .UsingEntity(j=>j.ToTable("StorageName"));
            });
        }
    }
}
