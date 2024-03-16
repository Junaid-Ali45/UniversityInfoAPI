using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityInfoCoreModel.ResponseDTO;

namespace UniversityInfoService.Interfaces
{
    public interface IFileService
    {
        Task<FileEntity> SearchFile(string fileName);
    }
}
