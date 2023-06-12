using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EFCode
{
    public class VocabularyAppContext : DbContext
    {


        public VocabularyAppContext(
            DbContextOptions<VocabularyAppContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>()
                .HasIndex(w => w.Text)
                .IsUnique();

            modelBuilder.Entity<Define>()
                .HasIndex(d => new { d.VocabularyId, d.Definition })
                .IsUnique();
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<Vocabulary> Vocabularies { get; set; }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Define> Defines { get; set; }



    }
}
