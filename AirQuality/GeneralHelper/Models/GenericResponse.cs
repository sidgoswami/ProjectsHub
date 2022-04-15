using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralHelper.Models
{
    public class GenericResponse<T>
    {
        public GenericResponse(bool success, T data)
        {
            Success = success;
            Data = data;
        }
        public GenericResponse(bool success, T data, string message)
        {
            Success = success;
            Message = message;
            Data = data;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

    }
}
