using System.Collections.Generic;
using System.Web.Mvc;
using DualGlobe.ERP.Utility;
using Library.DualGlobe.ERP.Models;

namespace DualGlobe.ERP.Models
{
    public class ProjectModel
    {
        public ProjectModel()
        {
            
        }

        public ProjectModel(Project[]  projectArr)
        {
            projectArray = projectArr;
        }

        public ProjectModel(Project projectSeleted)
        {
            projectRecord = projectSeleted;
        }

        public Project projectRecord { get; set; }
        public Project[] projectArray { get; set; }

        public IEnumerable<SelectListItem> ClientList = DropdownBuilder.GetAllClients();

        public IEnumerable<SelectListItem> StatusList = DropdownBuilder.GetProjectStatus();

        public int ClientId { get; set; }
        public string PageMode { get; set; }
    }
}