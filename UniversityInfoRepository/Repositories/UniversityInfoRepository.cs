using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityInfoCoreModel.ResponseDTO;
using UniversityInfoRepository.DBManagerContext;
using UniversityInfoRepository.Interfaces;

namespace UniversityInfoRepository.Repositories
{
    public class UniversityInfoRepository : IUniversityInfoRepository
    {
        public readonly IConfiguration _iConfiguration;
        private DBManager _dbManager; //= new DBManager();
        private DataTable dataTable = null;
        public UniversityInfoRepository(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
            _dbManager = new DBManager(_iConfiguration);
        }

        public async Task<List<UniversityInfoResponseDTO>> GetAllUniversityInfo(string Country)
        {
            DataTable dtUniversityInfoList = new DataTable();
            List<UniversityInfoResponseDTO> universityInfoResponseDTOs = new List<UniversityInfoResponseDTO>();

            dtUniversityInfoList = GetAllUniversityInfos(Country);


            for (int i = 0; i < dtUniversityInfoList.Rows.Count; i++)
            {
                UniversityInfoResponseDTO universityInfoResponseDTO = new UniversityInfoResponseDTO();
                universityInfoResponseDTO.UniversityId = Convert.ToInt32(dtUniversityInfoList.Rows[i]["UniversityId"]);
                universityInfoResponseDTO.UniversityName = dtUniversityInfoList.Rows[i]["UniversityName"].ToString();
                universityInfoResponseDTO.Domain = dtUniversityInfoList.Rows[i]["Domain"].ToString();
                universityInfoResponseDTO.WebSite = dtUniversityInfoList.Rows[i]["WebSite"].ToString();
                universityInfoResponseDTO.CountryCode = dtUniversityInfoList.Rows[i]["CountryCode"].ToString();
                universityInfoResponseDTO.State = dtUniversityInfoList.Rows[i]["State"].ToString();
                universityInfoResponseDTO.Country = dtUniversityInfoList.Rows[i]["Country"].ToString();
                universityInfoResponseDTO.IsActive = Convert.ToBoolean(dtUniversityInfoList.Rows[i]["IsActive"]);
                universityInfoResponseDTO.CreatedBy = dtUniversityInfoList.Rows[i]["CreatedBy"].ToString();
                universityInfoResponseDTO.CreatedOn = Convert.ToDateTime(dtUniversityInfoList.Rows[i]["CreatedOn"]);
                
                universityInfoResponseDTOs.Add(universityInfoResponseDTO);
            }
            return universityInfoResponseDTOs;
        }
        public async Task<UniversityInfoResponseDTO> GetUniversityInfoDetail(string Country, int UniversityId)
        {
            DataTable dtUniversityInfoList = new DataTable();
            UniversityInfoResponseDTO universityInfoResponseDTO = new UniversityInfoResponseDTO();
            dtUniversityInfoList = GetUniversityInfoDetails(Country, UniversityId);
            if (dtUniversityInfoList.Rows.Count > 0)
            {
                universityInfoResponseDTO.UniversityId = Convert.ToInt32(dtUniversityInfoList.Rows[0]["UniversityId"]);
                universityInfoResponseDTO.UniversityName = dtUniversityInfoList.Rows[0]["UniversityName"].ToString();
                universityInfoResponseDTO.Domain = dtUniversityInfoList.Rows[0]["Domain"].ToString();
                universityInfoResponseDTO.WebSite = dtUniversityInfoList.Rows[0]["WebSite"].ToString();
                universityInfoResponseDTO.CountryCode = dtUniversityInfoList.Rows[0]["CountryCode"].ToString();
                universityInfoResponseDTO.State = dtUniversityInfoList.Rows[0]["State"].ToString();
                universityInfoResponseDTO.Country = dtUniversityInfoList.Rows[0]["Country"].ToString();
                universityInfoResponseDTO.IsActive = Convert.ToBoolean(dtUniversityInfoList.Rows[0]["IsActive"]);
                universityInfoResponseDTO.CreatedBy = dtUniversityInfoList.Rows[0]["CreatedBy"].ToString();
                universityInfoResponseDTO.CreatedOn = Convert.ToDateTime(dtUniversityInfoList.Rows[0]["CreatedOn"]);
            }
            return universityInfoResponseDTO;
        }
        public async Task<ResponseData> AddEditUniversityInfo(UniversityInfoResponseDTO objUniversityInfoRequestDTO)
        {
            ResponseData responseData = new ResponseData();
            responseData.Message = AddEditUniversityInfos(objUniversityInfoRequestDTO);
            return responseData;
        }
        public DataTable GetAllUniversityInfos(string Country)
        {
            dataTable = new DataTable();
            string query = "Exec [dbo].[GetUniversityInfoDetail] '" + Country + "', NULL";
            dataTable = _dbManager.GetData(query);

            return dataTable;
        }
        public DataTable GetUniversityInfoDetails(string Country, int UniversityId)
        {
            dataTable = new DataTable();
            string query = "Exec GetUniversityInfoDetail '" + Country + "', " + UniversityId;
            dataTable = _dbManager.GetData(query);

            return dataTable;
        }
        public string AddEditUniversityInfos(UniversityInfoResponseDTO objUniversityInfoRequestDTO)
        {
            string query = "dbo.AddEditUniversityInfo";
            DateTime CreatedOn = DateTime.Now;

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UniversityId", objUniversityInfoRequestDTO.UniversityId),
                new SqlParameter("@UniversityName", objUniversityInfoRequestDTO.UniversityName.ToString()),
                new SqlParameter("@Domain", objUniversityInfoRequestDTO.Domain.ToString()),
                new SqlParameter("@WebSite", objUniversityInfoRequestDTO.WebSite.ToString()),
                new SqlParameter("@CountryCode", objUniversityInfoRequestDTO.CountryCode.ToString()),
                new SqlParameter("@State", objUniversityInfoRequestDTO.State.ToString()),
                new SqlParameter("@Country", objUniversityInfoRequestDTO.Country.ToString()),
                new SqlParameter("@IsActive", objUniversityInfoRequestDTO.IsActive),
                new SqlParameter("@CreatedBy", objUniversityInfoRequestDTO.CreatedBy.ToString()),
                new SqlParameter("@CreatedOn", CreatedOn),
                new SqlParameter("@IsNew", objUniversityInfoRequestDTO.IsNew),

            };
            if (_dbManager.SetDataStoreProc(query, parameters) > 0)
            {
                return "Saved";
            }
            else
            {
                return "something went wrong";
            }
        }
    }
}
