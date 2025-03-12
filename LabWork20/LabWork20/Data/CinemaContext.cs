using LabWork20.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;

namespace LabWork20.Data;

public partial class CinemaContext : DbContext
{
    public CinemaContext()
    {
    }

    public CinemaContext(DbContextOptions<CinemaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CinemaUser> CinemaUsers { get; set; }

    public virtual DbSet<CinemaUserRole> CinemaUserRoles { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = mssql; Initial Catalog = ispp3511; User ID = ispp3511; Password = 3511; Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CinemaUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsFixedLength();

            entity.HasOne(d => d.UserRole).WithMany(p => p.CinemaUsers)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CinemaUsers_CinemaUserRoles");
        });

        modelBuilder.Entity<CinemaUserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId);

            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

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

        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("Session");

            entity.HasIndex(e => e.SessionId, "UQ_SessionId").IsUnique();

            entity.Property(e => e.DateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Price)
                .HasDefaultValue(200m)
                .HasColumnType("decimal(4, 0)");

            entity.HasOne(d => d.Film).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKFilmID");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket");

            entity.HasIndex(e => e.Place, "UQ_Place").IsUnique();

            entity.HasIndex(e => e.Row, "UQ_Row").IsUnique();

            entity.HasOne(d => d.Session).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Session");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    public bool CheckAuthentication(string login, string password)
    {
        return CinemaUsers.AsNoTracking().SingleOrDefault(u => u.Login == login && u.Password == password) != null;
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
