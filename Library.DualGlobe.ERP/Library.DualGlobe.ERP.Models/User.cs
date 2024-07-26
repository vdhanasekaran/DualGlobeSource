using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class User
	{
		public virtual Employee employee
		{
			get;
			set;
		}

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

		public string IsActive
		{
			get;
			set;
		}

		public string Password
		{
			get;
			set;
		}

		public string Role
		{
			get;
			set;
		}

		public string UserId
		{
			get;
			set;
		}

		public User()
		{
		}
	}
}