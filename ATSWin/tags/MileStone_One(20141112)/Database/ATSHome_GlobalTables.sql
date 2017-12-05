USE [ATSHome]
GO
/****** Object:  User [ATSUser]    Script Date: 11/12/2014 09:06:50 ******/
CREATE USER [ATSUser] FOR LOGIN [ATSUser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [BackGround]    Script Date: 11/12/2014 09:06:50 ******/
CREATE USER [BackGround] FOR LOGIN [ATSBackGround] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [maintainUser]    Script Date: 11/12/2014 09:06:50 ******/
CREATE USER [maintainUser] FOR LOGIN [ATSMaintainUser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[RolesTable]    Script Date: 11/12/2014 09:06:50 ******/
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
/****** Object:  Table [dbo].[UserLoginInfo]    Script Date: 11/12/2014 09:06:50 ******/
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
SET IDENTITY_INSERT [dbo].[UserLoginInfo] ON
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (1, N'test', CAST(0x00008EAC00C5C100 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'', N'haha', N'eeee')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (2, N'', CAST(0x0000A3CC0099CB7C AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (3, N'', CAST(0x0000A3CC009BDC00 AS DateTime), CAST(0x0000A3CC009BE68C AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'=======================User:terry.yin于2014/10/22 9:27:37修改==========================
 **********************表[TopoTestPlan]修改如下********************** 
**********************表[TopoTestControl]修改如下********************** 
**********************表[TopoTestModel]修改如下********************** 
**********************表[TopoTestParameter]修改如下********************** 
**********************表[TopoEquipment]修改如下********************** 
**********************表[TopoEquipmentParameter]修改如下********************** 
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (4, N'', CAST(0x0000A3CC00A7BE30 AS DateTime), CAST(0x0000A3CC00A7C2E0 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (5, N'terry.yin', CAST(0x0000A3CC00A8B8F8 AS DateTime), CAST(0x0000A3CC00A8E0D0 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'=======================User:terry.yin于2014/10/22 10:14:49修改==========================
 **********************表[TopoTestPlan]修改如下********************** 
**********************表[TopoTestControl]修改如下********************** 
**********************表[TopoTestModel]修改如下********************** 
**********************表[TopoTestParameter]修改如下********************** 
**********************表[TopoEquipment]修改如下********************** 
**********************表[TopoEquipmentParameter]修改如下********************** 
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (6, N'terry.yin', CAST(0x0000A3CC00A94DCC AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'=======================User:terry.yin于2014/10/22 10:16:49修改==========================
 **********************表[TopoTestPlan]修改如下********************** 
**********************表[TopoTestControl]修改如下********************** 
**********************表[TopoTestModel]修改如下********************** 
**********************表[TopoTestParameter]修改如下********************** 
**********************表[TopoEquipment]修改如下********************** 
**********************表[TopoEquipmentParameter]修改如下********************** 
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (7, N'terry.yin', CAST(0x0000A3CC00ADA00C AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (8, N'terry.yin', CAST(0x0000A3CC00AEAC68 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (9, N'terry.yin', CAST(0x0000A3CC00B1993C AS DateTime), CAST(0x0000A3CC00B29404 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:terry.yin于2014/10/22 10:47:18修改==
 **表[TopoTestPlan]修改如下** 
修改前资料为: ID:19 ;PID:1 ;ItemName:1 ;SWVersion:1 ;HwVersion:1 ;USBPort:1 ;AuxAttribles:1 ;
修改后资料为: ID:19 ;PID:1 ;ItemName:terryTEST ;SWVersion:1 ;HwVersion:1 ;USBPort:1 ;AuxAttribles:1 ;
**表[TopoTestControl]修改如下** 
**表[TopoTestModel]修改如下** 
**表[TopoTestParameter]修改如下** 
**表[TopoEquipment]修改如下** 
已经删除资料: ID:124 ;PID:19 ;ItemType:Attennuator ;ItemName:AQ2211Atten_124_Attennuator ;
新增一笔资料为: ID:126 ;PID:19 ;ItemType:Scope ;ItemName:D86100_126_Scope ;
**表[TopoEquipmentParameter]修改如下** 
已经删除资料: ID:1751 ;PID:124 ;Item:Addr ;ItemValue:6 ;
已经删除资料: ID:1752 ;PID:124 ;Item:IOType ;ItemValue:GPIB ;
已经删除资料: ID:1753 ;PID:124 ;Item:Reset ;ItemValue:False ;
已经删除资料: ID:1754 ;PID:124 ;Item:Name ;ItemValue:AQ2211Atten ;
已经删除资料: ID:1755 ;PID:124 ;Item:TOTALCHANNEL ;ItemValue:4 ;
已经删除资料: ID:1756 ;PID:124 ;Item:AttValue ;ItemValue:20 ;
已经删除资料: ID:1757 ;PID:124 ;Item:AttSlot ;ItemValue:1 ;
已经删除资料: ID:1758 ;PID:124 ;Item:WAVELENGTH ;ItemValue:1270,1290,1310,1330 ;
已经删除资料: ID:1759 ;PID:124 ;Item:AttChannel ;ItemValue:1 ;
新增一笔资料为: ID:1772 ;PID:126 ;Item:Addr ;ItemValue:15 ;
新增一笔资料为: ID:1773 ;PID:126 ;Item:IOType ;ItemValue:GPIB ;
新增一笔资料为: ID:1774 ;PID:126 ;Item:Reset ;ItemValue:False ;
新增一笔资料为: ID:1775 ;PID:126 ;Item:Name ;ItemValue:D86100 ;
新增一笔资料为: ID:1776 ;PID:126 ;Item:OptChannel ;ItemValue:1 ;
新增一笔资料为: ID:1777 ;PID:126 ;Item:ElecChannel ;ItemValue:2 ;
新增一笔资料为: ID:1778 ;PID:126 ;Item:Scale ;ItemValue:0.00095 ;
新增一笔资料为: ID:1779 ;PID:126 ;Item:Offset ;ItemValue:1e-005 ;
新增一笔资料为: ID:1780 ;PID:126 ;Item:opticalMaskName ;ItemValue:10GbE_10_3125_May02.msk ;
新增一笔资料为: ID:1781 ;PID:126 ;Item:DcaAtt ;ItemValue:1.8 ;
新增一笔资料为: ID:1782 ;PID:126 ;Item:FilterFreq ;ItemValue:10.3125 ;
新增一笔资料为: ID:1783 ;PID:126 ;Item:Percentage ;ItemValue:0 ;
新增一笔资料为: ID:1784 ;PID:126 ;Item:DcaDataRate ;ItemValue:10312500000 ;
新增一笔资料为: ID:1785 ;PID:126 ;Item:DcaWavelength ;ItemValue:2 ;
新增一笔资料为: ID:1786 ;PID:126 ;Item:DcaThreshold ;ItemValue:80,50,20 ;
新增一笔资料为: ID:1787 ;PID:126 ;Item:TriggerBwlimit ;ItemValue:2 ;
新增一笔资料为: ID:1788 ;PID:126 ;Item:WaveformCount ;ItemValue:700 ;
新增一笔资料为: ID:1789 ;PID:126 ;Item:elecMaskName ;ItemValue:"" ;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (10, N'terry.yin', CAST(0x0000A3CC00B4BBF8 AS DateTime), CAST(0x0000A3CC00B506F8 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:terry.yin于2014/10/22 10:58:17修改==
 **表[TopoTestPlan]修改如下** 
**表[TopoTestControl]修改如下** 
**表[TopoTestModel]修改如下** 
**表[TopoTestParameter]修改如下** 
**表[TopoEquipment]修改如下** 
已经删除资料: ID:126 ;PID:19 ;ItemType:Scope ;ItemName:D86100_126_Scope ;
新增一笔资料为: ID:127 ;PID:19 ;ItemType:ErrorDetector ;ItemName:N490XED_127_ErrorDetector ;
**表[TopoEquipmentParameter]修改如下** 
已经删除资料: ID:1772 ;PID:126 ;Item:Addr ;ItemValue:15 ;
已经删除资料: ID:1773 ;PID:126 ;Item:IOType ;ItemValue:GPIB ;
已经删除资料: ID:1774 ;PID:126 ;Item:Reset ;ItemValue:False ;
已经删除资料: ID:1775 ;PID:126 ;Item:Name ;ItemValue:D86100 ;
已经删除资料: ID:1776 ;PID:126 ;Item:OptChannel ;ItemValue:1 ;
已经删除资料: ID:1777 ;PID:126 ;Item:ElecChannel ;ItemValue:2 ;
已经删除资料: ID:1778 ;PID:126 ;Item:Scale ;ItemValue:0.00095 ;
已经删除资料: ID:1779 ;PID:126 ;Item:Offset ;ItemValue:1e-005 ;
已经删除资料: ID:1780 ;PID:126 ;Item:opticalMaskName ;ItemValue:10GbE_10_3125_May02.msk ;
已经删除资料: ID:1781 ;PID:126 ;Item:DcaAtt ;ItemValue:1.8 ;
已经删除资料: ID:1782 ;PID:126 ;Item:FilterFreq ;ItemValue:10.3125 ;
已经删除资料: ID:1783 ;PID:126 ;Item:Percentage ;ItemValue:0 ;
已经删除资料: ID:1784 ;PID:126 ;Item:DcaDataRate ;ItemValue:10312500000 ;
已经删除资料: ID:1785 ;PID:126 ;Item:DcaWavelength ;ItemValue:2 ;
已经删除资料: ID:1786 ;PID:126 ;Item:DcaThreshold ;ItemValue:80,50,20 ;
已经删除资料: ID:1787 ;PID:126 ;Item:TriggerBwlimit ;ItemValue:2 ;
已经删除资料: ID:1788 ;PID:126 ;Item:WaveformCount ;ItemValue:700 ;
已经删除资料: ID:1789 ;PID:126 ;Item:elecMaskName ;ItemValue:"" ;
新增一笔资料为: ID:1790 ;PID:127 ;Item:Addr ;ItemValue:5 ;
新增一笔资料为: ID:1791 ;PID:127 ;Item:IOType ;ItemValue:GPIB ;
新增一笔资料为: ID:1792 ;PID:127 ;Item:Reset ;ItemValue:False ;
新增一笔资料为: ID:1793 ;PID:127 ;Item:Name ;ItemValue:N490xED ;
新增一笔资料为: ID:1794 ;PID:127 ;Item:PRBS ;ItemValue:31 ;
新增一笔资料为: ID:1795 ;PID:127 ;Item:CDRSwitch ;ItemValue:false ;
新增一笔资料为: ID:1796 ;PID:127 ;Item:CDRFreq ;ItemValue:10312500000 ;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (11, N'terry.yin', CAST(0x0000A3CC00C96378 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'==User:terry.yin于2014/10/22 12:13:44修改==
 **表[TopoTestPlan]修改如下** 
**表[TopoTestControl]修改如下** 
**表[TopoTestModel]修改如下** 
新增一笔资料为: ID:338 ;PID:71 ;ItemName:TestTxPowerDmi ;Seq:1 ;AppModeID:4 ;EquipmentList:E3631_125_Powersupply ;
**表[TopoTestParameter]修改如下** 
新增一笔资料为: ID:2061 ;PID:338 ;ItemName:CURRENTTXPOWER(DBM) ;ItemType:double ;Direction:output ;ItemValue:-32768 ;DefaultLowLimit:-32768 ;DefaultUpperLimit:32767 ;ItemSpecific:0 ;LogRecord:1 ;Failbreak:0 ;DataRecord:0 ;
新增一笔资料为: ID:2062 ;PID:338 ;ItemName:DMITXPOWER(DBM) ;ItemType:double ;Direction:output ;ItemValue:-32768 ;DefaultLowLimit:-32768 ;DefaultUpperLimit:32767 ;ItemSpecific:0 ;LogRecord:1 ;Failbreak:0 ;DataRecord:0 ;
新增一笔资料为: ID:2063 ;PID:338 ;ItemName:DMITXPOWERERR(DB) ;ItemType:double ;Direction:output ;ItemValue:-32768 ;DefaultLowLimit:-1.5 ;DefaultUpperLimit:1.5 ;ItemSpecific:1 ;LogRecord:0 ;Failbreak:0 ;DataRecord:1 ;
**表[TopoEquipment]修改如下** 
**表[TopoEquipmentParameter]修改如下** 
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (12, N'terry.yin', CAST(0x0000A3CC00CDFF8C AS DateTime), CAST(0x0000A3CC00CE6EE0 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:terry.yin于2014/10/22 12:30:43修改==
 **表[TopoTestPlan]修改如下** 
**表[TopoTestControl]修改如下** 
已经删除资料: ID=71 ;19 ;1 ;1 ;1 ;1 ;1 ;1 ;1 ;1=1 ;
新增一笔资料为: ID=72 ;19 ;2 ;1 ;2 ;2 ;2 ;2 ;2 ;2=2 ;
**表[TopoTestModel]修改如下** 
已经删除资料: ID=338 ;71 ;TestTxPowerDmi ;1 ;4 ;E3631_125_Powersupply ;
新增一笔资料为: ID=339 ;72 ;TestIBiasDmi ;1 ;5 ; ;
**表[TopoTestParameter]修改如下** 
已经删除资料: ID=2061 ;338 ;CURRENTTXPOWER(DBM) ;double ;output ;-32768 ;-32768 ;32767 ;0 ;1 ;0 ;0 ;
已经删除资料: ID=2062 ;338 ;DMITXPOWER(DBM) ;double ;output ;-32768 ;-32768 ;32767 ;0 ;1 ;0 ;0 ;
已经删除资料: ID=2063 ;338 ;DMITXPOWERERR(DB) ;double ;output ;-32768 ;-1.5 ;1.5 ;1 ;0 ;0 ;1 ;
新增一笔资料为: ID=2064 ;339 ;IBIAS(MA) ;double ;output ;-32768 ;10 ;100 ;1 ;0 ;0 ;1 ;
**表[TopoEquipment]修改如下** 
**表[TopoEquipmentParameter]修改如下** 
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (13, N'terry.yin', CAST(0x0000A3CD0113FFDC AS DateTime), CAST(0x0000A3CD011428E0 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (14, N'terry.yin', CAST(0x0000A3CE009BBFE0 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登入', N'==User:terry.yin于2014/10/24 9:31:00修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:15;
修改后资料为:ItemValue:1;
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:1;
修改后资料为:ItemValue:2;
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:0;
修改后资料为:ItemValue:1;
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 9:35:50修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:-15;
修改后资料为:ItemValue:-17;
修改前资料为:ItemValue:-22;
修改后资料为:ItemValue:-30;
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:-15;
修改后资料为:ItemValue:-17;
修改前资料为:ItemValue:-22;
修改后资料为:ItemValue:-30;
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:-15;
修改后资料为:ItemValue:-17;
修改前资料为:ItemValue:-22;
修改后资料为:ItemValue:-30;
修改前资料为:
修改后资料为:
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 11:01:07修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:-19;
修改后资料为:ItemValue:-21;
修改前资料为:
修改后资料为:
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 11:07:06修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:ItemValue:-17;
修改后资料为:ItemValue:-14;
修改前资料为:ItemValue:-17;
修改后资料为:ItemValue:-14;
修改前资料为:ItemValue:-17;
修改后资料为:ItemValue:-14;
修改前资料为:
修改后资料为:
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 11:14:59修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:-21;
修改后资料为:ItemValue:-19;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 11:29:23修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:ItemValue:1;
修改后资料为:ItemValue:15;
修改前资料为:
修改后资料为:
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (15, N'terry.yin', CAST(0x0000A3CE00E51424 AS DateTime), CAST(0x0000A3CE00E5E714 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:terry.yin于2014/10/24 13:56:59修改==
**表[TopoTestPlan]修改如下**
新增一笔资料为:ID=21;3;FTM-TestLosAD;1;1;0;SN_CHECK = 0;ApcStyle=1;
**表[TopoTestControl]修改如下**
新增一笔资料为:ID=74;21;FMT-3.3-20-1-4;5;0;20;3.3;31;25.78125e+9;TempSleep=20;
新增一笔资料为:ID=75;21;FMT-3.3-40-1_4;6;0;40;3.3;31;25.78125E+9;TempSleep=60;
新增一笔资料为:ID=76;21;FMT-60-3.3-1_4;7;0;60;3.3;31;25.78125e+9;TempSleep=60;
**表[TopoTestModel]修改如下**
新增一笔资料为:ID=341;74;TestRXLosAD;18;8;E3631_136_Powersupply,AQ2211Atten_141_Attennuator;
新增一笔资料为:ID=342;75;TestRXLosAD;12;8;E3631_136_Powersupply,AQ2211Atten_141_Attennuator;
新增一笔资料为:ID=343;76;TestRXLosAD;10;8;E3631_136_Powersupply,AQ2211Atten_141_Attennuator;
**表[TopoTestParameter]修改如下**
新增一笔资料为:ID=2084;341;LOSA;double;output;-32768;-29;-16;1;0;0;1;
新增一笔资料为:ID=2085;341;LOSD;double;output;-32768;-29;-13;1;0;0;1;
新增一笔资料为:ID=2086;341;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2087;341;LOSDMAX;double;input;-14;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2088;341;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2089;341;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2090;341;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
新增一笔资料为:ID=2091;341;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2092;342;LOSA;double;output;-32768;-29;-16;1;0;0;1;
新增一笔资料为:ID=2093;342;LOSD;double;output;-32768;-29;-13;1;0;0;1;
新增一笔资料为:ID=2094;342;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2095;342;LOSDMAX;double;input;-14;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2096;342;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2097;342;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2098;342;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
新增一笔资料为:ID=2099;342;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2100;343;LOSA;double;output;-32768;-29;-16;1;0;0;1;
新增一笔资料为:ID=2101;343;LOSD;double;output;-32768;-29;-13;1;0;0;1;
新增一笔资料为:ID=2102;343;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2103;343;LOSDMAX;double;input;-14;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2104;343;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2105;343;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2106;343;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
新增一笔资料为:ID=2107;343;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
**表[TopoEquipment]修改如下**
新增一笔资料为:ID=136;21;1;Powersupply;E3631_136_Powersupply;
新增一笔资料为:ID=137;21;1;Scope;FLEX86100_137_Scope;
新增一笔资料为:ID=138;21;1;PPG;MP1800PPG_138_PPG;
新增一笔资料为:ID=139;21;1;Thermocontroller;TPO4300_139_Thermocontroller;
新增一笔资料为:ID=140;21;1;OpticalSwitch;AQ2211OpticalSwitch_140_OpticalSwitch;
新增一笔资料为:ID=141;21;1;Attennuator;AQ2211Atten_141_Attennuator;
新增一笔资料为:ID=142;21;1;ErrorDetector;MP1800ED_142_ErrorDetector;
新增一笔资料为:ID=143;21;1;OpticalSwitch;AQ2211OpticalSwitch_143_OpticalSwitch;
**表[TopoEquipmentParameter]修改如下**
新增一笔资料为:ID=1914;136;Addr;5;
新增一笔资料为:ID=1915;136;IOType;GPIB;
新增一笔资料为:ID=1916;136;Reset;false;
新增一笔资料为:ID=1917;136;Name;E3631;
新增一笔资料为:ID=1918;136;DutChannel;1;
新增一笔资料为:ID=1919;136;OptSourceChannel;2;
新增一笔资料为:ID=1920;136;DutVoltage;3.5;
新增一笔资料为:ID=1921;136;DutCurrent;2.5;
新增一笔资料为:ID=1922;136;OptVoltage;3.5;
新增一笔资料为:ID=1923;136;OptCurrent;1.5;
新增一笔资料为:ID=1924;136;voltageoffset;0.2;
新增一笔资料为:ID=1925;136;currentoffset;0;
新增一笔资料为:ID=1926;137;Addr;7;
新增一笔资料为:ID=1927;137;IOType;GPIB;
新增一笔资料为:ID=1928;137;Reset;false;
新增一笔资料为:ID=1929;137;Name;FLEX86100;
新增一笔资料为:ID=1930;137;configFilePath;1;
新增一笔资料为:ID=1931;137;FlexDcaDataRate;25.78125E+9;
新增一笔资料为:ID=1932;137;FilterSwitch;1;
新增一笔资料为:ID=1933;137;FlexFilterFreq;25.78125;
新增一笔资料为:ID=1934;137;triggerSource;1;
新增一笔资料为:ID=1935;137;FlexTriggerBwlimit;2;
新增一笔资料为:ID=1936;137;opticalSlot;3;
新增一笔资料为:ID=1937;137;elecSlot;4;
新增一笔资料为:ID=1938;137;FlexOptChannel;1;
新增一笔资料为:ID=1939;137;FlexElecChannel;1;
新增一笔资料为:ID=1940;137;FlexDcaWavelength;1;
新增一笔资料为:ID=1941;137;opticalAttSwitch;1;
新增一笔资料为:ID=1942;137;FlexDcaAtt;0;
新增一笔资料为:ID=1943;137;erFactor;0;
新增一笔资料为:ID=1944;137;FlexScale;300;
新增一笔资料为:ID=1945;137;FlexOffset;300;
新增一笔资料为:ID=1946;137;Threshold;0;
新增一笔资料为:ID=1947;137;reference;0;
新增一笔资料为:ID=1948;137;precisionTimebaseModuleSlot;1;
新增一笔资料为:ID=1949;137;precisionTimebaseSynchMethod;1;
新增一笔资料为:ID=1950;137;precisionTimebaseRefClk;6.445e+9;
新增一笔资料为:ID=1951;137;rapidEyeSwitch;1;
新增一笔资料为:ID=1952;137;marginType;1;
新增一笔资料为:ID=1953;137;marginHitType;0;
新增一笔资料为:ID=1954;137;marginHitRatio;5e-006;
新增一笔资料为:ID=1955;137;marginHitCount;0;
新增一笔资料为:ID=1956;137;acqLimitType;0;
新增一笔资料为:ID=1957;137;acqLimitNumber;700;
新增一笔资料为:ID=1958;137;opticalMaskName;c:\scope\masks\25.78125_100GBASE-LR4_Tx_Optical_D31.MSK;
新增一笔资料为:ID=1959;137;elecMaskName;c:\Eye;
新增一笔资料为:ID=1960;137;opticalEyeSavePath;D:\Eye\;
新增一笔资料为:ID=1961;137;elecEyeSavePath;D:\Eye\;
新增一笔资料为:ID=1962;137;ERFACTORSWITCH;1;
新增一笔资料为:ID=1963;138;Addr;1;
新增一笔资料为:ID=1964;138;IOType;GPIB;
新增一笔资料为:ID=1965;138;Reset;false;
新增一笔资料为:ID=1966;138;Name;MP1800PPG;
新增一笔资料为:ID=1967;138;dataRate;25.78128;
新增一笔资料为:ID=1968;138;dataLevelGuardAmpMax;1;
新增一笔资料为:ID=1969;138;dataLevelGuardOffsetMax;0;
新增一笔资料为:ID=1970;138;dataLevelGuardOffsetMin;0;
新增一笔资料为:ID=1971;138;dataLevelGuardSwitch;0;
新增一笔资料为:ID=1972;138;dataAmplitude;0.5;
新增一笔资料为:ID=1973;138;dataCrossPoint;50;
新增一笔资料为:ID=1974;138;configFilePath;0;
新增一笔资料为:ID=1975;138;slot;1;
新增一笔资料为:ID=1976;138;clockSource;0;
新增一笔资料为:ID=1977;138;auxOutputClkDiv;0;
新增一笔资料为:ID=1978;138;prbsLength;31;
新增一笔资料为:ID=1979;138;patternType;0;
新增一笔资料为:ID=1980;138;dataSwitch;1;
新增一笔资料为:ID=1981;138;dataTrackingSwitch;1;
新增一笔资料为:ID=1982;138;dataAcModeSwitch;0;
新增一笔资料为:ID=1983;138;dataLevelMode;0;
新增一笔资料为:ID=1984;138;clockSwitch;1;
新增一笔资料为:ID=1985;138;outputSwitch;1;
新增一笔资料为:ID=1986;138;TotalChannel;4;
新增一笔资料为:ID=1987;139;Addr;23;
新增一笔资料为:ID=1988;139;IOType;GPIB;
新增一笔资料为:ID=1989;139;Reset;False;
新增一笔资料为:ID=1990;139;Name;TPO4300;
新增一笔资料为:ID=1991;139;FLSE;14;
新增一笔资料为:ID=1992;139;ULIM;90;
新增一笔资料为:ID=1993;139;LLIM;-20;
新增一笔资料为:ID=1994;139;Sensor;1;
新增一笔资料为:ID=1995;140;Addr;20;
新增一笔资料为:ID=1996;140;IOType;GPIB;
新增一笔资料为:ID=1997;140;Reset;false;
新增一笔资料为:ID=1998;140;Name;AQ2011OpticalSwitch;
新增一笔资料为:ID=1999;140;OpticalSwitchSlot;1;
新增一笔资料为:ID=2000;140;SwitchChannel;1;
新增一笔资料为:ID=2001;140;ToChannel;1;
新增一笔资料为:ID=2002;141;Addr;20;
新增一笔资料为:ID=2003;141;IOType;GPIB;
新增一笔资料为:ID=2004;141;Reset;false;
新增一笔资料为:ID=2005;141;Name;AQ2211Atten;
新增一笔资料为:ID=2006;141;TOTALCHANNEL;4;
新增一笔资料为:ID=2007;141;AttValue;20;
新增一笔资料为:ID=2008;141;AttSlot;2;
新增一笔资料为:ID=2009;141;WAVELENGTH;1310,1310,1310,1310;
新增一笔资料为:ID=2010;141;AttChannel;1;
新增一笔资料为:ID=2011;142;Addr;1;
新增一笔资料为:ID=2012;142;IOType;GPIB;
新增一笔资料为:ID=2013;142;Reset;false;
新增一笔资料为:ID=2014;142;Name;MP1800ED;
新增一笔资料为:ID=2015;142;slot;3;
新增一笔资料为:ID=2016;142;TotalChannel;4;
新增一笔资料为:ID=2017;142;currentChannel;1;
新增一笔资料为:ID=2018;142;dataInputInterface;2;
新增一笔资料为:ID=2019;142;prbsLength;31;
新增一笔资料为:ID=2020;142;errorResultZoom;1;
新增一笔资料为:ID=2021;142;edGatingMode;1;
新增一笔资料为:ID=2022;142;edGatingUnit;0;
新增一笔资料为:ID=2023;142;edGatingTime;5;
新增一笔资料为:ID=2024;143;Addr;20;
新增一笔资料为:ID=2025;143;IOType;GPIB;
新增一笔资料为:ID=2026;143;Reset;false;
新增一笔资料为:ID=2027;143;Name;AQ2011OpticalSwitch;
新增一笔资料为:ID=2028;143;OpticalSwitchSlot;3;
新增一笔资料为:ID=2029;143;SwitchChannel;1;
新增一笔资料为:ID=2030;143;ToChannel;1;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (16, N'terry.yin', CAST(0x0000A3CE00E63EF8 AS DateTime), CAST(0x0000A3CE00FC867C AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'==User:terry.yin于2014/10/24 13:59:16修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:ItemValue:-14;
修改后资料为:ItemValue:-13;
修改前资料为:ItemValue:-14;
修改后资料为:ItemValue:-13;
修改前资料为:ItemValue:-14;
修改后资料为:ItemValue:-13;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 14:09:00修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 14:14:35修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:ItemValue:-19;
修改后资料为:ItemValue:-20;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 14:48:58修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:
修改后资料为:
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 14:53:07修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:15;
修改后资料为:ItemValue:1;
修改前资料为:
修改后资料为:
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 15:08:10修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:1;
修改后资料为:ItemValue:3;
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:-20;
修改后资料为:ItemValue:-19;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 15:13:58修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:ItemValue:-14;
修改后资料为:ItemValue:-13;
修改前资料为:ItemValue:-14;
修改后资料为:ItemValue:-13;
修改前资料为:ItemValue:-14;
修改后资料为:ItemValue:-13;
修改前资料为:
修改后资料为:
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (17, N'terry.yin', CAST(0x0000A3CE00FE8C74 AS DateTime), CAST(0x0000A3CE012BC1E4 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'==User:terry.yin于2014/10/24 15:27:21修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:ItemValue:3;
修改后资料为:ItemValue:15;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 16:36:32修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:ItemValue:15;
修改后资料为:ItemValue:14;
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:15;
修改后资料为:ItemValue:30;
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:-19;
修改后资料为:ItemValue:-21;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 16:59:18修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:ItemValue:32;
修改后资料为:ItemValue:2;
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:24135;
修改后资料为:ItemValue:30;
修改前资料为:ItemValue:300;
修改后资料为:ItemValue:14;
修改前资料为:ItemValue:-20;
修改后资料为:ItemValue:-21;
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 17:09:10修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 17:12:29修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
修改前资料为:AuxAttribles:TempSleep=60;
修改后资料为:AuxAttribles:TempSleep=2;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 18:00:29修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
修改前资料为:AuxAttribles:TempSleep=20;
修改后资料为:AuxAttribles:TempSleep=2;
修改前资料为:AuxAttribles:TempSleep=60;
修改后资料为:AuxAttribles:TempSleep=2;
修改前资料为:AuxAttribles:TempSleep=60;
修改后资料为:AuxAttribles:TempSleep=2;
修改前资料为:
修改后资料为:
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (18, N'terry.yin', CAST(0x0000A3CE01768620 AS DateTime), CAST(0x0000A3CE017B1B2C AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/10/24 22:49:32修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
新增一笔资料为:ID=344;62;AdjustLos;6;6;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;
**表[TopoTestParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
新增一笔资料为:ID=2108;344;LOSDVOLTAGETUNESTEP(V);byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2109;344;LOSAVOLTAGESTARTVALUE(V);UInt16;input;14;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2110;344;ISLOSALOSDCOMBIN;bool;input;True;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2111;344;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2112;344;LOSDVOLTAGEUPERLIMIT(V);UInt16;input;30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2113;344;LOSDVOLTAGESTARTVALUE(V);UInt16;input;14;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2114;344;LOSDINPUTPOWER;double;input;-21;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2115;344;LOSAVOLTAGETUNESTEP(V);byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2116;344;LOSAVOLTAGEUPERLIMIT(V);UInt16;input;30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2117;344;LOSTOLERANCESTEP(V);byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2118;344;LOSDVOLTAGELOWLIMIT(V);UInt16;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2119;344;LOSAINPUTPOWER;double;input;-21;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2120;344;LOSAVOLTAGELOWLIMIT(V);UInt16;input;1;-32768;32767;0;0;0;0;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/24 22:52:39修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
新增一笔资料为:ID=345;61;TestRXLosAD;13;8;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;
新增一笔资料为:ID=346;60;TestRXLosAD;12;8;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;
新增一笔资料为:ID=347;59;TestRXLosAD;18;8;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;
**表[TopoTestParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
新增一笔资料为:ID=2121;345;LOSA;double;output;-32768;-29;-16;1;0;0;1;
新增一笔资料为:ID=2122;345;LOSD;double;output;-32768;-29;-13;1;0;0;1;
新增一笔资料为:ID=2123;345;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2124;345;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2125;345;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2126;345;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2127;345;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
新增一笔资料为:ID=2128;345;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2129;346;LOSA;double;output;-32768;-29;-16;1;0;0;1;
新增一笔资料为:ID=2130;346;LOSD;double;output;-32768;-29;-13;1;0;0;1;
新增一笔资料为:ID=2131;346;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2132;346;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2133;346;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2134;346;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2135;346;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
新增一笔资料为:ID=2136;346;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2137;347;LOSA;double;output;-32768;-29;-16;1;0;0;1;
新增一笔资料为:ID=2138;347;LOSD;double;output;-32768;-29;-13;1;0;0;1;
新增一笔资料为:ID=2139;347;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2140;347;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2141;347;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2142;347;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2143;347;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
新增一笔资料为:ID=2144;347;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (19, N'terry.yin', CAST(0x0000A3CF00BC6F10 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登入', N'==User:terry.yin于2014/10/25 11:26:58修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:1;
修改后资料为:ItemValue:0;
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
==User:terry.yin于2014/10/25 11:27:17修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/25 13:50:15修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:350,450,550;
修改后资料为:ItemValue:300,400,500;
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:350,450,550;
修改后资料为:ItemValue:300,400,500;
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:350,450,550;
修改后资料为:ItemValue:300.400,500;
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/25 13:53:23修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:300.400,500;
修改后资料为:ItemValue:300,400,500;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/25 14:15:04修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:ItemValue:300;
修改后资料为:ItemValue:250;
修改前资料为:ItemValue:300;
修改后资料为:ItemValue:200;
修改前资料为:ItemValue:300;
修改后资料为:ItemValue:200;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (20, N'terry.yin', CAST(0x0000A3CF00EE893C AS DateTime), CAST(0x0000A3CF00FEDE7C AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'==User:terry.yin于2014/10/25 14:29:35修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:ItemValue:500;
修改后资料为:ItemValue:300;
修改前资料为:ItemValue:500;
修改后资料为:ItemValue:300;
修改前资料为:ItemValue:500;
修改后资料为:ItemValue:300;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (21, N'terry.yin', CAST(0x0000A3D100A82424 AS DateTime), CAST(0x0000A3D100AABC98 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (22, N'', CAST(0x0000A3D100CF93D8 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'EEPROM', N'用户已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (23, N'', CAST(0x0000A3D100D0A610 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'EEPROM', N'用户已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (24, N'', CAST(0x0000A3D100D1F538 AS DateTime), CAST(0x0000A3D100D2A17C AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:Terry.Yin于2014/10/27 12:44:40修改==
**表[GlobalMSAEEPROMInitialize]修改如下**
修改前资料为:Data0:0D00070200000000000000056700014B00000040417269737461204E6574776F726B732007001C73515346502D3430472D554E495620202030316658251C469B000000D85844503133313734303030312020202031343130313520200800007A1003000000000000000000000000000000000000000002F8000000008B451DF3;
修改后资料为:Data0:0DCA070200000000000000056700014B00000040417269737461204E6574776F726B732007001C73515346502D3430472D554E495620202030316658251C469B000000D85844503133313734303030312020202031343130313520200800007A1003000000000000000000000000000000000000000002F8000000008B451DF3;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (25, N'', CAST(0x0000A3D100D309C8 AS DateTime), CAST(0x0000A3D100D43BA4 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:Terry.Yin于2014/10/27 12:50:48修改==
**表[GlobalMSAEEPROMInitialize]修改如下**
新增一笔资料为:ID=8;5;XFPRev0.2;XFP;06005F00E7005A00EC00FFFFFFFFFFFFFFFF271000FA232801F43DE80630312D07CB312400642710007D94706D609088714800000000000000000000000000000000000000000000000000000000000000400040BC00000000000000000000001E0300000D61000000017F25000092B800000000000000000000000000000001;06005F00E7005A00EC00FFFFFFFFFFFFFFFF271000FA232801F43DE80630312D07CB312400642710007D94706D609088714800000000000000000000000000000000000000000000000000000000000000400040BC00000000000000000000001E0300000D61000000017F25000092B800000000000000000000000000000001;;;;;;;
==User:Terry.Yin于2014/10/27 12:51:13修改==
**表[GlobalMSAEEPROMInitialize]修改如下**
修改前资料为:Data0:06005F00E7005A00EC00FFFFFFFFFFFFFFFF271000FA232801F43DE80630312D07CB312400642710007D94706D609088714800000000000000000000000000000000000000000000000000000000000000400040BC00000000000000000000001E0300000D61000000017F25000092B800000000000000000000000000000001;
修改后资料为:Data0:07085F00E7005A00EC00FFFFFFFFFFFFFFFF271000FA232801F43DE80630312D07CB312400642710007D94706D609088714800000000000000000000000000000000000000000000000000000000000000400040BC00000000000000000000001E0300000D61000000017F25000092B800000000000000000000000000000001;
==User:Terry.Yin于2014/10/27 12:51:48修改==
**表[GlobalMSAEEPROMInitialize]修改如下**
修改前资料为:Data1:1;CRCData1:0;
修改后资料为:Data1:FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF;CRCData1:FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (26, N'Terry.Yin', CAST(0x0000A3D100E2FEF0 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'==User:Terry.Yin于2014/10/27 13:46:44修改==
**表[GlobalMSAEEPROMInitialize]修改如下**
修改前资料为:Data0:0DCA070200000000000000056700014B00000040417269737461204E6574776F726B732007001C73515346502D3430472D554E495620202030316658251C469B000000D85844503133313734303030312020202031343130313520200800007A1003000000000000000000000000000000000000000002F8000000008B451DF3;
修改后资料为:Data0:0CCA070200000000000000056700014B00000040417269737461204E6574776F726B732007001C73515346502D3430472D554E495620202030316658251C469B000000D85844503133313734303030312020202031343130313520200800007A1003000000000000000000000000000000000000000002F8000000008B451DF3;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (27, N'Terry.Yin', CAST(0x0000A3D100E86368 AS DateTime), CAST(0x0000A3D100EEFE6C AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:Terry.Yin于2014/10/27 14:11:28修改==
**表[GlobalMSAEEPROMInitialize]修改如下**
修改前资料为:CRCData0:069007000000000000140010637150000000007E494E4E4F4C4947485420202020202020FF447C7F54522D485832325A2D4E303020202020314179DC001846DEAF966600494E424251303130303037312020202031313132303820200860743934200000000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF;
修改后资料为:CRCData0:068907000000000000140010637150000000007E494E4E4F4C4947485420202020202020FF447C7F54522D485832325A2D4E303020202020314179DC001846DEAF966600494E424251303130303037312020202031313132303820200860743934200000000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (28, N'terry.yin', CAST(0x0000A3D100EC3844 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (29, N'Terry.Yin', CAST(0x0000A3D100EF1000 AS DateTime), CAST(0x0000A3D100EFE7A0 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (30, N'Terry.Yin', CAST(0x0000A3D100F1BB34 AS DateTime), CAST(0x0000A3D100F2B020 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (31, N'terry.yin', CAST(0x0000A3D100F22254 AS DateTime), CAST(0x0000A3D100F9360C AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'==User:terry.yin于2014/10/27 14:44:00修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:0.01,0.01,0;
修改后资料为:ItemValue:0.01,0.005,0;
修改前资料为:ItemValue:0.01,0.01,0;
修改后资料为:ItemValue:0.01,0.005,0;
修改前资料为:ItemValue:0.01,0.01,0;
修改后资料为:ItemValue:0.01,0.005,0;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/27 14:59:51修改==
**表[TopoTestPlan]修改如下**
新增一笔资料为:ID=22;3;CGR4_2Temp;1;1;0;SN_CHECK = 0;ApcStyle=1;
**表[TopoTestControl]修改如下**
新增一笔资料为:ID=77;22;FMT-3.3-20-1;6;0;20;3.3;31;25.78125e+9;TempSleep=20;
新增一笔资料为:ID=78;22;FMT-3.3-40-1_4;5;0;40;3.3;31;25.78125E+9;TempSleep=60;
新增一笔资料为:ID=79;22;FMT-60-3.3-1_4;3;0;60;3.3;31;25.78125e+9;TempSleep=60;
新增一笔资料为:ID=80;22;LP-3.3-20-1-4;1;0;20;3.3;31;25.78125e+9;TempSleep=20;
新增一笔资料为:ID=81;22;LP-3.3-60-1-4;2;0;60;3.3;31;25.78125e+9;TempSleep=60;
**表[TopoTestModel]修改如下**
新增一笔资料为:ID=348;77;TestEye;7;3;E3631_144_Powersupply,FLEX86100_145_Scope;
新增一笔资料为:ID=349;77;TestTempDmi;10;13;E3631_144_Powersupply,TPO4300_147_Thermocontroller;
新增一笔资料为:ID=350;77;TestVccDmi;13;14;E3631_144_Powersupply;
新增一笔资料为:ID=351;77;TestIcc;14;15;E3631_144_Powersupply;
新增一笔资料为:ID=352;77;TestTxPowerDmi;9;4;E3631_144_Powersupply,FLEX86100_145_Scope;
新增一笔资料为:ID=353;77;TestIBiasDmi;15;5;E3631_144_Powersupply;
新增一笔资料为:ID=354;77;TestRxPowerDmi;16;9;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;
新增一笔资料为:ID=355;77;TestBer;17;10;E3631_144_Powersupply,AQ2211Atten_149_Attennuator,MP1800ED_150_ErrorDetector;
新增一笔资料为:ID=356;77;TestRXLosAD;18;8;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;
新增一笔资料为:ID=357;78;TestEye;1;3;E3631_144_Powersupply,FLEX86100_145_Scope;
新增一笔资料为:ID=358;78;TestTxPowerDmi;2;4;E3631_144_Powersupply,FLEX86100_145_Scope;
新增一笔资料为:ID=359;78;TestTempDmi;6;13;E3631_144_Powersupply,TPO4300_147_Thermocontroller;
新增一笔资料为:ID=360;78;TestIcc;7;15;E3631_144_Powersupply;
新增一笔资料为:ID=361;78;TestVccDmi;8;14;E3631_144_Powersupply;
新增一笔资料为:ID=362;78;TestIBiasDmi;9;5;E3631_144_Powersupply;
新增一笔资料为:ID=363;78;TestRxPowerDmi;10;9;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;
新增一笔资料为:ID=364;78;TestBer;11;10;E3631_144_Powersupply,AQ2211Atten_149_Attennuator,MP1800ED_150_ErrorDetector;
新增一笔资料为:ID=365;78;TestRXLosAD;12;8;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;
新增一笔资料为:ID=366;79;TestEye;1;3;E3631_144_Powersupply,FLEX86100_145_Scope;
新增一笔资料为:ID=367;79;TestTxPowerDmi;2;4;E3631_144_Powersupply,FLEX86100_145_Scope;
新增一笔资料为:ID=368;79;TestIBiasDmi;5;5;E3631_144_Powersupply;
新增一笔资料为:ID=369;79;TestTempDmi;7;13;E3631_144_Powersupply,TPO4300_147_Thermocontroller;
新增一笔资料为:ID=370;79;TestVccDmi;8;14;E3631_144_Powersupply;
新增一笔资料为:ID=371;79;TestIcc;9;15;E3631_144_Powersupply;
新增一笔资料为:ID=372;79;TestRxPowerDmi;11;9;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;
新增一笔资料为:ID=373;79;TestBer;12;10;E3631_144_Powersupply,AQ2211Atten_149_Attennuator,MP1800ED_150_ErrorDetector;
新增一笔资料为:ID=374;79;TestRXLosAD;13;8;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;
新增一笔资料为:ID=375;80;AdjustTxPowerDmi;1;2;E3631_144_Powersupply,FLEX86100_145_Scope;
新增一笔资料为:ID=376;80;AdjustEye;2;1;E3631_144_Powersupply,FLEX86100_145_Scope;
新增一笔资料为:ID=377;80;CalTempDminoProcessingCoef;3;19;E3631_144_Powersupply,TPO4300_147_Thermocontroller;
新增一笔资料为:ID=378;80;CalVccDminoProcessingCoef;4;20;E3631_144_Powersupply;
新增一笔资料为:ID=379;80;CalRxDminoProcessingCoef;5;18;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;
新增一笔资料为:ID=380;80;AdjustLos;6;6;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;
新增一笔资料为:ID=381;81;AdjustTxPowerDmi;1;2;E3631_144_Powersupply,FLEX86100_145_Scope;
新增一笔资料为:ID=382;81;AdjustEye;2;1;E3631_144_Powersupply,FLEX86100_145_Scope;
新增一笔资料为:ID=383;81;CalTempDminoProcessingCoef;3;19;E3631_144_Powersupply,TPO4300_147_Thermocontroller;
新增一笔资料为:ID=384;81;CalVccDminoProcessingCoef;4;20;E3631_144_Powersupply;
**表[TopoTestParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
新增一笔资料为:ID=2145;348;AP(DBM);double;output;-32768;0;3;1;0;0;1;
新增一笔资料为:ID=2146;348;JITTERPP(PS);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2147;348;TXOMA(UW);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2148;348;FALLTIME(PS);double;output;-32768;-32768;65;1;0;0;1;
新增一笔资料为:ID=2149;348;RISETIME(PS);double;output;-32768;-32768;100;1;0;0;1;
新增一笔资料为:ID=2150;348;JITTERRMS(PS);double;output;-32768;-32768;30;1;0;0;1;
新增一笔资料为:ID=2151;348;MASKMARGIN(%);double;output;-32768;10;90;1;0;0;1;
新增一笔资料为:ID=2152;348;ER(DB);double;output;-32768;3.5;6;1;0;0;1;
新增一笔资料为:ID=2153;348;ISOPTICALEYEORELECEYE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2154;348;CROSSING(%);double;output;-32768;40;60;1;0;0;1;
新增一笔资料为:ID=2155;349;DMITEMPERR(C);double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2156;349;DMITEMP(C);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2157;350;DMIVCCERR(V);double;output;-32768;-0.165;0.165;1;0;0;1;
新增一笔资料为:ID=2158;350;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2159;351;ICC(MA);double;output;-32768;500;1050;1;0;0;1;
新增一笔资料为:ID=2160;352;DMITXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2161;352;CURRENTTXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2162;352;DMITXPOWERERR(DB);double;output;-32768;-2;2;1;0;0;1;
新增一笔资料为:ID=2163;353;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
新增一笔资料为:ID=2164;354;ARRAYLISTRXINPUTPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2165;354;DMIRXPWRMAXERRPOINT(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2166;354;DMIRXPWRMAXERR;double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2167;355;CSENTARGETBER;double;input;1.0E-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2168;355;COEFCSENSUBSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2169;355;CSEN(DBM);double;output;-32768;-20;-11;1;0;0;1;
新增一笔资料为:ID=2170;355;SEARCHTARGETBERSUBSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2171;355;SEARCHTARGETBERADDSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2172;355;SEARCHTARGETBERLL;double;input;1E-8;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2173;355;SEARCHTARGETBERUL;double;input;1.00E-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2174;355;CSENSTARTINGRXPWR(DBM);double;input;-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2175;355;CSENALIGNRXPWR(DBM);double;input;-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2176;355;COEFCSENADDSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2177;355;IsBerQuickTest;bool;input;false;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2178;356;LOSA;double;output;-32768;-29;-16;1;0;0;1;
新增一笔资料为:ID=2179;356;LOSD;double;output;-32768;-29;-13;1;0;0;1;
新增一笔资料为:ID=2180;356;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2181;356;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2182;356;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2183;356;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2184;356;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
新增一笔资料为:ID=2185;356;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2186;357;AP(DBM);double;output;-32768;0;3;1;0;0;1;
新增一笔资料为:ID=2187;357;JITTERPP(PS);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2188;357;TXOMA(UW);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2189;357;FALLTIME(PS);double;output;-32768;-32768;65;1;0;0;1;
新增一笔资料为:ID=2190;357;RISETIME(PS);double;output;-32768;-32768;100;1;0;0;1;
新增一笔资料为:ID=2191;357;JITTERRMS(PS);double;output;-32768;-32768;30;1;0;0;1;
新增一笔资料为:ID=2192;357;MASKMARGIN(%);double;output;-32768;10;90;1;0;0;1;
新增一笔资料为:ID=2193;357;ER(DB);double;output;-32768;3.5;6;1;0;0;1;
新增一笔资料为:ID=2194;357;ISOPTICALEYEORELECEYE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2195;357;CROSSING(%);double;output;-32768;40;60;1;0;0;1;
新增一笔资料为:ID=2196;358;DMITXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2197;358;CURRENTTXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2198;358;DMITXPOWERERR(DB);double;output;-32768;-2;2;1;0;0;1;
新增一笔资料为:ID=2199;359;DMITEMPERR(C);double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2200;359;DMITEMP(C);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2201;360;ICC(MA);double;output;-32768;500;1050;1;0;0;1;
新增一笔资料为:ID=2202;361;DMIVCCERR(V);double;output;-32768;-0.165;0.165;1;0;0;1;
新增一笔资料为:ID=2203;361;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2204;362;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
新增一笔资料为:ID=2205;363;ARRAYLISTRXINPUTPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2206;363;DMIRXPWRMAXERRPOINT(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2207;363;DMIRXPWRMAXERR;double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2208;364;CSENTARGETBER;double;input;1.0E-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2209;364;COEFCSENSUBSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2210;364;CSEN(DBM);double;output;-32768;-20;-11;1;0;0;1;
新增一笔资料为:ID=2211;364;SEARCHTARGETBERSUBSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2212;364;SEARCHTARGETBERADDSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2213;364;SEARCHTARGETBERLL;double;input;1E-8;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2214;364;SEARCHTARGETBERUL;double;input;1.00E-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2215;364;CSENSTARTINGRXPWR(DBM);double;input;-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2216;364;CSENALIGNRXPWR(DBM);double;input;-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2217;364;COEFCSENADDSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2218;364;IsBerQuickTest;bool;input;false;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2219;365;LOSA;double;output;-32768;-29;-16;1;0;0;1;
新增一笔资料为:ID=2220;365;LOSD;double;output;-32768;-29;-13;1;0;0;1;
新增一笔资料为:ID=2221;365;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2222;365;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2223;365;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2224;365;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2225;365;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
新增一笔资料为:ID=2226;365;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2227;366;AP(DBM);double;output;-32768;0;3;1;0;0;1;
新增一笔资料为:ID=2228;366;JITTERPP(PS);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2229;366;TXOMA(UW);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2230;366;FALLTIME(PS);double;output;-32768;-32768;65;1;0;0;1;
新增一笔资料为:ID=2231;366;RISETIME(PS);double;output;-32768;-32768;100;1;0;0;1;
新增一笔资料为:ID=2232;366;JITTERRMS(PS);double;output;-32768;-32768;30;1;0;0;1;
新增一笔资料为:ID=2233;366;MASKMARGIN(%);double;output;-32768;10;90;1;0;0;1;
新增一笔资料为:ID=2234;366;ER(DB);double;output;-32768;3.5;6;1;0;0;1;
新增一笔资料为:ID=2235;366;ISOPTICALEYEORELECEYE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2236;366;CROSSING(%);double;output;-32768;40;60;1;0;0;1;
新增一笔资料为:ID=2237;367;DMITXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2238;367;CURRENTTXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2239;367;DMITXPOWERERR(DB);double;output;-32768;-2;2;1;0;0;1;
新增一笔资料为:ID=2240;368;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
新增一笔资料为:ID=2241;369;DMITEMPERR(C);double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2242;369;DMITEMP(C);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2243;370;DMIVCCERR(V);double;output;-32768;-0.165;0.165;1;0;0;1;
新增一笔资料为:ID=2244;370;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2245;371;ICC(MA);double;output;-32768;100;2000;1;0;0;1;
新增一笔资料为:ID=2246;372;ARRAYLISTRXINPUTPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2247;372;DMIRXPWRMAXERRPOINT(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2248;372;DMIRXPWRMAXERR;double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2249;373;CSENTARGETBER;double;input;1.0E-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2250;373;COEFCSENSUBSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2251;373;CSEN(DBM);double;output;-32768;-20;-11;1;0;0;1;
新增一笔资料为:ID=2252;373;SEARCHTARGETBERSUBSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2253;373;SEARCHTARGETBERADDSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2254;373;SEARCHTARGETBERLL;double;input;1E-8;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2255;373;SEARCHTARGETBERUL;double;input;1.00E-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2256;373;CSENSTARTINGRXPWR(DBM);double;input;-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2257;373;CSENALIGNRXPWR(DBM);double;input;-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2258;373;COEFCSENADDSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2259;373;IsBerQuickTest;bool;input;false;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2260;374;LOSA;double;output;-32768;-29;-16;1;0;0;1;
新增一笔资料为:ID=2261;374;LOSD;double;output;-32768;-29;-13;1;0;0;1;
新增一笔资料为:ID=2262;374;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2263;374;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2264;374;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2265;374;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2266;374;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
新增一笔资料为:ID=2267;374;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2268;375;FIXEDMODDAC(MA);UInt16;input;300;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2269;375;ARRAYLISTXDMICOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2270;375;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2271;375;IBIASADCORTXPOWERADC;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2272;375;ARRAYIBIAS(MA);ArrayList;input;300,400,500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2273;375;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2274;375;ISTEMPRELATIVE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2275;375;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2276;375;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2277;375;HighestCalTemp;double;input;60;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2278;375;LowestCalTemp;double;input;20;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2279;375;ISNEWALGORITHM;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2280;376;IMODMIN(MA);UInt16;input;100;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2281;376;ARRAYLISTTXMODCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2282;376;1STOR2STORPIDTXLOP;byte;input;0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2283;376;1STOR2STORPIDER;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2284;376;ISOPENLOOPORCLOSELOOPORBOTH;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2285;376;IMODSTART(MA);UInt16;input;250;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2286;376;IMODSTEP;byte;input;64;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2287;376;IMODMETHOD;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2288;376;TXERTOLERANCE(DB);double;input;0.2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2289;376;TXERTARGET(DB);double;input;4;3.5;6;1;0;1;0;
新增一笔资料为:ID=2290;376;ARRAYLISTTXPOWERCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2291;376;IBIASMETHOD;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2292;376;IBIASSTEP(MA);byte;input;64;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2293;376;IBIASSTART(MA);UInt16;input;600;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2294;376;IBIASMIN(MA);UInt16;input;400;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2295;376;IBIASMAX(MA);UInt16;input;2500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2296;376;FIXEDMOD(MA);UInt16;input;500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2297;376;TXLOPTOLERANCE(UW);double;input;100;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2298;376;TXLOPTARGET(UW);double;input;900;1000;2000;1;0;1;0;
新增一笔资料为:ID=2299;376;IMODMAX(MA);UInt16;input;700;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2300;376;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2301;376;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2302;376;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2303;376;PIDCOEFARRAY;ArrayList;input;0.01,0.005,0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2304;376;FIXEDIBIAS(MA);UInt32;input;280;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2305;376;FixedIBiasArray;ArrayList;input;280,280,280,280;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2306;376;FixedModArray;ArrayList;input;500,500,500,500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2307;377;1STOR2STORPID;byte;input;1;0;0;0;0;0;0;
新增一笔资料为:ID=2308;377;ARRAYLISTDMITEMPCOEF;ArrayList;output;-32768;0;0;0;1;0;0;
新增一笔资料为:ID=2309;378;GENERALVCC(V);double;input;3.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2310;378;ARRAYLISTVCC(V);ArrayList;input;3.1,3.3,3.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2311;378;ARRAYLISTDMIVCCCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2312;378;1STOR2STORPID;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2313;379;ARRAYLISTDMIRXCOEF;ArrayList;output;;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2314;379;ARRAYLISTRXPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2315;379;HasOffset;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2316;379;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2317;380;LOSDVOLTAGETUNESTEP(V);byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2318;380;LOSAVOLTAGESTARTVALUE(V);UInt16;input;14;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2319;380;ISLOSALOSDCOMBIN;bool;input;True;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2320;380;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2321;380;LOSDVOLTAGEUPERLIMIT(V);UInt16;input;30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2322;380;LOSDVOLTAGESTARTVALUE(V);UInt16;input;14;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2323;380;LOSDINPUTPOWER;double;input;-21;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2324;380;LOSAVOLTAGETUNESTEP(V);byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2325;380;LOSAVOLTAGEUPERLIMIT(V);UInt16;input;30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2326;380;LOSTOLERANCESTEP(V);byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2327;380;LOSDVOLTAGELOWLIMIT(V);UInt16;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2328;380;LOSAINPUTPOWER;double;input;-21;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2329;380;LOSAVOLTAGELOWLIMIT(V);UInt16;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2330;381;FIXEDMODDAC(MA);UInt16;input;300;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2331;381;ARRAYLISTXDMICOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2332;381;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2333;381;IBIASADCORTXPOWERADC;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2334;381;ARRAYIBIAS(MA);ArrayList;input;300,400,500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2335;381;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2336;381;ISTEMPRELATIVE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2337;381;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2338;381;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2339;381;HighestCalTemp;double;input;60;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2340;381;LowestCalTemp;double;input;20;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2341;381;ISNEWALGORITHM;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2342;382;IMODMIN(MA);UInt16;input;100;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2343;382;ARRAYLISTTXMODCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2344;382;1STOR2STORPIDTXLOP;byte;input;0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2345;382;1STOR2STORPIDER;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2346;382;ISOPENLOOPORCLOSELOOPORBOTH;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2347;382;IMODSTART(MA);UInt16;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2348;382;IMODSTEP;byte;input;64;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2349;382;IMODMETHOD;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2350;382;TXERTOLERANCE(DB);double;input;0.2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2351;382;TXERTARGET(DB);double;input;4;3.5;6;1;0;1;0;
新增一笔资料为:ID=2352;382;ARRAYLISTTXPOWERCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2353;382;IBIASMETHOD;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2354;382;IBIASSTEP(MA);byte;input;64;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2355;382;IBIASSTART(MA);UInt16;input;600;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2356;382;IBIASMIN(MA);UInt16;input;400;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2357;382;IBIASMAX(MA);UInt16;input;2500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2358;382;FIXEDMOD(MA);UInt16;input;500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2359;382;TXLOPTOLERANCE(UW);double;input;100;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2360;382;TXLOPTARGET(UW);double;input;900;1000;2000;1;0;1;0;
新增一笔资料为:ID=2361;382;IMODMAX(MA);UInt16;input;1000;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2362;382;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2363;382;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2364;382;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2365;382;PIDCOEFARRAY;ArrayList;input;0.01,0.005,0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2366;382;FIXEDIBIAS(MA);UInt32;input;280;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2367;382;FixedIBiasArray;ArrayList;input;280,280,280,280;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2368;382;FixedModArray;ArrayList;input;500,500,500,500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2369;383;1STOR2STORPID;byte;input;1;0;0;0;0;0;0;
新增一笔资料为:ID=2370;383;ARRAYLISTDMITEMPCOEF;ArrayList;output;-32768;0;0;0;1;0;0;
新增一笔资料为:ID=2371;384;GENERALVCC(V);double;input;3.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2372;384;ARRAYLISTVCC(V);ArrayList;input;3.1,3.3,3.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2373;384;ARRAYLISTDMIVCCCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2374;384;1STOR2STORPID;byte;input;1;-32768;32767;0;0;0;0;
**表[TopoEquipment]修改如下**
新增一笔资料为:ID=144;22;1;Powersupply;E3631_144_Powersupply;
新增一笔资料为:ID=145;22;1;Scope;FLEX86100_145_Scope;
新增一笔资料为:ID=146;22;1;PPG;MP1800PPG_146_PPG;
新增一笔资料为:ID=147;22;1;Thermocontroller;TPO4300_147_Thermocontroller;
新增一笔资料为:ID=148;22;1;OpticalSwitch;AQ2211OpticalSwitch_148_OpticalSwitch;
新增一笔资料为:ID=149;22;1;Attennuator;AQ2211Atten_149_Attennuator;
新增一笔资料为:ID=150;22;1;ErrorDetector;MP1800ED_150_ErrorDetector;
新增一笔资料为:ID=151;22;1;OpticalSwitch;AQ2211OpticalSwitch_151_OpticalSwitch;
**表[TopoEquipmentParameter]修改如下**
新增一笔资料为:ID=2031;144;Addr;5;
新增一笔资料为:ID=2032;144;IOType;GPIB;
新增一笔资料为:ID=2033;144;Reset;false;
新增一笔资料为:ID=2034;144;Name;E3631;
新增一笔资料为:ID=2035;144;DutChannel;1;
新增一笔资料为:ID=2036;144;OptSourceChannel;2;
新增一笔资料为:ID=2037;144;DutVoltage;3.5;
新增一笔资料为:ID=2038;144;DutCurrent;2.5;
新增一笔资料为:ID=2039;144;OptVoltage;3.5;
新增一笔资料为:ID=2040;144;OptCurrent;1.5;
新增一笔资料为:ID=2041;144;voltageoffset;0.2;
新增一笔资料为:ID=2042;144;currentoffset;0;
新增一笔资料为:ID=2043;145;Addr;7;
新增一笔资料为:ID=2044;145;IOType;GPIB;
新增一笔资料为:ID=2045;145;Reset;false;
新增一笔资料为:ID=2046;145;Name;FLEX86100;
新增一笔资料为:ID=2047;145;configFilePath;1;
新增一笔资料为:ID=2048;145;FlexDcaDataRate;25.78125E+9;
新增一笔资料为:ID=2049;145;FilterSwitch;1;
新增一笔资料为:ID=2050;145;FlexFilterFreq;25.78125;
新增一笔资料为:ID=2051;145;triggerSource;0;
新增一笔资料为:ID=2052;145;FlexTriggerBwlimit;2;
新增一笔资料为:ID=2053;145;opticalSlot;3;
新增一笔资料为:ID=2054;145;elecSlot;4;
新增一笔资料为:ID=2055;145;FlexOptChannel;1;
新增一笔资料为:ID=2056;145;FlexElecChannel;1;
新增一笔资料为:ID=2057;145;FlexDcaWavelength;1;
新增一笔资料为:ID=2058;145;opticalAttSwitch;1;
新增一笔资料为:ID=2059;145;FlexDcaAtt;0;
新增一笔资料为:ID=2060;145;erFactor;0;
新增一笔资料为:ID=2061;145;FlexScale;300;
新增一笔资料为:ID=2062;145;FlexOffset;300;
新增一笔资料为:ID=2063;145;Threshold;0;
新增一笔资料为:ID=2064;145;reference;0;
新增一笔资料为:ID=2065;145;precisionTimebaseModuleSlot;1;
新增一笔资料为:ID=2066;145;precisionTimebaseSynchMethod;1;
新增一笔资料为:ID=2067;145;precisionTimebaseRefClk;6.445e+9;
新增一笔资料为:ID=2068;145;rapidEyeSwitch;1;
新增一笔资料为:ID=2069;145;marginType;1;
新增一笔资料为:ID=2070;145;marginHitType;0;
新增一笔资料为:ID=2071;145;marginHitRatio;5e-006;
新增一笔资料为:ID=2072;145;marginHitCount;0;
新增一笔资料为:ID=2073;145;acqLimitType;0;
新增一笔资料为:ID=2074;145;acqLimitNumber;700;
新增一笔资料为:ID=2075;145;opticalMaskName;c:\scope\masks\25.78125_100GBASE-LR4_Tx_Optical_D31.MSK;
新增一笔资料为:ID=2076;145;elecMaskName;c:\Eye;
新增一笔资料为:ID=2077;145;opticalEyeSavePath;D:\Eye\;
新增一笔资料为:ID=2078;145;elecEyeSavePath;D:\Eye\;
新增一笔资料为:ID=2079;145;ERFACTORSWITCH;1;
新增一笔资料为:ID=2080;146;Addr;1;
新增一笔资料为:ID=2081;146;IOType;GPIB;
新增一笔资料为:ID=2082;146;Reset;false;
新增一笔资料为:ID=2083;146;Name;MP1800PPG;
新增一笔资料为:ID=2084;146;dataRate;25.78128;
新增一笔资料为:ID=2085;146;dataLevelGuardAmpMax;1;
新增一笔资料为:ID=2086;146;dataLevelGuardOffsetMax;0;
新增一笔资料为:ID=2087;146;dataLevelGuardOffsetMin;0;
新增一笔资料为:ID=2088;146;dataLevelGuardSwitch;0;
新增一笔资料为:ID=2089;146;dataAmplitude;0.5;
新增一笔资料为:ID=2090;146;dataCrossPoint;50;
新增一笔资料为:ID=2091;146;configFilePath;0;
新增一笔资料为:ID=2092;146;slot;1;
新增一笔资料为:ID=2093;146;clockSource;0;
新增一笔资料为:ID=2094;146;auxOutputClkDiv;0;
新增一笔资料为:ID=2095;146;prbsLength;31;
新增一笔资料为:ID=2096;146;patternType;0;
新增一笔资料为:ID=2097;146;dataSwitch;1;
新增一笔资料为:ID=2098;146;dataTrackingSwitch;1;
新增一笔资料为:ID=2099;146;dataAcModeSwitch;0;
新增一笔资料为:ID=2100;146;dataLevelMode;0;
新增一笔资料为:ID=2101;146;clockSwitch;1;
新增一笔资料为:ID=2102;146;outputSwitch;1;
新增一笔资料为:ID=2103;146;TotalChannel;4;
新增一笔资料为:ID=2104;147;Addr;23;
新增一笔资料为:ID=2105;147;IOType;GPIB;
新增一笔资料为:ID=2106;147;Reset;False;
新增一笔资料为:ID=2107;147;Name;TPO4300;
新增一笔资料为:ID=2108;147;FLSE;14;
新增一笔资料为:ID=2109;147;ULIM;90;
新增一笔资料为:ID=2110;147;LLIM;-20;
新增一笔资料为:ID=2111;147;Sensor;1;
新增一笔资料为:ID=2112;148;Addr;20;
新增一笔资料为:ID=2113;148;IOType;GPIB;
新增一笔资料为:ID=2114;148;Reset;false;
新增一笔资料为:ID=2115;148;Name;AQ2011OpticalSwitch;
新增一笔资料为:ID=2116;148;OpticalSwitchSlot;1;
新增一笔资料为:ID=2117;148;SwitchChannel;1;
新增一笔资料为:ID=2118;148;ToChannel;1;
新增一笔资料为:ID=2119;149;Addr;20;
新增一笔资料为:ID=2120;149;IOType;GPIB;
新增一笔资料为:ID=2121;149;Reset;false;
新增一笔资料为:ID=2122;149;Name;AQ2211Atten;
新增一笔资料为:ID=2123;149;TOTALCHANNEL;4;
新增一笔资料为:ID=2124;149;AttValue;20;
新增一笔资料为:ID=2125;149;AttSlot;2;
新增一笔资料为:ID=2126;149;WAVELENGTH;1310,1310,1310,1310;
新增一笔资料为:ID=2127;149;AttChannel;1;
新增一笔资料为:ID=2128;150;Addr;1;
新增一笔资料为:ID=2129;150;IOType;GPIB;
新增一笔资料为:ID=2130;150;Reset;false;
新增一笔资料为:ID=2131;150;Name;MP1800ED;
新增一笔资料为:ID=2132;150;slot;3;
新增一笔资料为:ID=2133;150;TotalChannel;4;
新增一笔资料为:ID=2134;150;currentChannel;1;
新增一笔资料为:ID=2135;150;dataInputInterface;2;
新增一笔资料为:ID=2136;150;prbsLength;31;
新增一笔资料为:ID=2137;150;errorResultZoom;1;
新增一笔资料为:ID=2138;150;edGatingMode;1;
新增一笔资料为:ID=2139;150;edGatingUnit;0;
新增一笔资料为:ID=2140;150;edGatingTime;5;
新增一笔资料为:ID=2141;151;Addr;20;
新增一笔资料为:ID=2142;151;IOType;GPIB;
新增一笔资料为:ID=2143;151;Reset;false;
新增一笔资料为:ID=2144;151;Name;AQ2011OpticalSwitch;
新增一笔资料为:ID=2145;151;OpticalSwitchSlot;3;
新增一笔资料为:ID=2146;151;SwitchChannel;1;
新增一笔资料为:ID=2147;151;ToChannel;1;
==User:terry.yin于2014/10/27 15:02:43修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
修改前资料为:AuxAttribles:TempSleep=20;
修改后资料为:AuxAttribles:TempSleep=60;
修改前资料为:AuxAttribles:TempSleep=60;
修改后资料为:AuxAttribles:TempSleep=5;
修改前资料为:AuxAttribles:TempSleep=20;
修改后资料为:AuxAttribles:TempSleep=60;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (32, N'Terry.Yin', CAST(0x0000A3D100F2B728 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (33, N'Terry.Yin', CAST(0x0000A3D100F2DCA8 AS DateTime), CAST(0x0000A3D100FE27AC AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (34, N'terry.yin', CAST(0x0000A3D100FE4E58 AS DateTime), CAST(0x0000A3D1010241AC AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (35, N'Terry.Yin', CAST(0x0000A3D20099852C AS DateTime), CAST(0x0000A3D2009A7B44 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (36, N'terry.yin', CAST(0x0000A3D2009BC238 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (37, N'terry.yin', CAST(0x0000A3D2009D9824 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (38, N'Terry.Yin', CAST(0x0000A3D2009DC254 AS DateTime), CAST(0x0000A3D2009DD514 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (39, N'Terry.Yin', CAST(0x0000A3D200A82C58 AS DateTime), CAST(0x0000A3D200A836E4 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (40, N'Terry.Yin', CAST(0x0000A3D200A999F8 AS DateTime), CAST(0x0000A3D200AA5B54 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (41, N'terry.yin', CAST(0x0000A3D200AA8C8C AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (42, N'terry.yin', CAST(0x0000A3D200AE5808 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (43, N'terry.yin', CAST(0x0000A3D200B10B70 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (44, N'terry.yin', CAST(0x0000A3D200B1DE60 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (45, N'terry.yin', CAST(0x0000A3D200E8D3E8 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (46, N'terry.yin', CAST(0x0000A3D200EBF44C AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (47, N'Terry.Yin', CAST(0x0000A3D200EC0838 AS DateTime), CAST(0x0000A3D200ED0300 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (48, N'terry.yin', CAST(0x0000A3D200ED15C0 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (49, N'Terry.Yin', CAST(0x0000A3D200ED23D0 AS DateTime), CAST(0x0000A3D200ED2E5C AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (50, N'terry.yin', CAST(0x0000A3D200EDD014 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (51, N'terry.yin', CAST(0x0000A3D200EE5A5C AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (52, N'Terry.Yin', CAST(0x0000A3D200EF658C AS DateTime), CAST(0x0000A3D200EF6DC0 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (53, N'Terry.Yin', CAST(0x0000A3D200EF7BD0 AS DateTime), CAST(0x0000A3D200EF8530 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (54, N'Terry.Yin', CAST(0x0000A3D200EF90E8 AS DateTime), CAST(0x0000A3D200EF96C4 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (55, N'terry.yin', CAST(0x0000A3D200F22F38 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (56, N'terry.yin', CAST(0x0000A3D200F27330 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (57, N'terry.yin', CAST(0x0000A3D200F30804 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (58, N'terry.yin', CAST(0x0000A3D200F408A8 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (59, N'terry.yin', CAST(0x0000A3D200F4A934 AS DateTime), CAST(0x0000A3D200F4E174 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (60, N'terry.yin', CAST(0x0000A3D200F6CB4C AS DateTime), CAST(0x0000A3D200F9C75C AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (61, N'terry.yin', CAST(0x0000A3D200F9D440 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (62, N'terry.yin', CAST(0x0000A3D200FA5FB4 AS DateTime), CAST(0x0000A3D200FB11D4 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:terry.yin于2014/10/28 15:14:02修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:SEQ:1;
修改后资料为:SEQ:2;
新增一笔资料为:ID=152;19;3;PPG;MP1800PPG_TRX_PPG;
**表[TopoEquipmentParameter]修改如下**
新增一笔资料为:ID=2148;152;Addr;1;
新增一笔资料为:ID=2149;152;IOType;GPIB;
新增一笔资料为:ID=2150;152;Reset;false;
新增一笔资料为:ID=2151;152;Name;MP1800PPG;
新增一笔资料为:ID=2152;152;dataRate;25.78125;
新增一笔资料为:ID=2153;152;dataLevelGuardAmpMax;1000;
新增一笔资料为:ID=2154;152;dataLevelGuardOffsetMax;1000;
新增一笔资料为:ID=2155;152;dataLevelGuardOffsetMin;-1000;
新增一笔资料为:ID=2156;152;dataLevelGuardSwitch;1;
新增一笔资料为:ID=2157;152;dataAmplitude;500;
新增一笔资料为:ID=2158;152;dataCrossPoint;50;
新增一笔资料为:ID=2159;152;configFilePath;"";
新增一笔资料为:ID=2160;152;slot;1;
新增一笔资料为:ID=2161;152;clockSource;0;
新增一笔资料为:ID=2162;152;auxOutputClkDiv;8;
新增一笔资料为:ID=2163;152;prbsLength;31;
新增一笔资料为:ID=2164;152;patternType;0;
新增一笔资料为:ID=2165;152;dataSwitch;1;
新增一笔资料为:ID=2166;152;dataTrackingSwitch;1;
新增一笔资料为:ID=2167;152;dataAcModeSwitch;1;
新增一笔资料为:ID=2168;152;dataLevelMode;0;
新增一笔资料为:ID=2169;152;clockSwitch;1;
新增一笔资料为:ID=2170;152;outputSwitch;1;
新增一笔资料为:ID=2171;152;TotalChannel;4;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (63, N'terry.yin', CAST(0x0000A3D20101655C AS DateTime), CAST(0x0000A3D20101B638 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (64, N'terry.yin', CAST(0x0000A3D2010213F8 AS DateTime), CAST(0x0000A3D20102BA60 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (65, N'terry.yin', CAST(0x0000A3D201191828 AS DateTime), CAST(0x0000A3D2011983F8 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:terry.yin于2014/10/28 17:04:20修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
修改前资料为:ItemName:E3631_125_Powersupply;
修改前资料为:ItemName:E3631_NA_Powersupply;
修改前资料为:ItemName:N490XED_127_ErrorDetector;
修改前资料为:ItemName:N490XED_RX_ErrorDetector;
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (66, N'terry.yin', CAST(0x0000A3D2011ADED8 AS DateTime), CAST(0x0000A3D2011CB974 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:terry.yin于2014/10/28 17:15:12修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
修改前资料为:ItemName:E3631_28_Powersupply;
修改前资料为:ItemName:E3631_NA_Powersupply;
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (67, N'Terry.Yin', CAST(0x0000A3D3009D43C4 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登入', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (68, N'terry.yin', CAST(0x0000A3D300A8E454 AS DateTime), CAST(0x0000A3D300AB2160 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (69, N'terry.yin', CAST(0x0000A3D300AF23F0 AS DateTime), CAST(0x0000A3D300BCD75C AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (70, N'Terry.Yin', CAST(0x0000A3D300B4F438 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登入', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (71, N'terry.yin', CAST(0x0000A3D300F1EB40 AS DateTime), CAST(0x0000A3D300F25BC0 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (72, N'terry.yin', CAST(0x0000A3D300F2664C AS DateTime), CAST(0x0000A3D300F29400 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (73, N'terry.yin', CAST(0x0000A3D300F2AEF4 AS DateTime), CAST(0x0000A3D300F2C9E8 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (74, N'terry.yin', CAST(0x0000A3D300F32C58 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (75, N'terry.yin', CAST(0x0000A3D300F72A38 AS DateTime), CAST(0x0000A3D300FF8D18 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (76, N'terry.yin', CAST(0x0000A3D300FFFA14 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (77, N'terry.yin', CAST(0x0000A3D3010FDB50 AS DateTime), CAST(0x0000A3D30110D870 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/10/29 16:30:14修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
修改前资料为:ItemName:E3631_NA_Powersupply;
修改后资料为:ItemName:E3631_28_Powersupply;
修改前资料为:ItemName:E3631_NA_Powersupply;
修改后资料为:ItemName:E3631_125_Powersupply;
修改前资料为:ItemName:N490XED_RX_ErrorDetector;
修改后资料为:ItemName:N490XED_127_ErrorDetector;
修改前资料为:ItemName:MP1800PPG_TRX_PPG;
修改后资料为:ItemName:MP1800PPG_152_PPG;
**表[TopoEquipmentParameter]修改如下**
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:ItemValue:700;
修改后资料为:ItemValue:1000;
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
修改前资料为:
修改后资料为:
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (78, N'terry.yin', CAST(0x0000A3D40098AB34 AS DateTime), CAST(0x0000A3D4009ED93C AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (79, N'terry.yin', CAST(0x0000A3D4010CE2C4 AS DateTime), CAST(0x0000A3D4010CFEE4 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (80, N'terry.yin', CAST(0x0000A3D4010D5920 AS DateTime), CAST(0x0000A3D4010DF628 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (81, N'terry.yin', CAST(0x0000A3D4010E2760 AS DateTime), CAST(0x0000A3D4010E3444 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (82, N'terry.yin', CAST(0x0000A3D4010E3A20 AS DateTime), CAST(0x0000A3D40115F314 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/10/30 16:24:37修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
新增一笔资料为:ID=153;7;2;Attennuator;AQ2211Atten;0;
**表[TopoEquipmentParameter]修改如下**
新增一笔资料为:ID=2172;153;Addr;6;
新增一笔资料为:ID=2173;153;IOType;GPIB;
新增一笔资料为:ID=2174;153;Reset;False;
新增一笔资料为:ID=2175;153;Name;AQ2211Atten;
新增一笔资料为:ID=2176;153;TOTALCHANNEL;4;
新增一笔资料为:ID=2177;153;AttValue;20;
新增一笔资料为:ID=2178;153;AttSlot;1;
新增一笔资料为:ID=2179;153;WAVELENGTH;1270,1290,1310,1330;
新增一笔资料为:ID=2180;153;AttChannel;1;
==User:terry.yin于2014/10/30 16:26:54修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/30 16:29:05修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/30 16:29:40修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
修改前资料为:SEQ:2;
修改前资料为:SEQ:1;
已经删除资料:ID=28;7;1;Powersupply;E3631_28_Powersupply;0;
新增一笔资料为:ID=154;7;2;Powersupply;E3631;1;
**表[TopoEquipmentParameter]修改如下**
已经删除资料:ID=356;28;Addr;5;
已经删除资料:ID=357;28;IOType;GPIB;
已经删除资料:ID=358;28;Reset;True;
已经删除资料:ID=359;28;Name;E3631;
已经删除资料:ID=360;28;DutChannel;1;
已经删除资料:ID=361;28;OptSourceChannel;2;
已经删除资料:ID=362;28;DutVoltage;3.5;
已经删除资料:ID=363;28;DutCurrent;1.5;
已经删除资料:ID=364;28;OptVoltage;3.5;
已经删除资料:ID=365;28;OptCurrent;1.0;
已经删除资料:ID=1025;28;voltageoffset;0.2;
已经删除资料:ID=1026;28;currentoffset;0;
新增一笔资料为:ID=2181;154;Addr;5;
新增一笔资料为:ID=2182;154;IOType;GPIB;
新增一笔资料为:ID=2183;154;Reset;False;
新增一笔资料为:ID=2184;154;Name;E3631;
新增一笔资料为:ID=2185;154;DutChannel;1;
新增一笔资料为:ID=2186;154;OptSourceChannel;2;
新增一笔资料为:ID=2187;154;DutVoltage;3.3;
新增一笔资料为:ID=2188;154;DutCurrent;1.5;
新增一笔资料为:ID=2189;154;OptVoltage;3.3;
新增一笔资料为:ID=2190;154;OptCurrent;1.5;
新增一笔资料为:ID=2191;154;voltageoffset;0;
新增一笔资料为:ID=2192;154;currentoffset;0.2;
==User:terry.yin于2014/10/30 16:32:04修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
修改前资料为:SEQ:1;
修改前资料为:SEQ:2;
修改前资料为:SEQ:2;Role:1;
修改前资料为:SEQ:1;Role:0;
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (83, N'terry.yin', CAST(0x0000A3D4010F56E4 AS DateTime), CAST(0x0000A3D401100A30 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (84, N'terry.yin', CAST(0x0000A3D401161F9C AS DateTime), CAST(0x0000A3D4012E4FCC AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/10/30 17:27:36修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
修改前资料为:ItemValue:6;
修改前资料为:ItemValue:20;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (85, N'terry.yin', CAST(0x0000A3D401321A1C AS DateTime), CAST(0x0000A3D40132F1BC AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/10/30 18:34:48修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/30 18:35:39修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (86, N'terry.yin', CAST(0x0000A3D50098D0B4 AS DateTime), CAST(0x0000A3D50098F2B0 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (87, N'Terry.Yin', CAST(0x0000A3D5009DF710 AS DateTime), CAST(0x0000A3D5009EE29C AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (88, N'terry.yin', CAST(0x0000A3D500AAA8AC AS DateTime), CAST(0x0000A3D500ABF450 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (89, N'terry.yin', CAST(0x0000A3D500B144DC AS DateTime), CAST(0x0000A3D500B350B0 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (90, N'terry.yin', CAST(0x0000A3D500E19858 AS DateTime), CAST(0x0000A3D500E259B4 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (91, N'terry.yin', CAST(0x0000A3D500E2BE7C AS DateTime), CAST(0x0000A3D500E320EC AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:terry.yin于2014/10/31 13:46:48修改==
**表[TopoTestPlan]修改如下**
新增一笔资料为:ID=23;3;CGR4-V5;1;1;0;SN_CHECK = 0;ApcStyle=1;
**表[TopoTestControl]修改如下**
新增一笔资料为:ID=82;23;FMT-3.3-20-1;6;0;20;3.3;31;25.78125e+9;TempSleep=20;False;
新增一笔资料为:ID=83;23;FMT-3.3-40-1_4;5;0;40;3.3;31;25.78125E+9;TempSleep=60;False;
新增一笔资料为:ID=84;23;FMT-60-3.3-1_4;3;0;60;3.3;31;25.78125e+9;TempSleep=60;False;
新增一笔资料为:ID=85;23;LP-3.3-20-1-4;1;0;20;3.3;31;25.78125e+9;TempSleep=20;False;
新增一笔资料为:ID=86;23;LP-3.3-40-1-4;4;0;40;3.3;31;25.78125e+9;TempSleep=60;False;
新增一笔资料为:ID=87;23;LP-3.3-60-1-4;2;0;60;3.3;31;25.78125e+9;TempSleep=60;False;
**表[TopoTestModel]修改如下**
新增一笔资料为:ID=385;82;TestEye;7;3;E3631_97_Powersupply,FLEX86100_98_Scope;False;
新增一笔资料为:ID=386;82;TestTempDmi;10;13;E3631_97_Powersupply,TPO4300_100_Thermocontroller;False;
新增一笔资料为:ID=387;82;TestVccDmi;13;14;E3631_97_Powersupply;False;
新增一笔资料为:ID=388;82;TestIcc;14;15;E3631_97_Powersupply;False;
新增一笔资料为:ID=389;82;TestTxPowerDmi;9;4;E3631_97_Powersupply,FLEX86100_98_Scope;False;
新增一笔资料为:ID=390;82;TestIBiasDmi;15;5;E3631_97_Powersupply;False;
新增一笔资料为:ID=391;82;TestRxPowerDmi;16;9;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
新增一笔资料为:ID=392;82;TestBer;17;10;E3631_97_Powersupply,AQ2211Atten_102_Attennuator,MP1800ED_103_ErrorDetector;False;
新增一笔资料为:ID=393;82;TestRXLosAD;18;8;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
新增一笔资料为:ID=394;83;TestEye;1;3;E3631_97_Powersupply,FLEX86100_98_Scope;False;
新增一笔资料为:ID=395;83;TestTxPowerDmi;2;4;E3631_97_Powersupply,FLEX86100_98_Scope;False;
新增一笔资料为:ID=396;83;TestTempDmi;6;13;E3631_97_Powersupply,TPO4300_100_Thermocontroller;False;
新增一笔资料为:ID=397;83;TestIcc;7;15;E3631_97_Powersupply;False;
新增一笔资料为:ID=398;83;TestVccDmi;8;14;E3631_97_Powersupply;False;
新增一笔资料为:ID=399;83;TestIBiasDmi;9;5;E3631_97_Powersupply;False;
新增一笔资料为:ID=400;83;TestRxPowerDmi;10;9;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
新增一笔资料为:ID=401;83;TestBer;11;10;E3631_97_Powersupply,AQ2211Atten_102_Attennuator,MP1800ED_103_ErrorDetector;False;
新增一笔资料为:ID=402;83;TestRXLosAD;12;8;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
新增一笔资料为:ID=403;84;TestEye;1;3;E3631_97_Powersupply,FLEX86100_98_Scope;False;
新增一笔资料为:ID=404;84;TestTxPowerDmi;2;4;E3631_97_Powersupply,FLEX86100_98_Scope;False;
新增一笔资料为:ID=405;84;TestIBiasDmi;5;5;E3631_97_Powersupply;False;
新增一笔资料为:ID=406;84;TestTempDmi;7;13;E3631_97_Powersupply,TPO4300_100_Thermocontroller;False;
新增一笔资料为:ID=407;84;TestVccDmi;8;14;E3631_97_Powersupply;False;
新增一笔资料为:ID=408;84;TestIcc;9;15;E3631_97_Powersupply;False;
新增一笔资料为:ID=409;84;TestRxPowerDmi;11;9;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
新增一笔资料为:ID=410;84;TestBer;12;10;E3631_97_Powersupply,AQ2211Atten_102_Attennuator,MP1800ED_103_ErrorDetector;False;
新增一笔资料为:ID=411;84;TestRXLosAD;13;8;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
新增一笔资料为:ID=412;85;AdjustTxPowerDmi;1;2;E3631_97_Powersupply,FLEX86100_98_Scope;False;
新增一笔资料为:ID=413;85;AdjustEye;2;1;E3631_97_Powersupply,FLEX86100_98_Scope;False;
新增一笔资料为:ID=414;85;CalTempDminoProcessingCoef;3;19;E3631_97_Powersupply,TPO4300_100_Thermocontroller;False;
新增一笔资料为:ID=415;85;CalVccDminoProcessingCoef;4;20;E3631_97_Powersupply;False;
新增一笔资料为:ID=416;85;CalRxDminoProcessingCoef;5;18;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
新增一笔资料为:ID=417;85;AdjustLos;6;6;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
新增一笔资料为:ID=418;86;AdjustTxPowerDmi;1;2;E3631_97_Powersupply,FLEX86100_98_Scope;False;
新增一笔资料为:ID=419;86;AdjustEye;2;1;E3631_97_Powersupply,FLEX86100_98_Scope;False;
新增一笔资料为:ID=420;86;CalTempDminoProcessingCoef;3;19;E3631_97_Powersupply,TPO4300_100_Thermocontroller;False;
新增一笔资料为:ID=421;86;CalVccDminoProcessingCoef;4;20;E3631_97_Powersupply;False;
新增一笔资料为:ID=422;87;AdjustTxPowerDmi;1;2;E3631_97_Powersupply,FLEX86100_98_Scope;False;
新增一笔资料为:ID=423;87;AdjustEye;2;1;E3631_97_Powersupply,FLEX86100_98_Scope;False;
新增一笔资料为:ID=424;87;CalTempDminoProcessingCoef;3;19;E3631_97_Powersupply,TPO4300_100_Thermocontroller;False;
新增一笔资料为:ID=425;87;CalVccDminoProcessingCoef;4;20;E3631_97_Powersupply;False;
**表[TopoTestParameter]修改如下**
新增一笔资料为:ID=2375;385;AP(DBM);double;output;-32768;0;3;1;0;0;1;
新增一笔资料为:ID=2376;385;JITTERPP(PS);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2377;385;TXOMA(UW);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2378;385;FALLTIME(PS);double;output;-32768;-32768;65;1;0;0;1;
新增一笔资料为:ID=2379;385;RISETIME(PS);double;output;-32768;-32768;100;1;0;0;1;
新增一笔资料为:ID=2380;385;JITTERRMS(PS);double;output;-32768;-32768;30;1;0;0;1;
新增一笔资料为:ID=2381;385;MASKMARGIN(%);double;output;-32768;10;90;1;0;0;1;
新增一笔资料为:ID=2382;385;ER(DB);double;output;-32768;3.5;6;1;0;0;1;
新增一笔资料为:ID=2383;385;ISOPTICALEYEORELECEYE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2384;385;CROSSING(%);double;output;-32768;40;60;1;0;0;1;
新增一笔资料为:ID=2385;386;DMITEMPERR(C);double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2386;386;DMITEMP(C);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2387;387;DMIVCCERR(V);double;output;-32768;-0.165;0.165;1;0;0;1;
新增一笔资料为:ID=2388;387;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2389;388;ICC(MA);double;output;-32768;500;1050;1;0;0;1;
新增一笔资料为:ID=2390;389;DMITXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2391;389;CURRENTTXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2392;389;DMITXPOWERERR(DB);double;output;-32768;-2;2;1;0;0;1;
新增一笔资料为:ID=2393;390;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
新增一笔资料为:ID=2394;391;ARRAYLISTRXINPUTPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2395;391;DMIRXPWRMAXERRPOINT(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2396;391;DMIRXPWRMAXERR;double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2397;392;CSENTARGETBER;double;input;1.0E-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2398;392;COEFCSENSUBSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2399;392;CSEN(DBM);double;output;-32768;-20;-11;1;0;0;1;
新增一笔资料为:ID=2400;392;SEARCHTARGETBERSUBSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2401;392;SEARCHTARGETBERADDSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2402;392;SEARCHTARGETBERLL;double;input;1E-8;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2403;392;SEARCHTARGETBERUL;double;input;1.00E-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2404;392;CSENSTARTINGRXPWR(DBM);double;input;-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2405;392;CSENALIGNRXPWR(DBM);double;input;-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2406;392;COEFCSENADDSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2407;392;IsBerQuickTest;bool;input;false;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2408;393;LOSA;double;output;-32768;-29;-16;1;0;0;1;
新增一笔资料为:ID=2409;393;LOSD;double;output;-32768;-29;-13;1;0;0;1;
新增一笔资料为:ID=2410;393;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2411;393;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2412;393;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2413;393;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2414;393;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
新增一笔资料为:ID=2415;393;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2416;394;AP(DBM);double;output;-32768;0;3;1;0;0;1;
新增一笔资料为:ID=2417;394;JITTERPP(PS);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2418;394;TXOMA(UW);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2419;394;FALLTIME(PS);double;output;-32768;-32768;65;1;0;0;1;
新增一笔资料为:ID=2420;394;RISETIME(PS);double;output;-32768;-32768;100;1;0;0;1;
新增一笔资料为:ID=2421;394;JITTERRMS(PS);double;output;-32768;-32768;30;1;0;0;1;
新增一笔资料为:ID=2422;394;MASKMARGIN(%);double;output;-32768;10;90;1;0;0;1;
新增一笔资料为:ID=2423;394;ER(DB);double;output;-32768;3.5;6;1;0;0;1;
新增一笔资料为:ID=2424;394;ISOPTICALEYEORELECEYE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2425;394;CROSSING(%);double;output;-32768;40;60;1;0;0;1;
新增一笔资料为:ID=2426;395;DMITXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2427;395;CURRENTTXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2428;395;DMITXPOWERERR(DB);double;output;-32768;-2;2;1;0;0;1;
新增一笔资料为:ID=2429;396;DMITEMPERR(C);double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2430;396;DMITEMP(C);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2431;397;ICC(MA);double;output;-32768;500;1050;1;0;0;1;
新增一笔资料为:ID=2432;398;DMIVCCERR(V);double;output;-32768;-0.165;0.165;1;0;0;1;
新增一笔资料为:ID=2433;398;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2434;399;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
新增一笔资料为:ID=2435;400;ARRAYLISTRXINPUTPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2436;400;DMIRXPWRMAXERRPOINT(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2437;400;DMIRXPWRMAXERR;double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2438;401;CSENTARGETBER;double;input;1.0E-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2439;401;COEFCSENSUBSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2440;401;CSEN(DBM);double;output;-32768;-20;-11;1;0;0;1;
新增一笔资料为:ID=2441;401;SEARCHTARGETBERSUBSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2442;401;SEARCHTARGETBERADDSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2443;401;SEARCHTARGETBERLL;double;input;1E-8;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2444;401;SEARCHTARGETBERUL;double;input;1.00E-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2445;401;CSENSTARTINGRXPWR(DBM);double;input;-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2446;401;CSENALIGNRXPWR(DBM);double;input;-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2447;401;COEFCSENADDSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2448;401;IsBerQuickTest;bool;input;false;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2449;402;LOSA;double;output;-32768;-29;-16;1;0;0;1;
新增一笔资料为:ID=2450;402;LOSD;double;output;-32768;-29;-13;1;0;0;1;
新增一笔资料为:ID=2451;402;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2452;402;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2453;402;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2454;402;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2455;402;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
新增一笔资料为:ID=2456;402;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2457;403;AP(DBM);double;output;-32768;0;3;1;0;0;1;
新增一笔资料为:ID=2458;403;JITTERPP(PS);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2459;403;TXOMA(UW);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2460;403;FALLTIME(PS);double;output;-32768;-32768;65;1;0;0;1;
新增一笔资料为:ID=2461;403;RISETIME(PS);double;output;-32768;-32768;100;1;0;0;1;
新增一笔资料为:ID=2462;403;JITTERRMS(PS);double;output;-32768;-32768;30;1;0;0;1;
新增一笔资料为:ID=2463;403;MASKMARGIN(%);double;output;-32768;10;90;1;0;0;1;
新增一笔资料为:ID=2464;403;ER(DB);double;output;-32768;3.5;6;1;0;0;1;
新增一笔资料为:ID=2465;403;ISOPTICALEYEORELECEYE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2466;403;CROSSING(%);double;output;-32768;40;60;1;0;0;1;
新增一笔资料为:ID=2467;404;DMITXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2468;404;CURRENTTXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2469;404;DMITXPOWERERR(DB);double;output;-32768;-2;2;1;0;0;1;
新增一笔资料为:ID=2470;405;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
新增一笔资料为:ID=2471;406;DMITEMPERR(C);double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2472;406;DMITEMP(C);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2473;407;DMIVCCERR(V);double;output;-32768;-0.165;0.165;1;0;0;1;
新增一笔资料为:ID=2474;407;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2475;408;ICC(MA);double;output;-32768;100;2000;1;0;0;1;
新增一笔资料为:ID=2476;409;ARRAYLISTRXINPUTPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2477;409;DMIRXPWRMAXERRPOINT(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2478;409;DMIRXPWRMAXERR;double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2479;410;CSENTARGETBER;double;input;1.0E-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2480;410;COEFCSENSUBSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2481;410;CSEN(DBM);double;output;-32768;-20;-11;1;0;0;1;
新增一笔资料为:ID=2482;410;SEARCHTARGETBERSUBSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2483;410;SEARCHTARGETBERADDSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2484;410;SEARCHTARGETBERLL;double;input;1E-8;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2485;410;SEARCHTARGETBERUL;double;input;1.00E-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2486;410;CSENSTARTINGRXPWR(DBM);double;input;-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2487;410;CSENALIGNRXPWR(DBM);double;input;-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2488;410;COEFCSENADDSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2489;410;IsBerQuickTest;bool;input;false;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2490;411;LOSA;double;output;-32768;-29;-16;1;0;0;1;
新增一笔资料为:ID=2491;411;LOSD;double;output;-32768;-29;-13;1;0;0;1;
新增一笔资料为:ID=2492;411;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2493;411;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2494;411;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2495;411;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2496;411;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
新增一笔资料为:ID=2497;411;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2498;412;FIXEDMODDAC(MA);UInt16;input;300;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2499;412;ARRAYLISTXDMICOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2500;412;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2501;412;IBIASADCORTXPOWERADC;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2502;412;ARRAYIBIAS(MA);ArrayList;input;300,400,500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2503;412;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2504;412;ISTEMPRELATIVE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2505;412;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2506;412;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2507;412;HighestCalTemp;double;input;60;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2508;412;LowestCalTemp;double;input;20;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2509;412;ISNEWALGORITHM;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2510;413;IMODMIN(MA);UInt16;input;100;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2511;413;ARRAYLISTTXMODCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2512;413;1STOR2STORPIDTXLOP;byte;input;0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2513;413;1STOR2STORPIDER;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2514;413;ISOPENLOOPORCLOSELOOPORBOTH;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2515;413;IMODSTART(MA);UInt16;input;250;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2516;413;IMODSTEP;byte;input;64;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2517;413;IMODMETHOD;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2518;413;TXERTOLERANCE(DB);double;input;0.2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2519;413;TXERTARGET(DB);double;input;4;3.5;6;1;0;1;0;
新增一笔资料为:ID=2520;413;ARRAYLISTTXPOWERCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2521;413;IBIASMETHOD;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2522;413;IBIASSTEP(MA);byte;input;64;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2523;413;IBIASSTART(MA);UInt16;input;600;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2524;413;IBIASMIN(MA);UInt16;input;400;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2525;413;IBIASMAX(MA);UInt16;input;2500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2526;413;FIXEDMOD(MA);UInt16;input;500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2527;413;TXLOPTOLERANCE(UW);double;input;100;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2528;413;TXLOPTARGET(UW);double;input;900;1000;2000;1;0;1;0;
新增一笔资料为:ID=2529;413;IMODMAX(MA);UInt16;input;700;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2530;413;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2531;413;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2532;413;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2533;413;PIDCOEFARRAY;ArrayList;input;0.01,0.005,0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2534;413;FIXEDIBIAS(MA);UInt32;input;280;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2535;413;FixedIBiasArray;ArrayList;input;280,280,280,280;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2536;413;FixedModArray;ArrayList;input;500,500,500,500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2537;414;1STOR2STORPID;byte;input;1;0;0;0;0;0;0;
新增一笔资料为:ID=2538;414;ARRAYLISTDMITEMPCOEF;ArrayList;output;-32768;0;0;0;1;0;0;
新增一笔资料为:ID=2539;415;GENERALVCC(V);double;input;3.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2540;415;ARRAYLISTVCC(V);ArrayList;input;3.1,3.3,3.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2541;415;ARRAYLISTDMIVCCCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2542;415;1STOR2STORPID;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2543;416;ARRAYLISTDMIRXCOEF;ArrayList;output;;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2544;416;ARRAYLISTRXPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2545;416;HasOffset;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2546;416;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2547;417;LOSDVOLTAGETUNESTEP(V);byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2548;417;LOSAVOLTAGESTARTVALUE(V);UInt16;input;14;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2549;417;IsAdjustLos;bool;input;True;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2550;417;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2551;417;LOSDVOLTAGEUPERLIMIT(V);UInt16;input;30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2552;417;LOSDVOLTAGESTARTVALUE(V);UInt16;input;14;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2553;417;LOSDINPUTPOWER;double;input;-21;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2554;417;LOSAVOLTAGETUNESTEP(V);byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2555;417;LOSAVOLTAGEUPERLIMIT(V);UInt16;input;30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2556;417;LosValue(V);UINT32;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2557;417;LOSDVOLTAGELOWLIMIT(V);UInt16;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2558;417;LOSAINPUTPOWER;double;input;-21;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2559;417;LOSAVOLTAGELOWLIMIT(V);UInt16;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2560;418;FIXEDMODDAC(MA);UInt16;input;300;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2561;418;ARRAYLISTXDMICOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2562;418;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2563;418;IBIASADCORTXPOWERADC;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2564;418;ARRAYIBIAS(MA);ArrayList;input;300,400,500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2565;418;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2566;418;ISTEMPRELATIVE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2567;418;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2568;418;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2569;418;HighestCalTemp;double;input;60;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2570;418;LowestCalTemp;double;input;20;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2571;418;ISNEWALGORITHM;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2572;419;IMODMIN(MA);UInt16;input;100;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2573;419;ARRAYLISTTXMODCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2574;419;1STOR2STORPIDTXLOP;byte;input;0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2575;419;1STOR2STORPIDER;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2576;419;ISOPENLOOPORCLOSELOOPORBOTH;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2577;419;IMODSTART(MA);UInt16;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2578;419;IMODSTEP;byte;input;64;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2579;419;IMODMETHOD;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2580;419;TXERTOLERANCE(DB);double;input;0.2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2581;419;TXERTARGET(DB);double;input;4;3.5;6;1;0;1;0;
新增一笔资料为:ID=2582;419;ARRAYLISTTXPOWERCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2583;419;IBIASMETHOD;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2584;419;IBIASSTEP(MA);byte;input;64;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2585;419;IBIASSTART(MA);UInt16;input;600;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2586;419;IBIASMIN(MA);UInt16;input;400;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2587;419;IBIASMAX(MA);UInt16;input;2500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2588;419;FIXEDMOD(MA);UInt16;input;500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2589;419;TXLOPTOLERANCE(UW);double;input;100;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2590;419;TXLOPTARGET(UW);double;input;900;1000;2000;1;0;1;0;
新增一笔资料为:ID=2591;419;IMODMAX(MA);UInt16;input;1000;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2592;419;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2593;419;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2594;419;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2595;419;PIDCOEFARRAY;ArrayList;input;0.01,0.005,0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2596;419;FIXEDIBIAS(MA);UInt32;input;280;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2597;419;FixedIBiasArray;ArrayList;input;280,280,280,280;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2598;419;FixedModArray;ArrayList;input;500,500,500,500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2599;420;1STOR2STORPID;byte;input;1;0;0;0;0;0;0;
新增一笔资料为:ID=2600;420;ARRAYLISTDMITEMPCOEF;ArrayList;output;-32768;0;0;0;1;0;0;
新增一笔资料为:ID=2601;421;GENERALVCC(V);double;input;3.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2602;421;ARRAYLISTVCC(V);ArrayList;input;3.1,3.3,3.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2603;421;ARRAYLISTDMIVCCCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2604;421;1STOR2STORPID;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2605;422;FIXEDMODDAC(MA);UInt16;input;300;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2606;422;ARRAYLISTXDMICOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2607;422;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2608;422;IBIASADCORTXPOWERADC;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2609;422;ARRAYIBIAS(MA);ArrayList;input;300,400,500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2610;422;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2611;422;ISTEMPRELATIVE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2612;422;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2613;422;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2614;422;HighestCalTemp;double;input;60;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2615;422;LowestCalTemp;double;input;20;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2616;422;ISNEWALGORITHM;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2617;423;IMODMIN(MA);UInt16;input;100;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2618;423;ARRAYLISTTXMODCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2619;423;1STOR2STORPIDTXLOP;byte;input;0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2620;423;1STOR2STORPIDER;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2621;423;ISOPENLOOPORCLOSELOOPORBOTH;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2622;423;IMODSTART(MA);UInt16;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2623;423;IMODSTEP;byte;input;64;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2624;423;IMODMETHOD;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2625;423;TXERTOLERANCE(DB);double;input;0.2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2626;423;TXERTARGET(DB);double;input;4;3.5;6;1;0;1;0;
新增一笔资料为:ID=2627;423;ARRAYLISTTXPOWERCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2628;423;IBIASMETHOD;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2629;423;IBIASSTEP(MA);byte;input;64;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2630;423;IBIASSTART(MA);UInt16;input;600;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2631;423;IBIASMIN(MA);UInt16;input;400;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2632;423;IBIASMAX(MA);UInt16;input;2500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2633;423;FIXEDMOD(MA);UInt16;input;500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2634;423;TXLOPTOLERANCE(UW);double;input;100;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2635;423;TXLOPTARGET(UW);double;input;900;1000;2000;1;0;1;0;
新增一笔资料为:ID=2636;423;IMODMAX(MA);UInt16;input;1000;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2637;423;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2638;423;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2639;423;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2640;423;PIDCOEFARRAY;ArrayList;input;0.01,0.005,0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2641;423;FIXEDIBIAS(MA);UInt32;input;280;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2642;423;FixedIBiasArray;ArrayList;input;280,280,280,280;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2643;423;FixedModArray;ArrayList;input;500,500,500,500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2644;424;1STOR2STORPID;byte;input;1;0;0;0;0;0;0;
新增一笔资料为:ID=2645;424;ARRAYLISTDMITEMPCOEF;ArrayList;output;-32768;0;0;0;1;0;0;
新增一笔资料为:ID=2646;425;GENERALVCC(V);double;input;3.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2647;425;ARRAYLISTVCC(V);ArrayList;input;3.1,3.3,3.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2648;425;ARRAYLISTDMIVCCCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2649;425;1STOR2STORPID;byte;input;1;-32768;32767;0;0;0;0;
**表[TopoEquipment]修改如下**
修改前资料为:SEQ:1;
修改前资料为:SEQ:2;
修改前资料为:SEQ:1;
修改前资料为:SEQ:3;
修改前资料为:SEQ:1;
修改前资料为:SEQ:4;
修改前资料为:SEQ:1;
修改前资料为:SEQ:5;
修改前资料为:SEQ:1;
修改前资料为:SEQ:6;
修改前资料为:SEQ:1;
修改前资料为:SEQ:7;
修改前资料为:SEQ:1;
修改前资料为:SEQ:8;
新增一笔资料为:ID=155;23;1;Powersupply;E3631_97_Powersupply;0;
新增一笔资料为:ID=156;23;2;Scope;FLEX86100_98_Scope;0;
新增一笔资料为:ID=157;23;3;PPG;MP1800PPG_99_PPG;0;
新增一笔资料为:ID=158;23;4;Thermocontroller;TPO4300_100_Thermocontroller;0;
新增一笔资料为:ID=159;23;5;OpticalSwitch;AQ2211OpticalSwitch_101_OpticalSwitch;0;
新增一笔资料为:ID=160;23;6;Attennuator;AQ2211Atten_102_Attennuator;0;
新增一笔资料为:ID=161;23;7;ErrorDetector;MP1800ED_103_ErrorDetector;0;
新增一笔资料为:ID=162;23;8;OpticalSwitch;AQ2211OpticalSwitch_104_OpticalSwitch;0;
**表[TopoEquipmentParameter]修改如下**
新增一笔资料为:ID=2193;97;opendelay;0;
新增一笔资料为:ID=2194;97;closedelay;0;
新增一笔资料为:ID=2195;155;Addr;5;
新增一笔资料为:ID=2196;155;IOType;GPIB;
新增一笔资料为:ID=2197;155;Reset;false;
新增一笔资料为:ID=2198;155;Name;E3631;
新增一笔资料为:ID=2199;155;DutChannel;1;
新增一笔资料为:ID=2200;155;OptSourceChannel;2;
新增一笔资料为:ID=2201;155;DutVoltage;3.5;
新增一笔资料为:ID=2202;155;DutCurrent;2.5;
新增一笔资料为:ID=2203;155;OptVoltage;3.5;
新增一笔资料为:ID=2204;155;OptCurrent;1.5;
新增一笔资料为:ID=2205;155;voltageoffset;0.2;
新增一笔资料为:ID=2206;155;currentoffset;0;
新增一笔资料为:ID=2207;156;Addr;7;
新增一笔资料为:ID=2208;156;IOType;GPIB;
新增一笔资料为:ID=2209;156;Reset;false;
新增一笔资料为:ID=2210;156;Name;FLEX86100;
新增一笔资料为:ID=2211;156;configFilePath;1;
新增一笔资料为:ID=2212;156;FlexDcaDataRate;25.78125E+9;
新增一笔资料为:ID=2213;156;FilterSwitch;1;
新增一笔资料为:ID=2214;156;FlexFilterFreq;25.78125;
新增一笔资料为:ID=2215;156;triggerSource;0;
新增一笔资料为:ID=2216;156;FlexTriggerBwlimit;2;
新增一笔资料为:ID=2217;156;opticalSlot;3;
新增一笔资料为:ID=2218;156;elecSlot;4;
新增一笔资料为:ID=2219;156;FlexOptChannel;1;
新增一笔资料为:ID=2220;156;FlexElecChannel;1;
新增一笔资料为:ID=2221;156;FlexDcaWavelength;1;
新增一笔资料为:ID=2222;156;opticalAttSwitch;1;
新增一笔资料为:ID=2223;156;FlexDcaAtt;0;
新增一笔资料为:ID=2224;156;erFactor;0;
新增一笔资料为:ID=2225;156;FlexScale;300;
新增一笔资料为:ID=2226;156;FlexOffset;300;
新增一笔资料为:ID=2227;156;Threshold;0;
新增一笔资料为:ID=2228;156;reference;0;
新增一笔资料为:ID=2229;156;precisionTimebaseModuleSlot;1;
新增一笔资料为:ID=2230;156;precisionTimebaseSynchMethod;1;
新增一笔资料为:ID=2231;156;precisionTimebaseRefClk;6.445e+9;
新增一笔资料为:ID=2232;156;rapidEyeSwitch;1;
新增一笔资料为:ID=2233;156;marginType;1;
新增一笔资料为:ID=2234;156;marginHitType;0;
新增一笔资料为:ID=2235;156;marginHitRatio;5e-006;
新增一笔资料为:ID=2236;156;marginHitCount;0;
新增一笔资料为:ID=2237;156;acqLimitType;0;
新增一笔资料为:ID=2238;156;acqLimitNumber;1000;
新增一笔资料为:ID=2239;156;opticalMaskName;c:\scope\masks\25.78125_100GBASE-LR4_Tx_Optical_D31.MSK;
新增一笔资料为:ID=2240;156;elecMaskName;c:\Eye;
新增一笔资料为:ID=2241;156;opticalEyeSavePath;D:\Eye\;
新增一笔资料为:ID=2242;156;elecEyeSavePath;D:\Eye\;
新增一笔资料为:ID=2243;156;ERFACTORSWITCH;1;
新增一笔资料为:ID=2244;157;Addr;1;
新增一笔资料为:ID=2245;157;IOType;GPIB;
新增一笔资料为:ID=2246;157;Reset;false;
新增一笔资料为:ID=2247;157;Name;MP1800PPG;
新增一笔资料为:ID=2248;157;dataRate;25.78128;
新增一笔资料为:ID=2249;157;dataLevelGuardAmpMax;1;
新增一笔资料为:ID=2250;157;dataLevelGuardOffsetMax;0;
新增一笔资料为:ID=2251;157;dataLevelGuardOffsetMin;0;
新增一笔资料为:ID=2252;157;dataLevelGuardSwitch;0;
新增一笔资料为:ID=2253;157;dataAmplitude;0.5;
新增一笔资料为:ID=2254;157;dataCrossPoint;50;
新增一笔资料为:ID=2255;157;configFilePath;0;
新增一笔资料为:ID=2256;157;slot;1;
新增一笔资料为:ID=2257;157;clockSource;0;
新增一笔资料为:ID=2258;157;auxOutputClkDiv;0;
新增一笔资料为:ID=2259;157;prbsLength;31;
新增一笔资料为:ID=2260;157;patternType;0;
新增一笔资料为:ID=2261;157;dataSwitch;1;
新增一笔资料为:ID=2262;157;dataTrackingSwitch;1;
新增一笔资料为:ID=2263;157;dataAcModeSwitch;0;
新增一笔资料为:ID=2264;157;dataLevelMode;0;
新增一笔资料为:ID=2265;157;clockSwitch;1;
新增一笔资料为:ID=2266;157;outputSwitch;1;
新增一笔资料为:ID=2267;157;TotalChannel;4;
新增一笔资料为:ID=2268;158;Addr;23;
新增一笔资料为:ID=2269;158;IOType;GPIB;
新增一笔资料为:ID=2270;158;Reset;False;
新增一笔资料为:ID=2271;158;Name;TPO4300;
新增一笔资料为:ID=2272;158;FLSE;14;
新增一笔资料为:ID=2273;158;ULIM;90;
新增一笔资料为:ID=2274;158;LLIM;-20;
新增一笔资料为:ID=2275;158;Sensor;1;
新增一笔资料为:ID=2276;159;Addr;20;
新增一笔资料为:ID=2277;159;IOType;GPIB;
新增一笔资料为:ID=2278;159;Reset;false;
新增一笔资料为:ID=2279;159;Name;AQ2011OpticalSwitch;
新增一笔资料为:ID=2280;159;OpticalSwitchSlot;1;
新增一笔资料为:ID=2281;159;SwitchChannel;1;
新增一笔资料为:ID=2282;159;ToChannel;1;
新增一笔资料为:ID=2283;160;Addr;20;
新增一笔资料为:ID=2284;160;IOType;GPIB;
新增一笔资料为:ID=2285;160;Reset;false;
新增一笔资料为:ID=2286;160;Name;AQ2211Atten;
新增一笔资料为:ID=2287;160;TOTALCHANNEL;4;
新增一笔资料为:ID=2288;160;AttValue;20;
新增一笔资料为:ID=2289;160;AttSlot;2;
新增一笔资料为:ID=2290;160;WAVELENGTH;1310,1310,1310,1310;
新增一笔资料为:ID=2291;160;AttChannel;1;
新增一笔资料为:ID=2292;161;Addr;1;
新增一笔资料为:ID=2293;161;IOType;GPIB;
新增一笔资料为:ID=2294;161;Reset;false;
新增一笔资料为:ID=2295;161;Name;MP1800ED;
新增一笔资料为:ID=2296;161;slot;3;
新增一笔资料为:ID=2297;161;TotalChannel;4;
新增一笔资料为:ID=2298;161;currentChannel;1;
新增一笔资料为:ID=2299;161;dataInputInterface;2;
新增一笔资料为:ID=2300;161;prbsLength;31;
新增一笔资料为:ID=2301;161;errorResultZoom;1;
新增一笔资料为:ID=2302;161;edGatingMode;1;
新增一笔资料为:ID=2303;161;edGatingUnit;0;
新增一笔资料为:ID=2304;161;edGatingTime;5;
新增一笔资料为:ID=2305;162;Addr;20;
新增一笔资料为:ID=2306;162;IOType;GPIB;
新增一笔资料为:ID=2307;162;Reset;false;
新增一笔资料为:ID=2308;162;Name;AQ2011OpticalSwitch;
新增一笔资料为:ID=2309;162;OpticalSwitchSlot;3;
新增一笔资料为:ID=2310;162;SwitchChannel;1;
新增一笔资料为:ID=2311;162;ToChannel;1;
新增一笔资料为:ID=2312;155;opendelay;0;
新增一笔资料为:ID=2313;155;closedelay;0;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (92, N'terry.yin', CAST(0x0000A3D500E7DDD0 AS DateTime), CAST(0x0000A3D500E7F8C4 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:terry.yin于2014/10/31 14:04:28修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
新增一笔资料为:ID=2314;125;opendelay;0;
新增一笔资料为:ID=2315;125;closedelay;0;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (93, N'terry.yin', CAST(0x0000A3D500E82678 AS DateTime), CAST(0x0000A3D500E83DE8 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:terry.yin于2014/10/31 14:05:28修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
修改前资料为:ID=125SEQ:1;
修改前资料为:ID=125SEQ:3;
修改前资料为:ID=127SEQ:2;
修改前资料为:ID=127SEQ:1;
修改前资料为:ID=152SEQ:3;
修改前资料为:ID=152SEQ:2;
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (94, N'terry.yin', CAST(0x0000A3D500EB3548 AS DateTime), CAST(0x0000A3D500EB503C AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (95, N'terry.yin', CAST(0x0000A3D500EBA820 AS DateTime), CAST(0x0000A3D500EBBAE0 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (96, N'terry.yin', CAST(0x0000A3D500F45F60 AS DateTime), CAST(0x0000A3D50100890C AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/10/31 14:50:10修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
修改前资料为:Role:0;
修改前资料为:Role:1;
修改前资料为:Role:0;
修改前资料为:Role:2;
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/31 14:53:29修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/31 14:53:59修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (97, N'terry.yin', CAST(0x0000A3D500FB3AD8 AS DateTime), CAST(0x0000A3D500FB6AE4 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:terry.yin于2014/10/31 15:15:19修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
修改前资料为:ID=2193ItemValue:0;
修改前资料为:ID=2193ItemValue:2000;
修改前资料为:ID=2194ItemValue:0;
修改前资料为:ID=2194ItemValue:500;
修改前资料为:ID=2312ItemValue:0;
修改前资料为:ID=2312ItemValue:2000;
修改前资料为:ID=2313ItemValue:0;
修改前资料为:ID=2313ItemValue:500;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (98, N'terry.yin', CAST(0x0000A3D501018884 AS DateTime), CAST(0x0000A3D50107F250 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/10/31 15:56:42修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/10/31 15:57:24修改==
**表[TopoTestPlan]修改如下**
已经删除资料:ID=19;1;terryTEST;1;1;1;1;
**表[TopoTestControl]修改如下**
已经删除资料:ID=72;19;2;1;2;2;2;2;2;2=2;False;
**表[TopoTestModel]修改如下**
已经删除资料:ID=339;72;TestIBiasDmi;1;5;;False;
**表[TopoTestParameter]修改如下**
已经删除资料:ID=2064;339;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
**表[TopoEquipment]修改如下**
已经删除资料:ID=125;19;3;Powersupply;E3631_125_Powersupply;0;
已经删除资料:ID=127;19;1;ErrorDetector;N490XED_127_ErrorDetector;0;
已经删除资料:ID=152;19;2;PPG;MP1800PPG_152_PPG;0;
**表[TopoEquipmentParameter]修改如下**
已经删除资料:ID=1760;125;Addr;5;
已经删除资料:ID=1761;125;IOType;GPIB;
已经删除资料:ID=1762;125;Reset;False;
已经删除资料:ID=1763;125;Name;E3631;
已经删除资料:ID=1764;125;DutChannel;1;
已经删除资料:ID=1765;125;OptSourceChannel;2;
已经删除资料:ID=1766;125;DutVoltage;3.3;
已经删除资料:ID=1767;125;DutCurrent;1.5;
已经删除资料:ID=1768;125;OptVoltage;3.3;
已经删除资料:ID=1769;125;OptCurrent;1.5;
已经删除资料:ID=1770;125;voltageoffset;0;
已经删除资料:ID=1771;125;currentoffset;0;
已经删除资料:ID=1790;127;Addr;5;
已经删除资料:ID=1791;127;IOType;GPIB;
已经删除资料:ID=1792;127;Reset;False;
已经删除资料:ID=1793;127;Name;N490xED;
已经删除资料:ID=1794;127;PRBS;31;
已经删除资料:ID=1795;127;CDRSwitch;false;
已经删除资料:ID=1796;127;CDRFreq;10312500000;
已经删除资料:ID=2148;152;Addr;1;
已经删除资料:ID=2149;152;IOType;GPIB;
已经删除资料:ID=2150;152;Reset;false;
已经删除资料:ID=2151;152;Name;MP1800PPG;
已经删除资料:ID=2152;152;dataRate;25.78125;
已经删除资料:ID=2153;152;dataLevelGuardAmpMax;1000;
已经删除资料:ID=2154;152;dataLevelGuardOffsetMax;1000;
已经删除资料:ID=2155;152;dataLevelGuardOffsetMin;-1000;
已经删除资料:ID=2156;152;dataLevelGuardSwitch;1;
已经删除资料:ID=2157;152;dataAmplitude;500;
已经删除资料:ID=2158;152;dataCrossPoint;50;
已经删除资料:ID=2159;152;configFilePath;"";
已经删除资料:ID=2160;152;slot;1;
已经删除资料:ID=2161;152;clockSource;0;
已经删除资料:ID=2162;152;auxOutputClkDiv;8;
已经删除资料:ID=2163;152;prbsLength;31;
已经删除资料:ID=2164;152;patternType;0;
已经删除资料:ID=2165;152;dataSwitch;1;
已经删除资料:ID=2166;152;dataTrackingSwitch;1;
已经删除资料:ID=2167;152;dataAcModeSwitch;1;
已经删除资料:ID=2168;152;dataLevelMode;0;
已经删除资料:ID=2169;152;clockSwitch;1;
已经删除资料:ID=2170;152;outputSwitch;1;
已经删除资料:ID=2171;152;TotalChannel;4;
已经删除资料:ID=2314;125;opendelay;0;
已经删除资料:ID=2315;125;closedelay;0;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (99, N'terry.yin', CAST(0x0000A3D50101A828 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登入', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (100, N'terry.yin', CAST(0x0000A3D5010248B4 AS DateTime), CAST(0x0000A3D50108351C AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'数据更新中出现问题!系统 恢复到更新前的状态...2014/10/31 15:51:52:耗时: 0分15.0秒')
GO
print 'Processed 100 total records'
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (101, N'terry.yin', CAST(0x0000A3D501058B14 AS DateTime), CAST(0x0000A3D50105A608 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (102, N'terry.yin', CAST(0x0000A3D50105C354 AS DateTime), CAST(0x0000A3D50105C6D8 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (103, N'terry.yin', CAST(0x0000A3D50105DD1C AS DateTime), CAST(0x0000A3D5010C5750 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'数据更新中出现问题!系统 恢复到更新前的状态...2014/10/31 16:16:44:耗时: 18分1.0秒')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (104, N'terry.yin', CAST(0x0000A3D50108A920 AS DateTime), CAST(0x0000A3D5010E37C8 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'==User:terry.yin于2014/10/31 16:09:17修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:ID=2292ItemValue:64;
修改前资料为:ID=2292ItemValue:32;
修改前资料为:ID=2293ItemValue:600;
修改前资料为:ID=2293ItemValue:40;
修改前资料为:ID=2294ItemValue:400;
修改前资料为:ID=2294ItemValue:35;
修改前资料为:ID=2295ItemValue:2500;
修改前资料为:ID=2295ItemValue:45;
修改前资料为:ID=2297ItemValue:100;
修改前资料为:ID=2297ItemValue:500;
修改前资料为:ID=2298ItemValue:900;
修改前资料为:ID=2298ItemValue:1500;
修改前资料为:ID=2305ItemValue:280,280,280,280;
修改前资料为:ID=2305ItemValue:520,520,520,520;
修改前资料为:ID=2306ItemValue:500,500,500,500;
修改前资料为:ID=2306ItemValue:0,0,0,0;
修改前资料为:ID=2354ItemValue:64;
修改前资料为:ID=2354ItemValue:32;
修改前资料为:ID=2355ItemValue:600;
修改前资料为:ID=2355ItemValue:40;
修改前资料为:ID=2356ItemValue:400;
修改前资料为:ID=2356ItemValue:35;
修改前资料为:ID=2357ItemValue:2500;
修改前资料为:ID=2357ItemValue:45;
修改前资料为:ID=2359ItemValue:100;
修改前资料为:ID=2359ItemValue:500;
修改前资料为:ID=2360ItemValue:900;
修改前资料为:ID=2360ItemValue:1500;
修改前资料为:ID=2367ItemValue:280,280,280,280;
修改前资料为:ID=2367ItemValue:520,520,520,520;
修改前资料为:ID=2368ItemValue:500,500,500,500;
修改前资料为:ID=2368ItemValue:0,0,0,0;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (105, N'terry.yin', CAST(0x0000A3D50110D168 AS DateTime), CAST(0x0000A3D501133070 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/10/31 16:39:15修改==
**表[TopoTestPlan]修改如下**
已经删除资料:ID=23;3;CGR4-V5;1;1;0;SN_CHECK = 0;ApcStyle=1;
**表[TopoTestControl]修改如下**
已经删除资料:ID=82;23;FMT-3.3-20-1;6;0;20;3.3;31;25.78125e+9;TempSleep=20;False;
已经删除资料:ID=83;23;FMT-3.3-40-1_4;5;0;40;3.3;31;25.78125E+9;TempSleep=60;False;
已经删除资料:ID=84;23;FMT-60-3.3-1_4;3;0;60;3.3;31;25.78125e+9;TempSleep=60;False;
已经删除资料:ID=85;23;LP-3.3-20-1-4;1;0;20;3.3;31;25.78125e+9;TempSleep=20;False;
已经删除资料:ID=86;23;LP-3.3-40-1-4;4;0;40;3.3;31;25.78125e+9;TempSleep=60;False;
已经删除资料:ID=87;23;LP-3.3-60-1-4;2;0;60;3.3;31;25.78125e+9;TempSleep=60;False;
**表[TopoTestModel]修改如下**
已经删除资料:ID=385;82;TestEye;7;3;E3631_97_Powersupply,FLEX86100_98_Scope;False;
已经删除资料:ID=386;82;TestTempDmi;10;13;E3631_97_Powersupply,TPO4300_100_Thermocontroller;False;
已经删除资料:ID=387;82;TestVccDmi;13;14;E3631_97_Powersupply;False;
已经删除资料:ID=388;82;TestIcc;14;15;E3631_97_Powersupply;False;
已经删除资料:ID=389;82;TestTxPowerDmi;9;4;E3631_97_Powersupply,FLEX86100_98_Scope;False;
已经删除资料:ID=390;82;TestIBiasDmi;15;5;E3631_97_Powersupply;False;
已经删除资料:ID=391;82;TestRxPowerDmi;16;9;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
已经删除资料:ID=392;82;TestBer;17;10;E3631_97_Powersupply,AQ2211Atten_102_Attennuator,MP1800ED_103_ErrorDetector;False;
已经删除资料:ID=393;82;TestRXLosAD;18;8;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
已经删除资料:ID=394;83;TestEye;1;3;E3631_97_Powersupply,FLEX86100_98_Scope;False;
已经删除资料:ID=395;83;TestTxPowerDmi;2;4;E3631_97_Powersupply,FLEX86100_98_Scope;False;
已经删除资料:ID=396;83;TestTempDmi;6;13;E3631_97_Powersupply,TPO4300_100_Thermocontroller;False;
已经删除资料:ID=397;83;TestIcc;7;15;E3631_97_Powersupply;False;
已经删除资料:ID=398;83;TestVccDmi;8;14;E3631_97_Powersupply;False;
已经删除资料:ID=399;83;TestIBiasDmi;9;5;E3631_97_Powersupply;False;
已经删除资料:ID=400;83;TestRxPowerDmi;10;9;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
已经删除资料:ID=401;83;TestBer;11;10;E3631_97_Powersupply,AQ2211Atten_102_Attennuator,MP1800ED_103_ErrorDetector;False;
已经删除资料:ID=402;83;TestRXLosAD;12;8;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
已经删除资料:ID=403;84;TestEye;1;3;E3631_97_Powersupply,FLEX86100_98_Scope;False;
已经删除资料:ID=404;84;TestTxPowerDmi;2;4;E3631_97_Powersupply,FLEX86100_98_Scope;False;
已经删除资料:ID=405;84;TestIBiasDmi;5;5;E3631_97_Powersupply;False;
已经删除资料:ID=406;84;TestTempDmi;7;13;E3631_97_Powersupply,TPO4300_100_Thermocontroller;False;
已经删除资料:ID=407;84;TestVccDmi;8;14;E3631_97_Powersupply;False;
已经删除资料:ID=408;84;TestIcc;9;15;E3631_97_Powersupply;False;
已经删除资料:ID=409;84;TestRxPowerDmi;11;9;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
已经删除资料:ID=410;84;TestBer;12;10;E3631_97_Powersupply,AQ2211Atten_102_Attennuator,MP1800ED_103_ErrorDetector;False;
已经删除资料:ID=411;84;TestRXLosAD;13;8;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
已经删除资料:ID=412;85;AdjustTxPowerDmi;1;2;E3631_97_Powersupply,FLEX86100_98_Scope;False;
已经删除资料:ID=413;85;AdjustEye;2;1;E3631_97_Powersupply,FLEX86100_98_Scope;False;
已经删除资料:ID=414;85;CalTempDminoProcessingCoef;3;19;E3631_97_Powersupply,TPO4300_100_Thermocontroller;False;
已经删除资料:ID=415;85;CalVccDminoProcessingCoef;4;20;E3631_97_Powersupply;False;
已经删除资料:ID=416;85;CalRxDminoProcessingCoef;5;18;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
已经删除资料:ID=417;85;AdjustLos;6;6;E3631_97_Powersupply,AQ2211Atten_102_Attennuator;False;
已经删除资料:ID=418;86;AdjustTxPowerDmi;1;2;E3631_97_Powersupply,FLEX86100_98_Scope;False;
已经删除资料:ID=419;86;AdjustEye;2;1;E3631_97_Powersupply,FLEX86100_98_Scope;False;
已经删除资料:ID=420;86;CalTempDminoProcessingCoef;3;19;E3631_97_Powersupply,TPO4300_100_Thermocontroller;False;
已经删除资料:ID=421;86;CalVccDminoProcessingCoef;4;20;E3631_97_Powersupply;False;
已经删除资料:ID=422;87;AdjustTxPowerDmi;1;2;E3631_97_Powersupply,FLEX86100_98_Scope;False;
已经删除资料:ID=423;87;AdjustEye;2;1;E3631_97_Powersupply,FLEX86100_98_Scope;False;
已经删除资料:ID=424;87;CalTempDminoProcessingCoef;3;19;E3631_97_Powersupply,TPO4300_100_Thermocontroller;False;
已经删除资料:ID=425;87;CalVccDminoProcessingCoef;4;20;E3631_97_Powersupply;False;
**表[TopoTestParameter]修改如下**
已经删除资料:ID=2375;385;AP(DBM);double;output;-32768;0;3;1;0;0;1;
已经删除资料:ID=2376;385;JITTERPP(PS);double;output;-32768;-32768;32767;1;0;0;1;
已经删除资料:ID=2377;385;TXOMA(UW);double;output;-32768;-32768;32767;1;0;0;1;
已经删除资料:ID=2378;385;FALLTIME(PS);double;output;-32768;-32768;65;1;0;0;1;
已经删除资料:ID=2379;385;RISETIME(PS);double;output;-32768;-32768;100;1;0;0;1;
已经删除资料:ID=2380;385;JITTERRMS(PS);double;output;-32768;-32768;30;1;0;0;1;
已经删除资料:ID=2381;385;MASKMARGIN(%);double;output;-32768;10;90;1;0;0;1;
已经删除资料:ID=2382;385;ER(DB);double;output;-32768;3.5;6;1;0;0;1;
已经删除资料:ID=2383;385;ISOPTICALEYEORELECEYE;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2384;385;CROSSING(%);double;output;-32768;40;60;1;0;0;1;
已经删除资料:ID=2385;386;DMITEMPERR(C);double;output;-32768;-3;3;1;0;0;1;
已经删除资料:ID=2386;386;DMITEMP(C);double;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2387;387;DMIVCCERR(V);double;output;-32768;-0.165;0.165;1;0;0;1;
已经删除资料:ID=2388;387;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2389;388;ICC(MA);double;output;-32768;500;1050;1;0;0;1;
已经删除资料:ID=2390;389;DMITXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2391;389;CURRENTTXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2392;389;DMITXPOWERERR(DB);double;output;-32768;-2;2;1;0;0;1;
已经删除资料:ID=2393;390;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
已经删除资料:ID=2394;391;ARRAYLISTRXINPUTPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
已经删除资料:ID=2395;391;DMIRXPWRMAXERRPOINT(DBM);double;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2396;391;DMIRXPWRMAXERR;double;output;-32768;-3;3;1;0;0;1;
已经删除资料:ID=2397;392;CSENTARGETBER;double;input;1.0E-12;-32768;32767;0;0;0;0;
已经删除资料:ID=2398;392;COEFCSENSUBSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
已经删除资料:ID=2399;392;CSEN(DBM);double;output;-32768;-20;-11;1;0;0;1;
已经删除资料:ID=2400;392;SEARCHTARGETBERSUBSTEP;double;input;0.5;-32768;32767;0;0;0;0;
已经删除资料:ID=2401;392;SEARCHTARGETBERADDSTEP;double;input;0.5;-32768;32767;0;0;0;0;
已经删除资料:ID=2402;392;SEARCHTARGETBERLL;double;input;1E-8;-32768;32767;0;0;0;0;
已经删除资料:ID=2403;392;SEARCHTARGETBERUL;double;input;1.00E-7;-32768;32767;0;0;0;0;
已经删除资料:ID=2404;392;CSENSTARTINGRXPWR(DBM);double;input;-12;-32768;32767;0;0;0;0;
已经删除资料:ID=2405;392;CSENALIGNRXPWR(DBM);double;input;-7;-32768;32767;0;0;0;0;
已经删除资料:ID=2406;392;COEFCSENADDSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
已经删除资料:ID=2407;392;IsBerQuickTest;bool;input;false;-32768;32767;0;0;0;0;
已经删除资料:ID=2408;393;LOSA;double;output;-32768;-29;-16;1;0;0;1;
已经删除资料:ID=2409;393;LOSD;double;output;-32768;-29;-13;1;0;0;1;
已经删除资料:ID=2410;393;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
已经删除资料:ID=2411;393;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
已经删除资料:ID=2412;393;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
已经删除资料:ID=2413;393;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
已经删除资料:ID=2414;393;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
已经删除资料:ID=2415;393;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2416;394;AP(DBM);double;output;-32768;0;3;1;0;0;1;
已经删除资料:ID=2417;394;JITTERPP(PS);double;output;-32768;-32768;32767;1;0;0;1;
已经删除资料:ID=2418;394;TXOMA(UW);double;output;-32768;-32768;32767;1;0;0;1;
已经删除资料:ID=2419;394;FALLTIME(PS);double;output;-32768;-32768;65;1;0;0;1;
已经删除资料:ID=2420;394;RISETIME(PS);double;output;-32768;-32768;100;1;0;0;1;
已经删除资料:ID=2421;394;JITTERRMS(PS);double;output;-32768;-32768;30;1;0;0;1;
已经删除资料:ID=2422;394;MASKMARGIN(%);double;output;-32768;10;90;1;0;0;1;
已经删除资料:ID=2423;394;ER(DB);double;output;-32768;3.5;6;1;0;0;1;
已经删除资料:ID=2424;394;ISOPTICALEYEORELECEYE;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2425;394;CROSSING(%);double;output;-32768;40;60;1;0;0;1;
已经删除资料:ID=2426;395;DMITXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2427;395;CURRENTTXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2428;395;DMITXPOWERERR(DB);double;output;-32768;-2;2;1;0;0;1;
已经删除资料:ID=2429;396;DMITEMPERR(C);double;output;-32768;-3;3;1;0;0;1;
已经删除资料:ID=2430;396;DMITEMP(C);double;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2431;397;ICC(MA);double;output;-32768;500;1050;1;0;0;1;
已经删除资料:ID=2432;398;DMIVCCERR(V);double;output;-32768;-0.165;0.165;1;0;0;1;
已经删除资料:ID=2433;398;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2434;399;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
已经删除资料:ID=2435;400;ARRAYLISTRXINPUTPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
已经删除资料:ID=2436;400;DMIRXPWRMAXERRPOINT(DBM);double;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2437;400;DMIRXPWRMAXERR;double;output;-32768;-3;3;1;0;0;1;
已经删除资料:ID=2438;401;CSENTARGETBER;double;input;1.0E-12;-32768;32767;0;0;0;0;
已经删除资料:ID=2439;401;COEFCSENSUBSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
已经删除资料:ID=2440;401;CSEN(DBM);double;output;-32768;-20;-11;1;0;0;1;
已经删除资料:ID=2441;401;SEARCHTARGETBERSUBSTEP;double;input;0.5;-32768;32767;0;0;0;0;
已经删除资料:ID=2442;401;SEARCHTARGETBERADDSTEP;double;input;0.5;-32768;32767;0;0;0;0;
已经删除资料:ID=2443;401;SEARCHTARGETBERLL;double;input;1E-8;-32768;32767;0;0;0;0;
已经删除资料:ID=2444;401;SEARCHTARGETBERUL;double;input;1.00E-7;-32768;32767;0;0;0;0;
已经删除资料:ID=2445;401;CSENSTARTINGRXPWR(DBM);double;input;-12;-32768;32767;0;0;0;0;
已经删除资料:ID=2446;401;CSENALIGNRXPWR(DBM);double;input;-7;-32768;32767;0;0;0;0;
已经删除资料:ID=2447;401;COEFCSENADDSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
已经删除资料:ID=2448;401;IsBerQuickTest;bool;input;false;-32768;32767;0;0;0;0;
已经删除资料:ID=2449;402;LOSA;double;output;-32768;-29;-16;1;0;0;1;
已经删除资料:ID=2450;402;LOSD;double;output;-32768;-29;-13;1;0;0;1;
已经删除资料:ID=2451;402;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
已经删除资料:ID=2452;402;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
已经删除资料:ID=2453;402;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
已经删除资料:ID=2454;402;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
已经删除资料:ID=2455;402;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
已经删除资料:ID=2456;402;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2457;403;AP(DBM);double;output;-32768;0;3;1;0;0;1;
已经删除资料:ID=2458;403;JITTERPP(PS);double;output;-32768;-32768;32767;1;0;0;1;
已经删除资料:ID=2459;403;TXOMA(UW);double;output;-32768;-32768;32767;1;0;0;1;
已经删除资料:ID=2460;403;FALLTIME(PS);double;output;-32768;-32768;65;1;0;0;1;
已经删除资料:ID=2461;403;RISETIME(PS);double;output;-32768;-32768;100;1;0;0;1;
已经删除资料:ID=2462;403;JITTERRMS(PS);double;output;-32768;-32768;30;1;0;0;1;
已经删除资料:ID=2463;403;MASKMARGIN(%);double;output;-32768;10;90;1;0;0;1;
已经删除资料:ID=2464;403;ER(DB);double;output;-32768;3.5;6;1;0;0;1;
已经删除资料:ID=2465;403;ISOPTICALEYEORELECEYE;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2466;403;CROSSING(%);double;output;-32768;40;60;1;0;0;1;
已经删除资料:ID=2467;404;DMITXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2468;404;CURRENTTXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2469;404;DMITXPOWERERR(DB);double;output;-32768;-2;2;1;0;0;1;
已经删除资料:ID=2470;405;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
已经删除资料:ID=2471;406;DMITEMPERR(C);double;output;-32768;-3;3;1;0;0;1;
已经删除资料:ID=2472;406;DMITEMP(C);double;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2473;407;DMIVCCERR(V);double;output;-32768;-0.165;0.165;1;0;0;1;
已经删除资料:ID=2474;407;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2475;408;ICC(MA);double;output;-32768;100;2000;1;0;0;1;
已经删除资料:ID=2476;409;ARRAYLISTRXINPUTPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
已经删除资料:ID=2477;409;DMIRXPWRMAXERRPOINT(DBM);double;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2478;409;DMIRXPWRMAXERR;double;output;-32768;-3;3;1;0;0;1;
已经删除资料:ID=2479;410;CSENTARGETBER;double;input;1.0E-12;-32768;32767;0;0;0;0;
已经删除资料:ID=2480;410;COEFCSENSUBSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
已经删除资料:ID=2481;410;CSEN(DBM);double;output;-32768;-20;-11;1;0;0;1;
已经删除资料:ID=2482;410;SEARCHTARGETBERSUBSTEP;double;input;0.5;-32768;32767;0;0;0;0;
已经删除资料:ID=2483;410;SEARCHTARGETBERADDSTEP;double;input;0.5;-32768;32767;0;0;0;0;
已经删除资料:ID=2484;410;SEARCHTARGETBERLL;double;input;1E-8;-32768;32767;0;0;0;0;
已经删除资料:ID=2485;410;SEARCHTARGETBERUL;double;input;1.00E-7;-32768;32767;0;0;0;0;
已经删除资料:ID=2486;410;CSENSTARTINGRXPWR(DBM);double;input;-12;-32768;32767;0;0;0;0;
已经删除资料:ID=2487;410;CSENALIGNRXPWR(DBM);double;input;-7;-32768;32767;0;0;0;0;
已经删除资料:ID=2488;410;COEFCSENADDSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
已经删除资料:ID=2489;410;IsBerQuickTest;bool;input;false;-32768;32767;0;0;0;0;
已经删除资料:ID=2490;411;LOSA;double;output;-32768;-29;-16;1;0;0;1;
已经删除资料:ID=2491;411;LOSD;double;output;-32768;-29;-13;1;0;0;1;
已经删除资料:ID=2492;411;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
已经删除资料:ID=2493;411;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
已经删除资料:ID=2494;411;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
已经删除资料:ID=2495;411;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
已经删除资料:ID=2496;411;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
已经删除资料:ID=2497;411;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2498;412;FIXEDMODDAC(MA);UInt16;input;300;-32768;32767;0;0;0;0;
已经删除资料:ID=2499;412;ARRAYLISTXDMICOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2500;412;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
已经删除资料:ID=2501;412;IBIASADCORTXPOWERADC;byte;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2502;412;ARRAYIBIAS(MA);ArrayList;input;300,400,500;-32768;32767;0;0;0;0;
已经删除资料:ID=2503;412;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2504;412;ISTEMPRELATIVE;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2505;412;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2506;412;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
已经删除资料:ID=2507;412;HighestCalTemp;double;input;60;-32768;32767;0;0;0;0;
已经删除资料:ID=2508;412;LowestCalTemp;double;input;20;-32768;32767;0;0;0;0;
已经删除资料:ID=2509;412;ISNEWALGORITHM;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2510;413;IMODMIN(MA);UInt16;input;100;-32768;32767;0;0;0;0;
已经删除资料:ID=2511;413;ARRAYLISTTXMODCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2512;413;1STOR2STORPIDTXLOP;byte;input;0;-32768;32767;0;0;0;0;
已经删除资料:ID=2513;413;1STOR2STORPIDER;byte;input;2;-32768;32767;0;0;0;0;
已经删除资料:ID=2514;413;ISOPENLOOPORCLOSELOOPORBOTH;byte;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2515;413;IMODSTART(MA);UInt16;input;250;-32768;32767;0;0;0;0;
已经删除资料:ID=2516;413;IMODSTEP;byte;input;64;-32768;32767;0;0;0;0;
已经删除资料:ID=2517;413;IMODMETHOD;byte;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2518;413;TXERTOLERANCE(DB);double;input;0.2;-32768;32767;0;0;0;0;
已经删除资料:ID=2519;413;TXERTARGET(DB);double;input;4;3.5;6;1;0;1;0;
已经删除资料:ID=2520;413;ARRAYLISTTXPOWERCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2521;413;IBIASMETHOD;byte;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2522;413;IBIASSTEP(MA);byte;input;64;-32768;32767;0;0;0;0;
已经删除资料:ID=2523;413;IBIASSTART(MA);UInt16;input;600;-32768;32767;0;0;0;0;
已经删除资料:ID=2524;413;IBIASMIN(MA);UInt16;input;400;-32768;32767;0;0;0;0;
已经删除资料:ID=2525;413;IBIASMAX(MA);UInt16;input;2500;-32768;32767;0;0;0;0;
已经删除资料:ID=2526;413;FIXEDMOD(MA);UInt16;input;500;-32768;32767;0;0;0;0;
已经删除资料:ID=2527;413;TXLOPTOLERANCE(UW);double;input;100;-32768;32767;0;0;0;0;
已经删除资料:ID=2528;413;TXLOPTARGET(UW);double;input;900;1000;2000;1;0;1;0;
已经删除资料:ID=2529;413;IMODMAX(MA);UInt16;input;700;-32768;32767;0;0;0;0;
已经删除资料:ID=2530;413;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2531;413;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2532;413;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
已经删除资料:ID=2533;413;PIDCOEFARRAY;ArrayList;input;0.01,0.005,0;-32768;32767;0;0;0;0;
已经删除资料:ID=2534;413;FIXEDIBIAS(MA);UInt32;input;280;-32768;32767;0;0;0;0;
已经删除资料:ID=2535;413;FixedIBiasArray;ArrayList;input;280,280,280,280;-32768;32767;0;0;0;0;
已经删除资料:ID=2536;413;FixedModArray;ArrayList;input;500,500,500,500;-32768;32767;0;0;0;0;
已经删除资料:ID=2537;414;1STOR2STORPID;byte;input;1;0;0;0;0;0;0;
已经删除资料:ID=2538;414;ARRAYLISTDMITEMPCOEF;ArrayList;output;-32768;0;0;0;1;0;0;
已经删除资料:ID=2539;415;GENERALVCC(V);double;input;3.3;-32768;32767;0;0;0;0;
已经删除资料:ID=2540;415;ARRAYLISTVCC(V);ArrayList;input;3.1,3.3,3.5;-32768;32767;0;0;0;0;
已经删除资料:ID=2541;415;ARRAYLISTDMIVCCCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2542;415;1STOR2STORPID;byte;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2543;416;ARRAYLISTDMIRXCOEF;ArrayList;output;;-32768;32767;0;1;0;0;
已经删除资料:ID=2544;416;ARRAYLISTRXPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
已经删除资料:ID=2545;416;HasOffset;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2546;416;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
已经删除资料:ID=2547;417;LOSDVOLTAGETUNESTEP(V);byte;input;2;-32768;32767;0;0;0;0;
已经删除资料:ID=2548;417;LOSAVOLTAGESTARTVALUE(V);UInt16;input;14;-32768;32767;0;0;0;0;
已经删除资料:ID=2549;417;IsAdjustLos;bool;input;True;-32768;32767;0;0;0;0;
已经删除资料:ID=2550;417;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2551;417;LOSDVOLTAGEUPERLIMIT(V);UInt16;input;30;-32768;32767;0;0;0;0;
已经删除资料:ID=2552;417;LOSDVOLTAGESTARTVALUE(V);UInt16;input;14;-32768;32767;0;0;0;0;
已经删除资料:ID=2553;417;LOSDINPUTPOWER;double;input;-21;-32768;32767;0;0;0;0;
已经删除资料:ID=2554;417;LOSAVOLTAGETUNESTEP(V);byte;input;2;-32768;32767;0;0;0;0;
已经删除资料:ID=2555;417;LOSAVOLTAGEUPERLIMIT(V);UInt16;input;30;-32768;32767;0;0;0;0;
已经删除资料:ID=2556;417;LosValue(V);UINT32;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2557;417;LOSDVOLTAGELOWLIMIT(V);UInt16;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2558;417;LOSAINPUTPOWER;double;input;-21;-32768;32767;0;0;0;0;
已经删除资料:ID=2559;417;LOSAVOLTAGELOWLIMIT(V);UInt16;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2560;418;FIXEDMODDAC(MA);UInt16;input;300;-32768;32767;0;0;0;0;
已经删除资料:ID=2561;418;ARRAYLISTXDMICOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2562;418;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
已经删除资料:ID=2563;418;IBIASADCORTXPOWERADC;byte;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2564;418;ARRAYIBIAS(MA);ArrayList;input;300,400,500;-32768;32767;0;0;0;0;
已经删除资料:ID=2565;418;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2566;418;ISTEMPRELATIVE;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2567;418;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2568;418;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
已经删除资料:ID=2569;418;HighestCalTemp;double;input;60;-32768;32767;0;0;0;0;
已经删除资料:ID=2570;418;LowestCalTemp;double;input;20;-32768;32767;0;0;0;0;
已经删除资料:ID=2571;418;ISNEWALGORITHM;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2572;419;IMODMIN(MA);UInt16;input;100;-32768;32767;0;0;0;0;
已经删除资料:ID=2573;419;ARRAYLISTTXMODCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2574;419;1STOR2STORPIDTXLOP;byte;input;0;-32768;32767;0;0;0;0;
已经删除资料:ID=2575;419;1STOR2STORPIDER;byte;input;2;-32768;32767;0;0;0;0;
已经删除资料:ID=2576;419;ISOPENLOOPORCLOSELOOPORBOTH;byte;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2577;419;IMODSTART(MA);UInt16;input;200;-32768;32767;0;0;0;0;
已经删除资料:ID=2578;419;IMODSTEP;byte;input;64;-32768;32767;0;0;0;0;
已经删除资料:ID=2579;419;IMODMETHOD;byte;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2580;419;TXERTOLERANCE(DB);double;input;0.2;-32768;32767;0;0;0;0;
已经删除资料:ID=2581;419;TXERTARGET(DB);double;input;4;3.5;6;1;0;1;0;
已经删除资料:ID=2582;419;ARRAYLISTTXPOWERCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2583;419;IBIASMETHOD;byte;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2584;419;IBIASSTEP(MA);byte;input;64;-32768;32767;0;0;0;0;
已经删除资料:ID=2585;419;IBIASSTART(MA);UInt16;input;600;-32768;32767;0;0;0;0;
已经删除资料:ID=2586;419;IBIASMIN(MA);UInt16;input;400;-32768;32767;0;0;0;0;
已经删除资料:ID=2587;419;IBIASMAX(MA);UInt16;input;2500;-32768;32767;0;0;0;0;
已经删除资料:ID=2588;419;FIXEDMOD(MA);UInt16;input;500;-32768;32767;0;0;0;0;
已经删除资料:ID=2589;419;TXLOPTOLERANCE(UW);double;input;100;-32768;32767;0;0;0;0;
已经删除资料:ID=2590;419;TXLOPTARGET(UW);double;input;900;1000;2000;1;0;1;0;
已经删除资料:ID=2591;419;IMODMAX(MA);UInt16;input;1000;-32768;32767;0;0;0;0;
已经删除资料:ID=2592;419;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2593;419;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2594;419;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
已经删除资料:ID=2595;419;PIDCOEFARRAY;ArrayList;input;0.01,0.005,0;-32768;32767;0;0;0;0;
已经删除资料:ID=2596;419;FIXEDIBIAS(MA);UInt32;input;280;-32768;32767;0;0;0;0;
已经删除资料:ID=2597;419;FixedIBiasArray;ArrayList;input;280,280,280,280;-32768;32767;0;0;0;0;
已经删除资料:ID=2598;419;FixedModArray;ArrayList;input;500,500,500,500;-32768;32767;0;0;0;0;
已经删除资料:ID=2599;420;1STOR2STORPID;byte;input;1;0;0;0;0;0;0;
已经删除资料:ID=2600;420;ARRAYLISTDMITEMPCOEF;ArrayList;output;-32768;0;0;0;1;0;0;
已经删除资料:ID=2601;421;GENERALVCC(V);double;input;3.3;-32768;32767;0;0;0;0;
已经删除资料:ID=2602;421;ARRAYLISTVCC(V);ArrayList;input;3.1,3.3,3.5;-32768;32767;0;0;0;0;
已经删除资料:ID=2603;421;ARRAYLISTDMIVCCCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2604;421;1STOR2STORPID;byte;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2605;422;FIXEDMODDAC(MA);UInt16;input;300;-32768;32767;0;0;0;0;
已经删除资料:ID=2606;422;ARRAYLISTXDMICOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2607;422;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
已经删除资料:ID=2608;422;IBIASADCORTXPOWERADC;byte;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2609;422;ARRAYIBIAS(MA);ArrayList;input;300,400,500;-32768;32767;0;0;0;0;
已经删除资料:ID=2610;422;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2611;422;ISTEMPRELATIVE;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2612;422;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2613;422;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
已经删除资料:ID=2614;422;HighestCalTemp;double;input;60;-32768;32767;0;0;0;0;
已经删除资料:ID=2615;422;LowestCalTemp;double;input;20;-32768;32767;0;0;0;0;
已经删除资料:ID=2616;422;ISNEWALGORITHM;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2617;423;IMODMIN(MA);UInt16;input;100;-32768;32767;0;0;0;0;
已经删除资料:ID=2618;423;ARRAYLISTTXMODCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2619;423;1STOR2STORPIDTXLOP;byte;input;0;-32768;32767;0;0;0;0;
已经删除资料:ID=2620;423;1STOR2STORPIDER;byte;input;2;-32768;32767;0;0;0;0;
已经删除资料:ID=2621;423;ISOPENLOOPORCLOSELOOPORBOTH;byte;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2622;423;IMODSTART(MA);UInt16;input;200;-32768;32767;0;0;0;0;
已经删除资料:ID=2623;423;IMODSTEP;byte;input;64;-32768;32767;0;0;0;0;
已经删除资料:ID=2624;423;IMODMETHOD;byte;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2625;423;TXERTOLERANCE(DB);double;input;0.2;-32768;32767;0;0;0;0;
已经删除资料:ID=2626;423;TXERTARGET(DB);double;input;4;3.5;6;1;0;1;0;
已经删除资料:ID=2627;423;ARRAYLISTTXPOWERCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2628;423;IBIASMETHOD;byte;input;1;-32768;32767;0;0;0;0;
已经删除资料:ID=2629;423;IBIASSTEP(MA);byte;input;64;-32768;32767;0;0;0;0;
已经删除资料:ID=2630;423;IBIASSTART(MA);UInt16;input;600;-32768;32767;0;0;0;0;
已经删除资料:ID=2631;423;IBIASMIN(MA);UInt16;input;400;-32768;32767;0;0;0;0;
已经删除资料:ID=2632;423;IBIASMAX(MA);UInt16;input;2500;-32768;32767;0;0;0;0;
已经删除资料:ID=2633;423;FIXEDMOD(MA);UInt16;input;500;-32768;32767;0;0;0;0;
已经删除资料:ID=2634;423;TXLOPTOLERANCE(UW);double;input;100;-32768;32767;0;0;0;0;
已经删除资料:ID=2635;423;TXLOPTARGET(UW);double;input;900;1000;2000;1;0;1;0;
已经删除资料:ID=2636;423;IMODMAX(MA);UInt16;input;1000;-32768;32767;0;0;0;0;
已经删除资料:ID=2637;423;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2638;423;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
已经删除资料:ID=2639;423;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
已经删除资料:ID=2640;423;PIDCOEFARRAY;ArrayList;input;0.01,0.005,0;-32768;32767;0;0;0;0;
已经删除资料:ID=2641;423;FIXEDIBIAS(MA);UInt32;input;280;-32768;32767;0;0;0;0;
已经删除资料:ID=2642;423;FixedIBiasArray;ArrayList;input;280,280,280,280;-32768;32767;0;0;0;0;
已经删除资料:ID=2643;423;FixedModArray;ArrayList;input;500,500,500,500;-32768;32767;0;0;0;0;
已经删除资料:ID=2644;424;1STOR2STORPID;byte;input;1;0;0;0;0;0;0;
已经删除资料:ID=2645;424;ARRAYLISTDMITEMPCOEF;ArrayList;output;-32768;0;0;0;1;0;0;
已经删除资料:ID=2646;425;GENERALVCC(V);double;input;3.3;-32768;32767;0;0;0;0;
已经删除资料:ID=2647;425;ARRAYLISTVCC(V);ArrayList;input;3.1,3.3,3.5;-32768;32767;0;0;0;0;
已经删除资料:ID=2648;425;ARRAYLISTDMIVCCCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
已经删除资料:ID=2649;425;1STOR2STORPID;byte;input;1;-32768;32767;0;0;0;0;
**表[TopoEquipment]修改如下**
已经删除资料:ID=155;23;1;Powersupply;E3631;0;
已经删除资料:ID=156;23;2;Scope;FLEX86100;0;
已经删除资料:ID=157;23;3;PPG;MP1800PPG;0;
已经删除资料:ID=158;23;4;Thermocontroller;TPO4300;0;
已经删除资料:ID=159;23;5;OpticalSwitch;AQ2211OpticalSwitch;1;
已经删除资料:ID=160;23;6;Attennuator;AQ2211Atten;0;
已经删除资料:ID=161;23;7;ErrorDetector;MP1800ED;0;
已经删除资料:ID=162;23;8;OpticalSwitch;AQ2211OpticalSwitch;2;
**表[TopoEquipmentParameter]修改如下**
已经删除资料:ID=2195;155;Addr;5;
已经删除资料:ID=2196;155;IOType;GPIB;
已经删除资料:ID=2197;155;Reset;false;
已经删除资料:ID=2198;155;Name;E3631;
已经删除资料:ID=2199;155;DutChannel;1;
已经删除资料:ID=2200;155;OptSourceChannel;2;
已经删除资料:ID=2201;155;DutVoltage;3.5;
已经删除资料:ID=2202;155;DutCurrent;2.5;
已经删除资料:ID=2203;155;OptVoltage;3.5;
已经删除资料:ID=2204;155;OptCurrent;1.5;
已经删除资料:ID=2205;155;voltageoffset;0.2;
已经删除资料:ID=2206;155;currentoffset;0;
已经删除资料:ID=2207;156;Addr;7;
已经删除资料:ID=2208;156;IOType;GPIB;
已经删除资料:ID=2209;156;Reset;false;
已经删除资料:ID=2210;156;Name;FLEX86100;
已经删除资料:ID=2211;156;configFilePath;1;
已经删除资料:ID=2212;156;FlexDcaDataRate;25.78125E+9;
已经删除资料:ID=2213;156;FilterSwitch;1;
已经删除资料:ID=2214;156;FlexFilterFreq;25.78125;
已经删除资料:ID=2215;156;triggerSource;0;
已经删除资料:ID=2216;156;FlexTriggerBwlimit;2;
已经删除资料:ID=2217;156;opticalSlot;3;
已经删除资料:ID=2218;156;elecSlot;4;
已经删除资料:ID=2219;156;FlexOptChannel;1;
已经删除资料:ID=2220;156;FlexElecChannel;1;
已经删除资料:ID=2221;156;FlexDcaWavelength;1;
已经删除资料:ID=2222;156;opticalAttSwitch;1;
已经删除资料:ID=2223;156;FlexDcaAtt;0;
已经删除资料:ID=2224;156;erFactor;0;
已经删除资料:ID=2225;156;FlexScale;300;
已经删除资料:ID=2226;156;FlexOffset;300;
已经删除资料:ID=2227;156;Threshold;0;
已经删除资料:ID=2228;156;reference;0;
已经删除资料:ID=2229;156;precisionTimebaseModuleSlot;1;
已经删除资料:ID=2230;156;precisionTimebaseSynchMethod;1;
已经删除资料:ID=2231;156;precisionTimebaseRefClk;6.445e+9;
已经删除资料:ID=2232;156;rapidEyeSwitch;1;
已经删除资料:ID=2233;156;marginType;1;
已经删除资料:ID=2234;156;marginHitType;0;
已经删除资料:ID=2235;156;marginHitRatio;5e-006;
已经删除资料:ID=2236;156;marginHitCount;0;
已经删除资料:ID=2237;156;acqLimitType;0;
已经删除资料:ID=2238;156;acqLimitNumber;1000;
已经删除资料:ID=2239;156;opticalMaskName;c:\scope\masks\25.78125_100GBASE-LR4_Tx_Optical_D31.MSK;
已经删除资料:ID=2240;156;elecMaskName;c:\Eye;
已经删除资料:ID=2241;156;opticalEyeSavePath;D:\Eye\;
已经删除资料:ID=2242;156;elecEyeSavePath;D:\Eye\;
已经删除资料:ID=2243;156;ERFACTORSWITCH;1;
已经删除资料:ID=2244;157;Addr;1;
已经删除资料:ID=2245;157;IOType;GPIB;
已经删除资料:ID=2246;157;Reset;false;
已经删除资料:ID=2247;157;Name;MP1800PPG;
已经删除资料:ID=2248;157;dataRate;25.78128;
已经删除资料:ID=2249;157;dataLevelGuardAmpMax;1;
已经删除资料:ID=2250;157;dataLevelGuardOffsetMax;0;
已经删除资料:ID=2251;157;dataLevelGuardOffsetMin;0;
已经删除资料:ID=2252;157;dataLevelGuardSwitch;0;
已经删除资料:ID=2253;157;dataAmplitude;0.5;
已经删除资料:ID=2254;157;dataCrossPoint;50;
已经删除资料:ID=2255;157;configFilePath;0;
已经删除资料:ID=2256;157;slot;1;
已经删除资料:ID=2257;157;clockSource;0;
已经删除资料:ID=2258;157;auxOutputClkDiv;0;
已经删除资料:ID=2259;157;prbsLength;31;
已经删除资料:ID=2260;157;patternType;0;
已经删除资料:ID=2261;157;dataSwitch;1;
已经删除资料:ID=2262;157;dataTrackingSwitch;1;
已经删除资料:ID=2263;157;dataAcModeSwitch;0;
已经删除资料:ID=2264;157;dataLevelMode;0;
已经删除资料:ID=2265;157;clockSwitch;1;
已经删除资料:ID=2266;157;outputSwitch;1;
已经删除资料:ID=2267;157;TotalChannel;4;
已经删除资料:ID=2268;158;Addr;23;
已经删除资料:ID=2269;158;IOType;GPIB;
已经删除资料:ID=2270;158;Reset;False;
已经删除资料:ID=2271;158;Name;TPO4300;
已经删除资料:ID=2272;158;FLSE;14;
已经删除资料:ID=2273;158;ULIM;90;
已经删除资料:ID=2274;158;LLIM;-20;
已经删除资料:ID=2275;158;Sensor;1;
已经删除资料:ID=2276;159;Addr;20;
已经删除资料:ID=2277;159;IOType;GPIB;
已经删除资料:ID=2278;159;Reset;false;
已经删除资料:ID=2279;159;Name;AQ2011OpticalSwitch;
已经删除资料:ID=2280;159;OpticalSwitchSlot;1;
已经删除资料:ID=2281;159;SwitchChannel;1;
已经删除资料:ID=2282;159;ToChannel;1;
已经删除资料:ID=2283;160;Addr;20;
已经删除资料:ID=2284;160;IOType;GPIB;
已经删除资料:ID=2285;160;Reset;false;
已经删除资料:ID=2286;160;Name;AQ2211Atten;
已经删除资料:ID=2287;160;TOTALCHANNEL;4;
已经删除资料:ID=2288;160;AttValue;20;
已经删除资料:ID=2289;160;AttSlot;2;
已经删除资料:ID=2290;160;WAVELENGTH;1310,1310,1310,1310;
已经删除资料:ID=2291;160;AttChannel;1;
已经删除资料:ID=2292;161;Addr;1;
已经删除资料:ID=2293;161;IOType;GPIB;
已经删除资料:ID=2294;161;Reset;false;
已经删除资料:ID=2295;161;Name;MP1800ED;
已经删除资料:ID=2296;161;slot;3;
已经删除资料:ID=2297;161;TotalChannel;4;
已经删除资料:ID=2298;161;currentChannel;1;
已经删除资料:ID=2299;161;dataInputInterface;2;
已经删除资料:ID=2300;161;prbsLength;31;
已经删除资料:ID=2301;161;errorResultZoom;1;
已经删除资料:ID=2302;161;edGatingMode;1;
已经删除资料:ID=2303;161;edGatingUnit;0;
已经删除资料:ID=2304;161;edGatingTime;5;
已经删除资料:ID=2305;162;Addr;20;
已经删除资料:ID=2306;162;IOType;GPIB;
已经删除资料:ID=2307;162;Reset;false;
已经删除资料:ID=2308;162;Name;AQ2011OpticalSwitch;
已经删除资料:ID=2309;162;OpticalSwitchSlot;3;
已经删除资料:ID=2310;162;SwitchChannel;1;
已经删除资料:ID=2311;162;ToChannel;1;
已经删除资料:ID=2312;155;opendelay;2000;
已经删除资料:ID=2313;155;closedelay;500;
==User:terry.yin于2014/10/31 16:39:31修改==
**表[TopoTestPlan]修改如下**
新增一笔资料为:ID=24;3;CGR4-v5;1;1;0;SN_CHECK = 0;ApcStyle=1;
**表[TopoTestControl]修改如下**
新增一笔资料为:ID=88;24;FMT-3.3-20-1;6;0;20;3.3;31;25.78125e+9;TempSleep=60;False;
新增一笔资料为:ID=89;24;FMT-3.3-40-1_4;5;0;40;3.3;31;25.78125E+9;TempSleep=60;False;
新增一笔资料为:ID=90;24;FMT-60-3.3-1_4;3;0;60;3.3;31;25.78125e+9;TempSleep=5;False;
新增一笔资料为:ID=91;24;LP-3.3-20-1-4;1;0;20;3.3;31;25.78125e+9;TempSleep=60;False;
新增一笔资料为:ID=92;24;LP-3.3-60-1-4;2;0;60;3.3;31;25.78125e+9;TempSleep=60;False;
**表[TopoTestModel]修改如下**
新增一笔资料为:ID=426;88;TestEye;7;3;E3631_144_Powersupply,FLEX86100_145_Scope;False;
新增一笔资料为:ID=427;88;TestTempDmi;10;13;E3631_144_Powersupply,TPO4300_147_Thermocontroller;False;
新增一笔资料为:ID=428;88;TestVccDmi;13;14;E3631_144_Powersupply;False;
新增一笔资料为:ID=429;88;TestIcc;14;15;E3631_144_Powersupply;False;
新增一笔资料为:ID=430;88;TestTxPowerDmi;9;4;E3631_144_Powersupply,FLEX86100_145_Scope;False;
新增一笔资料为:ID=431;88;TestIBiasDmi;15;5;E3631_144_Powersupply;False;
新增一笔资料为:ID=432;88;TestRxPowerDmi;16;9;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;False;
新增一笔资料为:ID=433;88;TestBer;17;10;E3631_144_Powersupply,AQ2211Atten_149_Attennuator,MP1800ED_150_ErrorDetector;False;
新增一笔资料为:ID=434;88;TestRXLosAD;18;8;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;False;
新增一笔资料为:ID=435;89;TestEye;1;3;E3631_144_Powersupply,FLEX86100_145_Scope;False;
新增一笔资料为:ID=436;89;TestTxPowerDmi;2;4;E3631_144_Powersupply,FLEX86100_145_Scope;False;
新增一笔资料为:ID=437;89;TestTempDmi;6;13;E3631_144_Powersupply,TPO4300_147_Thermocontroller;False;
新增一笔资料为:ID=438;89;TestIcc;7;15;E3631_144_Powersupply;False;
新增一笔资料为:ID=439;89;TestVccDmi;8;14;E3631_144_Powersupply;False;
新增一笔资料为:ID=440;89;TestIBiasDmi;9;5;E3631_144_Powersupply;False;
新增一笔资料为:ID=441;89;TestRxPowerDmi;10;9;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;False;
新增一笔资料为:ID=442;89;TestBer;11;10;E3631_144_Powersupply,AQ2211Atten_149_Attennuator,MP1800ED_150_ErrorDetector;False;
新增一笔资料为:ID=443;89;TestRXLosAD;12;8;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;False;
新增一笔资料为:ID=444;90;TestEye;1;3;E3631_144_Powersupply,FLEX86100_145_Scope;False;
新增一笔资料为:ID=445;90;TestTxPowerDmi;2;4;E3631_144_Powersupply,FLEX86100_145_Scope;False;
新增一笔资料为:ID=446;90;TestIBiasDmi;5;5;E3631_144_Powersupply;False;
新增一笔资料为:ID=447;90;TestTempDmi;7;13;E3631_144_Powersupply,TPO4300_147_Thermocontroller;False;
新增一笔资料为:ID=448;90;TestVccDmi;8;14;E3631_144_Powersupply;False;
新增一笔资料为:ID=449;90;TestIcc;9;15;E3631_144_Powersupply;False;
新增一笔资料为:ID=450;90;TestRxPowerDmi;11;9;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;False;
新增一笔资料为:ID=451;90;TestBer;12;10;E3631_144_Powersupply,AQ2211Atten_149_Attennuator,MP1800ED_150_ErrorDetector;False;
新增一笔资料为:ID=452;90;TestRXLosAD;13;8;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;False;
新增一笔资料为:ID=453;91;AdjustTxPowerDmi;1;2;E3631_144_Powersupply,FLEX86100_145_Scope;False;
新增一笔资料为:ID=454;91;AdjustEye;2;1;E3631_144_Powersupply,FLEX86100_145_Scope;False;
新增一笔资料为:ID=455;91;CalTempDminoProcessingCoef;3;19;E3631_144_Powersupply,TPO4300_147_Thermocontroller;False;
新增一笔资料为:ID=456;91;CalVccDminoProcessingCoef;4;20;E3631_144_Powersupply;False;
新增一笔资料为:ID=457;91;CalRxDminoProcessingCoef;5;18;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;False;
新增一笔资料为:ID=458;91;AdjustLos;6;6;E3631_144_Powersupply,AQ2211Atten_149_Attennuator;False;
新增一笔资料为:ID=459;92;AdjustTxPowerDmi;1;2;E3631_144_Powersupply,FLEX86100_145_Scope;False;
新增一笔资料为:ID=460;92;AdjustEye;2;1;E3631_144_Powersupply,FLEX86100_145_Scope;False;
新增一笔资料为:ID=461;92;CalTempDminoProcessingCoef;3;19;E3631_144_Powersupply,TPO4300_147_Thermocontroller;False;
新增一笔资料为:ID=462;92;CalVccDminoProcessingCoef;4;20;E3631_144_Powersupply;False;
**表[TopoTestParameter]修改如下**
新增一笔资料为:ID=2650;426;AP(DBM);double;output;-32768;0;3;1;0;0;1;
新增一笔资料为:ID=2651;426;JITTERPP(PS);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2652;426;TXOMA(UW);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2653;426;FALLTIME(PS);double;output;-32768;-32768;65;1;0;0;1;
新增一笔资料为:ID=2654;426;RISETIME(PS);double;output;-32768;-32768;100;1;0;0;1;
新增一笔资料为:ID=2655;426;JITTERRMS(PS);double;output;-32768;-32768;30;1;0;0;1;
新增一笔资料为:ID=2656;426;MASKMARGIN(%);double;output;-32768;10;90;1;0;0;1;
新增一笔资料为:ID=2657;426;ER(DB);double;output;-32768;3.5;6;1;0;0;1;
新增一笔资料为:ID=2658;426;ISOPTICALEYEORELECEYE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2659;426;CROSSING(%);double;output;-32768;40;60;1;0;0;1;
新增一笔资料为:ID=2660;427;DMITEMPERR(C);double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2661;427;DMITEMP(C);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2662;428;DMIVCCERR(V);double;output;-32768;-0.165;0.165;1;0;0;1;
新增一笔资料为:ID=2663;428;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2664;429;ICC(MA);double;output;-32768;500;1050;1;0;0;1;
新增一笔资料为:ID=2665;430;DMITXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2666;430;CURRENTTXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2667;430;DMITXPOWERERR(DB);double;output;-32768;-2;2;1;0;0;1;
新增一笔资料为:ID=2668;431;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
新增一笔资料为:ID=2669;432;ARRAYLISTRXINPUTPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2670;432;DMIRXPWRMAXERRPOINT(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2671;432;DMIRXPWRMAXERR;double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2672;433;CSENTARGETBER;double;input;1.0E-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2673;433;COEFCSENSUBSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2674;433;CSEN(DBM);double;output;-32768;-20;-11;1;0;0;1;
新增一笔资料为:ID=2675;433;SEARCHTARGETBERSUBSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2676;433;SEARCHTARGETBERADDSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2677;433;SEARCHTARGETBERLL;double;input;1E-8;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2678;433;SEARCHTARGETBERUL;double;input;1.00E-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2679;433;CSENSTARTINGRXPWR(DBM);double;input;-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2680;433;CSENALIGNRXPWR(DBM);double;input;-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2681;433;COEFCSENADDSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2682;433;IsBerQuickTest;bool;input;false;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2683;434;LOSA;double;output;-32768;-29;-16;1;0;0;1;
新增一笔资料为:ID=2684;434;LOSD;double;output;-32768;-29;-13;1;0;0;1;
新增一笔资料为:ID=2685;434;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2686;434;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2687;434;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2688;434;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2689;434;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
新增一笔资料为:ID=2690;434;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2691;435;AP(DBM);double;output;-32768;0;3;1;0;0;1;
新增一笔资料为:ID=2692;435;JITTERPP(PS);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2693;435;TXOMA(UW);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2694;435;FALLTIME(PS);double;output;-32768;-32768;65;1;0;0;1;
新增一笔资料为:ID=2695;435;RISETIME(PS);double;output;-32768;-32768;100;1;0;0;1;
新增一笔资料为:ID=2696;435;JITTERRMS(PS);double;output;-32768;-32768;30;1;0;0;1;
新增一笔资料为:ID=2697;435;MASKMARGIN(%);double;output;-32768;10;90;1;0;0;1;
新增一笔资料为:ID=2698;435;ER(DB);double;output;-32768;3.5;6;1;0;0;1;
新增一笔资料为:ID=2699;435;ISOPTICALEYEORELECEYE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2700;435;CROSSING(%);double;output;-32768;40;60;1;0;0;1;
新增一笔资料为:ID=2701;436;DMITXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2702;436;CURRENTTXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2703;436;DMITXPOWERERR(DB);double;output;-32768;-2;2;1;0;0;1;
新增一笔资料为:ID=2704;437;DMITEMPERR(C);double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2705;437;DMITEMP(C);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2706;438;ICC(MA);double;output;-32768;500;1050;1;0;0;1;
新增一笔资料为:ID=2707;439;DMIVCCERR(V);double;output;-32768;-0.165;0.165;1;0;0;1;
新增一笔资料为:ID=2708;439;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2709;440;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
新增一笔资料为:ID=2710;441;ARRAYLISTRXINPUTPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2711;441;DMIRXPWRMAXERRPOINT(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2712;441;DMIRXPWRMAXERR;double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2713;442;CSENTARGETBER;double;input;1.0E-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2714;442;COEFCSENSUBSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2715;442;CSEN(DBM);double;output;-32768;-20;-11;1;0;0;1;
新增一笔资料为:ID=2716;442;SEARCHTARGETBERSUBSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2717;442;SEARCHTARGETBERADDSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2718;442;SEARCHTARGETBERLL;double;input;1E-8;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2719;442;SEARCHTARGETBERUL;double;input;1.00E-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2720;442;CSENSTARTINGRXPWR(DBM);double;input;-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2721;442;CSENALIGNRXPWR(DBM);double;input;-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2722;442;COEFCSENADDSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2723;442;IsBerQuickTest;bool;input;false;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2724;443;LOSA;double;output;-32768;-29;-16;1;0;0;1;
新增一笔资料为:ID=2725;443;LOSD;double;output;-32768;-29;-13;1;0;0;1;
新增一笔资料为:ID=2726;443;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2727;443;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2728;443;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2729;443;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2730;443;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
新增一笔资料为:ID=2731;443;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2732;444;AP(DBM);double;output;-32768;0;3;1;0;0;1;
新增一笔资料为:ID=2733;444;JITTERPP(PS);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2734;444;TXOMA(UW);double;output;-32768;-32768;32767;1;0;0;1;
新增一笔资料为:ID=2735;444;FALLTIME(PS);double;output;-32768;-32768;65;1;0;0;1;
新增一笔资料为:ID=2736;444;RISETIME(PS);double;output;-32768;-32768;100;1;0;0;1;
新增一笔资料为:ID=2737;444;JITTERRMS(PS);double;output;-32768;-32768;30;1;0;0;1;
新增一笔资料为:ID=2738;444;MASKMARGIN(%);double;output;-32768;10;90;1;0;0;1;
新增一笔资料为:ID=2739;444;ER(DB);double;output;-32768;3.5;6;1;0;0;1;
新增一笔资料为:ID=2740;444;ISOPTICALEYEORELECEYE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2741;444;CROSSING(%);double;output;-32768;40;60;1;0;0;1;
新增一笔资料为:ID=2742;445;DMITXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2743;445;CURRENTTXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2744;445;DMITXPOWERERR(DB);double;output;-32768;-2;2;1;0;0;1;
新增一笔资料为:ID=2745;446;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
新增一笔资料为:ID=2746;447;DMITEMPERR(C);double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2747;447;DMITEMP(C);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2748;448;DMIVCCERR(V);double;output;-32768;-0.165;0.165;1;0;0;1;
新增一笔资料为:ID=2749;448;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2750;449;ICC(MA);double;output;-32768;100;2000;1;0;0;1;
新增一笔资料为:ID=2751;450;ARRAYLISTRXINPUTPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2752;450;DMIRXPWRMAXERRPOINT(DBM);double;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2753;450;DMIRXPWRMAXERR;double;output;-32768;-3;3;1;0;0;1;
新增一笔资料为:ID=2754;451;CSENTARGETBER;double;input;1.0E-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2755;451;COEFCSENSUBSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2756;451;CSEN(DBM);double;output;-32768;-20;-11;1;0;0;1;
新增一笔资料为:ID=2757;451;SEARCHTARGETBERSUBSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2758;451;SEARCHTARGETBERADDSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2759;451;SEARCHTARGETBERLL;double;input;1E-8;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2760;451;SEARCHTARGETBERUL;double;input;1.00E-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2761;451;CSENSTARTINGRXPWR(DBM);double;input;-12;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2762;451;CSENALIGNRXPWR(DBM);double;input;-7;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2763;451;COEFCSENADDSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2764;451;IsBerQuickTest;bool;input;false;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2765;452;LOSA;double;output;-32768;-29;-16;1;0;0;1;
新增一笔资料为:ID=2766;452;LOSD;double;output;-32768;-29;-13;1;0;0;1;
新增一笔资料为:ID=2767;452;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2768;452;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2769;452;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2770;452;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2771;452;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
新增一笔资料为:ID=2772;452;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2773;453;FIXEDMODDAC(MA);UInt16;input;300;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2774;453;ARRAYLISTXDMICOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2775;453;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2776;453;IBIASADCORTXPOWERADC;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2777;453;ARRAYIBIAS(MA);ArrayList;input;300,400,500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2778;453;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2779;453;ISTEMPRELATIVE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2780;453;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2781;453;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2782;453;HighestCalTemp;double;input;60;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2783;453;LowestCalTemp;double;input;20;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2784;453;ISNEWALGORITHM;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2785;454;IMODMIN(MA);UInt16;input;100;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2786;454;ARRAYLISTTXMODCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2787;454;1STOR2STORPIDTXLOP;byte;input;0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2788;454;1STOR2STORPIDER;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2789;454;ISOPENLOOPORCLOSELOOPORBOTH;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2790;454;IMODSTART(MA);UInt16;input;250;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2791;454;IMODSTEP;byte;input;64;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2792;454;IMODMETHOD;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2793;454;TXERTOLERANCE(DB);double;input;0.2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2794;454;TXERTARGET(DB);double;input;4;3.5;6;1;0;1;0;
新增一笔资料为:ID=2795;454;ARRAYLISTTXPOWERCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2796;454;IBIASMETHOD;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2797;454;IBIASSTEP(MA);byte;input;32;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2798;454;IBIASSTART(MA);UInt16;input;40;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2799;454;IBIASMIN(MA);UInt16;input;35;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2800;454;IBIASMAX(MA);UInt16;input;45;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2801;454;FIXEDMOD(MA);UInt16;input;500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2802;454;TXLOPTOLERANCE(UW);double;input;500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2803;454;TXLOPTARGET(UW);double;input;1500;1000;2000;1;0;1;0;
新增一笔资料为:ID=2804;454;IMODMAX(MA);UInt16;input;700;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2805;454;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2806;454;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2807;454;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2808;454;PIDCOEFARRAY;ArrayList;input;0.01,0.005,0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2809;454;FIXEDIBIAS(MA);UInt32;input;280;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2810;454;FixedIBiasArray;ArrayList;input;520,520,520,520;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2811;454;FixedModArray;ArrayList;input;0,0,0,0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2812;455;1STOR2STORPID;byte;input;1;0;0;0;0;0;0;
新增一笔资料为:ID=2813;455;ARRAYLISTDMITEMPCOEF;ArrayList;output;-32768;0;0;0;1;0;0;
新增一笔资料为:ID=2814;456;GENERALVCC(V);double;input;3.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2815;456;ARRAYLISTVCC(V);ArrayList;input;3.1,3.3,3.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2816;456;ARRAYLISTDMIVCCCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2817;456;1STOR2STORPID;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2818;457;ARRAYLISTDMIRXCOEF;ArrayList;output;;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2819;457;ARRAYLISTRXPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2820;457;HasOffset;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2821;457;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2822;458;LOSDVOLTAGETUNESTEP(V);byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2823;458;LOSAVOLTAGESTARTVALUE(V);UInt16;input;14;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2824;458;IsAdjustLos;bool;input;True;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2825;458;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2826;458;LOSDVOLTAGEUPERLIMIT(V);UInt16;input;30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2827;458;LOSDVOLTAGESTARTVALUE(V);UInt16;input;14;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2828;458;LOSDINPUTPOWER;double;input;-21;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2829;458;LOSAVOLTAGETUNESTEP(V);byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2830;458;LOSAVOLTAGEUPERLIMIT(V);UInt16;input;30;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2831;458;LosValue(V);UINT32;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2832;458;LOSDVOLTAGELOWLIMIT(V);UInt16;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2833;458;LOSAINPUTPOWER;double;input;-21;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2834;458;LOSAVOLTAGELOWLIMIT(V);UInt16;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2835;459;FIXEDMODDAC(MA);UInt16;input;300;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2836;459;ARRAYLISTXDMICOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2837;459;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2838;459;IBIASADCORTXPOWERADC;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2839;459;ARRAYIBIAS(MA);ArrayList;input;300,400,500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2840;459;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2841;459;ISTEMPRELATIVE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2842;459;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2843;459;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2844;459;HighestCalTemp;double;input;60;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2845;459;LowestCalTemp;double;input;20;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2846;459;ISNEWALGORITHM;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2847;460;IMODMIN(MA);UInt16;input;100;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2848;460;ARRAYLISTTXMODCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2849;460;1STOR2STORPIDTXLOP;byte;input;0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2850;460;1STOR2STORPIDER;byte;input;2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2851;460;ISOPENLOOPORCLOSELOOPORBOTH;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2852;460;IMODSTART(MA);UInt16;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2853;460;IMODSTEP;byte;input;64;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2854;460;IMODMETHOD;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2855;460;TXERTOLERANCE(DB);double;input;0.2;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2856;460;TXERTARGET(DB);double;input;4;3.5;6;1;0;1;0;
新增一笔资料为:ID=2857;460;ARRAYLISTTXPOWERCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2858;460;IBIASMETHOD;byte;input;1;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2859;460;IBIASSTEP(MA);byte;input;32;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2860;460;IBIASSTART(MA);UInt16;input;40;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2861;460;IBIASMIN(MA);UInt16;input;35;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2862;460;IBIASMAX(MA);UInt16;input;45;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2863;460;FIXEDMOD(MA);UInt16;input;500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2864;460;TXLOPTOLERANCE(UW);double;input;500;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2865;460;TXLOPTARGET(UW);double;input;1500;1000;2000;1;0;1;0;
新增一笔资料为:ID=2866;460;IMODMAX(MA);UInt16;input;1000;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2867;460;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2868;460;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2869;460;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2870;460;PIDCOEFARRAY;ArrayList;input;0.01,0.005,0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2871;460;FIXEDIBIAS(MA);UInt32;input;280;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2872;460;FixedIBiasArray;ArrayList;input;520,520,520,520;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2873;460;FixedModArray;ArrayList;input;0,0,0,0;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2874;461;1STOR2STORPID;byte;input;1;0;0;0;0;0;0;
新增一笔资料为:ID=2875;461;ARRAYLISTDMITEMPCOEF;ArrayList;output;-32768;0;0;0;1;0;0;
新增一笔资料为:ID=2876;462;GENERALVCC(V);double;input;3.3;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2877;462;ARRAYLISTVCC(V);ArrayList;input;3.1,3.3,3.5;-32768;32767;0;0;0;0;
新增一笔资料为:ID=2878;462;ARRAYLISTDMIVCCCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
新增一笔资料为:ID=2879;462;1STOR2STORPID;byte;input;1;-32768;32767;0;0;0;0;
**表[TopoEquipment]修改如下**
新增一笔资料为:ID=163;24;1;Powersupply;E3631_144_Powersupply;0;
新增一笔资料为:ID=164;24;1;Scope;FLEX86100_145_Scope;0;
新增一笔资料为:ID=165;24;1;PPG;MP1800PPG_146_PPG;0;
新增一笔资料为:ID=166;24;1;Thermocontroller;TPO4300_147_Thermocontroller;0;
新增一笔资料为:ID=167;24;1;OpticalSwitch;AQ2211OpticalSwitch_148_OpticalSwitch;0;
新增一笔资料为:ID=168;24;1;Attennuator;AQ2211Atten_149_Attennuator;0;
新增一笔资料为:ID=169;24;1;ErrorDetector;MP1800ED_150_ErrorDetector;0;
新增一笔资料为:ID=170;24;1;OpticalSwitch;AQ2211OpticalSwitch_151_OpticalSwitch;0;
**表[TopoEquipmentParameter]修改如下**
新增一笔资料为:ID=2316;163;Addr;5;
新增一笔资料为:ID=2317;163;IOType;GPIB;
新增一笔资料为:ID=2318;163;Reset;false;
新增一笔资料为:ID=2319;163;Name;E3631;
新增一笔资料为:ID=2320;163;DutChannel;1;
新增一笔资料为:ID=2321;163;OptSourceChannel;2;
新增一笔资料为:ID=2322;163;DutVoltage;3.5;
新增一笔资料为:ID=2323;163;DutCurrent;2.5;
新增一笔资料为:ID=2324;163;OptVoltage;3.5;
新增一笔资料为:ID=2325;163;OptCurrent;1.5;
新增一笔资料为:ID=2326;163;voltageoffset;0.2;
新增一笔资料为:ID=2327;163;currentoffset;0;
新增一笔资料为:ID=2328;164;Addr;7;
新增一笔资料为:ID=2329;164;IOType;GPIB;
新增一笔资料为:ID=2330;164;Reset;false;
新增一笔资料为:ID=2331;164;Name;FLEX86100;
新增一笔资料为:ID=2332;164;configFilePath;1;
新增一笔资料为:ID=2333;164;FlexDcaDataRate;25.78125E+9;
新增一笔资料为:ID=2334;164;FilterSwitch;1;
新增一笔资料为:ID=2335;164;FlexFilterFreq;25.78125;
新增一笔资料为:ID=2336;164;triggerSource;0;
新增一笔资料为:ID=2337;164;FlexTriggerBwlimit;2;
新增一笔资料为:ID=2338;164;opticalSlot;3;
新增一笔资料为:ID=2339;164;elecSlot;4;
新增一笔资料为:ID=2340;164;FlexOptChannel;1;
新增一笔资料为:ID=2341;164;FlexElecChannel;1;
新增一笔资料为:ID=2342;164;FlexDcaWavelength;1;
新增一笔资料为:ID=2343;164;opticalAttSwitch;1;
新增一笔资料为:ID=2344;164;FlexDcaAtt;0;
新增一笔资料为:ID=2345;164;erFactor;0;
新增一笔资料为:ID=2346;164;FlexScale;300;
新增一笔资料为:ID=2347;164;FlexOffset;300;
新增一笔资料为:ID=2348;164;Threshold;0;
新增一笔资料为:ID=2349;164;reference;0;
新增一笔资料为:ID=2350;164;precisionTimebaseModuleSlot;1;
新增一笔资料为:ID=2351;164;precisionTimebaseSynchMethod;1;
新增一笔资料为:ID=2352;164;precisionTimebaseRefClk;6.445e+9;
新增一笔资料为:ID=2353;164;rapidEyeSwitch;1;
新增一笔资料为:ID=2354;164;marginType;1;
新增一笔资料为:ID=2355;164;marginHitType;0;
新增一笔资料为:ID=2356;164;marginHitRatio;5e-006;
新增一笔资料为:ID=2357;164;marginHitCount;0;
新增一笔资料为:ID=2358;164;acqLimitType;0;
新增一笔资料为:ID=2359;164;acqLimitNumber;700;
新增一笔资料为:ID=2360;164;opticalMaskName;c:\scope\masks\25.78125_100GBASE-LR4_Tx_Optical_D31.MSK;
新增一笔资料为:ID=2361;164;elecMaskName;c:\Eye;
新增一笔资料为:ID=2362;164;opticalEyeSavePath;D:\Eye\;
新增一笔资料为:ID=2363;164;elecEyeSavePath;D:\Eye\;
新增一笔资料为:ID=2364;164;ERFACTORSWITCH;1;
新增一笔资料为:ID=2365;165;Addr;1;
新增一笔资料为:ID=2366;165;IOType;GPIB;
新增一笔资料为:ID=2367;165;Reset;false;
新增一笔资料为:ID=2368;165;Name;MP1800PPG;
新增一笔资料为:ID=2369;165;dataRate;25.78128;
新增一笔资料为:ID=2370;165;dataLevelGuardAmpMax;1;
新增一笔资料为:ID=2371;165;dataLevelGuardOffsetMax;0;
新增一笔资料为:ID=2372;165;dataLevelGuardOffsetMin;0;
新增一笔资料为:ID=2373;165;dataLevelGuardSwitch;0;
新增一笔资料为:ID=2374;165;dataAmplitude;0.5;
新增一笔资料为:ID=2375;165;dataCrossPoint;50;
新增一笔资料为:ID=2376;165;configFilePath;0;
新增一笔资料为:ID=2377;165;slot;1;
新增一笔资料为:ID=2378;165;clockSource;0;
新增一笔资料为:ID=2379;165;auxOutputClkDiv;0;
新增一笔资料为:ID=2380;165;prbsLength;31;
新增一笔资料为:ID=2381;165;patternType;0;
新增一笔资料为:ID=2382;165;dataSwitch;1;
新增一笔资料为:ID=2383;165;dataTrackingSwitch;1;
新增一笔资料为:ID=2384;165;dataAcModeSwitch;0;
新增一笔资料为:ID=2385;165;dataLevelMode;0;
新增一笔资料为:ID=2386;165;clockSwitch;1;
新增一笔资料为:ID=2387;165;outputSwitch;1;
新增一笔资料为:ID=2388;165;TotalChannel;4;
新增一笔资料为:ID=2389;166;Addr;23;
新增一笔资料为:ID=2390;166;IOType;GPIB;
新增一笔资料为:ID=2391;166;Reset;False;
新增一笔资料为:ID=2392;166;Name;TPO4300;
新增一笔资料为:ID=2393;166;FLSE;14;
新增一笔资料为:ID=2394;166;ULIM;90;
新增一笔资料为:ID=2395;166;LLIM;-20;
新增一笔资料为:ID=2396;166;Sensor;1;
新增一笔资料为:ID=2397;167;Addr;20;
新增一笔资料为:ID=2398;167;IOType;GPIB;
新增一笔资料为:ID=2399;167;Reset;false;
新增一笔资料为:ID=2400;167;Name;AQ2011OpticalSwitch;
新增一笔资料为:ID=2401;167;OpticalSwitchSlot;1;
新增一笔资料为:ID=2402;167;SwitchChannel;1;
新增一笔资料为:ID=2403;167;ToChannel;1;
新增一笔资料为:ID=2404;168;Addr;20;
新增一笔资料为:ID=2405;168;IOType;GPIB;
新增一笔资料为:ID=2406;168;Reset;false;
新增一笔资料为:ID=2407;168;Name;AQ2211Atten;
新增一笔资料为:ID=2408;168;TOTALCHANNEL;4;
新增一笔资料为:ID=2409;168;AttValue;20;
新增一笔资料为:ID=2410;168;AttSlot;2;
新增一笔资料为:ID=2411;168;WAVELENGTH;1310,1310,1310,1310;
新增一笔资料为:ID=2412;168;AttChannel;1;
新增一笔资料为:ID=2413;169;Addr;1;
新增一笔资料为:ID=2414;169;IOType;GPIB;
新增一笔资料为:ID=2415;169;Reset;false;
新增一笔资料为:ID=2416;169;Name;MP1800ED;
新增一笔资料为:ID=2417;169;slot;3;
新增一笔资料为:ID=2418;169;TotalChannel;4;
新增一笔资料为:ID=2419;169;currentChannel;1;
新增一笔资料为:ID=2420;169;dataInputInterface;2;
新增一笔资料为:ID=2421;169;prbsLength;31;
新增一笔资料为:ID=2422;169;errorResultZoom;1;
新增一笔资料为:ID=2423;169;edGatingMode;1;
新增一笔资料为:ID=2424;169;edGatingUnit;0;
新增一笔资料为:ID=2425;169;edGatingTime;5;
新增一笔资料为:ID=2426;170;Addr;20;
新增一笔资料为:ID=2427;170;IOType;GPIB;
新增一笔资料为:ID=2428;170;Reset;false;
新增一笔资料为:ID=2429;170;Name;AQ2011OpticalSwitch;
新增一笔资料为:ID=2430;170;OpticalSwitchSlot;3;
新增一笔资料为:ID=2431;170;SwitchChannel;1;
新增一笔资料为:ID=2432;170;ToChannel;1;
==User:terry.yin于2014/10/31 16:39:35修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (106, N'terry.yin', CAST(0x0000A3D50110F5BC AS DateTime), CAST(0x0000A3D5011E69E0 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (107, N'terry.yin', CAST(0x0000A3D80098E374 AS DateTime), CAST(0x0000A3D800A51FE0 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/11/3 9:16:54修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
修改前资料为:ItemName:FMT-3.3-1-4;
修改前资料为:ItemName:FMT;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/3 9:18:35修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (108, N'Terry.Yin', CAST(0x0000A3D800A34094 AS DateTime), CAST(0x0000A3D800A355AC AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (109, N'Terry.Yin', CAST(0x0000A3D800A38234 AS DateTime), CAST(0x0000A3D800A3AA0C AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (110, N'Terry.Yin', CAST(0x0000A3D800AA6CE8 AS DateTime), CAST(0x0000A3D800AA8A34 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (111, N'Terry.Yin', CAST(0x0000A3D800AB53C4 AS DateTime), CAST(0x0000A3D800AB5BF8 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (112, N'terry.yin', CAST(0x0000A3D800AE77AC AS DateTime), CAST(0x0000A3D800B0C19C AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/11/3 10:36:01修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
修改前资料为:SEQ:1;
修改前资料为:SEQ:2;
修改前资料为:SEQ:1;
修改前资料为:SEQ:3;
修改前资料为:SEQ:1;
修改前资料为:SEQ:4;
修改前资料为:SEQ:1;Role:0;
修改前资料为:SEQ:5;Role:1;
修改前资料为:SEQ:1;
修改前资料为:SEQ:6;
修改前资料为:SEQ:1;
修改前资料为:SEQ:7;
修改前资料为:SEQ:1;Role:0;
修改前资料为:SEQ:8;Role:2;
**表[TopoEquipmentParameter]修改如下**
新增一笔资料为:ID=2433;163;opendelay;2000;
新增一笔资料为:ID=2434;163;closedelay;500;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (113, N'terry.yin', CAST(0x0000A3D800B16A5C AS DateTime), CAST(0x0000A3D800B2050C AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (114, N'Terry.Yin', CAST(0x0000A3D800CEA5F4 AS DateTime), CAST(0x0000A3D800D567A4 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (115, N'Terry.Yin', CAST(0x0000A3D800D56D80 AS DateTime), CAST(0x0000A3D801229A24 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (116, N'terry.yin', CAST(0x0000A3D800E179E0 AS DateTime), CAST(0x0000A3D800E45520 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (117, N'terry.yin', CAST(0x0000A3D800E4690C AS DateTime), CAST(0x0000A3D800E47014 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (118, N'Terry.Yin', CAST(0x0000A3D800EA13D4 AS DateTime), CAST(0x0000A3D800EAD788 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:Terry.Yin于2014/11/03 14:13:47修改==
**表[GlobalMSAEEPROMInitialize]修改如下**
新增一笔资料为:ID=9;1;test01;QSFP;0AFF060200000000000000056700014B00000040417269737461204E6574776F726B732007001C73515346502D3430472D554E495620202030316658251C469B000000D85844503133313734303030312020202031343130313520200800007A1003000000000000000000000000000000000000000002F8000000008B451DF3;33FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF;;;;;;;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (119, N'terry.yin', CAST(0x0000A3D800ECE938 AS DateTime), CAST(0x0000A3D800F558FC AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (120, N'terry.yin', CAST(0x0000A3D80129E874 AS DateTime), CAST(0x0000A3D8012AB588 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (121, N'terry.yin', CAST(0x0000A3D8012CD9F8 AS DateTime), CAST(0x0000A3D8012D1364 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/11/3 18:15:57修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
修改前资料为:Channel:0;
修改前资料为:Channel:4;
修改前资料为:Channel:0;
修改前资料为:Channel:4;
修改前资料为:Channel:0;
修改前资料为:Channel:4;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (122, N'terry.yin', CAST(0x0000A3D8012DB2C4 AS DateTime), CAST(0x0000A3D8013E7C08 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/11/3 18:32:08修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/3 18:38:11修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
**表[TopoTestModel]修改如下**
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/3 18:51:26修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
**表[TopoTestModel]修改如下**
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/3 18:52:16修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (123, N'terry.yin', CAST(0x0000A3D801425918 AS DateTime), CAST(0x0000A3D801462CC8 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/11/3 19:34:02修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
修改前资料为:Channel:4;
修改前资料为:Channel:0;
修改前资料为:Channel:4;
修改前资料为:Channel:0;
修改前资料为:Channel:4;
修改前资料为:Channel:0;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (124, N'Terry.Yin', CAST(0x0000A3D9008FFD54 AS DateTime), CAST(0x0000A3D900904728 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (125, N'Terry.Yin', CAST(0x0000A3D90090D620 AS DateTime), CAST(0x0000A3D900979B54 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (126, N'Terry.Yin', CAST(0x0000A3D9009AFD58 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登入', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (127, N'Terry.Yin', CAST(0x0000A3D9009CFD74 AS DateTime), CAST(0x0000A3D9009D05A8 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (128, N'Terry.Yin', CAST(0x0000A3D900A7169C AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登入', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (129, N'Terry.Yin', CAST(0x0000A3D900A770D8 AS DateTime), CAST(0x0000A3D900AA9268 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (130, N'Terry.Yin', CAST(0x0000A3D900AAA1A4 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登入', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (131, N'Terry.Yin', CAST(0x0000A3D900AB7CC8 AS DateTime), CAST(0x0000A3D900AB8880 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (132, N'Terry.Yin', CAST(0x0000A3D900ABE190 AS DateTime), CAST(0x0000A3D900ABF7D4 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (133, N'Terry.Yin', CAST(0x0000A3D900AC245C AS DateTime), CAST(0x0000A3D900AC3AA0 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (134, N'Terry.Yin', CAST(0x0000A3D900AC4B08 AS DateTime), CAST(0x0000A3D900AC9284 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (135, N'Terry.Yin', CAST(0x0000A3D900ACA2EC AS DateTime), CAST(0x0000A3D900ACB0FC AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (136, N'Terry.Yin', CAST(0x0000A3D900ACBF0C AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登入', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (137, N'Terry.Yin', CAST(0x0000A3D900AD2C08 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登入', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (138, N'terry.yin', CAST(0x0000A3D900B4E178 AS DateTime), CAST(0x0000A3D9010DA54C AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/11/4 10:58:56修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
修改前资料为:ItemValue:700;
修改前资料为:ItemValue:1000;
==User:terry.yin于2014/11/4 15:32:42修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/4 16:21:35修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
修改前资料为:IgnoreFlag:True;
修改前资料为:IgnoreFlag:False;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (139, N'terry.yin', CAST(0x0000A3D900B911BC AS DateTime), CAST(0x0000A3D900B95F14 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (140, N'terry.yin', CAST(0x0000A3D900F63EAC AS DateTime), CAST(0x0000A3D900F64F14 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (141, N'terry.yin', CAST(0x0000A3D900F68E5C AS DateTime), CAST(0x0000A3D90122562C AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'==User:terry.yin于2014/11/4 15:02:40修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:ID=2824,ItemValue:True;
修改前资料为:ID=2824,ItemValue:false;
修改前资料为:ID=2831,ItemValue:1;
修改前资料为:ID=2831,ItemValue:4;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/4 15:19:49修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:ID=2675,ItemValue:0.5;
修改前资料为:ID=2675,ItemValue:5;
修改前资料为:ID=2676,ItemValue:0.5;
修改前资料为:ID=2676,ItemValue:5;
修改前资料为:ID=2716,ItemValue:0.5;
修改前资料为:ID=2716,ItemValue:5;
修改前资料为:ID=2717,ItemValue:0.5;
修改前资料为:ID=2717,ItemValue:5;
修改前资料为:ID=2757,ItemValue:0.5;
修改前资料为:ID=2757,ItemValue:5;
修改前资料为:ID=2758,ItemValue:0.5;
修改前资料为:ID=2758,ItemValue:5;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (142, N'terry.yin', CAST(0x0000A3D900FBA6A8 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'Login Name =terry.yin login successfully at computer=INPCSZ0443[10.160.80.85];', N'Login Name =terry.yinbeen modified Nothing...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (143, N'Terry.Yin', CAST(0x0000A3D900FC3CA8 AS DateTime), CAST(0x0000A3D900FECBBC AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (144, N'Terry.Yin', CAST(0x0000A3D9010242D8 AS DateTime), CAST(0x0000A3D90102C744 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (145, N'Terry.Yin', CAST(0x0000A3D90109C4B8 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登入', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (146, N'terry.yin', CAST(0x0000A3D90116AE94 AS DateTime), CAST(0x0000A3D90117DCEC AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (147, N'Terry.Yin', CAST(0x0000A3DA0099CF00 AS DateTime), CAST(0x0000A3DA0099DBE4 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (148, N'Terry.Yin', CAST(0x0000A3DA009C3060 AS DateTime), CAST(0x0000A3DA009C5838 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (149, N'Terry.Yin', CAST(0x0000A3DA009E0E80 AS DateTime), CAST(0x0000A3DA009E1DBC AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (150, N'Terry.Yin', CAST(0x0000A3DA009E3C34 AS DateTime), CAST(0x0000A3DA009E4A44 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (151, N'Terry.Yin', CAST(0x0000A3DA00A1B350 AS DateTime), CAST(0x0000A3DA00A1DEAC AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (152, N'Terry.Yin', CAST(0x0000A3DA00A1F3C4 AS DateTime), CAST(0x0000A3DA00A20C60 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (153, N'terry.yin', CAST(0x0000A3DA00A33F68 AS DateTime), CAST(0x0000A3DA00A3A1D8 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (154, N'terry.yin', CAST(0x0000A3DA00A3CE60 AS DateTime), CAST(0x0000A3DA00A48D64 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:terry.yin于2014/11/05 9:59:00修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (155, N'terry.yin', CAST(0x0000A3DA00A45074 AS DateTime), CAST(0x0000A3DA00A4E8CC AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/11/5 9:59:51修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
OriginalData:ID=2756,SpecMax:-11;
NewData:ID=2756,SpecMax:-9;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/5 10:00:19修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
OriginalData:ID=2674,SpecMax:-11;
NewData:ID=2674,SpecMax:-9;
OriginalData:ID=2715,SpecMax:-11;
NewData:ID=2715,SpecMax:-9;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/5 10:00:21修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (156, N'terry.yin', CAST(0x0000A3DA00A52490 AS DateTime), CAST(0x0000A3DA00A5B130 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'==User:terry.yin于2014/11/05 10:01:39修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/05 10:02:42修改==
**表[TopoTestPlan]修改如下**
OriginalData:ID=13,IgnoreFlag:False;
NewData:ID=13,IgnoreFlag:True;
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (157, N'Terry.Yin', CAST(0x0000A3DA00A53624 AS DateTime), CAST(0x0000A3DA00A55BA4 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (158, N'terry.yin', CAST(0x0000A3DA00A64F64 AS DateTime), CAST(0x0000A3DA00A6647C AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/11/5 10:05:47修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
Data Deleted:ID=153;7;2;Attennuator;AQ2211Atten;0;
**表[TopoEquipmentParameter]修改如下**
Data Deleted:ID=2172;153;Addr;20;
Data Deleted:ID=2173;153;IOType;GPIB;
Data Deleted:ID=2174;153;Reset;False;
Data Deleted:ID=2175;153;Name;AQ2211Atten;
Data Deleted:ID=2176;153;TOTALCHANNEL;4;
Data Deleted:ID=2177;153;AttValue;20;
Data Deleted:ID=2178;153;AttSlot;1;
Data Deleted:ID=2179;153;WAVELENGTH;1270,1290,1310,1330;
Data Deleted:ID=2180;153;AttChannel;1;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (159, N'terry.yin', CAST(0x0000A3DA00A98BE8 AS DateTime), CAST(0x0000A3DA00B1FA80 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/11/5 10:17:34修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/5 10:21:41修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
Data Added:ID=2435;154;opendelay;2000;
Data Added:ID=2436;154;closedelay;500;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (160, N'terry.yin', CAST(0x0000A3DA00AFD4E4 AS DateTime), CAST(0x0000A3DA00B47DDC AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'Login Name =terry.yinbeen modified Nothing...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (161, N'Terry.Yin', CAST(0x0000A3DA00B349A8 AS DateTime), CAST(0x0000A3DA00B35560 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (162, N'Terry.Yin', CAST(0x0000A3DA00B35FEC AS DateTime), CAST(0x0000A3DA00B36370 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (163, N'Terry.Yin', CAST(0x0000A3DA00B37180 AS DateTime), CAST(0x0000A3DA00B380BC AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (164, N'Terry.Yin', CAST(0x0000A3DA00B66B38 AS DateTime), CAST(0x0000A3DA00B96E50 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (165, N'Terry.Yin', CAST(0x0000A3DA00B98110 AS DateTime), CAST(0x0000A3DA00B9A690 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (166, N'Terry.Yin', CAST(0x0000A3DA00B9FAF0 AS DateTime), CAST(0x0000A3DA00BA29D0 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (167, N'Terry.Yin', CAST(0x0000A3DA00BA4CF8 AS DateTime), CAST(0x0000A3DA00BA66C0 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (168, N'Terry.Yin', CAST(0x0000A3DA00BACA5C AS DateTime), CAST(0x0000A3DA00BAEC58 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (169, N'Terry.Yin', CAST(0x0000A3DA00BB18E0 AS DateTime), CAST(0x0000A3DA00BB32A8 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (170, N'Terry.Yin', CAST(0x0000A3DA00BD513C AS DateTime), CAST(0x0000A3DA00BD7464 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (171, N'terry.yin', CAST(0x0000A3DA00BEC710 AS DateTime), CAST(0x0000A3DA00BF54DC AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'Login Name =terry.yinbeen modified Nothing...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (172, N'terry.yin', CAST(0x0000A3DA00D61D48 AS DateTime), CAST(0x0000A3DA01206EAC AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'用户terry.yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (173, N'Terry.Yin', CAST(0x0000A3DA00E5743C AS DateTime), CAST(0x0000A3DA00E607E4 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (174, N'Terry.Yin', CAST(0x0000A3DA00E621AC AS DateTime), CAST(0x0000A3DA00E636C4 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (175, N'Terry.Yin', CAST(0x0000A3DA00E682F0 AS DateTime), CAST(0x0000A3DA00E6B7AC AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (176, N'Terry.Yin', CAST(0x0000A3DA00E725D4 AS DateTime), CAST(0x0000A3DA00E7444C AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (177, N'terry.yin', CAST(0x0000A3DA00E93784 AS DateTime), CAST(0x0000A3DA00EA9A98 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'Login Name =terry.yinbeen modified Nothing...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (178, N'terry.yin', CAST(0x0000A3DB00984414 AS DateTime), CAST(0x0000A3DB0098E374 AS DateTime), N'MaintainATSPlan', N'用户terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'==User:terry.yin于2014/11/6 9:16:03修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
修改前资料为:ID=2675,ItemValue:5;
修改前资料为:ID=2675,ItemValue:8;
修改前资料为:ID=2676,ItemValue:5;
修改前资料为:ID=2676,ItemValue:8;
修改前资料为:ID=2716,ItemValue:5;
修改前资料为:ID=2716,ItemValue:8;
修改前资料为:ID=2717,ItemValue:5;
修改前资料为:ID=2717,ItemValue:8;
修改前资料为:ID=2757,ItemValue:5;
修改前资料为:ID=2757,ItemValue:8;
修改前资料为:ID=2758,ItemValue:5;
修改前资料为:ID=2758,ItemValue:8;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (179, N'Terry.Yin', CAST(0x0000A3DB00ACF29C AS DateTime), CAST(0x0000A3DB00AD0304 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (180, N'terry.yin', CAST(0x0000A3DB00BB11D8 AS DateTime), CAST(0x0000A3DB00BB6188 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'Login Name =terry.yinbeen modified Nothing...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (181, N'Terry.Yin', CAST(0x0000A3DB0103D01C AS DateTime), CAST(0x0000A3DB0110A288 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (182, N'Terry.Yin', CAST(0x0000A3DB011E8AB0 AS DateTime), CAST(0x0000A3DB011EA34C AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (183, N'Terry.Yin', CAST(0x0000A3DB011EACAC AS DateTime), CAST(0x0000A3DB013C4154 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (184, N'terry.yin', CAST(0x0000A3DC00A09D94 AS DateTime), CAST(0x0000A3DC00A12584 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/11/7 9:45:48修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
OriginalData:ID=2803,SpecMax:2000;
NewData:ID=2803,SpecMax:4000;
OriginalData:ID=2865,SpecMax:2000;
NewData:ID=2865,SpecMax:4000;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (185, N'terry.yin', CAST(0x0000A3DC00A6F6F8 AS DateTime), CAST(0x0000A3DC00E10384 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'Login Name =terry.yinbeen modified Nothing...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (186, N'Terry.Yin', CAST(0x0000A3DC00BA4AA0 AS DateTime), CAST(0x0000A3DC00BA60E4 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (187, N'Terry.Yin', CAST(0x0000A3DC00BA9A50 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登入', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (188, N'Terry.Yin', CAST(0x0000A3DC00DC9B00 AS DateTime), CAST(0x0000A3DC00DD0DD8 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (189, N'Terry.Yin', CAST(0x0000A3DC00DD2674 AS DateTime), CAST(0x0000A3DC00DD43C0 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (190, N'terry.yin', CAST(0x0000A3DC01013FDC AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'Login Name =terry.yin login successfully at computer=INPCSZ0228[10.160.80.46];', N'Login Name =terry.yinbeen modified Nothing...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (191, N'Terry.Yin', CAST(0x0000A3DF00B1F24C AS DateTime), CAST(0x0000A3DF00B5B594 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (192, N'terry.yin', CAST(0x0000A3DF00B4B61C AS DateTime), CAST(0x0000A3DF00B4C8DC AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'Login Name =terry.yinbeen modified Nothing...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (193, N'terry.yin', CAST(0x0000A3DF00B4D6EC AS DateTime), CAST(0x0000A3DF00B64A68 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/11/10 11:02:05修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
OriginalData:ID=427,EquipmentList:E3631_144_Powersupply,TPO4300_147_Thermocontroller;IgnoreFlag:False;
NewData:ID=427,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=428,EquipmentList:E3631_144_Powersupply;IgnoreFlag:False;
NewData:ID=428,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=429,EquipmentList:E3631_144_Powersupply;IgnoreFlag:False;
NewData:ID=429,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=430,EquipmentList:E3631_144_Powersupply,FLEX86100_145_Scope;
NewData:ID=430,EquipmentList:;
OriginalData:ID=431,EquipmentList:E3631_144_Powersupply;IgnoreFlag:False;
NewData:ID=431,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=432,EquipmentList:E3631_144_Powersupply,AQ2211Atten_149_Attennuator;IgnoreFlag:False;
NewData:ID=432,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=433,EquipmentList:E3631_144_Powersupply,AQ2211Atten_149_Attennuator,MP1800ED_150_ErrorDetector;IgnoreFlag:False;
NewData:ID=433,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=434,EquipmentList:E3631_144_Powersupply,AQ2211Atten_149_Attennuator;IgnoreFlag:False;
NewData:ID=434,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=437,EquipmentList:E3631_144_Powersupply,TPO4300_147_Thermocontroller;IgnoreFlag:False;
NewData:ID=437,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=438,EquipmentList:E3631_144_Powersupply;IgnoreFlag:False;
NewData:ID=438,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=439,EquipmentList:E3631_144_Powersupply;IgnoreFlag:False;
NewData:ID=439,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=440,EquipmentList:E3631_144_Powersupply;IgnoreFlag:False;
NewData:ID=440,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=441,EquipmentList:E3631_144_Powersupply,AQ2211Atten_149_Attennuator;IgnoreFlag:False;
NewData:ID=441,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=442,EquipmentList:E3631_144_Powersupply,AQ2211Atten_149_Attennuator,MP1800ED_150_ErrorDetector;IgnoreFlag:False;
NewData:ID=442,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=443,EquipmentList:E3631_144_Powersupply,AQ2211Atten_149_Attennuator;IgnoreFlag:False;
NewData:ID=443,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=446,EquipmentList:E3631_144_Powersupply;IgnoreFlag:False;
NewData:ID=446,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=447,EquipmentList:E3631_144_Powersupply,TPO4300_147_Thermocontroller;IgnoreFlag:False;
NewData:ID=447,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=448,EquipmentList:E3631_144_Powersupply;IgnoreFlag:False;
NewData:ID=448,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=449,EquipmentList:E3631_144_Powersupply;IgnoreFlag:False;
NewData:ID=449,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=450,EquipmentList:E3631_144_Powersupply,AQ2211Atten_149_Attennuator;IgnoreFlag:False;
NewData:ID=450,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=451,EquipmentList:E3631_144_Powersupply,AQ2211Atten_149_Attennuator,MP1800ED_150_ErrorDetector;IgnoreFlag:False;
NewData:ID=451,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=452,EquipmentList:E3631_144_Powersupply,AQ2211Atten_149_Attennuator;IgnoreFlag:False;
NewData:ID=452,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=455,EquipmentList:E3631_144_Powersupply,TPO4300_147_Thermocontroller;IgnoreFlag:False;
NewData:ID=455,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=456,EquipmentList:E3631_144_Powersupply;IgnoreFlag:False;
NewData:ID=456,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=457,EquipmentList:E3631_144_Powersupply,AQ2211Atten_149_Attennuator;IgnoreFlag:False;
NewData:ID=457,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=458,EquipmentList:E3631_144_Powersupply,AQ2211Atten_149_Attennuator;IgnoreFlag:False;
NewData:ID=458,EquipmentList:;IgnoreFlag:True;
OriginalData:ID=462,EquipmentList:E3631_144_Powersupply;IgnoreFlag:False;
NewData:ID=462,EquipmentList:;IgnoreFlag:True;
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (194, N'Terry.Yin', CAST(0x0000A3DF00B5D2E0 AS DateTime), CAST(0x0000A3DF00C9D8A8 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (195, N'terry.yin', CAST(0x0000A3DF00BAE7A8 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'Login Name =terry.yin login successfully at computer=INPCSZ0228[10.160.80.46];', N'==User:terry.yin于2014/11/10 11:22:25修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
OriginalData:ID=428,IgnoreFlag:True;
NewData:ID=428,IgnoreFlag:False;
OriginalData:ID=429,IgnoreFlag:True;
NewData:ID=429,IgnoreFlag:False;
OriginalData:ID=431,IgnoreFlag:True;
NewData:ID=431,IgnoreFlag:False;
OriginalData:ID=432,IgnoreFlag:True;
NewData:ID=432,IgnoreFlag:False;
OriginalData:ID=433,IgnoreFlag:True;
NewData:ID=433,IgnoreFlag:False;
OriginalData:ID=434,IgnoreFlag:True;
NewData:ID=434,IgnoreFlag:False;
OriginalData:ID=437,IgnoreFlag:True;
NewData:ID=437,IgnoreFlag:False;
OriginalData:ID=438,IgnoreFlag:True;
NewData:ID=438,IgnoreFlag:False;
OriginalData:ID=439,IgnoreFlag:True;
NewData:ID=439,IgnoreFlag:False;
OriginalData:ID=440,IgnoreFlag:True;
NewData:ID=440,IgnoreFlag:False;
OriginalData:ID=441,IgnoreFlag:True;
NewData:ID=441,IgnoreFlag:False;
OriginalData:ID=442,IgnoreFlag:True;
NewData:ID=442,IgnoreFlag:False;
OriginalData:ID=443,IgnoreFlag:True;
NewData:ID=443,IgnoreFlag:False;
OriginalData:ID=446,IgnoreFlag:True;
NewData:ID=446,IgnoreFlag:False;
OriginalData:ID=447,IgnoreFlag:True;
NewData:ID=447,IgnoreFlag:False;
OriginalData:ID=448,IgnoreFlag:True;
NewData:ID=448,IgnoreFlag:False;
OriginalData:ID=449,IgnoreFlag:True;
NewData:ID=449,IgnoreFlag:False;
OriginalData:ID=450,IgnoreFlag:True;
NewData:ID=450,IgnoreFlag:False;
OriginalData:ID=451,IgnoreFlag:True;
NewData:ID=451,IgnoreFlag:False;
OriginalData:ID=452,IgnoreFlag:True;
NewData:ID=452,IgnoreFlag:False;
OriginalData:ID=455,IgnoreFlag:True;
NewData:ID=455,IgnoreFlag:False;
OriginalData:ID=456,IgnoreFlag:True;
NewData:ID=456,IgnoreFlag:False;
OriginalData:ID=457,IgnoreFlag:True;
NewData:ID=457,IgnoreFlag:False;
OriginalData:ID=458,IgnoreFlag:True;
NewData:ID=458,IgnoreFlag:False;
OriginalData:ID=462,IgnoreFlag:True;
NewData:ID=462,IgnoreFlag:False;
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (196, N'terry.yin', CAST(0x0000A3DF00E2B2C4 AS DateTime), CAST(0x0000A3DF00F17610 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/11/10 13:45:47修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
OriginalData:ID=91,IgnoreFlag:False;
NewData:ID=91,IgnoreFlag:True;
OriginalData:ID=92,IgnoreFlag:False;
NewData:ID=92,IgnoreFlag:True;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/10 13:49:57修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
OriginalData:ID=427,IgnoreFlag:True;
NewData:ID=427,IgnoreFlag:False;
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/10 13:54:28修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/10 14:39:06修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
OriginalData:ID=91,IgnoreFlag:True;
NewData:ID=91,IgnoreFlag:False;
OriginalData:ID=92,IgnoreFlag:True;
NewData:ID=92,IgnoreFlag:False;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (197, N'terry.yin', CAST(0x0000A3DF00FA6338 AS DateTime), CAST(0x0000A3DF00FCD050 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'Login Name =terry.yin modified Nothing...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (198, N'Terry.Yin', CAST(0x0000A3DF00FFDDF4 AS DateTime), CAST(0x0000A3DF00FFE4FC AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (199, N'terry.yin', CAST(0x0000A3DF010449FC AS DateTime), CAST(0x0000A3DF011317D4 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'Login Name =terry.yinbeen modified Nothing...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (200, N'terry.yin', CAST(0x0000A3DF010744E0 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'Login Name =terry.yin login successfully at computer=INPCSZ0443[10.160.80.85];', N'Login Name =terry.yin modified Nothing...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (201, N'terry.yin', CAST(0x0000A3DF01093110 AS DateTime), CAST(0x0000A3DF010EACF8 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'Login Name =terry.yin modified Nothing...')
GO
print 'Processed 200 total records'
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (202, N'terry.yin', CAST(0x0000A3E000AF97F4 AS DateTime), CAST(0x0000A3E000B17740 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'Login Name =terry.yinbeen modified Nothing...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (203, N'Terry.Yin', CAST(0x0000A3E000AFD3B8 AS DateTime), CAST(0x0000A3E000AFE420 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (204, N'Terry.Yin', CAST(0x0000A3E000AFFA64 AS DateTime), CAST(0x0000A3E000B23C20 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (205, N'terry.yin', CAST(0x0000A3E000B14284 AS DateTime), CAST(0x0000A3E000B1579C AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0443[10.160.80.85]登出', N'Login Name =terry.yin modified Nothing...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (206, N'terry.yin', CAST(0x0000A3E000B17E48 AS DateTime), CAST(0x0000A3E001023018 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/11/11 10:46:48修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
Data Added:ID=2489;164;flexsetscaledelay;1000;
Data Added:ID=2490;168;opendelay;1000;
Data Added:ID=2491;168;closedelay;1000;
Data Added:ID=2492;168;setattdelay;1000;
==User:terry.yin于2014/11/11 14:02:22修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/11 14:13:47修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/11 14:28:59修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
OriginalData:ID=2191,ItemValue:0;
NewData:ID=2191,ItemValue:0.2;
==User:terry.yin于2014/11/11 14:34:10修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
Data Added:ID=95;7;FMT-2;2;0;40;3.3;2;10.3125E+9;TempSleep=5;False;
Data Added:ID=96;7;fmt3;3;0;0;3.3;31;10.3125E+9;TempSleep=5;False;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/11 14:37:21修改==
**表[TopoTestPlan]修改如下**
Data Added:ID=26;3;CGR4-v6;1;1;0;SN_CHECK = 0;ApcStyle=1;False;
**表[TopoTestControl]修改如下**
Data Added:ID=97;26;FMT-3.3-20-1;6;0;20;3.3;31;25.78125e+9;TempSleep=60;False;
Data Added:ID=98;26;FMT-3.3-40-1_4;5;0;40;3.3;31;25.78125E+9;TempSleep=60;False;
Data Added:ID=99;26;FMT-60-3.3-1_4;3;0;60;3.3;31;25.78125e+9;TempSleep=5;False;
Data Added:ID=100;26;LP-3.3-20-1-4;1;0;20;3.3;31;25.78125e+9;TempSleep=60;False;
Data Added:ID=101;26;LP-3.3-60-1-4;2;0;60;3.3;31;25.78125e+9;TempSleep=60;False;
**表[TopoTestModel]修改如下**
Data Added:ID=469;97;TestEye;7;3;E3631_144_Powersupply,FLEX86100_145_Scope;False;
Data Added:ID=470;97;TestTempDmi;10;13;;False;
Data Added:ID=471;97;TestVccDmi;13;14;;False;
Data Added:ID=472;97;TestIcc;14;15;;False;
Data Added:ID=473;97;TestTxPowerDmi;9;4;;False;
Data Added:ID=474;97;TestIBiasDmi;15;5;;False;
Data Added:ID=475;97;TestRxPowerDmi;16;9;;False;
Data Added:ID=476;97;TestBer;17;10;;False;
Data Added:ID=477;97;TestRXLosAD;18;8;;False;
Data Added:ID=478;98;TestEye;1;3;E3631_144_Powersupply,FLEX86100_145_Scope;False;
Data Added:ID=479;98;TestTxPowerDmi;2;4;E3631_144_Powersupply,FLEX86100_145_Scope;False;
Data Added:ID=480;98;TestTempDmi;6;13;;False;
Data Added:ID=481;98;TestIcc;7;15;;False;
Data Added:ID=482;98;TestVccDmi;8;14;;False;
Data Added:ID=483;98;TestIBiasDmi;9;5;;False;
Data Added:ID=484;98;TestRxPowerDmi;10;9;;False;
Data Added:ID=485;98;TestBer;11;10;;False;
Data Added:ID=486;98;TestRXLosAD;12;8;;False;
Data Added:ID=487;99;TestEye;1;3;E3631_144_Powersupply,FLEX86100_145_Scope;False;
Data Added:ID=488;99;TestTxPowerDmi;2;4;E3631_144_Powersupply,FLEX86100_145_Scope;False;
Data Added:ID=489;99;TestIBiasDmi;5;5;;False;
Data Added:ID=490;99;TestTempDmi;7;13;;False;
Data Added:ID=491;99;TestVccDmi;8;14;;False;
Data Added:ID=492;99;TestIcc;9;15;;False;
Data Added:ID=493;99;TestRxPowerDmi;11;9;;False;
Data Added:ID=494;99;TestBer;12;10;;False;
Data Added:ID=495;99;TestRXLosAD;13;8;;False;
Data Added:ID=496;100;AdjustTxPowerDmi;1;2;E3631_144_Powersupply,FLEX86100_145_Scope;False;
Data Added:ID=497;100;AdjustEye;2;1;E3631_144_Powersupply,FLEX86100_145_Scope;False;
Data Added:ID=498;100;CalTempDminoProcessingCoef;3;19;;False;
Data Added:ID=499;100;CalVccDminoProcessingCoef;4;20;;False;
Data Added:ID=500;100;CalRxDminoProcessingCoef;5;18;;False;
Data Added:ID=501;100;AdjustLos;6;6;;False;
Data Added:ID=502;101;AdjustTxPowerDmi;1;2;E3631_144_Powersupply,FLEX86100_145_Scope;False;
Data Added:ID=503;101;AdjustEye;2;1;E3631_144_Powersupply,FLEX86100_145_Scope;False;
Data Added:ID=504;101;CalTempDminoProcessingCoef;3;19;E3631_144_Powersupply,TPO4300_147_Thermocontroller;False;
Data Added:ID=505;101;CalVccDminoProcessingCoef;4;20;;False;
**表[TopoTestParameter]修改如下**
Data Added:ID=2920;469;AP(DBM);double;output;-32768;0;3;1;0;0;1;
Data Added:ID=2921;469;JITTERPP(PS);double;output;-32768;-32768;32767;1;0;0;1;
Data Added:ID=2922;469;TXOMA(UW);double;output;-32768;-32768;32767;1;0;0;1;
Data Added:ID=2923;469;FALLTIME(PS);double;output;-32768;-32768;65;1;0;0;1;
Data Added:ID=2924;469;RISETIME(PS);double;output;-32768;-32768;100;1;0;0;1;
Data Added:ID=2925;469;JITTERRMS(PS);double;output;-32768;-32768;30;1;0;0;1;
Data Added:ID=2926;469;MASKMARGIN(%);double;output;-32768;10;90;1;0;0;1;
Data Added:ID=2927;469;ER(DB);double;output;-32768;3.5;6;1;0;0;1;
Data Added:ID=2928;469;ISOPTICALEYEORELECEYE;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=2929;469;CROSSING(%);double;output;-32768;40;60;1;0;0;1;
Data Added:ID=2930;470;DMITEMPERR(C);double;output;-32768;-3;3;1;0;0;1;
Data Added:ID=2931;470;DMITEMP(C);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=2932;471;DMIVCCERR(V);double;output;-32768;-0.165;0.165;1;0;0;1;
Data Added:ID=2933;471;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=2934;472;ICC(MA);double;output;-32768;500;1050;1;0;0;1;
Data Added:ID=2935;473;DMITXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=2936;473;CURRENTTXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=2937;473;DMITXPOWERERR(DB);double;output;-32768;-2;2;1;0;0;1;
Data Added:ID=2938;474;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
Data Added:ID=2939;475;ARRAYLISTRXINPUTPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
Data Added:ID=2940;475;DMIRXPWRMAXERRPOINT(DBM);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=2941;475;DMIRXPWRMAXERR;double;output;-32768;-3;3;1;0;0;1;
Data Added:ID=2942;476;CSENTARGETBER;double;input;1.0E-12;-32768;32767;0;0;0;0;
Data Added:ID=2943;476;COEFCSENSUBSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
Data Added:ID=2944;476;CSEN(DBM);double;output;-32768;-20;-9;1;0;0;1;
Data Added:ID=2945;476;SEARCHTARGETBERSUBSTEP;double;input;8;-32768;32767;0;0;0;0;
Data Added:ID=2946;476;SEARCHTARGETBERADDSTEP;double;input;8;-32768;32767;0;0;0;0;
Data Added:ID=2947;476;SEARCHTARGETBERLL;double;input;1E-8;-32768;32767;0;0;0;0;
Data Added:ID=2948;476;SEARCHTARGETBERUL;double;input;1.00E-7;-32768;32767;0;0;0;0;
Data Added:ID=2949;476;CSENSTARTINGRXPWR(DBM);double;input;-12;-32768;32767;0;0;0;0;
Data Added:ID=2950;476;CSENALIGNRXPWR(DBM);double;input;-7;-32768;32767;0;0;0;0;
Data Added:ID=2951;476;COEFCSENADDSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
Data Added:ID=2952;476;IsBerQuickTest;bool;input;false;-32768;32767;0;0;0;0;
Data Added:ID=2953;477;LOSA;double;output;-32768;-29;-16;1;0;0;1;
Data Added:ID=2954;477;LOSD;double;output;-32768;-29;-13;1;0;0;1;
Data Added:ID=2955;477;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
Data Added:ID=2956;477;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
Data Added:ID=2957;477;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
Data Added:ID=2958;477;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
Data Added:ID=2959;477;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
Data Added:ID=2960;477;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=2961;478;AP(DBM);double;output;-32768;0;3;1;0;0;1;
Data Added:ID=2962;478;JITTERPP(PS);double;output;-32768;-32768;32767;1;0;0;1;
Data Added:ID=2963;478;TXOMA(UW);double;output;-32768;-32768;32767;1;0;0;1;
Data Added:ID=2964;478;FALLTIME(PS);double;output;-32768;-32768;65;1;0;0;1;
Data Added:ID=2965;478;RISETIME(PS);double;output;-32768;-32768;100;1;0;0;1;
Data Added:ID=2966;478;JITTERRMS(PS);double;output;-32768;-32768;30;1;0;0;1;
Data Added:ID=2967;478;MASKMARGIN(%);double;output;-32768;10;90;1;0;0;1;
Data Added:ID=2968;478;ER(DB);double;output;-32768;3.5;6;1;0;0;1;
Data Added:ID=2969;478;ISOPTICALEYEORELECEYE;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=2970;478;CROSSING(%);double;output;-32768;40;60;1;0;0;1;
Data Added:ID=2971;479;DMITXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=2972;479;CURRENTTXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=2973;479;DMITXPOWERERR(DB);double;output;-32768;-2;2;1;0;0;1;
Data Added:ID=2974;480;DMITEMPERR(C);double;output;-32768;-3;3;1;0;0;1;
Data Added:ID=2975;480;DMITEMP(C);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=2976;481;ICC(MA);double;output;-32768;500;1050;1;0;0;1;
Data Added:ID=2977;482;DMIVCCERR(V);double;output;-32768;-0.165;0.165;1;0;0;1;
Data Added:ID=2978;482;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=2979;483;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
Data Added:ID=2980;484;ARRAYLISTRXINPUTPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
Data Added:ID=2981;484;DMIRXPWRMAXERRPOINT(DBM);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=2982;484;DMIRXPWRMAXERR;double;output;-32768;-3;3;1;0;0;1;
Data Added:ID=2983;485;CSENTARGETBER;double;input;1.0E-12;-32768;32767;0;0;0;0;
Data Added:ID=2984;485;COEFCSENSUBSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
Data Added:ID=2985;485;CSEN(DBM);double;output;-32768;-20;-9;1;0;0;1;
Data Added:ID=2986;485;SEARCHTARGETBERSUBSTEP;double;input;8;-32768;32767;0;0;0;0;
Data Added:ID=2987;485;SEARCHTARGETBERADDSTEP;double;input;8;-32768;32767;0;0;0;0;
Data Added:ID=2988;485;SEARCHTARGETBERLL;double;input;1E-8;-32768;32767;0;0;0;0;
Data Added:ID=2989;485;SEARCHTARGETBERUL;double;input;1.00E-7;-32768;32767;0;0;0;0;
Data Added:ID=2990;485;CSENSTARTINGRXPWR(DBM);double;input;-12;-32768;32767;0;0;0;0;
Data Added:ID=2991;485;CSENALIGNRXPWR(DBM);double;input;-7;-32768;32767;0;0;0;0;
Data Added:ID=2992;485;COEFCSENADDSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
Data Added:ID=2993;485;IsBerQuickTest;bool;input;false;-32768;32767;0;0;0;0;
Data Added:ID=2994;486;LOSA;double;output;-32768;-29;-16;1;0;0;1;
Data Added:ID=2995;486;LOSD;double;output;-32768;-29;-13;1;0;0;1;
Data Added:ID=2996;486;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
Data Added:ID=2997;486;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
Data Added:ID=2998;486;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
Data Added:ID=2999;486;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
Data Added:ID=3000;486;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
Data Added:ID=3001;486;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3002;487;AP(DBM);double;output;-32768;0;3;1;0;0;1;
Data Added:ID=3003;487;JITTERPP(PS);double;output;-32768;-32768;32767;1;0;0;1;
Data Added:ID=3004;487;TXOMA(UW);double;output;-32768;-32768;32767;1;0;0;1;
Data Added:ID=3005;487;FALLTIME(PS);double;output;-32768;-32768;65;1;0;0;1;
Data Added:ID=3006;487;RISETIME(PS);double;output;-32768;-32768;100;1;0;0;1;
Data Added:ID=3007;487;JITTERRMS(PS);double;output;-32768;-32768;30;1;0;0;1;
Data Added:ID=3008;487;MASKMARGIN(%);double;output;-32768;10;90;1;0;0;1;
Data Added:ID=3009;487;ER(DB);double;output;-32768;3.5;6;1;0;0;1;
Data Added:ID=3010;487;ISOPTICALEYEORELECEYE;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3011;487;CROSSING(%);double;output;-32768;40;60;1;0;0;1;
Data Added:ID=3012;488;DMITXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=3013;488;CURRENTTXPOWER(DBM);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=3014;488;DMITXPOWERERR(DB);double;output;-32768;-2;2;1;0;0;1;
Data Added:ID=3015;489;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
Data Added:ID=3016;490;DMITEMPERR(C);double;output;-32768;-3;3;1;0;0;1;
Data Added:ID=3017;490;DMITEMP(C);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=3018;491;DMIVCCERR(V);double;output;-32768;-0.165;0.165;1;0;0;1;
Data Added:ID=3019;491;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=3020;492;ICC(MA);double;output;-32768;100;2000;1;0;0;1;
Data Added:ID=3021;493;ARRAYLISTRXINPUTPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
Data Added:ID=3022;493;DMIRXPWRMAXERRPOINT(DBM);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=3023;493;DMIRXPWRMAXERR;double;output;-32768;-3;3;1;0;0;1;
Data Added:ID=3024;494;CSENTARGETBER;double;input;1.0E-12;-32768;32767;0;0;0;0;
Data Added:ID=3025;494;COEFCSENSUBSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
Data Added:ID=3026;494;CSEN(DBM);double;output;-32768;-20;-9;1;0;0;1;
Data Added:ID=3027;494;SEARCHTARGETBERSUBSTEP;double;input;8;-32768;32767;0;0;0;0;
Data Added:ID=3028;494;SEARCHTARGETBERADDSTEP;double;input;8;-32768;32767;0;0;0;0;
Data Added:ID=3029;494;SEARCHTARGETBERLL;double;input;1E-8;-32768;32767;0;0;0;0;
Data Added:ID=3030;494;SEARCHTARGETBERUL;double;input;1.00E-7;-32768;32767;0;0;0;0;
Data Added:ID=3031;494;CSENSTARTINGRXPWR(DBM);double;input;-12;-32768;32767;0;0;0;0;
Data Added:ID=3032;494;CSENALIGNRXPWR(DBM);double;input;-7;-32768;32767;0;0;0;0;
Data Added:ID=3033;494;COEFCSENADDSTEP(DBM);double;input;0.3;-32768;32767;0;0;0;0;
Data Added:ID=3034;494;IsBerQuickTest;bool;input;false;-32768;32767;0;0;0;0;
Data Added:ID=3035;495;LOSA;double;output;-32768;-29;-16;1;0;0;1;
Data Added:ID=3036;495;LOSD;double;output;-32768;-29;-13;1;0;0;1;
Data Added:ID=3037;495;LOSADSTEP;double;input;0.5;-32768;32767;0;0;0;0;
Data Added:ID=3038;495;LOSDMAX;double;input;-13;-32768;32767;0;0;0;0;
Data Added:ID=3039;495;LOSAMIN;double;input;-30;-32768;32767;0;0;0;0;
Data Added:ID=3040;495;LOSAMAX;double;input;-18;-32768;32767;0;0;0;0;
Data Added:ID=3041;495;LOSH;double;output;-32768;0.5;32767;1;0;0;1;
Data Added:ID=3042;495;ISLOSDETAIL;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3043;496;FIXEDMODDAC(MA);UInt16;input;150;-32768;32767;0;0;0;0;
Data Added:ID=3044;496;ARRAYLISTXDMICOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=3045;496;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
Data Added:ID=3046;496;IBIASADCORTXPOWERADC;byte;input;1;-32768;32767;0;0;0;0;
Data Added:ID=3047;496;ARRAYIBIAS(MA);ArrayList;input;180,280,380;-32768;32767;0;0;0;0;
Data Added:ID=3048;496;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3049;496;ISTEMPRELATIVE;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3050;496;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3051;496;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
Data Added:ID=3052;496;HighestCalTemp;double;input;60;-32768;32767;0;0;0;0;
Data Added:ID=3053;496;LowestCalTemp;double;input;20;-32768;32767;0;0;0;0;
Data Added:ID=3054;496;ISNEWALGORITHM;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3055;497;IMODMIN(MA);UInt16;input;100;-32768;32767;0;0;0;0;
Data Added:ID=3056;497;ARRAYLISTTXMODCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=3057;497;1STOR2STORPIDTXLOP;byte;input;0;-32768;32767;0;0;0;0;
Data Added:ID=3058;497;1STOR2STORPIDER;byte;input;2;-32768;32767;0;0;0;0;
Data Added:ID=3059;497;ISOPENLOOPORCLOSELOOPORBOTH;byte;input;1;-32768;32767;0;0;0;0;
Data Added:ID=3060;497;IMODSTART(MA);UInt16;input;250;-32768;32767;0;0;0;0;
Data Added:ID=3061;497;IMODSTEP;byte;input;64;-32768;32767;0;0;0;0;
Data Added:ID=3062;497;IMODMETHOD;byte;input;1;-32768;32767;0;0;0;0;
Data Added:ID=3063;497;TXERTOLERANCE(DB);double;input;0.2;-32768;32767;0;0;0;0;
Data Added:ID=3064;497;TXERTARGET(DB);double;input;4;3.5;6;1;0;1;0;
Data Added:ID=3065;497;ARRAYLISTTXPOWERCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=3066;497;IBIASMETHOD;byte;input;1;-32768;32767;0;0;0;0;
Data Added:ID=3067;497;IBIASSTEP(MA);byte;input;32;-32768;32767;0;0;0;0;
Data Added:ID=3068;497;IBIASSTART(MA);UInt16;input;40;-32768;32767;0;0;0;0;
Data Added:ID=3069;497;IBIASMIN(MA);UInt16;input;35;-32768;32767;0;0;0;0;
Data Added:ID=3070;497;IBIASMAX(MA);UInt16;input;45;-32768;32767;0;0;0;0;
Data Added:ID=3071;497;FIXEDMOD(MA);UInt16;input;500;-32768;32767;0;0;0;0;
Data Added:ID=3072;497;TXLOPTOLERANCE(UW);double;input;500;-32768;32767;0;0;0;0;
Data Added:ID=3073;497;TXLOPTARGET(UW);double;input;1500;1000;4000;1;0;1;0;
Data Added:ID=3074;497;IMODMAX(MA);UInt16;input;700;-32768;32767;0;0;0;0;
Data Added:ID=3075;497;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3076;497;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3077;497;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
Data Added:ID=3078;497;PIDCOEFARRAY;ArrayList;input;0.01,0.005,0;-32768;32767;0;0;0;0;
Data Added:ID=3079;497;FIXEDIBIAS(MA);UInt32;input;280;-32768;32767;0;0;0;0;
Data Added:ID=3080;497;FixedIBiasArray;ArrayList;input;520,520,520,520;-32768;32767;0;0;0;0;
Data Added:ID=3081;497;FixedModArray;ArrayList;input;0,0,0,0;-32768;32767;0;0;0;0;
Data Added:ID=3082;498;1STOR2STORPID;byte;input;1;0;0;0;0;0;0;
Data Added:ID=3083;498;ARRAYLISTDMITEMPCOEF;ArrayList;output;-32768;0;0;0;1;0;0;
Data Added:ID=3084;499;GENERALVCC(V);double;input;3.3;-32768;32767;0;0;0;0;
Data Added:ID=3085;499;ARRAYLISTVCC(V);ArrayList;input;3.1,3.3,3.5;-32768;32767;0;0;0;0;
Data Added:ID=3086;499;ARRAYLISTDMIVCCCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=3087;499;1STOR2STORPID;byte;input;1;-32768;32767;0;0;0;0;
Data Added:ID=3088;500;ARRAYLISTDMIRXCOEF;ArrayList;output;;-32768;32767;0;1;0;0;
Data Added:ID=3089;500;ARRAYLISTRXPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
Data Added:ID=3090;500;HasOffset;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3091;500;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
Data Added:ID=3092;501;LOSDVOLTAGETUNESTEP(V);byte;input;2;-32768;32767;0;0;0;0;
Data Added:ID=3093;501;LOSAVOLTAGESTARTVALUE(V);UInt16;input;14;-32768;32767;0;0;0;0;
Data Added:ID=3094;501;IsAdjustLos;bool;input;false;-32768;32767;0;0;0;0;
Data Added:ID=3095;501;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3096;501;LOSDVOLTAGEUPERLIMIT(V);UInt16;input;30;-32768;32767;0;0;0;0;
Data Added:ID=3097;501;LOSDVOLTAGESTARTVALUE(V);UInt16;input;14;-32768;32767;0;0;0;0;
Data Added:ID=3098;501;LOSDINPUTPOWER;double;input;-21;-32768;32767;0;0;0;0;
Data Added:ID=3099;501;LOSAVOLTAGETUNESTEP(V);byte;input;2;-32768;32767;0;0;0;0;
Data Added:ID=3100;501;LOSAVOLTAGEUPERLIMIT(V);UInt16;input;30;-32768;32767;0;0;0;0;
Data Added:ID=3101;501;LosValue(V);UINT32;input;4;-32768;32767;0;0;0;0;
Data Added:ID=3102;501;LOSDVOLTAGELOWLIMIT(V);UInt16;input;1;-32768;32767;0;0;0;0;
Data Added:ID=3103;501;LOSAINPUTPOWER;double;input;-21;-32768;32767;0;0;0;0;
Data Added:ID=3104;501;LOSAVOLTAGELOWLIMIT(V);UInt16;input;1;-32768;32767;0;0;0;0;
Data Added:ID=3105;502;FIXEDMODDAC(MA);UInt16;input;150;-32768;32767;0;0;0;0;
Data Added:ID=3106;502;ARRAYLISTXDMICOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=3107;502;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
Data Added:ID=3108;502;IBIASADCORTXPOWERADC;byte;input;1;-32768;32767;0;0;0;0;
Data Added:ID=3109;502;ARRAYIBIAS(MA);ArrayList;input;180,280,380;-32768;32767;0;0;0;0;
Data Added:ID=3110;502;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3111;502;ISTEMPRELATIVE;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3112;502;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3113;502;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
Data Added:ID=3114;502;HighestCalTemp;double;input;60;-32768;32767;0;0;0;0;
Data Added:ID=3115;502;LowestCalTemp;double;input;20;-32768;32767;0;0;0;0;
Data Added:ID=3116;502;ISNEWALGORITHM;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3117;503;IMODMIN(MA);UInt16;input;100;-32768;32767;0;0;0;0;
Data Added:ID=3118;503;ARRAYLISTTXMODCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=3119;503;1STOR2STORPIDTXLOP;byte;input;0;-32768;32767;0;0;0;0;
Data Added:ID=3120;503;1STOR2STORPIDER;byte;input;2;-32768;32767;0;0;0;0;
Data Added:ID=3121;503;ISOPENLOOPORCLOSELOOPORBOTH;byte;input;1;-32768;32767;0;0;0;0;
Data Added:ID=3122;503;IMODSTART(MA);UInt16;input;200;-32768;32767;0;0;0;0;
Data Added:ID=3123;503;IMODSTEP;byte;input;64;-32768;32767;0;0;0;0;
Data Added:ID=3124;503;IMODMETHOD;byte;input;1;-32768;32767;0;0;0;0;
Data Added:ID=3125;503;TXERTOLERANCE(DB);double;input;0.2;-32768;32767;0;0;0;0;
Data Added:ID=3126;503;TXERTARGET(DB);double;input;4;3.5;6;1;0;1;0;
Data Added:ID=3127;503;ARRAYLISTTXPOWERCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=3128;503;IBIASMETHOD;byte;input;1;-32768;32767;0;0;0;0;
Data Added:ID=3129;503;IBIASSTEP(MA);byte;input;32;-32768;32767;0;0;0;0;
Data Added:ID=3130;503;IBIASSTART(MA);UInt16;input;40;-32768;32767;0;0;0;0;
Data Added:ID=3131;503;IBIASMIN(MA);UInt16;input;35;-32768;32767;0;0;0;0;
Data Added:ID=3132;503;IBIASMAX(MA);UInt16;input;45;-32768;32767;0;0;0;0;
Data Added:ID=3133;503;FIXEDMOD(MA);UInt16;input;500;-32768;32767;0;0;0;0;
Data Added:ID=3134;503;TXLOPTOLERANCE(UW);double;input;500;-32768;32767;0;0;0;0;
Data Added:ID=3135;503;TXLOPTARGET(UW);double;input;1500;1000;4000;1;0;1;0;
Data Added:ID=3136;503;IMODMAX(MA);UInt16;input;1000;-32768;32767;0;0;0;0;
Data Added:ID=3137;503;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3138;503;DCtoDC;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3139;503;FIXEDCrossDac;UInt32;input;200;-32768;32767;0;0;0;0;
Data Added:ID=3140;503;PIDCOEFARRAY;ArrayList;input;0.01,0.005,0;-32768;32767;0;0;0;0;
Data Added:ID=3141;503;FIXEDIBIAS(MA);UInt32;input;280;-32768;32767;0;0;0;0;
Data Added:ID=3142;503;FixedIBiasArray;ArrayList;input;520,520,520,520;-32768;32767;0;0;0;0;
Data Added:ID=3143;503;FixedModArray;ArrayList;input;0,0,0,0;-32768;32767;0;0;0;0;
Data Added:ID=3144;504;1STOR2STORPID;byte;input;1;0;0;0;0;0;0;
Data Added:ID=3145;504;ARRAYLISTDMITEMPCOEF;ArrayList;output;-32768;0;0;0;1;0;0;
Data Added:ID=3146;505;GENERALVCC(V);double;input;3.3;-32768;32767;0;0;0;0;
Data Added:ID=3147;505;ARRAYLISTVCC(V);ArrayList;input;3.1,3.3,3.5;-32768;32767;0;0;0;0;
Data Added:ID=3148;505;ARRAYLISTDMIVCCCOEF;ArrayList;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=3149;505;1STOR2STORPID;byte;input;1;-32768;32767;0;0;0;0;
**表[TopoEquipment]修改如下**
Data Added:ID=175;26;1;Powersupply;E3631;0;
Data Added:ID=176;26;2;Scope;FLEX86100;0;
Data Added:ID=177;26;3;PPG;MP1800PPG;0;
Data Added:ID=178;26;4;Thermocontroller;TPO4300;0;
Data Added:ID=179;26;5;OpticalSwitch;AQ2211OpticalSwitch;1;
Data Added:ID=180;26;6;Attennuator;AQ2211Atten;0;
Data Added:ID=181;26;7;ErrorDetector;MP1800ED;0;
Data Added:ID=182;26;8;OpticalSwitch;AQ2211OpticalSwitch;2;
**表[TopoEquipmentParameter]修改如下**
Data Added:ID=2493;175;Addr;5;
Data Added:ID=2494;175;IOType;GPIB;
Data Added:ID=2495;175;Reset;false;
Data Added:ID=2496;175;Name;E3631;
Data Added:ID=2497;175;DutChannel;1;
Data Added:ID=2498;175;OptSourceChannel;2;
Data Added:ID=2499;175;DutVoltage;3.5;
Data Added:ID=2500;175;DutCurrent;2.5;
Data Added:ID=2501;175;OptVoltage;3.5;
Data Added:ID=2502;175;OptCurrent;1.5;
Data Added:ID=2503;175;voltageoffset;0.2;
Data Added:ID=2504;175;currentoffset;0;
Data Added:ID=2505;175;opendelay;2000;
Data Added:ID=2506;175;closedelay;500;
Data Added:ID=2507;176;Addr;7;
Data Added:ID=2508;176;IOType;GPIB;
Data Added:ID=2509;176;Reset;false;
Data Added:ID=2510;176;Name;FLEX86100;
Data Added:ID=2511;176;configFilePath;1;
Data Added:ID=2512;176;FlexDcaDataRate;25.78125E+9;
Data Added:ID=2513;176;FilterSwitch;1;
Data Added:ID=2514;176;FlexFilterFreq;25.78125;
Data Added:ID=2515;176;triggerSource;0;
Data Added:ID=2516;176;FlexTriggerBwlimit;2;
Data Added:ID=2517;176;opticalSlot;3;
Data Added:ID=2518;176;elecSlot;4;
Data Added:ID=2519;176;FlexOptChannel;1;
Data Added:ID=2520;176;FlexElecChannel;1;
Data Added:ID=2521;176;FlexDcaWavelength;1;
Data Added:ID=2522;176;opticalAttSwitch;1;
Data Added:ID=2523;176;FlexDcaAtt;0;
Data Added:ID=2524;176;erFactor;0;
Data Added:ID=2525;176;FlexScale;300;
Data Added:ID=2526;176;FlexOffset;300;
Data Added:ID=2527;176;Threshold;0;
Data Added:ID=2528;176;reference;0;
Data Added:ID=2529;176;precisionTimebaseModuleSlot;1;
Data Added:ID=2530;176;precisionTimebaseSynchMethod;1;
Data Added:ID=2531;176;precisionTimebaseRefClk;6.445e+9;
Data Added:ID=2532;176;rapidEyeSwitch;1;
Data Added:ID=2533;176;marginType;1;
Data Added:ID=2534;176;marginHitType;0;
Data Added:ID=2535;176;marginHitRatio;5e-006;
Data Added:ID=2536;176;marginHitCount;0;
Data Added:ID=2537;176;acqLimitType;0;
Data Added:ID=2538;176;acqLimitNumber;1000;
Data Added:ID=2539;176;opticalMaskName;c:\scope\masks\25.78125_100GBASE-LR4_Tx_Optical_D31.MSK;
Data Added:ID=2540;176;elecMaskName;c:\Eye;
Data Added:ID=2541;176;opticalEyeSavePath;D:\Eye\;
Data Added:ID=2542;176;elecEyeSavePath;D:\Eye\;
Data Added:ID=2543;176;ERFACTORSWITCH;1;
Data Added:ID=2544;176;flexsetscaledelay;1000;
Data Added:ID=2545;177;Addr;1;
Data Added:ID=2546;177;IOType;GPIB;
Data Added:ID=2547;177;Reset;false;
Data Added:ID=2548;177;Name;MP1800PPG;
Data Added:ID=2549;177;dataRate;25.78128;
Data Added:ID=2550;177;dataLevelGuardAmpMax;1;
Data Added:ID=2551;177;dataLevelGuardOffsetMax;0;
Data Added:ID=2552;177;dataLevelGuardOffsetMin;0;
Data Added:ID=2553;177;dataLevelGuardSwitch;0;
Data Added:ID=2554;177;dataAmplitude;0.5;
Data Added:ID=2555;177;dataCrossPoint;50;
Data Added:ID=2556;177;configFilePath;0;
Data Added:ID=2557;177;slot;1;
Data Added:ID=2558;177;clockSource;0;
Data Added:ID=2559;177;auxOutputClkDiv;0;
Data Added:ID=2560;177;prbsLength;31;
Data Added:ID=2561;177;patternType;0;
Data Added:ID=2562;177;dataSwitch;1;
Data Added:ID=2563;177;dataTrackingSwitch;1;
Data Added:ID=2564;177;dataAcModeSwitch;0;
Data Added:ID=2565;177;dataLevelMode;0;
Data Added:ID=2566;177;clockSwitch;1;
Data Added:ID=2567;177;outputSwitch;1;
Data Added:ID=2568;177;TotalChannel;4;
Data Added:ID=2569;178;Addr;23;
Data Added:ID=2570;178;IOType;GPIB;
Data Added:ID=2571;178;Reset;False;
Data Added:ID=2572;178;Name;TPO4300;
Data Added:ID=2573;178;FLSE;14;
Data Added:ID=2574;178;ULIM;90;
Data Added:ID=2575;178;LLIM;-20;
Data Added:ID=2576;178;Sensor;1;
Data Added:ID=2577;179;Addr;20;
Data Added:ID=2578;179;IOType;GPIB;
Data Added:ID=2579;179;Reset;false;
Data Added:ID=2580;179;Name;AQ2011OpticalSwitch;
Data Added:ID=2581;179;OpticalSwitchSlot;1;
Data Added:ID=2582;179;SwitchChannel;1;
Data Added:ID=2583;179;ToChannel;1;
Data Added:ID=2584;180;Addr;20;
Data Added:ID=2585;180;IOType;GPIB;
Data Added:ID=2586;180;Reset;false;
Data Added:ID=2587;180;Name;AQ2211Atten;
Data Added:ID=2588;180;TOTALCHANNEL;4;
Data Added:ID=2589;180;AttValue;20;
Data Added:ID=2590;180;AttSlot;2;
Data Added:ID=2591;180;WAVELENGTH;1310,1310,1310,1310;
Data Added:ID=2592;180;AttChannel;1;
Data Added:ID=2593;180;opendelay;1000;
Data Added:ID=2594;180;closedelay;1000;
Data Added:ID=2595;180;setattdelay;1000;
Data Added:ID=2596;181;Addr;1;
Data Added:ID=2597;181;IOType;GPIB;
Data Added:ID=2598;181;Reset;false;
Data Added:ID=2599;181;Name;MP1800ED;
Data Added:ID=2600;181;slot;3;
Data Added:ID=2601;181;TotalChannel;4;
Data Added:ID=2602;181;currentChannel;1;
Data Added:ID=2603;181;dataInputInterface;2;
Data Added:ID=2604;181;prbsLength;31;
Data Added:ID=2605;181;errorResultZoom;1;
Data Added:ID=2606;181;edGatingMode;1;
Data Added:ID=2607;181;edGatingUnit;0;
Data Added:ID=2608;181;edGatingTime;5;
Data Added:ID=2609;182;Addr;20;
Data Added:ID=2610;182;IOType;GPIB;
Data Added:ID=2611;182;Reset;false;
Data Added:ID=2612;182;Name;AQ2011OpticalSwitch;
Data Added:ID=2613;182;OpticalSwitchSlot;3;
Data Added:ID=2614;182;SwitchChannel;1;
Data Added:ID=2615;182;ToChannel;1;
==User:terry.yin于2014/11/11 14:38:27修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
OriginalData:ID=97,SEQ:6;
NewData:ID=97,SEQ:5;
OriginalData:ID=98,SEQ:5;
NewData:ID=98,SEQ:6;
OriginalData:ID=99,SEQ:3;
NewData:ID=99,SEQ:2;
OriginalData:ID=100,SEQ:1;
NewData:ID=100,SEQ:3;
OriginalData:ID=101,SEQ:2;
NewData:ID=101,SEQ:1;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/11 14:38:45修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
OriginalData:ID=98,IgnoreFlag:False;
NewData:ID=98,IgnoreFlag:True;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/11 14:44:44修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
OriginalData:ID=500,IgnoreFlag:False;
NewData:ID=500,IgnoreFlag:True;
OriginalData:ID=501,IgnoreFlag:False;
NewData:ID=501,IgnoreFlag:True;
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/11 14:47:51修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
Data Added:ID=506;101;CalRxDminoProcessingCoef;5;18;;False;
**表[TopoTestParameter]修改如下**
Data Added:ID=3150;506;ARRAYLISTDMIRXCOEF;ArrayList;output;;-32768;32767;0;1;0;0;
Data Added:ID=3151;506;1STOR2STORPID;byte;input;2;-32768;32767;0;0;0;0;
Data Added:ID=3152;506;ARRAYLISTRXPOWER(DBM);ArrayList;input;-6,-7,-8,-10;-32768;32767;0;0;0;0;
Data Added:ID=3153;506;HasOffset;bool;input;true;-32768;32767;0;0;0;0;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/11 14:49:52修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
Data Added:ID=507;101;AdjustLos;6;6;;False;
**表[TopoTestParameter]修改如下**
Data Added:ID=3154;507;LOSDVOLTAGETUNESTEP(V);byte;input;2;-32768;32767;0;0;0;0;
Data Added:ID=3155;507;LOSAVOLTAGESTARTVALUE(V);UInt16;input;14;-32768;32767;0;0;0;0;
Data Added:ID=3156;507;IsAdjustLos;bool;input;false;-32768;32767;0;0;0;0;
Data Added:ID=3157;507;AUTOTUNE;bool;input;true;-32768;32767;0;0;0;0;
Data Added:ID=3158;507;LOSDVOLTAGEUPERLIMIT(V);UInt16;input;30;-32768;32767;0;0;0;0;
Data Added:ID=3159;507;LOSDVOLTAGESTARTVALUE(V);UInt16;input;14;-32768;32767;0;0;0;0;
Data Added:ID=3160;507;LOSDINPUTPOWER;double;input;-21;-32768;32767;0;0;0;0;
Data Added:ID=3161;507;LOSAVOLTAGETUNESTEP(V);byte;input;2;-32768;32767;0;0;0;0;
Data Added:ID=3162;507;LOSAVOLTAGEUPERLIMIT(V);UInt16;input;30;-32768;32767;0;0;0;0;
Data Added:ID=3163;507;LosValue(V);UINT32;input;4;-32768;32767;0;0;0;0;
Data Added:ID=3164;507;LOSDVOLTAGELOWLIMIT(V);UInt16;input;1;-32768;32767;0;0;0;0;
Data Added:ID=3165;507;LOSAINPUTPOWER;double;input;-21;-32768;32767;0;0;0;0;
Data Added:ID=3166;507;LOSAVOLTAGELOWLIMIT(V);UInt16;input;1;-32768;32767;0;0;0;0;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (207, N'Terry.Yin', CAST(0x0000A3E000B94420 AS DateTime), CAST(0x0000A3E000B97558 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (208, N'Terry.Yin', CAST(0x0000A3E000B97FE4 AS DateTime), CAST(0x0000A3E000B9D1EC AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (209, N'Terry.Yin', CAST(0x0000A3E000BA67EC AS DateTime), CAST(0x0000A3E000BAA284 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (210, N'Terry.Yin', CAST(0x0000A3E000BAE424 AS DateTime), CAST(0x0000A3E000BAEFDC AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (211, N'Terry.Yin', CAST(0x0000A3E000BB1EBC AS DateTime), CAST(0x0000A3E000BB281C AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (212, N'Terry.Yin', CAST(0x0000A3E000BB3884 AS DateTime), CAST(0x0000A3E000BB4C70 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (213, N'Terry.Yin', CAST(0x0000A3E000BBF65C AS DateTime), CAST(0x0000A3E000BC0340 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (214, N'Terry.Yin', CAST(0x0000A3E000BC0DCC AS DateTime), CAST(0x0000A3E000BC35A4 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (215, N'Terry.Yin', CAST(0x0000A3E000BCD9B4 AS DateTime), CAST(0x0000A3E000BCF958 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (216, N'Terry.Yin', CAST(0x0000A3E000CB2320 AS DateTime), CAST(0x0000A3E000CB3130 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (217, N'Terry.Yin', CAST(0x0000A3E000CB8590 AS DateTime), CAST(0x0000A3E000CB8C98 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (218, N'Terry.Yin', CAST(0x0000A3E000CBF610 AS DateTime), CAST(0x0000A3E000CBFAC0 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (219, N'Terry.Yin', CAST(0x0000A3E000CC4494 AS DateTime), CAST(0x0000A3E000CC4F20 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (220, N'Terry.Yin', CAST(0x0000A3E000CD8804 AS DateTime), CAST(0x0000A3E000DC6644 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (221, N'terry.yin', CAST(0x0000A3E000E4D3B0 AS DateTime), CAST(0x0000A3E00118EBA0 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'==User :terry.yin Operation Logs at 2014/11/11 14:01:11 ==
**[TopoTestParameter]**
OriginalData:ID=2773,ItemValue:300;
New     Data:ID=2773,ItemValue:150;
OriginalData:ID=2777,ItemValue:300,400,500;
New     Data:ID=2777,ItemValue:180,280,380;
OriginalData:ID=2835,ItemValue:300;
New     Data:ID=2835,ItemValue:150;
OriginalData:ID=2839,ItemValue:300,400,500;
New     Data:ID=2839,ItemValue:180,280,380;
==User :terry.yin Operation Logs at 2014/11/11 14:48:50 ==
**[TopoTestParameter]**
OriginalData:ID=2773,ItemValue:150;
New     Data:ID=2773,ItemValue:0;
OriginalData:ID=2777,ItemValue:180,280,380;
New     Data:ID=2777,ItemValue:350,400,450;
OriginalData:ID=2835,ItemValue:150;
New     Data:ID=2835,ItemValue:0;
OriginalData:ID=2839,ItemValue:180,280,380;
New     Data:ID=2839,ItemValue:300,350,400;
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (222, N'Terry.Yin', CAST(0x0000A3E000E82420 AS DateTime), CAST(0x0000A3E000E829FC AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (223, N'Terry.Yin', CAST(0x0000A3E000ECA090 AS DateTime), CAST(0x0000A3E000ECAD74 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (224, N'Terry.Yin', CAST(0x0000A3E000ECC28C AS DateTime), CAST(0x0000A3E000ECC4E4 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (225, N'Terry.Yin', CAST(0x0000A3E000ECE230 AS DateTime), CAST(0x0000A3E000ECE6E0 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (226, N'Terry.Yin', CAST(0x0000A3E000EDA38C AS DateTime), CAST(0x0000A3E000EDA968 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (227, N'Terry.Yin', CAST(0x0000A3E000EE5DE0 AS DateTime), CAST(0x0000A3E000EE6740 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (228, N'Terry.Yin', CAST(0x0000A3E000EEEBAC AS DateTime), CAST(0x0000A3E000EEF3E0 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (229, N'Terry.Yin', CAST(0x0000A3E000EFA984 AS DateTime), CAST(0x0000A3E000EFC34C AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (230, N'Terry.Yin', CAST(0x0000A3E000F02814 AS DateTime), CAST(0x0000A3E000F02B98 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (231, N'Terry.Yin', CAST(0x0000A3E000F05F28 AS DateTime), CAST(0x0000A3E000F06504 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (232, N'Terry.Yin', CAST(0x0000A3E000F08A84 AS DateTime), CAST(0x0000A3E000F0A578 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (233, N'Terry.Yin', CAST(0x0000A3E000F432D8 AS DateTime), CAST(0x0000A3E000F446C4 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (234, N'Terry.Yin', CAST(0x0000A3E000F44EF8 AS DateTime), CAST(0x0000A3E000F45D08 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (235, N'Terry.Yin', CAST(0x0000A3E000F46E9C AS DateTime), CAST(0x0000A3E000F475A4 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (236, N'Terry.Yin', CAST(0x0000A3E000F51504 AS DateTime), CAST(0x0000A3E000F51F90 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (237, N'Terry.Yin', CAST(0x0000A3E000F528F0 AS DateTime), CAST(0x0000A3E000F52FF8 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (238, N'Terry.Yin', CAST(0x0000A3E000F551F4 AS DateTime), CAST(0x0000A3E000F5544C AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (239, N'Terry.Yin', CAST(0x0000A3E000F5A780 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登入', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (240, N'Terry.Yin', CAST(0x0000A3E000F667B0 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登入', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (241, N'Terry.Yin', CAST(0x0000A3E000F8CEEC AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'Maintain', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登入', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (242, N'Terry.Yin', CAST(0x0000A3E00106AC88 AS DateTime), CAST(0x0000A3E00106B96C AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (243, N'Terry.Yin', CAST(0x0000A3E0010F6878 AS DateTime), CAST(0x0000A3E001153410 AS DateTime), N'EEPROM', N'用户Terry.Yin已经在电脑INPCSZ0524[10.160.80.56]登出', N'用户Terry.Yin尚未修改资料...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (244, N'terry.yin', CAST(0x0000A3E001137210 AS DateTime), CAST(0x0000A3E001294C98 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0228[10.160.80.46]登出', N'==User:terry.yin于2014/11/11 16:43:27修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
OriginalData:ID=2601,Item:TotalChannel;
NewData:ID=2601,Item:totalChannel;
Data Added:ID=2616;177;patternfile;@"C:\02";
Data Added:ID=2617;181;patternfile;@"C:\02";
==User:terry.yin于2014/11/11 16:56:00修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/11 16:56:30修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
OriginalData:ID=89,IgnoreFlag:False;
NewData:ID=89,IgnoreFlag:True;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
==User:terry.yin于2014/11/11 16:57:24修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
OriginalData:ID=2418,Item:TotalChannel;
NewData:ID=2418,Item:totalChannel;
Data Added:ID=2618;165;patternfile;@"C:\02";
Data Added:ID=2619;169;patternfile;@"C:\02";
==User:terry.yin于2014/11/11 17:44:24修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
**表[TopoTestModel]修改如下**
Data Added:ID=508;95;TestIBiasDmi;1;5;;False;
Data Added:ID=509;95;TestIcc;2;15;;False;
Data Added:ID=510;95;TestVccDmi;3;14;;False;
Data Added:ID=511;96;TestIcc;1;15;;False;
Data Added:ID=512;96;TestVccDmi;2;14;;False;
Data Added:ID=513;96;TestIBiasDmi;3;5;;False;
**表[TopoTestParameter]修改如下**
Data Added:ID=3167;508;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
Data Added:ID=3168;509;ICC(MA);double;output;-32768;100;2000;1;0;0;1;
Data Added:ID=3169;510;DMIVCCERR(V);double;output;-32768;-0.200000002980232;0.200000002980232;1;0;0;1;
Data Added:ID=3170;510;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=3171;511;ICC(MA);double;output;-32768;100;2000;1;0;0;1;
Data Added:ID=3172;512;DMIVCCERR(V);double;output;-32768;-0.200000002980232;0.200000002980232;1;0;0;1;
Data Added:ID=3173;512;DMIVCC(V);double;output;-32768;-32768;32767;0;1;0;0;
Data Added:ID=3174;513;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (245, N'terry.yin', CAST(0x0000A3E00120609C AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'Login Name =terry.yin login successfully at computer=INPCSZ0072[10.160.80.42];', N'Login Name =terry.yinbeen modified Nothing...')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (246, N'terry.yin', CAST(0x0000A3E00120942C AS DateTime), CAST(0x0000A3E00121BF00 AS DateTime), N'MaintainATSPlan', N'User: terry.yin已经在电脑INPCSZ0072[10.160.80.42]登出', N'==User :terry.yin Operation Logs at 2014/11/11 17:32:12 ==
**[TopoTestParameter]**
OriginalData:ID=2681,ItemValue:0.3;
New     Data:ID=2681,ItemValue:0.4;
OriginalData:ID=2722,ItemValue:0.3;
New     Data:ID=2722,ItemValue:0.4;
OriginalData:ID=2763,ItemValue:0.3;
New     Data:ID=2763,ItemValue:0.4;
==User :terry.yin Operation Logs at 2014/11/11 17:33:48 ==
**[TopoTestParameter]**
')
INSERT [dbo].[UserLoginInfo] ([ID], [UserName], [LoginOntime], [LoginOffTime], [Apptype], [LoginInfo], [OPLogs]) VALUES (247, N'terry.yin', CAST(0x0000A3E0012CE934 AS DateTime), CAST(0x00008EAC00C5C100 AS DateTime), N'MaintainATSPlan', N'Login Name =terry.yin login successfully at computer=INPCSZ0228[10.160.80.46];', N'==User:terry.yin于2014/11/11 18:18:30修改==
**表[TopoTestPlan]修改如下**
**表[TopoTestControl]修改如下**
OriginalData:ID=88,AuxAttribles:TempSleep=60;
NewData:ID=88,AuxAttribles:TempSleep=100;
OriginalData:ID=90,AuxAttribles:TempSleep=5;
NewData:ID=90,AuxAttribles:TempSleep=80;
OriginalData:ID=91,IgnoreFlag:False;
NewData:ID=91,IgnoreFlag:True;
OriginalData:ID=92,IgnoreFlag:False;
NewData:ID=92,IgnoreFlag:True;
**表[TopoTestModel]修改如下**
**表[TopoTestParameter]修改如下**
**表[TopoEquipment]修改如下**
**表[TopoEquipmentParameter]修改如下**
')
SET IDENTITY_INSERT [dbo].[UserLoginInfo] OFF
/****** Object:  Table [dbo].[UserInfo]    Script Date: 11/12/2014 09:06:50 ******/
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
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginONTime], [lastComputerName], [lastLoginOffTime], [lastIP], [Remarks]) VALUES (2, N'terry.yin', N'terry', N'ysz', CAST(0x0000A3E0012CE808 AS DateTime), N'INPCSZ0228', CAST(0x0000A3E001294A40 AS DateTime), N'10.160.80.46', N'测试1')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginONTime], [lastComputerName], [lastLoginOffTime], [lastIP], [Remarks]) VALUES (5, N'Leo', N'leo.chen', N'陈江鹏', CAST(0x0000A3D400DCFB5B AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'测试0')
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
/****** Object:  Table [dbo].[GlobalAllEquipmentList]    Script Date: 11/12/2014 09:06:50 ******/
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
SET IDENTITY_INSERT [dbo].[GlobalAllEquipmentList] OFF
/****** Object:  Table [dbo].[GlobalAllAppModelList]    Script Date: 11/12/2014 09:06:50 ******/
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
SET IDENTITY_INSERT [dbo].[GlobalAllAppModelList] OFF
/****** Object:  StoredProcedure [dbo].[GetCurrServerTime]    Script Date: 11/12/2014 09:06:48 ******/
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
/****** Object:  Table [dbo].[FunctionTable]    Script Date: 11/12/2014 09:06:50 ******/
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
/****** Object:  Table [dbo].[GlobalMSA]    Script Date: 11/12/2014 09:06:50 ******/
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
/****** Object:  Table [dbo].[GlobalManufactureCoefficientsGroup]    Script Date: 11/12/2014 09:06:50 ******/
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
SET IDENTITY_INSERT [dbo].[GlobalManufactureCoefficientsGroup] OFF
/****** Object:  Table [dbo].[GlobalManufactureCoefficients]    Script Date: 11/12/2014 09:06:50 ******/
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
SET IDENTITY_INSERT [dbo].[GlobalManufactureCoefficients] OFF
/****** Object:  Table [dbo].[GlobalProductionType]    Script Date: 11/12/2014 09:06:50 ******/
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
/****** Object:  Table [dbo].[UserRoleTable]    Script Date: 11/12/2014 09:06:50 ******/
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
/****** Object:  Table [dbo].[GlobalMSADefintionInf]    Script Date: 11/12/2014 09:06:50 ******/
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
SET IDENTITY_INSERT [dbo].[GlobalMSADefintionInf] OFF
/****** Object:  Table [dbo].[GlobalAllTestModelList]    Script Date: 11/12/2014 09:06:50 ******/
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
SET IDENTITY_INSERT [dbo].[GlobalAllTestModelList] OFF
/****** Object:  Table [dbo].[GlobalAllEquipmentParamterList]    Script Date: 11/12/2014 09:06:50 ******/
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
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (17, 2, N'AttSlot', N'1', N'插槽数', N'byte')
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
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (98, 10, N'FlexOptChannel', N'1', N'光口通道（1=A,2=B,3=C,4=D）', N'byte')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [Item], [ItemValue], [ItemDescription], [ItemType]) VALUES (99, 10, N'FlexElecChannel', N'1', N'电口通道（1=A,2=B,3=C,4=D）', N'byte')
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
', N'Scope')
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
SET IDENTITY_INSERT [dbo].[GlobalAllEquipmentParamterList] OFF
/****** Object:  Table [dbo].[RoleFunctionTable]    Script Date: 11/12/2014 09:06:50 ******/
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
SET IDENTITY_INSERT [dbo].[RoleFunctionTable] OFF
/****** Object:  Table [dbo].[OperationLogs]    Script Date: 11/12/2014 09:06:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationLogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[Optype] [nvarchar](100) NOT NULL,
	[DetailLogs] [ntext] NULL,
 CONSTRAINT [PK_OperationLogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[OperationLogs] ON
INSERT [dbo].[OperationLogs] ([ID], [PID], [Optype], [DetailLogs]) VALUES (1, 201, N'Modified [PN=TR-QQ13L-N00;TestPlan =TerryTest]', N'================**2014/11/10 16:22:54**================
<**********************PN=TR-QQ13L-N00:TestPlan =TerryTest**********************>
**[TopoEquipment]**:  
<==E3631==>
Modified--> <ItemName=E3631>OriginalData:[SEQ]=3;
Modified--> <ItemName=E3631>ModifiedData:[SEQ]=2;

**[TopoEquipment]**:  
<==D86100==>
Modified--> <ItemName=D86100>OriginalData:[SEQ]=1;
Modified--> <ItemName=D86100>ModifiedData:[SEQ]=3;

**[TopoEquipment]**:  
<==AQ2211Atten==>
Modified--> <ItemName=AQ2211Atten>OriginalData:[SEQ]=2;
Modified--> <ItemName=AQ2211Atten>ModifiedData:[SEQ]=1;

***[TopoEquipmentParameter]***:
<==AQ2211Atten==>
Modified--> <Item=Addr>OriginalData:[ItemValue]=6;
Modified--> <Item=Addr>ModifiedData:[ItemValue]=7;

**[TopoTestControl]**:
<==FTM00==>
Modified--> <ItemName=FTM00>OriginalData:[IgnoreFlag]=False;
Modified--> <ItemName=FTM00>ModifiedData:[IgnoreFlag]=True;

***[TopoTestModel]***:
<==TestIBiasDmi==>
Deleted--> <Item=TestIBiasDmi>465;93;TestIBiasDmi;1;5;;False;
****[TopoTestParameter]****:
<==TestIBiasDmi==>
Deleted--> <Item=IBIAS(MA)>2891;465;IBIAS(MA);double;output;-32768;10;100;1;0;0;1;
****[TopoTestParameter]****:
<==AdjustTxPowerDmi==>
Modified--> <ItemName=FIXEDMODDAC(MA)>OriginalData:[ItemValue]=512;
Modified--> <ItemName=FIXEDMODDAC(MA)>ModifiedData:[ItemValue]=256;


')
SET IDENTITY_INSERT [dbo].[OperationLogs] OFF
/****** Object:  StoredProcedure [dbo].[InsertRunRecord]    Script Date: 11/12/2014 09:06:48 ******/
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
--@EndTime	IN	Datetime	测试结束时间	--140721
--@FWRev	IN	Nvarchar(5)	FW版本号
CREATE PROCEDURE [dbo].[InsertRunRecord]
 @ID int OUTPUT,
 @SN nvarchar(30),  
 @PID int,
 @StartTime datetime,
 @EndTime datetime, --140721
 @FWRev nvarchar(5) --141027
AS
 
BEGIN
   
SET XACT_ABORT ON 
BEGIN TRANSACTION 
INSERT INTO  TopoRunRecordTable VALUES(@SN, @PID,@StartTime,@EndTime,@FWRev);
set @ID = (Select Ident_Current('TopoRunRecordTable'))
COMMIT TRANSACTION 
SET XACT_ABORT OFF 
print @id
return @ID

END
GO
/****** Object:  StoredProcedure [dbo].[InsertLogRecord]    Script Date: 11/12/2014 09:06:48 ******/
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
/****** Object:  Table [dbo].[GlobalTestModelParamterList]    Script Date: 11/12/2014 09:06:50 ******/
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
 CONSTRAINT [PK_GlobalTestModelParamterList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalTestModelParamterList] ON
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (1, 1, N'IMODMIN(MA)', N'UInt16', N'input', N'100', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (2, 1, N'ARRAYLISTTXMODCOEF', N'ArrayList', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (3, 1, N'1STOR2STORPIDTXLOP', N'byte', N'input', N'2', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (4, 1, N'1STOR2STORPIDER', N'byte', N'input', N'2', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (5, 1, N'ISOPENLOOPORCLOSELOOPORBOTH', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (6, 1, N'IMODSTART(MA)', N'UInt16', N'input', N'650', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (7, 1, N'IMODSTEP', N'byte', N'input', N'64', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (8, 1, N'IMODMETHOD', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (9, 1, N'TXERTOLERANCE(DB)', N'double', N'input', N'0.2', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (10, 1, N'TXERTARGET(DB)', N'double', N'input', N'4.5', 4, 5, 1, 0, 1, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (11, 1, N'ARRAYLISTTXPOWERCOEF', N'ArrayList', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (12, 1, N'IBIASMETHOD', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (13, 1, N'IBIASSTEP(MA)', N'byte', N'input', N'64', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (14, 1, N'IBIASSTART(MA)', N'UInt16', N'input', N'600', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (15, 1, N'IBIASMIN(MA)', N'UInt16', N'input', N'400', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (16, 1, N'IBIASMAX(MA)', N'UInt16', N'input', N'2500', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (17, 1, N'FIXEDMOD(MA)', N'UInt16', N'input', N'512', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (18, 1, N'TXLOPTOLERANCE(UW)', N'double', N'input', N'100', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (19, 1, N'TXLOPTARGET(UW)', N'double', N'input', N'900', 500, 1500, 1, 0, 1, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (20, 1, N'IMODMAX(MA)', N'UInt16', N'input', N'2500', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (21, 1, N'AUTOTUNE', N'bool', N'input', N'true', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (22, 2, N'FIXEDMODDAC(MA)', N'UInt16', N'input', N'512', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (23, 2, N'ARRAYLISTXDMICOEF', N'ArrayList', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (24, 2, N'1STOR2STORPID', N'byte', N'input', N'2', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (25, 2, N'IBIASADCORTXPOWERADC', N'byte', N'input', N'0', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (26, 2, N'ARRAYIBIAS(MA)', N'ArrayList', N'input', N'750,850,950', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (27, 2, N'AUTOTUNE', N'bool', N'input', N'true', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (28, 2, N'ISTEMPRELATIVE', N'bool', N'input', N'true', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (29, 3, N'AP(DBM)', N'double', N'output', N'-32768', -3.5, 2, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (30, 3, N'JITTERPP(PS)', N'double', N'output', N'-32768', -32768, 32767, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (31, 3, N'TXOMA(UW)', N'double', N'output', N'-32768', -32768, 32767, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (32, 3, N'FALLTIME(PS)', N'double', N'output', N'-32768', -32768, 65, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (33, 3, N'RISETIME(PS)', N'double', N'output', N'-32768', -32768, 100, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (34, 3, N'JITTERRMS(PS)', N'double', N'output', N'-32768', -32768, 30, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (35, 3, N'MASKMARGIN(%)', N'double', N'output', N'-32768', 0, 90, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (36, 3, N'ER(DB)', N'double', N'output', N'-32768', 3.7999999523162842, 5, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (37, 3, N'ISOPTICALEYEORELECEYE', N'bool', N'input', N'true', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (38, 3, N'CROSSING(%)', N'double', N'output', N'-32768', 40, 60, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (39, 4, N'DMITXPOWER(DBM)', N'double', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (40, 4, N'CURRENTTXPOWER(DBM)', N'double', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (41, 4, N'DMITXPOWERERR(DB)', N'double', N'output', N'-32768', -1.5, 1.5, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (42, 5, N'IBIAS(MA)', N'double', N'output', N'-32768', 10, 100, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (43, 6, N'LOSDVOLTAGETUNESTEP(V)', N'byte', N'input', N'32', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (44, 6, N'LOSAVOLTAGESTARTVALUE(V)', N'UInt16', N'input', N'15', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (45, 6, N'IsAdjustLos', N'bool', N'input', N'True', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (46, 6, N'AUTOTUNE', N'bool', N'input', N'true', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (47, 6, N'LOSDVOLTAGEUPERLIMIT(V)', N'UInt16', N'input', N'24135', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (48, 6, N'LOSDVOLTAGESTARTVALUE(V)', N'UInt16', N'input', N'300', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (49, 6, N'LOSDINPUTPOWER', N'double', N'input', N'-20', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (50, 6, N'LOSAVOLTAGETUNESTEP(V)', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (51, 6, N'LOSAVOLTAGEUPERLIMIT(V)', N'UInt16', N'input', N'15', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (52, 6, N'LosValue(V)', N'UINT32', N'input', N'0', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (53, 6, N'LOSDVOLTAGELOWLIMIT(V)', N'UInt16', N'input', N'1', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (54, 6, N'LOSAINPUTPOWER', N'double', N'input', N'-19', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (55, 6, N'LOSAVOLTAGELOWLIMIT(V)', N'UInt16', N'input', N'1', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (56, 7, N'ARRAYLISTDMIRXCOEF', N'ArrayList', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (57, 7, N'1STOR2STORPID', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (58, 7, N'ARRAYLISTRXPOWER(DBM)', N'ArrayList', N'input', N'-7,-10,-13,-16', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (59, 8, N'LOSA', N'double', N'output', N'-32768', -29, -16, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (60, 8, N'LOSD', N'double', N'output', N'-32768', -29, -13, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (61, 8, N'LOSADSTEP', N'double', N'input', N'0.5', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (62, 8, N'LOSDMAX', N'double', N'input', N'-13', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (63, 8, N'LOSAMIN', N'double', N'input', N'-29', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (64, 8, N'LOSAMAX', N'double', N'input', N'-16', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (65, 8, N'LOSH', N'double', N'output', N'-32768', 0.5, 32767, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (66, 9, N'ARRAYLISTRXINPUTPOWER(DBM)', N'ArrayList', N'input', N'-7,-10,-13,-16', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (67, 9, N'DMIRXPWRMAXERRPOINT(DBM)', N'double', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (68, 9, N'DMIRXPWRMAXERR', N'double', N'output', N'-32768', -3, 3, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (69, 10, N'CSENTARGETBER', N'double', N'input', N'1.0E-12', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (70, 10, N'COEFCSENSUBSTEP(DBM)', N'double', N'input', N'0.3', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (71, 10, N'CSEN(DBM)', N'double', N'output', N'-32768', -20, -11, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (72, 10, N'SEARCHTARGETBERSUBSTEP', N'double', N'input', N'0.3', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (73, 10, N'SEARCHTARGETBERADDSTEP', N'double', N'input', N'0.5', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (74, 10, N'SEARCHTARGETBERLL', N'double', N'input', N'3.00E-5', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (75, 10, N'SEARCHTARGETBERUL', N'double', N'input', N'1.00E-4', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (76, 10, N'CSENSTARTINGRXPWR(DBM)', N'double', N'input', N'-20', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (77, 10, N'CSENALIGNRXPWR(DBM)', N'double', N'input', N'-7', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (78, 10, N'COEFCSENADDSTEP(DBM)', N'double', N'input', N'0.3', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (79, 11, N'GENERALVCC(V)', N'double', N'input', N'3.3', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (80, 11, N'ARRAYLISTVCC(V)', N'ArrayList', N'input', N'3.1,3.3,3.5', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (81, 11, N'ARRAYLISTDMIVCCCOEF', N'ArrayList', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (82, 11, N'1STOR2STORPID', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (83, 12, N'1STOR2STORPID', N'byte', N'input', N'1', 0, 0, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (84, 12, N'ARRAYLISTDMITEMPCOEF', N'ArrayList', N'output', N'-32768', 0, 0, 0, 1, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (85, 13, N'DMITEMPERR(C)', N'double', N'output', N'-32768', -3, 3, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (86, 13, N'DMITEMP(C)', N'double', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (87, 14, N'DMIVCCERR(V)', N'double', N'output', N'-32768', -0.20000000298023224, 0.20000000298023224, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (88, 14, N'DMIVCC(V)', N'double', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (89, 15, N'ICC(MA)', N'double', N'output', N'-32768', 100, 2000, 1, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (90, 16, N'AUTOTUNE', N'bool', N'input', N'TRUE', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (91, 16, N'APDCALPOINT(DBM)', N'double', N'input', N'-24', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (92, 16, N'ARRAYLISTAPDBIASPOINTS(V)', N'ArrayList', N'input', N'700,725,750,775,800,825,850,875,900', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (93, 16, N'APDBIASSTEP(V)', N'byte', N'input', N'5', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (94, 16, N'1STOR2STORPID', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (95, 16, N'ARRAYLISTAPDCOEF', N'ArrayList', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (96, 17, N'TEMPHA', N'bool', N'output', N'-32768', -32768, 32767, 0, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (97, 17, N'TEMPHW', N'bool', N'output', N'-32768', -32768, 32767, 0, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (98, 17, N'TEMPLA', N'bool', N'output', N'-32768', -32768, 32767, 0, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (99, 17, N'TEMPLW', N'bool', N'output', N'-32768', -32768, 32767, 0, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (100, 17, N'VCCHA', N'bool', N'output', N'-32768', -32768, 32767, 0, 0, 0, 1)
GO
print 'Processed 100 total records'
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (101, 17, N'VCCHW', N'bool', N'output', N'-32768', -32768, 32767, 0, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (102, 17, N'VCCLA', N'bool', N'output', N'-32768', -32768, 32767, 0, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (103, 17, N'VCCLW', N'bool', N'output', N'-32768', -32768, 32767, 0, 0, 0, 1)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (104, 18, N'ARRAYLISTDMIRXCOEF', N'ArrayList', N'output', N'', -32768, 32767, 0, 1, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (105, 18, N'1STOR2STORPID', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (106, 18, N'ARRAYLISTRXPOWER(DBM)', N'ArrayList', N'input', N'-7,-10,-13,-16', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (107, 19, N'1STOR2STORPID', N'byte', N'input', N'1', 0, 0, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (108, 19, N'ARRAYLISTDMITEMPCOEF', N'ArrayList', N'output', N'-32768', 0, 0, 0, 1, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (109, 20, N'GENERALVCC(V)', N'double', N'input', N'3.3', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (110, 20, N'ARRAYLISTVCC(V)', N'ArrayList', N'input', N'3.1,3.3,3.5', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (111, 20, N'ARRAYLISTDMIVCCCOEF', N'ArrayList', N'output', N'-32768', -32768, 32767, 0, 1, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (112, 20, N'1STOR2STORPID', N'byte', N'input', N'1', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (114, 1, N'DCtoDC', N'bool', N'input', N'false', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (115, 1, N'FIXEDCrossDac', N'UInt32', N'input', N'200', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (116, 2, N'DCtoDC', N'bool', N'input', N'false', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (117, 2, N'FIXEDCrossDac', N'UInt32', N'input', N'200', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (120, 8, N'ISLOSDETAIL', N'bool', N'input', N'false', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (121, 1, N'PIDCOEFARRAY', N'ArrayList', N'input', N'-32768', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (122, 10, N'IsBerQuickTest', N'bool', N'input', N'false', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (123, 2, N'HighestCalTemp', N'double', N'input', N'0', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (124, 2, N'LowestCalTemp', N'double', N'input', N'0', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (125, 18, N'HasOffset', N'bool', N'input', N'false', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (126, 7, N'HasOffset', N'bool', N'input', N'false', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (127, 2, N'ISNEWALGORITHM', N'bool', N'input', N'false', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (128, 1, N'FIXEDIBIAS(MA)', N'UInt32', N'input', N'20', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (129, 1, N'FixedIBiasArray', N'ArrayList', N'input', N'280,280,280,280', -32768, 32767, 0, 0, 0, 0)
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [Direction], [ItemValue], [SpecMin], [SpecMax], [ItemSpecific], [LogRecord], [Failbreak], [DataRecord]) VALUES (130, 1, N'FixedModArray', N'ArrayList', N'input', N'500,500,500,500', -32768, 32767, 0, 0, 0, 0)
SET IDENTITY_INSERT [dbo].[GlobalTestModelParamterList] OFF
/****** Object:  Table [dbo].[GlobalProductionName]    Script Date: 11/12/2014 09:06:50 ******/
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
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [AuxAttribles]) VALUES (4, 3, N'SFP-TEST', N'SFP', 1, 1, 1, 1, N'X')
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [AuxAttribles]) VALUES (5, 4, N'XFP-TEST', N'XFP', 1, 1, 1, 1, N'x')
SET IDENTITY_INSERT [dbo].[GlobalProductionName] OFF
/****** Object:  Table [dbo].[GlobalMSAEEPROMInitialize]    Script Date: 11/12/2014 09:06:50 ******/
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
	[CRCData0] [nvarchar](512) NOT NULL,
	[Data1] [nvarchar](512) NOT NULL,
	[CRCData1] [nvarchar](512) NOT NULL,
	[Data2] [nvarchar](512) NOT NULL,
	[CRCData2] [nvarchar](512) NOT NULL,
	[Data3] [nvarchar](512) NOT NULL,
	[CRCData3] [nvarchar](512) NOT NULL,
 CONSTRAINT [PK_GlobalMSAEEPROMInitialize] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalMSAEEPROMInitialize] ON
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (2, 1, N'test1', N'QSFP', N'0CCA070200000000000000056700014B00000040417269737461204E6574776F726B732007001C73515346502D3430472D554E495620202030316658251C469B000000D85844503133313734303030312020202031343130313520200800007A1003000000000000000000000000000000000000000002F8000000008B451DF3', N'33FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'5000F6004B00FB00FFFFFFFFFFFFFFFF908871708C707548FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF7B8701AB621F02A4927C138888B81D4C7B870585621F06F2FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF0000000000000000000000000000000000000000000000000000000000000000', N'33FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'0', N'0', N'0', N'0')
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (3, 1, N'test3', N'QSFP', N'0102030405FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'0504030201FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'01CFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'CCCFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'0', N'0', N'0', N'0')
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (4, 1, N'test2', N'QSFP', N'0', N'0', N'0A0B0C0D0EFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'0', N'0', N'0', N'0')
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (6, 4, N'2', N'SFP', N'0304070000000000000000066700010E00000000494E4E4F4C494748542020202020202000447C7F54522D50583133432D5630302020202031412020051E00AF001A000020202020202020202020202020202020313230353034202068F005E3FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'0304070000000000000000066700010E00000000494E4E4F4C494748542020202020202000447C7F54522D50583133432D5630302020202031412020051E00AF001A000020202020202020202020202020202020313230353034202068F005E3FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'6400CE005F00D30094706D6090887148AFC803E89C4005DC3DE003B6313804B0312000642710007E000000000000000000000000000000000000000000000000000000003F8000000000000001000000010000000100000001000000000000351B2F81BD426C157400010000000032000040000000400000002232FFFFFFFF00', N'6400CE005F00D30094706D6090887148AFC803E89C4005DC3DE003B6313804B0312000642710007E000000000000000000000000000000000000000000000000000000003F8000000000000001000000010000000100000001000000000000351B2F81BD426C157400010000000032000040000000400000002232FFFFFFFF00', N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'0', N'0')
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (7, 5, N'test', N'XFP', N'069007000000000000140010637150000000007E494E4E4F4C4947485420202020202020FF447C7F54522D485832325A2D4E303020202020314179DC001846DEAF966600494E424251303130303037312020202031313132303820200860743934200000000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'068907000000000000140010637150000000007E494E4E4F4C4947485420202020202020FF447C7F54522D485832325A2D4E303020202020314179DC001846DEAF966600494E424251303130303037312020202031313132303820200860743934200000000000000000000000000000FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'06005500E2004B00E7000000000000000000FDE82710EA603A988A861BA76E1E22D00F8D000A0C5A00108CA0753088B87918FFFF0000FFFF000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000', N'06005500E2004B00E7000000000000000000FDE82710EA603A988A861BA76E1E22D00F8D000A0C5A00108CA0753088B87918FFFF0000FFFF000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000', N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF0000000000000000', N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF0000000000000000', N'0', N'0')
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (8, 5, N'XFPRev0.2', N'XFP', N'07085F00E7005A00EC00FFFFFFFFFFFFFFFF271000FA232801F43DE80630312D07CB312400642710007D94706D609088714800000000000000000000000000000000000000000000000000000000000000400040BC00000000000000000000001E0300000D61000000017F25000092B800000000000000000000000000000001', N'06005F00E7005A00EC00FFFFFFFFFFFFFFFF271000FA232801F43DE80630312D07CB312400642710007D94706D609088714800000000000000000000000000000000000000000000000000000000000000400040BC00000000000000000000001E0300000D61000000017F25000092B800000000000000000000000000000001', N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'0', N'0', N'0', N'0')
INSERT [dbo].[GlobalMSAEEPROMInitialize] ([ID], [PID], [ItemName], [ItemType], [Data0], [CRCData0], [Data1], [CRCData1], [Data2], [CRCData2], [Data3], [CRCData3]) VALUES (9, 1, N'test01', N'QSFP', N'0AFF060200000000000000056700014B00000040417269737461204E6574776F726B732007001C73515346502D3430472D554E495620202030316658251C469B000000D85844503133313734303030312020202031343130313520200800007A1003000000000000000000000000000000000000000002F8000000008B451DF3', N'33FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF', N'1', N'0', N'0', N'0', N'0', N'0')
SET IDENTITY_INSERT [dbo].[GlobalMSAEEPROMInitialize] OFF
/****** Object:  Table [dbo].[GlobalManufactureChipsetInitialize]    Script Date: 11/12/2014 09:06:50 ******/
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
SET IDENTITY_INSERT [dbo].[GlobalManufactureChipsetInitialize] OFF
/****** Object:  Table [dbo].[GlobalManufactureChipsetControl]    Script Date: 11/12/2014 09:06:50 ******/
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
SET IDENTITY_INSERT [dbo].[GlobalManufactureChipsetControl] OFF
/****** Object:  Default [DF__FunctionT__Title__5EFF0ABF]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[FunctionTable] ADD  CONSTRAINT [DF__FunctionT__Title__5EFF0ABF]  DEFAULT ('') FOR [Title]
GO
/****** Object:  Default [DF__FunctionT__Funct__5FF32EF8]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[FunctionTable] ADD  CONSTRAINT [DF__FunctionT__Funct__5FF32EF8]  DEFAULT ('0') FOR [FunctionCode]
GO
/****** Object:  Default [DF__FunctionT__Remar__60E75331]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[FunctionTable] ADD  CONSTRAINT [DF__FunctionT__Remar__60E75331]  DEFAULT ('') FOR [Remarks]
GO
/****** Object:  Default [DF_GlobalAllAppModelList_Name]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalAllAppModelList] ADD  CONSTRAINT [DF_GlobalAllAppModelList_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalAllAppModelList_Description]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalAllAppModelList] ADD  CONSTRAINT [DF_GlobalAllAppModelList_Description]  DEFAULT ('') FOR [ItemDescription]
GO
/****** Object:  Default [DF_GlobalAllEquipmentList_Name]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentList] ADD  CONSTRAINT [DF_GlobalAllEquipmentList_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalAllEquipmentList_Type]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentList] ADD  CONSTRAINT [DF_GlobalAllEquipmentList_Type]  DEFAULT ('') FOR [ItemType]
GO
/****** Object:  Default [DF_GlobalAllEquipmentList_Description]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentList] ADD  CONSTRAINT [DF_GlobalAllEquipmentList_Description]  DEFAULT ('') FOR [ItemDescription]
GO
/****** Object:  Default [DF_GlobalAllEquipmentParamterList_FieldName]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] ADD  CONSTRAINT [DF_GlobalAllEquipmentParamterList_FieldName]  DEFAULT ('') FOR [Item]
GO
/****** Object:  Default [DF_GlobalAllEquipmentParamterList_DefaultValue]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] ADD  CONSTRAINT [DF_GlobalAllEquipmentParamterList_DefaultValue]  DEFAULT ('') FOR [ItemValue]
GO
/****** Object:  Default [DF_GlobalAllEquipmentParamterList_Description]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] ADD  CONSTRAINT [DF_GlobalAllEquipmentParamterList_Description]  DEFAULT ('') FOR [ItemDescription]
GO
/****** Object:  Default [DF_GlobalAllEquipmentParamterList_TypeofValue]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] ADD  CONSTRAINT [DF_GlobalAllEquipmentParamterList_TypeofValue]  DEFAULT ('') FOR [ItemType]
GO
/****** Object:  Default [DF_GlobalAllTestModelList_Name]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalAllTestModelList] ADD  CONSTRAINT [DF_GlobalAllTestModelList_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalAllTestModelList_Description]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalAllTestModelList] ADD  CONSTRAINT [DF_GlobalAllTestModelList_Description]  DEFAULT ('') FOR [ItemDescription]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_PID]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_ItemName]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_ItemName]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_ModuleLine]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_ModuleLine]  DEFAULT ((0)) FOR [ModuleLine]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_ChipLine]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_ChipLine]  DEFAULT ((0)) FOR [ChipLine]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_DriveType]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_DriveType]  DEFAULT ((0)) FOR [DriveType]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_RegisterAddress]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_RegisterAddress]  DEFAULT ((0)) FOR [RegisterAddress]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_Length]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_Length]  DEFAULT ((1)) FOR [Length]
GO
/****** Object:  Default [DF_GlobalManufactureChipsetControl_Endianness]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] ADD  CONSTRAINT [DF_GlobalManufactureChipsetControl_Endianness]  DEFAULT ('false') FOR [Endianness]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_PID_1]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_PID_1]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_DriveType_1]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_DriveType_1]  DEFAULT ((0)) FOR [DriveType]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_ChipLine_1]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_ChipLine_1]  DEFAULT ((0)) FOR [ChipLine]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_RegisterAddress_1]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_RegisterAddress_1]  DEFAULT ((0)) FOR [RegisterAddress]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_Length_1]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_Length_1]  DEFAULT ((0)) FOR [Length]
GO
/****** Object:  Default [DF_ManufactureChipsetInitialize_ItemVaule]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] ADD  CONSTRAINT [DF_ManufactureChipsetInitialize_ItemVaule]  DEFAULT ((0)) FOR [ItemValue]
GO
/****** Object:  Default [DF_GlobalManufactureChipsetInitialize_Endianness]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] ADD  CONSTRAINT [DF_GlobalManufactureChipsetInitialize_Endianness]  DEFAULT ('false') FOR [Endianness]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_PID]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_TYPE]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_TYPE]  DEFAULT ('') FOR [ItemTYPE]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_Name]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_Channel]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_Channel]  DEFAULT ('0') FOR [Channel]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_Page]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_Page]  DEFAULT ('0') FOR [Page]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_StartAddress]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_StartAddress]  DEFAULT ('0') FOR [StartAddress]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_Length]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_Length]  DEFAULT ('0') FOR [Length]
GO
/****** Object:  Default [DF_GlobalManufactureMemory_Format]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients] ADD  CONSTRAINT [DF_GlobalManufactureMemory_Format]  DEFAULT ('') FOR [Format]
GO
/****** Object:  Default [DF_GlobalManufactureMemoryGroupTable_Name]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficientsGroup] ADD  CONSTRAINT [DF_GlobalManufactureMemoryGroupTable_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalMSA_Name]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSA] ADD  CONSTRAINT [DF_GlobalMSA_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalMSA_AccessInterface]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSA] ADD  CONSTRAINT [DF_GlobalMSA_AccessInterface]  DEFAULT ('') FOR [AccessInterface]
GO
/****** Object:  Default [DF_GlobalMSA_SlaveAddress]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSA] ADD  CONSTRAINT [DF_GlobalMSA_SlaveAddress]  DEFAULT ((0)) FOR [SlaveAddress]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_PID]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_FieldName]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_FieldName]  DEFAULT ('') FOR [FieldName]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_Channel]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_Channel]  DEFAULT ('0') FOR [Channel]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_SlaveAddress]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_SlaveAddress]  DEFAULT ('0') FOR [SlaveAddress]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_Page]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_Page]  DEFAULT ('0') FOR [Page]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_StartAddress]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_StartAddress]  DEFAULT ('0') FOR [StartAddress]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_Length]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_Length]  DEFAULT ('0') FOR [Length]
GO
/****** Object:  Default [DF_GlobalMSADefintionInf_Format]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf] ADD  CONSTRAINT [DF_GlobalMSADefintionInf_Format]  DEFAULT ('') FOR [Format]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_PID]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_ItemType1]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_ItemType1]  DEFAULT ((0)) FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_SlaveAddress]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_SlaveAddress]  DEFAULT ((0)) FOR [ItemType]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Page]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Page]  DEFAULT ((0)) FOR [Data0]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Address]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Address]  DEFAULT ((0)) FOR [CRCData0]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Length]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Length]  DEFAULT ((1)) FOR [Data1]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_ItemValue]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_ItemValue]  DEFAULT ((0)) FOR [CRCData1]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Data01]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data01]  DEFAULT ((0)) FOR [Data2]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Data02]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data02]  DEFAULT ((0)) FOR [CRCData2]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Data03]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data03]  DEFAULT ((0)) FOR [Data3]
GO
/****** Object:  Default [DF_GlobalMSAEEPROMInitialize_Data04]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] ADD  CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data04]  DEFAULT ((0)) FOR [CRCData3]
GO
/****** Object:  Default [DF_GlobalProductionName_PID]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_GlobalProductionName_PN]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_PN]  DEFAULT ('') FOR [PN]
GO
/****** Object:  Default [DF_GlobalProductionName_Name]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalProductionName_Channels]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_Channels]  DEFAULT ('4') FOR [Channels]
GO
/****** Object:  Default [DF_GlobalProductionName_Voltages]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_Voltages]  DEFAULT ((0)) FOR [Voltages]
GO
/****** Object:  Default [DF_GlobalProductionName_Tsensors]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_Tsensors]  DEFAULT ((0)) FOR [Tsensors]
GO
/****** Object:  Default [DF_GlobalProductionName_MGroupID]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_MGroupID]  DEFAULT ('0') FOR [MCoefsID]
GO
/****** Object:  Default [DF_GlobalProductionName_AuxAttribles]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalProductionName] ADD  CONSTRAINT [DF_GlobalProductionName_AuxAttribles]  DEFAULT ('') FOR [AuxAttribles]
GO
/****** Object:  Default [DF_GlobalProductionType_Name]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalProductionType] ADD  CONSTRAINT [DF_GlobalProductionType_Name]  DEFAULT ('') FOR [ItemName]
GO
/****** Object:  Default [DF_GlobalProductionType_MSAID]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalProductionType] ADD  CONSTRAINT [DF_GlobalProductionType_MSAID]  DEFAULT ('0') FOR [MSAID]
GO
/****** Object:  Default [DF_GlobalTestModelParamterList_ItemValue]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList] ADD  CONSTRAINT [DF_GlobalTestModelParamterList_ItemValue]  DEFAULT ('-32768') FOR [ItemValue]
GO
/****** Object:  Default [DF_GlobalTestModelParamterList_DefaultLowLimit]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList] ADD  CONSTRAINT [DF_GlobalTestModelParamterList_DefaultLowLimit]  DEFAULT ('-32768') FOR [SpecMin]
GO
/****** Object:  Default [DF_GlobalTestModelParamterList_DefaultUpperLimit]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList] ADD  CONSTRAINT [DF_GlobalTestModelParamterList_DefaultUpperLimit]  DEFAULT ('32767') FOR [SpecMax]
GO
/****** Object:  Default [DF_GlobalTestModelParamterList_ItemSpecific]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList] ADD  CONSTRAINT [DF_GlobalTestModelParamterList_ItemSpecific]  DEFAULT ('0') FOR [ItemSpecific]
GO
/****** Object:  Default [DF_GlobalTestModelParamterList_LogRecord]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList] ADD  CONSTRAINT [DF_GlobalTestModelParamterList_LogRecord]  DEFAULT ('0') FOR [LogRecord]
GO
/****** Object:  Default [DF_GlobalTestModelParamterList_Failbreak]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList] ADD  CONSTRAINT [DF_GlobalTestModelParamterList_Failbreak]  DEFAULT ('0') FOR [Failbreak]
GO
/****** Object:  Default [DF_GlobalTestModelParamterList_DataRecord]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList] ADD  CONSTRAINT [DF_GlobalTestModelParamterList_DataRecord]  DEFAULT ('0') FOR [DataRecord]
GO
/****** Object:  Default [DF_OperationLogs_PID]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[OperationLogs] ADD  CONSTRAINT [DF_OperationLogs_PID]  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF_Table_1_OperationType]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[OperationLogs] ADD  CONSTRAINT [DF_Table_1_OperationType]  DEFAULT ('') FOR [Optype]
GO
/****** Object:  Default [DF_Table_1_TestLog]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[OperationLogs] ADD  CONSTRAINT [DF_Table_1_TestLog]  DEFAULT ('') FOR [DetailLogs]
GO
/****** Object:  Default [DF__RoleFunct__RoleI__63C3BFDC]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[RoleFunctionTable] ADD  DEFAULT ('0') FOR [RoleID]
GO
/****** Object:  Default [DF__RoleFunct__Funct__64B7E415]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[RoleFunctionTable] ADD  DEFAULT ('0') FOR [FunctionID]
GO
/****** Object:  Default [DF__RolesTabl__RoleN__5B2E79DB]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[RolesTable] ADD  CONSTRAINT [DF__RolesTabl__RoleN__5B2E79DB]  DEFAULT ('') FOR [RoleName]
GO
/****** Object:  Default [DF__RolesTabl__Remar__5C229E14]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[RolesTable] ADD  CONSTRAINT [DF__RolesTabl__Remar__5C229E14]  DEFAULT ('') FOR [Remarks]
GO
/****** Object:  Default [DF_UserInfo_TrueName]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_TrueName]  DEFAULT ('') FOR [TrueName]
GO
/****** Object:  Default [DF_UserInfo_CreatTime]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_CreatTime]  DEFAULT (getdate()) FOR [lastLoginONTime]
GO
/****** Object:  Default [DF_UserInfo_lastComputerName]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_lastComputerName]  DEFAULT ('') FOR [lastComputerName]
GO
/****** Object:  Default [DF_UserInfo_lastLoginOffTime]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_lastLoginOffTime]  DEFAULT ('2000/1/1 12:00:00') FOR [lastLoginOffTime]
GO
/****** Object:  Default [DF_UserInfo_lastIP]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_lastIP]  DEFAULT ('0.0.0.0') FOR [lastIP]
GO
/****** Object:  Default [DF_UserInfo_Remarks]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_Remarks]  DEFAULT ('') FOR [Remarks]
GO
/****** Object:  Default [DF_UserLoginInfo_LoginOntime]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[UserLoginInfo] ADD  CONSTRAINT [DF_UserLoginInfo_LoginOntime]  DEFAULT (getdate()) FOR [LoginOntime]
GO
/****** Object:  Default [DF_UserLoginInfo_LoginOffTime]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[UserLoginInfo] ADD  CONSTRAINT [DF_UserLoginInfo_LoginOffTime]  DEFAULT (getdate()) FOR [LoginOffTime]
GO
/****** Object:  Default [DF_UserLoginInfo_Apptype]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[UserLoginInfo] ADD  CONSTRAINT [DF_UserLoginInfo_Apptype]  DEFAULT ((0)) FOR [Apptype]
GO
/****** Object:  Default [DF__UserRoleT__UserI__679450C0]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[UserRoleTable] ADD  CONSTRAINT [DF__UserRoleT__UserI__679450C0]  DEFAULT ('0') FOR [UserID]
GO
/****** Object:  Default [DF__UserRoleT__RoleI__688874F9]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[UserRoleTable] ADD  CONSTRAINT [DF__UserRoleT__RoleI__688874F9]  DEFAULT ('0') FOR [RoleID]
GO
/****** Object:  ForeignKey [FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList]  WITH CHECK ADD  CONSTRAINT [FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalAllEquipmentList] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] CHECK CONSTRAINT [FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList]
GO
/****** Object:  ForeignKey [FK_GlobalAllTestModelList_GlobalAllAppModelList]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalAllTestModelList]  WITH CHECK ADD  CONSTRAINT [FK_GlobalAllTestModelList_GlobalAllAppModelList] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalAllAppModelList] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalAllTestModelList] CHECK CONSTRAINT [FK_GlobalAllTestModelList_GlobalAllAppModelList]
GO
/****** Object:  ForeignKey [FK_GlobalManufactureChipsetControl_GlobalProductionName]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetControl]  WITH CHECK ADD  CONSTRAINT [FK_GlobalManufactureChipsetControl_GlobalProductionName] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionName] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] CHECK CONSTRAINT [FK_GlobalManufactureChipsetControl_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_GlobalManufactureChipsetInitialize_GlobalProductionName]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize]  WITH CHECK ADD  CONSTRAINT [FK_GlobalManufactureChipsetInitialize_GlobalProductionName] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionName] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] CHECK CONSTRAINT [FK_GlobalManufactureChipsetInitialize_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalManufactureCoefficients]  WITH CHECK ADD  CONSTRAINT [FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalManufactureCoefficientsGroup] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalManufactureCoefficients] CHECK CONSTRAINT [FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable]
GO
/****** Object:  ForeignKey [FK_GlobalMSADefintionInf_GlobalMSA]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSADefintionInf]  WITH CHECK ADD  CONSTRAINT [FK_GlobalMSADefintionInf_GlobalMSA] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalMSA] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[GlobalMSADefintionInf] CHECK CONSTRAINT [FK_GlobalMSADefintionInf_GlobalMSA]
GO
/****** Object:  ForeignKey [FK_GlobalMSAEEPROMInitialize_GlobalProductionName]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize]  WITH CHECK ADD  CONSTRAINT [FK_GlobalMSAEEPROMInitialize_GlobalProductionName] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionName] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalMSAEEPROMInitialize] CHECK CONSTRAINT [FK_GlobalMSAEEPROMInitialize_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_GlobalProductionName_GlobalManufactureCoefficientsGroup]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalProductionName]  WITH CHECK ADD  CONSTRAINT [FK_GlobalProductionName_GlobalManufactureCoefficientsGroup] FOREIGN KEY([MCoefsID])
REFERENCES [dbo].[GlobalManufactureCoefficientsGroup] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalProductionName] CHECK CONSTRAINT [FK_GlobalProductionName_GlobalManufactureCoefficientsGroup]
GO
/****** Object:  ForeignKey [FK_GlobalProductionName_GlobalProductionType]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalProductionName]  WITH CHECK ADD  CONSTRAINT [FK_GlobalProductionName_GlobalProductionType] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionType] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalProductionName] CHECK CONSTRAINT [FK_GlobalProductionName_GlobalProductionType]
GO
/****** Object:  ForeignKey [FK_GlobalProductionType_GlobalMSA]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalProductionType]  WITH CHECK ADD  CONSTRAINT [FK_GlobalProductionType_GlobalMSA] FOREIGN KEY([MSAID])
REFERENCES [dbo].[GlobalMSA] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[GlobalProductionType] CHECK CONSTRAINT [FK_GlobalProductionType_GlobalMSA]
GO
/****** Object:  ForeignKey [FK_GlobalTestModelParamterList_GlobalAllTestModelList]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[GlobalTestModelParamterList]  WITH CHECK ADD  CONSTRAINT [FK_GlobalTestModelParamterList_GlobalAllTestModelList] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalAllTestModelList] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalTestModelParamterList] CHECK CONSTRAINT [FK_GlobalTestModelParamterList_GlobalAllTestModelList]
GO
/****** Object:  ForeignKey [FK_OperationLogs_UserLoginInfo]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[OperationLogs]  WITH CHECK ADD  CONSTRAINT [FK_OperationLogs_UserLoginInfo] FOREIGN KEY([PID])
REFERENCES [dbo].[UserLoginInfo] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OperationLogs] CHECK CONSTRAINT [FK_OperationLogs_UserLoginInfo]
GO
/****** Object:  ForeignKey [FK_RoleFunctionTable_FunctionTable]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[RoleFunctionTable]  WITH CHECK ADD  CONSTRAINT [FK_RoleFunctionTable_FunctionTable] FOREIGN KEY([FunctionID])
REFERENCES [dbo].[FunctionTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleFunctionTable] CHECK CONSTRAINT [FK_RoleFunctionTable_FunctionTable]
GO
/****** Object:  ForeignKey [FK_RoleFunctionTable_RolesTable]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[RoleFunctionTable]  WITH CHECK ADD  CONSTRAINT [FK_RoleFunctionTable_RolesTable] FOREIGN KEY([RoleID])
REFERENCES [dbo].[RolesTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleFunctionTable] CHECK CONSTRAINT [FK_RoleFunctionTable_RolesTable]
GO
/****** Object:  ForeignKey [FK_UserRoleTable_RolesTable]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[UserRoleTable]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleTable_RolesTable] FOREIGN KEY([RoleID])
REFERENCES [dbo].[RolesTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoleTable] CHECK CONSTRAINT [FK_UserRoleTable_RolesTable]
GO
/****** Object:  ForeignKey [FK_UserRoleTable_UserInfo]    Script Date: 11/12/2014 09:06:50 ******/
ALTER TABLE [dbo].[UserRoleTable]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleTable_UserInfo] FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoleTable] CHECK CONSTRAINT [FK_UserRoleTable_UserInfo]
GO
