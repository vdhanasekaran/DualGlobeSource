
ALTER TABLE [ERPDB].[dbo].[SalaryDetail] ADD [DeductedBasicSalary] [decimal](18, 2) NULL DEFAULT(0)
ALTER TABLE [ERPDB].[dbo].[SalaryDetail] ADD [DeductedFixedAllowance] [decimal](18, 2) NULL DEFAULT(0)
ALTER TABLE [ERPDB].[dbo].[SalaryDetail] ADD [DeductedBonusAllowance] [decimal](18, 2) NULL DEFAULT(0)
