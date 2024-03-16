using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityInfoCoreModel.ResponseDTO;
using UniversityInfoRepository.Interfaces;
using UniversityInfoService.Interfaces;

namespace UniversityInfoService.Services
{
    public class UniversityInfoService : IUniversityInfoService
    {
        private readonly IUniversityInfoRepository _iRUniversityInfo;
        public readonly IConfiguration _iConfiguration;

        public UniversityInfoService(IUniversityInfoRepository iRUniversityInfo, IConfiguration iConfiguration)
        {
            _iRUniversityInfo = iRUniversityInfo;
            _iConfiguration = iConfiguration;
        }
        public async Task<List<UniversityInfoResponseDTO>> GetAllUniversityInfo(string Country)
        {
            return await _iRUniversityInfo.GetAllUniversityInfo(Country);
        }
        public async Task<UniversityInfoResponseDTO> GetUniversityInfoDetail(string Country, int UniversityId)
        {
            return await _iRUniversityInfo.GetUniversityInfoDetail(Country, UniversityId);
        }
        public async Task<ResponseData> AddEditUniversityInfo(UniversityInfoResponseDTO objUniversityInfoRequestDTO)
        {
            return await _iRUniversityInfo.AddEditUniversityInfo(objUniversityInfoRequestDTO);
        }
    }
}
