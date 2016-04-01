using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeMS.Models
{
    public class IndexParameterModel
    {
        public string SortOrder { get; set; }
        public string SearchBy { get; set; }
        public string CurrentFilter { get; set; }
        [Range(minimum:0, maximum:int.MaxValue)]
        public int PageNo { get; set; }
        [Range(0, int.MaxValue)]
        public int PerPage {get;set;}
    }
}