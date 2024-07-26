ALTER TABLE [ERPDB].[dbo].[Client] ADD [FaxNumber] nvarchar(max) NULL

ALTER TABLE [ERPDB].[dbo].[Employee] ADD [HousingAllowance] [decimal](18, 2) NULL
ALTER TABLE [ERPDB].[dbo].[Employee] ADD [TransportAllowance] [decimal](18, 2) NULL
ALTER TABLE [ERPDB].[dbo].[Employee] ADD [OtherAllowance] [decimal](18, 2) NULL
ALTER TABLE [ERPDB].[dbo].[Employee] ADD [HousingDeduction] [decimal](18, 2) NULL
ALTER TABLE [ERPDB].[dbo].[Employee] ADD [TransportDeduction] [decimal](18, 2) NULL
ALTER TABLE [ERPDB].[dbo].[Employee] ADD [OtherDeduction] [decimal](18, 2) NULL
ALTER TABLE [ERPDB].[dbo].[Employee] ADD [FixedDeduction] [decimal](18, 2) NULL
ALTER TABLE [ERPDB].[dbo].[Employee] ADD [OADescription] [nvarchar](max) NULL
ALTER TABLE [ERPDB].[dbo].[Employee] ADD [ODDescription] [nvarchar](max) NULL
ALTER TABLE [ERPDB].[dbo].[Employee] ADD [IsHavingDeduction] [bit] NOT NULL DEFAULT ((0))
ALTER TABLE [ERPDB].[dbo].[Employee] ADD [TierType] [nvarchar](max) NULL

ALTER TABLE [ERPDB].[dbo].[SalaryDetail] ADD [IsMYE] [bit] NOT NULL DEFAULT ((0))
ALTER TABLE [ERPDB].[dbo].[SalaryDetail] ADD [Skill] [nvarchar](max) NULL
ALTER TABLE [ERPDB].[dbo].[SalaryDetail] ADD [FixedDeduction] [decimal](18, 2) NULL
ALTER TABLE [ERPDB].[dbo].[SalaryDetail] ADD [TotalCPF] [decimal](18, 2) NULL

ALTER TABLE [ERPDB].[dbo].[CPF] ADD [TotalCPF] [decimal](18, 2) NULL
ALTER TABLE [ERPDB].[dbo].[CPF] ADD [OrdinaryWages] [decimal](18, 2) NULL
ALTER TABLE [ERPDB].[dbo].[CPF] ADD [FixedDecution] [decimal](18, 2) NULL

ALTER TABLE [ERPDB].[dbo].[Invoice] ADD [Email] [nvarchar](max) NULL
ALTER TABLE [ERPDB].[dbo].[Invoice] ADD [ProjectId] [int] NULL

ALTER TABLE [ERPDB].[dbo].[Quotation] ADD [Email] [nvarchar](max) NULL

ALTER TABLE [ERPDB].[dbo].[SalaryDetail] ADD [SDL] [decimal](18, 2) NULL
