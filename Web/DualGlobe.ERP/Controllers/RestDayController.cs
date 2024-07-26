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
    public class RestDayController : BaseController
    {
        // GET: RestDay
        public ActionResult Index()
        {
            var conf = RestDayInterface.Read();
            var model = new RestDayModel(conf);
            return View(model);
        }

        public ActionResult RestDay(int? restDayId, string pageMode)
        {
            if (restDayId != null)
            {
                var conf = RestDayInterface.Read(restDayId.Value);
                var model = new RestDayModel(conf);

                model.PageMode = pageMode != null ? pageMode : string.Empty;
                return View(model);
            }
            else
            {
                return View(new RestDayModel());
            }
        }

        [AuthorizeUser(Roles = "Admin")]
        public ActionResult Submit(RestDayModel model)
        {
            if (model.RestDayRecord != null)
            {
                if (model.RestDayRecord.Id == 0)
                {
                    RestDayInterface.Create(model.RestDayRecord);
                }
                else
                {
                    RestDayInterface.Update(model.RestDayRecord);
                    if (model.RestDayRecord.RestDates != null && model.RestDayRecord.RestDates.Count > 0)
                    {
                        foreach (var restDayItem in model.RestDayRecord.RestDates)
                        {
                            if (restDayItem.Id == 0)
                            {
                                restDayItem.RestDayId = model.RestDayRecord.Id;
                                RestDayInterface.InsertRestDateDetail(restDayItem);
                            }
                            else
                                RestDayInterface.UpdateRestDateDetail(restDayItem);
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public void RemoveRestDayItem(int id)
        {
            if (id != 0)
                RestDayInterface.DeleteRestDateDetail(id);
        }
    }
}