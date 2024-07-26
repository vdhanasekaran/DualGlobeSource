using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class Project
	{
		public virtual Client client
		{
			get;
			set;
		}

		public int ClientId
		{
			get;
			set;
		}

		public DateTime EndDate
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public string ProjectDescription
		{
			get;
			set;
		}

		public string ProjectName
		{
			get;
			set;
		}

		public DateTime StartDate
		{
			get;
			set;
		}

		public string Status
		{
			get;
			set;
		}

		public Project()
		{
		}
	}
}