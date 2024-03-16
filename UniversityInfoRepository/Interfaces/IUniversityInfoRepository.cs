using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityInfoCoreModel.ResponseDTO;

namespace UniversityInfoRepository.Interfaces
{
    public interface IUniversityInfoRepository
    {
        Task<List<UniversityInfoResponseDTO>> GetAllUniversityInfo(string Country);
        Task<UniversityInfoResponseDTO> GetUniversityInfoDetail(string Country, int UniversityId);
        Task<ResponseData> AddEditUniversityInfo(UniversityInfoResponseDTO objUniversityInfoRequestDTO);
    }
}
