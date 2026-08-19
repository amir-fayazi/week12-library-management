using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Infrastructure.Repositories.EfCore
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public EfCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public Category Add(Category newCategory)
        {
            _context.Categories.Add(newCategory);
            _context.SaveChanges();

            return newCategory;
        }

        public void Delete(int id)
        {
            var category = GetById(id);
            if (category is null)
                throw new NotFoundException("category not found for deletion.");
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public bool ExistsByName(string name)
        {
            return _context.Categories.Any(p => p.Name == name);
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public List<Book> GetBooksByCategoryName(string categoryName)
        {
            return _context.Books.Where(p => p.Category.Name == categoryName).ToList();
        }

        public Category? GetById(int id)
        {
            return _context.Categories.Find(id);
        }

        public void Update(Category updatedCategory)
        {
            _context.Categories.Update(updatedCategory);
            _context.SaveChanges();
        }
    }
}
