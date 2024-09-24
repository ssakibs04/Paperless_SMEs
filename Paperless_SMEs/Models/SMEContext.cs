using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Paperless_SMEs.Models
{
    public partial class SMEContext : DbContext
    {
        public SMEContext()
        {
        }

        public SMEContext(DbContextOptions<SMEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<DeliveryChallan> DeliveryChallans { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; } = null!;
        public virtual DbSet<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; } = null!;
        public virtual DbSet<Quotation> Quotations { get; set; } = null!;
        public virtual DbSet<QuotationLineItem> QuotationLineItems { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=SADMANSAKIB; database=SME; trusted_connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<DeliveryChallan>(entity =>
            {
                entity.Property(e => e.ChallanNumber).HasMaxLength(255);

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.DeliveryChallans)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .HasConstraintName("FK__DeliveryC__Purch__267ABA7A");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.InvoiceNumber).HasMaxLength(255);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.DeliveryChallan)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.DeliveryChallanId)
                    .HasConstraintName("FK__Invoices__Delive__29572725");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK__Payments__Invoic__2C3393D0");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasOne(d => d.Client)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__PurchaseO__Clien__1ED998B2");

                entity.HasOne(d => d.Quotation)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.QuotationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PurchaseO__Quota__1FCDBCEB");
            });

            modelBuilder.Entity<PurchaseOrderLineItem>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PurchaseOrderLineItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__PurchaseO__Produ__239E4DCF");

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.PurchaseOrderLineItems)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .HasConstraintName("FK__PurchaseO__Purch__22AA2996");
            });

            modelBuilder.Entity<Quotation>(entity =>
            {
                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Quotations)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Quotation__Clien__1273C1CD");
            });

            modelBuilder.Entity<QuotationLineItem>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.QuotationLineItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Quotation__Produ__1BFD2C07");

                entity.HasOne(d => d.Quotation)
                    .WithMany(p => p.QuotationLineItems)
                    .HasForeignKey(d => d.QuotationId)
                    .HasConstraintName("FK__Quotation__Quota__1B0907CE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
