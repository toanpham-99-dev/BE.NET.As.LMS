using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface ICategoryServices
    {
        Task<List<CategoryOutput>> GetAllAsTreeFormat(long? parentId);
        Task<List<CategoryOutput>> GetAllRow();
        Task<CategoriesAdminOutput> GetAllPagingFilterSearch(string searchString, int pageSize,
            int pageIndex, string parentCategoryTitle);
        Task<List<CategoryOutput>> GetAllByParentHashCode(string parentHashCode);
        Task<Category> GetByHashCode(string hashCode);
        Task<int> AddCategory(string currentUserHashCode, CategoryInput categoryInput);
        Task<int> UpdateDetailCategory(string currentUserHashCode, CategoryUpdateInput categoryUpdateInput);
        Task<int> UpdateInlineCategory(string currentUserHashCode, CategoryUpdateInput categoryUpdateInput);
        Task<int> DeleteCategory(string hashCode);
        Task<bool> IsCategoryHaveParent(string hashCode);
        Task<bool> IsCategoryHaveChild(string hashCode);
    }
}
