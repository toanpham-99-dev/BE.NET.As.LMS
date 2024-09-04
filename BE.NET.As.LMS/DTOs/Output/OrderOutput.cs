using BE.NET.As.LMS.DTOs.Output.Base;
using System;
using System.Collections.Generic;
using static BE.NET.As.LMS.Utilities.Constaint;

namespace BE.NET.As.LMS.DTOs.Output
{
    public class OrderOutput : BaseOutput 
    {
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UserHashCode { get; set; }
        public string UserName { get; set; }
        public List<OrderDetailOutput> OrderDetails { get; set; }
    }
}
