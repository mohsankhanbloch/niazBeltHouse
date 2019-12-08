using LawCMS.Models;
using LawCMS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawCMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            tbl_Case tbl_Case = new tbl_Case();

            using (dbLawCMSEntities db = new dbLawCMSEntities())
            {

            }

            return View();
        }

        [HttpPost]
        public ActionResult GetDashboardData()
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                DashboardViewModel response = new DashboardViewModel();
                using (dbLawCMSEntities db = new dbLawCMSEntities())
                {
                    response.totalClients = db.tbl_Client.Count();
                    response.totalCases = db.tbl_Case.Count();
                    response.completedCases = db.tbl_Case.Where(x => x.Status == "Completed").Count();
                    response.inCompleteCases = db.tbl_Case.Where(x => x.Status == "InCompleted").Count();
                    //response.cases = db.tbl_Case.OrderByDescending(x => x.Case_Date).Take(5).ToList();

                    response.cases = (
                        from a in db.tbl_Case
                        join b in db.tbl_Client on a.Client_ID equals b.Client_ID
                        join c in db.tbl_Type_Of_Matter on a.Type_Of_Matter_ID equals c.Type_Of_Matter_ID
                        select new CaseViewModel()
                        {
                            CaseId = a.Case_ID,
                            Title = a.Case_Title,
                            ClientName = b.First_Name + " " + b.Last_Name,
                            Matter = c.Type_Of_Matter,
                            Date = a.Case_Date
                        }
                        ).Take(10).OrderByDescending(x => x.Date).ToList();
                }
                baseResponse.Success = true;
                baseResponse.Result = response;
                return new JsonResult { Data = baseResponse, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception)
            {
                baseResponse.Success = false;
                return new JsonResult { Data = baseResponse, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}