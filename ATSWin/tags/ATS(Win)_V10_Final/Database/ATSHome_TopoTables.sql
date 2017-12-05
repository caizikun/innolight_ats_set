USE [ATSHome]
GO
/****** Object:  ForeignKey [FK_OperationLogs_UserLoginInfo]    Script Date: 05/19/2015 12:44:01 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OperationLogs_UserLoginInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[OperationLogs]'))
ALTER TABLE [dbo].[OperationLogs] DROP CONSTRAINT [FK_OperationLogs_UserLoginInfo]
GO
/****** Object:  ForeignKey [FK_TopoEquipment_TopoTestPlan]    Script Date: 05/19/2015 12:44:01 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoEquipment_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoEquipment]'))
ALTER TABLE [dbo].[TopoEquipment] DROP CONSTRAINT [FK_TopoEquipment_TopoTestPlan]
GO
/****** Object:  ForeignKey [FK_TopoEquipmentParameter_TopoEquipment]    Script Date: 05/19/2015 12:44:02 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoEquipmentParameter_TopoEquipment]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoEquipmentParameter]'))
ALTER TABLE [dbo].[TopoEquipmentParameter] DROP CONSTRAINT [FK_TopoEquipmentParameter_TopoEquipment]
GO
/****** Object:  ForeignKey [FK_TopoLogRecord_TopoTestPlanRunRecordTable]    Script Date: 05/19/2015 12:44:02 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoLogRecord_TopoTestPlanRunRecordTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoLogRecord]'))
ALTER TABLE [dbo].[TopoLogRecord] DROP CONSTRAINT [FK_TopoLogRecord_TopoTestPlanRunRecordTable]
GO
/****** Object:  ForeignKey [FK_TopoManufactureConfigInit_TopoTestPlan]    Script Date: 05/19/2015 12:44:02 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoManufactureConfigInit_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoManufactureConfigInit]'))
ALTER TABLE [dbo].[TopoManufactureConfigInit] DROP CONSTRAINT [FK_TopoManufactureConfigInit_TopoTestPlan]
GO
/****** Object:  ForeignKey [FK_GlobalMSAEEPROMInitialize_GlobalProductionName]    Script Date: 05/19/2015 12:44:02 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalMSAEEPROMInitialize_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoMSAEEPROMSet]'))
ALTER TABLE [dbo].[TopoMSAEEPROMSet] DROP CONSTRAINT [FK_GlobalMSAEEPROMInitialize_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_TopoTestCoefBackup_TopoRunRecordTable]    Script Date: 05/19/2015 12:44:03 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestCoefBackup_TopoRunRecordTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestCoefBackup]'))
ALTER TABLE [dbo].[TopoTestCoefBackup] DROP CONSTRAINT [FK_TopoTestCoefBackup_TopoRunRecordTable]
GO
/****** Object:  ForeignKey [FK_TopoTestControll_TopoTestPlan]    Script Date: 05/19/2015 12:44:03 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestControll_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestControl]'))
ALTER TABLE [dbo].[TopoTestControl] DROP CONSTRAINT [FK_TopoTestControll_TopoTestPlan]
GO
/****** Object:  ForeignKey [FK_TopoTestData_TopoLogRecord]    Script Date: 05/19/2015 12:44:03 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestData_TopoLogRecord]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestData]'))
ALTER TABLE [dbo].[TopoTestData] DROP CONSTRAINT [FK_TopoTestData_TopoLogRecord]
GO
/****** Object:  ForeignKey [FK_TopoTestModel_TopoTestControll]    Script Date: 05/19/2015 12:44:03 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestModel_TopoTestControll]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestModel]'))
ALTER TABLE [dbo].[TopoTestModel] DROP CONSTRAINT [FK_TopoTestModel_TopoTestControll]
GO
/****** Object:  ForeignKey [FK_TopoTestParameter_TopoTestModel]    Script Date: 05/19/2015 12:44:03 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestParameter_TopoTestModel]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestParameter]'))
ALTER TABLE [dbo].[TopoTestParameter] DROP CONSTRAINT [FK_TopoTestParameter_TopoTestModel]
GO
/****** Object:  ForeignKey [FK_TopoTestPlan_GlobalProductionName]    Script Date: 05/19/2015 12:44:04 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestPlan_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestPlan]'))
ALTER TABLE [dbo].[TopoTestPlan] DROP CONSTRAINT [FK_TopoTestPlan_GlobalProductionName]
GO
/****** Object:  Table [dbo].[TopoTestParameter]    Script Date: 05/19/2015 12:44:03 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestParameter_TopoTestModel]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestParameter]'))
ALTER TABLE [dbo].[TopoTestParameter] DROP CONSTRAINT [FK_TopoTestParameter_TopoTestModel]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestParameter_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestParameter] DROP CONSTRAINT [DF_TopoTestParameter_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestParameter_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestParameter] DROP CONSTRAINT [DF_TopoTestParameter_Name]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestParameter_Type]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestParameter] DROP CONSTRAINT [DF_TopoTestParameter_Type]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestParameter_Direction]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestParameter] DROP CONSTRAINT [DF_TopoTestParameter_Direction]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestParameter_Value]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestParameter] DROP CONSTRAINT [DF_TopoTestParameter_Value]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestParameter_DefaultLowLimit]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestParameter] DROP CONSTRAINT [DF_TopoTestParameter_DefaultLowLimit]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestParameter_DefaultUpperLimit]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestParameter] DROP CONSTRAINT [DF_TopoTestParameter_DefaultUpperLimit]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestParameter_Specific]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestParameter] DROP CONSTRAINT [DF_TopoTestParameter_Specific]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestParameter_LogRecord]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestParameter] DROP CONSTRAINT [DF_TopoTestParameter_LogRecord]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestParameter_DataRecord]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestParameter] DROP CONSTRAINT [DF_TopoTestParameter_DataRecord]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoTestParameter]') AND type in (N'U'))
DROP TABLE [dbo].[TopoTestParameter]
GO
/****** Object:  Table [dbo].[TopoTestModel]    Script Date: 05/19/2015 12:44:03 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestModel_TopoTestControll]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestModel]'))
ALTER TABLE [dbo].[TopoTestModel] DROP CONSTRAINT [FK_TopoTestModel_TopoTestControll]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestModel_TestModelName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestModel] DROP CONSTRAINT [DF_TopoTestModel_TestModelName]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestModel_Seq]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestModel] DROP CONSTRAINT [DF_TopoTestModel_Seq]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestModel_AppModeID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestModel] DROP CONSTRAINT [DF_TopoTestModel_AppModeID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestModel_EquipmentList]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestModel] DROP CONSTRAINT [DF_TopoTestModel_EquipmentList]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestModel_IgnoreFlag]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestModel] DROP CONSTRAINT [DF_TopoTestModel_IgnoreFlag]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestModel_Failbreak_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestModel] DROP CONSTRAINT [DF_TopoTestModel_Failbreak_1]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoTestModel]') AND type in (N'U'))
DROP TABLE [dbo].[TopoTestModel]
GO
/****** Object:  Table [dbo].[TopoEquipmentParameter]    Script Date: 05/19/2015 12:44:02 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoEquipmentParameter_TopoEquipment]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoEquipmentParameter]'))
ALTER TABLE [dbo].[TopoEquipmentParameter] DROP CONSTRAINT [FK_TopoEquipmentParameter_TopoEquipment]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoEquipmentParameter_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoEquipmentParameter] DROP CONSTRAINT [DF_TopoEquipmentParameter_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoEquipmentParameter_Item]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoEquipmentParameter] DROP CONSTRAINT [DF_TopoEquipmentParameter_Item]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoEquipmentParameter_Value]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoEquipmentParameter] DROP CONSTRAINT [DF_TopoEquipmentParameter_Value]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoEquipmentParameter]') AND type in (N'U'))
DROP TABLE [dbo].[TopoEquipmentParameter]
GO
/****** Object:  Table [dbo].[TopoTestControl]    Script Date: 05/19/2015 12:44:03 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestControll_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestControl]'))
ALTER TABLE [dbo].[TopoTestControl] DROP CONSTRAINT [FK_TopoTestControll_TopoTestPlan]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestControll_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestControl] DROP CONSTRAINT [DF_TopoTestControll_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestControll_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestControl] DROP CONSTRAINT [DF_TopoTestControll_Name]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestControll_SEQ]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestControl] DROP CONSTRAINT [DF_TopoTestControll_SEQ]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestControll_Channel]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestControl] DROP CONSTRAINT [DF_TopoTestControll_Channel]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestControll_Temp]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestControl] DROP CONSTRAINT [DF_TopoTestControll_Temp]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestControll_Vcc]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestControl] DROP CONSTRAINT [DF_TopoTestControll_Vcc]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestControll_Pattent]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestControl] DROP CONSTRAINT [DF_TopoTestControll_Pattent]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestControll_DataRate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestControl] DROP CONSTRAINT [DF_TopoTestControll_DataRate]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestControll_AuxAttribles]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestControl] DROP CONSTRAINT [DF_TopoTestControll_AuxAttribles]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestControl_IgnoreFlag]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestControl] DROP CONSTRAINT [DF_TopoTestControl_IgnoreFlag]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestControl_ItemDescription]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestControl] DROP CONSTRAINT [DF_TopoTestControl_ItemDescription]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestControl_CtrlType_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestControl] DROP CONSTRAINT [DF_TopoTestControl_CtrlType_1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestControl_TempOffset]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestControl] DROP CONSTRAINT [DF_TopoTestControl_TempOffset]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoTestControl]') AND type in (N'U'))
DROP TABLE [dbo].[TopoTestControl]
GO
/****** Object:  Table [dbo].[TopoManufactureConfigInit]    Script Date: 05/19/2015 12:44:02 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoManufactureConfigInit_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoManufactureConfigInit]'))
ALTER TABLE [dbo].[TopoManufactureConfigInit] DROP CONSTRAINT [FK_TopoManufactureConfigInit_TopoTestPlan]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureEEPROMInitialize_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoManufactureConfigInit] DROP CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureEEPROMInitialize_SlaveAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoManufactureConfigInit] DROP CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_SlaveAddress]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureEEPROMInitialize_Page]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoManufactureConfigInit] DROP CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_Page]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureEEPROMInitialize_StartAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoManufactureConfigInit] DROP CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_StartAddress]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureEEPROMInitialize_Length]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoManufactureConfigInit] DROP CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_Length]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureEEPROMInitialize_ItemValue]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoManufactureConfigInit] DROP CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_ItemValue]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoManufactureConfigInit]') AND type in (N'U'))
DROP TABLE [dbo].[TopoManufactureConfigInit]
GO
/****** Object:  Table [dbo].[TopoEquipment]    Script Date: 05/19/2015 12:44:01 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoEquipment_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoEquipment]'))
ALTER TABLE [dbo].[TopoEquipment] DROP CONSTRAINT [FK_TopoEquipment_TopoTestPlan]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoEquipment_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoEquipment] DROP CONSTRAINT [DF_TopoEquipment_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoEquipment_SEQ]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoEquipment] DROP CONSTRAINT [DF_TopoEquipment_SEQ]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoEquipment_Type]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoEquipment] DROP CONSTRAINT [DF_TopoEquipment_Type]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoEquipment_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoEquipment] DROP CONSTRAINT [DF_TopoEquipment_Name]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoEquipment_Roel]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoEquipment] DROP CONSTRAINT [DF_TopoEquipment_Roel]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoEquipment]') AND type in (N'U'))
DROP TABLE [dbo].[TopoEquipment]
GO
/****** Object:  Table [dbo].[TopoMSAEEPROMSet]    Script Date: 05/19/2015 12:44:02 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalMSAEEPROMInitialize_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoMSAEEPROMSet]'))
ALTER TABLE [dbo].[TopoMSAEEPROMSet] DROP CONSTRAINT [FK_GlobalMSAEEPROMInitialize_GlobalProductionName]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSAEEPROMInitialize_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoMSAEEPROMSet] DROP CONSTRAINT [DF_GlobalMSAEEPROMInitialize_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSAEEPROMInitialize_ItemType1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoMSAEEPROMSet] DROP CONSTRAINT [DF_GlobalMSAEEPROMInitialize_ItemType1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSAEEPROMInitialize_SlaveAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoMSAEEPROMSet] DROP CONSTRAINT [DF_GlobalMSAEEPROMInitialize_SlaveAddress]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSAEEPROMInitialize_Page]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoMSAEEPROMSet] DROP CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Page]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSAEEPROMInitialize_Address]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoMSAEEPROMSet] DROP CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Address]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSAEEPROMInitialize_Length]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoMSAEEPROMSet] DROP CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Length]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSAEEPROMInitialize_ItemValue]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoMSAEEPROMSet] DROP CONSTRAINT [DF_GlobalMSAEEPROMInitialize_ItemValue]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSAEEPROMInitialize_Data01]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoMSAEEPROMSet] DROP CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data01]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSAEEPROMInitialize_Data02]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoMSAEEPROMSet] DROP CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data02]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSAEEPROMInitialize_Data03]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoMSAEEPROMSet] DROP CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data03]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSAEEPROMInitialize_Data04]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoMSAEEPROMSet] DROP CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data04]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoMSAEEPROMSet]') AND type in (N'U'))
DROP TABLE [dbo].[TopoMSAEEPROMSet]
GO
/****** Object:  Table [dbo].[TopoTestPlan]    Script Date: 05/19/2015 12:44:04 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestPlan_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestPlan]'))
ALTER TABLE [dbo].[TopoTestPlan] DROP CONSTRAINT [FK_TopoTestPlan_GlobalProductionName]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestPlan_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestPlan] DROP CONSTRAINT [DF_TopoTestPlan_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestPlan_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestPlan] DROP CONSTRAINT [DF_TopoTestPlan_Name]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestPlan_SWVersion]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestPlan] DROP CONSTRAINT [DF_TopoTestPlan_SWVersion]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestPlan_HwVersion]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestPlan] DROP CONSTRAINT [DF_TopoTestPlan_HwVersion]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestPlan_USBPort]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestPlan] DROP CONSTRAINT [DF_TopoTestPlan_USBPort]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestPlan_IsChipInitialize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestPlan] DROP CONSTRAINT [DF_TopoTestPlan_IsChipInitialize]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestPlan_IsEEPROMInitialize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestPlan] DROP CONSTRAINT [DF_TopoTestPlan_IsEEPROMInitialize]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestPlan_IgnoreBackupCoef]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestPlan] DROP CONSTRAINT [DF_TopoTestPlan_IgnoreBackupCoef]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestPlan_AuxAttribles]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestPlan] DROP CONSTRAINT [DF_TopoTestPlan_AuxAttribles]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestPlan_IgnoreFlag]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestPlan] DROP CONSTRAINT [DF_TopoTestPlan_IgnoreFlag]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestPlan_ItemDescription]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestPlan] DROP CONSTRAINT [DF_TopoTestPlan_ItemDescription]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoTestPlan]') AND type in (N'U'))
DROP TABLE [dbo].[TopoTestPlan]
GO
/****** Object:  Table [dbo].[TopoTestData]    Script Date: 05/19/2015 12:44:03 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestData_TopoLogRecord]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestData]'))
ALTER TABLE [dbo].[TopoTestData] DROP CONSTRAINT [FK_TopoTestData_TopoLogRecord]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestData_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestData] DROP CONSTRAINT [DF_TopoTestData_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestData_ItemName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestData] DROP CONSTRAINT [DF_TopoTestData_ItemName]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestData_ItemValue]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestData] DROP CONSTRAINT [DF_TopoTestData_ItemValue]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestData_PassOrFail]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestData] DROP CONSTRAINT [DF_TopoTestData_PassOrFail]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestData_SpecMin]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestData] DROP CONSTRAINT [DF_TopoTestData_SpecMin]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestData_SpecMax]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestData] DROP CONSTRAINT [DF_TopoTestData_SpecMax]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoTestData]') AND type in (N'U'))
DROP TABLE [dbo].[TopoTestData]
GO
/****** Object:  Table [dbo].[OperationLogs]    Script Date: 05/19/2015 12:44:01 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OperationLogs_UserLoginInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[OperationLogs]'))
ALTER TABLE [dbo].[OperationLogs] DROP CONSTRAINT [FK_OperationLogs_UserLoginInfo]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_OperationLogs_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OperationLogs] DROP CONSTRAINT [DF_OperationLogs_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_OperationLogs_ModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OperationLogs] DROP CONSTRAINT [DF_OperationLogs_ModifyTime]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_OperationType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OperationLogs] DROP CONSTRAINT [DF_Table_1_OperationType]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_TestLog]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OperationLogs] DROP CONSTRAINT [DF_Table_1_TestLog]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OperationLogs]') AND type in (N'U'))
DROP TABLE [dbo].[OperationLogs]
GO
/****** Object:  Table [dbo].[TopoLogRecord]    Script Date: 05/19/2015 12:44:02 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoLogRecord_TopoTestPlanRunRecordTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoLogRecord]'))
ALTER TABLE [dbo].[TopoLogRecord] DROP CONSTRAINT [FK_TopoLogRecord_TopoTestPlanRunRecordTable]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoLogRecord_TestPlanID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoLogRecord] DROP CONSTRAINT [DF_TopoLogRecord_TestPlanID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoLogRecord_StartTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoLogRecord] DROP CONSTRAINT [DF_TopoLogRecord_StartTime]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoLogRecord_EndTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoLogRecord] DROP CONSTRAINT [DF_TopoLogRecord_EndTime]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoLogRecord_TestLog]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoLogRecord] DROP CONSTRAINT [DF_TopoLogRecord_TestLog]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoLogRecord_Temp]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoLogRecord] DROP CONSTRAINT [DF_TopoLogRecord_Temp]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoLogRecord_Temp1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoLogRecord] DROP CONSTRAINT [DF_TopoLogRecord_Temp1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoLogRecord_Channel]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoLogRecord] DROP CONSTRAINT [DF_TopoLogRecord_Channel]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoLogRecord_Result]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoLogRecord] DROP CONSTRAINT [DF_TopoLogRecord_Result]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoLogRecord_CtrlType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoLogRecord] DROP CONSTRAINT [DF_TopoLogRecord_CtrlType]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoLogRecord]') AND type in (N'U'))
DROP TABLE [dbo].[TopoLogRecord]
GO
/****** Object:  Table [dbo].[TopoTestCoefBackup]    Script Date: 05/19/2015 12:44:03 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestCoefBackup_TopoRunRecordTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestCoefBackup]'))
ALTER TABLE [dbo].[TopoTestCoefBackup] DROP CONSTRAINT [FK_TopoTestCoefBackup_TopoRunRecordTable]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestCoefBackup_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestCoefBackup] DROP CONSTRAINT [DF_TopoTestCoefBackup_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_RunRecordID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestCoefBackup] DROP CONSTRAINT [DF_Table_1_RunRecordID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestCoefBackup_Page]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestCoefBackup] DROP CONSTRAINT [DF_TopoTestCoefBackup_Page]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_StartTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestCoefBackup] DROP CONSTRAINT [DF_Table_1_StartTime]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_EndTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoTestCoefBackup] DROP CONSTRAINT [DF_Table_1_EndTime]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoTestCoefBackup]') AND type in (N'U'))
DROP TABLE [dbo].[TopoTestCoefBackup]
GO
/****** Object:  Table [dbo].[UserLoginInfo]    Script Date: 05/19/2015 12:44:04 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserLoginInfo_LoginOntime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserLoginInfo] DROP CONSTRAINT [DF_UserLoginInfo_LoginOntime]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserLoginInfo_LoginOffTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserLoginInfo] DROP CONSTRAINT [DF_UserLoginInfo_LoginOffTime]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserLoginInfo_Apptype]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserLoginInfo] DROP CONSTRAINT [DF_UserLoginInfo_Apptype]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserLoginInfo]') AND type in (N'U'))
DROP TABLE [dbo].[UserLoginInfo]
GO
/****** Object:  Table [dbo].[TopoRunRecordTable]    Script Date: 05/19/2015 12:44:02 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestSN_SN]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoRunRecordTable] DROP CONSTRAINT [DF_TopoTestSN_SN]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestPlanRunRecordTable_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoRunRecordTable] DROP CONSTRAINT [DF_TopoTestPlanRunRecordTable_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestPlanRunRecordTable_StartTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoRunRecordTable] DROP CONSTRAINT [DF_TopoTestPlanRunRecordTable_StartTime]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoTestPlanRunRecordTable_EndTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoRunRecordTable] DROP CONSTRAINT [DF_TopoTestPlanRunRecordTable_EndTime]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoRunRecordTable_FWRev]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoRunRecordTable] DROP CONSTRAINT [DF_TopoRunRecordTable_FWRev]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoRunRecordTable_IP]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoRunRecordTable] DROP CONSTRAINT [DF_TopoRunRecordTable_IP]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoRunRecordTable_LightSource_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoRunRecordTable] DROP CONSTRAINT [DF_TopoRunRecordTable_LightSource_1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TopoRunRecordTable_Remark_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TopoRunRecordTable] DROP CONSTRAINT [DF_TopoRunRecordTable_Remark_1]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoRunRecordTable]') AND type in (N'U'))
DROP TABLE [dbo].[TopoRunRecordTable]
GO
/****** Object:  Table [dbo].[TopoRunRecordTable]    Script Date: 05/19/2015 12:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoRunRecordTable]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TopoRunRecordTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SN] [nvarchar](30) NOT NULL CONSTRAINT [DF_TopoTestSN_SN]  DEFAULT (''),
	[PID] [int] NOT NULL CONSTRAINT [DF_TopoTestPlanRunRecordTable_PID]  DEFAULT ('0'),
	[StartTime] [datetime] NOT NULL CONSTRAINT [DF_TopoTestPlanRunRecordTable_StartTime]  DEFAULT (getdate()),
	[EndTime] [datetime] NOT NULL CONSTRAINT [DF_TopoTestPlanRunRecordTable_EndTime]  DEFAULT (getdate()),
	[FWRev] [nvarchar](5) NOT NULL CONSTRAINT [DF_TopoRunRecordTable_FWRev]  DEFAULT ('00'),
	[IP] [nvarchar](50) NOT NULL CONSTRAINT [DF_TopoRunRecordTable_IP]  DEFAULT (''),
	[LightSource] [nvarchar](100) NOT NULL CONSTRAINT [DF_TopoRunRecordTable_LightSource_1]  DEFAULT (''),
	[Remark] [ntext] NOT NULL CONSTRAINT [DF_TopoRunRecordTable_Remark_1]  DEFAULT (''),
 CONSTRAINT [PK_TopoTestSN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TopoRunRecordTable]') AND name = N'IX_TopoRunRecordTable')
CREATE NONCLUSTERED INDEX [IX_TopoRunRecordTable] ON [dbo].[TopoRunRecordTable] 
(
	[PID] ASC,
	[SN] ASC,
	[IP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLoginInfo]    Script Date: 05/19/2015 12:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserLoginInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserLoginInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[LoginOntime] [datetime] NOT NULL CONSTRAINT [DF_UserLoginInfo_LoginOntime]  DEFAULT (getdate()),
	[LoginOffTime] [datetime] NOT NULL CONSTRAINT [DF_UserLoginInfo_LoginOffTime]  DEFAULT (getdate()),
	[Apptype] [nvarchar](50) NOT NULL CONSTRAINT [DF_UserLoginInfo_Apptype]  DEFAULT ((0)),
	[LoginInfo] [ntext] NOT NULL,
	[OPLogs] [ntext] NULL,
 CONSTRAINT [PK_UserLoginInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserLoginInfo]') AND name = N'IX_UserLoginInfo')
CREATE NONCLUSTERED INDEX [IX_UserLoginInfo] ON [dbo].[UserLoginInfo] 
(
	[UserName] ASC,
	[Apptype] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoTestCoefBackup]    Script Date: 05/19/2015 12:44:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoTestCoefBackup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TopoTestCoefBackup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_TopoTestCoefBackup_PID]  DEFAULT ((0)),
	[StartAddr] [int] NOT NULL CONSTRAINT [DF_Table_1_RunRecordID]  DEFAULT ((0)),
	[Page] [tinyint] NOT NULL CONSTRAINT [DF_TopoTestCoefBackup_Page]  DEFAULT ((0)),
	[ItemSize] [tinyint] NOT NULL CONSTRAINT [DF_Table_1_StartTime]  DEFAULT ((0)),
	[ItemValue] [nvarchar](16) NOT NULL CONSTRAINT [DF_Table_1_EndTime]  DEFAULT (''),
 CONSTRAINT [PK_TopoTestCoefBackup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TopoLogRecord]    Script Date: 05/19/2015 12:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoLogRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TopoLogRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RunRecordID] [int] NOT NULL CONSTRAINT [DF_TopoLogRecord_TestPlanID]  DEFAULT ('0'),
	[StartTime] [datetime] NOT NULL CONSTRAINT [DF_TopoLogRecord_StartTime]  DEFAULT (getdate()),
	[EndTime] [datetime] NOT NULL CONSTRAINT [DF_TopoLogRecord_EndTime]  DEFAULT (getdate()),
	[TestLog] [ntext] NULL CONSTRAINT [DF_TopoLogRecord_TestLog]  DEFAULT (''),
	[Temp] [real] NOT NULL CONSTRAINT [DF_TopoLogRecord_Temp]  DEFAULT ((-32768)),
	[Voltage] [real] NOT NULL CONSTRAINT [DF_TopoLogRecord_Temp1]  DEFAULT ((-32768)),
	[Channel] [tinyint] NOT NULL CONSTRAINT [DF_TopoLogRecord_Channel]  DEFAULT ((0)),
	[Result] [bit] NOT NULL CONSTRAINT [DF_TopoLogRecord_Result]  DEFAULT ('false'),
	[CtrlType] [tinyint] NOT NULL CONSTRAINT [DF_TopoLogRecord_CtrlType]  DEFAULT ((2)),
 CONSTRAINT [PK_TopoLogRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[OperationLogs]    Script Date: 05/19/2015 12:44:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OperationLogs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OperationLogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_OperationLogs_PID]  DEFAULT ((0)),
	[ModifyTime] [datetime] NOT NULL CONSTRAINT [DF_OperationLogs_ModifyTime]  DEFAULT (getdate()),
	[Optype] [nvarchar](max) NOT NULL CONSTRAINT [DF_Table_1_OperationType]  DEFAULT (''),
	[DetailLogs] [ntext] NULL CONSTRAINT [DF_Table_1_TestLog]  DEFAULT (''),
 CONSTRAINT [PK_OperationLogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[OperationLogs]') AND name = N'IX_OperationLogs')
CREATE NONCLUSTERED INDEX [IX_OperationLogs] ON [dbo].[OperationLogs] 
(
	[ModifyTime] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoTestData]    Script Date: 05/19/2015 12:44:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoTestData]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TopoTestData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_TopoTestData_PID]  DEFAULT ((0)),
	[ItemName] [nvarchar](30) NOT NULL CONSTRAINT [DF_TopoTestData_ItemName]  DEFAULT (''),
	[ItemValue] [float] NOT NULL CONSTRAINT [DF_TopoTestData_ItemValue]  DEFAULT (''),
	[Result] [bit] NOT NULL CONSTRAINT [DF_TopoTestData_PassOrFail]  DEFAULT ('false'),
	[SpecMin] [float] NOT NULL CONSTRAINT [DF_TopoTestData_SpecMin]  DEFAULT ((-32768)),
	[SpecMax] [float] NOT NULL CONSTRAINT [DF_TopoTestData_SpecMax]  DEFAULT ((32767)),
 CONSTRAINT [PK_TopoTestData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TopoTestData]') AND name = N'IX_TopoTestData')
CREATE NONCLUSTERED INDEX [IX_TopoTestData] ON [dbo].[TopoTestData] 
(
	[PID] ASC,
	[ItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoTestPlan]    Script Date: 05/19/2015 12:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoTestPlan]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TopoTestPlan](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_TopoTestPlan_PID]  DEFAULT ((0)),
	[ItemName] [nvarchar](30) NOT NULL CONSTRAINT [DF_TopoTestPlan_Name]  DEFAULT (''),
	[SWVersion] [nvarchar](30) NOT NULL CONSTRAINT [DF_TopoTestPlan_SWVersion]  DEFAULT (''),
	[HwVersion] [nvarchar](30) NOT NULL CONSTRAINT [DF_TopoTestPlan_HwVersion]  DEFAULT (''),
	[USBPort] [tinyint] NOT NULL CONSTRAINT [DF_TopoTestPlan_USBPort]  DEFAULT ('0'),
	[IsChipInitialize] [bit] NOT NULL CONSTRAINT [DF_TopoTestPlan_IsChipInitialize]  DEFAULT ('false'),
	[IsEEPROMInitialize] [bit] NOT NULL CONSTRAINT [DF_TopoTestPlan_IsEEPROMInitialize]  DEFAULT ('false'),
	[IgnoreBackupCoef] [bit] NOT NULL CONSTRAINT [DF_TopoTestPlan_IgnoreBackupCoef]  DEFAULT ('false'),
	[AuxAttribles] [nvarchar](255) NOT NULL CONSTRAINT [DF_TopoTestPlan_AuxAttribles]  DEFAULT (''),
	[IgnoreFlag] [bit] NOT NULL CONSTRAINT [DF_TopoTestPlan_IgnoreFlag]  DEFAULT ('false'),
	[ItemDescription] [nvarchar](200) NOT NULL CONSTRAINT [DF_TopoTestPlan_ItemDescription]  DEFAULT (''),
 CONSTRAINT [PK_TopoTestPlan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_TopoTestPlan] UNIQUE NONCLUSTERED 
(
	[PID] ASC,
	[ItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TopoMSAEEPROMSet]    Script Date: 05/19/2015 12:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoMSAEEPROMSet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TopoMSAEEPROMSet](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_GlobalMSAEEPROMInitialize_PID]  DEFAULT ((0)),
	[ItemName] [nvarchar](50) NOT NULL CONSTRAINT [DF_GlobalMSAEEPROMInitialize_ItemType1]  DEFAULT ((0)),
	[ItemType] [nvarchar](20) NOT NULL CONSTRAINT [DF_GlobalMSAEEPROMInitialize_SlaveAddress]  DEFAULT ((0)),
	[Data0] [nvarchar](512) NOT NULL CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Page]  DEFAULT ((0)),
	[CRCData0] [tinyint] NOT NULL CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Address]  DEFAULT ((0)),
	[Data1] [nvarchar](512) NOT NULL CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Length]  DEFAULT ((1)),
	[CRCData1] [tinyint] NOT NULL CONSTRAINT [DF_GlobalMSAEEPROMInitialize_ItemValue]  DEFAULT ((0)),
	[Data2] [nvarchar](512) NOT NULL CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data01]  DEFAULT ((0)),
	[CRCData2] [tinyint] NOT NULL CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data02]  DEFAULT ((0)),
	[Data3] [nvarchar](512) NOT NULL CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data03]  DEFAULT ((0)),
	[CRCData3] [tinyint] NOT NULL CONSTRAINT [DF_GlobalMSAEEPROMInitialize_Data04]  DEFAULT ((0)),
 CONSTRAINT [PK_GlobalMSAEEPROMInitialize] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalMSAEEPROMInitialize] UNIQUE NONCLUSTERED 
(
	[PID] ASC,
	[ItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TopoEquipment]    Script Date: 05/19/2015 12:44:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoEquipment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TopoEquipment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_TopoEquipment_PID]  DEFAULT ((0)),
	[SEQ] [int] NOT NULL CONSTRAINT [DF_TopoEquipment_SEQ]  DEFAULT ((1)),
	[ItemType] [nvarchar](30) NOT NULL CONSTRAINT [DF_TopoEquipment_Type]  DEFAULT (''),
	[ItemName] [nvarchar](100) NOT NULL CONSTRAINT [DF_TopoEquipment_Name]  DEFAULT (''),
	[Role] [smallint] NOT NULL CONSTRAINT [DF_TopoEquipment_Roel]  DEFAULT ((0)),
 CONSTRAINT [PK_TopoEquipment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TopoEquipment]') AND name = N'IX_TopoEquipment')
CREATE NONCLUSTERED INDEX [IX_TopoEquipment] ON [dbo].[TopoEquipment] 
(
	[ItemType] ASC,
	[ItemName] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoManufactureConfigInit]    Script Date: 05/19/2015 12:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoManufactureConfigInit]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TopoManufactureConfigInit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_PID]  DEFAULT ((0)),
	[SlaveAddress] [int] NOT NULL CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_SlaveAddress]  DEFAULT ('0'),
	[Page] [tinyint] NOT NULL CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_Page]  DEFAULT ('0'),
	[StartAddress] [int] NOT NULL CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_StartAddress]  DEFAULT ('0'),
	[Length] [tinyint] NOT NULL CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_Length]  DEFAULT ('0'),
	[ItemValue] [int] NOT NULL CONSTRAINT [DF_GlobalManufactureEEPROMInitialize_ItemValue]  DEFAULT ((0)),
 CONSTRAINT [PK_TopoManufactureConfigInit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TopoTestControl]    Script Date: 05/19/2015 12:44:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoTestControl]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TopoTestControl](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_TopoTestControll_PID]  DEFAULT ((0)),
	[ItemName] [nvarchar](30) NOT NULL CONSTRAINT [DF_TopoTestControll_Name]  DEFAULT (''),
	[SEQ] [int] NOT NULL CONSTRAINT [DF_TopoTestControll_SEQ]  DEFAULT ('0'),
	[Channel] [tinyint] NOT NULL CONSTRAINT [DF_TopoTestControll_Channel]  DEFAULT ('0'),
	[Temp] [real] NOT NULL CONSTRAINT [DF_TopoTestControll_Temp]  DEFAULT ('0'),
	[Vcc] [real] NOT NULL CONSTRAINT [DF_TopoTestControll_Vcc]  DEFAULT ('3.3'),
	[Pattent] [tinyint] NOT NULL CONSTRAINT [DF_TopoTestControll_Pattent]  DEFAULT ('7'),
	[DataRate] [nvarchar](50) NOT NULL CONSTRAINT [DF_TopoTestControll_DataRate]  DEFAULT ('10312500000'),
	[AuxAttribles] [nvarchar](1024) NOT NULL CONSTRAINT [DF_TopoTestControll_AuxAttribles]  DEFAULT (''),
	[IgnoreFlag] [bit] NOT NULL CONSTRAINT [DF_TopoTestControl_IgnoreFlag]  DEFAULT ('false'),
	[ItemDescription] [nvarchar](200) NOT NULL CONSTRAINT [DF_TopoTestControl_ItemDescription]  DEFAULT (''),
	[CtrlType] [tinyint] NOT NULL CONSTRAINT [DF_TopoTestControl_CtrlType_1]  DEFAULT ((2)),
	[TempOffset] [real] NOT NULL CONSTRAINT [DF_TopoTestControl_TempOffset]  DEFAULT ((0)),
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
END
GO
/****** Object:  Table [dbo].[TopoEquipmentParameter]    Script Date: 05/19/2015 12:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoEquipmentParameter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TopoEquipmentParameter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_TopoEquipmentParameter_PID]  DEFAULT ((0)),
	[Item] [nvarchar](30) NOT NULL CONSTRAINT [DF_TopoEquipmentParameter_Item]  DEFAULT (''),
	[ItemValue] [nvarchar](255) NOT NULL CONSTRAINT [DF_TopoEquipmentParameter_Value]  DEFAULT (''),
 CONSTRAINT [PK_TopoEquipmentParameter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_TopoEquipmentParameter] UNIQUE NONCLUSTERED 
(
	[Item] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TopoTestModel]    Script Date: 05/19/2015 12:44:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoTestModel]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TopoTestModel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemName] [nvarchar](50) NOT NULL CONSTRAINT [DF_TopoTestModel_TestModelName]  DEFAULT (''),
	[Seq] [int] NOT NULL CONSTRAINT [DF_TopoTestModel_Seq]  DEFAULT ('0'),
	[AppModeID] [int] NOT NULL CONSTRAINT [DF_TopoTestModel_AppModeID]  DEFAULT ('0'),
	[EquipmentList] [nvarchar](500) NOT NULL CONSTRAINT [DF_TopoTestModel_EquipmentList]  DEFAULT (''),
	[IgnoreFlag] [bit] NOT NULL CONSTRAINT [DF_TopoTestModel_IgnoreFlag]  DEFAULT ('false'),
	[Failbreak] [bit] NOT NULL CONSTRAINT [DF_TopoTestModel_Failbreak_1]  DEFAULT ('false'),
 CONSTRAINT [PK_TopoTestModel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_TopoTestModel_1] UNIQUE NONCLUSTERED 
(
	[ItemName] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TopoTestModel]') AND name = N'IX_TopoTestModel')
CREATE NONCLUSTERED INDEX [IX_TopoTestModel] ON [dbo].[TopoTestModel] 
(
	[AppModeID] ASC,
	[ItemName] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopoTestParameter]    Script Date: 05/19/2015 12:44:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TopoTestParameter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TopoTestParameter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_TopoTestParameter_PID]  DEFAULT ((0)),
	[ItemName] [nvarchar](30) NOT NULL CONSTRAINT [DF_TopoTestParameter_Name]  DEFAULT (''),
	[ItemType] [nvarchar](10) NOT NULL CONSTRAINT [DF_TopoTestParameter_Type]  DEFAULT (''),
	[Direction] [nvarchar](10) NOT NULL CONSTRAINT [DF_TopoTestParameter_Direction]  DEFAULT (''),
	[ItemValue] [nvarchar](50) NOT NULL CONSTRAINT [DF_TopoTestParameter_Value]  DEFAULT (''),
	[SpecMin] [float] NOT NULL CONSTRAINT [DF_TopoTestParameter_DefaultLowLimit]  DEFAULT ((-32768)),
	[SpecMax] [float] NOT NULL CONSTRAINT [DF_TopoTestParameter_DefaultUpperLimit]  DEFAULT ((32767)),
	[ItemSpecific] [tinyint] NOT NULL CONSTRAINT [DF_TopoTestParameter_Specific]  DEFAULT ((0)),
	[LogRecord] [tinyint] NOT NULL CONSTRAINT [DF_TopoTestParameter_LogRecord]  DEFAULT ((0)),
	[DataRecord] [tinyint] NOT NULL CONSTRAINT [DF_TopoTestParameter_DataRecord]  DEFAULT ((0)),
 CONSTRAINT [PK_TopoTestParameter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_TopoTestParameter_1] UNIQUE NONCLUSTERED 
(
	[PID] ASC,
	[ItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[TopoTestParameter]') AND name = N'IX_TopoTestParameter')
CREATE NONCLUSTERED INDEX [IX_TopoTestParameter] ON [dbo].[TopoTestParameter] 
(
	[ItemName] ASC,
	[ItemType] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_OperationLogs_UserLoginInfo]    Script Date: 05/19/2015 12:44:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OperationLogs_UserLoginInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[OperationLogs]'))
ALTER TABLE [dbo].[OperationLogs]  WITH CHECK ADD  CONSTRAINT [FK_OperationLogs_UserLoginInfo] FOREIGN KEY([PID])
REFERENCES [dbo].[UserLoginInfo] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OperationLogs_UserLoginInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[OperationLogs]'))
ALTER TABLE [dbo].[OperationLogs] CHECK CONSTRAINT [FK_OperationLogs_UserLoginInfo]
GO
/****** Object:  ForeignKey [FK_TopoEquipment_TopoTestPlan]    Script Date: 05/19/2015 12:44:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoEquipment_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoEquipment]'))
ALTER TABLE [dbo].[TopoEquipment]  WITH CHECK ADD  CONSTRAINT [FK_TopoEquipment_TopoTestPlan] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoTestPlan] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoEquipment_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoEquipment]'))
ALTER TABLE [dbo].[TopoEquipment] CHECK CONSTRAINT [FK_TopoEquipment_TopoTestPlan]
GO
/****** Object:  ForeignKey [FK_TopoEquipmentParameter_TopoEquipment]    Script Date: 05/19/2015 12:44:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoEquipmentParameter_TopoEquipment]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoEquipmentParameter]'))
ALTER TABLE [dbo].[TopoEquipmentParameter]  WITH CHECK ADD  CONSTRAINT [FK_TopoEquipmentParameter_TopoEquipment] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoEquipment] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoEquipmentParameter_TopoEquipment]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoEquipmentParameter]'))
ALTER TABLE [dbo].[TopoEquipmentParameter] CHECK CONSTRAINT [FK_TopoEquipmentParameter_TopoEquipment]
GO
/****** Object:  ForeignKey [FK_TopoLogRecord_TopoTestPlanRunRecordTable]    Script Date: 05/19/2015 12:44:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoLogRecord_TopoTestPlanRunRecordTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoLogRecord]'))
ALTER TABLE [dbo].[TopoLogRecord]  WITH CHECK ADD  CONSTRAINT [FK_TopoLogRecord_TopoTestPlanRunRecordTable] FOREIGN KEY([RunRecordID])
REFERENCES [dbo].[TopoRunRecordTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoLogRecord_TopoTestPlanRunRecordTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoLogRecord]'))
ALTER TABLE [dbo].[TopoLogRecord] CHECK CONSTRAINT [FK_TopoLogRecord_TopoTestPlanRunRecordTable]
GO
/****** Object:  ForeignKey [FK_TopoManufactureConfigInit_TopoTestPlan]    Script Date: 05/19/2015 12:44:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoManufactureConfigInit_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoManufactureConfigInit]'))
ALTER TABLE [dbo].[TopoManufactureConfigInit]  WITH CHECK ADD  CONSTRAINT [FK_TopoManufactureConfigInit_TopoTestPlan] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoTestPlan] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoManufactureConfigInit_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoManufactureConfigInit]'))
ALTER TABLE [dbo].[TopoManufactureConfigInit] CHECK CONSTRAINT [FK_TopoManufactureConfigInit_TopoTestPlan]
GO
/****** Object:  ForeignKey [FK_GlobalMSAEEPROMInitialize_GlobalProductionName]    Script Date: 05/19/2015 12:44:02 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalMSAEEPROMInitialize_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoMSAEEPROMSet]'))
ALTER TABLE [dbo].[TopoMSAEEPROMSet]  WITH CHECK ADD  CONSTRAINT [FK_GlobalMSAEEPROMInitialize_GlobalProductionName] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionName] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalMSAEEPROMInitialize_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoMSAEEPROMSet]'))
ALTER TABLE [dbo].[TopoMSAEEPROMSet] CHECK CONSTRAINT [FK_GlobalMSAEEPROMInitialize_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_TopoTestCoefBackup_TopoRunRecordTable]    Script Date: 05/19/2015 12:44:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestCoefBackup_TopoRunRecordTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestCoefBackup]'))
ALTER TABLE [dbo].[TopoTestCoefBackup]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestCoefBackup_TopoRunRecordTable] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoRunRecordTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestCoefBackup_TopoRunRecordTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestCoefBackup]'))
ALTER TABLE [dbo].[TopoTestCoefBackup] CHECK CONSTRAINT [FK_TopoTestCoefBackup_TopoRunRecordTable]
GO
/****** Object:  ForeignKey [FK_TopoTestControll_TopoTestPlan]    Script Date: 05/19/2015 12:44:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestControll_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestControl]'))
ALTER TABLE [dbo].[TopoTestControl]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestControll_TopoTestPlan] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoTestPlan] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestControll_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestControl]'))
ALTER TABLE [dbo].[TopoTestControl] CHECK CONSTRAINT [FK_TopoTestControll_TopoTestPlan]
GO
/****** Object:  ForeignKey [FK_TopoTestData_TopoLogRecord]    Script Date: 05/19/2015 12:44:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestData_TopoLogRecord]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestData]'))
ALTER TABLE [dbo].[TopoTestData]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestData_TopoLogRecord] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoLogRecord] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestData_TopoLogRecord]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestData]'))
ALTER TABLE [dbo].[TopoTestData] CHECK CONSTRAINT [FK_TopoTestData_TopoLogRecord]
GO
/****** Object:  ForeignKey [FK_TopoTestModel_TopoTestControll]    Script Date: 05/19/2015 12:44:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestModel_TopoTestControll]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestModel]'))
ALTER TABLE [dbo].[TopoTestModel]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestModel_TopoTestControll] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoTestControl] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestModel_TopoTestControll]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestModel]'))
ALTER TABLE [dbo].[TopoTestModel] CHECK CONSTRAINT [FK_TopoTestModel_TopoTestControll]
GO
/****** Object:  ForeignKey [FK_TopoTestParameter_TopoTestModel]    Script Date: 05/19/2015 12:44:03 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestParameter_TopoTestModel]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestParameter]'))
ALTER TABLE [dbo].[TopoTestParameter]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestParameter_TopoTestModel] FOREIGN KEY([PID])
REFERENCES [dbo].[TopoTestModel] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestParameter_TopoTestModel]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestParameter]'))
ALTER TABLE [dbo].[TopoTestParameter] CHECK CONSTRAINT [FK_TopoTestParameter_TopoTestModel]
GO
/****** Object:  ForeignKey [FK_TopoTestPlan_GlobalProductionName]    Script Date: 05/19/2015 12:44:04 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestPlan_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestPlan]'))
ALTER TABLE [dbo].[TopoTestPlan]  WITH CHECK ADD  CONSTRAINT [FK_TopoTestPlan_GlobalProductionName] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionName] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TopoTestPlan_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[TopoTestPlan]'))
ALTER TABLE [dbo].[TopoTestPlan] CHECK CONSTRAINT [FK_TopoTestPlan_GlobalProductionName]
GO
