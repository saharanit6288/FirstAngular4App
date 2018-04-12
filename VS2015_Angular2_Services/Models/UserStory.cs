using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VS2015_Angular2_Services.Models
{
    public class UserStory : BaseAuditable
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Story { get; set; }
        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }
    }

    public class UserStoryViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Story { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
    }

    public class UserStoryDDLViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
    }
}