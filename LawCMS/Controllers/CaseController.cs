using LawCMS.Models;
using LawCMS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawCMS.Controllers
{
    public class CaseController : Controller
    {
        // GET: Case
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult ViewCase()
        {
            return View();
        }

        public ActionResult CreateCase(AddCaseViewModel addCaseViewModel)
        {
            BaseResponse baseResponse = new BaseResponse();

            using (dbLawCMSEntities db = new dbLawCMSEntities())
            {
                db.tbl_Case.Add(addCaseViewModel.Case);

                db.tbl_Client.Add(addCaseViewModel.Client);

                db.tbl_Case_Attachments.Add(addCaseViewModel.Attachments);

                db.tbl_Case_Notes.Add(addCaseViewModel.Notes);

                db.SaveChanges();

            }

            return new JsonResult { Data = baseResponse, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public BaseResponse SaveAttachments()
        {
            return new BaseResponse();

        }

        public ActionResult GetAllCases()
        {
            BaseResponse baseResponse = new BaseResponse();

            using (dbLawCMSEntities db = new dbLawCMSEntities())
            {
                baseResponse.Result = (
                     from a in db.tbl_Case
                         //join b in db.tbl_Client on a.Client_ID equals b.Client_ID
                         //where a.Email != "aqibdae@gmail.com" && a.Active
                     select new CaseViewModel()
                     {
                         Case_Title = a.Case_Title,
                         Case_Description = a.Case_Description,
                         Status = a.Status,

                     }
                     ).ToList();


            }

            baseResponse.Success = true;

            return new JsonResult { Data = baseResponse, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }






    }
}