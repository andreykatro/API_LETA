using API_LETA.Models;
using Microsoft.EntityFrameworkCore;

namespace API_LETA.DAL
{
    public class LinkRecordContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<OriginalUrl> OriginalUrls { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<LinkRecord> LinkRecords { get; set; }
        public virtual DbSet<TagsLinkRecord> TagsLinkRecords { get; set; }

        public LinkRecordContext(DbContextOptions<LinkRecordContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.CategoryName)
                    .IsUnique();

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnType("VARCHAR(30)");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasIndex(e => e.LanguageName)
                    .IsUnique();

                entity.Property(e => e.LanguageName)
                    .IsRequired()
                    .HasColumnType("VARCHAR(30)");
            });

            modelBuilder.Entity<LinkRecord>(entity =>
            {
                entity.HasIndex(e => e.Url)
                    .IsUnique();

                entity.Property(e => e.CreateTime).IsRequired();

                entity.Property(e => e.Title).HasColumnType("VARCHAR(140)");

                entity.Property(e => e.Url).IsRequired();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.LinkRecords)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.LinkRecords)
                    .HasForeignKey(d => d.LanguageId);

                entity.HasOne(d => d.OriginalUrl)
                    .WithMany(p => p.LinkRecords)
                    .HasForeignKey(d => d.OriginalUrlId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.LinkRecords)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<OriginalUrl>(entity =>
            {
                entity.HasIndex(e => e.OriginalUrlValue)
                    .IsUnique();

                entity.Property(e => e.OriginalUrlValue)
                    .IsRequired()
                    .HasColumnType("VARCHAR(64)");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasIndex(e => e.TagName)
                    .IsUnique();

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasColumnType("VARCHAR(30)");
            });

            modelBuilder.Entity<TagsLinkRecord>(entity =>
            {
                entity.HasIndex(e => new { e.TagId, e.LinkRecordId })
                    .IsUnique();

                entity.HasOne(d => d.LinkRecord)
                    .WithMany(p => p.TagsLinkRecords)
                    .HasForeignKey(d => d.LinkRecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TagsLinkRecords)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.HasIndex(e => e.TypeName)
                    .IsUnique();

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasColumnType("VARCHAR(30)");
            });
        }
    }
}
