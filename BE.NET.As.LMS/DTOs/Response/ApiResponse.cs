using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.DTOs.Response
{
    public class ApiResponse<T> where T : class
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
    }
}
