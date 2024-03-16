using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityInfoCoreModel.ResponseDTO;

namespace UniversityInfoService.Interfaces
{
    public interface IUniversityInfoService
    {
        Task<List<UniversityInfoResponseDTO>> GetAllUniversityInfo(string Country);
        Task<UniversityInfoResponseDTO> GetUniversityInfoDetail(string Country, int UniversityInfoId);
        Task<ResponseData> AddEditUniversityInfo(UniversityInfoResponseDTO objUniversityInfoRequestDTO);
    }
}
