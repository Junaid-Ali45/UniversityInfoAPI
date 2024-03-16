using Microsoft.AspNetCore.Mvc;
using UniversityInfoCoreModel.ResponseDTO;

namespace UniversityInformation.Helpers
{
    public static class ResponseMessage
    {
        public static void GenerateSuccessResponse(ResponseDTO<object> response, object data)
        {
            response.StatusCode = 200;
            response.IsSuccess = true;
            response.Message = "Success";
            response.Data = data;
            response.ExceptionMessage = "";
        }

        public static void GenerateNotFoundResponse(ResponseDTO<object> response)
        {
            response.StatusCode = 404;
            response.IsSuccess = false;
            response.Message = "No record found";
            response.Data = null;
            response.ExceptionMessage = "";
        }

        public static void GenerateErrorResponse(ResponseDTO<object> response, int statusCode, string message, string exceptionMessage)
        {
            response.StatusCode = statusCode;
            response.IsSuccess = false;
            response.Message = message;
            response.Data = null;
            response.ExceptionMessage = exceptionMessage;
        }
    }
}
