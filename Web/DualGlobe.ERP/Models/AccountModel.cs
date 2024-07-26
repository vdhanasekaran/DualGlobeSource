using System.Collections.Generic;
using System.Web.Mvc;
using Library.DualGlobe.ERP.Models;
using DualGlobe.ERP.Utility;
using System.ComponentModel.DataAnnotations;

namespace DualGlobe.ERP.Models
{
    public class AccountModel
    {
        public AccountModel()
        {

        }

        public AccountModel(User[]  userArr)
        {
            userArray = userArr;
        }

        public AccountModel(User userSeleted)
        {
            userRecord = userSeleted;
        }

        public User userRecord { get; set; }
        public User[] userArray { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public string PageMode { get; set; }

        public string EmployeeName { get; set; }

        public IEnumerable<SelectListItem> UserGroupList = DropdownBuilder.GetUserGroup();

        public IEnumerable<SelectListItem> EmployeeList = DropdownBuilder.GetAllEmployees();
    }

    public class CustomPrincipalSerializeModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> role { get; set; }
    }
}