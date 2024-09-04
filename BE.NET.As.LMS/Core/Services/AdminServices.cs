using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Response;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileStorageServices _fileStorageServices;
        private readonly ICategoryServices _categoryServices;
        private readonly UserManager<User> _userManager;
        public AdminServices(IUnitOfWork uow, IFileStorageServices fileStorageServices,
            ICategoryServices categoryServices, UserManager<User> userManager)
        {
            _uow = uow;
            _fileStorageServices = fileStorageServices;
            _categoryServices = categoryServices;
            _userManager = userManager;
        }
        public async Task<User> GetUserByHashCode(string hashCode)
        {
            return await _uow.GetRepository<User>().AsQueryable()
                 .FirstOrDefaultAsync(_ => _.HashCode == hashCode);
        }
        public async Task<List<UserDetailOutput>> GetListUser()
        {
            var users = await _uow.GetRepository<User>().AsQueryable()
                .Select(_ => new UserDetailOutput
                {
                    FullName = _.FullName,
                    HashCode = _.HashCode,
                    Avatar = _.Avatar,
                    DateOfBirth = _.DateOfBirth,
                    Email = _.Email,
                    PhoneNumber = _.PhoneNumber,
                    isDeleted = _.isDeleted
                }).ToListAsync();
            foreach (var item in users)
            {
                var userRole = await _uow.GetRepository<UserRole>().AsQueryable()
               .Include(_ => _.Role)
               .Where(_ => _.UserHashCode == item.HashCode)
               .FirstOrDefaultAsync();
                item.Role = new RoleOutput
                {
                    HashCode = userRole.RoleHashCode,
                    Name = userRole.Role.Name,
                    Descripton = userRole.Role.Description
                };
            }
            return users;
        }
        public async Task<PageResponse<UserDetailOutput>> GetUserPaging(string roleHashCode, PagingInput pagingInput)
        {
            var query = _uow.GetRepository<User>().AsQueryable()
                .Include(_ => _.UserRoles).ThenInclude(_ => _.Role)
                .Where(_ => _.HashCode != null);
            if (!string.IsNullOrEmpty(pagingInput.Keyword))
            {
                query = query.Where(_ => _.FullName.Contains(pagingInput.Keyword)
                || _.Email.Contains(pagingInput.Keyword)
                || _.UserName.Contains(pagingInput.Keyword));
            }
            int totalRow = await query.CountAsync();
            List<UserDetailOutput> users = await query.Skip((pagingInput.PageIndex - 1) * (pagingInput.PageSize))
                .Take(pagingInput.PageSize)
                .Select(_ => new UserDetailOutput()
                {
                    FullName = _.FullName,
                    Avatar = _.Avatar,
                    DateOfBirth = _.DateOfBirth,
                    Email = _.Email,
                    PhoneNumber = _.PhoneNumber,
                    HashCode = _.HashCode,
                    isDeleted = _.isDeleted
                }).ToListAsync();
            foreach (var item in users)
            {
                var userRole = await _uow.GetRepository<UserRole>().AsQueryable()
               .Include(_ => _.Role)
               .Where(_ => _.UserHashCode == item.HashCode)
               .FirstOrDefaultAsync();
                item.Role = new RoleOutput
                {
                    HashCode = userRole.RoleHashCode,
                    Name = userRole.Role.Name,
                    Descripton = userRole.Role.Description
                };
            }
            if (!string.IsNullOrEmpty(roleHashCode))
            {
                users = users.Where(_ => _.Role.HashCode == roleHashCode).ToList();
            }
            var pageResponse = new PageResponse<UserDetailOutput>()
            {
                Items = users,
                PageIndex = (pagingInput.PageIndex - 1) * pagingInput.PageSize,
                PageSize = pagingInput.PageSize,
                TotalRecords = totalRow
            };
            return pageResponse;
        }

        public async Task<bool> UpdateDetailUser(UserUpdateInput userInput, string hashCode)
        {
            var user = await GetUserByHashCode(hashCode);
            if (user == null)
            {
                return false;
            }
            _uow.BeginTransaction();
            user.FullName = userInput.FullName;
            user.DateOfBirth = userInput.DateOfBirth;
            user.Email = userInput.Email;
            user.PhoneNumber = userInput.PhoneNumber;
            user.isDeleted = userInput.isDeleted;
            if (userInput.NewAvatar != null)
            {
                var newAvatarUrl = await _fileStorageServices.SaveFileAsync(userInput.NewAvatar);
                user.Avatar = newAvatarUrl;
            }
            _uow.GetRepository<User>().Update(user);
            await _uow.SaveChangesAsync();
            if (userInput.Role.RoleHashCode == null)
            {
                _uow.CommitTransaction();
                return true;
            }
            Role role = _uow.GetRepository<Role>().AsQueryable()
                .FirstOrDefault(_ => _.HashCode == userInput.Role.RoleHashCode);
            UserRole userRole = _uow.GetRepository<UserRole>().AsQueryable()
                .FirstOrDefault(_ => _.UserHashCode == user.HashCode);
            if (userRole == null || role == null)
            {
                _uow.RollbackTransaction();
                return false;
            }
            _uow.GetRepository<UserRole>().Delete(userRole);
            await _uow.SaveChangesAsync();
            _uow.GetRepository<UserRole>().Add(new UserRole
            {
                HashCode = Guid.NewGuid().ToString(),
                RoleHashCode = role.HashCode,
                RoleId = role.Id,
                UserHashCode = user.HashCode,
                UserId = user.Id
            });
            await _uow.SaveChangesAsync();
            _uow.CommitTransaction();
            return true;
        }

        public async Task<ApiResponse<UserDetailOutput>> GetUserDetail(string hashCode)
        {
            var user = await GetUserByHashCode(hashCode);
            if (user == null)
            {
                return new ApiResponse<UserDetailOutput>
                {
                    Data = null,
                    Message = "Not Found User",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
            var userRole = await _uow.GetRepository<UserRole>().AsQueryable()
                .Include(_ => _.Role)
                .Where(_ => _.UserHashCode == hashCode)
                .FirstOrDefaultAsync();
            if (userRole == null)
            {
                return new ApiResponse<UserDetailOutput>
                {
                    Data = null,
                    Message = "Not Found User",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
            return new ApiResponse<UserDetailOutput>
            {
                Data = new UserDetailOutput()
                {
                    FullName = user.FullName,
                    Avatar = user.Avatar,
                    DateOfBirth = user.DateOfBirth,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    HashCode = user.HashCode,
                    Role = new RoleOutput
                    {
                        HashCode = userRole.RoleHashCode,
                        Name = userRole.Role.Name,
                        Descripton = userRole.Role.Description
                    }
                },
                Message = "Successful",
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        public async Task<int> AddUser(RegisterInput registerInput, string roleHashCode)
        {
            var user = await _userManager.FindByEmailAsync(registerInput.Email);
            if (user != null)
            {
                return -1;
            }
            user = await _userManager.FindByNameAsync(registerInput.UserName);
            if (user != null)
            {
                return -1;
            }
            user = new User
            {
                Email = registerInput.Email.ToLower(),
                NormalizedEmail = registerInput.Email.ToLower(),
                FullName = registerInput.FullName,
                PhoneNumber = registerInput.PhoneNumber,
                UserName = registerInput.UserName.ToLower().Trim(),
                NormalizedUserName = registerInput.UserName.ToLower().Trim(),
                DateOfBirth = registerInput.DateOfBirth,
                PasswordHash = Helper.CreateMD5(registerInput.Password),
                EmailConfirmed = true
            };
            try
            {
                _uow.BeginTransaction();
                _uow.GetRepository<User>().Add(user);
                await _uow.SaveChangesAsync();
                Role role = _uow.GetRepository<Role>().AsQueryable()
               .FirstOrDefault(_ => _.HashCode == roleHashCode);
                UserRole userRole = _uow.GetRepository<UserRole>().AsQueryable()
                    .FirstOrDefault(_ => _.UserHashCode == user.HashCode);
                if (userRole != null || role == null)
                {
                    _uow.RollbackTransaction();
                    return -1;
                }
                _uow.GetRepository<UserRole>().Add(new UserRole
                {
                    HashCode = Guid.NewGuid().ToString(),
                    RoleHashCode = role.HashCode,
                    RoleId = role.Id,
                    UserHashCode = user.HashCode,
                    UserId = user.Id
                });
                _uow.GetRepository<UserRole>().Add(userRole);
                await _uow.SaveChangesAsync();
                _uow.CommitTransaction();
                return 1;

            }
            catch
            {
                _uow.RollbackTransaction();
                return -1;
            }
        }

        public async Task<bool> UpdateUserInLine(UserUpdateInput userInput, string hashCode)
        {
            var user = await GetUserByHashCode(hashCode);
            if (user == null)
            {
                return false;
            }
            user.FullName = userInput.FullName;
            user.DateOfBirth = userInput.DateOfBirth;
            user.Email = userInput.Email;
            user.PhoneNumber = userInput.PhoneNumber;
            user.isDeleted = userInput.isDeleted;
            _uow.GetRepository<User>().Update(user);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<int> DeleteUser(string hashCode)
        {
            var user = await GetUserByHashCode(hashCode);
            if (user == null)
                return -1;
            user.isDeleted = true;
            _uow.GetRepository<User>().Update(user);
            return await _uow.SaveChangesAsync();
        }

        public async Task<StatisticOutput> GetStatistics()
        {
            List<UserRole> numberOfAllUser = await _uow.GetRepository<UserRole>()
                .AsQueryable()
                .Where(_ => _.User.isDeleted == false)
                .ToListAsync();
            return new StatisticOutput
            {
                NumberOfAdmin = numberOfAllUser.Where(ur => ur.RoleId == 1).Count(),
                NumberOfInstructor = numberOfAllUser.Where(ur => ur.RoleId == 2).Count(),
                NumberOfUser = numberOfAllUser.Where(ur => ur.RoleId == 3).Count(),
                NumberOfCategory = (await _categoryServices.GetAllRow()).Count(),
                NumberOfCourse = _uow.GetRepository<Course>()
                    .AsQueryable()
                    .Where(_ => _.isDeleted == false)
                    .Count(),
                NumberOfLesson = _uow.GetRepository<Lesson>()
                    .AsQueryable()
                    .Where(_ => _.isDeleted == false)
                    .Count(),
                Top5RatingCourses = _uow.GetRepository<Course>()
                    .AsQueryable()
                    .Where(c => c.isDeleted == false)
                    .OrderByDescending(_ => _.Rating)
                    .Take(5)
                    .Select(_ => _.HashCode)
                    .ToList(),
                Top5Students = _uow.GetRepository<UserCourse>()
                    .AsQueryable()
                    .Where(_ => _.Completed == 1 &&
                           _.User.isDeleted == false)
                    .GroupBy(_ => _.UserHashCode)
                    .Select(g => new
                    {
                        UserHashCode = g.Key,
                        Count = g.Count(),
                    })
                    .OrderByDescending(_ => _.Count)
                    .Take(5)
                    .Select(_ => _.UserHashCode)
                    .ToList()
            };
        }
        public async Task<int> RecoverUser(string hashCode)
        {
            var user = await GetUserByHashCode(hashCode);
            if (user == null)
                return -1;
            user.isDeleted = false;
            _uow.GetRepository<User>().Update(user);
            return await _uow.SaveChangesAsync();
        }
        public async Task<List<SearchInstructorOutput>> SearchByInstructor(string searchString, int pageSize, int pageIndex)
        {
            IEnumerable<UserRole> users = await _uow.GetRepository<UserRole>()
            .AsQueryable()
            .Include(_ => _.User)
            .Where(_ => _.User.isDeleted == false && _.RoleHashCode == Utilities.Constaint.RoleHashCode.Instructor)
            .ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
                users = users.Where(_ => _.User.FullName.Contains(searchString))
                .OrderBy(_ => _.User.FullName);
            users = Helper.Paging<UserRole>(users.ToList(), pageSize, pageIndex);
            return users.Select(_ => new SearchInstructorOutput()
            {
                FullName = _.User.FullName,
                Avatar = _.User.Avatar,
                DateOfBirth = _.User.DateOfBirth,
            }).ToList();
        }

    }
}
