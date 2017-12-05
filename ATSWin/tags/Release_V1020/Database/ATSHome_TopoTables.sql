USE [ATSHome]
GO
/****** Object:  Table [dbo].[UserLoginInfo]    Script Date: 01/26/2015 17:11:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[LoginOntime] [datetime] NOT NULL,
	[LoginOffTime] [datetime] NOT NULL,
	[Apptype] [nvarchar](50) NOT NULL,
	[LoginInfo] [ntext] NOT NULL,
	[OPLogs] [ntext] NULL,
 CONSTRAINT [PK_UserLoginInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserLoginInfo] ON [dbo].[UserLoginInfo] 
(
	[UserName] ASC,
	[Apptype] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoRunRecordTable]    Script Date: 01/26/2015 17:11:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoRunRecordTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SN] [nvarchar](30) NOT NULL,
	[PID] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[FWRev] [nvarchar](5) NOT NULL,
	[IP] [nvarchar](50) NOT NULL,
	[LightSource] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TopoTestSN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TopoRunRecordTable] ON [dbo].[TopoRunRecordTable] 
(
	[PID] ASC,
	[SN] ASC,
	[IP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoTestCoefBackup]    Script Date: 01/26/2015 17:11:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoTestCoefBackup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[StartAddr] [int] NOT NULL,
	[Page] [tinyint] NOT NULL,
	[ItemSize] [tinyint] NOT NULL,
	[ItemValue] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_TopoTestCoefBackup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoLogRecord]    Script Date: 01/26/2015 17:11:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoLogRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[RunRecordID] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[TestLog] [ntext] NULL,
	[Temp] [real] NOT NULL,
	[Voltage] [real] NOT NULL,
	[Channel] [tinyint] NOT NULL,
	[Result] [bit] NOT NULL,
 CONSTRAINT [PK_TopoLogRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TopoLogRecord] ON [dbo].[TopoLogRecord] 
(
	[PID] ASC,
	[RunRecordID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperationLogs]    Script Date: 01/26/2015 17:11:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationLogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ModifyTime] [datetime] NOT NULL,
	[Optype] [ntext] NOT NULL,
	[DetailLogs] [ntext] NULL,
 CONSTRAINT [PK_OperationLogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_OperationLogs] ON [dbo].[OperationLogs] 
(
	[ModifyTime] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoTestData]    Script Date: 01/26/2015 17:11:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoTestData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL,
	[ItemValue] [float] NOT NULL,
	[Result] [bit] NOT NULL,
	[SpecMin] [float] NOT NULL,
	[SpecMax] [float] NOT NULL,
 CONSTRAINT [PK_TopoTestData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TopoTestData] ON [dbo].[TopoTestData] 
(
	[PID] ASC,
	[ItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoTestPlan]    Script Date: 01/26/2015 17:11:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoTestPlan](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL,
	[SWVersion] [nvarchar](30) NOT NULL,
	[HwVersion] [nvarchar](30) NOT NULL,
	[USBPort] [tinyint] NOT NULL,
	[IsChipInitialize] [bit] NOT NULL,
	[IgnoreBackupCoef] [bit] NOT NULL,
	[AuxAttribles] [nvarchar](255) NOT NULL,
	[IgnoreFlag] [bit] NOT NULL,
 CONSTRAINT [PK_TopoTestPlan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TopoTestPlan] ON [dbo].[TopoTestPlan] 
(
	[PID] ASC,
	[ItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoTestControl]    Script Date: 01/26/2015 17:11:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoTestControl](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL,
	[SEQ] [int] NOT NULL,
	[Channel] [tinyint] NOT NULL,
	[Temp] [real] NOT NULL,
	[Vcc] [real] NOT NULL,
	[Pattent] [tinyint] NOT NULL,
	[DataRate] [nvarchar](50) NOT NULL,
	[AuxAttribles] [nvarchar](1024) NOT NULL,
	[IgnoreFlag] [bit] NOT NULL,
 CONSTRAINT [PK_TopoTestControll] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TopoTestControl] ON [dbo].[TopoTestControl] 
(
	[ItemName] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoEquipment]    Script Date: 01/26/2015 17:11:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoEquipment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[SEQ] [int] NOT NULL,
	[ItemType] [nvarchar](30) NOT NULL,
	[ItemName] [nvarchar](100) NOT NULL,
	[Role] [smallint] NOT NULL,
 CONSTRAINT [PK_TopoEquipment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TopoEquipment] ON [dbo].[TopoEquipment] 
(
	[ItemType] ASC,
	[ItemName] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoTestModel]    Script Date: 01/26/2015 17:11:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoTestModel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemName] [nvarchar](50) NOT NULL,
	[Seq] [int] NOT NULL,
	[AppModeID] [int] NOT NULL,
	[EquipmentList] [nvarchar](500) NOT NULL,
	[IgnoreFlag] [bit] NOT NULL,
 CONSTRAINT [PK_TopoTestModel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TopoTestModel] ON [dbo].[TopoTestModel] 
(
	[AppModeID] ASC,
	[ItemName] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoEquipmentParameter]    Script Date: 01/26/2015 17:11:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoEquipmentParameter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[Item] [nvarchar](30) NOT NULL,
	[ItemValue] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_TopoEquipmentParameter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TopoEquipmentParameter] ON [dbo].[TopoEquipmentParameter] 
(
	[Item] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoTestParameter]    Script Date: 01/26/2015 17:11:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoTestParameter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL,
	[ItemType] [nvarchar](10) NOT NULL,
	[Direction] [nvarchar](10) NOT NULL,
	[ItemValue] [nvarchar](50) NOT NULL,
	[SpecMin] [float] NOT NULL,
	[SpecMax] [float] NOT NULL,
	[ItemSpecific] [tinyint] NOT NULL,
	[LogRecord] [tinyint] NOT NULL,
	[Failbreak] [tinyint] NOT NULL,
	[DataRecord] [tinyint] NOT NULL,
 CONSTRAINT [PK_TopoTestParameter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TopoTestParameter] ON [dbo].[TopoTestParameter] 
(
	[ItemName] ASC,
	[ItemType] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Default [DF_UserLoginInfo_LoginOntime]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[UserLoginInfo] ADD  CONSTRAINT [DF_UserLoginInfo_LoginOntime]  DEFAULT (getdate()) FOR [LoginOntime]
GO
/****** Object:  Default [DF_UserLoginInfo_LoginOffTime]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[UserLoginInfo] ADD  CONSTRAINT [DF_UserLoginInfo_LoginOffTime]  DEFAULT (getdate()) FOR [LoginOffTime]
GO
/****** Object:  Default [DF_UserLoginInfo_Apptype]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[UserLoginInfo] ADD  CONSTRAINT [DF_UserLoginInfo_Apptype]  DEFAULT ((0)) FOR [Apptype]
GO
/****** Object:  Default [DF_TopoTestPlan_PID]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_TopoTestPlan_Name]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_TopoTestPlan_SWVersion]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_SWVersion]  DEFAULT ('') FOR [SWVersion]
GO
/****** Object:  Default [DF_TopoTestPlan_HwVersion]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_HwVersion]  DEFAULT ('') FOR [HwVersion]
GO
/****** Object:  Default [DF_TopoTestPlan_USBPort]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_USBPort]  DEFAULT ('0') FOR [USBPort]
GO
/****** Object:  Default [DF_TopoTestPlan_IsChipInitialize]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_IsChipInitialize]  DEFAULT ('false') FOR [IsChipInitialize]
GO
/****** Object:  Default [DF_TopoTestPlan_IgnoreBackupCoef]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_IgnoreBackupCoef]  DEFAULT ('false') FOR [IgnoreBackupCoef]
GO
/****** Object:  Default [DF_TopoTestPlan_AuxAttribles]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_AuxAttribles]  DEFAULT ('') FOR [AuxAttribles]
GO
/****** Object:  Default [DF_TopoTestPlan_IgnoreFlag]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_IgnoreFlag]  DEFAULT ('false') FOR [IgnoreFlag]
GO
/****** Object:  Default [DF_TopoTestParameter_PID]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestParameter] ADD  CONSTRAINT [DF_TopoTestParameter_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_TopoTestParameter_Name]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestParameter] ADD  CONSTRAINT [DF_TopoTestParameter_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_TopoTestParameter_Type]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestParameter] ADD  CONSTRAINT [DF_TopoTestParameter_Type]  DEFAULT ('') FOR [ItemType]
GO
/****** Object:  Default [DF_TopoTestParameter_Direction]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestParameter] ADD  CONSTRAINT [DF_TopoTestParameter_Direction]  DEFAULT ('') FOR [Direction]
GO
/****** Object:  Default [DF_TopoTestParameter_Value]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestParameter] ADD  CONSTRAINT [DF_TopoTestParameter_Value]  DEFAULT ('') FOR [ItemValue]
GO
/****** Object:  Default [DF_TopoTestParameter_DefaultLowLimit]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestParameter] ADD  CONSTRAINT [DF_TopoTestParameter_DefaultLowLimit]  DEFAULT ((-32768)) FOR [SpecMin]
GO
/****** Object:  Default [DF_TopoTestParameter_DefaultUpperLimit]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestParameter] ADD  CONSTRAINT [DF_TopoTestParameter_DefaultUpperLimit]  DEFAULT ((32767)) FOR [SpecMax]
GO
/****** Object:  Default [DF_TopoTestParameter_Specific]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestParameter] ADD  CONSTRAINT [DF_TopoTestParameter_Specific]  DEFAULT ((0)) FOR [ItemSpecific]
GO
/****** Object:  Default [DF_TopoTestParameter_LogRecord]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestParameter] ADD  CONSTRAINT [DF_TopoTestParameter_LogRecord]  DEFAULT ((0)) FOR [LogRecord]
GO
/****** Object:  Default [DF_TopoTestParameter_Failbreak]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestParameter] ADD  CONSTRAINT [DF_TopoTestParameter_Failbreak]  DEFAULT ((0)) FOR [Failbreak]
GO
/****** Object:  Default [DF_TopoTestParameter_DataRecord]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestParameter] ADD  CONSTRAINT [DF_TopoTestParameter_DataRecord]  DEFAULT ((0)) FOR [DataRecord]
GO
/****** Object:  Default [DF_TopoTestModel_TestModelName]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestModel] ADD  CONSTRAINT [DF_TopoTestModel_TestModelName]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_TopoTestModel_Seq]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestModel] ADD  CONSTRAINT [DF_TopoTestModel_Seq]  DEFAULT ('0') FOR [Seq]
GO
/****** Object:  Default [DF_TopoTestModel_AppModeID]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestModel] ADD  CONSTRAINT [DF_TopoTestModel_AppModeID]  DEFAULT ('0') FOR [AppModeID]
GO
/****** Object:  Default [DF_TopoTestModel_EquipmentList]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestModel] ADD  CONSTRAINT [DF_TopoTestModel_EquipmentList]  DEFAULT ('') FOR [EquipmentList]
GO
/****** Object:  Default [DF_TopoTestModel_IgnoreFlag]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestModel] ADD  CONSTRAINT [DF_TopoTestModel_IgnoreFlag]  DEFAULT ('false') FOR [IgnoreFlag]
GO
/****** Object:  Default [DF_TopoTestData_PID]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestData] ADD  CONSTRAINT [DF_TopoTestData_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_TopoTestData_ItemName]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestData] ADD  CONSTRAINT [DF_TopoTestData_ItemName]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_TopoTestData_ItemValue]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestData] ADD  CONSTRAINT [DF_TopoTestData_ItemValue]  DEFAULT ('') FOR [ItemValue]
GO
/****** Object:  Default [DF_TopoTestData_PassOrFail]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestData] ADD  CONSTRAINT [DF_TopoTestData_PassOrFail]  DEFAULT ('false') FOR [Result]
GO
/****** Object:  Default [DF_TopoTestData_SpecMin]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestData] ADD  CONSTRAINT [DF_TopoTestData_SpecMin]  DEFAULT ((-32768)) FOR [SpecMin]
GO
/****** Object:  Default [DF_TopoTestData_SpecMax]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestData] ADD  CONSTRAINT [DF_TopoTestData_SpecMax]  DEFAULT ((32767)) FOR [SpecMax]
GO
/****** Object:  Default [DF_TopoTestControll_PID]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_TopoTestControll_Name]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_TopoTestControll_SEQ]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_SEQ]  DEFAULT ('0') FOR [SEQ]
GO
/****** Object:  Default [DF_TopoTestControll_Channel]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_Channel]  DEFAULT ('0') FOR [Channel]
GO
/****** Object:  Default [DF_TopoTestControll_Temp]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_Temp]  DEFAULT ('0') FOR [Temp]
GO
/****** Object:  Default [DF_TopoTestControll_Vcc]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_Vcc]  DEFAULT ('3.3') FOR [Vcc]
GO
/****** Object:  Default [DF_TopoTestControll_Pattent]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_Pattent]  DEFAULT ('7') FOR [Pattent]
GO
/****** Object:  Default [DF_TopoTestControll_DataRate]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_DataRate]  DEFAULT ('10312500000') FOR [DataRate]
GO
/****** Object:  Default [DF_TopoTestControll_AuxAttribles]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_AuxAttribles]  DEFAULT ('') FOR [AuxAttribles]
GO
/****** Object:  Default [DF_TopoTestControl_IgnoreFlag]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControl_IgnoreFlag]  DEFAULT ('false') FOR [IgnoreFlag]
GO
/****** Object:  Default [DF_TopoTestCoefBackup_PID]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestCoefBackup] ADD  CONSTRAINT [DF_TopoTestCoefBackup_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_Table_1_RunRecordID]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestCoefBackup] ADD  CONSTRAINT [DF_Table_1_RunRecordID]  DEFAULT ((0)) FOR [StartAddr]
GO
/****** Object:  Default [DF_TopoTestCoefBackup_Page]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestCoefBackup] ADD  CONSTRAINT [DF_TopoTestCoefBackup_Page]  DEFAULT ((0)) FOR [Page]
GO
/****** Object:  Default [DF_Table_1_StartTime]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestCoefBackup] ADD  CONSTRAINT [DF_Table_1_StartTime]  DEFAULT ((0)) FOR [ItemSize]
GO
/****** Object:  Default [DF_Table_1_EndTime]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestCoefBackup] ADD  CONSTRAINT [DF_Table_1_EndTime]  DEFAULT ('') FOR [ItemValue]
GO
/****** Object:  Default [DF_TopoTestSN_SN]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoRunRecordTable] ADD  CONSTRAINT [DF_TopoTestSN_SN]  DEFAULT ('') FOR [SN]
GO
/****** Object:  Default [DF_TopoTestPlanRunRecordTable_PID]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoRunRecordTable] ADD  CONSTRAINT [DF_TopoTestPlanRunRecordTable_PID]  DEFAULT ('0') FOR [PID]
GO
/****** Object:  Default [DF_TopoTestPlanRunRecordTable_StartTime]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoRunRecordTable] ADD  CONSTRAINT [DF_TopoTestPlanRunRecordTable_StartTime]  DEFAULT (getdate()) FOR [StartTime]
GO
/****** Object:  Default [DF_TopoTestPlanRunRecordTable_EndTime]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoRunRecordTable] ADD  CONSTRAINT [DF_TopoTestPlanRunRecordTable_EndTime]  DEFAULT (getdate()) FOR [EndTime]
GO
/****** Object:  Default [DF_TopoRunRecordTable_FWRev]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoRunRecordTable] ADD  CONSTRAINT [DF_TopoRunRecordTable_FWRev]  DEFAULT ('00') FOR [FWRev]
GO
/****** Object:  Default [DF_TopoRunRecordTable_IP]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoRunRecordTable] ADD  CONSTRAINT [DF_TopoRunRecordTable_IP]  DEFAULT ('') FOR [IP]
GO
/****** Object:  Default [DF_TopoRunRecordTable_LightSource_1]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoRunRecordTable] ADD  CONSTRAINT [DF_TopoRunRecordTable_LightSource_1]  DEFAULT ('') FOR [LightSource]
GO
/****** Object:  Default [DF_TopoLogRecord_PID]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_TopoLogRecord_TestPlanID]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_TestPlanID]  DEFAULT ('0') FOR [RunRecordID]
GO
/****** Object:  Default [DF_TopoLogRecord_StartTime]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_StartTime]  DEFAULT (getdate()) FOR [StartTime]
GO
/****** Object:  Default [DF_TopoLogRecord_EndTime]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_EndTime]  DEFAULT (getdate()) FOR [EndTime]
GO
/****** Object:  Default [DF_TopoLogRecord_TestLog]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_TestLog]  DEFAULT ('') FOR [TestLog]
GO
/****** Object:  Default [DF_TopoLogRecord_Temp]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_Temp]  DEFAULT ((-32768)) FOR [Temp]
GO
/****** Object:  Default [DF_TopoLogRecord_Temp1]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_Temp1]  DEFAULT ((-32768)) FOR [Voltage]
GO
/****** Object:  Default [DF_TopoLogRecord_Channel]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_Channel]  DEFAULT ((0)) FOR [Channel]
GO
/****** Object:  Default [DF_TopoLogRecord_Result]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_Result]  DEFAULT ('false') FOR [Result]
GO
/****** Object:  Default [DF_OperationLogs_PID]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[OperationLogs] ADD  CONSTRAINT [DF_OperationLogs_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_OperationLogs_ModifyTime]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[OperationLogs] ADD  CONSTRAINT [DF_OperationLogs_ModifyTime]  DEFAULT (getdate()) FOR [ModifyTime]
GO
/****** Object:  Default [DF_Table_1_OperationType]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[OperationLogs] ADD  CONSTRAINT [DF_Table_1_OperationType]  DEFAULT ('') FOR [Optype]
GO
/****** Object:  Default [DF_Table_1_TestLog]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[OperationLogs] ADD  CONSTRAINT [DF_Table_1_TestLog]  DEFAULT ('') FOR [DetailLogs]
GO
/****** Object:  Default [DF_TopoEquipment_PID]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoEquipment] ADD  CONSTRAINT [DF_TopoEquipment_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_TopoEquipment_SEQ]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoEquipment] ADD  CONSTRAINT [DF_TopoEquipment_SEQ]  DEFAULT ((1)) FOR [SEQ]
GO
/****** Object:  Default [DF_TopoEquipment_Type]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoEquipment] ADD  CONSTRAINT [DF_TopoEquipment_Type]  DEFAULT ('') FOR [ItemType]
GO
/****** Object:  Default [DF_TopoEquipment_Name]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoEquipment] ADD  CONSTRAINT [DF_TopoEquipment_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_TopoEquipment_Roel]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoEquipment] ADD  CONSTRAINT [DF_TopoEquipment_Roel]  DEFAULT ((0)) FOR [Role]
GO
/****** Object:  Default [DF_TopoEquipmentParameter_PID]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoEquipmentParameter] ADD  CONSTRAINT [DF_TopoEquipmentParameter_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_TopoEquipmentParameter_Item]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoEquipmentParameter] ADD  CONSTRAINT [DF_TopoEquipmentParameter_Item]  DEFAULT ('') FOR [Item]
GO
/****** Object:  Default [DF_TopoEquipmentParameter_Value]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoEquipmentParameter] ADD  CONSTRAINT [DF_TopoEquipmentParameter_Value]  DEFAULT ('') FOR [ItemValue]
GO
/****** Object:  ForeignKey [FK_TopoTestPlan_GlobalProductionName]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestPlan]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestPlan_GlobalProductionName] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionName] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoTestPlan] CHECK CONSTRAINT [FK_TopoTestPlan_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_TopoTestParameter_TopoTestModel]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestParameter]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestParameter_TopoTestModel] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoTestModel] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoTestParameter] CHECK CONSTRAINT [FK_TopoTestParameter_TopoTestModel]
GO
/****** Object:  ForeignKey [FK_TopoTestModel_TopoTestControll]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestModel]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestModel_TopoTestControll] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoTestControl] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoTestModel] CHECK CONSTRAINT [FK_TopoTestModel_TopoTestControll]
GO
/****** Object:  ForeignKey [FK_TopoTestData_TopoLogRecord]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestData]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestData_TopoLogRecord] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoLogRecord] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoTestData] CHECK CONSTRAINT [FK_TopoTestData_TopoLogRecord]
GO
/****** Object:  ForeignKey [FK_TopoTestControll_TopoTestPlan]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestControl]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestControll_TopoTestPlan] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoTestPlan] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoTestControl] CHECK CONSTRAINT [FK_TopoTestControll_TopoTestPlan]
GO
/****** Object:  ForeignKey [FK_TopoTestCoefBackup_TopoRunRecordTable]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoTestCoefBackup]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestCoefBackup_TopoRunRecordTable] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoRunRecordTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoTestCoefBackup] CHECK CONSTRAINT [FK_TopoTestCoefBackup_TopoRunRecordTable]
GO
/****** Object:  ForeignKey [FK_TopoLogRecord_TopoTestPlanRunRecordTable]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoLogRecord]  WITH CHECK ADD  CONSTRAINT [FK_TopoLogRecord_TopoTestPlanRunRecordTable] FOREIGN KEY([RunRecordID])
REFERENCES [dbo].[TopoRunRecordTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoLogRecord] CHECK CONSTRAINT [FK_TopoLogRecord_TopoTestPlanRunRecordTable]
GO
/****** Object:  ForeignKey [FK_OperationLogs_UserLoginInfo]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[OperationLogs]  WITH CHECK ADD  CONSTRAINT [FK_OperationLogs_UserLoginInfo] FOREIGN KEY([PID])
REFERENCES [dbo].[UserLoginInfo] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OperationLogs] CHECK CONSTRAINT [FK_OperationLogs_UserLoginInfo]
GO
/****** Object:  ForeignKey [FK_TopoEquipment_TopoTestPlan]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoEquipment]  WITH CHECK ADD  CONSTRAINT [FK_TopoEquipment_TopoTestPlan] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoTestPlan] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoEquipment] CHECK CONSTRAINT [FK_TopoEquipment_TopoTestPlan]
GO
/****** Object:  ForeignKey [FK_TopoEquipmentParameter_TopoEquipment]    Script Date: 01/26/2015 17:11:54 ******/
ALTER TABLE [dbo].[TopoEquipmentParameter]  WITH CHECK ADD  CONSTRAINT [FK_TopoEquipmentParameter_TopoEquipment] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoEquipment] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoEquipmentParameter] CHECK CONSTRAINT [FK_TopoEquipmentParameter_TopoEquipment]
GO
