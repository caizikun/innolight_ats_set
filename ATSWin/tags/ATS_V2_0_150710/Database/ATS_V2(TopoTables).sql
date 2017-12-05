USE [ATS_V2]
GO
/****** Object:  Table [dbo].[UserLoginInfo]    Script Date: 07/01/2015 10:44:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[LogInTime] [datetime] NOT NULL,
	[LogOffTime] [datetime] NOT NULL,
	[Apptype] [nvarchar](50) NOT NULL,
	[LoginInfo] [nvarchar](max) NOT NULL,
	[Remark] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_UserLoginInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_UserLoginInfo] ON [dbo].[UserLoginInfo] 
(
	[UserName] ASC,
	[Apptype] ASC,
	[LogInTime] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'存放登入的IP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserLoginInfo', @level2type=N'COLUMN',@level2name=N'LoginInfo'
GO
/****** Object:  Table [dbo].[TopoRunRecordTable]    Script Date: 07/01/2015 10:44:56 ******/
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
	[Remark] [ntext] NOT NULL,
 CONSTRAINT [PK_TopoTestSN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TopoRunRecordTable] ON [dbo].[TopoRunRecordTable] 
(
	[PID] ASC,
	[SN] ASC,
	[IP] ASC,
	[StartTime] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoLogRecord]    Script Date: 07/01/2015 10:44:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoLogRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RunRecordID] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[TestLog] [ntext] NULL,
	[Temp] [real] NOT NULL,
	[Voltage] [real] NOT NULL,
	[Channel] [tinyint] NOT NULL,
	[Result] [bit] NOT NULL,
	[CtrlType] [tinyint] NOT NULL,
 CONSTRAINT [PK_TopoLogRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TopoLogRecord] ON [dbo].[TopoLogRecord] 
(
	[RunRecordID] ASC,
	[StartTime] ASC,
	[Temp] ASC,
	[Voltage] ASC,
	[CtrlType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoTestCoefBackup]    Script Date: 07/01/2015 10:44:56 ******/
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
CREATE NONCLUSTERED INDEX [IX_TopoTestCoefBackup] ON [dbo].[TopoTestCoefBackup] 
(
	[PID] ASC,
	[Page] ASC,
	[StartAddr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperationLogs]    Script Date: 07/01/2015 10:44:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationLogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ModifyTime] [datetime] NOT NULL,
	[BlockType] [nvarchar](50) NOT NULL,
	[Optype] [nvarchar](50) NOT NULL,
	[DetailLogs] [ntext] NOT NULL,
	[TracingInfo] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_OperationLogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_OperationLogs] ON [dbo].[OperationLogs] 
(
	[ModifyTime] ASC,
	[PID] ASC,
	[BlockType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoProcData]    Script Date: 07/01/2015 10:44:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoProcData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ModelName] [nvarchar](50) NOT NULL,
	[ItemName] [nvarchar](4000) NOT NULL,
	[ItemValue] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_TopoProcData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TopoProcData] ON [dbo].[TopoProcData] 
(
	[ModelName] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoTestData]    Script Date: 07/01/2015 10:44:56 ******/
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'测试数据按PID和ItemName建立索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TopoTestData', @level2type=N'INDEX',@level2name=N'IX_TopoTestData'
GO
/****** Object:  Table [dbo].[TopoMSAEEPROMSet]    Script Date: 07/01/2015 10:44:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoMSAEEPROMSet](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemName] [nvarchar](50) NOT NULL,
	[Data0] [nvarchar](512) NOT NULL,
	[CRCData0] [tinyint] NOT NULL,
	[Data1] [nvarchar](512) NOT NULL,
	[CRCData1] [tinyint] NOT NULL,
	[Data2] [nvarchar](512) NOT NULL,
	[CRCData2] [tinyint] NOT NULL,
	[Data3] [nvarchar](512) NOT NULL,
	[CRCData3] [tinyint] NOT NULL,
 CONSTRAINT [PK_GlobalMSAEEPROMInitialize] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoTestPlan]    Script Date: 07/01/2015 10:44:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoTestPlan](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL,
	[SWVersion] [nvarchar](30) NOT NULL,
	[HWVersion] [nvarchar](30) NOT NULL,
	[USBPort] [tinyint] NOT NULL,
	[IsChipInitialize] [bit] NOT NULL,
	[IsEEPROMInitialize] [bit] NOT NULL,
	[IgnoreBackupCoef] [bit] NOT NULL,
	[SNCheck] [bit] NOT NULL,
	[IgnoreFlag] [bit] NOT NULL,
	[ItemDescription] [nvarchar](200) NOT NULL,
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_TopoTestPlan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_TopoTestPlan_PID_ItemName] UNIQUE NONCLUSTERED 
(
	[ItemName] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoTestControl]    Script Date: 07/01/2015 10:44:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoTestControl](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemName] [nvarchar](50) NOT NULL,
	[SEQ] [int] NOT NULL,
	[Channel] [tinyint] NOT NULL,
	[Temp] [real] NOT NULL,
	[Vcc] [real] NOT NULL,
	[Pattent] [tinyint] NOT NULL,
	[DataRate] [nvarchar](50) NOT NULL,
	[CtrlType] [tinyint] NOT NULL,
	[TempOffset] [real] NOT NULL,
	[TempWaitTimes] [real] NOT NULL,
	[ItemDescription] [nvarchar](200) NOT NULL,
	[IgnoreFlag] [bit] NOT NULL,
 CONSTRAINT [PK_TopoTestControll] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_TopoTestControl] UNIQUE NONCLUSTERED 
(
	[ItemName] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:LP;2:FTM;3:Both' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TopoTestControl', @level2type=N'COLUMN',@level2name=N'CtrlType'
GO
/****** Object:  Table [dbo].[TopoPNSpecsParams]    Script Date: 07/01/2015 10:44:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoPNSpecsParams](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[SID] [int] NOT NULL,
	[Typical] [float] NOT NULL,
	[SpecMin] [float] NOT NULL,
	[SpecMax] [float] NOT NULL,
 CONSTRAINT [PK_GlobalPNSpecficsParams] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_TopoPNSpecsParams] UNIQUE NONCLUSTERED 
(
	[PID] ASC,
	[SID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoManufactureConfigInit]    Script Date: 07/01/2015 10:44:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoManufactureConfigInit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[SlaveAddress] [int] NOT NULL,
	[Page] [tinyint] NOT NULL,
	[StartAddress] [int] NOT NULL,
	[Length] [tinyint] NOT NULL,
	[ItemValue] [int] NOT NULL,
 CONSTRAINT [PK_TopoManufactureConfigInit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_TopoManufactureConfigInit] UNIQUE NONCLUSTERED 
(
	[PID] ASC,
	[SlaveAddress] ASC,
	[Page] ASC,
	[StartAddress] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoEquipment]    Script Date: 07/01/2015 10:44:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoEquipment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[GID] [int] NOT NULL,
	[SEQ] [int] NOT NULL,
	[Role] [tinyint] NOT NULL,
 CONSTRAINT [PK_TopoEquipment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TopoEquipment] ON [dbo].[TopoEquipment] 
(
	[GID] ASC,
	[Role] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:NA;1:TX;2:RX' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TopoEquipment', @level2type=N'COLUMN',@level2name=N'Role'
GO
/****** Object:  Table [dbo].[TopoTestModel]    Script Date: 07/01/2015 10:44:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoTestModel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[GID] [int] NOT NULL,
	[Seq] [int] NOT NULL,
	[IgnoreFlag] [bit] NOT NULL,
	[Failbreak] [bit] NOT NULL,
 CONSTRAINT [PK_TopoTestModel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_TopoTestModel] UNIQUE NONCLUSTERED 
(
	[GID] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoEquipmentParameter]    Script Date: 07/01/2015 10:44:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoEquipmentParameter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[GID] [int] NOT NULL,
	[ItemValue] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_TopoEquipmentParameter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_TopoEquipmentParameter] UNIQUE NONCLUSTERED 
(
	[GID] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoTestParameter]    Script Date: 07/01/2015 10:44:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopoTestParameter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[GID] [int] NOT NULL,
	[ItemValue] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_TopoTestParameter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_TopoTestParameter] UNIQUE NONCLUSTERED 
(
	[GID] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_OperationLogs_PID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[OperationLogs] ADD  CONSTRAINT [DF_OperationLogs_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_OperationLogs_ModifyTime]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[OperationLogs] ADD  CONSTRAINT [DF_OperationLogs_ModifyTime]  DEFAULT (getdate()) FOR [ModifyTime]
GO
/****** Object:  Default [DF_OperationLogs_BlockType]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[OperationLogs] ADD  CONSTRAINT [DF_OperationLogs_BlockType]  DEFAULT ('') FOR [BlockType]
GO
/****** Object:  Default [DF_Table_1_OperationType]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[OperationLogs] ADD  CONSTRAINT [DF_Table_1_OperationType]  DEFAULT ('') FOR [Optype]
GO
/****** Object:  Default [DF_Table_1_TestLog]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[OperationLogs] ADD  CONSTRAINT [DF_Table_1_TestLog]  DEFAULT ('') FOR [DetailLogs]
GO
/****** Object:  Default [DF_OperationLogs_TracePath]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[OperationLogs] ADD  CONSTRAINT [DF_OperationLogs_TracePath]  DEFAULT ('') FOR [TracingInfo]
GO
/****** Object:  Default [DF_TopoEquipment_PID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoEquipment] ADD  CONSTRAINT [DF_TopoEquipment_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_TopoEquipment_GID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoEquipment] ADD  CONSTRAINT [DF_TopoEquipment_GID]  DEFAULT ((0)) FOR [GID]
GO
/****** Object:  Default [DF_TopoEquipment_SEQ]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoEquipment] ADD  CONSTRAINT [DF_TopoEquipment_SEQ]  DEFAULT ((1)) FOR [SEQ]
GO
/****** Object:  Default [DF_TopoEquipment_Roel]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoEquipment] ADD  CONSTRAINT [DF_TopoEquipment_Roel]  DEFAULT ((0)) FOR [Role]
GO
/****** Object:  Default [DF_TopoEquipmentParameter_PID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoEquipmentParameter] ADD  CONSTRAINT [DF_TopoEquipmentParameter_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_TopoEquipmentParameter_GID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoEquipmentParameter] ADD  CONSTRAINT [DF_TopoEquipmentParameter_GID]  DEFAULT ((0)) FOR [GID]
GO
/****** Object:  Default [DF_TopoEquipmentParameter_Value]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoEquipmentParameter] ADD  CONSTRAINT [DF_TopoEquipmentParameter_Value]  DEFAULT ('') FOR [ItemValue]
GO
/****** Object:  Default [DF_TopoLogRecord_TestPlanID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_TestPlanID]  DEFAULT ('0') FOR [RunRecordID]
GO
/****** Object:  Default [DF_TopoLogRecord_StartTime]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_StartTime]  DEFAULT (getdate()) FOR [StartTime]
GO
/****** Object:  Default [DF_TopoLogRecord_EndTime]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_EndTime]  DEFAULT (getdate()) FOR [EndTime]
GO
/****** Object:  Default [DF_TopoLogRecord_TestLog]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_TestLog]  DEFAULT ('') FOR [TestLog]
GO
/****** Object:  Default [DF_TopoLogRecord_Temp]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_Temp]  DEFAULT ((-32768)) FOR [Temp]
GO
/****** Object:  Default [DF_TopoLogRecord_Temp1]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_Temp1]  DEFAULT ((-32768)) FOR [Voltage]
GO
/****** Object:  Default [DF_TopoLogRecord_Channel]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_Channel]  DEFAULT ((0)) FOR [Channel]
GO
/****** Object:  Default [DF_TopoLogRecord_Result]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_Result]  DEFAULT ('false') FOR [Result]
GO
/****** Object:  Default [DF_TopoLogRecord_CtrlType]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoLogRecord] ADD  CONSTRAINT [DF_TopoLogRecord_CtrlType]  DEFAULT ((2)) FOR [CtrlType]
GO
/****** Object:  Default [DF_GlobalManufactureEEPROMInitialize_PID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoManufactureConfigInit] ADD  CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_GlobalManufactureEEPROMInitialize_SlaveAddress]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoManufactureConfigInit] ADD  CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_SlaveAddress]  DEFAULT ('0') FOR [SlaveAddress]
GO
/****** Object:  Default [DF_GlobalManufactureEEPROMInitialize_Page]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoManufactureConfigInit] ADD  CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_Page]  DEFAULT ('0') FOR [Page]
GO
/****** Object:  Default [DF_GlobalManufactureEEPROMInitialize_StartAddress]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoManufactureConfigInit] ADD  CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_StartAddress]  DEFAULT ('0') FOR [StartAddress]
GO
/****** Object:  Default [DF_GlobalManufactureEEPROMInitialize_Length]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoManufactureConfigInit] ADD  CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_Length]  DEFAULT ('0') FOR [Length]
GO
/****** Object:  Default [DF_GlobalManufactureEEPROMInitialize_ItemValue]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoManufactureConfigInit] ADD  CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_ItemValue]  DEFAULT ((0)) FOR [ItemValue]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_PID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoMSAEEPROMSet] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_ItemType1]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoMSAEEPROMSet] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_ItemType1]  DEFAULT ((0)) FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Page]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoMSAEEPROMSet] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Page]  DEFAULT ((0)) FOR [Data0]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Address]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoMSAEEPROMSet] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Address]  DEFAULT ((0)) FOR [CRCData0]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Length]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoMSAEEPROMSet] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Length]  DEFAULT ((1)) FOR [Data1]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_ItemValue]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoMSAEEPROMSet] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_ItemValue]  DEFAULT ((0)) FOR [CRCData1]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Data01]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoMSAEEPROMSet] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data01]  DEFAULT ((0)) FOR [Data2]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Data02]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoMSAEEPROMSet] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data02]  DEFAULT ((0)) FOR [CRCData2]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Data03]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoMSAEEPROMSet] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data03]  DEFAULT ((0)) FOR [Data3]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Data04]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoMSAEEPROMSet] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data04]  DEFAULT ((0)) FOR [CRCData3]
GO
/****** Object:  Default [DF_PNSpecficParams_PID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoPNSpecsParams] ADD  CONSTRAINT [DF_PNSpecficParams_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_GlobalPNSpecficsParams_SID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoPNSpecsParams] ADD  CONSTRAINT [DF_GlobalPNSpecficsParams_SID]  DEFAULT ((0)) FOR [SID]
GO
/****** Object:  Default [DF_Table_1_Unit]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoPNSpecsParams] ADD  CONSTRAINT [DF_Table_1_Unit]  DEFAULT ((0)) FOR [Typical]
GO
/****** Object:  Default [DF_PNSpecficParams_SpecMin]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoPNSpecsParams] ADD  CONSTRAINT [DF_PNSpecficParams_SpecMin]  DEFAULT ((-32768)) FOR [SpecMin]
GO
/****** Object:  Default [DF_Table_1_SpecMin1]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoPNSpecsParams] ADD  CONSTRAINT [DF_Table_1_SpecMin1]  DEFAULT ((32767)) FOR [SpecMax]
GO
/****** Object:  Default [DF_ProcData_PID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoProcData] ADD  CONSTRAINT [DF_ProcData_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_ProcData_ModelName]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoProcData] ADD  CONSTRAINT [DF_ProcData_ModelName]  DEFAULT ('') FOR [ModelName]
GO
/****** Object:  Default [DF_ProcData_ItemName]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoProcData] ADD  CONSTRAINT [DF_ProcData_ItemName]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_ProcData_ItemValue]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoProcData] ADD  CONSTRAINT [DF_ProcData_ItemValue]  DEFAULT ('') FOR [ItemValue]
GO
/****** Object:  Default [DF_TopoTestSN_SN]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoRunRecordTable] ADD  CONSTRAINT [DF_TopoTestSN_SN]  DEFAULT ('') FOR [SN]
GO
/****** Object:  Default [DF_TopoTestPlanRunRecordTable_PID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoRunRecordTable] ADD  CONSTRAINT [DF_TopoTestPlanRunRecordTable_PID]  DEFAULT ('0') FOR [PID]
GO
/****** Object:  Default [DF_TopoTestPlanRunRecordTable_StartTime]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoRunRecordTable] ADD  CONSTRAINT [DF_TopoTestPlanRunRecordTable_StartTime]  DEFAULT (getdate()) FOR [StartTime]
GO
/****** Object:  Default [DF_TopoTestPlanRunRecordTable_EndTime]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoRunRecordTable] ADD  CONSTRAINT [DF_TopoTestPlanRunRecordTable_EndTime]  DEFAULT (getdate()) FOR [EndTime]
GO
/****** Object:  Default [DF_TopoRunRecordTable_FWRev]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoRunRecordTable] ADD  CONSTRAINT [DF_TopoRunRecordTable_FWRev]  DEFAULT ('00') FOR [FWRev]
GO
/****** Object:  Default [DF_TopoRunRecordTable_FWRev1]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoRunRecordTable] ADD  CONSTRAINT [DF_TopoRunRecordTable_FWRev1]  DEFAULT ('') FOR [IP]
GO
/****** Object:  Default [DF_TopoRunRecordTable_LightSource]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoRunRecordTable] ADD  CONSTRAINT [DF_TopoRunRecordTable_LightSource]  DEFAULT ('') FOR [LightSource]
GO
/****** Object:  Default [DF_TopoRunRecordTable_Remark]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoRunRecordTable] ADD  CONSTRAINT [DF_TopoRunRecordTable_Remark]  DEFAULT ('') FOR [Remark]
GO
/****** Object:  Default [DF_TopoTestCoefBackup_PID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestCoefBackup] ADD  CONSTRAINT [DF_TopoTestCoefBackup_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_Table_1_RunRecordID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestCoefBackup] ADD  CONSTRAINT [DF_Table_1_RunRecordID]  DEFAULT ((0)) FOR [StartAddr]
GO
/****** Object:  Default [DF_TopoTestCoefBackup_Page]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestCoefBackup] ADD  CONSTRAINT [DF_TopoTestCoefBackup_Page]  DEFAULT ((0)) FOR [Page]
GO
/****** Object:  Default [DF_Table_1_StartTime]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestCoefBackup] ADD  CONSTRAINT [DF_Table_1_StartTime]  DEFAULT ((0)) FOR [ItemSize]
GO
/****** Object:  Default [DF_Table_1_EndTime]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestCoefBackup] ADD  CONSTRAINT [DF_Table_1_EndTime]  DEFAULT ('') FOR [ItemValue]
GO
/****** Object:  Default [DF_TopoTestControll_PID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_TopoTestControll_Name]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_TopoTestControll_SEQ]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_SEQ]  DEFAULT ('0') FOR [SEQ]
GO
/****** Object:  Default [DF_TopoTestControll_Channel]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_Channel]  DEFAULT ('0') FOR [Channel]
GO
/****** Object:  Default [DF_TopoTestControll_Temp]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_Temp]  DEFAULT ('0') FOR [Temp]
GO
/****** Object:  Default [DF_TopoTestControll_Vcc]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_Vcc]  DEFAULT ('3.3') FOR [Vcc]
GO
/****** Object:  Default [DF_TopoTestControll_Pattent]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_Pattent]  DEFAULT ('7') FOR [Pattent]
GO
/****** Object:  Default [DF_TopoTestControll_DataRate]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_DataRate]  DEFAULT ('10312500000') FOR [DataRate]
GO
/****** Object:  Default [DF_TopoTestControl_CtrlType]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControl_CtrlType]  DEFAULT ((2)) FOR [CtrlType]
GO
/****** Object:  Default [DF_TopoTestControl_TempOffset_1]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControl_TempOffset_1]  DEFAULT ((0)) FOR [TempOffset]
GO
/****** Object:  Default [DF_TopoTestControll_AuxAttribles]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControll_AuxAttribles]  DEFAULT ((0)) FOR [TempWaitTimes]
GO
/****** Object:  Default [DF_TopoTestControl_ItemDescription]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControl_ItemDescription]  DEFAULT ('') FOR [ItemDescription]
GO
/****** Object:  Default [DF_TopoTestControl_IgnoreFlag]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestControl] ADD  CONSTRAINT [DF_TopoTestControl_IgnoreFlag]  DEFAULT ('false') FOR [IgnoreFlag]
GO
/****** Object:  Default [DF_TopoTestData_PID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestData] ADD  CONSTRAINT [DF_TopoTestData_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_TopoTestData_ItemName]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestData] ADD  CONSTRAINT [DF_TopoTestData_ItemName]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_TopoTestData_ItemValue]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestData] ADD  CONSTRAINT [DF_TopoTestData_ItemValue]  DEFAULT ('') FOR [ItemValue]
GO
/****** Object:  Default [DF_TopoTestData_PassOrFail]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestData] ADD  CONSTRAINT [DF_TopoTestData_PassOrFail]  DEFAULT ('false') FOR [Result]
GO
/****** Object:  Default [DF_TopoTestData_SpecMin]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestData] ADD  CONSTRAINT [DF_TopoTestData_SpecMin]  DEFAULT ((-32768)) FOR [SpecMin]
GO
/****** Object:  Default [DF_TopoTestData_SpecMax]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestData] ADD  CONSTRAINT [DF_TopoTestData_SpecMax]  DEFAULT ((32767)) FOR [SpecMax]
GO
/****** Object:  Default [DF_TopoTestModel_GID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestModel] ADD  CONSTRAINT [DF_TopoTestModel_GID]  DEFAULT ((0)) FOR [GID]
GO
/****** Object:  Default [DF_TopoTestModel_Seq]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestModel] ADD  CONSTRAINT [DF_TopoTestModel_Seq]  DEFAULT ('0') FOR [Seq]
GO
/****** Object:  Default [DF_TopoTestModel_IgnoreFlag]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestModel] ADD  CONSTRAINT [DF_TopoTestModel_IgnoreFlag]  DEFAULT ('false') FOR [IgnoreFlag]
GO
/****** Object:  Default [DF_TopoTestModel_Failbreak]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestModel] ADD  CONSTRAINT [DF_TopoTestModel_Failbreak]  DEFAULT ('true') FOR [Failbreak]
GO
/****** Object:  Default [DF_TopoTestParameter_PID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestParameter] ADD  CONSTRAINT [DF_TopoTestParameter_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_TopoTestParameter_GID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestParameter] ADD  CONSTRAINT [DF_TopoTestParameter_GID]  DEFAULT ((0)) FOR [GID]
GO
/****** Object:  Default [DF_TopoTestParameter_Value]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestParameter] ADD  CONSTRAINT [DF_TopoTestParameter_Value]  DEFAULT ('') FOR [ItemValue]
GO
/****** Object:  Default [DF_TopoTestPlan_PID]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_TopoTestPlan_Name]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_TopoTestPlan_SWVersion]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_SWVersion]  DEFAULT ('') FOR [SWVersion]
GO
/****** Object:  Default [DF_TopoTestPlan_HwVersion]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_HwVersion]  DEFAULT ('') FOR [HWVersion]
GO
/****** Object:  Default [DF_TopoTestPlan_USBPort]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_USBPort]  DEFAULT ('0') FOR [USBPort]
GO
/****** Object:  Default [DF_TopoTestPlan_IgnoreFlag1]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_IgnoreFlag1]  DEFAULT ('false') FOR [IsChipInitialize]
GO
/****** Object:  Default [DF_TopoTestPlan_IsChipInitialize1_1]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_IsChipInitialize1_1]  DEFAULT ('false') FOR [IsEEPROMInitialize]
GO
/****** Object:  Default [DF_TopoTestPlan_IsChipInitialize1]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_IsChipInitialize1]  DEFAULT ('false') FOR [IgnoreBackupCoef]
GO
/****** Object:  Default [DF_TopoTestPlan_AuxAttribles]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_AuxAttribles]  DEFAULT ('False') FOR [SNCheck]
GO
/****** Object:  Default [DF_TopoTestPlan_IgnoreFlag]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_IgnoreFlag]  DEFAULT ('false') FOR [IgnoreFlag]
GO
/****** Object:  Default [DF_TopoTestPlan_ItemDescription]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_ItemDescription]  DEFAULT ('') FOR [ItemDescription]
GO
/****** Object:  Default [DF_TopoTestPlan_Version]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestPlan] ADD  CONSTRAINT [DF_TopoTestPlan_Version]  DEFAULT ((0)) FOR [Version]
GO
/****** Object:  Default [DF_UserLoginInfo_LoginOntime]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[UserLoginInfo] ADD  CONSTRAINT [DF_UserLoginInfo_LoginOntime]  DEFAULT (getdate()) FOR [LogInTime]
GO
/****** Object:  Default [DF_UserLoginInfo_LoginOffTime]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[UserLoginInfo] ADD  CONSTRAINT [DF_UserLoginInfo_LoginOffTime]  DEFAULT (getdate()) FOR [LogOffTime]
GO
/****** Object:  Default [DF_UserLoginInfo_Apptype]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[UserLoginInfo] ADD  CONSTRAINT [DF_UserLoginInfo_Apptype]  DEFAULT ((0)) FOR [Apptype]
GO
/****** Object:  Default [DF_UserLoginInfo_LoginInfo]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[UserLoginInfo] ADD  CONSTRAINT [DF_UserLoginInfo_LoginInfo]  DEFAULT ('') FOR [LoginInfo]
GO
/****** Object:  Default [DF_UserLoginInfo_Remark]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[UserLoginInfo] ADD  CONSTRAINT [DF_UserLoginInfo_Remark]  DEFAULT ('') FOR [Remark]
GO
/****** Object:  ForeignKey [FK_OperationLogs_UserLoginInfo]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[OperationLogs]  WITH CHECK ADD  CONSTRAINT [FK_OperationLogs_UserLoginInfo] FOREIGN KEY([PID])
REFERENCES [dbo].[UserLoginInfo] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OperationLogs] CHECK CONSTRAINT [FK_OperationLogs_UserLoginInfo]
GO
/****** Object:  ForeignKey [FK_TopoEquipment_GlobalAllEquipmentList]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoEquipment]  WITH CHECK ADD  CONSTRAINT [FK_TopoEquipment_GlobalAllEquipmentList] FOREIGN KEY([GID])
REFERENCES [dbo].[GlobalAllEquipmentList] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoEquipment] CHECK CONSTRAINT [FK_TopoEquipment_GlobalAllEquipmentList]
GO
/****** Object:  ForeignKey [FK_TopoEquipment_TopoTestPlan]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoEquipment]  WITH CHECK ADD  CONSTRAINT [FK_TopoEquipment_TopoTestPlan] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoTestPlan] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoEquipment] CHECK CONSTRAINT [FK_TopoEquipment_TopoTestPlan]
GO
/****** Object:  ForeignKey [FK_TopoEquipmentParameter_TopoEquipment]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoEquipmentParameter]  WITH CHECK ADD  CONSTRAINT [FK_TopoEquipmentParameter_TopoEquipment] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoEquipment] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoEquipmentParameter] CHECK CONSTRAINT [FK_TopoEquipmentParameter_TopoEquipment]
GO
/****** Object:  ForeignKey [FK_TopoLogRecord_TopoRunRecordTable]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoLogRecord]  WITH CHECK ADD  CONSTRAINT [FK_TopoLogRecord_TopoRunRecordTable] FOREIGN KEY([RunRecordID])
REFERENCES [dbo].[TopoRunRecordTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoLogRecord] CHECK CONSTRAINT [FK_TopoLogRecord_TopoRunRecordTable]
GO
/****** Object:  ForeignKey [FK_TopoManufactureConfigInit_TopoTestPlan]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoManufactureConfigInit]  WITH CHECK ADD  CONSTRAINT [FK_TopoManufactureConfigInit_TopoTestPlan] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoTestPlan] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoManufactureConfigInit] CHECK CONSTRAINT [FK_TopoManufactureConfigInit_TopoTestPlan]
GO
/****** Object:  ForeignKey [FK_TopoMSAEEPROMSet_GlobalProductionName]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoMSAEEPROMSet]  WITH CHECK ADD  CONSTRAINT [FK_TopoMSAEEPROMSet_GlobalProductionName] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionName] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoMSAEEPROMSet] CHECK CONSTRAINT [FK_TopoMSAEEPROMSet_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_GlobalPNSpecficsParams_GlobalSpecifics]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoPNSpecsParams]  WITH CHECK ADD  CONSTRAINT [FK_GlobalPNSpecficsParams_GlobalSpecifics] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoTestPlan] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoPNSpecsParams] CHECK CONSTRAINT [FK_GlobalPNSpecficsParams_GlobalSpecifics]
GO
/****** Object:  ForeignKey [FK_GlobalPNSpecsParams_GlobalSpecs]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoPNSpecsParams]  WITH CHECK ADD  CONSTRAINT [FK_GlobalPNSpecsParams_GlobalSpecs] FOREIGN KEY([SID])
REFERENCES [dbo].[GlobalSpecs] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoPNSpecsParams] CHECK CONSTRAINT [FK_GlobalPNSpecsParams_GlobalSpecs]
GO
/****** Object:  ForeignKey [FK_TopoProcData_TopoLogRecord]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoProcData]  WITH CHECK ADD  CONSTRAINT [FK_TopoProcData_TopoLogRecord] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoLogRecord] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoProcData] CHECK CONSTRAINT [FK_TopoProcData_TopoLogRecord]
GO
/****** Object:  ForeignKey [FK_TopoTestCoefBackup_TopoRunRecordTable]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestCoefBackup]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestCoefBackup_TopoRunRecordTable] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoRunRecordTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoTestCoefBackup] CHECK CONSTRAINT [FK_TopoTestCoefBackup_TopoRunRecordTable]
GO
/****** Object:  ForeignKey [FK_TopoTestControll_TopoTestPlan]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestControl]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestControll_TopoTestPlan] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoTestPlan] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoTestControl] CHECK CONSTRAINT [FK_TopoTestControll_TopoTestPlan]
GO
/****** Object:  ForeignKey [FK_TopoTestData_TopoLogRecord]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestData]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestData_TopoLogRecord] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoLogRecord] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoTestData] CHECK CONSTRAINT [FK_TopoTestData_TopoLogRecord]
GO
/****** Object:  ForeignKey [FK_TopoTestModel_GlobalAllTestModelList]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestModel]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestModel_GlobalAllTestModelList] FOREIGN KEY([GID])
REFERENCES [dbo].[GlobalAllTestModelList] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoTestModel] CHECK CONSTRAINT [FK_TopoTestModel_GlobalAllTestModelList]
GO
/****** Object:  ForeignKey [FK_TopoTestModel_TopoTestControll]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestModel]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestModel_TopoTestControll] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoTestControl] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoTestModel] CHECK CONSTRAINT [FK_TopoTestModel_TopoTestControll]
GO
/****** Object:  ForeignKey [FK_TopoTestParameter_TopoTestModel]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestParameter]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestParameter_TopoTestModel] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoTestModel] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoTestParameter] CHECK CONSTRAINT [FK_TopoTestParameter_TopoTestModel]
GO
/****** Object:  ForeignKey [FK_TopoTestPlan_GlobalProductionName]    Script Date: 07/01/2015 10:44:56 ******/
ALTER TABLE [dbo].[TopoTestPlan]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestPlan_GlobalProductionName] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionName] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TopoTestPlan] CHECK CONSTRAINT [FK_TopoTestPlan_GlobalProductionName]
GO
