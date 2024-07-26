using System.Web.Mvc;
using Library.DualGlobe.ERP.Interfaces;
using Library.DualGlobe.ERP.Utilities;
using DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using System.Web.Security;
using System.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DualGlobe.ERP.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        // GET: Login       
        public ActionResult Index()
        {
            return View();
        }

        //public PartialViewResult DynamicMenu(int? roleCode)
        //{
        //    List<DynamicMenuViewModel> dynamicMenuList = new List<DynamicMenuViewModel>();
        //    if (((CustomPrincipal)HttpContext.User) != null)
        //    {
        //        var userCode = ((CustomPrincipal)HttpContext.User).UserId;
        //        if (roleCode != null && roleCode.GetValueOrDefault() != 0)
        //        {
        //            var dynamicMenus = RoleMenuInterface.Read();
        //            if (dynamicMenus != null && dynamicMenus.Count > 0)
        //            {
        //                dynamicMenus = dynamicMenus.Where(i => i.RoleId== roleCode).ToList();
        //                foreach (var item in dynamicMenus)
        //                {
        //                    var menuItem = MenuInterface.Read(item.MenuId);
        //                    dynamicMenuList.Add(new DynamicMenuViewModel() { Id = menuItem.Id, MenuName = menuItem.MenuName, MenuURL = menuItem.MenuURL, ParentMenucode = menuItem.ParentMenucode });
        //                }
        //            }
        //        }
        //        else
        //        {
        //            var menuList = MenuInterface.Read();
        //            if (menuList != null && menuList.Any())
        //            {
        //                foreach (var item in menuList)
        //                {
        //                    dynamicMenuList.Add(new DynamicMenuViewModel() { Id = item.Id, MenuName = item.MenuName, MenuURL = item.MenuURL, ParentMenucode = item.ParentMenucode });
        //                }
        //            }
        //        }
        //    }
        //    return PartialView("_DynamicMenu", dynamicMenuList);
        //}

        [HttpPost]
        public ActionResult Login(AccountModel model, string returnUrl = "")
        {
            if (model.userRecord != null)
            {
                if (!string.IsNullOrEmpty(model.userRecord.UserId))
                {
                    var userRecord = UserInterface.ReadByUsername(model.userRecord.UserId);

                    if (userRecord == null)
                    {
                        ViewBag.ErrorMessage = "Invalid User Name";
                        return View("Index");
                    }
                    else
                    {
                        if (model.userRecord.Password == SecurityHelper.Decrypt(userRecord.Password))
                        {
                            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                            serializeModel.UserId = userRecord.UserId;
                            serializeModel.FirstName = userRecord.employee != null ? userRecord.employee.FirstName : "Admin";
                            serializeModel.LastName = userRecord.employee != null ? userRecord.employee.LastName : "";
                            //serializeModel.role = Utilities.GetUserRole(model.userRecord.UserId);
                            List<string> roleList = new List<string>();
                            roleList.Add(userRecord.Role);
                            serializeModel.role = roleList;

                            string userData = JsonConvert.SerializeObject(serializeModel);
                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                                                    1,
                                                                    userRecord.UserId,
                                                                    DateTime.Now,
                                                                    DateTime.Now.AddDays(1),
                                                                    false,
                                                                    userData);

                            string encTicket = FormsAuthentication.Encrypt(authTicket);
                            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                            Response.Cookies.Add(faCookie);

                            //FormsAuthentication.SetAuthCookie(model.userRecord.UserId, false);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Incorrect Password";
                            return View("Index");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Account");
                }

            }
            else
            {
                return RedirectToAction("Index", "Account");
            }


        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Account");
        }

        public ActionResult AccessDenied()
        {
            return View("~/Views/Shared/AccessDenied.cshtml");
        }

        // GET: UserDetails
        public ActionResult UserDetails()
        {
            var conf = UserInterface.Read();
            var model = new AccountModel(conf);
            return View(model);
        }

        public ActionResult AddUser(int? id, string pageMode)
        {
            if (id != null)
            {
                var conf = UserInterface.Read(id.Value);
                var model = new AccountModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                var model = new AccountModel();
                model.PageMode = "Edit";
                return View(model);
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult SubmitUser(AccountModel accountModel)
        {
            if (accountModel.userRecord != null)
            {
                if (accountModel.userRecord.Id == 0)
                {
                    accountModel.userRecord.Password = SecurityHelper.Encrypt(accountModel.userRecord.Password);
                    UserInterface.Create(accountModel.userRecord);
                }
                else
                {
                    UserInterface.Update(accountModel.userRecord);
                }
            }
            return RedirectToAction("UserDetails");
        }

        public JsonResult GetSecurityPassword(string pwd)
        {
            string encryptedPassword = SecurityHelper.Encrypt(pwd);
            return Json(encryptedPassword, JsonRequestBehavior.AllowGet);
        }
    }
}
