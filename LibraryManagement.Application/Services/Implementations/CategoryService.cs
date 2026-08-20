using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.DTOs;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LibraryManagement.Application.Contracts.Services
{
    public class CategoryService : ICategoryService
    {
        
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService( ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;

        }

        public void ChangeName(int categoryId, string name)
        {
            var category = _categoryRepo.GetById(categoryId);

            if (category is null)
                throw new NotFoundException("Category not found");
            category.ChangeName(name);
            _categoryRepo.Update(category);
        }

        public Category CreateCategory(string name)
        {
            var category = new Category(name);

            if (_categoryRepo.ExistsByName(name))
                throw new DuplicateException("title is already exists");

            _categoryRepo.Add(category);
            return category;
        }

        public void DeleteCategory(int categoryId)
        {
            var category = GetCategoryById(categoryId);

            if (category.Books.Any())
                throw new BusinessRuleException("Cannot delete category because it has books.");

            _categoryRepo.Delete(categoryId);
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepo.GetAll();
        }

        public List<CategoryWithBookCountDto> GetCategoriesWithBookCount()
        {
           return _categoryRepo.GetCategoriesWithBookCount();
        }

        public Category GetCategoryById(int id)
        {
            var category = _categoryRepo.GetById(id);
            return category is null ? throw new NotFoundException("Category not found") : category;
        }


    }
}
