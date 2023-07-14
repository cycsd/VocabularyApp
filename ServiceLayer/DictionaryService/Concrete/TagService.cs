using DataLayer.EfClasses;
using DataLayer.EFCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DictionaryService.Concrete
{
    public class TagService
    {
        private readonly VocabularyAppContext _context;

        public TagService(VocabularyAppContext context)
        {
            _context = context;
        }

        public IQueryable<CategoryTag> GetCategoryTags()
        {
            return _context.CategoryTags;
        }
    }
}
