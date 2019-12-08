using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawCMS.Models.ViewModels
{
    public class AddCaseViewModel
    {
        public tbl_Case Case { get; set; }
        public tbl_Client Client { get; set; }
        public tbl_Case_Attachments Attachments { get; set; }
        public tbl_Case_Notes Notes { get; set; }
    }
}