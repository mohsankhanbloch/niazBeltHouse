using System.Collections.Generic;

namespace LawCMS.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int totalClients { get; set; }
        public int completedCases { get; set; }
        public int inCompleteCases { get; set; }
        public int totalCases { get; set; }
        public List<CaseViewModel> cases { get; set; }
    }
}