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
                entity.HasOne(d => d.Tab)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.TabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favourite__TabId__70DDC3D8");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favourite__UserI__6FE99F9F");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.Property(e => e.Rating1).HasColumnName("Rating");

                entity.HasOne(d => d.Tab)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.TabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ratings__TabId__72C60C4A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ratings__UserId__71D1E811");
            });

            modelBuilder.Entity<Tab>(entity =>
            {
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
                    .HasConstraintName("FK__Tabs__TabCreator__619B8048");
            });

            modelBuilder.Entity<User>(entity =>
            {
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
