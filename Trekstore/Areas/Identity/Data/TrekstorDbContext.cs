using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trekstore.Areas.Identity.Data;
using Trekstore.Models;

namespace Trekstore.Areas.Identity.Data;

public class TrekstorDbContext : IdentityDbContext<ApplicationUser>
{
    public TrekstorDbContext()
    {

    }
    public TrekstorDbContext(DbContextOptions<TrekstorDbContext> options)
        : base(options)
    {
    }


    public DbSet<Trekstore.Models.Categories> Categories { get; set; } = default!;

    public DbSet<Trekstore.Models.Client> Client { get; set; } = default!;

    public DbSet<Trekstore.Models.Products> Products { get; set; } = default!;

    public DbSet<Trekstore.Models.Providers> Providers { get; set; } = default!;

    public DbSet<Trekstore.Models.PurchaseDetails> PurchaseDetails { get; set; } = default!;

    public DbSet<Trekstore.Models.Purchases> Purchases { get; set; } = default!;

    public DbSet<Trekstore.Models.Sales> Sales { get; set; } = default!;

    public DbSet<Trekstore.Models.SalesDetails> SalesDetails { get; set; } = default!;

}
/*
public partial class TrekstorDbcontext : DbContext
{
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=INSOMNIA\\SQLEXPRESS; Database=Trekstore; Trusted_Connection=True; TrustServerCertificate=True ");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(e => e.CategoryID).HasName("PK__categori__C929A10A7EEFE47C");

            entity.ToTable("categorias");

            entity.Property(e => e.CategoryID).HasColumnName("Categoria_ID");
            entity.Property(e => e.CategoriaDescripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Categoria_Descripcion");
            entity.Property(e => e.CategoriaNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Categoria_Nombre");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clientes__EB7C89B4B9598C94");

            entity.Property(e => e.ClientId).HasColumnName("Clientes_ID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Email");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Last Name");
            entity.Property(e => e.PhoneNumber).HasColumnName("Clientes_Telefono");
        });

        modelBuilder.Entity<PurchaseDetails>(entity =>
        {
            entity.HasKey(e => e.purch_id).HasName("PK__Detalle___F2D38BA1A2AECE7D");

            entity.ToTable("Detalle_Entradas");

            entity.Property(e => e.purch_id).HasColumnName("Purchase_ID");
            entity.Property(e => e.Amount).HasColumnName("Amount");
            entity.Property(e => e.PurchDate)
                .HasColumnType("datetime")
                .HasColumnName("Purchase Date");
            entity.Property(e => e.ProductID).HasColumnName("Producto_ID");
            entity.Property(e => e.ProviderID).HasColumnName("Proveedor_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseDetails)
                .HasForeignKey(d => d.ProductID)
                .HasConstraintName("FK__Detalle_E__Produ__619B8048");

            entity.HasOne(d => d.Provider).WithMany(p => p.PurchaseDetails)
                .HasForeignKey(d => d.ProviderID)
                .HasConstraintName("FK__Detalle_E__Prove__628FA481");
        });

        modelBuilder.Entity<SalesDetails>(entity =>
        {
            entity.HasKey(e => e.SalesDetailsID).HasName("PK__Detalle___19966E071664E64A");

            entity.ToTable("Detalle_Salida", tb => tb.HasTrigger("trg_AfterInsertDetalleSalida"));

            entity.Property(e => e.SalesDetailsID).HasColumnName("Detalle_Salida_ID");
            entity.Property(e => e.ClientId).HasColumnName("Clientes_ID");
            entity.Property(e => e.Amount).HasColumnName("Detalle_Salida_Cantidad");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("Detalle_Salida_Fecha");
            entity.Property(e => e.ProductId).HasColumnName("Producto_ID");

            entity.HasOne(d => d.Clients).WithMany(p => p.SalesDetails)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Detalle_S__Clien__66603565");

            entity.HasOne(d => d.Product).WithMany(p => p.SalesDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Detalle_S__Produ__656C112C");
        });

        modelBuilder.Entity<Purchases>(entity =>
        {
            entity.HasKey(e => e.PurchaseID).HasName("PK__Entradas__6386012C55174F8E");

            entity.Property(e => e.PurchaseID).HasColumnName("Entradas_ID");
            entity.Property(e => e.Amount).HasColumnName("Entradas_Cantidad");
        });

        modelBuilder.Entity<Products>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__producto__9F1B153D3A6D9720");

            entity.ToTable("productos");

            entity.Property(e => e.ProductId).HasColumnName("Producto_ID");
            entity.Property(e => e.CategoryID).HasColumnName("Categoria_ID");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Producto_Descripcion");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Producto_Nombre");
            entity.Property(e => e.ProductPrice).HasColumnName("Producto_Precio");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryID)
                .HasConstraintName("FK__productos__Categ__398D8EEE");
        });

        modelBuilder.Entity<Providers>(entity =>
        {
            entity.HasKey(e => e.ProviderID).HasName("PK__Proveedo__FD838DBA931BD65F");

            entity.Property(e => e.ProviderID).HasColumnName("Proveedores_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Proveedores_Direccion");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Proveedores_Nombre");
            entity.Property(e => e.Telephone).HasColumnName("Proveedores_Telefono");
        });

        modelBuilder.Entity<Sales>(entity =>
        {
            entity.HasKey(e => e.SalesID).HasName("PK__Salidas__F501686081B47896");

            entity.Property(e => e.SalesID).HasColumnName("Salidas_ID");
            entity.Property(e => e.Amount).HasColumnName("Salidas_Cantidad");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
*/