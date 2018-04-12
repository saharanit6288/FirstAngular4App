using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VS2015_Angular2_Services.Models
{
    public class BaseAuditable
    {
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}