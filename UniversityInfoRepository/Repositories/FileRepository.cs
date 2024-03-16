using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityInfoCoreModel.ResponseDTO;
using UniversityInfoRepository.DBManagerContext;
using UniversityInfoRepository.Interfaces;

namespace UniversityInfoRepository.Repositories
{
    public class FileRepository : IFileRepository
    {
        public readonly IConfiguration _iConfiguration;
        private DBManager _dbManager; //= new DBManager();
        private DataTable dataTable = null;
        public FileRepository(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
            _dbManager = new DBManager(_iConfiguration);
        }

        public FileEntity GetFileByName(string FileName)
        {
            DataTable dtFileInfo = new DataTable();
            FileEntity fileEntity = new FileEntity();

            dtFileInfo = GetFile(FileName);

            fileEntity.Id = Convert.ToInt32(dtFileInfo.Rows[0]["Id"]);
            fileEntity.FileName = dtFileInfo.Rows[0]["FileName"].ToString();
            return fileEntity;
        }

        public DataTable GetFile(string FileName)
        {
            dataTable = new DataTable();
            string query = "Exec [dbo].[GetFileByName] '" + FileName + "'";
            dataTable = _dbManager.GetData(query);

            return dataTable;
        }

    }
}
