using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VS2015_Angular2_Services.Models
{
    public class ManagerComment : BaseAuditable
    {
        public int ID { get; set; }
        public string Comments { get; set; }
        public DateTime CommentDate { get; set; }
        public int ProjectTaskID { get; set; }
        public virtual ProjectTask ProjectTask { get; set; }
    }

    public class CommentViewModel
    {
        public int ID { get; set; }
        public string Comments { get; set; }
        public DateTime CommentDate { get; set; }
        public int ProjectTaskID { get; set; }
        public string EmployeeName { get; set; }
        public string UserStoryTitle { get; set; }
        public string ProjectName { get; set; }
    }

}