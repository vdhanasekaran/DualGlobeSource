ALTER TABLE [ERPDB].[dbo].[SalaryDetail] ADD [IncentiveAllowance] [decimal](18, 2) NULL
ALTER TABLE [ERPDB].[dbo].[SalaryDetail] ADD [DeductedIncentiveAllowance] [decimal](18, 2) NULL
ALTER TABLE [ERPDB].[dbo].[Allowance] ADD [IncentiveAllowance] [decimal](18, 2) NULL
ALTER TABLE [ERPDB].[dbo].[Allowance] ADD [ApprovedByEmployeeId] [int] NULL
ALTER TABLE [ERPDB].[dbo].[Allowance] ADD [AllowanceType] [nvarchar](max) NULL