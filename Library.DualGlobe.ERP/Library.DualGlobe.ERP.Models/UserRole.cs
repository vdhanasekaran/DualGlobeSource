using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class UserRole
	{
		public int? EmployeeId
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public virtual Library.DualGlobe.ERP.Models.Role Role
		{
			get;
			set;
		}

		public int RoleId
		{
			get;
			set;
		}

		public string UserId
		{
			get;
			set;
		}

		public UserRole()
		{
		}
	}
}