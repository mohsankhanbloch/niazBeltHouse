//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LawCMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Case
    {
        public int Case_ID { get; set; }
        public Nullable<int> Client_ID { get; set; }
        public Nullable<int> Type_Of_Matter_ID { get; set; }
        public string Case_Title { get; set; }
        public string Case_Description { get; set; }
        public string Related_Parties { get; set; }
        public string Attorney_User_Name { get; set; }
        public string Clerk_User_Name { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> Case_Date { get; set; }
    }
}
