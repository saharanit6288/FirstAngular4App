using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VS2015_Angular2_Services.Models
{
    public class Project : BaseAuditable
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public string ClientName { get; set; }
    }

    public class ProjectDDLViewModel
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
    }
}