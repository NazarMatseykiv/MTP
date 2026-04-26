using System;
using System.Collections.Generic;
using AdventureWorksWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksWebAPI;

public partial class AdventureWorksDbContext : DbContext
{
    public AdventureWorksDbContext()
    {
    }

    public AdventureWorksDbContext(DbContextOptions<AdventureWorksDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

    public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AdventureWorksLT;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SalesOrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.SalesOrderId, e.SalesOrderDetailId }).HasName("PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID");

            entity.Property(e => e.LineTotal).HasComputedColumnSql("(isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))", false);
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())", "DF_SalesOrderDetail_ModifiedDate");
            entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())", "DF_SalesOrderDetail_rowguid");

            entity.HasOne(d => d.SalesOrder).WithMany(p => p.SalesOrderDetails).HasConstraintName("FK_SalesOrderDetail_SalesOrderHeader");
        });

        modelBuilder.Entity<SalesOrderHeader>(entity =>
        {
            entity.HasKey(e => e.SalesOrderId).HasName("PK_SalesOrderHeader_SalesOrderID");

            entity.Property(e => e.SalesOrderId).ValueGeneratedNever();
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())", "DF_SalesOrderHeader_ModifiedDate");
            entity.Property(e => e.OnlineOrderFlag).HasDefaultValue(true, "DF_SalesOrderHeader_OnlineOrderFlag");
            entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())", "DF_SalesOrderHeader_rowguid");
            entity.Property(e => e.SalesOrderNumber).HasComputedColumnSql("(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]))", false);
            entity.Property(e => e.Status).HasDefaultValue((byte)1, "DF_SalesOrderHeader_Status");
            entity.Property(e => e.TotalDue).HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))", false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
