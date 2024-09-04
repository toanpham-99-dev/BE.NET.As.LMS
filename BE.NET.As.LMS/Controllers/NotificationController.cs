using BE.NET.As.LMS.Controllers.Base;
using BE.NET.As.LMS.Core.Interfaces;
using BE.NET.As.LMS.Core.Models;
using BE.NET.As.LMS.DTOs.Input;
using BE.NET.As.LMS.DTOs.Output;
using BE.NET.As.LMS.DTOs.Response;
using BE.NET.As.LMS.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Controllers
{
    public class NotificationController : BaseAPIController
    {
        private readonly INotificationServices _notificationServices;
        private readonly INotificationUserService _notificationUserServices;
        public NotificationController(INotificationServices notificationServices,
            INotificationUserService notificationUserServices)
        {
            _notificationServices = notificationServices;
            _notificationUserServices = notificationUserServices;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getall-paging-filter-search-notification")]
        public async Task<IActionResult> GetAllPagingFilterSearch(string searchString, int pageSize,
            int pageIndex, bool? isRead)
        {
            if (pageSize <= 0 || pageIndex <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Get notifications failed"
                });
            var result = await _notificationServices.GetAllPagingFilterSearch(searchString, pageSize,
                pageIndex, isRead);
            if (result == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Get notifications failed"
                });
            return Ok(new ApiResponse<List<NotificationOutput>>
            {
                Data = result,
                Message = "Get notifications success"
            });
        }
        [Authorize]
        [HttpGet("getall-by-user-hash-code")]
        public async Task<IActionResult> GetAllByUserHashCode()
        {
            string userHashCode = Helper.GetCurrentUserHashCode(this.User);
            var result = await _notificationServices.GetAllByUserHashCode(userHashCode);
            if (result == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Get notifications failed"
                });
            return Ok(new ApiResponse<List<NotificationOutput>>
            {
                Data = result,
                Message = "Get notifications success"
            });
        }
        [Authorize(Roles = "admin")]
        [HttpPost("add-notification")]
        public async Task<IActionResult> AddNotification(NotificationInput notificationInput)
        {
            var result = await _notificationServices.AddNotification(notificationInput);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Add notification failed"
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Add notification success"
            });
        }

        [Authorize(Roles = "admin")]
        [HttpPut("update-detail-notification")]
        public async Task<IActionResult> UpdateDetailNotification
            (NotificationUpdateDetailInput notificationUpdateDetailInput)
        {
            var result = await _notificationServices.UpdateDetailNotification(notificationUpdateDetailInput);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Update notification failed"
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Update notification success"
            });
        }

        [Authorize]
        [HttpPut("update-inline-notification")]
        public async Task<IActionResult> UpdateInlineNotification(NotificationInlineInput notificationInlineInput)
        {
            var result = await _notificationServices.UpdateInlineNotification(notificationInlineInput);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Update notification failed"
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Update notification success"
            });
        }

        [Authorize(Roles = "admin")]
        [HttpPut("delete-notification")]
        public async Task<IActionResult> DeleteNotification(string hashCode)
        {
            var result = await _notificationServices.DeleteNotification(hashCode);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Delete notification failed"
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Delete notification success"
            });
        }
        [HttpGet("get-notification")]
        [Authorize]
        public async Task<IActionResult> GetNotification(string hashCode)
        {
            var result = await _notificationServices.GetNotification(hashCode);
            if (result == null)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Get notification failed"
                });
            return Ok(new ApiResponse<NotificationOutput>
            {
                Data = result,
                Message = "Get notification success"
            });
        }

        [HttpPost("send-notification")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> SendNotification(string userHashCode, string notificationHashCode)
        {
            if (string.IsNullOrEmpty(userHashCode) || string.IsNullOrEmpty(notificationHashCode))
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Send notification failed"
                });
            var result = await _notificationUserServices.SendNotification(userHashCode, notificationHashCode);
            if (result <= 0)
                return BadRequest(new ApiResponse<string>
                {
                    Message = "Send notification failed"
                });
            return Ok(new ApiResponse<string>
            {
                Message = "Send notification success"
            });
        }
    }
}
