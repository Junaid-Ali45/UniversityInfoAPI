using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityInfoCoreModel.ResponseDTO;

namespace UniversityInfoRepository.Interfaces
{
    public interface IFileRepository
    {
        FileEntity GetFileByName(string fileName);
    }
}
