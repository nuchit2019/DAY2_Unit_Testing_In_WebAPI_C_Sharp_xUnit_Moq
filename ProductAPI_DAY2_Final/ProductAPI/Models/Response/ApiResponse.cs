﻿namespace ProductAPI.Models.Response
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

    }
}
