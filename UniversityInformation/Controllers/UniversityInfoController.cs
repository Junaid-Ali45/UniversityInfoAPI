using Microsoft.AspNetCore.Mvc;
using UniversityInfoCoreModel.ResponseDTO;
using UniversityInformation.Helpers;
using UniversityInfoService.Interfaces;

namespace UniversityInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityInfoController : Controller
    {
        private readonly IUniversityInfoService _iSUniversityInfo;
        private readonly IConfiguration _iConfiguration;
        public UniversityInfoController(IUniversityInfoService iUniversityInfoService, IConfiguration iConfiguration)
        {
            _iSUniversityInfo = iUniversityInfoService;
            _iConfiguration = iConfiguration;
        }

        [HttpGet]
        [Route("GetAllUniversityInfo")]
        public async Task<IActionResult> GetAllUniversityInfo([FromQuery] string Country)
        {
            ResponseDTO<object> response = new ResponseDTO<object>();
            try
            {

                var universityInfoResponseDTO = await _iSUniversityInfo.GetAllUniversityInfo(Country);
                //SiteInfoResponseDTO respObj = new SiteInfoResponseDTO
                //{
                //    ordersList = orders
                //};
                if (universityInfoResponseDTO != null)
                {
                    response.StatusCode = 200;
                    response.IsSuccess = true;
                    response.Message = "success";
                    response.Data = universityInfoResponseDTO;
                    response.ExceptionMessage = "";
                }
                else
                {
                    response.StatusCode = 404;
                    response.IsSuccess = false;
                    response.Message = "No record found";
                    response.Data = null;
                    response.ExceptionMessage = "";
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.IsSuccess = false;
                response.Message = "Internal Server Error";
                response.Data = null;
                response.ExceptionMessage = ex.Message.ToString();
            }
            return ResponseHelper<object>.GenerateResponse(response);

        }
        [HttpGet]
        [Route("GetUniversityInfoDetail")]
        public async Task<IActionResult> GetUniversityInfoDetail([FromQuery] string Country, int UniversityInfoId)
        {
            ResponseDTO<object> response = new ResponseDTO<object>();
            try
            {

                var UniversityInfoResponseDTO = await _iSUniversityInfo.GetUniversityInfoDetail(Country, UniversityInfoId);
                //SiteInfoResponseDTO respObj = new SiteInfoResponseDTO
                //{
                //    ordersList = orders
                //};
                if (UniversityInfoResponseDTO != null)
                {
                    response.StatusCode = 200;
                    response.IsSuccess = true;
                    response.Message = "success";
                    response.Data = UniversityInfoResponseDTO;
                    response.ExceptionMessage = "";
                }
                else
                {
                    response.StatusCode = 404;
                    response.IsSuccess = false;
                    response.Message = "No record found";
                    response.Data = null;
                    response.ExceptionMessage = "";
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.IsSuccess = false;
                response.Message = "Internal Server Error";
                response.Data = null;
                response.ExceptionMessage = ex.Message.ToString();
            }
            return ResponseHelper<object>.GenerateResponse(response);

        }
        [HttpPost]
        [Route("AddEditUniversityInfo")]
        public async Task<IActionResult> AddEditUniversityInfo([FromBody] UniversityInfoResponseDTO objUniversityInfoRequestDTO)
        {
            ResponseDTO<object> response = new ResponseDTO<object>();
            try
            {

                var responseDTO = await _iSUniversityInfo.AddEditUniversityInfo(objUniversityInfoRequestDTO);
                //SiteInfoResponseDTO respObj = new SiteInfoResponseDTO
                //{
                //    ordersList = orders
                //};
                if (responseDTO != null)
                {
                    response.StatusCode = 200;
                    response.IsSuccess = true;
                    response.Message = "success";
                    response.Data = responseDTO;
                    response.ExceptionMessage = "";
                }
                else
                {
                    response.StatusCode = 404;
                    response.IsSuccess = false;
                    response.Message = "No record found";
                    response.Data = null;
                    response.ExceptionMessage = "";
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.IsSuccess = false;
                response.Message = "Internal Server Error";
                response.Data = null;
                response.ExceptionMessage = ex.Message.ToString();
            }
            return ResponseHelper<object>.GenerateResponse(response);

        }
    }
}
