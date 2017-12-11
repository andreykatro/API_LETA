using API_LETA.DAL;
using API_LETA.Interfaces;
using API_LETA.Models;
using System.Linq;

namespace API_LETA.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LinkRecordContext context;

        public CategoryRepository(LinkRecordContext context)
        {
            this.context = context;
        }

        public bool Delete(int id)
        {
            var category = GetById(id);

            if (category != null)
            {
                context.Categories.Remove(category);
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }

        public Category GetById(int id)
        {
            return (id > 0) ? context.Categories.Find(id) : null;
        }

        public Category GetByName(string name)
        {
            if (name != null && name.Trim().Length > 0)
            {
                return context.Categories.FirstOrDefault(c => c.CategoryName == name);
            }
            return null;
        }

        public bool Insert(string categoryName)
        {
            if (categoryName != null
                && (categoryName.Trim().Length > 0)
                && GetByName(categoryName) == null)
            {
                context.Categories.Add(new Category { CategoryName = categoryName });
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }
        public bool Update(Category category)
        {
            if (category != null
                && (category.CategoryName != null)
                && (GetById(category.Id) != null)
                && (GetByName(category.CategoryName) == null))
            {
                context.Categories.Update(category);
                return (context.SaveChanges() > 0) ? true : false;
            }
            return false;
        }
    }
}
