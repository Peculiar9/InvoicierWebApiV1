using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicierWebApiV1.Core.Shared
{
    public class Response
    {

        public string Message { get; set; }
        public string Subject { get; set; }
        public int StatusCode { get; set; }
        public object Data { get; set; }
        public Response success(string? message, object? data = null, ResponseType? statuscode = ResponseType.Successful)
        {
            var response = new Response();
            if (data == null)
            {
                response.Message = $"Successful Add {nameof(data)} ";
            }
            response.Message = "Successful Request";
            response.Data = data;
            response.StatusCode = (int)statuscode;
            return response;
        }
        public Response failed(string? message, object? data = null, ResponseType? statuscode = ResponseType.NotFound)
        {
            var response = new Response();
            if (data == null)
            {
                response.Message = $"Successful Add {nameof(data)} ";
            }
            response.Message = "Successful Request";
            response.Data = data;
            response.StatusCode = (int)statuscode.Value;
            return response;
        }

    }
}
