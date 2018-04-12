using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VS2015_Angular2_Services.Models
{
    public class Employee : BaseAuditable
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public int? ManagerID { get; set; }
        public string ContactNo { get; set; }
        public string EMailId { get; set; }
        public string SkillSets { get; set; }
        public string ImagePath { get; set; }
    }

    public class ManagerViewModel
    {
        public int? ManagerID { get; set; }
        public string ManagerName { get; set; }
    }

    public class EmployeeViewModel
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public int? ManagerID { get; set; }
        public string ContactNo { get; set; }
        public string EMailId { get; set; }
        public string SkillSets { get; set; }
        public string ManagerName { get; set; }
        public string ImagePath { get; set; }
    }

    public class EmployeeDDLViewModel
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
    }

    public class EmployeeInputViewModel
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public int? ManagerID { get; set; }
        public string ContactNo { get; set; }
        public string EMailId { get; set; }
        public string SkillSets { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public string ImgFile { get; set; }
		public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}