using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Models
{
	public class LevyLookup
	{
		public decimal? BasicTier
		{
			get;
			set;
		}

		[Column(TypeName="date")]
		public DateTime FromDate
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public decimal? MYNBasicSkilled
		{
			get;
			set;
		}

		public decimal? MYNHighSkilled
		{
			get;
			set;
		}

		public decimal? MYNWaiverBasicSkilled
		{
			get;
			set;
		}

		public decimal? MYNWaiverHighSkilled
		{
			get;
			set;
		}

		public string PassType
		{
			get;
			set;
		}

		public string Sector
		{
			get;
			set;
		}

		public decimal? Tier2
		{
			get;
			set;
		}

		[Column(TypeName="date")]
		public DateTime ToDate
		{
			get;
			set;
		}

		public LevyLookup()
		{
		}
	}
}