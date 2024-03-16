using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityInfoCoreModel.ResponseDTO;
using UniversityInfoRepository.Interfaces;
using UniversityInfoRepository.Repositories;
using UniversityInfoService.Interfaces;

namespace UniversityInfoService.Services
{
    public class FileService : IFileService
    {
        //private readonly IThirdPartyService _thirdPartyService;
        private readonly IFileRepository _fileRepository;

        public FileService(/*IThirdPartyService thirdPartyService,*/ IFileRepository fileRepository)
        {
            //_thirdPartyService = thirdPartyService;
            _fileRepository = fileRepository;
        }

        public async Task<FileEntity> SearchFile(string fileName)
        {
            //var file = _thirdPartyService.SearchFile(fileName);
            var file = (dynamic)null;
            if (file == null)
            {
                file = _fileRepository.GetFileByName(fileName);
            }

            return file;
        }
    }
}
