using System;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class Menu
	{
		public int Id
		{
			get;
			set;
		}

		public string MenuName
		{
			get;
			set;
		}

		public string MenuType
		{
			get;
			set;
		}

		public string MenuURL
		{
			get;
			set;
		}

		public int ParentMenucode
		{
			get;
			set;
		}

		public Menu()
		{
		}
	}
}