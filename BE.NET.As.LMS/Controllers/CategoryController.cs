using BE.NET.As.LMS.Controllers.Base;
using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Response;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Controllers
{
    public class CategoryController : BaseAPIController
    {
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [HttpGet("get-all-row-categories")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllRow()
        {
            var result = await _categoryServices.GetAllRow();
            if (result == null)
                return Ok(new ApiResponse<string>
                {
                    Message = "No category found",
                    StatusCode = (int)HttpStatusCode.OK
                });
            return Ok(new ApiResponse<List<CategoryOutput>>
            {
                Data = result,
                Message = "Get categories success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("get-all-as-tree-format-categories")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsTreeFormat()
        {
            var result = await _categoryServices.GetAllAsTreeFormat(null);
            if (result == null)
                return Ok(new ApiResponse<string>
                {
                    Message = "No category found",
                    StatusCode = (int)HttpStatusCode.OK
                });
            return Ok(new ApiResponse<List<CategoryOutput>>
            {
                Data = result,
                Message = "Get categories success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [Authorize(Roles = "admin")]
        [HttpGet("get-all-paging-filter-search-category")]
        public async Task<IActionResult> GetAllPagingFilterSearch(string searchString, int pageSize,
            int pageIndex, string parentCategoryTitle)
        {
            if (pageSize <= 0 || pageIndex <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Get categories failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            var result = await _categoryServices.GetAllPagingFilterSearch(searchString, pageSize,
                pageIndex, parentCategoryTitle);
            if (result == null)
                return Ok(new ApiResponse<string>
                {
                    Message = "No categories found",
                    StatusCode = (int)HttpStatusCode.OK
                });
            return Ok(new ApiResponse<CategoriesAdminOutput>
            {
                Data = result,
                Message = "Get categories success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("get-all-categories-by-parent-category-hash-code")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllByParentHashCode(string parentHashCode)
        {
            if (String.IsNullOrEmpty(parentHashCode))
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Invalid parent category",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            var result = await _categoryServices.GetAllByParentHashCode(parentHashCode);
            if (result == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Get categories fail",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<List<CategoryOutput>>
            {
                Data = result,
                Message = "Get categories success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("get-category-by-hash-code")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByHashCode(string hashCode)
        {
            if (String.IsNullOrEmpty(hashCode))
                return BadRequest();
            var result = await _categoryServices.GetByHashCode(hashCode);
            if (result == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Category not found",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            result.Id = -1;
            return Ok(new ApiResponse<Category>
            {
                Data = result,
                Message = "Get category success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPost("add-category")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddCategory(CategoryInput categoryInput)
        {
            string currentUserHashCode = Helper.GetCurrentUserHashCode(this.User);
            var result = await _categoryServices.AddCategory(currentUserHashCode, categoryInput);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Add category failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Add category success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [Authorize(Roles = "admin")]
        [HttpPut("update-detail-category")]
        public async Task<IActionResult> UpdateDetailCategory(CategoryUpdateInput categoryUpdateInput)
        {
            string currentUserHashCode = Helper.GetCurrentUserHashCode(this.User);
            var result = await _categoryServices.UpdateDetailCategory(currentUserHashCode, categoryUpdateInput);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Update category failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Update category success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [Authorize(Roles = "admin")]
        [HttpPut("update-inline-category")]
        public async Task<IActionResult> UpdateInlineCategory(CategoryUpdateInput categoryUpdateInput)
        {
            string currentUserHashCode = Helper.GetCurrentUserHashCode(this.User);
            var result = await _categoryServices.UpdateInlineCategory(currentUserHashCode, categoryUpdateInput);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Update category failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Update category success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [Authorize(Roles = "admin")]
        [HttpPut("delete-category")]
        public async Task<IActionResult> DeleteCategory(string hashCode)
        {
            var result = await _categoryServices.DeleteCategory(hashCode);
            if (result < 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Delete category failed",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            else if (result == 0)
                return Ok(new ApiResponse<string>
                {
                    Message = "You cant delete this parent category",
                    StatusCode = (int)HttpStatusCode.OK
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Delete category success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("is-category-have-parent")]
        [AllowAnonymous]
        public async Task<IActionResult> IsCategoryHaveParent(string hashCode)
        {
            if (String.IsNullOrEmpty(hashCode))
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Invalid category",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            var result = await _categoryServices.IsCategoryHaveParent(hashCode);
            if (!result)
                return Ok(new ApiResponse<string>
                {
                    Message = "Category have no parent category",
                    StatusCode = (int)HttpStatusCode.OK
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Category have a parent category",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpGet("is-category-have-child")]
        [AllowAnonymous]
        public async Task<IActionResult> IsCategoryHaveChild(string hashCode)
        {
            if (String.IsNullOrEmpty(hashCode))
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Invalid category",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            var result = await _categoryServices.IsCategoryHaveChild(hashCode);
            if (result == false)
                return Ok(new ApiResponse<string>
                {
                    Message = "Category have no child category",
                    StatusCode = (int)HttpStatusCode.OK
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Category have a child category",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
    }
}
