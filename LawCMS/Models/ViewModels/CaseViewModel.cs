using System;

namespace LawCMS.Models.ViewModels
{
    public class CaseViewModel
    {
        public int CaseId { get; set; }
        public string Title { get; set; }
        public string ClientName { get; set; }
        public string Matter { get; set; }
        public DateTime? Date { get; set; }

        public string Case_Title { get; set; }
        public string Case_Description { get; set; }
        public string Related_Parties { get; set; }
        public string Attorney_User_Name { get; set; }
        public string Clerk_User_Name { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> Case_Date { get; set; }
    }
}