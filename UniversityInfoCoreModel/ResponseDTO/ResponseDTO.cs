using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityInfoCoreModel.ResponseDTO
{
    public class ResponseDTO<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
    }
    public class ResponseData
    {
        public int ID { get; set; }
        public string Message { get; set; }
    }
}