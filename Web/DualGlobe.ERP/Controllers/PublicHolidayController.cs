using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Interfaces;

namespace DualGlobe.ERP.Controllers
{
    public class PublicHolidayController : BaseController
    {
        // GET: PublicHoliday
        public ActionResult Index(int? year)
        {
            if (year == null)
            {
                year = DateTime.Today.Year;
            }
            var conf = PublicHolidayInterface.ReadByYear(year.Value);
            var model = new PublicHolidayModel(conf);
            model.SelectedYear = year.ToString();
            return View(model);
        }

        public ActionResult Search(PublicHolidayModel model)
        {
            return RedirectToAction("Index", new { year = model.SelectedYear});
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(PublicHolidayModel publicHolidayModel)
        {
            if (publicHolidayModel.PublicHolidayArray != null)
            {
                foreach (var record in publicHolidayModel.PublicHolidayArray)
                {
                    if (record.Id == 0)
                    {
                        PublicHolidayInterface.Create(record);
                    }
                    else
                    {
                        PublicHolidayInterface.Update(record);
                    }
                }
            }
            return RedirectToAction("Index", new { year = publicHolidayModel.SelectedYear });
        }

        public void RemoveHolidayItem(int id)
        {
            if (id != 0)
                PublicHolidayInterface.Delete(id);
        }
    }
}