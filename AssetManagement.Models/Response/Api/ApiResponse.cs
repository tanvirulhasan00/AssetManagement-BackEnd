using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AssetManagement.Models.Response.Api
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public object? Result { get; set; }
        public object? Error { get; set; }
    }
}