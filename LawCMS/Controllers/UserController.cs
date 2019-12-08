using LawCMS.Common;
using LawCMS.Models;
using LawCMS.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LawCMS.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        public ActionResult List()
        {
            //using (dbLawCMSEntities db = new dbLawCMSEntities())
            //{
            //    var xx = db.AspNetRoles.Where(x => x.Name == "Attorney").Select(x=>x.AspNetUsers).ToList();

            //}
            return View();
        }

        public ActionResult Create()
        {
            //using (dbLawCMSEntities db = new dbLawCMSEntities())
            //{
            //    var xx = db.AspNetRoles.Where(x => x.Name == "Attorney").Select(x=>x.AspNetUsers).ToList();

            //}
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult GetUsers()
        {
            using (dbLawCMSEntities db = new dbLawCMSEntities())
            {
                //List<AspNetUser> v = db.AspNetUsers.ToList();

                var users = (
                    from a in db.AspNetUsers
                        //join b in db.AspNetRoles on a.Id equals b.Id
                    where a.Active == true
                    select new ApplicationUserViewModel()
                    {
                        Id = a.Id,
                        Email = a.Email,
                        Password = a.Password,
                        Active = a.Active,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        PhoneNo = a.PhoneNo,

                        //Branch = b.BranchName,
                        //Active = a.Active == true ? "Yes" : "No"
                    }
                    ).ToList();


                foreach (var item in users)
                {
                    if (item.Password != null)
                    {
                        item.Password = DataProtectionLib.Decrypt(item.Password);
                        item.ConfirmPassword = item.Password;
                    }
                }

                return new JsonResult { Data = users, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }



        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult GetUser(string UserId)
        {
            using (dbLawCMSEntities db = new dbLawCMSEntities())
            {
                //List<AspNetUser> v = db.AspNetUsers.ToList();

                var users = (
                    from a in db.AspNetUsers
                        //join b in db.AspNetRoles on a.Id equals b.Id
                    where a.Id == UserId
                    select new ApplicationUserViewModel()
                    {
                        Id = a.Id,
                        Email = a.Email,
                        Password = a.Password,
                        Active = a.Active,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        PhoneNo = a.PhoneNo,

                        //Branch = b.BranchName,
                        //Active = a.Active == true ? "Yes" : "No"
                    }
                    ).ToList();


                foreach (var item in users)
                {
                    if (item.Password != null)
                    {
                        item.Password = DataProtectionLib.Decrypt(item.Password);
                        item.ConfirmPassword = item.Password;
                    }
                }

                return new JsonResult { Data = users, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }



        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UserCreate(ApplicationUserViewModel model)
        {
            //ViewBag.BranchList = _branchService.GetBranchList();
            BaseResponse baseResponse = new BaseResponse();
            // model.Role = "Employee";
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (ModelState.IsValid)
            {
                var checkUser = UserManager.FindByNameAsync(model.Email);
                if (checkUser.Result == null)
                {
                    string newPassword = DataProtectionLib.Encrypt(model.Password);
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        Password = newPassword,
                        Active = model.Active,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNo = model.PhoneNo,

                    };
                    var result = UserManager.Create(user, model.Password);
                    if (result.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, model.Role);
                        if (result1.Succeeded)
                        {
                            baseResponse.Success = true;
                        }
                        else
                        {
                            deleteApplicationUser(user.Id);
                            baseResponse.Success = false;
                            baseResponse.Message = "User not created";
                        }
                    }
                    else
                    {
                        baseResponse.Success = false;
                        baseResponse.Message = "User not created";
                    }
                }
                else
                {
                    baseResponse.Success = false;
                    baseResponse.Message = "User already exist";
                }
            }
            else
            {
                baseResponse.Success = false;
                baseResponse.Message = "Please enter valid data";
            }

            return new JsonResult { Data = baseResponse, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult GetUserTypes()
        {
            //ViewBag.BranchList = _branchService.GetBranchList();
            BaseResponse baseResponse = new BaseResponse();

            using (dbLawCMSEntities db = new dbLawCMSEntities())
            {
                baseResponse.Result = (
                    from a in db.AspNetRoles
                        //join b in db.BrandBranches on a.BranchId equals b.BranchId
                        //where a.Email != "aqibdae@gmail.com" && a.Active
                    select new UserRoleViewModel()
                    {
                        Id = a.Id,
                        Name = a.Name
                        //Branch = b.BranchName,
                        //Active = a.Active == true ? "Yes" : "No"
                    }
                    ).ToList();

            }

            baseResponse.Success = true;

            return new JsonResult { Data = baseResponse, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(ApplicationUserViewModel aspNetUser)
        {
            //ViewBag.BranchList = _branchService.GetBranchList();

            BaseResponse baseResponse = new BaseResponse();
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = UserManager.FindById(aspNetUser.Id);
            user.PasswordHash = UserManager.PasswordHasher.HashPassword(aspNetUser.Password);
            user.Password = DataProtectionLib.Encrypt(aspNetUser.Password);
            user.Active = aspNetUser.Active;
            user.FirstName = aspNetUser.FirstName;
            user.LastName = aspNetUser.LastName;
            user.PhoneNo = aspNetUser.PhoneNo;
            user.Email = aspNetUser.Email;

            IdentityResult result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                baseResponse.Success = true;
            }
            else
            {
                baseResponse.Success = false;
                baseResponse.Message = "User not updated.!";
            }

            return new JsonResult { Data = baseResponse, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult DeleteUser(string userId)
        {
            BaseResponse baseResponse = deleteApplicationUser(userId);
            return new JsonResult { Data = baseResponse, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private BaseResponse deleteApplicationUser(string userId)
        {
            BaseResponse baseResponse = new BaseResponse();
            using (dbLawCMSEntities db = new dbLawCMSEntities())
            {
                var data = db.AspNetUsers.Where(x => x.Id == userId).FirstOrDefault();
                if (data != null)
                {
                    data.Active = false;
                    db.Entry(data).State = EntityState.Modified;
                    int Result = db.SaveChanges();
                    if (Result > 0)
                    {
                        baseResponse.Success = true;
                    }
                    else
                    {
                        baseResponse.Success = false;
                        baseResponse.Message = "User not deleted";
                    }
                }
                else
                {
                    baseResponse.Success = false;
                    baseResponse.Message = "User not found";
                }
            }
            return baseResponse;
        }
    }
}