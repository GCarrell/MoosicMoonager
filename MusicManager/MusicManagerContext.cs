using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MusicManager
{
    public partial class MusicManagerContext : DbContext
    {
        public MusicManagerContext()
        {
        }

        public MusicManagerContext(DbContextOptions<MusicManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Favourite> Favourites { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Tab> Tabs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MusicManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favourite>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.Tab)
                    .WithMany()
                    .HasForeignKey(d => d.TabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favourite__TabId__3A81B327");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favourite__UserI__3B75D760");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Rating1).HasColumnName("Rating");

                entity.HasOne(d => d.Tab)
                    .WithMany()
                    .HasForeignKey(d => d.TabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ratings__TabId__3D5E1FD2");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ratings__UserId__3E52440B");
            });

            modelBuilder.Entity<Tab>(entity =>
            {
                entity.Property(e => e.TabId).ValueGeneratedNever();

                entity.Property(e => e.BandName)
                    .IsRequired()
                    .HasMaxLength(48)
                    .IsUnicode(false);

                entity.Property(e => e.Instrument)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.TabName)
                    .IsRequired()
                    .HasMaxLength(48)
                    .IsUnicode(false);

                entity.Property(e => e.TabUrl)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.TabCreatorNavigation)
                    .WithMany(p => p.Tabs)
                    .HasForeignKey(d => d.TabCreator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tabs__TabCreator__38996AB5");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
