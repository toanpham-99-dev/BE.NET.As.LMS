using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Services
{
    public class OrderDetailServices : IOrderDetailServices
    {
        private readonly IUnitOfWork _uow;
        private IOrderServices _orderServices;
        private ICourseServices _courseServices;
        public OrderDetailServices(IUnitOfWork uow, IOrderServices orderServices,
            ICourseServices courseServices)
        {
            _uow = uow;
            _orderServices = orderServices;
            _courseServices = courseServices;
        }

        public async Task<int> AddOrderDetail(string orderHashCode, string courseHashCode)
        {
            try
            {
                _uow.BeginTransaction();
                Order order = await _orderServices.GetByHashCode(orderHashCode);
                Course course = await _courseServices.GetByHashCode(courseHashCode);
                if (order == null || course == null)
                    return -1;
                OrderDetail orderDetail = new OrderDetail
                {
                    Price = course.Price,
                    CourseName = course.Title,
                    CourseId = course.Id,
                    OrderId = order.Id,
                    CreatedAt = DateTime.Now
                };
                _uow.GetRepository<OrderDetail>().Add(orderDetail);
                await _uow.SaveChangesAsync();

                order.Quantity += 1;
                order.TotalPrice += course.Price;
                order.UpdatedAt = DateTime.Now;
                _uow.GetRepository<OrderDetail>().Update(orderDetail);
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

        public async Task<OrderDetail> GetByHashCode(string orderDetailHashCode)
        {
            return await _uow.GetRepository<OrderDetail>()
                .AsQueryable()
                .FirstOrDefaultAsync(_ => _.HashCode == orderDetailHashCode &&
                                     _.isDeleted == false);
        }

        public bool IsOrderDetailInOrder(Order order, OrderDetail orderDetail)
        {
            return order.OrderDetails
                .Any(_ => _.isDeleted == false &&
                     _.HashCode == orderDetail.HashCode);
        }

        public async Task<int> RemoveOrderDetails(List<string> orderDetailHashCodes)
        {
            _uow.BeginTransaction();
            Order order = await _uow.GetRepository<Order>()
                .AsQueryable()
                .FirstOrDefaultAsync(_ => _.isDeleted == false &&
                                     _.OrderDetails.Any(_ => _.HashCode == orderDetailHashCodes[0]));
            if (order == null)
                return -1;
            foreach (string hashCode in orderDetailHashCodes)
            {
                try
                {
                    OrderDetail orderDetail = await GetByHashCode(hashCode);
                    if (orderDetail == null || !IsOrderDetailInOrder(order, orderDetail))
                        return -1;
                    orderDetail.isDeleted = true;
                    orderDetail.UpdatedAt = DateTime.Now;
                    _uow.GetRepository<OrderDetail>().Update(orderDetail);
                    await _uow.SaveChangesAsync();

                    order.Quantity -= 1;
                    order.TotalPrice -= orderDetail.Price;
                    _uow.GetRepository<Order>().Update(order);
                    await _uow.SaveChangesAsync();
                }
                catch
                {
                    _uow.RollbackTransaction();
                    return -1;
                }
            }
            _uow.CommitTransaction();
            return 1;
        }
    }
}
