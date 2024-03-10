using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SMSVendor.Models;

public partial class SmsVendorDbContext : DbContext
{
    public SmsVendorDbContext()
    {
    }

    public SmsVendorDbContext(DbContextOptions<SmsVendorDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Message> Messages { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("MESSAGES");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityColumn<int>();
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("CREATED");
            entity.Property(e => e.Recipient)
                .HasMaxLength(15)
                .HasColumnName("RECIPIENT");
            entity.Property(e => e.Text)
                .HasMaxLength(480)
                .HasColumnName("TEXT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
