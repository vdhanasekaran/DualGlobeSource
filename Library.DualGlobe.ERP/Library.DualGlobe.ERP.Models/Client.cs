using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class Client
	{
		public string AddressLine1
		{
			get;
			set;
		}

		public string AddressLine2
		{
			get;
			set;
		}

		public string BusinessRegistrationNumber
		{
			get;
			set;
		}

		public string City
		{
			get;
			set;
		}

		public string Comments
		{
			get;
			set;
		}

		public string CompanyName
		{
			get;
			set;
		}

		public string Country
		{
			get;
			set;
		}

		public string Currency
		{
			get;
			set;
		}

		public string EmailAddress
		{
			get;
			set;
		}

		public string FaxNumber
		{
			get;
			set;
		}

		public string FirstName
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public string LastName
		{
			get;
			set;
		}

		public string LogoUrl
		{
			get;
			set;
		}

		public string NRICNumber
		{
			get;
			set;
		}

		public string PhoneNumber
		{
			get;
			set;
		}

		public virtual List<Project> projects
		{
			get;
			set;
		}

		public string State
		{
			get;
			set;
		}

		public bool Status
		{
			get;
			set;
		}

		public string Zip
		{
			get;
			set;
		}

		public Client()
		{
		}
	}
}