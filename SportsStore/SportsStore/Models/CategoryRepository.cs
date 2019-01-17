using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private DataContext _context;

        public CategoryRepository(DataContext ctx) => _context = ctx;


        public IEnumerable<Category> Categories => _context.Categories;

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
    }
}
