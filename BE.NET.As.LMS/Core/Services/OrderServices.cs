using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Output;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BE.NET.As.LMS.Utilities.Constaint;

namespace BE.NET.As.LMS.Core.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _uow;
        private IUserServices _userServices;
        public OrderServices(IUnitOfWork uow, IUserServices userServices)
        {
            _uow = uow;
            _userServices = userServices;
        }
        public async Task<int> AddOrder(string userHashCode)
        {
            User user = await _userServices.GetByHashCode(userHashCode);
            if (user == null)
                return -1;
            try
            {
                Order order = new Order
                {
                    Quantity = 0,
                    TotalPrice = 0,
                    UserId = user.Id,
                    CreatedAt = DateTime.Now,
                    Status = (int)OrderStatus.UnPaid,
                };
                _uow.GetRepository<Order>().Add(order);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }

        public async Task<int> PaidOrCancelOrder(string orderHashCode, OrderStatus orderStatus)
        {
            try
            {
                Order order = await GetByHashCode(orderHashCode);
                if (order == null)
                    return -1;
                order.Status = (int)orderStatus;
                order.UpdatedAt = DateTime.Now;
                _uow.GetRepository<Order>().Update(order);
                return await _uow.SaveChangesAsync();
            }
            catch
            {
                return -1;
            }
        }

        public async Task<List<OrderOutput>> GetAllByUserHashCode(string userHashCode)
        {
            User user = await _userServices.GetByHashCode(userHashCode);
            if (user == null)
                return null;
            return (await GetAll()).Where(_ => _.UserHashCode == userHashCode).ToList();
        }

        public async Task<Order> GetByHashCode(string hashCode)
        {
            return await _uow.GetRepository<Order>()
                .AsQueryable()
                .FirstOrDefaultAsync(_ => _.HashCode == hashCode &&
                                     _.isDeleted == false);
        }

        public async Task<List<OrderOutput>> GetAll()
        {
            return await _uow.GetRepository<Order>()
                .AsQueryable()
                .Include(_ => _.OrderDetails.Where(_ => _.isDeleted == false))
                .Where(_ => _.isDeleted == false &&
                       _.User.isDeleted == false)
                .Select(_ => new OrderOutput
                {
                    HashCode = _.HashCode,
                    Quantity = _.Quantity,
                    TotalPrice = _.TotalPrice,
                    Status = (OrderStatus)_.Status,
                    CreatedAt = _.CreatedAt,
                    UpdatedAt = _.UpdatedAt,
                    UserHashCode = _.User.HashCode,
                    UserName = _.User.UserName,
                    OrderDetails = _.OrderDetails
                    .Where(_ => _.isDeleted == false)
                    .Select(_ => new OrderDetailOutput
                    {
                        HashCode = _.HashCode,
                        Price = _.Price,
                        CreatedAt = _.CreatedAt,
                        UpdatedAt = _.UpdatedAt,
                        CourseName = _.CourseName,
                        CourseHashCode = _.Course.HashCode,
                        OrderHashCode = _.Order.HashCode
                    }).ToList()
                }).ToListAsync();
        }
    }
}
