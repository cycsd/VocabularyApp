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

        public DbSet<Word> Words { get; set; }
        public DbSet<Vocabulary> Vocabularies { get; set; }
    }
}
