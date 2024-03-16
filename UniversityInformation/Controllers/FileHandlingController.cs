using Microsoft.AspNetCore.Mvc;
using UniversityInfoCoreModel.ResponseDTO;
using UniversityInformation.Helpers;
using UniversityInfoService.Interfaces;

namespace UniversityInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileHandlingController : Controller
    {
        private readonly IFileService _iSFileService;
        private readonly IConfiguration _iConfiguration;
        public FileHandlingController(IFileService IFileService, IConfiguration iConfiguration)
        {
            _iSFileService = IFileService;
            _iConfiguration = iConfiguration;
        }

        [HttpGet]
        [Route("GetFileByName")]
        public async Task<IActionResult> GetFileByName([FromQuery] string FileName)
        {
            ResponseDTO<object> response = new ResponseDTO<object>();

            try
            {
                var fileEntity = await _iSFileService.SearchFile(FileName);

                if (fileEntity != null)
                {
                    ResponseMessage.GenerateSuccessResponse(response, fileEntity);
                }
                else
                {
                    ResponseMessage.GenerateNotFoundResponse(response);
                }
            }
            catch (FileNotFoundException ex)
            {
                ResponseMessage.GenerateErrorResponse(response, 404, "File not found", ex.Message);
            }
            catch (Exception ex)
            {
                ResponseMessage.GenerateErrorResponse(response, 500, "Internal Server Error", ex.Message);
            }

            return ResponseHelper<object>.GenerateResponse(response);

        }
    }
}
