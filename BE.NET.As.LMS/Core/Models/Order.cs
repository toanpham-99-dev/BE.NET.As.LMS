using BE.NET.As.LMS.Core.Models.Base;
using System.Collections.Generic;

namespace BE.NET.As.LMS.Core.Models
{
    public class Order : BaseModel
    {
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
