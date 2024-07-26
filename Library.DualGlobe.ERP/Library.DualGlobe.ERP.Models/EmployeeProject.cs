using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class EmployeeProject
	{
		public virtual Employee employee
		{
			get;
			set;
		}

		public int EmployeeId
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public virtual Project project
		{
			get;
			set;
		}

		public int projectId
		{
			get;
			set;
		}

		public EmployeeProject()
		{
		}
	}
}