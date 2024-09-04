using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface IOrderDetailServices
    {
        Task<OrderDetail> GetByHashCode(string orderDetailHashCode);
        Task<int> AddOrderDetail(string orderHashCode, string courseHashCode);
        Task<int> RemoveOrderDetails(List<string> orderDetailHashCodes);
        bool IsOrderDetailInOrder(Order order, OrderDetail orderDetail);
    }
}
