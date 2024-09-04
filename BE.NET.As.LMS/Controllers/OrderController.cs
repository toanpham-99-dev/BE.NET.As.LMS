using BE.NET.As.LMS.Controllers.Base;
using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Response;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static BE.NET.As.LMS.Utilities.Constaint;

namespace BE.NET.As.LMS.Controllers
{
    public class OrderController : BaseAPIController
    {
        private readonly IOrderServices _orderServices;
        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        [Authorize(Roles = "admin")]
        [HttpGet("get-all-order")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderServices.GetAll();
            if (result == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "No user found",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<List<OrderOutput>>
            {
                Data = result,
                Message = "Get orders success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }

        [Authorize]
        [HttpGet("get-all-order-by-current-user-hash-code")]
        public async Task<IActionResult> GetAllByUserHashCode()
        {
            string userHashCode = Helper.GetCurrentUserHashCode(this.User);
            var result = await _orderServices.GetAllByUserHashCode(userHashCode);
            if (result == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "No user found",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<List<OrderOutput>>
            {
                Data = result,
                Message = "Get orders success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [HttpPost("add-order")]
        [Authorize]
        public async Task<IActionResult> AddOrder()
        {
            string userHashCode = Helper.GetCurrentUserHashCode(this.User);
            var result = await _orderServices.AddOrder(userHashCode);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Add order fail",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Add order success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        [Authorize]
        [HttpPut("paid-or-cancel-order")]
        public async Task<IActionResult> PaidOrCancelOrder(string orderHashCode, OrderStatus orderStatus)
        {
            if (orderStatus == OrderStatus.UnPaid || orderHashCode == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "The input is invalid",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            var result = await _orderServices.PaidOrCancelOrder(orderHashCode, orderStatus);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Update order fail",
                    StatusCode = (int)HttpStatusCode.BadRequest
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Update order success",
                StatusCode = (int)HttpStatusCode.OK
            });
        }
    }
}
