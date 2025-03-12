using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PractWork1.Models;

namespace PractWork1.Data;

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

    public virtual DbSet<Genre> Genres { get; set; }

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

            entity.HasOne(d => d.GenreNavigation).WithMany(p => p.Films)
                .HasForeignKey(d => d.Genre)
                .HasConstraintName("FK_Film_Genre");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Genre1).HasName("PK_Genre_1");

            entity.ToTable("Genre");

            entity.Property(e => e.Genre1)
                .HasMaxLength(50)
                .HasColumnName("Genre");
            entity.Property(e => e.GenreId).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
