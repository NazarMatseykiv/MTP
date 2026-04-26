using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DormWebApi.Models;

public partial class DormContext : DbContext
{
    public DormContext()
    {
    }

    public DormContext(DbContextOptions<DormContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Settlement> Settlements { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DormDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rooms__3214EC07094D98D2");

            entity.Property(e => e.RoomNumber).HasMaxLength(10);
        });

        modelBuilder.Entity<Settlement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Settleme__3214EC0701EFAC27");

            entity.Property(e => e.CheckInDate).HasColumnType("datetime");
            entity.Property(e => e.CheckOutDate).HasColumnType("datetime");

            entity.HasOne(d => d.Room).WithMany(p => p.Settlements)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Settlemen__RoomI__4F7CD00D");

            entity.HasOne(d => d.Student).WithMany(p => p.Settlements)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Settlemen__Stude__4E88ABD4");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC07ACD4F139");

            entity.Property(e => e.Faculty).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
