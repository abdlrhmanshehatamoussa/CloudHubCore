using CloudHub.Domain.Models;
using CloudHub.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Text.Json;

namespace CloudHub.Tests.Unit.Domain
{
    public partial class InMemoryContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(HelperFunctions.RandomString(20));
        }

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
