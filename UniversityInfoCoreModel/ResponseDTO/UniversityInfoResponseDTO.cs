using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityInfoCoreModel.ResponseDTO
{
    public class UniversityInfoResponseDTO
    {
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string Domain { get; set; }   
        public string WebSite { get; set; }
        public string CountryCode { get; set; }
        public string State {  get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsNew { get; set; }
    }
}
