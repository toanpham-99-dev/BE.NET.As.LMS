using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Output;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BE.NET.As.LMS.Utilities.Constaint;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface IOrderServices
    {
        Task<List<OrderOutput>> GetAll();
        Task<List<OrderOutput>> GetAllByUserHashCode(string userHashCode);
        Task<Order> GetByHashCode(string hashCode);
        Task<int> AddOrder(string userHashCode);
        Task<int> PaidOrCancelOrder(string orderHashCode, OrderStatus orderStatus);
    }
}
