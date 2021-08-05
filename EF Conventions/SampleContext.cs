using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Humanizer;
using System.Linq;

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

        public virtual DbSet<Album> EfcAlbums { get; set; }
        public virtual DbSet<Artist> EfcArtists { get; set; }

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
            DefineConventions(modelBuilder);

            /*
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
            */

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private void DefineConventions(ModelBuilder modelBuilder)
        {
            var entityNames = modelBuilder.Model.GetEntityTypes()
                .Select(e => e.ShortName());
            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                var entityName = entity.ShortName();
                modelBuilder.Entity(entity.Name, e =>
                {
                    e.ToTable("efc_" + entityName);
                    e.HasKey("Id");
                    e.Property("Id")
                        .HasColumnName("efc_" + entityName + "_id")
                        .HasColumnType("integer")
                        .ValueGeneratedNever();
                    foreach (var prop in entity.GetProperties())
                    {
                        if (prop.Name == "Id") continue;
                        if (prop.Name.EndsWith("Id") && 
                            entityNames.Contains(prop.Name[..^2])) {
                            // it's a key
                            e.Property(prop.Name)
                                .HasColumnName("efc_" + prop.Name.Underscore());
                            continue;
                        }
                        if (prop.ClrType.IsValueType || prop.ClrType == typeof(string))
                        {
                            e.Property(prop.Name)
                                .HasColumnName(prop.Name.Underscore());
                        }
                    }
                });
            }
        }
    }
}
