using BE.NET.As.LMS.Controllers.Base;
using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Controllers
{
    public class OrderDetailController : BaseAPIController
    {
        private readonly IOrderDetailServices _orderDetailServices;
        public OrderDetailController(IOrderDetailServices orderDetailServices)
        {
            _orderDetailServices = orderDetailServices;
        }
        [Authorize]
        [HttpGet("get-order-detail-by-hash-code")]
        public async Task<IActionResult> GetByHashCode(string orderDetailHashCode)
        {
            if (orderDetailHashCode == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "The input is invalid",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            var result = await _orderDetailServices.GetByHashCode(orderDetailHashCode);
            if (result == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Get order detail fail",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            result.Id = -1;
            return Ok(new ApiResponse<OrderDetail>
            {
                Data = result,
                Message = "Get order detail success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPost("add-order-detail")]
        [Authorize]
        public async Task<IActionResult> AddOrderDetail(string orderHashCode, string courseHashCode)
        {
            if (orderHashCode == null || courseHashCode == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "The input is invalid",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            var result = await _orderDetailServices.AddOrderDetail(orderHashCode, courseHashCode);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Add order detail fail",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Add order detail success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [Authorize]
        [HttpPut("remove-multiple-order-detail")]
        public async Task<IActionResult> RemoveOrderDetails(List<string> orderDetailHashCodes)
        {
            if (orderDetailHashCodes.Count == 0 || orderDetailHashCodes == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "The input is invalid",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            var result = await _orderDetailServices.RemoveOrderDetails(orderDetailHashCodes);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Remove order details fail",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Remove order details success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
    }
}
