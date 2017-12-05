USE [ATSHome]
GO
/****** Object:  User [ATSUser]    Script Date: 12/22/2014 17:37:58 ******/
CREATE USER [ATSUser] FOR LOGIN [ATSUser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [BackGround]    Script Date: 12/22/2014 17:37:58 ******/
CREATE USER [BackGround] FOR LOGIN [ATSBackGround] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [maintainUser]    Script Date: 12/22/2014 17:37:58 ******/
CREATE USER [maintainUser] FOR LOGIN [ATSMaintainUser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[RolesTable]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolesTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](25) NOT NULL,
	[Remarks] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_RolesTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RolesTable] ON
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (1, N'TestEngineer', N'测试工程师')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (2, N'OP', N'普通操作者')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (3, N'Admin', N'管理员')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (4, N'Guest', N'访客用户')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (5, N'DBOwner', N'数据库拥有者')
SET IDENTITY_INSERT [dbo].[RolesTable] OFF
/****** Object:  Table [dbo].[UserInfo]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LoginName] [nvarchar](50) NOT NULL,
	[LoginPassword] [nvarchar](50) NOT NULL,
	[TrueName] [nvarchar](20) NOT NULL,
	[lastLoginONTime] [datetime] NOT NULL,
	[lastComputerName] [nvarchar](50) NOT NULL,
	[lastLoginOffTime] [datetime] NOT NULL,
	[lastIP] [nvarchar](20) NOT NULL,
	[Remarks] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserInfo] ON
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginONTime], [lastComputerName], [lastLoginOffTime], [lastIP], [Remarks]) VALUES (2, N'terry.yin', N'terry', N'ysz', CAST(0x0000A3FE00A20D8C AS DateTime), N'INPCSZ0228', CAST(0x0000A3FE00A26318 AS DateTime), N'10.160.80.46', N'测试1')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginONTime], [lastComputerName], [lastLoginOffTime], [lastIP], [Remarks]) VALUES (5, N'Leo', N'leo', N'陈江鹏', CAST(0x0000A3D400DCFB5B AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'测试0')
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
/****** Object:  Table [dbo].[GlobalAllEquipmentList]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalAllEquipmentList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL,
	[ItemType] [nvarchar](30) NOT NULL,
	[ItemDescription] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_GlobalAllEquipmentList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalAllEquipmentList] ON
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ItemType], [ItemDescription]) VALUES (1, N'E3631', N'Powersupply', N'电源')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ItemType], [ItemDescription]) VALUES (2, N'AQ2211Atten', N'Attennuator', N'衰减器')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ItemType], [ItemDescription]) VALUES (3, N'D86100', N'Scope', N'示波器')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ItemType], [ItemDescription]) VALUES (4, N'ElectricalSwitch', N'ElecSwitch', N'电开关')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ItemType], [ItemDescription]) VALUES (5, N'N490XPPG', N'PPG', N'误码仪 PPG')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ItemType], [ItemDescription]) VALUES (6, N'N490XED', N'ErrorDetector', N'误码仪 ED')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ItemType], [ItemDescription]) VALUES (7, N'AQ2211OpticalSwitch', N'OpticalSwitch', N'光开关')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ItemType], [ItemDescription]) VALUES (8, N'AQ2211PowerMeter', N'PowerMeter', N'功率计')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ItemType], [ItemDescription]) VALUES (9, N'TPO4300', N'Thermocontroller', N'热流仪')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ItemType], [ItemDescription]) VALUES (10, N'FLEX86100', N'Scope', N'示波器')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ItemType], [ItemDescription]) VALUES (11, N'MP1800PPG', N'PPG', N'误码仪 PPG')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ItemType], [ItemDescription]) VALUES (12, N'MP1800ED', N'ErrorDetector', N'误码仪 ED')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ItemType], [ItemDescription]) VALUES (13, N'TestEQ', N'TestBackGround', N'System.Windows.Forms.TextBox, Text: TestEQxxx')
SET IDENTITY_INSERT [dbo].[GlobalAllEquipmentList] OFF
/****** Object:  Table [dbo].[GlobalAllAppModelList]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalAllAppModelList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL,
	[ItemDescription] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_GlobalAllAppModelList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalAllAppModelList] ON
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (1, N'APPTXCAL', N'TXCalibration')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (2, N'APPTXFMT', N'TXFinalModuleTest')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (3, N'APPRXCAL', N'RXCalibration')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (4, N'APPRXFMT', N'RXFinalModuleTest')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (5, N'APPDUTCAL', N'DUTMonitorCalibration')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (6, N'APPDUTFMT', N'DUTFinalModuleTest')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (7, N'APPEDVT', N'EDVTProcess')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (8, N'APPEEPROM', N'Checkeeprom&AlarmWarning')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (9, N'APPDUTPrepare', N'ReadDUTInformationandInitializeRegister')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (10, N'AppModelTest', N'terry Test!!!')
SET IDENTITY_INSERT [dbo].[GlobalAllAppModelList] OFF
/****** Object:  StoredProcedure [dbo].[GetCurrServerTime]    Script Date: 12/22/2014 17:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--存储过程 GetCurrServerTime
--
--获取当前的系统时间
--
--参数		方向	类型		描述
--@CurrTime	Out		datetime	服务器当前时间

CREATE PROCEDURE [dbo].[GetCurrServerTime]
--@CurrTime varchar(30) Output
@CurrTime datetime Output
AS

BEGIN

SET XACT_ABORT ON 
BEGIN TRANSACTION 
--set @CurrTime = (SELECT CONVERT(varchar(30), GETDATE(), 120))
set @CurrTime = GETDATE()

COMMIT TRANSACTION 
SET XACT_ABORT OFF 

print @CurrTime
END
GO
/****** Object:  Table [dbo].[FunctionTable]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FunctionTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](25) NOT NULL,
	[FunctionCode] [int] NOT NULL,
	[Remarks] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_FunctionTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[FunctionTable] ON
INSERT [dbo].[FunctionTable] ([ID], [Title], [FunctionCode], [Remarks]) VALUES (1, N'Readable', 1, N'查询读取')
INSERT [dbo].[FunctionTable] ([ID], [Title], [FunctionCode], [Remarks]) VALUES (2, N'Writable', 2, N'编辑写入')
INSERT [dbo].[FunctionTable] ([ID], [Title], [FunctionCode], [Remarks]) VALUES (3, N'Addable', 4, N'新增资料')
INSERT [dbo].[FunctionTable] ([ID], [Title], [FunctionCode], [Remarks]) VALUES (4, N'Deletable', 8, N'删除资料')
INSERT [dbo].[FunctionTable] ([ID], [Title], [FunctionCode], [Remarks]) VALUES (5, N'Duplicable', 16, N'复制测试计划')
INSERT [dbo].[FunctionTable] ([ID], [Title], [FunctionCode], [Remarks]) VALUES (6, N'Exportable', 32, N'导出测试计划')
INSERT [dbo].[FunctionTable] ([ID], [Title], [FunctionCode], [Remarks]) VALUES (7, N'Importable', 64, N'导入测试计划')
INSERT [dbo].[FunctionTable] ([ID], [Title], [FunctionCode], [Remarks]) VALUES (8, N'DBOwner', 128, N'数据库变更')
INSERT [dbo].[FunctionTable] ([ID], [Title], [FunctionCode], [Remarks]) VALUES (9, N'EEPROMProgram', 256, N'EEPROM烧写')
INSERT [dbo].[FunctionTable] ([ID], [Title], [FunctionCode], [Remarks]) VALUES (10, N'MaintainEEPROM', 512, N'EEPROM资料维护')
SET IDENTITY_INSERT [dbo].[FunctionTable] OFF
/****** Object:  Table [dbo].[GlobalMSA]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalMSA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](25) NOT NULL,
	[AccessInterface] [nvarchar](25) NOT NULL,
	[SlaveAddress] [int] NOT NULL,
 CONSTRAINT [PK_GlobalMSA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalMSA] ON
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress]) VALUES (1, N'SFF8636', N'I2C', 160)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress]) VALUES (2, N'CFPMSA', N'MDIO', 32768)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress]) VALUES (3, N'SFF8472', N'I2C', 162)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress]) VALUES (4, N'SFF8077', N'I2C', 160)
SET IDENTITY_INSERT [dbo].[GlobalMSA] OFF
/****** Object:  Table [dbo].[GlobalManufactureCoefficientsGroup]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalManufactureCoefficientsGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_GlobalManufactureMemoryGroupTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalManufactureCoefficientsGroup] ON
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName]) VALUES (1, N'QFSP_1')
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName]) VALUES (4, N'CGR4')
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName]) VALUES (5, N'SFP')
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName]) VALUES (6, N'CSR4(AOC)')
SET IDENTITY_INSERT [dbo].[GlobalManufactureCoefficientsGroup] OFF
/****** Object:  Table [dbo].[GlobalManufactureCoefficients]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalManufactureCoefficients](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemTYPE] [nvarchar](30) NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL,
	[Channel] [tinyint] NOT NULL,
	[Page] [tinyint] NOT NULL,
	[StartAddress] [int] NOT NULL,
	[Length] [tinyint] NOT NULL,
	[Format] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_GlobalManufactureMemory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalManufactureCoefficients] ON
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (1, 1, N'Firmware', N'FirmwareRev', 0, 4, 128, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (2, 1, N'ADC', N'VccAdc', 0, 4, 130, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (3, 1, N'ADC', N'TemperatureAdc', 0, 4, 132, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (4, 1, N'ADC', N'RxPowerAdc', 1, 4, 134, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (5, 1, N'ADC', N'RxPowerAdc', 2, 4, 136, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (6, 1, N'ADC', N'RxPowerAdc', 3, 4, 138, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (7, 1, N'ADC', N'RxPowerAdc', 4, 4, 140, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (8, 1, N'ADC', N'TxBiasAdc', 1, 4, 142, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (9, 1, N'ADC', N'TxBiasAdc', 2, 4, 144, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (10, 1, N'ADC', N'TxBiasAdc', 3, 4, 146, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (11, 1, N'ADC', N'TxBiasAdc', 4, 4, 148, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (12, 1, N'ADC', N'TxPowerAdc', 1, 4, 150, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (13, 1, N'ADC', N'TxPowerAdc', 2, 4, 152, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (14, 1, N'ADC', N'TxPowerAdc', 3, 4, 154, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (15, 1, N'ADC', N'TxPowerAdc', 4, 4, 156, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (16, 1, N'Coefficient', N'DmiVccCoefB', 0, 5, 128, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (17, 1, N'Coefficient', N'DmiVccCoefC', 0, 5, 130, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (18, 1, N'Coefficient', N'DmiTempCoefB', 0, 5, 132, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (19, 1, N'Coefficient', N'DmiTempCoefC', 0, 5, 134, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (20, 1, N'Coefficient', N'DmiRxpowerCoefB', 1, 5, 136, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (21, 1, N'Coefficient', N'DmiRxpowerCoefC', 1, 5, 138, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (22, 1, N'Coefficient', N'DmiRxpowerCoefB', 2, 5, 140, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (23, 1, N'Coefficient', N'DmiRxpowerCoefC', 2, 5, 142, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (24, 1, N'Coefficient', N'DmiRxpowerCoefB', 3, 5, 144, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (25, 1, N'Coefficient', N'DmiRxpowerCoefC', 3, 5, 146, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (26, 1, N'Coefficient', N'DmiRxpowerCoefB', 4, 5, 148, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (27, 1, N'Coefficient', N'DmiRxpowerCoefC', 4, 5, 150, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (28, 1, N'Coefficient', N'DmiTxPowerSlopCoefA', 1, 7, 128, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (29, 1, N'Coefficient', N'DmiTxPowerSlopCoefB', 1, 7, 132, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (30, 1, N'Coefficient', N'DmiTxPowerSlopCoefC', 1, 7, 136, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (31, 1, N'Coefficient', N'DmiTxPowerOffsetCoefA', 1, 7, 140, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (32, 1, N'Coefficient', N'DmiTxPowerOffsetCoefB', 1, 7, 144, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (33, 1, N'Coefficient', N'DmiTxPowerOffsetCoefC', 1, 7, 148, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (34, 1, N'Coefficient', N'DmiTxPowerSlopCoefA', 2, 7, 152, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (35, 1, N'Coefficient', N'DmiTxPowerSlopCoefB', 2, 7, 156, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (36, 1, N'Coefficient', N'DmiTxPowerSlopCoefC', 2, 7, 160, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (37, 1, N'Coefficient', N'DmiTxPowerOffsetCoefA', 2, 7, 164, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (38, 1, N'Coefficient', N'DmiTxPowerOffsetCoefB', 2, 7, 168, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (39, 1, N'Coefficient', N'DmiTxPowerOffsetCoefC', 2, 7, 172, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (40, 1, N'Coefficient', N'DmiTxPowerSlopCoefA', 3, 7, 176, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (41, 1, N'Coefficient', N'DmiTxPowerSlopCoefB', 3, 7, 180, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (42, 1, N'Coefficient', N'DmiTxPowerSlopCoefC', 3, 7, 184, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (43, 1, N'Coefficient', N'DmiTxPowerOffsetCoefA', 3, 7, 188, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (44, 1, N'Coefficient', N'DmiTxPowerOffsetCoefB', 3, 7, 192, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (45, 1, N'Coefficient', N'DmiTxPowerOffsetCoefC', 3, 7, 196, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (46, 1, N'Coefficient', N'DmiTxPowerSlopCoefA', 4, 7, 200, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (47, 1, N'Coefficient', N'DmiTxPowerSlopCoefB', 4, 7, 204, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (48, 1, N'Coefficient', N'DmiTxPowerSlopCoefC', 4, 7, 208, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (49, 1, N'Coefficient', N'DmiTxPowerOffsetCoefA', 4, 7, 212, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (50, 1, N'Coefficient', N'DmiTxPowerOffsetCoefB', 4, 7, 216, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (51, 1, N'Coefficient', N'DmiTxPowerOffsetCoefC', 4, 7, 220, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (52, 1, N'Coefficient', N'TxTargetBiasDacCoefA', 1, 5, 152, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (53, 1, N'Coefficient', N'TxTargetBiasDacCoefB', 1, 5, 156, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (54, 1, N'Coefficient', N'TxTargetBiasDacCoefC', 1, 5, 160, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (55, 1, N'Coefficient', N'TxTargetBiasDacCoefA', 2, 5, 164, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (56, 1, N'Coefficient', N'TxTargetBiasDacCoefB', 2, 5, 168, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (57, 1, N'Coefficient', N'TxTargetBiasDacCoefC', 2, 5, 172, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (58, 1, N'Coefficient', N'TxTargetBiasDacCoefA', 3, 5, 176, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (59, 1, N'Coefficient', N'TxTargetBiasDacCoefB', 3, 5, 180, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (60, 1, N'Coefficient', N'TxTargetBiasDacCoefC', 3, 5, 184, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (61, 1, N'Coefficient', N'TxTargetBiasDacCoefA', 4, 5, 188, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (62, 1, N'Coefficient', N'TxTargetBiasDacCoefB', 4, 5, 192, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (63, 1, N'Coefficient', N'TxTargetBiasDacCoefC', 4, 5, 196, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (64, 1, N'Coefficient', N'TxTargetModDacCoefA', 1, 5, 200, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (65, 1, N'Coefficient', N'TxTargetModDacCoefB', 1, 5, 204, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (66, 1, N'Coefficient', N'TxTargetModDacCoefC', 1, 5, 208, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (67, 1, N'Coefficient', N'TxTargetModDacCoefA', 2, 5, 212, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (68, 1, N'Coefficient', N'TxTargetModDacCoefB', 2, 5, 216, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (69, 1, N'Coefficient', N'TxTargetModDacCoefC', 2, 5, 220, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (70, 1, N'Coefficient', N'TxTargetModDacCoefA', 3, 5, 224, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (71, 1, N'Coefficient', N'TxTargetModDacCoefB', 3, 5, 228, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (72, 1, N'Coefficient', N'TxTargetModDacCoefC', 3, 5, 232, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (73, 1, N'Coefficient', N'TxTargetModDacCoefA', 4, 5, 236, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (74, 1, N'Coefficient', N'TxTargetModDacCoefB', 4, 5, 240, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (75, 1, N'Coefficient', N'TxTargetModDacCoefC', 4, 5, 244, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (80, 1, N'Coefficient', N'TargetLopcoefA', 0, 0, 0, 0, N'0')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (81, 1, N'Coefficient', N'TargetLopcoefB', 0, 0, 0, 0, N'0')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (82, 1, N'Coefficient', N'TargetLopcoefC', 0, 0, 0, 0, N'0')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (95, 1, N'APC config', N'APCControll', 0, 4, 255, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (100, 4, N'Firmware', N'FirmwareRev', 0, 4, 128, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (101, 4, N'ADC', N'VccAdc', 0, 4, 130, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (102, 4, N'ADC', N'Vcc2Adc', 0, 4, 132, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (103, 4, N'ADC', N'Vcc3Adc', 0, 4, 134, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (104, 4, N'ADC', N'TemperatureAdc', 0, 4, 136, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (105, 4, N'ADC', N'Temperature2Adc', 0, 4, 138, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (106, 4, N'ADC', N'RxPowerAdc', 1, 4, 140, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (107, 4, N'ADC', N'RxPowerAdc', 2, 4, 142, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (108, 4, N'ADC', N'RxPowerAdc', 3, 4, 144, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (109, 4, N'ADC', N'RxPowerAdc', 4, 4, 146, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (110, 4, N'ADC', N'TxBiasAdc', 1, 4, 148, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (111, 4, N'ADC', N'TxBiasAdc', 2, 4, 150, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (112, 4, N'ADC', N'TxBiasAdc', 3, 4, 152, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (113, 4, N'ADC', N'TxBiasAdc', 4, 4, 154, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (114, 4, N'ADC', N'TxPowerAdc', 1, 4, 156, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (115, 4, N'ADC', N'TxPowerAdc', 2, 4, 158, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (116, 4, N'ADC', N'TxPowerAdc', 3, 4, 160, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (117, 4, N'ADC', N'TxPowerAdc', 4, 4, 162, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (118, 4, N'Coefficient', N'DmiVccCoefB', 0, 5, 128, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (119, 4, N'Coefficient', N'DmiVcc2CoefB', 0, 5, 136, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (120, 4, N'Coefficient', N'DmiVcc3CoefB', 0, 5, 144, 4, N'IEEE754')
GO
print 'Processed 100 total records'
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (121, 4, N'Coefficient', N'DmiVccCoefC', 0, 5, 132, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (122, 4, N'Coefficient', N'DmiVcc2CoefC', 0, 5, 140, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (123, 4, N'Coefficient', N'DmiVcc3CoefC', 0, 5, 148, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (124, 4, N'Coefficient', N'DmiTempCoefB', 0, 5, 152, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (125, 4, N'Coefficient', N'DmiTemp2CoefB', 0, 5, 160, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (126, 4, N'Coefficient', N'DmiTempCoefC', 0, 5, 156, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (127, 4, N'Coefficient', N'DmiTemp2CoefC', 0, 5, 164, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (128, 4, N'Coefficient', N'DMIRXPOWERCOEFA', 1, 5, 168, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (129, 4, N'Coefficient', N'DMIRXPOWERCOEFA', 2, 5, 180, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (130, 4, N'Coefficient', N'DMIRXPOWERCOEFA', 3, 5, 192, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (131, 4, N'Coefficient', N'DMIRXPOWERCOEFA', 4, 5, 204, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (132, 4, N'Coefficient', N'DmiRxpowerCoefB', 1, 5, 172, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (133, 4, N'Coefficient', N'DmiRxpowerCoefB', 2, 5, 184, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (134, 4, N'Coefficient', N'DmiRxpowerCoefB', 3, 5, 196, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (135, 4, N'Coefficient', N'DmiRxpowerCoefB', 4, 5, 208, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (136, 4, N'Coefficient', N'DmiRxpowerCoefC', 1, 5, 176, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (137, 4, N'Coefficient', N'DmiRxpowerCoefC', 2, 5, 188, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (138, 4, N'Coefficient', N'DmiRxpowerCoefC', 3, 5, 200, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (139, 4, N'Coefficient', N'DmiRxpowerCoefC', 4, 5, 212, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (140, 4, N'Coefficient', N'DmiTxPowerSlopCoefA', 2, 6, 152, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (141, 4, N'Coefficient', N'DmiTxPowerSlopCoefA', 3, 6, 176, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (142, 4, N'Coefficient', N'DmiTxPowerSlopCoefA', 4, 6, 188, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (143, 4, N'Coefficient', N'DmiTxPowerSlopCoefB', 1, 6, 132, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (144, 4, N'Coefficient', N'DmiTxPowerSlopCoefB', 2, 6, 156, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (145, 4, N'Coefficient', N'DmiTxPowerSlopCoefB', 3, 6, 180, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (146, 4, N'Coefficient', N'DmiTxPowerSlopCoefB', 4, 6, 204, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (147, 4, N'Coefficient', N'DmiTxPowerSlopCoefC', 1, 6, 136, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (148, 4, N'Coefficient', N'DmiTxPowerSlopCoefC', 2, 6, 160, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (149, 4, N'Coefficient', N'DmiTxPowerSlopCoefC', 3, 6, 184, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (150, 4, N'Coefficient', N'DmiTxPowerSlopCoefC', 4, 6, 208, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (151, 4, N'Coefficient', N'DmiTxPowerOffsetCoefA', 1, 6, 140, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (152, 4, N'Coefficient', N'DmiTxPowerOffsetCoefA', 2, 6, 164, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (153, 4, N'Coefficient', N'DmiTxPowerOffsetCoefA', 3, 6, 188, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (154, 4, N'Coefficient', N'DmiTxPowerOffsetCoefA', 4, 6, 212, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (155, 4, N'Coefficient', N'DmiTxPowerOffsetCoefB', 1, 6, 144, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (156, 4, N'Coefficient', N'DmiTxPowerOffsetCoefB', 2, 6, 168, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (157, 4, N'Coefficient', N'DmiTxPowerOffsetCoefB', 3, 6, 192, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (158, 4, N'Coefficient', N'DmiTxPowerOffsetCoefB', 4, 6, 216, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (159, 4, N'Coefficient', N'DmiTxPowerOffsetCoefC', 1, 6, 148, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (160, 4, N'Coefficient', N'DmiTxPowerOffsetCoefC', 2, 6, 172, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (161, 4, N'Coefficient', N'DmiTxPowerOffsetCoefC', 3, 6, 196, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (162, 4, N'Coefficient', N'DmiTxPowerOffsetCoefC', 4, 6, 220, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (163, 4, N'Coefficient', N'TxTargetBiasDacCoefA', 1, 7, 128, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (164, 4, N'Coefficient', N'TxTargetBiasDacCoefA', 2, 7, 140, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (165, 4, N'Coefficient', N'TxTargetBiasDacCoefA', 3, 7, 152, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (166, 4, N'Coefficient', N'TxTargetBiasDacCoefA', 4, 7, 164, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (167, 4, N'Coefficient', N'TxTargetBiasDacCoefB', 1, 7, 132, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (168, 4, N'Coefficient', N'TxTargetBiasDacCoefB', 2, 7, 144, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (169, 4, N'Coefficient', N'TxTargetBiasDacCoefB', 3, 7, 156, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (170, 4, N'Coefficient', N'TxTargetBiasDacCoefB', 4, 7, 168, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (171, 4, N'Coefficient', N'TxTargetBiasDacCoefC', 1, 7, 136, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (172, 4, N'Coefficient', N'TxTargetBiasDacCoefC', 2, 7, 148, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (173, 4, N'Coefficient', N'TxTargetBiasDacCoefC', 3, 7, 160, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (174, 4, N'Coefficient', N'TxTargetBiasDacCoefC', 4, 7, 172, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (175, 4, N'Coefficient', N'TxTargetModDacCoefA', 1, 7, 196, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (176, 4, N'Coefficient', N'TxTargetModDacCoefA', 2, 7, 212, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (177, 4, N'Coefficient', N'TxTargetModDacCoefA', 3, 7, 228, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (178, 4, N'Coefficient', N'TxTargetModDacCoefA', 4, 7, 244, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (179, 4, N'Coefficient', N'TxTargetModDacCoefB', 1, 7, 200, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (180, 4, N'Coefficient', N'TxTargetModDacCoefB', 2, 7, 216, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (181, 4, N'Coefficient', N'TxTargetModDacCoefB', 3, 7, 232, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (182, 4, N'Coefficient', N'TxTargetModDacCoefB', 4, 7, 248, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (183, 4, N'Coefficient', N'TxTargetModDacCoefC', 1, 7, 204, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (184, 4, N'Coefficient', N'TxTargetModDacCoefC', 2, 7, 220, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (185, 4, N'Coefficient', N'TxTargetModDacCoefC', 3, 7, 236, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (186, 4, N'Coefficient', N'TxTargetModDacCoefC', 4, 7, 252, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (187, 4, N'APC config', N'APCControll', 0, 5, 255, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (188, 4, N'Coefficient', N'DEBUGINTERFACE', 0, 4, 200, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (189, 1, N'Coefficient', N'DEBUGINTERFACE', 0, 4, 158, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (190, 4, N'Coefficient', N'CLOSELOOPCOEFA', 1, 8, 128, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (191, 4, N'Coefficient', N'CLOSELOOPCOEFB', 1, 8, 132, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (192, 4, N'Coefficient', N'CLOSELOOPCOEFC', 1, 8, 136, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (193, 4, N'Coefficient', N'CLOSELOOPCOEFA', 2, 8, 140, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (194, 4, N'Coefficient', N'CLOSELOOPCOEFB', 2, 8, 144, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (195, 4, N'Coefficient', N'CLOSELOOPCOEFC', 2, 8, 144, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (196, 4, N'Coefficient', N'CLOSELOOPCOEFA', 3, 8, 152, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (197, 4, N'Coefficient', N'CLOSELOOPCOEFB', 3, 8, 156, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (198, 4, N'Coefficient', N'CLOSELOOPCOEFC', 3, 8, 160, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (199, 4, N'Coefficient', N'CLOSELOOPCOEFA', 4, 8, 164, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (200, 4, N'Coefficient', N'CLOSELOOPCOEFB', 4, 8, 168, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (201, 4, N'Coefficient', N'CLOSELOOPCOEFC', 4, 8, 172, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (202, 4, N'Coefficient', N'COEFP', 1, 7, 132, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (203, 4, N'Coefficient', N'COEFP', 2, 7, 148, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (204, 4, N'Coefficient', N'COEFP', 3, 7, 164, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (205, 4, N'Coefficient', N'COEFP', 4, 7, 180, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (206, 4, N'Coefficient', N'COEFI', 1, 7, 136, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (207, 4, N'Coefficient', N'COEFI', 2, 7, 152, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (208, 4, N'Coefficient', N'COEFI', 3, 7, 168, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (209, 4, N'Coefficient', N'COEFI', 4, 7, 184, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (210, 4, N'Coefficient', N'COEFD', 1, 7, 140, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (211, 4, N'Coefficient', N'COEFD', 2, 7, 156, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (212, 4, N'Coefficient', N'COEFD', 3, 7, 172, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (213, 4, N'Coefficient', N'COEFD', 4, 7, 188, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (214, 4, N'threshold', N'BIASADCTHRESHOLD', 1, 5, 236, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (215, 4, N'threshold', N'BIASADCTHRESHOLD', 2, 5, 237, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (216, 4, N'threshold', N'BIASADCTHRESHOLD', 3, 5, 238, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (217, 4, N'threshold', N'BIASADCTHRESHOLD', 4, 5, 239, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (218, 4, N'Coefficient', N'REFERENCETEMPERATURECOEF', 0, 6, 128, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (219, 4, N'threshold', N'RXADCTHRESHOLD', 1, 5, 240, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (220, 4, N'threshold', N'RXADCTHRESHOLD', 2, 5, 241, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (221, 4, N'threshold', N'RXADCTHRESHOLD', 3, 5, 242, 1, N'U8')
GO
print 'Processed 200 total records'
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (222, 4, N'threshold', N'RXADCTHRESHOLD', 4, 5, 243, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (223, 4, N'Coefficient', N'SETPOINT', 1, 7, 128, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (224, 4, N'Coefficient', N'SETPOINT', 2, 7, 144, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (225, 4, N'Coefficient', N'SETPOINT', 3, 7, 160, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (226, 4, N'Coefficient', N'SETPOINT', 4, 7, 176, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (227, 4, N'Coefficient', N'TXPFITSCOEFA', 1, 6, 162, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (228, 4, N'Coefficient', N'TXPFITSCOEFA', 2, 6, 174, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (229, 4, N'Coefficient', N'TXPFITSCOEFA', 3, 6, 186, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (230, 4, N'Coefficient', N'TXPFITSCOEFA', 4, 6, 198, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (231, 4, N'Coefficient', N'TXPFITSCOEFB', 1, 6, 166, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (232, 4, N'Coefficient', N'TXPFITSCOEFB', 2, 6, 178, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (233, 4, N'Coefficient', N'TXPFITSCOEFB', 3, 6, 190, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (234, 4, N'Coefficient', N'TXPFITSCOEFB', 4, 6, 202, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (235, 4, N'Coefficient', N'TXPFITSCOEFC', 1, 6, 170, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (236, 4, N'Coefficient', N'TXPFITSCOEFC', 2, 6, 182, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (237, 4, N'Coefficient', N'TXPFITSCOEFC', 3, 6, 194, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (238, 4, N'Coefficient', N'TXPFITSCOEFC', 4, 6, 206, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (239, 4, N'Coefficient', N'TXPPROPORTIONGREATCOEF', 1, 6, 146, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (240, 4, N'Coefficient', N'TXPPROPORTIONGREATCOEF', 2, 6, 150, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (241, 4, N'Coefficient', N'TXPPROPORTIONGREATCOEF', 3, 6, 154, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (242, 4, N'Coefficient', N'TXPPROPORTIONGREATCOEF', 4, 6, 158, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (243, 4, N'Coefficient', N'TXPPROPORTIONLESSCOEF', 1, 6, 130, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (244, 4, N'Coefficient', N'TXPPROPORTIONLESSCOEF', 2, 6, 134, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (245, 4, N'Coefficient', N'TXPPROPORTIONLESSCOEF', 3, 6, 138, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (246, 4, N'Coefficient', N'TXPPROPORTIONLESSCOEF', 4, 6, 142, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (248, 5, N'Firmware', N'FirmwareRev', 0, 4, 0, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (249, 6, N'Firmware', N'FirmwareRev', 0, 4, 128, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (250, 6, N'ADC', N'VccAdc', 0, 4, 130, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (251, 6, N'ADC', N'Vcc2Adc', 0, 4, 132, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (252, 6, N'ADC', N'Vcc3Adc', 0, 4, 134, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (253, 6, N'ADC', N'TemperatureAdc', 0, 4, 136, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (254, 6, N'ADC', N'Temperature2Adc', 0, 4, 138, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (255, 6, N'ADC', N'RxPowerAdc', 1, 4, 140, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (256, 6, N'ADC', N'RxPowerAdc', 2, 4, 142, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (257, 6, N'ADC', N'RxPowerAdc', 3, 4, 144, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (258, 6, N'ADC', N'RxPowerAdc', 4, 4, 146, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (259, 6, N'ADC', N'TxBiasAdc', 1, 4, 148, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (260, 6, N'ADC', N'TxBiasAdc', 2, 4, 150, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (261, 6, N'ADC', N'TxBiasAdc', 3, 4, 152, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (262, 6, N'ADC', N'TxBiasAdc', 4, 4, 154, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (263, 6, N'ADC', N'TxPowerAdc', 1, 4, 156, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (264, 6, N'ADC', N'TxPowerAdc', 2, 4, 158, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (265, 6, N'ADC', N'TxPowerAdc', 3, 4, 160, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (266, 6, N'ADC', N'TxPowerAdc', 4, 4, 162, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (267, 6, N'Coefficient', N'DmiVccCoefB', 0, 5, 128, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (268, 6, N'Coefficient', N'DmiVcc2CoefB', 0, 5, 136, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (269, 6, N'Coefficient', N'DmiVcc3CoefB', 0, 5, 144, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (270, 6, N'Coefficient', N'DmiVccCoefC', 0, 5, 132, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (271, 6, N'Coefficient', N'DmiVcc2CoefC', 0, 5, 140, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (272, 6, N'Coefficient', N'DmiVcc3CoefC', 0, 5, 148, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (273, 6, N'Coefficient', N'DmiTempCoefB', 0, 5, 152, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (274, 6, N'Coefficient', N'DmiTemp2CoefB', 0, 5, 160, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (275, 6, N'Coefficient', N'DmiTempCoefC', 0, 5, 156, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (276, 6, N'Coefficient', N'DmiTemp2CoefC', 0, 5, 164, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (277, 6, N'Coefficient', N'DmiRxpowerCoefA', 1, 5, 168, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (278, 6, N'Coefficient', N'DmiRxpowerCoefB', 1, 5, 172, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (279, 6, N'Coefficient', N'DmiRxpowerCoefC', 1, 5, 176, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (280, 6, N'Coefficient', N'DmiRxpowerCoefA', 2, 5, 180, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (281, 6, N'Coefficient', N'DmiRxpowerCoefB', 2, 5, 184, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (282, 6, N'Coefficient', N'DmiRxpowerCoefC', 2, 5, 188, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (283, 6, N'Coefficient', N'DmiRxpowerCoefA', 3, 5, 192, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (284, 6, N'Coefficient', N'DmiRxpowerCoefB', 3, 5, 196, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (285, 6, N'Coefficient', N'DmiRxpowerCoefC', 3, 5, 200, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (286, 6, N'Coefficient', N'DmiRxpowerCoefA', 4, 5, 204, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (287, 6, N'Coefficient', N'DmiRxpowerCoefB', 4, 5, 208, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (288, 6, N'Coefficient', N'DmiRxpowerCoefC', 4, 5, 212, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (289, 6, N'Coefficient', N'TxTargetBiasDacCoefA', 1, 7, 128, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (290, 6, N'Coefficient', N'TxTargetBiasDacCoefB', 1, 7, 132, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (291, 6, N'Coefficient', N'TxTargetBiasDacCoefC', 1, 7, 136, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (292, 6, N'Coefficient', N'TxTargetBiasDacCoefA', 2, 7, 140, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (293, 6, N'Coefficient', N'TxTargetBiasDacCoefB', 2, 7, 144, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (294, 6, N'Coefficient', N'TxTargetBiasDacCoefC', 2, 7, 148, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (295, 6, N'Coefficient', N'TxTargetBiasDacCoefA', 3, 7, 152, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (296, 6, N'Coefficient', N'TxTargetBiasDacCoefB', 3, 7, 156, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (297, 6, N'Coefficient', N'TxTargetBiasDacCoefC', 3, 7, 160, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (298, 6, N'Coefficient', N'TxTargetBiasDacCoefA', 4, 7, 164, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (299, 6, N'Coefficient', N'TxTargetBiasDacCoefB', 4, 7, 168, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (300, 6, N'Coefficient', N'TxTargetBiasDacCoefC', 4, 7, 172, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (301, 6, N'Coefficient', N'TxTargetModDacCoefA', 1, 7, 176, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (302, 6, N'Coefficient', N'TxTargetModDacCoefB', 1, 7, 180, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (303, 6, N'Coefficient', N'TxTargetModDacCoefC', 1, 7, 184, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (304, 6, N'Coefficient', N'TxTargetModDacCoefA', 2, 7, 188, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (305, 6, N'Coefficient', N'TxTargetModDacCoefB', 2, 7, 192, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (306, 6, N'Coefficient', N'TxTargetModDacCoefC', 2, 7, 196, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (307, 6, N'Coefficient', N'TxTargetModDacCoefA', 3, 7, 200, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (308, 6, N'Coefficient', N'TxTargetModDacCoefB', 3, 7, 204, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (309, 6, N'Coefficient', N'TxTargetModDacCoefC', 3, 7, 208, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (310, 6, N'Coefficient', N'TxTargetModDacCoefA', 4, 7, 212, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (311, 6, N'Coefficient', N'TxTargetModDacCoefB', 4, 7, 216, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (312, 6, N'Coefficient', N'TxTargetModDacCoefC', 4, 7, 220, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (313, 6, N'Coefficient', N'TXPPROPORTIONLESSCOEF', 2, 6, 134, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (314, 6, N'Coefficient', N'TXPPROPORTIONLESSCOEF', 3, 6, 138, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (315, 6, N'Coefficient', N'TXPPROPORTIONLESSCOEF', 1, 6, 130, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (316, 6, N'Coefficient', N'TXPPROPORTIONLESSCOEF', 4, 6, 142, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (317, 6, N'Coefficient', N'REFERENCETEMPERATURECOEF', 0, 6, 128, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (318, 6, N'Coefficient', N'TXPPROPORTIONGREATCOEF', 1, 6, 146, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (319, 6, N'Coefficient', N'TXPPROPORTIONGREATCOEF', 2, 6, 150, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (320, 6, N'Coefficient', N'TXPPROPORTIONGREATCOEF', 3, 6, 154, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (321, 6, N'Coefficient', N'TXPPROPORTIONGREATCOEF', 4, 6, 158, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (322, 6, N'Coefficient', N'TXPFITSCOEFA', 1, 6, 162, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (323, 6, N'Coefficient', N'TXPFITSCOEFA', 2, 6, 174, 4, N'IEEE754')
GO
print 'Processed 300 total records'
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (324, 6, N'Coefficient', N'TXPFITSCOEFA', 3, 6, 186, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (325, 6, N'Coefficient', N'TXPFITSCOEFA', 4, 6, 198, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (326, 6, N'Coefficient', N'TXPFITSCOEFB', 1, 6, 166, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (327, 6, N'Coefficient', N'TXPFITSCOEFB', 2, 6, 178, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (328, 6, N'Coefficient', N'TXPFITSCOEFB', 3, 6, 190, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (329, 6, N'Coefficient', N'TXPFITSCOEFB', 4, 6, 202, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (330, 6, N'Coefficient', N'TXPFITSCOEFC', 1, 6, 170, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (331, 6, N'Coefficient', N'TXPFITSCOEFC', 2, 6, 182, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (332, 6, N'Coefficient', N'TXPFITSCOEFC', 3, 6, 194, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (333, 6, N'Coefficient', N'TXPFITSCOEFC', 4, 6, 206, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (334, 6, N'Coefficient', N'DEBUGINTERFACE', 0, 4, 200, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (335, 6, N'APC config', N'APCControll', 0, 5, 255, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (336, 6, N'threshold', N'BIASADCTHRESHOLD', 1, 5, 236, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (337, 6, N'threshold', N'BIASADCTHRESHOLD', 2, 5, 237, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (338, 6, N'threshold', N'BIASADCTHRESHOLD', 3, 5, 238, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (339, 6, N'threshold', N'BIASADCTHRESHOLD', 4, 5, 239, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (340, 6, N'threshold', N'RXADCTHRESHOLD', 1, 5, 240, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (341, 6, N'threshold', N'RXADCTHRESHOLD', 2, 5, 241, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (342, 6, N'threshold', N'RXADCTHRESHOLD', 3, 5, 242, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (343, 6, N'threshold', N'RXADCTHRESHOLD', 4, 5, 243, 1, N'U8')
SET IDENTITY_INSERT [dbo].[GlobalManufactureCoefficients] OFF
/****** Object:  Table [dbo].[GlobalProductionType]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalProductionType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](25) NOT NULL,
	[MSAID] [int] NOT NULL,
 CONSTRAINT [PK_GlobalProductionType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalProductionType] ON
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID]) VALUES (1, N'QSFP', 1)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID]) VALUES (2, N'CFP', 2)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID]) VALUES (3, N'SFP', 3)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID]) VALUES (4, N'XFP', 4)
SET IDENTITY_INSERT [dbo].[GlobalProductionType] OFF
/****** Object:  Table [dbo].[UserRoleTable]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoleTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_UserRoleTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserRoleTable] ON
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (1, 2, 3)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (3, 2, 5)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (14, 5, 1)
SET IDENTITY_INSERT [dbo].[UserRoleTable] OFF
/****** Object:  Table [dbo].[GlobalMSADefintionInf]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalMSADefintionInf](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[FieldName] [nvarchar](30) NOT NULL,
	[Channel] [tinyint] NOT NULL,
	[SlaveAddress] [int] NOT NULL,
	[Page] [tinyint] NOT NULL,
	[StartAddress] [int] NOT NULL,
	[Length] [tinyint] NOT NULL,
	[Format] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_GlobalMSADefintionInf] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalMSADefintionInf] ON
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (1, 1, N'DmiTemp', 0, 160, 0, 22, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (2, 1, N'DmiVcc', 0, 160, 0, 26, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (3, 1, N'DmiBias', 1, 160, 0, 42, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (4, 1, N'DmiBias', 2, 160, 0, 44, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (5, 1, N'DmiBias', 3, 160, 0, 46, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (6, 1, N'DmiBias', 4, 160, 0, 48, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (7, 1, N'DmiTxPower', 1, 160, 0, 50, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (8, 1, N'DmiTxPower', 2, 160, 0, 52, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (9, 1, N'DmiTxPower', 3, 160, 0, 54, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (10, 1, N'DmiTxPower', 4, 160, 0, 56, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (11, 1, N'DmiRxPower', 1, 160, 0, 34, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (12, 1, N'DmiRxPower', 2, 160, 0, 36, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (13, 1, N'DmiRxPower', 3, 160, 0, 38, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (14, 1, N'DmiRxPower', 4, 160, 0, 40, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (15, 1, N'RxLos', 1, 160, 0, 3, 1, N'U8')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (16, 1, N'RxLos', 2, 160, 0, 3, 1, N'U8')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (17, 1, N'RxLos', 3, 160, 0, 3, 1, N'U8')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (18, 1, N'RxLos', 4, 160, 0, 3, 1, N'U8')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (19, 1, N'TxDisable', 1, 160, 0, 86, 1, N'U8')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (20, 1, N'TxDisable', 2, 160, 0, 86, 1, N'U8')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (21, 1, N'TxDisable', 3, 160, 0, 86, 1, N'U8')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (22, 1, N'TxDisable', 4, 160, 0, 86, 1, N'U8')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (23, 1, N'VendorName', 0, 160, 0, 148, 16, N'ASCII')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (24, 1, N'VendorPN', 0, 160, 0, 168, 16, N'ASCII')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (25, 1, N'VendorSN', 0, 160, 0, 196, 16, N'ASCII')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (26, 1, N'TempHighAlarmTh', 0, 160, 3, 128, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (27, 1, N'TempLowAlarmTh', 0, 160, 3, 130, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (28, 1, N'TempHighWarningTh', 0, 160, 3, 132, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (29, 1, N'TempLowWarningTh', 0, 160, 3, 134, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (30, 1, N'VccHighAlarmTh', 0, 160, 3, 144, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (31, 1, N'VccLowAlarmTh', 0, 160, 3, 146, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (32, 1, N'VccHighWarningTh', 0, 160, 3, 148, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (33, 1, N'VccLowWarningTh', 0, 160, 3, 150, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (34, 1, N'RxPowerHighAlarmTh', 0, 160, 3, 176, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (35, 1, N'RxPowerLowAlarmTh', 0, 160, 3, 178, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (36, 1, N'RxPowerHighWarningTh', 0, 160, 3, 180, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (37, 1, N'RxPowerLowWarningTh', 0, 160, 3, 182, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (38, 1, N'TxBiasHighAlarmTh', 0, 160, 3, 184, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (39, 1, N'TxBiasLowAlarmTh', 0, 160, 3, 186, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (40, 1, N'TxBiasHighWarningTh', 0, 160, 3, 188, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (41, 1, N'TxBiasLowWarningTh', 0, 160, 3, 190, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (42, 1, N'TxPowerHighAlarmTh', 0, 160, 3, 192, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (43, 1, N'TxPowerLowAlarmTh', 0, 160, 3, 194, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (44, 1, N'TxPowerHighWarningTh', 0, 160, 3, 196, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (45, 1, N'TxPowerLowWarningTh', 0, 160, 3, 198, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (50, 3, N'VccHighAlarmTh', 1, 162, 0, 8, 2, N'U16')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (51, 3, N'VccLowAlarmTh', 1, 162, 0, 10, 2, N'U16')
SET IDENTITY_INSERT [dbo].[GlobalMSADefintionInf] OFF
/****** Object:  Table [dbo].[GlobalAllTestModelList]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalAllTestModelList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL,
	[ItemDescription] [nvarchar](50) NULL,
 CONSTRAINT [PK_GlobalAllTestModelList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalAllTestModelList] ON
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (1, 1, N'AdjustEye', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (2, 1, N'AdjustTxPowerDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (3, 2, N'TestEye', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (4, 2, N'TestTxPowerDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (5, 2, N'TestIBiasDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (6, 3, N'AdjustLos', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (7, 3, N'CalRxDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (8, 4, N'TestRXLosAD', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (9, 4, N'TestRxPowerDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (10, 4, N'TestBer', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (11, 5, N'CalVccDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (12, 5, N'CalTempDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (13, 6, N'TestTempDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (14, 6, N'TestVccDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (15, 6, N'TestIcc', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (16, 3, N'AdjustAPD', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (17, 8, N'AlarmWarning', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (18, 3, N'CalRxDminoProcessingCoef', N'140704新增')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (19, 5, N'CalTempDminoProcessingCoef', N'140704新增')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (20, 5, N'CalVccDminoProcessingCoef', N'140704新增')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (22, 10, N'ModelTest1', N'TerryModelTest1')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (23, 4, N'TestEleEye', N'TestEleEye...')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ItemDescription]) VALUES (24, 6, N'TestTransfer', N'TestTransfer  无输入参数 ；')
SET IDENTITY_INSERT [dbo].[GlobalAllTestModelList] OFF
/****** Object:  Table [dbo].[GlobalAllEquipmentParamterList]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalAllEquipmentParamterList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[Item] [nvarchar](30) NOT NULL,
	[ItemValue] [nvarchar](255) NOT NULL,
	[ItemDescription] [nvarchar](500) NOT NULL,
	[ItemType] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_GlobalAllEquipmentParamterList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalAllEquipmentParamterList] ON
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (1, 1, N'Addr', N'5', N'GPIB地址（1~30）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (2, 1, N'IOType', N'GPIB', N'接口类型（GPIB或USB）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (3, 1, N'Reset', N'False', N'是否复位（True 是，False 否）', N'bool')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (4, 1, N'Name', N'E3631', N'仪器名称（E3631）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (5, 1, N'DutChannel', N'1', N'DUT通道', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (6, 1, N'OptSourceChannel', N'2', N'光源通道', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (7, 1, N'DutVoltage', N'3.3', N'DUT电压值(单位V)', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (8, 1, N'DutCurrent', N'1.5', N'DUT限流(单位A)', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (9, 1, N'OptVoltage', N'3.3', N'光源电压(单位V)', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (10, 1, N'OptCurrent', N'1.5', N'光源限流(单位A)', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (11, 2, N'Addr', N'6', N'GPIB地址（1~30）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (12, 2, N'IOType', N'GPIB', N'接口类型（GPIB或USB）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (13, 2, N'Reset', N'False', N'是否复位（True 是，False 否）', N'bool')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (14, 2, N'Name', N'AQ2211Atten', N'仪器名称（AQ2211Atten）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (15, 2, N'TOTALCHANNEL', N'4', N'模块的总通道数', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (16, 2, N'AttValue', N'20', N'衰减值', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (17, 2, N'AttSlot', N'1,2,3,4', N'插槽数', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (18, 2, N'WAVELENGTH', N'1270,1290,1310,1330', N'通道1,2,3,4对应波长数', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (22, 2, N'AttChannel', N'1', N'衰减器通道数', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (23, 3, N'Addr', N'15', N'GPIB地址（1~30）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (24, 3, N'IOType', N'GPIB', N'接口类型（GPIB或USB）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (25, 3, N'Reset', N'False', N'是否复位（True 是，False 否）', N'bool')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (26, 3, N'Name', N'D86100', N'仪器名称（D86100）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (27, 3, N'OptChannel', N'1', N'光通道号（1-4）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (28, 3, N'ElecChannel', N'2', N'电通道号（1-4）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (29, 3, N'Scale', N'0.00095', N'周期', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (30, 3, N'Offset', N'1e-005', N'偏移量', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (31, 3, N'opticalMaskName', N'10GbE_10_3125_May02.msk', N'眼图模板 10GbE_10_3125_May02.msk', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (32, 3, N'DcaAtt', N'1.8', N'衰减补偿', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (33, 3, N'FilterFreq', N'10.3125', N'滤波器速率（10.3125 Gb/s）', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (34, 3, N'Percentage', N'0', N'EMM（0-100）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (35, 3, N'DcaDataRate', N'10312500000', N'数据速率（10312500000）', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (36, 3, N'DcaWavelength', N'2', N'波长选择（1 850,2 1310,3 1550 default 850）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (37, 3, N'DcaThreshold', N'80,50,20', N'门限80,50,20', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (38, 3, N'TriggerBwlimit', N'2', N'触发带宽限制（ 0 HIGH\1 LOW\2 DIV）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (39, 4, N'ElecSwitchChannel', N'1', N'电开关通道数', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (40, 4, N'Name', N'ElectricalSwitch', N'仪器名称（ElectricalSwitch）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (41, 4, N'Addr', N'0', N'USB地址 （0）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (42, 4, N'IOType', N'USB', N'接口类型（GPIB或USB）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (43, 4, N'Reset', N'False', N'是否复位（True 是，False 否）', N'bool')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (44, 5, N'Addr', N'14', N'GPIB地址（1~30）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (45, 5, N'IOType', N'GPIB', N'接口类型（GPIB或USB）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (46, 5, N'Reset', N'False', N'是否复位（True 是，False 否）', N'bool')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (47, 5, N'Name', N'N490xPPG', N'仪器名称（N490XPPG）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (48, 5, N'PRBS', N'31', N'PRBS码型（7,15,23,31..）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (49, 5, N'BertDataRate', N'10312500000', N'误码仪速率', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (50, 5, N'TriggerDRatio', N'16', N'触发比率 2', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (51, 5, N'TriggerMode', N'Custom', N'触发模式 （DCL，PATT）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (52, 5, N'ClockHigVoltage', N'0.5', N'时钟高电平电压值', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (53, 5, N'ClockLowVoltage', N'-0.5', N'时钟低电平电压值', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (54, 5, N'DataHigVoltage', N'0.5', N'数据高电平电压值', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (55, 5, N'DataLowVoltage', N'-0.5', N'数据低电平电压值', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (56, 6, N'Addr', N'5', N'GPIB地址（1~30）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (57, 6, N'IOType', N'GPIB', N'接口类型（GPIB或USB）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (58, 6, N'Reset', N'False', N'是否复位（True 是，False 否）', N'bool')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (59, 6, N'Name', N'N490xED', N'仪器名称（N490XED）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (60, 6, N'PRBS', N'31', N'(7,15,23,31..)', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (61, 6, N'CDRSwitch', N'false', N'CDR 开关', N'bool')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (62, 6, N'CDRFreq', N'10312500000', N'CDR频率', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (63, 7, N'Addr', N'20', N'GPIB地址', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (64, 7, N'IOType', N'GPIB', N'GPIB或USB 接口类型', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (65, 7, N'Reset', N'False', N'是否复位（True 是，False 否）', N'bool')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (66, 7, N'Name', N'AQ2011OpticalSwitch', N'仪器名字AQ2011OpticalSwitch', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (67, 7, N'OpticalSwitchSlot', N'3', N'光开关插槽数', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (68, 7, N'SwitchChannel', N'1', N'光开关所在通道数', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (69, 7, N'ToChannel', N'1', N'要切换的通道数', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (70, 8, N'Addr', N'', N'GPIB地址（1~30）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (71, 8, N'IOType', N'', N'接口类型（GPIB或USB）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (72, 8, N'Reset', N'False', N'是否复位（True 是，False 否）', N'bool')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (73, 8, N'Name', N'', N'仪器名称（AQ2011PowerMeter）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (74, 8, N'PowerMeterSlot', N'', N'功率计插槽数', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (75, 8, N'PowerMeterWavelength', N'', N'功率计波长', N'u32')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (77, 8, N'UnitType', N'', N'功率单位（0 "dBm",1 "W"）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (78, 9, N'Addr', N'23', N'GPIB地址（1~30）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (79, 9, N'IOType', N'GPIB', N'接口类型（GPIB或USB）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (80, 9, N'Reset', N'False', N'是否复位（True 是，False 否）', N'bool')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (81, 9, N'Name', N'TPO4300', N'仪器名称（TPO4300）', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (82, 9, N'FLSE', N'14', N'流量设置', N'u32')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (83, 9, N'ULIM', N'90', N'温度上限', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (84, 9, N'LLIM', N'-20', N'温度上限', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (85, 9, N'Sensor', N'1', N'温度传感器选择（0 No Sensor,1 T,2 k,3 rtd,4 diode ）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (86, 10, N'Addr', N'7', N'地址', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (87, 10, N'IOType', N'GPIB', N'接口类型:GPIB or USB', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (88, 10, N'Reset', N'false', N'是否需要复位设备，false=不需要，true=需要', N'bool')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (89, 10, N'Name', N'FLEX86100', N'仪器名称  FLEX86100', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (90, 10, N'configFilePath', N'1', N'仪器内部配置文件地址', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (91, 10, N'FlexDcaDataRate', N'10312500000', N'示波器速率', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (92, 10, N'FilterSwitch', N'1', N'滤波器开关，1=开，0=关
', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (93, 10, N'FlexFilterFreq', N'10.3125', N'滤波器速率默认为 10.3125 或 25.78125', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (94, 10, N'triggerSource', N'0', N'触发源选择（0=FrontPannel,1=FreeRun)', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (95, 10, N'FlexTriggerBwlimit', N'2', N'触发信号带宽（0=FILTered，1=EDGE，2=CLOCK）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (96, 10, N'opticalSlot', N'1', N'光口所处槽位', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (97, 10, N'elecSlot', N'2', N'电口所处槽位', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (98, 10, N'FlexOptChannel', N'1A,1B,1C,1D', N'光口通道（1A,1B,1C,1D）根据Slot确定', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (99, 10, N'FlexElecChannel', N'1A,1B,1C,1D', N'电口通道（1A,1B,1C,1D）根据Slot确定', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (100, 10, N'FlexDcaWavelength', N'2', N'波长选择波长选择0=850,1=1310,2=1550
', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (101, 10, N'opticalAttSwitch', N'1', N'光口补偿开关', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (102, 10, N'FlexDcaAtt', N'0', N'光口补偿值（单位dB)', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (103, 10, N'erFactor', N'0', N'ER修正值（单位%）', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (104, 10, N'FlexScale', N'300', N'屏幕显示比例(单位uW/div）', N'double')
GO
print 'Processed 100 total records'
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (105, 10, N'FlexOffset', N'300', N'屏幕显示偏移(单位uW)', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (106, 10, N'Threshold', N'0', N'RiseFallTime阈值点（0=80,50,20);(1=90,50,10)', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (107, 10, N'reference', N'0', N'RiseFallTime参考点（0=OneZero，1=TopBase）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (108, 10, N'precisionTimebaseModuleSlot', N'3', N'精准时基单元槽位', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (109, 10, N'precisionTimebaseSynchMethod', N'1', N'精准时基同步方式（0=OLIN，1=FAST）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (110, 10, N'precisionTimebaseRefClk', N'10312500000', N'精准时基单元参考时钟（单位bps）', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (111, 10, N'rapidEyeSwitch', N'1', N'快速眼图模式开关', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (112, 10, N'marginType', N'1', N'模板余量测试自动手动选择（0=手动，1=自动）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (113, 10, N'marginHitType', N'0', N'自动模板余量测试判决方式选择（0=碰撞点数，1=碰撞比例）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (114, 10, N'marginHitRatio', N'5e-006', N'模板余量测试自动碰撞比例', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (115, 10, N'marginHitCount', N'0', N'模板余量测试自动碰撞数', N'int')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (116, 10, N'acqLimitType', N'0', N'眼图累积方式选择（0=wavefors,1=samples)', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (117, 10, N'acqLimitNumber', N'100', N'眼图累积数量', N'int')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (118, 10, N'opticalMaskName', N'c:\scope\masks\10GBE_10_3125_MAY02.MSK', N'光眼图模板名称', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (119, 10, N'elecMaskName', N'c:\scope\masks\10GBE_10_3125_MAY02.MSK', N'电眼图模板名称', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (120, 10, N'opticalEyeSavePath', N'""', N'光眼图保存地址', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (121, 10, N'elecEyeSavePath', N'""', N'电眼图保存地址', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (122, 11, N'Addr', N'1', N'地址', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (123, 11, N'IOType', N'GPIB', N'接口类型:GPIB,usb', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (124, 11, N'Reset', N'false', N'是否需要复位', N'bool')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (125, 11, N'Name', N'MP1800PPG', N'仪器名称:MP1800PPG', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (126, 11, N'dataRate', N'25.78125', N'PPG速率（Gbps)', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (127, 11, N'dataLevelGuardAmpMax', N'1000', N'输出保护幅度最大值（单位mV', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (128, 11, N'dataLevelGuardOffsetMax', N'1000', N'输出保护最大偏移量（单位mV）', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (129, 11, N'dataLevelGuardOffsetMin', N'-1000', N'输出保护最小偏移量（单位mV）', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (130, 11, N'dataLevelGuardSwitch', N'1', N'输出保护开关', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (131, 11, N'dataAmplitude', N'500', N'输出单端幅度（单位mV）', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (132, 11, N'dataCrossPoint', N'50', N'输出数据信号交叉点', N'double')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (133, 11, N'configFilePath', N'""', N'仪器中配置文件的地址', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (134, 11, N'slot', N'1', N'PPG所处槽位', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (135, 11, N'clockSource', N'0', N'PPG码型选择（0=PRBS,1=Zero Subsitution,2=Data,3=Alternate,4=Mixed Data,5=Sequense）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (136, 11, N'auxOutputClkDiv', N'8', N'辅助输出是时钟信号的几分频', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (137, 11, N'prbsLength', N'31', N'PRBS码型长度:31,7', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (138, 11, N'patternType', N'0', N'（0=PRBS,1=Zero Subsitution,2=Data,3=Alternate,4=Mixed Data,5=Sequense）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (139, 11, N'dataSwitch', N'1', N'数据输出开关', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (140, 11, N'dataTrackingSwitch', N'1', N'DATA /DATA跟踪开关', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (141, 11, N'dataAcModeSwitch', N'1', N'输出模式选择(0=DC，1=AC）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (142, 11, N'dataLevelMode', N'0', N'输出电平模式选择（0=VARiable,1=NECL,2=PCML,3=NCML,4=SCFL,5=LVPecl,6=LVDS200,7=LVDS400）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (143, 11, N'clockSwitch', N'1', N'时钟输出开关', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (144, 11, N'outputSwitch', N'1', N'总输出开关', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (145, 12, N'Addr', N'1', N'设备地址', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (146, 12, N'IOType', N'GPIB', N'接口类型,USB', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (147, 12, N'Reset', N'false', N'是否需要复位', N'bool')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (148, 12, N'Name', N'MP1800ED', N'仪器名称', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (149, 12, N'slot', N'3', N'ED所处槽位', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (150, 12, N'totalChannel', N'4', N'ED总通道数', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (151, 12, N'currentChannel', N'1', N'数据输入接口类型', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (152, 12, N'dataInputInterface', N'2', N'数据输入接口类型', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (153, 12, N'prbsLength', N'31', N'PRBS码型长度', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (154, 12, N'errorResultZoom', N'1', N'0=ZoomIn(显示详细误码信息),1=ZoomOut(只显示误码率和误码数)', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (155, 12, N'edGatingMode', N'1', N'累积模式（0=REPeat,1=SINGle,2=UNTimed）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (156, 12, N'edGatingUnit', N'0', N'累积单位（0=TIME,1=CLOCk,2=ERRor,3=BLOCk）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (157, 12, N'edGatingTime', N'5', N'累积数量', N'int')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (158, 10, N'ERFACTORSWITCH', N'1', N'是否启用ER修正因子', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (159, 3, N'WaveformCount', N'700', N'Waveform累计点
', N'int')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (160, 11, N'TotalChannel', N'4', N'MP1800PPG的PPG通道总数', N'Byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (161, 3, N'elecMaskName', N'""', N'电眼图模板 10GbE_10_3125_May02.msk', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (162, 1, N'voltageoffset', N'0', N'voltageoffset', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (163, 1, N'currentoffset', N'0', N'currentoffset', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (176, 8, N'PowerMeterChannel', N'0', N'功率计通道数', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (177, 1, N'opendelay', N'2000', N'Power ON 延迟时间', N'int')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (178, 1, N'closedelay', N'500', N'Power OFF 延迟时间', N'int')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (179, 2, N'opendelay', N'1000', N'opendelay(unit:ms)', N'int')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (180, 2, N'closedelay', N'1000', N'closedelay(unit:ms)', N'int')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (181, 2, N'setattdelay', N'1000', N'setattdelay(unit:ms)', N'int')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (182, 3, N'setscaledelay', N'1000', N'setscaledelay(unit:ms)
', N'int')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (183, 10, N'flexsetscaledelay', N'1000', N'flexsetscaledelay(unit:ms)', N'int')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (184, 6, N'patternfile', N'@"C:\02"', N'patternfile path', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (185, 12, N'patternfile', N'@"C:\02"', N'patternfile path', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (186, 5, N'patternfile', N'@"C:\02"', N'patternfile path', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (187, 11, N'patternfile', N'@"C:\02"', N'patternfile path', N'string')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (190, 13, N'TestEQPrmtr3', N'3', N'TestEQPrmtr3xxx', N'string')
SET IDENTITY_INSERT [dbo].[GlobalAllEquipmentParamterList] OFF
/****** Object:  Table [dbo].[RoleFunctionTable]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleFunctionTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[FunctionID] [int] NOT NULL,
 CONSTRAINT [PK_RoleFunctionTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RoleFunctionTable] ON
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (1, 1, 1)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (2, 1, 2)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (3, 1, 3)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (4, 1, 5)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (5, 1, 6)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (6, 1, 7)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (8, 3, 1)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (9, 3, 2)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (10, 3, 3)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (11, 3, 4)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (12, 3, 5)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (13, 3, 6)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (14, 3, 7)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (15, 5, 1)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (16, 5, 2)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (17, 5, 3)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (18, 5, 4)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (19, 5, 5)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (20, 5, 6)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (21, 5, 7)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (23, 3, 8)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (25, 2, 1)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (27, 5, 8)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (28, 3, 9)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (29, 3, 10)
SET IDENTITY_INSERT [dbo].[RoleFunctionTable] OFF
/****** Object:  StoredProcedure [dbo].[InsertRunRecord]    Script Date: 12/22/2014 17:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--修改存储过程 InsertRunRecord
--
--存储过程 InsertRunRecord
--
--向TopoRunRecordTable添加一条新记录，并返回该记录的ID。
--TopoTestPlanRunRecordTable 字段 ID SN PID StartTime EndTime
--
--参数		方向	类型		描述
--@ID		OUT	Int		新记录ID
--@PID		IN	int		TestPlanID
--@SN		IN	Nvarchar(30)	模块序列号
--@StartTime	IN	Datetime	测试开始时间
--@EndTime	IN	Datetime		测试结束时间	--140721
--@FWRev	IN	Nvarchar(5)		FW版本号
--@IP		IN	Nvarchar(50)	IP
--@LightSource		In	Nvarchar(100)	LightSource	--141219

CREATE PROCEDURE [dbo].[InsertRunRecord]
 @ID int OUTPUT,
 @SN nvarchar(30),  
 @PID int,
 @StartTime datetime,
 @EndTime datetime, --140721
 @FWRev nvarchar(5), --141027 
 @IP nvarchar(50) ,--141027
 @LightSource nvarchar(100) --141219
AS
 
BEGIN
   
SET XACT_ABORT ON 
BEGIN TRANSACTION 
INSERT INTO  TopoRunRecordTable ("SN", "PID","StartTime","EndTime","FWRev","IP","LightSource") 
	VALUES(@SN, @PID,@StartTime,@EndTime,@FWRev,@IP,@LightSource);
set @ID = (Select Ident_Current('TopoRunRecordTable'))
COMMIT TRANSACTION 
SET XACT_ABORT OFF 
print @id
return @ID

END
GO
/****** Object:  StoredProcedure [dbo].[InsertLogRecord]    Script Date: 12/22/2014 17:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--修改存储过程 [InsertMyLogRecord]
--存储过程 [InsertMyLogRecord]
--
--向TopoLogRecord 添加一条新记录，并返回该记录的ID。
--TopoLogRecord  字段 ID PID RunRecordID StartTime EndTime TestLog Temp Voltage Channel Result
--
--参数		方向	类型		描述
--@ID		OUT	Int		新记录ID
--@PID		IN	int		TestControlID
--@RunRecordID	IN	int		RunRecordID
--@StartTime	IN	Datetime	测试开始时间
--@EndTime	IN	Datetime	测试结束时间
--@TestLog 	IN 	ntext		测试Log
--@Temp 	IN 	Real		测试温度
--@Voltage 	IN 	Real		测试电压
--@Channel 	IN 	tinyint		测试通道号
--@Result 	IN	bit 		测试结果

CREATE PROCEDURE [dbo].[InsertLogRecord]
 @ID int OUTPUT,
 @PID int,
 @RunRecordID int, 
 @StartTime datetime,
 @EndTime datetime,
 @TestLog ntext,
 @Temp real,
 @Voltage real,
 @Channel tinyint,
 @Result bit
AS

BEGIN

SET XACT_ABORT ON 
BEGIN TRANSACTION 

INSERT INTO  TopoLogRecord VALUES(@PID,@RunRecordID , @StartTime,@EndTime,@TestLog,@Temp,@Voltage,@Channel,@Result);
set @ID = (Select Ident_Current('TopoLogRecord'))

COMMIT TRANSACTION 
SET XACT_ABORT OFF 

print @id
return @ID
END
GO
/****** Object:  Table [dbo].[GlobalTestModelParamterList]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalTestModelParamterList](
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
	[ItemDescription] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_GlobalTestModelParamterList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalTestModelParamterList] ON
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (1, 1, N'IMODMIN(MA)', N'UInt16', N'input', N'100', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (2, 1, N'ARRAYLISTTXMODCOEF', N'ArrayList', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (3, 1, N'1STOR2STORPIDTXLOP', N'byte', N'input', N'2', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (4, 1, N'1STOR2STORPIDER', N'byte', N'input', N'2', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (5, 1, N'ISOPENLOOPORCLOSELOOPORBOTH', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (6, 1, N'IMODSTART(MA)', N'UInt16', N'input', N'650', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (7, 1, N'IMODSTEP', N'byte', N'input', N'64', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (8, 1, N'IMODMETHOD', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (9, 1, N'TXERTOLERANCE(DB)', N'double', N'input', N'0.2', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (10, 1, N'TXERTARGET(DB)', N'double', N'input', N'4.5', 4, 5, 1, 0, 1, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (11, 1, N'ARRAYLISTTXPOWERCOEF', N'ArrayList', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (12, 1, N'IBIASMETHOD', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (13, 1, N'IBIASSTEP(MA)', N'byte', N'input', N'64', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (14, 1, N'IBIASSTART(MA)', N'UInt16', N'input', N'600', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (15, 1, N'IBIASMIN(MA)', N'UInt16', N'input', N'400', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (16, 1, N'IBIASMAX(MA)', N'UInt16', N'input', N'2500', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (17, 1, N'FIXEDMOD(MA)', N'UInt16', N'input', N'512', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (18, 1, N'TXLOPTOLERANCE(UW)', N'double', N'input', N'100', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (19, 1, N'TXLOPTARGET(UW)', N'double', N'input', N'900', 500, 1500, 1, 0, 1, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (20, 1, N'IMODMAX(MA)', N'UInt16', N'input', N'2500', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (21, 1, N'AUTOTUNE', N'bool', N'input', N'true', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (22, 2, N'FIXEDMODDAC(MA)', N'UInt16', N'input', N'512', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (23, 2, N'ARRAYLISTXDMICOEF', N'ArrayList', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (24, 2, N'1STOR2STORPID', N'byte', N'input', N'2', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (25, 2, N'IBIASADCORTXPOWERADC', N'byte', N'input', N'0', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (26, 2, N'ARRAYIBIAS(MA)', N'ArrayList', N'input', N'750,850,950', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (27, 2, N'AUTOTUNE', N'bool', N'input', N'true', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (28, 2, N'ISTEMPRELATIVE', N'bool', N'input', N'true', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (29, 3, N'AP(DBM)', N'double', N'output', N'-32768', -3.5, 2, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (30, 3, N'JITTERPP(PS)', N'double', N'output', N'-32768', -32768, 32767, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (31, 3, N'TXOMA(mW)', N'double', N'output', N'-32768', -32768, 32767, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (32, 3, N'FALLTIME(PS)', N'double', N'output', N'-32768', -32768, 65, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (33, 3, N'RISETIME(PS)', N'double', N'output', N'-32768', -32768, 100, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (34, 3, N'JITTERRMS(PS)', N'double', N'output', N'-32768', -32768, 30, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (35, 3, N'MASKMARGIN(%)', N'double', N'output', N'-32768', 0, 90, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (36, 3, N'ER(DB)', N'double', N'output', N'-32768', 3.7999999523162842, 5, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (37, 3, N'ISOPTICALEYEORELECEYE', N'bool', N'input', N'true', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (38, 3, N'CROSSING(%)', N'double', N'output', N'-32768', 40, 60, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (39, 4, N'DMITXPOWER(DBM)', N'double', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (40, 4, N'CURRENTTXPOWER(DBM)', N'double', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (41, 4, N'DMITXPOWERERR(DB)', N'double', N'output', N'-32768', -1.5, 1.5, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (42, 5, N'IBIAS(MA)', N'double', N'output', N'-32768', 10, 100, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (43, 6, N'LOSDVOLTAGETUNESTEP(V)', N'byte', N'input', N'32', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (44, 6, N'LOSAVOLTAGESTARTVALUE(V)', N'UInt16', N'input', N'15', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (45, 6, N'IsAdjustLos', N'bool', N'input', N'True', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (46, 6, N'AUTOTUNE', N'bool', N'input', N'true', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (47, 6, N'LOSDVOLTAGEUPERLIMIT(V)', N'UInt16', N'input', N'24135', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (48, 6, N'LOSDVOLTAGESTARTVALUE(V)', N'UInt16', N'input', N'300', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (49, 6, N'LOSDINPUTPOWER', N'double', N'input', N'-20', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (50, 6, N'LOSAVOLTAGETUNESTEP(V)', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (51, 6, N'LOSAVOLTAGEUPERLIMIT(V)', N'UInt16', N'input', N'15', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (52, 6, N'LosValue(V)', N'UINT32', N'input', N'0', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (53, 6, N'LOSDVOLTAGELOWLIMIT(V)', N'UInt16', N'input', N'1', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (54, 6, N'LOSAINPUTPOWER', N'double', N'input', N'-19', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (55, 6, N'LOSAVOLTAGELOWLIMIT(V)', N'UInt16', N'input', N'1', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (56, 7, N'ARRAYLISTDMIRXCOEF', N'ArrayList', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (57, 7, N'1STOR2STORPID', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (58, 7, N'ARRAYLISTRXPOWER(DBM)', N'ArrayList', N'input', N'-7,-10,-13,-16', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (59, 8, N'LOSA', N'double', N'output', N'-32768', -29, -16, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (60, 8, N'LOSD', N'double', N'output', N'-32768', -29, -13, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (61, 8, N'LOSADSTEP', N'double', N'input', N'0.5', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (62, 8, N'LOSDMAX', N'double', N'input', N'-13', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (63, 8, N'LOSAMIN', N'double', N'input', N'-29', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (64, 8, N'LOSAMAX', N'double', N'input', N'-16', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (65, 8, N'LOSH', N'double', N'output', N'-32768', 0.5, 32767, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (66, 9, N'ARRAYLISTRXINPUTPOWER(DBM)', N'ArrayList', N'input', N'-7,-10,-13,-16', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (67, 9, N'DMIRXPWRMAXERRPOINT(DBM)', N'double', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (68, 9, N'DMIRXPWRMAXERR', N'double', N'output', N'-32768', -3, 3, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (69, 10, N'CSENTARGETBER', N'double', N'input', N'1.0E-12', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (70, 10, N'COEFCSENSUBSTEP(DBM)', N'double', N'input', N'0.3', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (71, 10, N'CSEN(DBM)', N'double', N'output', N'-32768', -20, -11, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (72, 10, N'SEARCHTARGETBERSUBSTEP', N'double', N'input', N'0.3', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (73, 10, N'SEARCHTARGETBERADDSTEP', N'double', N'input', N'0.5', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (74, 10, N'SEARCHTARGETBERLL', N'double', N'input', N'3.00E-5', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (75, 10, N'SEARCHTARGETBERUL', N'double', N'input', N'1.00E-4', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (76, 10, N'CSENSTARTINGRXPWR(DBM)', N'double', N'input', N'-20', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (77, 10, N'CSENALIGNRXPWR(DBM)', N'double', N'input', N'-7', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (78, 10, N'COEFCSENADDSTEP(DBM)', N'double', N'input', N'0.3', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (79, 11, N'GENERALVCC(V)', N'double', N'input', N'3.3', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (80, 11, N'ARRAYLISTVCC(V)', N'ArrayList', N'input', N'3.1,3.3,3.5', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (81, 11, N'ARRAYLISTDMIVCCCOEF', N'ArrayList', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (82, 11, N'1STOR2STORPID', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (83, 12, N'1STOR2STORPID', N'byte', N'input', N'1', 0, 0, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (84, 12, N'ARRAYLISTDMITEMPCOEF', N'ArrayList', N'output', N'-32768', 0, 0, 0, 1, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (85, 13, N'DMITEMPERR(C)', N'double', N'output', N'-32768', -3, 3, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (86, 13, N'DMITEMP(C)', N'double', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (87, 14, N'DMIVCCERR(V)', N'double', N'output', N'-32768', -0.20000000298023224, 0.20000000298023224, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (88, 14, N'DMIVCC(V)', N'double', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (89, 15, N'ICC(MA)', N'double', N'output', N'-32768', 100, 2000, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (90, 16, N'AUTOTUNE', N'bool', N'input', N'TRUE', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (91, 16, N'APDCALPOINT(DBM)', N'double', N'input', N'-24', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (92, 16, N'ARRAYLISTAPDBIASPOINTS(V)', N'ArrayList', N'input', N'700,725,750,775,800,825,850,875,900', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (93, 16, N'APDBIASSTEP(V)', N'byte', N'input', N'5', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (94, 16, N'1STOR2STORPID', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (95, 16, N'ARRAYLISTAPDCOEF', N'ArrayList', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (96, 17, N'TEMPHA', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (97, 17, N'TEMPHW', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (98, 17, N'TEMPLA', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (99, 17, N'TEMPLW', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (100, 17, N'VCCHA', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
GO
print 'Processed 100 total records'
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (101, 17, N'VCCHW', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (102, 17, N'VCCLA', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (103, 17, N'VCCLW', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (104, 18, N'ARRAYLISTDMIRXCOEF', N'ArrayList', N'output', N'', -32768, 32767, 0, 1, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (105, 18, N'1STOR2STORPID', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (106, 18, N'ARRAYLISTRXPOWER(DBM)', N'ArrayList', N'input', N'-7,-10,-13,-16', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (107, 19, N'1STOR2STORPID', N'byte', N'input', N'1', 0, 0, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (108, 19, N'ARRAYLISTDMITEMPCOEF', N'ArrayList', N'output', N'-32768', 0, 0, 0, 1, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (109, 20, N'GENERALVCC(V)', N'double', N'input', N'3.3', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (110, 20, N'ARRAYLISTVCC(V)', N'ArrayList', N'input', N'3.1,3.3,3.5', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (111, 20, N'ARRAYLISTDMIVCCCOEF', N'ArrayList', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (112, 20, N'1STOR2STORPID', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (114, 1, N'DCtoDC', N'bool', N'input', N'false', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (115, 1, N'FIXEDCrossDac', N'UInt32', N'input', N'200', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (116, 2, N'DCtoDC', N'bool', N'input', N'false', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (117, 2, N'FIXEDCrossDac', N'UInt32', N'input', N'200', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (120, 8, N'ISLOSDETAIL', N'bool', N'input', N'false', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (121, 1, N'PIDCOEFARRAY', N'ArrayList', N'input', N'-32768', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (122, 10, N'IsBerQuickTest', N'bool', N'input', N'false', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (123, 2, N'HighestCalTemp', N'double', N'input', N'0', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (124, 2, N'LowestCalTemp', N'double', N'input', N'0', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (125, 18, N'HasOffset', N'bool', N'input', N'false', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (126, 7, N'HasOffset', N'bool', N'input', N'false', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (127, 2, N'ISNEWALGORITHM', N'bool', N'input', N'false', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (128, 1, N'FIXEDIBIAS(MA)', N'UInt32', N'input', N'20', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (129, 1, N'FixedIBiasArray', N'ArrayList', N'input', N'280,280,280,280', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (130, 1, N'FixedModArray', N'ArrayList', N'input', N'500,500,500,500', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (132, 22, N'ModelTest1Prmtr1', N'UInt16', N'input', N'1', -32768, 32767, 0, 1, 1, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (133, 22, N'ModelTest1Prmtr2', N'byte', N'input', N'2', 2, 4, 0, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (134, 23, N'EECROSSING(%)', N'double', N'output', N'0', -32768, 32767, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (135, 23, N'EEMASKMARGIN(%)', N'double', N'output', N'0', -32768, 32767, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (136, 23, N'EEJITTERRMS(PS)', N'double', N'output', N'0', -32768, 32767, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (137, 23, N'EERISETIME(PS)', N'double', N'output', N'0', -32768, 32767, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (138, 23, N'EEFALLTIME(PS)', N'double', N'output', N'0', -32768, 32767, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (139, 23, N'EETXAMP(MV)', N'double', N'output', N'0', -32768, 32767, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (140, 23, N'EEJITTERPP(PS)', N'UInt16', N'output', N'0', -32768, 32767, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (141, 17, N'IBIASHA', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (142, 17, N'IBIASHW', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (143, 17, N'IBIASLA', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (144, 17, N'IBIASLW', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (145, 17, N'TXPOWERHA', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (146, 17, N'TXPOWERHW', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (147, 17, N'TXPOWERLA', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (148, 17, N'TXPOWERLW', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (149, 17, N'RXPOWERHA', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (150, 17, N'RXPOWERHW', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (151, 17, N'RXPOWERLA', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (152, 17, N'RXPOWERLW', N'double', N'output', N'0', 1, 1, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (153, 17, N'RXPOWERAWPOINT(DBM)', N'double', N'input', N'-10', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (154, 9, N'DMIRXNOPTICAL(DBM)', N'double', N'output', N'0', -40, -40, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (155, 23, N'EEEyeHight(MV)', N'double', N'output', N'0', -32768, 32767, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (156, 23, N'EEEyeWidth(PS)', N'double', N'output', N'0', -32768, 32767, 1, 0, 0, 1, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (157, 23, N'CENSIPOINT(DBM)', N'double', N'input', N'-10', -32768, 32767, 0, 0, 0, 0, N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (158, 1, N'SLEEPTIME', N'UInt16', N'input', N'200', -32768, 32767, 0, 0, 0, 0, N'SLEEPTIME:200 UNIT(MS)')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (159, 2, N'SLEEPTIME', N'UInt16', N'input', N'200', -32768, 32767, 0, 0, 0, 0, N'SLEEPTIME:200 UNIT(MS)')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (160, 24, N'BER', N'double', N'output', N'0', -32768, 32767, 1, 0, 0, 1, N'输出参数 ：BER  double型  是规格判定值
')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (161, 3, N'TXOMA(DBM)', N'double', N'output', N'0', -32768, 32767, 1, 0, 0, 0, N'TestEye 添加输出参数：TXOMA(DBM) 类型double')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (162, 10, N'CSEN(OMA)', N'double', N'output', N'0', -32768, 32767, 1, 0, 0, 1, N'TestBer添加输出参数：CSEN(OMA) 类型double。')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (163, 8, N'LOSA(OMA)', N'double', N'output', N'0', -32768, 32767, 1, 0, 0, 0, N'LOSA(OMA)、LOSD(OMA)两个输出参数，都是double型 SPEC参数')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (164, 8, N'LOSD(OMA)', N'double', N'output', N'0', -32768, 32767, 1, 0, 0, 1, N'LOSA(OMA)、LOSD(OMA)两个输出参数，都是double型 SPEC参数')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (165, 18, N'READRXADCCOUNT', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0, N'READRXADCCOUNT 类型byte，
默认值填1')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord], [ItemDescription]) VALUES (166, 18, N'SLEEPTIME', N'UInt16', N'input', N'200', -32768, 32767, 0, 0, 0, 0, N'SLEEPTIME默认值50ms')
SET IDENTITY_INSERT [dbo].[GlobalTestModelParamterList] OFF
/****** Object:  Table [dbo].[GlobalProductionName]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalProductionName](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[PN] [nvarchar](35) NOT NULL,
	[ItemName] [nvarchar](35) NOT NULL,
	[Channels] [tinyint] NOT NULL,
	[Voltages] [tinyint] NOT NULL,
	[Tsensors] [tinyint] NOT NULL,
	[MCoefsID] [int] NOT NULL,
	[AuxAttribles] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_GlobalProductionName] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalProductionName] ON
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [AuxAttribles]) VALUES (1, 1, N'TR-QQ13L-N00', N'QSFP LR4', 4, 1, 1, 1, N'OldDriver=1;ApcStyle=0')
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [AuxAttribles]) VALUES (3, 1, N'TR_CGR4_N00', N'CGR4', 4, 1, 1, 4, N'ApcStyle=1')
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [AuxAttribles]) VALUES (4, 3, N'SFP-TEST', N'SFP', 1, 2, 1, 5, N'SFP')
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [AuxAttribles]) VALUES (5, 4, N'XFP-TEST', N'XFP', 1, 1, 1, 1, N'x')
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [AuxAttribles]) VALUES (6, 3, N'SFP-TEST2', N'2', 1, 1, 1, 5, N'X')
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [AuxAttribles]) VALUES (7, 1, N'TR_CGR4_N01', N'NEW_BOX_PCB', 4, 1, 1, 4, N'ApcStyle=1')
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [AuxAttribles]) VALUES (8, 1, N'TR_CSR4_AOC(5002812)', N'AOC', 4, 1, 1, 6, N'ApcStyle=0')
SET IDENTITY_INSERT [dbo].[GlobalProductionName] OFF
/****** Object:  Table [dbo].[GlobalMSAEEPROMInitialize]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalMSAEEPROMInitialize](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemName] [nvarchar](50) NOT NULL,
	[ItemType] [nvarchar](20) NOT NULL,
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
SET IDENTITY_INSERT [dbo].[GlobalMSAEEPROMInitialize] ON
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (2, 1, N'test1', N'QSFP', N'03CA070200000000000000056700014B00000040417269737461204E6574776F726B732007001C73515346502D3430472D554E495620202030316658251C469B000000D85844503133313734303030312020202031343130313520200800007A1003000000000000000000000000000000000000000002F8000000008B451DF3', 117, N'3300F6004B00FB00FFFFFFFFFFFFFFFF908871708C707548FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF7B8701AB621F02A4927C138888B81D4C7B870585621F06F2FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF0000000000000000000000000000000000000000000000000000000000000000', 100, N'02FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 188, N'05FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 63)
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (3, 1, N'test2', N'QSFP', N'0102030405FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 164, N'01CFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 110, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53)
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (4, 1, N'test4', N'QSFP', N'03FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 226, N'0A0B0C0D0EFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 211, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53)
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (6, 4, N'2', N'SFP', N'0304070000000000000000066700010E00000000494E4E4F4C494748542020202020202000447C7F54522D50583133432D5630302020202031412020051E00AF001A000020202020202020202020202020202020313230353034202068F005E3FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 103, N'6400CE005F00D30094706D6090887148AFC803E89C4005DC3DE003B6313804B0312000642710007E000000000000000000000000000000000000000000000000000000003F8000000000000001000000010000000100000001000000000000351B2F81BD426C157400010000000032000040000000400000002232FFFFFFFF00', 141, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53, N'', 0)
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (7, 5, N'test', N'XFP', N'069007000000000000140010637150000000007E494E4E4F4C4947485420202020202020FF447C7F54522D485832325A2D4E303020202020314179DC001846DEAF966600494E424251303130303037312020202031313132303820200860743934200000000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 75, N'06005500E2004B00E7000000000000000000FDE82710EA603A988A861BA76E1E22D00F8D000A0C5A00108CA0753088B87918FFFF0000FFFF000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000', 70, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53, N'', 0)
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (8, 5, N'XFPRev0.2', N'XFP', N'07085F00E7005A00EC00FFFFFFFFFFFFFFFF271000FA232801F43DE80630312D07CB312400642710007D94706D609088714800000000000000000000000000000000000000000000000000000000000000400040BC00000000000000000000001E0300000D61000000017F25000092B800000000000000000000000000000001', 128, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53, N'', 0)
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (29, 3, N'V1A', N'QSFP', N'11CE07000070000000000001FF01020000000040494E4E4F4C494748542020202020202000447C7F54522D46433133542D483030202020203030665807D03CFE0007FCD2202020202020202020202020202020203132303130383031080067D1FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 11, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53, N'5000000046000A000000000000000000908871708C7075480000000000000000000000000000000000000000000000007B8700646E18013C927C138875301D4C7B8706306E1809D00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000', 28)
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (33, 1, N'test5', N'QSFP', N'463423FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 79, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53)
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (45, 3, N'test1', N'QSFP', N'03CA070200000000000000056700014B00000040417269737461204E6574776F726B732007001C73515346502D3430472D554E495620202030316658251C469B000000D85844503133313734303030312020202031343130313520200800007A1003000000000000000000000000000000000000000002F8000000008B451DF3', 117, N'3300F6004B00FB00FFFFFFFFFFFFFFFF908871708C707548FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF7B8701AB621F02A4927C138888B81D4C7B870585621F06F2FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF0000000000000000000000000000000000000000000000000000000000000000', 100, N'02FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 188, N'05FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 63)
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (46, 1, N'test6', N'QSFP', N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53)
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (47, 1, N'test8', N'QSFP', N'02FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 188, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53, N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', 53)
SET IDENTITY_INSERT [dbo].[GlobalMSAEEPROMInitialize] OFF
/****** Object:  Table [dbo].[GlobalManufactureChipsetInitialize]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalManufactureChipsetInitialize](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[DriveType] [tinyint] NOT NULL,
	[ChipLine] [tinyint] NOT NULL,
	[RegisterAddress] [int] NOT NULL,
	[Length] [tinyint] NOT NULL,
	[ItemValue] [int] NOT NULL,
	[Endianness] [bit] NOT NULL,
 CONSTRAINT [PK_ManufactureChipsetInitialize_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalManufactureChipsetInitialize] ON
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (6, 3, 3, 1, 8, 1, 8, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (7, 3, 3, 1, 9, 1, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (8, 3, 3, 1, 264, 1, 8, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (9, 3, 3, 1, 265, 1, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (10, 3, 3, 1, 520, 1, 8, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (11, 3, 3, 1, 521, 1, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (12, 3, 3, 1, 776, 1, 8, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (13, 3, 3, 1, 777, 1, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (15, 4, 1, 2, 2, 2, 2222, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (16, 6, 0, 2, 2, 2, 2, 0)
SET IDENTITY_INSERT [dbo].[GlobalManufactureChipsetInitialize] OFF
/****** Object:  Table [dbo].[GlobalManufactureChipsetControl]    Script Date: 12/22/2014 17:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalManufactureChipsetControl](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemName] [nvarchar](20) NOT NULL,
	[ModuleLine] [tinyint] NOT NULL,
	[ChipLine] [tinyint] NOT NULL,
	[DriveType] [tinyint] NOT NULL,
	[RegisterAddress] [int] NOT NULL,
	[Length] [int] NOT NULL,
	[Endianness] [bit] NOT NULL,
 CONSTRAINT [PK_ManufactureChipsetInitialize] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalManufactureChipsetControl] ON
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (3, 3, N'BiasDac', 1, 1, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (4, 3, N'BiasDac', 2, 2, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (5, 3, N'BiasDac', 3, 1, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (6, 3, N'BiasDac', 4, 2, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (7, 3, N'ModDac', 1, 1, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (8, 3, N'ModDac', 2, 2, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (9, 3, N'ModDac', 3, 1, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (10, 3, N'ModDac', 4, 2, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (11, 1, N'BiasDac', 1, 1, 0, 31, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (12, 1, N'ModDac', 1, 1, 0, 29, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (13, 1, N'LOSDac', 1, 2, 0, 48, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (14, 1, N'BiasDac', 2, 2, 0, 31, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (15, 1, N'ModDac', 2, 2, 0, 29, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (16, 1, N'LosDac', 2, 4, 0, 48, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (17, 1, N'BiasDac', 3, 3, 0, 31, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (18, 1, N'ModDac', 3, 3, 0, 29, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (19, 1, N'LosDac', 3, 3, 0, 48, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (20, 1, N'BiasDac', 4, 4, 0, 31, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (21, 1, N'ModDac', 4, 4, 0, 29, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (22, 1, N'LosDac', 4, 1, 0, 48, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (23, 3, N'LOSDAC', 1, 2, 3, 769, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (24, 3, N'LOSDAC', 2, 2, 3, 513, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (25, 3, N'LOSDAC', 3, 2, 3, 257, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (26, 3, N'LOSDAC', 4, 2, 3, 1, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (28, 4, N'ModDac', 1, 1, 0, 1, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (29, 6, N'SFP2', 1, 1, 0, 1, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (30, 7, N'BiasDac', 1, 1, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (31, 7, N'BiasDac', 2, 2, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (32, 7, N'BiasDac', 3, 3, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (33, 7, N'BiasDac', 4, 4, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (34, 7, N'ModDac', 1, 1, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (35, 7, N'ModDac', 2, 2, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (36, 7, N'ModDac', 3, 3, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (37, 7, N'ModDac', 4, 4, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (38, 7, N'LOSDac', 1, 2, 3, 769, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (39, 7, N'LOSDac', 2, 2, 3, 513, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (40, 7, N'LOSDac', 3, 2, 3, 257, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (41, 7, N'LOSDac', 4, 2, 3, 1, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (42, 8, N'BiasDac', 1, 1, 0, 16, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (43, 8, N'BiasDac', 2, 1, 0, 19, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (44, 8, N'BiasDac', 3, 1, 0, 22, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (45, 8, N'BiasDac', 4, 1, 0, 25, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (46, 8, N'ModDac', 1, 1, 0, 17, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (47, 8, N'ModDac', 2, 1, 0, 20, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (48, 8, N'ModDac', 3, 1, 0, 23, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (49, 8, N'ModDac', 4, 1, 0, 26, 1, 0)
SET IDENTITY_INSERT [dbo].[GlobalManufactureChipsetControl] OFF
/****** Object:  Default [DF__FunctionT__Title__5EFF0ABF]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[FunctionTable] ADD  CONSTRAINT [DF__FunctionT__Title__5EFF0ABF]  DEFAULT ('') FOR [Title]
GO
/****** Object:  Default [DF__FunctionT__Funct__5FF32EF8]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[FunctionTable] ADD  CONSTRAINT [DF__FunctionT__Funct__5FF32EF8]  DEFAULT ('0') FOR [FunctionCode]
GO
/****** Object:  Default [DF__FunctionT__Remar__60E75331]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[FunctionTable] ADD  CONSTRAINT [DF__FunctionT__Remar__60E75331]  DEFAULT ('') FOR [Remarks]
GO
/****** Object:  Default [DF_GlobalAllAppModelList_Name]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalAllAppModelList] ADD  CONSTRAINT [DF_GlobalAllAppModelList_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalAllAppModelList_Description]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalAllAppModelList] ADD  CONSTRAINT [DF_GlobalAllAppModelList_Description]  DEFAULT ('') FOR [ItemDescription]
GO
/****** Object:  Default [DF_GlobalAllEquipmentList_Name]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentList] ADD  CONSTRAINT [DF_GlobalAllEquipmentList_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalAllEquipmentList_Type]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentList] ADD  CONSTRAINT [DF_GlobalAllEquipmentList_Type]  DEFAULT ('') FOR [ItemType]
GO
/****** Object:  Default [DF_GlobalAllEquipmentList_Description]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentList] ADD  CONSTRAINT [DF_GlobalAllEquipmentList_Description]  DEFAULT ('') FOR [ItemDescription]
GO
/****** Object:  Default [DF_GlobalAllEquipmentParamterList_FieldName]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] ADD  CONSTRAINT [DF_GlobalAllEquipmentParamterList_FieldName]  DEFAULT ('') FOR [Item]
GO
/****** Object:  Default [DF_GlobalAllEquipmentParamterList_DefaultValue]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] ADD  CONSTRAINT [DF_GlobalAllEquipmentParamterList_DefaultValue]  DEFAULT ('') FOR [ItemValue]
GO
/****** Object:  Default [DF_GlobalAllEquipmentParamterList_Description]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] ADD  CONSTRAINT [DF_GlobalAllEquipmentParamterList_Description]  DEFAULT ('') FOR [ItemDescription]
GO
/****** Object:  Default [DF_GlobalAllEquipmentParamterList_TypeofValue]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] ADD  CONSTRAINT [DF_GlobalAllEquipmentParamterList_TypeofValue]  DEFAULT ('') FOR [ItemType]
GO
/****** Object:  Default [DF_GlobalAllTestModelList_Name]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalAllTestModelList] ADD  CONSTRAINT [DF_GlobalAllTestModelList_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalAllTestModelList_Description]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalAllTestModelList] ADD  CONSTRAINT [DF_GlobalAllTestModelList_Description]  DEFAULT ('') FOR [ItemDescription]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_PID]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_ItemName]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_ItemName]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_ModuleLine]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_ModuleLine]  DEFAULT ((0)) FOR [ModuleLine]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_ChipLine]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_ChipLine]  DEFAULT ((0)) FOR [ChipLine]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_DriveType]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_DriveType]  DEFAULT ((0)) FOR [DriveType]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_RegisterAddress]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_RegisterAddress]  DEFAULT ((0)) FOR [RegisterAddress]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_Length]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_Length]  DEFAULT ((1)) FOR [Length]
GO
/****** Object:  Default [DF_GlobalManufactureChipsetControl_Endianness]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_GlobalManufactureChipsetControl_Endianness]  DEFAULT ('false') FOR [Endianness]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_PID_1]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_PID_1]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_DriveType_1]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_DriveType_1]  DEFAULT ((0)) FOR [DriveType]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_ChipLine_1]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_ChipLine_1]  DEFAULT ((0)) FOR [ChipLine]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_RegisterAddress_1]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_RegisterAddress_1]  DEFAULT ((0)) FOR [RegisterAddress]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_Length_1]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_Length_1]  DEFAULT ((0)) FOR [Length]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_ItemVaule]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_ItemVaule]  DEFAULT ((0)) FOR [ItemValue]
GO
/****** Object:  Default [DF_GlobalManufactureChipsetInitialize_Endianness]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] ADD  CONSTRAINT [DF_GlobalManufactureChipsetInitialize_Endianness]  DEFAULT ('false') FOR [Endianness]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_PID]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_TYPE]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_TYPE]  DEFAULT ('') FOR [ItemTYPE]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_Name]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_Channel]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_Channel]  DEFAULT ('0') FOR [Channel]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_Page]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_Page]  DEFAULT ('0') FOR [Page]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_StartAddress]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_StartAddress]  DEFAULT ('0') FOR [StartAddress]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_Length]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_Length]  DEFAULT ('0') FOR [Length]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_Format]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_Format]  DEFAULT ('') FOR [Format]
GO
/****** Object:  Default [DF_GlobalManufactureMemoryGroupTable_Name]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficientsGroup] ADD  CONSTRAINT [DF_GlobalManufactureMemoryGroupTable_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalMSA_Name]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSA] ADD  CONSTRAINT [DF_GlobalMSA_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalMSA_AccessInterface]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSA] ADD  CONSTRAINT [DF_GlobalMSA_AccessInterface]  DEFAULT ('') FOR [AccessInterface]
GO
/****** Object:  Default [DF_GlobalMSA_SlaveAddress]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSA] ADD  CONSTRAINT [DF_GlobalMSA_SlaveAddress]  DEFAULT ((0)) FOR [SlaveAddress]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_PID]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_FieldName]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_FieldName]  DEFAULT ('') FOR [FieldName]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_Channel]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_Channel]  DEFAULT ('0') FOR [Channel]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_SlaveAddress]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_SlaveAddress]  DEFAULT ('0') FOR [SlaveAddress]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_Page]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_Page]  DEFAULT ('0') FOR [Page]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_StartAddress]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_StartAddress]  DEFAULT ('0') FOR [StartAddress]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_Length]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_Length]  DEFAULT ('0') FOR [Length]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_Format]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_Format]  DEFAULT ('') FOR [Format]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_PID]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_ItemType1]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_ItemType1]  DEFAULT ((0)) FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_SlaveAddress]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_SlaveAddress]  DEFAULT ((0)) FOR [ItemType]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Page]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Page]  DEFAULT ((0)) FOR [Data0]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Address]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Address]  DEFAULT ((0)) FOR [CRCData0]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Length]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Length]  DEFAULT ((1)) FOR [Data1]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_ItemValue]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_ItemValue]  DEFAULT ((0)) FOR [CRCData1]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Data01]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data01]  DEFAULT ((0)) FOR [Data2]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Data02]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data02]  DEFAULT ((0)) FOR [CRCData2]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Data03]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data03]  DEFAULT ((0)) FOR [Data3]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Data04]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data04]  DEFAULT ((0)) FOR [CRCData3]
GO
/****** Object:  Default [DF_GlobalProductionName_PID]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_GlobalProductionName_PN]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_PN]  DEFAULT ('') FOR [PN]
GO
/****** Object:  Default [DF_GlobalProductionName_Name]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalProductionName_Channels]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_Channels]  DEFAULT ('4') FOR [Channels]
GO
/****** Object:  Default [DF_GlobalProductionName_Voltages]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_Voltages]  DEFAULT ((0)) FOR [Voltages]
GO
/****** Object:  Default [DF_GlobalProductionName_Tsensors]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_Tsensors]  DEFAULT ((0)) FOR [Tsensors]
GO
/****** Object:  Default [DF_GlobalProductionName_MGroupID]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_MGroupID]  DEFAULT ('0') FOR [MCoefsID]
GO
/****** Object:  Default [DF_GlobalProductionName_AuxAttribles]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_AuxAttribles]  DEFAULT ('') FOR [AuxAttribles]
GO
/****** Object:  Default [DF_GlobalProductionType_Name]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalProductionType] ADD  CONSTRAINT [DF_GlobalProductionType_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalProductionType_MSAID]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalProductionType] ADD  CONSTRAINT [DF_GlobalProductionType_MSAID]  DEFAULT ('0') FOR [MSAID]
GO
/****** Object:  Default [DF_GlobalTestModelParamterList_ItemValue]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList] ADD  CONSTRAINT [DF_GlobalTestModelParamterList_ItemValue]  DEFAULT ('-32768') FOR [ItemValue]
GO
/****** Object:  Default [DF_GlobalTestModelParamterList_DefaultLowLimit]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList] ADD  CONSTRAINT [DF_GlobalTestModelParamterList_DefaultLowLimit]  DEFAULT ('-32768') FOR [SpecMin]
GO
/****** Object:  Default [DF_GlobalTestModelParamterList_DefaultUpperLimit]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList] ADD  CONSTRAINT [DF_GlobalTestModelParamterList_DefaultUpperLimit]  DEFAULT ('32767') FOR [SpecMax]
GO
/****** Object:  Default [DF_GlobalTestModelParamterList_ItemSpecific]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList] ADD  CONSTRAINT [DF_GlobalTestModelParamterList_ItemSpecific]  DEFAULT ('0') FOR [ItemSpecific]
GO
/****** Object:  Default [DF_GlobalTestModelParamterList_LogRecord]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList] ADD  CONSTRAINT [DF_GlobalTestModelParamterList_LogRecord]  DEFAULT ('0') FOR [LogRecord]
GO
/****** Object:  Default [DF_GlobalTestModelParamterList_Failbreak]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList] ADD  CONSTRAINT [DF_GlobalTestModelParamterList_Failbreak]  DEFAULT ('0') FOR [Failbreak]
GO
/****** Object:  Default [DF_GlobalTestModelParamterList_DataRecord]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList] ADD  CONSTRAINT [DF_GlobalTestModelParamterList_DataRecord]  DEFAULT ('0') FOR [DataRecord]
GO
/****** Object:  Default [DF_GlobalTestModelParamterList_ItemDescription]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList] ADD  CONSTRAINT [DF_GlobalTestModelParamterList_ItemDescription]  DEFAULT ('') FOR [ItemDescription]
GO
/****** Object:  Default [DF__RoleFunct__RoleI__63C3BFDC]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[RoleFunctionTable] ADD  DEFAULT ('0') FOR [RoleID]
GO
/****** Object:  Default [DF__RoleFunct__Funct__64B7E415]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[RoleFunctionTable] ADD  DEFAULT ('0') FOR [FunctionID]
GO
/****** Object:  Default [DF__RolesTabl__RoleN__5B2E79DB]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[RolesTable] ADD  CONSTRAINT [DF__RolesTabl__RoleN__5B2E79DB]  DEFAULT ('') FOR [RoleName]
GO
/****** Object:  Default [DF__RolesTabl__Remar__5C229E14]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[RolesTable] ADD  CONSTRAINT [DF__RolesTabl__Remar__5C229E14]  DEFAULT ('') FOR [Remarks]
GO
/****** Object:  Default [DF_UserInfo_TrueName]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_TrueName]  DEFAULT ('') FOR [TrueName]
GO
/****** Object:  Default [DF_UserInfo_CreatTime]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_CreatTime]  DEFAULT (getdate()) FOR [lastLoginONTime]
GO
/****** Object:  Default [DF_UserInfo_lastComputerName]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_lastComputerName]  DEFAULT ('') FOR [lastComputerName]
GO
/****** Object:  Default [DF_UserInfo_lastLoginOffTime]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_lastLoginOffTime]  DEFAULT ('2000/1/1 12:00:00') FOR [lastLoginOffTime]
GO
/****** Object:  Default [DF_UserInfo_lastIP]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_lastIP]  DEFAULT ('0.0.0.0') FOR [lastIP]
GO
/****** Object:  Default [DF_UserInfo_Remarks]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_Remarks]  DEFAULT ('') FOR [Remarks]
GO
/****** Object:  Default [DF__UserRoleT__UserI__679450C0]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[UserRoleTable] ADD  CONSTRAINT [DF__UserRoleT__UserI__679450C0]  DEFAULT ('0') FOR [UserID]
GO
/****** Object:  Default [DF__UserRoleT__RoleI__688874F9]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[UserRoleTable] ADD  CONSTRAINT [DF__UserRoleT__RoleI__688874F9]  DEFAULT ('0') FOR [RoleID]
GO
/****** Object:  ForeignKey [FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList]  WITH CHECK ADD  CONSTRAINT [FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalAllEquipmentList] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] CHECK CONSTRAINT [FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList]
GO
/****** Object:  ForeignKey [FK_GlobalAllTestModelList_GlobalAllAppModelList]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalAllTestModelList]  WITH CHECK ADD  CONSTRAINT [FK_GlobalAllTestModelList_GlobalAllAppModelList] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalAllAppModelList] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalAllTestModelList] CHECK CONSTRAINT [FK_GlobalAllTestModelList_GlobalAllAppModelList]
GO
/****** Object:  ForeignKey [FK_GlobalManufactureChipsetControl_GlobalProductionName]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl]  WITH CHECK ADD  CONSTRAINT [FK_GlobalManufactureChipsetControl_GlobalProductionName] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionName] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] CHECK CONSTRAINT [FK_GlobalManufactureChipsetControl_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_GlobalManufactureChipsetInitialize_GlobalProductionName]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize]  WITH CHECK ADD  CONSTRAINT [FK_GlobalManufactureChipsetInitialize_GlobalProductionName] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionName] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] CHECK CONSTRAINT [FK_GlobalManufactureChipsetInitialize_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients]  WITH CHECK ADD  CONSTRAINT [FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalManufactureCoefficientsGroup] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalManufactureCoefficients] CHECK CONSTRAINT [FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable]
GO
/****** Object:  ForeignKey [FK_GlobalMSADefintionInf_GlobalMSA]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf]  WITH CHECK ADD  CONSTRAINT [FK_GlobalMSADefintionInf_GlobalMSA] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalMSA] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[GlobalMSADefintionInf] CHECK CONSTRAINT [FK_GlobalMSADefintionInf_GlobalMSA]
GO
/****** Object:  ForeignKey [FK_GlobalMSAEEPROMInitialize_GlobalProductionName]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize]  WITH CHECK ADD  CONSTRAINT [FK_GlobalMSAEEPROMInitialize_GlobalProductionName] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionName] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] CHECK CONSTRAINT [FK_GlobalMSAEEPROMInitialize_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_GlobalProductionName_GlobalManufactureCoefficientsGroup]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalProductionName]  WITH CHECK ADD  CONSTRAINT [FK_GlobalProductionName_GlobalManufactureCoefficientsGroup] FOREIGN KEY([MCoefsID])
REFERENCES [dbo].[GlobalManufactureCoefficientsGroup] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalProductionName] CHECK CONSTRAINT [FK_GlobalProductionName_GlobalManufactureCoefficientsGroup]
GO
/****** Object:  ForeignKey [FK_GlobalProductionName_GlobalProductionType]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalProductionName]  WITH CHECK ADD  CONSTRAINT [FK_GlobalProductionName_GlobalProductionType] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionType] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalProductionName] CHECK CONSTRAINT [FK_GlobalProductionName_GlobalProductionType]
GO
/****** Object:  ForeignKey [FK_GlobalProductionType_GlobalMSA]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalProductionType]  WITH CHECK ADD  CONSTRAINT [FK_GlobalProductionType_GlobalMSA] FOREIGN KEY([MSAID])
REFERENCES [dbo].[GlobalMSA] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[GlobalProductionType] CHECK CONSTRAINT [FK_GlobalProductionType_GlobalMSA]
GO
/****** Object:  ForeignKey [FK_GlobalTestModelParamterList_GlobalAllTestModelList]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList]  WITH CHECK ADD  CONSTRAINT [FK_GlobalTestModelParamterList_GlobalAllTestModelList] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalAllTestModelList] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalTestModelParamterList] CHECK CONSTRAINT [FK_GlobalTestModelParamterList_GlobalAllTestModelList]
GO
/****** Object:  ForeignKey [FK_RoleFunctionTable_FunctionTable]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[RoleFunctionTable]  WITH CHECK ADD  CONSTRAINT [FK_RoleFunctionTable_FunctionTable] FOREIGN KEY([FunctionID])
REFERENCES [dbo].[FunctionTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleFunctionTable] CHECK CONSTRAINT [FK_RoleFunctionTable_FunctionTable]
GO
/****** Object:  ForeignKey [FK_RoleFunctionTable_RolesTable]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[RoleFunctionTable]  WITH CHECK ADD  CONSTRAINT [FK_RoleFunctionTable_RolesTable] FOREIGN KEY([RoleID])
REFERENCES [dbo].[RolesTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleFunctionTable] CHECK CONSTRAINT [FK_RoleFunctionTable_RolesTable]
GO
/****** Object:  ForeignKey [FK_UserRoleTable_RolesTable]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[UserRoleTable]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleTable_RolesTable] FOREIGN KEY([RoleID])
REFERENCES [dbo].[RolesTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoleTable] CHECK CONSTRAINT [FK_UserRoleTable_RolesTable]
GO
/****** Object:  ForeignKey [FK_UserRoleTable_UserInfo]    Script Date: 12/22/2014 17:37:58 ******/
ALTER TABLE [dbo].[UserRoleTable]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleTable_UserInfo] FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoleTable] CHECK CONSTRAINT [FK_UserRoleTable_UserInfo]
GO
