using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EF_Conventions
{
    public partial class SampleContext : DbContext
    {
        public SampleContext()
        {
        }

        public SampleContext(DbContextOptions<SampleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EfcAlbum> EfcAlbums { get; set; }
        public virtual DbSet<EfcArtist> EfcArtists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Data Source=sample.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EfcAlbum>(entity =>
            {
                entity.ToTable("efc_album");

                entity.Property(e => e.EfcAlbumId)
                    .HasColumnType("integer")
                    .ValueGeneratedNever()
                    .HasColumnName("efc_album_id");

                entity.Property(e => e.EfcArtistId)
                    .HasColumnType("integer")
                    .HasColumnName("efc_artist_id");

                entity.Property(e => e.Title)
                    .HasColumnType("text")
                    .HasColumnName("title");

                entity.HasOne(d => d.EfcArtist)
                    .WithMany(p => p.EfcAlbums)
                    .HasForeignKey(d => d.EfcArtistId);
            });

            modelBuilder.Entity<EfcArtist>(entity =>
            {
                entity.ToTable("efc_artist");

                entity.Property(e => e.EfcArtistId)
                    .HasColumnType("integer")
                    .ValueGeneratedNever()
                    .HasColumnName("efc_artist_id");

                entity.Property(e => e.ArtistName)
                    .HasColumnType("text")
                    .HasColumnName("artist_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
