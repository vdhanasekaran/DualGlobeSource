using Library.DualGlobe.ERP.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Library.DualGlobe.ERP.Interfaces
{
	public class LevyLookupInterface
	{
		public LevyLookupInterface()
		{
		}

		public static decimal GetLevy(Employee emp, DateTime date)
		{
			decimal? basicTier;
			decimal num;
			using (ERPContext context = new ERPContext())
			{
				decimal returnVal = new decimal();
				IQueryable<LevyLookup> levy = 
					from t in context.LevyLookups
					where (t.ToDate >= date) && (t.FromDate <= date)
					select t;
				if (emp.WorkStatus != "WorkPermit")
				{
					if (emp.WorkStatus == "SPass")
					{
						if (string.Compare(emp.TierType, "Basic", StringComparison.OrdinalIgnoreCase) == 0)
						{
							if (levy != null)
							{
								basicTier = (
									from l in levy
									where l.PassType == emp.WorkStatus
									select l).FirstOrDefault<LevyLookup>().BasicTier;
								returnVal = basicTier.GetValueOrDefault(decimal.Zero);
							}
						}
						else if (levy != null)
						{
							basicTier = (
								from l in levy
								where l.PassType == emp.WorkStatus
								select l).FirstOrDefault<LevyLookup>().Tier2;
							returnVal = basicTier.GetValueOrDefault(decimal.Zero);
						}
					}
				}
				else if (emp.MYE.GetValueOrDefault())
				{
					if (string.Compare(emp.Skill, "Basic", StringComparison.OrdinalIgnoreCase) == 0)
					{
						if (levy != null)
						{
							basicTier = (
								from l in levy
								where l.PassType == emp.WorkStatus
								select l).FirstOrDefault<LevyLookup>().MYNBasicSkilled;
							returnVal = basicTier.GetValueOrDefault(decimal.Zero);
						}
					}
					else if (levy != null)
					{
						basicTier = (
							from l in levy
							where l.PassType == emp.WorkStatus
							select l).FirstOrDefault<LevyLookup>().MYNHighSkilled;
						returnVal = basicTier.GetValueOrDefault(decimal.Zero);
					}
				}
				else if (string.Compare(emp.Skill, "Basic", StringComparison.OrdinalIgnoreCase) == 0)
				{
					if (levy != null)
					{
						basicTier = (
							from l in levy
							where l.PassType == emp.WorkStatus
							select l).FirstOrDefault<LevyLookup>().MYNWaiverBasicSkilled;
						returnVal = basicTier.GetValueOrDefault(decimal.Zero);
					}
				}
				else if (levy != null)
				{
					basicTier = (
						from l in levy
						where l.PassType == emp.WorkStatus
						select l).FirstOrDefault<LevyLookup>().MYNWaiverHighSkilled;
					returnVal = basicTier.GetValueOrDefault(decimal.Zero);
				}
				num = returnVal;
			}
			return num;
		}
	}
}