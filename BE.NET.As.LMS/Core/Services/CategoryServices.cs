using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IUnitOfWork _uow;
        private IUserServices _userServices;
        public CategoryServices(IUnitOfWork uow, IUserServices userServices)
        {
            _uow = uow;
            _userServices = userServices;
        }

        public async Task<int> AddCategory(string currentUserHashCode, CategoryInput categoryInput)
        {
            try
            {
                User user = await _userServices.GetByHashCode(currentUserHashCode);
                Category parentCategory = new Category();
                if (categoryInput.ParentCategoryHashCode != null)
                    parentCategory = await GetByHashCode(categoryInput.ParentCategoryHashCode);
                if (parentCategory == null)
                    return -1;
                var category = new Category
                {
                    Title = categoryInput.Title,
                    Alias = Helper.ToAlias(categoryInput.Title),
                    ImageURL = categoryInput.ImageURL,
                    Description = categoryInput.Description,
                    CreatedBy = user.UserName,
                    ParentId = categoryInput.ParentCategoryHashCode != null ? parentCategory.Id : null,
                    Status = categoryInput.Status
                };
                _uow.GetRepository<Category>().Add(category);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }

        public async Task<int> DeleteCategory(string hashCode)
        {
            try
            {
                var category = await GetByHashCode(hashCode);
                if (category == null)
                    return -1;
                if (await IsCategoryHaveChild(hashCode))
                    return 0;
                category.isDeleted = true;
                _uow.GetRepository<Category>().Update(category);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }

        public async Task<List<CategoryOutput>> GetAllAsTreeFormat(long? parentId)
        {
            List<CategoryOutput> categoryOutputs = new List<CategoryOutput>();
            List<Category> categories = await _uow.GetRepository<Category>()
                .AsQueryable()
                .Where(_ => _.isDeleted == false &&
                       _.ParentId == parentId)
                .ToListAsync();
            foreach (var c in categories)
            {
                categoryOutputs.Add(new CategoryOutput
                {
                    HashCode = c.HashCode,
                    Title = c.Title,
                    Alias = c.Alias,
                    ImageURL = c.ImageURL,
                    Description = c.Description,
                    Status = c.Status,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    ParentHashCode = c.ParentCategory == null ? null : c.ParentCategory.HashCode,
                    ParentCategoryTitle = c.ParentCategory == null ? null : c.ParentCategory.Title,
                    SubCategories = (await GetAllAsTreeFormat(c.Id)).ToList()
                });
            }
            return categoryOutputs;
        }

        public async Task<List<CategoryOutput>> GetAllByParentHashCode(string parentHashCode)
        {
            Category parentCategory = await GetByHashCode(parentHashCode);
            if(parentCategory == null)
                return null;
            return await _uow.GetRepository<Category>()
                .AsQueryable()
                .Where(_ => _.isDeleted == false &&
                       _.ParentId == parentCategory.Id)
                .Take(30)
                .Select(c => new CategoryOutput
                {
                    HashCode = c.HashCode,
                    Title = c.Title,
                    Alias = c.Alias,
                    ImageURL = c.ImageURL,
                    Description = c.Description,
                    Status = c.Status,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    ParentHashCode = parentCategory.HashCode,
                    ParentCategoryTitle = parentCategory.Title,
                })
                .ToListAsync();
        }

        public async Task<CategoriesAdminOutput> GetAllPagingFilterSearch(string searchString,
            int pageSize, int pageIndex, string parentCategoryTitle)
        {
            IEnumerable<CategoryOutput> categories = await GetAllRow();
            if (!String.IsNullOrEmpty(parentCategoryTitle))
            {
                Category parentCategory = await _uow.GetRepository<Category>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(_ => _.isDeleted == false &&
                                         _.Title.Equals(parentCategoryTitle));
                categories = await GetAllByParentHashCode(parentCategory.HashCode);
            }
            if (!String.IsNullOrEmpty(searchString))
                categories = categories.Where(_ => _.Title.Contains(searchString));
            categories = Helper.Paging<CategoryOutput>(categories.ToList(), pageSize, pageIndex);
            List<string> parentCategoryTitles = await _uow.GetRepository<Category>()
                .AsQueryable()
                .Select(category => category.Title)
                .ToListAsync();
            return new CategoriesAdminOutput
            {
                Categories = categories.ToList(),
                ParentCategories = new SelectList(parentCategoryTitles.ToList())
            };
        }

        public async Task<List<CategoryOutput>> GetAllRow()
        {
            return await _uow.GetRepository<Category>()
                .AsQueryable()
                .Where(_ => _.isDeleted == false)
                .Take(30)
                .Select(c => new CategoryOutput
                {
                    HashCode = c.HashCode,
                    Title = c.Title,
                    Alias = c.Alias,
                    ImageURL = c.ImageURL,
                    Description = c.Description,
                    Status = c.Status,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    ParentHashCode = c.ParentCategory == null ? null : c.ParentCategory.HashCode,
                    ParentCategoryTitle = c.ParentCategory == null ? null : c.ParentCategory.Title,
                })
                .ToListAsync();
        }

        public async Task<Category> GetByHashCode(string hashCode)
        {
            return await _uow.GetRepository<Category>()
                .AsQueryable()
                .FirstOrDefaultAsync(_ => _.HashCode == hashCode &&
                                     _.isDeleted == false);
        }

        public async Task<bool> IsCategoryHaveChild(string hashCode)
        {
            Category category = await GetByHashCode(hashCode);
            return await _uow.GetRepository<Category>()
                .AsQueryable()
                .AnyAsync(_ => _.ParentId == category.Id &&
                          _.isDeleted == false);
        }

        public async Task<bool> IsCategoryHaveParent(string hashCode)
        {
            return (await GetByHashCode(hashCode)).ParentId == null ? false : true;
        }

        public async Task<int> UpdateDetailCategory(string currentUserHashCode, CategoryUpdateInput categoryUpdateInput)
        {
            try
            {
                User user = await _userServices.GetByHashCode(currentUserHashCode);
                Category parentCategory = new Category();
                if (categoryUpdateInput.ParentCategoryHashCode != null)
                    parentCategory = await GetByHashCode(categoryUpdateInput.ParentCategoryHashCode);
                if (parentCategory == null)
                    return -1;
                Category existCategory = await GetByHashCode(categoryUpdateInput.HashCode);
                if (existCategory == null)
                    return -1;
                existCategory.Title = categoryUpdateInput.Title;
                existCategory.Alias = Helper.ToAlias(categoryUpdateInput.Title);
                existCategory.ImageURL = categoryUpdateInput.ImageURL;
                existCategory.Description = categoryUpdateInput.Description;
                existCategory.UpdatedBy = user.UserName;
                existCategory.ParentId = categoryUpdateInput.ParentCategoryHashCode != null ?
                    parentCategory.Id : null;
                existCategory.Status = categoryUpdateInput.Status;
                existCategory.UpdatedAt = DateTime.Now;
                _uow.GetRepository<Category>().Update(existCategory);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }

        public async Task<int> UpdateInlineCategory(string currentUserHashCode, CategoryUpdateInput categoryUpdateInput)
        {
            try
            {
                User user = await _userServices.GetByHashCode(currentUserHashCode);
                Category parentCategory = new Category();
                if (categoryUpdateInput.ParentCategoryHashCode != null)
                    parentCategory = await GetByHashCode(categoryUpdateInput.ParentCategoryHashCode);
                if (parentCategory == null)
                    return -1;
                Category existCategory = await GetByHashCode(categoryUpdateInput.HashCode);
                if (existCategory == null)
                    return -1;
                existCategory.Title = categoryUpdateInput.Title;
                existCategory.Alias = Helper.ToAlias(categoryUpdateInput.Title);
                existCategory.UpdatedBy = user.UserName;
                existCategory.ParentId = categoryUpdateInput.ParentCategoryHashCode != null ?
                    parentCategory.Id : null;
                existCategory.Status = categoryUpdateInput.Status;
                existCategory.UpdatedAt = DateTime.Now;
                _uow.GetRepository<Category>().Update(existCategory);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }
    }
}
