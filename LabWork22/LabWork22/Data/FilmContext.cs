using System;
using System.Collections.Generic;
using LabWork22.Models;
using Microsoft.EntityFrameworkCore;

namespace LabWork22.Data;

public partial class FilmContext : DbContext
{
    public FilmContext()
    {
    }

    public FilmContext(DbContextOptions<FilmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Film> Films { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = mssql; Initial Catalog = ispp3511; User ID = ispp3511; Password = 3511; Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Film>(entity =>
        {
            entity.ToTable("Film", tb => tb.HasTrigger("trDeletedFilm"));

            entity.Property(e => e.AgeRating)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Date).HasDefaultValueSql("(datepart(year,getdate()))");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.FilmPoster).IsUnicode(false);
            entity.Property(e => e.FilmSession).HasDefaultValue((short)90);
            entity.Property(e => e.FilmTitle).HasMaxLength(100);
            entity.Property(e => e.Genre).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
