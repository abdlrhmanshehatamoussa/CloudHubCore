using CloudHub.Domain.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Text.Json;

namespace CloudHub.Tests.Unit
{
    public partial class InMemoryContext : DbContext
    {
        public InMemoryContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Feature> Features { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<LoginType> LoginTypes { get; set; } = null!;
        public virtual DbSet<Nonce> Nonces { get; set; } = null!;
        public virtual DbSet<PaymentGateway> PaymentGateways { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserToken> UserTokens { get; set; } = null!;
        public virtual DbSet<Tenant> Tenants { get; set; } = null!;
        public virtual DbSet<PublicCollection> PublicCollections { get; set; } = null!;
        public virtual DbSet<PublicDocument> PublicDocuments { get; set; } = null!;
        public virtual DbSet<PrivateCollection> PrivateCollections { get; set; } = null!;
        public virtual DbSet<PrivateDocument> PrivateDocuments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrivateDocument>()
                .Property(c => c.Body)
                .HasConversion(v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<JsonDocument>(v));

            modelBuilder.Entity<PrivateDocument>().Property(p => p.Body)
              .HasConversion(
                  v => JsonDocumentToString(v),
                  v => JsonDocument.Parse(v, new JsonDocumentOptions()));

            modelBuilder.Entity<PublicDocument>()
                .Property(c => c.Body)
                .HasConversion(v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<JsonDocument>(v));

            modelBuilder.Entity<PublicDocument>().Property(p => p.Body)
              .HasConversion(
                  v => JsonDocumentToString(v),
                  v => JsonDocument.Parse(v, new JsonDocumentOptions()));
        }


        private static string JsonDocumentToString(JsonDocument document)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
                document.WriteTo(writer);
                writer.Flush();
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
