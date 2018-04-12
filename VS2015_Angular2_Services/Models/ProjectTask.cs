using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VS2015_Angular2_Services.Models
{
    public class ProjectTask : BaseAuditable
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskEndDate { get; set; }
        public int TaskCompletion { get; set; }
        public int UserStoryID { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual UserStory UserStory { get; set; }
    }

    public class ProjectTaskViewModel
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskEndDate { get; set; }
        public int TaskCompletion { get; set; }
        public int UserStoryID { get; set; }
        public string EmployeeName { get; set; }
        public string UserStoryTitle { get; set; }
    }
}