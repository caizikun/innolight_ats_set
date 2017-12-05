USE [ATS_V2]
GO
/****** Object:  ForeignKey [FK_BlockAscxInfo_AscxFile]    Script Date: 09/21/2015 15:16:26 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BlockAscxInfo_AscxFile]') AND parent_object_id = OBJECT_ID(N'[dbo].[BlockAscxInfo]'))
ALTER TABLE [dbo].[BlockAscxInfo] DROP CONSTRAINT [FK_BlockAscxInfo_AscxFile]
GO
/****** Object:  ForeignKey [FK_BlockAscxInfo_FunctionTable]    Script Date: 09/21/2015 15:16:26 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BlockAscxInfo_FunctionTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[BlockAscxInfo]'))
ALTER TABLE [dbo].[BlockAscxInfo] DROP CONSTRAINT [FK_BlockAscxInfo_FunctionTable]
GO
/****** Object:  ForeignKey [FK_ChannelMap_PNChipMap]    Script Date: 09/21/2015 15:16:26 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChannelMap_PNChipMap]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChannelMap]'))
ALTER TABLE [dbo].[ChannelMap] DROP CONSTRAINT [FK_ChannelMap_PNChipMap]
GO
/****** Object:  ForeignKey [FK_ChipRegister_RegisterFormula]    Script Date: 09/21/2015 15:16:26 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChipRegister_RegisterFormula]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChipRegister]'))
ALTER TABLE [dbo].[ChipRegister] DROP CONSTRAINT [FK_ChipRegister_RegisterFormula]
GO
/****** Object:  ForeignKey [FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList]    Script Date: 09/21/2015 15:16:27 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalAllEquipmentParamterList]'))
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] DROP CONSTRAINT [FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList]
GO
/****** Object:  ForeignKey [FK_GlobalAllTestModelList_GlobalAllAppModelList]    Script Date: 09/21/2015 15:16:27 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalAllTestModelList_GlobalAllAppModelList]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalAllTestModelList]'))
ALTER TABLE [dbo].[GlobalAllTestModelList] DROP CONSTRAINT [FK_GlobalAllTestModelList_GlobalAllAppModelList]
GO
/****** Object:  ForeignKey [FK_GlobalManufactureChipsetControl_GlobalProductionName]    Script Date: 09/21/2015 15:16:27 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalManufactureChipsetControl_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalManufactureChipsetControl]'))
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] DROP CONSTRAINT [FK_GlobalManufactureChipsetControl_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_GlobalManufactureChipsetInitialize_GlobalProductionName]    Script Date: 09/21/2015 15:16:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalManufactureChipsetInitialize_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalManufactureChipsetInitialize]'))
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] DROP CONSTRAINT [FK_GlobalManufactureChipsetInitialize_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable]    Script Date: 09/21/2015 15:16:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalManufactureCoefficients]'))
ALTER TABLE [dbo].[GlobalManufactureCoefficients] DROP CONSTRAINT [FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable]
GO
/****** Object:  ForeignKey [FK_GlobalMSADefintionInf_GlobalMSA]    Script Date: 09/21/2015 15:16:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalMSADefintionInf_GlobalMSA]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalMSADefintionInf]'))
ALTER TABLE [dbo].[GlobalMSADefintionInf] DROP CONSTRAINT [FK_GlobalMSADefintionInf_GlobalMSA]
GO
/****** Object:  ForeignKey [FK_GlobalProductionName_GlobalManufactureCoefficientsGroup]    Script Date: 09/21/2015 15:16:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalProductionName_GlobalManufactureCoefficientsGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalProductionName]'))
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [FK_GlobalProductionName_GlobalManufactureCoefficientsGroup]
GO
/****** Object:  ForeignKey [FK_GlobalProductionName_GlobalProductionType]    Script Date: 09/21/2015 15:16:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalProductionName_GlobalProductionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalProductionName]'))
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [FK_GlobalProductionName_GlobalProductionType]
GO
/****** Object:  ForeignKey [FK_GlobalProductionType_GlobalMSA]    Script Date: 09/21/2015 15:16:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalProductionType_GlobalMSA]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalProductionType]'))
ALTER TABLE [dbo].[GlobalProductionType] DROP CONSTRAINT [FK_GlobalProductionType_GlobalMSA]
GO
/****** Object:  ForeignKey [FK_GlobalTestModelParamterList_GlobalAllTestModelList]    Script Date: 09/21/2015 15:16:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalTestModelParamterList_GlobalAllTestModelList]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalTestModelParamterList]'))
ALTER TABLE [dbo].[GlobalTestModelParamterList] DROP CONSTRAINT [FK_GlobalTestModelParamterList_GlobalAllTestModelList]
GO
/****** Object:  ForeignKey [FK_PNChipMap_ChipBaseInfo]    Script Date: 09/21/2015 15:16:30 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PNChipMap_ChipBaseInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[PNChipMap]'))
ALTER TABLE [dbo].[PNChipMap] DROP CONSTRAINT [FK_PNChipMap_ChipBaseInfo]
GO
/****** Object:  ForeignKey [FK_PNChipMap_GlobalProductionName]    Script Date: 09/21/2015 15:16:30 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PNChipMap_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[PNChipMap]'))
ALTER TABLE [dbo].[PNChipMap] DROP CONSTRAINT [FK_PNChipMap_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_RegisterFormula_ChipBaseInfo]    Script Date: 09/21/2015 15:16:30 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RegisterFormula_ChipBaseInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[RegisterFormula]'))
ALTER TABLE [dbo].[RegisterFormula] DROP CONSTRAINT [FK_RegisterFormula_ChipBaseInfo]
GO
/****** Object:  ForeignKey [FK_RoleFunctionTable_FunctionTable]    Script Date: 09/21/2015 15:16:30 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleFunctionTable_FunctionTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleFunctionTable]'))
ALTER TABLE [dbo].[RoleFunctionTable] DROP CONSTRAINT [FK_RoleFunctionTable_FunctionTable]
GO
/****** Object:  ForeignKey [FK_RoleFunctionTable_RolesTable]    Script Date: 09/21/2015 15:16:30 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleFunctionTable_RolesTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleFunctionTable]'))
ALTER TABLE [dbo].[RoleFunctionTable] DROP CONSTRAINT [FK_RoleFunctionTable_RolesTable]
GO
/****** Object:  ForeignKey [FK_GlobalUserPlanAction_TopoTestPlan]    Script Date: 09/21/2015 15:16:30 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPlanAction_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPlanAction]'))
ALTER TABLE [dbo].[UserPlanAction] DROP CONSTRAINT [FK_GlobalUserPlanAction_TopoTestPlan]
GO
/****** Object:  ForeignKey [FK_GlobalUserPlanAction_UserInfo]    Script Date: 09/21/2015 15:16:30 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPlanAction_UserInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPlanAction]'))
ALTER TABLE [dbo].[UserPlanAction] DROP CONSTRAINT [FK_GlobalUserPlanAction_UserInfo]
GO
/****** Object:  ForeignKey [FK_GlobalUserPNAction_GlobalProductionName]    Script Date: 09/21/2015 15:16:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPNAction_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPNAction]'))
ALTER TABLE [dbo].[UserPNAction] DROP CONSTRAINT [FK_GlobalUserPNAction_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_GlobalUserPNAction_UserInfo]    Script Date: 09/21/2015 15:16:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPNAction_UserInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPNAction]'))
ALTER TABLE [dbo].[UserPNAction] DROP CONSTRAINT [FK_GlobalUserPNAction_UserInfo]
GO
/****** Object:  ForeignKey [FK_UserRoleTable_RolesTable]    Script Date: 09/21/2015 15:16:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoleTable_RolesTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoleTable]'))
ALTER TABLE [dbo].[UserRoleTable] DROP CONSTRAINT [FK_UserRoleTable_RolesTable]
GO
/****** Object:  ForeignKey [FK_UserRoleTable_UserInfo]    Script Date: 09/21/2015 15:16:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoleTable_UserInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoleTable]'))
ALTER TABLE [dbo].[UserRoleTable] DROP CONSTRAINT [FK_UserRoleTable_UserInfo]
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalTestModelParamterList]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalTestModelParamterList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_GlobalTestModelParamterList]
GO
/****** Object:  StoredProcedure [dbo].[Pro_TopoTestModelWithParams]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_TopoTestModelWithParams]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_TopoTestModelWithParams]
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalAllEquipmentParamterList]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalAllEquipmentParamterList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_GlobalAllEquipmentParamterList]
GO
/****** Object:  StoredProcedure [dbo].[Pro_CopyFlowCtrl]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_CopyFlowCtrl]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_CopyFlowCtrl]
GO
/****** Object:  StoredProcedure [dbo].[Pro_CopyTestPlan]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_CopyTestPlan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_CopyTestPlan]
GO
/****** Object:  Table [dbo].[GlobalTestModelParamterList]    Script Date: 09/21/2015 15:16:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalTestModelParamterList_GlobalAllTestModelList]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalTestModelParamterList]'))
ALTER TABLE [dbo].[GlobalTestModelParamterList] DROP CONSTRAINT [FK_GlobalTestModelParamterList_GlobalAllTestModelList]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalTestModelParamterList_ItemValue]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalTestModelParamterList] DROP CONSTRAINT [DF_GlobalTestModelParamterList_ItemValue]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalTestModelParamterList_ItemValue1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalTestModelParamterList] DROP CONSTRAINT [DF_GlobalTestModelParamterList_ItemValue1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalTestModelParamterList_ItemDescription1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalTestModelParamterList] DROP CONSTRAINT [DF_GlobalTestModelParamterList_ItemDescription1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalTestModelParamterList_ItemDescription]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalTestModelParamterList] DROP CONSTRAINT [DF_GlobalTestModelParamterList_ItemDescription]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalTestModelParamterList]') AND type in (N'U'))
DROP TABLE [dbo].[GlobalTestModelParamterList]
GO
/****** Object:  Table [dbo].[GlobalAllEquipmentParamterList]    Script Date: 09/21/2015 15:16:27 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalAllEquipmentParamterList]'))
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] DROP CONSTRAINT [FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalAllEquipmentParamterList_FieldName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] DROP CONSTRAINT [DF_GlobalAllEquipmentParamterList_FieldName]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalAllEquipmentParamterList_TypeofValue]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] DROP CONSTRAINT [DF_GlobalAllEquipmentParamterList_TypeofValue]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalAllEquipmentParamterList_DefaultValue]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] DROP CONSTRAINT [DF_GlobalAllEquipmentParamterList_DefaultValue]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalAllEquipmentParamterList_NeedSelect]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] DROP CONSTRAINT [DF_GlobalAllEquipmentParamterList_NeedSelect]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalAllEquipmentParamterList_Optionalparams]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] DROP CONSTRAINT [DF_GlobalAllEquipmentParamterList_Optionalparams]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalAllEquipmentParamterList_Description]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] DROP CONSTRAINT [DF_GlobalAllEquipmentParamterList_Description]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalAllEquipmentParamterList]') AND type in (N'U'))
DROP TABLE [dbo].[GlobalAllEquipmentParamterList]
GO
/****** Object:  StoredProcedure [dbo].[Pro_TopoEquipmentWithParams]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_TopoEquipmentWithParams]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_TopoEquipmentWithParams]
GO
/****** Object:  StoredProcedure [dbo].[Pro_TopoManufactureConfigInit]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_TopoManufactureConfigInit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_TopoManufactureConfigInit]
GO
/****** Object:  StoredProcedure [dbo].[Pro_TopoPNSpecsParams]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_TopoPNSpecsParams]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_TopoPNSpecsParams]
GO
/****** Object:  StoredProcedure [dbo].[Pro_TopoTestControl]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_TopoTestControl]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_TopoTestControl]
GO
/****** Object:  StoredProcedure [dbo].[Pro_UserPlanAction]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_UserPlanAction]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_UserPlanAction]
GO
/****** Object:  StoredProcedure [dbo].[Pro_ChannelMap]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_ChannelMap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_ChannelMap]
GO
/****** Object:  Table [dbo].[ChannelMap]    Script Date: 09/21/2015 15:16:26 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChannelMap_PNChipMap]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChannelMap]'))
ALTER TABLE [dbo].[ChannelMap] DROP CONSTRAINT [FK_ChannelMap_PNChipMap]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_PNID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChannelMap] DROP CONSTRAINT [DF_Table_1_PNID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_ChipID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChannelMap] DROP CONSTRAINT [DF_Table_1_ChipID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ChannelMap_ChipLine]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChannelMap] DROP CONSTRAINT [DF_ChannelMap_ChipLine]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChannelMap]') AND type in (N'U'))
DROP TABLE [dbo].[ChannelMap]
GO
/****** Object:  StoredProcedure [dbo].[Pro_UserPNAction]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_UserPNAction]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_UserPNAction]
GO
/****** Object:  StoredProcedure [dbo].[Pro_TopoMSAEEPROMSet]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_TopoMSAEEPROMSet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_TopoMSAEEPROMSet]
GO
/****** Object:  StoredProcedure [dbo].[Pro_PNChipMap]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_PNChipMap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_PNChipMap]
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalManufactureChipsetControl]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalManufactureChipsetControl]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_GlobalManufactureChipsetControl]
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalManufactureChipsetInitialize]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalManufactureChipsetInitialize]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_GlobalManufactureChipsetInitialize]
GO
/****** Object:  StoredProcedure [dbo].[Pro_TopoTestPlan]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_TopoTestPlan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_TopoTestPlan]
GO
/****** Object:  Table [dbo].[UserPlanAction]    Script Date: 09/21/2015 15:16:30 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPlanAction_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPlanAction]'))
ALTER TABLE [dbo].[UserPlanAction] DROP CONSTRAINT [FK_GlobalUserPlanAction_TopoTestPlan]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPlanAction_UserInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPlanAction]'))
ALTER TABLE [dbo].[UserPlanAction] DROP CONSTRAINT [FK_GlobalUserPlanAction_UserInfo]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_AddPlan]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserPlanAction] DROP CONSTRAINT [DF_Table_1_AddPlan]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_ModifyPlan1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserPlanAction] DROP CONSTRAINT [DF_Table_1_ModifyPlan1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalUserPlanAction_RunPlan]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserPlanAction] DROP CONSTRAINT [DF_GlobalUserPlanAction_RunPlan]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserPlanAction]') AND type in (N'U'))
DROP TABLE [dbo].[UserPlanAction]
GO
/****** Object:  Table [dbo].[UserPNAction]    Script Date: 09/21/2015 15:16:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPNAction_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPNAction]'))
ALTER TABLE [dbo].[UserPNAction] DROP CONSTRAINT [FK_GlobalUserPNAction_GlobalProductionName]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPNAction_UserInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPNAction]'))
ALTER TABLE [dbo].[UserPNAction] DROP CONSTRAINT [FK_GlobalUserPNAction_UserInfo]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalUserPNAction_AddPlan]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserPNAction] DROP CONSTRAINT [DF_GlobalUserPNAction_AddPlan]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalUserPNAction_ModifyPNInfo]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserPNAction] DROP CONSTRAINT [DF_GlobalUserPNAction_ModifyPNInfo]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserPNAction]') AND type in (N'U'))
DROP TABLE [dbo].[UserPNAction]
GO
/****** Object:  StoredProcedure [dbo].[Pro_ChipRegister]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_ChipRegister]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_ChipRegister]
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalProductionName]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalProductionName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_GlobalProductionName]
GO
/****** Object:  Table [dbo].[PNChipMap]    Script Date: 09/21/2015 15:16:30 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PNChipMap_ChipBaseInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[PNChipMap]'))
ALTER TABLE [dbo].[PNChipMap] DROP CONSTRAINT [FK_PNChipMap_ChipBaseInfo]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PNChipMap_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[PNChipMap]'))
ALTER TABLE [dbo].[PNChipMap] DROP CONSTRAINT [FK_PNChipMap_GlobalProductionName]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_ItemName_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PNChipMap] DROP CONSTRAINT [DF_Table_1_ItemName_1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_AccessInterface]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PNChipMap] DROP CONSTRAINT [DF_Table_1_AccessInterface]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_SlaveAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PNChipMap] DROP CONSTRAINT [DF_Table_1_SlaveAddress]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PNChipMap_ChipDirection]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PNChipMap] DROP CONSTRAINT [DF_PNChipMap_ChipDirection]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PNChipMap]') AND type in (N'U'))
DROP TABLE [dbo].[PNChipMap]
GO
/****** Object:  Table [dbo].[GlobalManufactureChipsetControl]    Script Date: 09/21/2015 15:16:27 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalManufactureChipsetControl_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalManufactureChipsetControl]'))
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] DROP CONSTRAINT [FK_GlobalManufactureChipsetControl_GlobalProductionName]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ManufactureChipsetInitialize_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] DROP CONSTRAINT [DF_ManufactureChipsetInitialize_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ManufactureChipsetInitialize_ItemName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] DROP CONSTRAINT [DF_ManufactureChipsetInitialize_ItemName]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ManufactureChipsetInitialize_ModuleLine]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] DROP CONSTRAINT [DF_ManufactureChipsetInitialize_ModuleLine]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ManufactureChipsetInitialize_ChipLine]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] DROP CONSTRAINT [DF_ManufactureChipsetInitialize_ChipLine]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ManufactureChipsetInitialize_DriveType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] DROP CONSTRAINT [DF_ManufactureChipsetInitialize_DriveType]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ManufactureChipsetInitialize_RegisterAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] DROP CONSTRAINT [DF_ManufactureChipsetInitialize_RegisterAddress]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ManufactureChipsetInitialize_Length]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] DROP CONSTRAINT [DF_ManufactureChipsetInitialize_Length]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureChipsetControl_Endianness]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] DROP CONSTRAINT [DF_GlobalManufactureChipsetControl_Endianness]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalManufactureChipsetControl]') AND type in (N'U'))
DROP TABLE [dbo].[GlobalManufactureChipsetControl]
GO
/****** Object:  Table [dbo].[GlobalManufactureChipsetInitialize]    Script Date: 09/21/2015 15:16:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalManufactureChipsetInitialize_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalManufactureChipsetInitialize]'))
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] DROP CONSTRAINT [FK_GlobalManufactureChipsetInitialize_GlobalProductionName]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ManufactureChipsetInitialize_PID_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] DROP CONSTRAINT [DF_ManufactureChipsetInitialize_PID_1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ManufactureChipsetInitialize_DriveType_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] DROP CONSTRAINT [DF_ManufactureChipsetInitialize_DriveType_1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ManufactureChipsetInitialize_ChipLine_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] DROP CONSTRAINT [DF_ManufactureChipsetInitialize_ChipLine_1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ManufactureChipsetInitialize_RegisterAddress_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] DROP CONSTRAINT [DF_ManufactureChipsetInitialize_RegisterAddress_1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ManufactureChipsetInitialize_Length_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] DROP CONSTRAINT [DF_ManufactureChipsetInitialize_Length_1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ManufactureChipsetInitialize_ItemVaule]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] DROP CONSTRAINT [DF_ManufactureChipsetInitialize_ItemVaule]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureChipsetInitialize_Endianness]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] DROP CONSTRAINT [DF_GlobalManufactureChipsetInitialize_Endianness]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalManufactureChipsetInitialize]') AND type in (N'U'))
DROP TABLE [dbo].[GlobalManufactureChipsetInitialize]
GO
/****** Object:  Table [dbo].[ChipRegister]    Script Date: 09/21/2015 15:16:26 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChipRegister_RegisterFormula]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChipRegister]'))
ALTER TABLE [dbo].[ChipRegister] DROP CONSTRAINT [FK_ChipRegister_RegisterFormula]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_ItemName_2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChipRegister] DROP CONSTRAINT [DF_Table_1_ItemName_2]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_WriteFormula]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChipRegister] DROP CONSTRAINT [DF_Table_1_WriteFormula]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_AnalogueUnit]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChipRegister] DROP CONSTRAINT [DF_Table_1_AnalogueUnit]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_ReadFormula]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChipRegister] DROP CONSTRAINT [DF_Table_1_ReadFormula]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ChipRegister_ChannelNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChipRegister] DROP CONSTRAINT [DF_ChipRegister_ChannelNumber]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChipRegister]') AND type in (N'U'))
DROP TABLE [dbo].[ChipRegister]
GO
/****** Object:  StoredProcedure [dbo].[InsertLogRecord]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertLogRecord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertLogRecord]
GO
/****** Object:  StoredProcedure [dbo].[Pro_ChipBaseInfo]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_ChipBaseInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_ChipBaseInfo]
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalAllAppModelList]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalAllAppModelList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_GlobalAllAppModelList]
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalAllEquipmentList]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalAllEquipmentList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_GlobalAllEquipmentList]
GO
/****** Object:  Table [dbo].[GlobalProductionName]    Script Date: 09/21/2015 15:16:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalProductionName_GlobalManufactureCoefficientsGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalProductionName]'))
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [FK_GlobalProductionName_GlobalManufactureCoefficientsGroup]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalProductionName_GlobalProductionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalProductionName]'))
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [FK_GlobalProductionName_GlobalProductionType]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_PN]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_PN]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_Name]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_Channels]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_Channels]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_Voltages]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_Voltages]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_Tsensors]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_Tsensors]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_MGroupID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_MGroupID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_IgnoreFlag]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_IgnoreFlag]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_IgnoreFlag1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_IgnoreFlag1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_TEC_Present]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_TEC_Present]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_Couple_Type]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_Couple_Type]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_APC_Type]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_APC_Type]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_BER(exp)]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_BER(exp)]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_MaxRate(G)]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_MaxRate(G)]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_Publish_PN]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_Publish_PN]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_NickName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_NickName]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_IbiasFormula]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_IbiasFormula]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_IbiasFormula1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_IbiasFormula1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_CurvingXParamters]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_CurvingXParamters]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionName_RxOverLoadDBm]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionName] DROP CONSTRAINT [DF_GlobalProductionName_RxOverLoadDBm]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalProductionName]') AND type in (N'U'))
DROP TABLE [dbo].[GlobalProductionName]
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalProductionType]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalProductionType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_GlobalProductionType]
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalSpecs]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalSpecs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_GlobalSpecs]
GO
/****** Object:  StoredProcedure [dbo].[Pro_RegisterFormula]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_RegisterFormula]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_RegisterFormula]
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalAllTestModelList]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalAllTestModelList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_GlobalAllTestModelList]
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalManufactureCoefficients]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalManufactureCoefficients]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_GlobalManufactureCoefficients]
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalManufactureCoefficientsGroup]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalManufactureCoefficientsGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_GlobalManufactureCoefficientsGroup]
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalMSA]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalMSA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_GlobalMSA]
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalMSADefintionInf]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalMSADefintionInf]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pro_GlobalMSADefintionInf]
GO
/****** Object:  Table [dbo].[BlockAscxInfo]    Script Date: 09/21/2015 15:16:26 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BlockAscxInfo_AscxFile]') AND parent_object_id = OBJECT_ID(N'[dbo].[BlockAscxInfo]'))
ALTER TABLE [dbo].[BlockAscxInfo] DROP CONSTRAINT [FK_BlockAscxInfo_AscxFile]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BlockAscxInfo_FunctionTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[BlockAscxInfo]'))
ALTER TABLE [dbo].[BlockAscxInfo] DROP CONSTRAINT [FK_BlockAscxInfo_FunctionTable]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BlockAscxInfo_FuncBlockID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BlockAscxInfo] DROP CONSTRAINT [DF_BlockAscxInfo_FuncBlockID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BlockAscxInfo_AscxID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BlockAscxInfo] DROP CONSTRAINT [DF_BlockAscxInfo_AscxID]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlockAscxInfo]') AND type in (N'U'))
DROP TABLE [dbo].[BlockAscxInfo]
GO
/****** Object:  Table [dbo].[RegisterFormula]    Script Date: 09/21/2015 15:16:30 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RegisterFormula_ChipBaseInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[RegisterFormula]'))
ALTER TABLE [dbo].[RegisterFormula] DROP CONSTRAINT [FK_RegisterFormula_ChipBaseInfo]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RegisterFormula_ItemName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RegisterFormula] DROP CONSTRAINT [DF_RegisterFormula_ItemName]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RegisterFormula_WriteFormula]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RegisterFormula] DROP CONSTRAINT [DF_RegisterFormula_WriteFormula]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RegisterFormula_AnalogueUnit]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RegisterFormula] DROP CONSTRAINT [DF_RegisterFormula_AnalogueUnit]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_WriteFormula1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RegisterFormula] DROP CONSTRAINT [DF_Table_1_WriteFormula1]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RegisterFormula]') AND type in (N'U'))
DROP TABLE [dbo].[RegisterFormula]
GO
/****** Object:  Table [dbo].[RoleFunctionTable]    Script Date: 09/21/2015 15:16:30 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleFunctionTable_FunctionTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleFunctionTable]'))
ALTER TABLE [dbo].[RoleFunctionTable] DROP CONSTRAINT [FK_RoleFunctionTable_FunctionTable]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleFunctionTable_RolesTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleFunctionTable]'))
ALTER TABLE [dbo].[RoleFunctionTable] DROP CONSTRAINT [FK_RoleFunctionTable_RolesTable]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__RoleFunct__RoleI__787EE5A0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoleFunctionTable] DROP CONSTRAINT [DF__RoleFunct__RoleI__787EE5A0]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__RoleFunct__Funct__797309D9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RoleFunctionTable] DROP CONSTRAINT [DF__RoleFunct__Funct__797309D9]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleFunctionTable]') AND type in (N'U'))
DROP TABLE [dbo].[RoleFunctionTable]
GO
/****** Object:  Table [dbo].[GlobalProductionType]    Script Date: 09/21/2015 15:16:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalProductionType_GlobalMSA]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalProductionType]'))
ALTER TABLE [dbo].[GlobalProductionType] DROP CONSTRAINT [FK_GlobalProductionType_GlobalMSA]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionType_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionType] DROP CONSTRAINT [DF_GlobalProductionType_Name]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionType_MSAID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionType] DROP CONSTRAINT [DF_GlobalProductionType_MSAID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalProductionType_IgnoreFlag]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalProductionType] DROP CONSTRAINT [DF_GlobalProductionType_IgnoreFlag]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalProductionType]') AND type in (N'U'))
DROP TABLE [dbo].[GlobalProductionType]
GO
/****** Object:  Table [dbo].[GlobalMSADefintionInf]    Script Date: 09/21/2015 15:16:29 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalMSADefintionInf_GlobalMSA]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalMSADefintionInf]'))
ALTER TABLE [dbo].[GlobalMSADefintionInf] DROP CONSTRAINT [FK_GlobalMSADefintionInf_GlobalMSA]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSADefintionInf_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalMSADefintionInf] DROP CONSTRAINT [DF_GlobalMSADefintionInf_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSADefintionInf_FieldName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalMSADefintionInf] DROP CONSTRAINT [DF_GlobalMSADefintionInf_FieldName]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSADefintionInf_Channel]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalMSADefintionInf] DROP CONSTRAINT [DF_GlobalMSADefintionInf_Channel]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSADefintionInf_SlaveAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalMSADefintionInf] DROP CONSTRAINT [DF_GlobalMSADefintionInf_SlaveAddress]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSADefintionInf_Page]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalMSADefintionInf] DROP CONSTRAINT [DF_GlobalMSADefintionInf_Page]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSADefintionInf_StartAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalMSADefintionInf] DROP CONSTRAINT [DF_GlobalMSADefintionInf_StartAddress]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSADefintionInf_Length]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalMSADefintionInf] DROP CONSTRAINT [DF_GlobalMSADefintionInf_Length]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSADefintionInf_Format]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalMSADefintionInf] DROP CONSTRAINT [DF_GlobalMSADefintionInf_Format]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalMSADefintionInf]') AND type in (N'U'))
DROP TABLE [dbo].[GlobalMSADefintionInf]
GO
/****** Object:  StoredProcedure [dbo].[InsertRunRecord]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertRunRecord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertRunRecord]
GO
/****** Object:  Table [dbo].[GlobalManufactureCoefficients]    Script Date: 09/21/2015 15:16:28 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalManufactureCoefficients]'))
ALTER TABLE [dbo].[GlobalManufactureCoefficients] DROP CONSTRAINT [FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureMemory_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureCoefficients] DROP CONSTRAINT [DF_GlobalManufactureMemory_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureMemory_TYPE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureCoefficients] DROP CONSTRAINT [DF_GlobalManufactureMemory_TYPE]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureMemory_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureCoefficients] DROP CONSTRAINT [DF_GlobalManufactureMemory_Name]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureMemory_Channel]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureCoefficients] DROP CONSTRAINT [DF_GlobalManufactureMemory_Channel]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureMemory_Page]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureCoefficients] DROP CONSTRAINT [DF_GlobalManufactureMemory_Page]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureMemory_StartAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureCoefficients] DROP CONSTRAINT [DF_GlobalManufactureMemory_StartAddress]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureMemory_Length]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureCoefficients] DROP CONSTRAINT [DF_GlobalManufactureMemory_Length]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureMemory_Format]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureCoefficients] DROP CONSTRAINT [DF_GlobalManufactureMemory_Format]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalManufactureCoefficients]') AND type in (N'U'))
DROP TABLE [dbo].[GlobalManufactureCoefficients]
GO
/****** Object:  Table [dbo].[GlobalAllTestModelList]    Script Date: 09/21/2015 15:16:27 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalAllTestModelList_GlobalAllAppModelList]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalAllTestModelList]'))
ALTER TABLE [dbo].[GlobalAllTestModelList] DROP CONSTRAINT [FK_GlobalAllTestModelList_GlobalAllAppModelList]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalAllTestModelList_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalAllTestModelList] DROP CONSTRAINT [DF_GlobalAllTestModelList_Name]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalAllTestModelList_ShowName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalAllTestModelList] DROP CONSTRAINT [DF_GlobalAllTestModelList_ShowName]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalAllTestModelList_Description]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalAllTestModelList] DROP CONSTRAINT [DF_GlobalAllTestModelList_Description]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalAllTestModelList]') AND type in (N'U'))
DROP TABLE [dbo].[GlobalAllTestModelList]
GO
/****** Object:  Table [dbo].[UserRoleTable]    Script Date: 09/21/2015 15:16:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoleTable_RolesTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoleTable]'))
ALTER TABLE [dbo].[UserRoleTable] DROP CONSTRAINT [FK_UserRoleTable_RolesTable]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoleTable_UserInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoleTable]'))
ALTER TABLE [dbo].[UserRoleTable] DROP CONSTRAINT [FK_UserRoleTable_UserInfo]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__UserRoleT__UserI__679450C0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserRoleTable] DROP CONSTRAINT [DF__UserRoleT__UserI__679450C0]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__UserRoleT__RoleI__688874F9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserRoleTable] DROP CONSTRAINT [DF__UserRoleT__RoleI__688874F9]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRoleTable]') AND type in (N'U'))
DROP TABLE [dbo].[UserRoleTable]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 09/21/2015 15:16:30 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserInfo_TrueName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [DF_UserInfo_TrueName]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserInfo_CreatTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [DF_UserInfo_CreatTime]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserInfo_lastComputerName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [DF_UserInfo_lastComputerName]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserInfo_lastLoginOffTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [DF_UserInfo_lastLoginOffTime]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserInfo_lastIP]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [DF_UserInfo_lastIP]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserInfo_Remarks]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [DF_UserInfo_Remarks]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserInfo]') AND type in (N'U'))
DROP TABLE [dbo].[UserInfo]
GO
/****** Object:  Table [dbo].[GlobalManufactureCoefficientsGroup]    Script Date: 09/21/2015 15:16:28 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureMemoryGroupTable_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureCoefficientsGroup] DROP CONSTRAINT [DF_GlobalManufactureMemoryGroupTable_Name]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureCoefficientsGroup_TypeID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureCoefficientsGroup] DROP CONSTRAINT [DF_GlobalManufactureCoefficientsGroup_TypeID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureCoefficientsGroup_ItemDescription]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureCoefficientsGroup] DROP CONSTRAINT [DF_GlobalManufactureCoefficientsGroup_ItemDescription]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalManufactureCoefficientsGroup_IgnoreFlag]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalManufactureCoefficientsGroup] DROP CONSTRAINT [DF_GlobalManufactureCoefficientsGroup_IgnoreFlag]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalManufactureCoefficientsGroup]') AND type in (N'U'))
DROP TABLE [dbo].[GlobalManufactureCoefficientsGroup]
GO
/****** Object:  Table [dbo].[GlobalMSA]    Script Date: 09/21/2015 15:16:28 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSA_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalMSA] DROP CONSTRAINT [DF_GlobalMSA_Name]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSA_AccessInterface]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalMSA] DROP CONSTRAINT [DF_GlobalMSA_AccessInterface]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSA_SlaveAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalMSA] DROP CONSTRAINT [DF_GlobalMSA_SlaveAddress]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalMSA_IgnoreFlag]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalMSA] DROP CONSTRAINT [DF_GlobalMSA_IgnoreFlag]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalMSA]') AND type in (N'U'))
DROP TABLE [dbo].[GlobalMSA]
GO
/****** Object:  UserDefinedFunction [dbo].[f_splitModelstr]    Script Date: 09/21/2015 15:16:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_splitModelstr]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_splitModelstr]
GO
/****** Object:  UserDefinedFunction [dbo].[f_splitstr]    Script Date: 09/21/2015 15:16:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_splitstr]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_splitstr]
GO
/****** Object:  Table [dbo].[FunctionTable]    Script Date: 09/21/2015 15:16:26 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FunctionTable_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FunctionTable] DROP CONSTRAINT [DF_FunctionTable_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FunctionTable_BlockLevel]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FunctionTable] DROP CONSTRAINT [DF_FunctionTable_BlockLevel]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FunctionTable_BlockTypeID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FunctionTable] DROP CONSTRAINT [DF_FunctionTable_BlockTypeID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FunctionTable_ItemName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FunctionTable] DROP CONSTRAINT [DF_FunctionTable_ItemName]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FunctionTable_AliasName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FunctionTable] DROP CONSTRAINT [DF_FunctionTable_AliasName]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__FunctionT__Title__5EFF0ABF]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FunctionTable] DROP CONSTRAINT [DF__FunctionT__Title__5EFF0ABF]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__FunctionT__Funct__5FF32EF8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FunctionTable] DROP CONSTRAINT [DF__FunctionT__Funct__5FF32EF8]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__FunctionT__Remar__60E75331]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FunctionTable] DROP CONSTRAINT [DF__FunctionT__Remar__60E75331]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FunctionTable]') AND type in (N'U'))
DROP TABLE [dbo].[FunctionTable]
GO
/****** Object:  StoredProcedure [dbo].[GetCurrServerTime]    Script Date: 09/21/2015 15:16:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCurrServerTime]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCurrServerTime]
GO
/****** Object:  Table [dbo].[GlobalAllAppModelList]    Script Date: 09/21/2015 15:16:26 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalAllAppModelList_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalAllAppModelList] DROP CONSTRAINT [DF_GlobalAllAppModelList_Name]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalAllAppModelList_Description]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalAllAppModelList] DROP CONSTRAINT [DF_GlobalAllAppModelList_Description]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalAllAppModelList]') AND type in (N'U'))
DROP TABLE [dbo].[GlobalAllAppModelList]
GO
/****** Object:  Table [dbo].[GlobalAllEquipmentList]    Script Date: 09/21/2015 15:16:27 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalAllEquipmentList_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalAllEquipmentList] DROP CONSTRAINT [DF_GlobalAllEquipmentList_Name]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalAllEquipmentList_ItemName1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalAllEquipmentList] DROP CONSTRAINT [DF_GlobalAllEquipmentList_ItemName1]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalAllEquipmentList_Type]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalAllEquipmentList] DROP CONSTRAINT [DF_GlobalAllEquipmentList_Type]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_GlobalAllEquipmentList_Description]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalAllEquipmentList] DROP CONSTRAINT [DF_GlobalAllEquipmentList_Description]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalAllEquipmentList]') AND type in (N'U'))
DROP TABLE [dbo].[GlobalAllEquipmentList]
GO
/****** Object:  Table [dbo].[AscxFile]    Script Date: 09/21/2015 15:16:26 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AscxFile_AscxFile]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AscxFile] DROP CONSTRAINT [DF_AscxFile_AscxFile]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AscxFile_Remark]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AscxFile] DROP CONSTRAINT [DF_AscxFile_Remark]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AscxFile]') AND type in (N'U'))
DROP TABLE [dbo].[AscxFile]
GO
/****** Object:  Table [dbo].[ChipBaseInfo]    Script Date: 09/21/2015 15:16:26 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_PNChipID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChipBaseInfo] DROP CONSTRAINT [DF_Table_1_PNChipID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ChipBaseInfo_Description]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChipBaseInfo] DROP CONSTRAINT [DF_ChipBaseInfo_Description]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ChipBaseInfo_BigEndian]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ChipBaseInfo] DROP CONSTRAINT [DF_ChipBaseInfo_BigEndian]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChipBaseInfo]') AND type in (N'U'))
DROP TABLE [dbo].[ChipBaseInfo]
GO
/****** Object:  Table [dbo].[GlobalSpecs]    Script Date: 09/21/2015 15:16:29 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_PID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalSpecs] DROP CONSTRAINT [DF_Table_1_PID]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_ModelName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalSpecs] DROP CONSTRAINT [DF_Table_1_ModelName]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table_1_ItemName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[GlobalSpecs] DROP CONSTRAINT [DF_Table_1_ItemName]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalSpecs]') AND type in (N'U'))
DROP TABLE [dbo].[GlobalSpecs]
GO
/****** Object:  Table [dbo].[RolesTable]    Script Date: 09/21/2015 15:16:30 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__RolesTabl__RoleN__5B2E79DB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RolesTable] DROP CONSTRAINT [DF__RolesTabl__RoleN__5B2E79DB]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__RolesTabl__Remar__5C229E14]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RolesTable] DROP CONSTRAINT [DF__RolesTabl__Remar__5C229E14]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolesTable]') AND type in (N'U'))
DROP TABLE [dbo].[RolesTable]
GO
/****** Object:  User [ATSUser]    Script Date: 09/21/2015 15:16:32 ******/
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'ATSUser')
DROP USER [ATSUser]
GO
/****** Object:  User [BackGround]    Script Date: 09/21/2015 15:16:32 ******/
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'BackGround')
DROP USER [BackGround]
GO
/****** Object:  User [maintainUser]    Script Date: 09/21/2015 15:16:32 ******/
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'maintainUser')
DROP USER [maintainUser]
GO
/****** Object:  User [RDBackGround]    Script Date: 09/21/2015 15:16:32 ******/
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'RDBackGround')
DROP USER [RDBackGround]
GO
/****** Object:  User [RDMaintain]    Script Date: 09/21/2015 15:16:32 ******/
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'RDMaintain')
DROP USER [RDMaintain]
GO
/****** Object:  User [RDUser]    Script Date: 09/21/2015 15:16:32 ******/
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'RDUser')
DROP USER [RDUser]
GO
/****** Object:  User [ATSUser]    Script Date: 09/21/2015 15:16:32 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'ATSUser')
CREATE USER [ATSUser] FOR LOGIN [ATSUser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [BackGround]    Script Date: 09/21/2015 15:16:32 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'BackGround')
CREATE USER [BackGround] FOR LOGIN [ATSBackGround] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [maintainUser]    Script Date: 09/21/2015 15:16:32 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'maintainUser')
CREATE USER [maintainUser] FOR LOGIN [ATSMaintainUser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [RDBackGround]    Script Date: 09/21/2015 15:16:32 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'RDBackGround')
CREATE USER [RDBackGround] FOR LOGIN [RDBackGround] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [RDMaintain]    Script Date: 09/21/2015 15:16:32 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'RDMaintain')
CREATE USER [RDMaintain] FOR LOGIN [RDMaintain] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [RDUser]    Script Date: 09/21/2015 15:16:32 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'RDUser')
CREATE USER [RDUser] FOR LOGIN [RDUser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[RolesTable]    Script Date: 09/21/2015 15:16:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolesTable]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RolesTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](25) NOT NULL CONSTRAINT [DF__RolesTabl__RoleN__5B2E79DB]  DEFAULT (''),
	[Remarks] [nvarchar](25) NOT NULL CONSTRAINT [DF__RolesTabl__Remar__5C229E14]  DEFAULT (''),
 CONSTRAINT [PK_RolesTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_RolesTable] UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[RolesTable] ON
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (1, N'TestEngineer', N'测试工程师')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (2, N'OP', N'普通操作者')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (3, N'Admin', N'管理员')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (4, N'Guest', N'访客用户')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (5, N'DBOwner', N'数据库拥有者')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (6, N'ReadATSPlan', N'')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (7, N'ReadProductionInfo', N'')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (8, N'MSAInfo', N'')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (9, N'MCoefGroup', N'')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (10, N'AppModel', N'')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (11, N'Equipment', N'')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (12, N'TestData', N'')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (13, N'GlobalSpecs', N'')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (14, N'EditAll', N'')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (15, N'DeleteAll', N'')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (16, N'AddedAll', N'')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (17, N'ReadAll', N'')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (19, N'Edit&AddPN', N'')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (20, N'ADDPN', N'')
INSERT [dbo].[RolesTable] ([ID], [RoleName], [Remarks]) VALUES (21, N'EditPN', N'')
SET IDENTITY_INSERT [dbo].[RolesTable] OFF
/****** Object:  Table [dbo].[GlobalSpecs]    Script Date: 09/21/2015 15:16:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalSpecs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GlobalSpecs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](100) NOT NULL CONSTRAINT [DF_Table_1_PID]  DEFAULT ((0)),
	[Unit] [nvarchar](50) NOT NULL CONSTRAINT [DF_Table_1_ModelName]  DEFAULT (''),
	[ItemDescription] [nvarchar](4000) NOT NULL CONSTRAINT [DF_Table_1_ItemName]  DEFAULT (''),
 CONSTRAINT [PK_GlobalSpecifics] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalSpecs] UNIQUE NONCLUSTERED 
(
	[ItemName] ASC,
	[Unit] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[GlobalSpecs] ON
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (1, N'AP', N'dBm', N'AP(dBm)X')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (2, N'ER', N'dB', N'ER(dB)')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (3, N'TxOMA', N'dBm', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (4, N'Crossing', N'%', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (5, N'IBias', N'mA', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (6, N'IMod', N'mA', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (7, N'MaskMargin', N'%', N'MaskMargin(%)')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (8, N'RiseTime', N'ps', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (9, N'JitterRMS', N'ps', N'JitterRMS(ps)')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (10, N'JitterPP', N'ps', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (11, N'Icc', N'mA', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (12, N'Csen', N'dBm', N'Csen(dBm)')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (13, N'CSenOMA', N'dBm', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (14, N'LosA', N'dBm', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (15, N'LosD', N'dBm', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (16, N'LosH', N'dBm', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (17, N'LOSA_OMA', N'dBm', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (18, N'LOSD_OMA', N'dBm', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (19, N'DmiVcc', N'V', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (20, N'DmiVccErr', N'V', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (21, N'DmiTemp', N'C', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (22, N'DmiTempErr', N'C', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (23, N'DmiTxPWR', N'dBm', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (24, N'DmiTxPOWERErr', N'dB', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (26, N'DmiRxPWRErr', N'dBm', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (27, N'DmiRxNOptical', N'dBm', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (28, N'DmiRxPWRMaxErrPoint', N'dBm', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (29, N'EECrossing', N'%', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (30, N'EEMaskMargin', N'%', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (31, N'EERiseTime', N'ps', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (32, N'EEFallTime', N'ps', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (33, N'FallTime', N'ps', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (34, N'EETXAmp', N'mV', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (35, N'EEJitterPP', N'ps', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (36, N'EEEyeHight', N'mV', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (37, N'EEEyeWidth', N'ps', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (38, N'EEJitterRMS', N'ps', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (42, N'TEMPHA	', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (43, N'TEMPHW', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (44, N'TEMPLA', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (45, N'TEMPLW', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (46, N'VCCHA', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (47, N'VCCHW', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (48, N'VCCLA', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (49, N'VCCLW', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (50, N'IBIASHA', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (51, N'IBIASHW', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (52, N'IBIASLA', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (53, N'IBIASLW', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (54, N'TXPOWERHA', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (55, N'TXPOWERHW', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (56, N'TXPOWERLA', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (57, N'TXPOWERLW', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (58, N'RXPOWERHA', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (59, N'RXPOWERHW', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (60, N'RXPOWERLA', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (61, N'RXPOWERLW', N'', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (62, N'TxDisablePower', N'dbm', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (75, N'DCATxPower', N'dBm', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (77, N'TXOMA', N'mw', N'')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (81, N'IBiasDMI', N'MA', N'IBias监控值')
INSERT [dbo].[GlobalSpecs] ([ID], [ItemName], [Unit], [ItemDescription]) VALUES (87, N'Tr_ErrorRate', N'', N'传输测试的误码率')
SET IDENTITY_INSERT [dbo].[GlobalSpecs] OFF
/****** Object:  Table [dbo].[ChipBaseInfo]    Script Date: 09/21/2015 15:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChipBaseInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ChipBaseInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](50) NOT NULL CONSTRAINT [DF_Table_1_PNChipID]  DEFAULT (''),
	[Channels] [tinyint] NOT NULL,
	[Description] [nvarchar](500) NOT NULL CONSTRAINT [DF_ChipBaseInfo_Description]  DEFAULT (''),
	[Width] [tinyint] NOT NULL,
	[LittleEndian] [bit] NOT NULL CONSTRAINT [DF_ChipBaseInfo_BigEndian]  DEFAULT ('false'),
 CONSTRAINT [PK_ChipBaseInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_ChipBaseInfo] UNIQUE NONCLUSTERED 
(
	[ItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'ChipBaseInfo', N'COLUMN',N'LittleEndian'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'大字节序:false;小字节序:true;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ChipBaseInfo', @level2type=N'COLUMN',@level2name=N'LittleEndian'
GO
SET IDENTITY_INSERT [dbo].[ChipBaseInfo] ON
INSERT [dbo].[ChipBaseInfo] ([ID], [ItemName], [Channels], [Description], [Width], [LittleEndian]) VALUES (1, N'GN115', 4, N'****************', 1, 0)
SET IDENTITY_INSERT [dbo].[ChipBaseInfo] OFF
/****** Object:  Table [dbo].[AscxFile]    Script Date: 09/21/2015 15:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AscxFile]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AscxFile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AscxFileName] [nvarchar](max) NOT NULL CONSTRAINT [DF_AscxFile_AscxFile]  DEFAULT (''),
	[Remarks] [nvarchar](max) NOT NULL CONSTRAINT [DF_AscxFile_Remark]  DEFAULT (''),
 CONSTRAINT [PK_AscxFile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[AscxFile] ON
INSERT [dbo].[AscxFile] ([ID], [AscxFileName], [Remarks]) VALUES (1, N'X1', N'2')
INSERT [dbo].[AscxFile] ([ID], [AscxFileName], [Remarks]) VALUES (2, N'X3', N'X333')
INSERT [dbo].[AscxFile] ([ID], [AscxFileName], [Remarks]) VALUES (3, N'x333', N'x4')
SET IDENTITY_INSERT [dbo].[AscxFile] OFF
/****** Object:  Table [dbo].[GlobalAllEquipmentList]    Script Date: 09/21/2015 15:16:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalAllEquipmentList]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GlobalAllEquipmentList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL CONSTRAINT [DF_GlobalAllEquipmentList_Name]  DEFAULT (''),
	[ShowName] [nvarchar](30) NOT NULL CONSTRAINT [DF_GlobalAllEquipmentList_ItemName1]  DEFAULT (''),
	[ItemType] [nvarchar](30) NOT NULL CONSTRAINT [DF_GlobalAllEquipmentList_Type]  DEFAULT (''),
	[ItemDescription] [nvarchar](50) NOT NULL CONSTRAINT [DF_GlobalAllEquipmentList_Description]  DEFAULT (''),
 CONSTRAINT [PK_GlobalAllEquipmentList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalAllEquipmentList] UNIQUE NONCLUSTERED 
(
	[ItemName] ASC,
	[ItemType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalAllEquipmentList_ShowNameUnique] UNIQUE NONCLUSTERED 
(
	[ShowName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[GlobalAllEquipmentList] ON
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (1, N'E3631', N'电源E3631', N'PowerSupply', N'电源!')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (2, N'AQ2211Atten', N'衰减器AQ2211', N'Attennuator', N'衰减器AQ2211')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (3, N'D86100', N'示波器86100', N'Scope', N'示波器')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (4, N'ElectricalSwitch', N'电开关', N'ElecSwitch', N'电开关')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (5, N'N490XPPG', N'误码仪N490X_PPG', N'PPG', N'误码仪 PPG')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (6, N'N490XED', N'误码仪N490X_ED', N'ErrorDetector', N'误码仪 ED')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (7, N'AQ2211OpticalSwitch', N'光开关AQ2211', N'OpticalSwitch', N'光开关')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (8, N'AQ2211PowerMeter', N'光功率计AQ2211', N'PowerMeter', N'功率计')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (9, N'TPO4300', N'热流仪', N'Thermocontroller', N'热流仪')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (10, N'FLEX86100', N'示波器FLEX86100', N'Scope', N'示波器FLEX86100')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (11, N'MP1800PPG', N'误码仪MP1800_ PPG', N'PPG', N'误码仪 PPG')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (12, N'MP1800ED', N'误码仪MP1800_ED', N'ErrorDetector', N'误码仪 ED')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (13, N'TestEQ', N'软件人员Demo程序', N'TestBackGround', N'TestEQ')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (14, N'MAP200Atten', N'衰减器MAP200', N'Attennuator', N'衰减器')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (15, N'MAP200OpticalSwitch', N'光开关MAP200', N'OpticalSwitch', N'光开关')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (25, N'Inno25GBertPPG', N'误码仪Inno25GBert_PPG', N'PPG', N'误码仪PPG')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (26, N'Inno25GBertED', N'误码仪Inno25GBert_ED', N'ErrorDetector', N'误码仪ED')
INSERT [dbo].[GlobalAllEquipmentList] ([ID], [ItemName], [ShowName], [ItemType], [ItemDescription]) VALUES (28, N'NewEqName', N'0702NewShowName', N'NewEqType', N'')
SET IDENTITY_INSERT [dbo].[GlobalAllEquipmentList] OFF
/****** Object:  Table [dbo].[GlobalAllAppModelList]    Script Date: 09/21/2015 15:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalAllAppModelList]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GlobalAllAppModelList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL CONSTRAINT [DF_GlobalAllAppModelList_Name]  DEFAULT (''),
	[ItemDescription] [nvarchar](50) NOT NULL CONSTRAINT [DF_GlobalAllAppModelList_Description]  DEFAULT (''),
 CONSTRAINT [PK_GlobalAllAppModelList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalAllAppModelList] UNIQUE NONCLUSTERED 
(
	[ItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[GlobalAllAppModelList] ON
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (1, N'APPTXCAL', N'TXCalibration')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (2, N'APPTXFMT', N'TXFinalModuleTest!')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (3, N'APPRXCAL', N'RXCalibration')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (4, N'APPRXFMT', N'RXFinalModuleTest')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (5, N'APPDUTCAL', N'DUTMonitorCalibration')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (6, N'APPDUTFMT', N'DUTFinalModuleTest')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (7, N'APPEDVT', N'EDVTProcess')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (8, N'APPEEPROM', N'Checkeeprom&AlarmWarning')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (9, N'APPDUTPrepare', N'ReadDUTInformationandInitializeRegister')
INSERT [dbo].[GlobalAllAppModelList] ([ID], [ItemName], [ItemDescription]) VALUES (10, N'AppModelTest', N'terry Test!!!')
SET IDENTITY_INSERT [dbo].[GlobalAllAppModelList] OFF
/****** Object:  StoredProcedure [dbo].[GetCurrServerTime]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCurrServerTime]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'--存储过程 GetCurrServerTime
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
' 
END
GO
/****** Object:  Table [dbo].[FunctionTable]    Script Date: 09/21/2015 15:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FunctionTable]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FunctionTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_FunctionTable_PID]  DEFAULT ((0)),
	[BlockLevel] [tinyint] NOT NULL CONSTRAINT [DF_FunctionTable_BlockLevel]  DEFAULT ((0)),
	[BlockTypeID] [int] NOT NULL CONSTRAINT [DF_FunctionTable_BlockTypeID]  DEFAULT ((0)),
	[ItemName] [nvarchar](50) NOT NULL CONSTRAINT [DF_FunctionTable_ItemName]  DEFAULT (''),
	[AliasName] [nvarchar](50) NOT NULL CONSTRAINT [DF_FunctionTable_AliasName]  DEFAULT (''),
	[Title] [nvarchar](50) NOT NULL CONSTRAINT [DF__FunctionT__Title__5EFF0ABF]  DEFAULT (''),
	[FunctionCode] [int] NOT NULL CONSTRAINT [DF__FunctionT__Funct__5FF32EF8]  DEFAULT ('0'),
	[Remarks] [nvarchar](100) NOT NULL CONSTRAINT [DF__FunctionT__Remar__60E75331]  DEFAULT (''),
 CONSTRAINT [PK_FunctionTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[FunctionTable] ON
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (1, 0, 0, 1, N'ATSPlan', N'维护测试计划', N'TestPlanReadable', 1, N'TestPlan查询')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (2, 0, 0, 1, N'ATSPlan', N'维护测试计划', N'TestPlanEditable', 2, N'TestPlan编辑')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (3, 0, 0, 1, N'ATSPlan', N'维护测试计划', N'TestPlanAddable', 4, N'TestPlan新增')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (4, 0, 0, 1, N'ATSPlan', N'维护测试计划', N'TestPlanDeletable', 8, N'TestPlan删除')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (5, 0, 0, 2, N'ProductionInfo', N'', N'PNInfoReadable', 16, N'读取产品信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (6, 0, 0, 2, N'ProductionInfo', N'', N'PNInfoEditable', 32, N'修改产品信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (7, 0, 0, 2, N'ProductionInfo', N'', N'PNInfoAddable', 64, N'产品信息新增')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (8, 0, 0, 2, N'ProductionInfo', N'', N'PNInfoDeleteable', 128, N'产品信息删除')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (9, 0, 0, 3, N'MSAInfo', N'', N'MSAInfoReadable', 256, N'读取MSA信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (10, 0, 0, 3, N'MSAInfo', N'', N'MSAEditable', 512, N'编辑MSA信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (12, 0, 0, 3, N'MSAInfo', N'', N'MSAInfoAddable', 1024, N'新增MSA信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (13, 0, 0, 3, N'MSAInfo', N'', N'MSAInfoDeleteable', 2048, N'删除MSA信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (14, 0, 0, 4, N'MCoefGroup', N'', N'MCoefReadable', 4096, N'读取MCoef信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (15, 0, 0, 4, N'MCoefGroup', N'', N'MCoefEditable', 8192, N'修改MCoef信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (16, 0, 0, 4, N'MCoefGroup', N'', N'MCoefAddable', 16384, N'新增MCoef信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (17, 0, 0, 4, N'MCoefGroup', N'', N'MCoefDeleteable', 32768, N'删除MCoef信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (18, 0, 0, 5, N'AppModel', N'', N'APPModelReadable', 65536, N'读取GlobalModel信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (19, 0, 0, 5, N'AppModel', N'', N'APPModelEditable', 131072, N'修改Model信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (20, 0, 0, 5, N'AppModel', N'', N'APPModelAddable', 262144, N'新增Model')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (21, 0, 0, 5, N'AppModel', N'', N'APPModelDeleteable', 524288, N'删除Model信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (22, 0, 0, 6, N'Equipment', N'', N'EquipReadable', 1048576, N'读取设备信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (23, 0, 0, 6, N'Equipment', N'', N'EquipEditable', 2097152, N'修改设备信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (24, 0, 0, 6, N'Equipment', N'', N'EquipAddable', 4194304, N'新增设备信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (25, 0, 0, 6, N'Equipment', N'', N'EquipDeleteable', 8388608, N'删除设备信息')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (26, 0, 0, 7, N'TestData', N'', N'TestDataReadable', 16777216, N'查看数据结果')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (27, 0, 0, 8, N'UserRoleFunction', N'', N'DBAccessCtrl', 1073741824, N'数据库管理权限')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (28, 0, 0, 8, N'UserRoleFunction', N'', N'ViewOPlogs', 33554432, N'查看操作日志')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (32, 27, 1, 8, N'ManagementList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (33, 32, 2, 8, N'UserList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (34, 32, 2, 8, N'RoleList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (35, 32, 2, 8, N'FuncBlockList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (36, 32, 2, 8, N'AscxFileList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (37, 1, 1, 1, N'TypeList', N'机种类型', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (38, 37, 2, 1, N'PNList', N'机种名称', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (39, 38, 3, 1, N'TopoTestPlanList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (40, 39, 4, 1, N'FlowCtrlList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (41, 39, 4, 1, N'EquipmentList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (42, 40, 5, 1, N'TestModelList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (43, 39, 4, 1, N'PlanSelfInfo', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (44, 40, 5, 1, N'CtrlSelfInfo', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (45, 39, 5, 1, N'EquipmentInfo', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (46, 42, 6, 1, N'TestModelInfo', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (47, 46, 7, 1, N'ModelParamsInfo', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (53, 0, 0, 10, N'ChipINfor', N'芯片信息', N'ChipRead', 268435456, N'只读')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (54, 5, 1, 2, N'ProductionTypeList', N'产品信息类型列表', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (55, 54, 2, 2, N'ProductionPNList', N'产品信息PN列表', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (56, 54, 2, 2, N'ProductionTypeInfor', N'产品信息类型信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (57, 55, 3, 2, N'ProductionPNInfor', N'产品PN信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (58, 55, 3, 2, N'ChipsetControlList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (59, 55, 3, 2, N'ChipsetIniList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (60, 58, 4, 2, N'ChipsetControlInfor', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (61, 57, 4, 2, N'ChipsetINIInfor', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (62, 9, 1, 3, N'MSATypeList', N'MSA模块类型', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (63, 62, 2, 3, N'MSAInfor', N'MSA模块类型信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (64, 62, 2, 3, N'GlobalMSADefList', N'MSA定义列表', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (65, 63, 3, 3, N'GlobalMSADefInfor', N'MSA定义信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (66, 14, 1, 4, N'MCoefTypeList', N'机种类型', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (67, 66, 2, 4, N'MCoefList', N'系数组别列表', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (68, 67, 3, 4, N'MCoefInfo', N'系数组别自身资料', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (69, 67, 3, 4, N'MCoefParamsList', N'参数资料列表', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (70, 69, 4, 4, N'MCoefParamInfo', N'当前参数资料', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (71, 18, 1, 5, N'ManageModelList', N'公共测试模型列表', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (72, 71, 2, 5, N'GlobalModelInfo', N'测试模型自身信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (73, 71, 2, 5, N'ModelParamsList', N'查看模型参数列表信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (74, 73, 3, 5, N'ModelParamInfo', N'参数信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (75, 22, 1, 6, N'ManageEquipmentList', N'公共设备列表', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (76, 75, 2, 6, N'GlobalEquipInfo', N'公共设备信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (77, 75, 2, 6, N'EquipParamsList', N'设备参数列表', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (78, 76, 3, 6, N'EquipParamInfo', N'当前选择的参数信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (79, 33, 3, 8, N'UserInfo', N'用户信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (80, 34, 3, 8, N'RoleInfo', N'角色信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (81, 35, 3, 8, N'FuncBlockInfo', N'功能块信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (82, 36, 3, 8, N'AscxFileInfo', N'用户控件信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (83, 33, 3, 8, N'UserRoleInfo', N'用户角色信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (84, 34, 3, 8, N'RoleFuncInfo', N'角色功能信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (85, 35, 3, 8, N'BlockAscxInfo', N'功能块模板信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (86, 53, 1, 9, N'ManageGlobalSpecsList', N'产品的参数信息列表', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (87, 86, 2, 9, N'GlobalSpecsInfo', N'当前选择的产品参数信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (88, 39, 4, 1, N'TopPNSpecList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (89, 88, 5, 1, N'TopPNSpecInfor', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (90, 55, 3, 2, N'TopPNSpecList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (91, 90, 4, 2, N'TopPNSpecInfor', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (94, 39, 4, 1, N'MConfigInitList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (95, 94, 5, 1, N'MConfigSelfInfo', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (104, 0, 0, 10, N'ChipINfor', N'芯片信息', N'ChipEdit', 536870912, N'编辑')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (107, 55, 3, 2, N'TopE2ROMList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (108, 107, 4, 2, N'TopE2ROMInfor', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (109, 107, 4, 2, N'E2ROMData0Infor', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (110, 107, 4, 2, N'E2ROMData1Infor', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (111, 107, 4, 2, N'E2ROMData2Infor', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (112, 107, 4, 2, N'E2ROMData3Infor', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (114, 26, 2, 7, N'Query', N'查询数据', N'', 1, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (115, 26, 2, 7, N'Report', N'报表', N'', 1, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (116, 26, 2, 7, N'UserOPLogs', N'操作记录', N'', 1, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (117, 26, 1, 7, N'FunctionList', N'查询列表', N'', 1, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (118, 114, 3, 7, N'BackCoef', N'', N'', 1, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (119, 114, 3, 7, N'TestControl', N'', N'', 1, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (120, 119, 4, 7, N'DetailTestDataFMT', N'', N'', 1, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (121, 119, 4, 7, N'DetailTestDataLP', N'', N'', 1, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (122, 116, 3, 7, N'OPLogsInfor', N'', N'', 1, N'')
GO
print 'Processed 100 total records'
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (123, 122, 4, 7, N'OPLogsDetail', N'', N'', 1, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (124, 0, 0, 9, N'GlobalSpecs', N'产预设的品参数', N'GlobalSpecsRead', 67108864, N'操作公共产品参数配置')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (125, 0, 0, 9, N'GlobalSpecs', N'产预设的品参数', N'GlobalSpecsEdit', 134217728, N'操作公共产品参数配置')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (127, 33, 3, 8, N'UserActionInfo', N'用户PN、TestPlan权限', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (128, 53, 1, 10, N'ChipList', N'芯片信息列表', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (129, 53, 2, 10, N'ChipSelfINfor', N'芯片信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (130, 53, 2, 10, N'ChipFormulaList', N'芯片公式', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (131, 130, 3, 10, N'FormulaInfor', N'公式信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (132, 130, 3, 10, N'RegisterList', N'寄存器列表', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (133, 132, 4, 10, N'RegisterInfor', N'寄存器信息', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (134, 55, 3, 2, N'PNChipList', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (135, 134, 4, 2, N'PNChipSelfInfor', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (136, 134, 4, 2, N'PNChannelMap', N'', N'', 0, N'')
INSERT [dbo].[FunctionTable] ([ID], [PID], [BlockLevel], [BlockTypeID], [ItemName], [AliasName], [Title], [FunctionCode], [Remarks]) VALUES (137, 136, 5, 2, N'ChannelMapInfor', N'', N'', 0, N'')
SET IDENTITY_INSERT [dbo].[FunctionTable] OFF
/****** Object:  UserDefinedFunction [dbo].[f_splitstr]    Script Date: 09/21/2015 15:16:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_splitstr]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N' CREATE FUNCTION [dbo].[f_splitstr](
    @str nvarchar(max),@splitChar char(1)
    )RETURNS @r TABLE(id int IDENTITY(1, 1), value nvarchar(max))
    AS
    BEGIN
    /* Function body */
    DECLARE @pos int
    SET @pos = CHARINDEX(@splitChar, @str)
    WHILE @pos > 0
    BEGIN
    INSERT @r(value) VALUES(LEFT(@str, @pos - 1))
    SELECT
    @str = STUFF(@str, 1, @pos, ''''),
    @pos = CHARINDEX(@splitChar, @str)
    END
    IF @str > ''''
    INSERT @r(value) VALUES(@str)
    RETURN
    END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[f_splitModelstr]    Script Date: 09/21/2015 15:16:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_splitModelstr]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
 CREATE FUNCTION [dbo].[f_splitModelstr](@PID int,
    @str nvarchar(max),@splitChar char(1),@splitItemChar char(1)
    )RETURNS @r TABLE(PID int,GID int, value nvarchar(max))
    AS
    BEGIN
    /* Function body */
    DECLARE @pos int,@MyFID int,@myValue nvarchar(max),@posItem int,@tempStr nvarchar(max)
    SET @pos = CHARINDEX(@splitChar, @str)
    WHILE @pos > 0
		BEGIN
			SET @posItem= CHARINDEX(@splitItemChar, @str)
			SET @tempStr =LEFT(@str, @pos - 1)
			DECLARE @length int
			set @length=len(@tempStr)
			INSERT @r(PID,GID,value) VALUES(@PID,LEFT(@tempStr, @posItem - 1),right(@tempStr, @length-(@posItem)))
			SELECT
			@str = STUFF(@str, 1, @pos, ''''),
			@pos = CHARINDEX(@splitChar, @str)
		END
    IF @str > ''''
    INSERT @r(value) VALUES(@str)
    RETURN
    END
' 
END
GO
/****** Object:  Table [dbo].[GlobalMSA]    Script Date: 09/21/2015 15:16:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalMSA]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GlobalMSA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](25) NOT NULL CONSTRAINT [DF_GlobalMSA_Name]  DEFAULT (''),
	[AccessInterface] [nvarchar](25) NOT NULL CONSTRAINT [DF_GlobalMSA_AccessInterface]  DEFAULT (''),
	[SlaveAddress] [int] NOT NULL CONSTRAINT [DF_GlobalMSA_SlaveAddress]  DEFAULT ((0)),
	[IgnoreFlag] [bit] NOT NULL CONSTRAINT [DF_GlobalMSA_IgnoreFlag]  DEFAULT ('false'),
 CONSTRAINT [PK_GlobalMSA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalMSA] UNIQUE NONCLUSTERED 
(
	[ItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[GlobalMSA] ON
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress], [IgnoreFlag]) VALUES (1, N'SFF8636', N'I2C', 160, 0)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress], [IgnoreFlag]) VALUES (2, N'CFP4MSA', N'MDIO', 32768, 0)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress], [IgnoreFlag]) VALUES (3, N'SFF8472', N'I2C', 162, 0)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress], [IgnoreFlag]) VALUES (4, N'SFF8077i', N'I2C', 160, 0)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress], [IgnoreFlag]) VALUES (5, N'XXFFFF', N'I2C', 160, 0)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress], [IgnoreFlag]) VALUES (6, N'XXXffff', N'test', 128, 0)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress], [IgnoreFlag]) VALUES (7, N'q', N'q', 1, 0)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress], [IgnoreFlag]) VALUES (8, N'w', N'1', 1, 0)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress], [IgnoreFlag]) VALUES (9, N'qw', N'1', 1, 1)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress], [IgnoreFlag]) VALUES (11, N'Q1', N'1', 1, 0)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress], [IgnoreFlag]) VALUES (12, N'0601', N'1', 1, 1)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress], [IgnoreFlag]) VALUES (16, N'1234', N'2', 2, 0)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress], [IgnoreFlag]) VALUES (17, N'asd', N'1', 1, 1)
INSERT [dbo].[GlobalMSA] ([ID], [ItemName], [AccessInterface], [SlaveAddress], [IgnoreFlag]) VALUES (21, N'12222', N'1', 1, 1)
SET IDENTITY_INSERT [dbo].[GlobalMSA] OFF
/****** Object:  Table [dbo].[GlobalManufactureCoefficientsGroup]    Script Date: 09/21/2015 15:16:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalManufactureCoefficientsGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GlobalManufactureCoefficientsGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL CONSTRAINT [DF_GlobalManufactureMemoryGroupTable_Name]  DEFAULT (''),
	[TypeID] [int] NOT NULL CONSTRAINT [DF_GlobalManufactureCoefficientsGroup_TypeID]  DEFAULT ((0)),
	[ItemDescription] [nvarchar](200) NOT NULL CONSTRAINT [DF_GlobalManufactureCoefficientsGroup_ItemDescription]  DEFAULT (''),
	[IgnoreFlag] [bit] NOT NULL CONSTRAINT [DF_GlobalManufactureCoefficientsGroup_IgnoreFlag]  DEFAULT ('false'),
 CONSTRAINT [PK_GlobalManufactureMemoryGroupTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalManufactureCoefficientsGroup] UNIQUE NONCLUSTERED 
(
	[ItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[GlobalManufactureCoefficientsGroup] ON
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName], [TypeID], [ItemDescription], [IgnoreFlag]) VALUES (1, N'QFSP_LR', 1, N'', 0)
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName], [TypeID], [ItemDescription], [IgnoreFlag]) VALUES (4, N'CGR4', 1, N'', 0)
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName], [TypeID], [ItemDescription], [IgnoreFlag]) VALUES (6, N'CSR4', 1, N'', 0)
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName], [TypeID], [ItemDescription], [IgnoreFlag]) VALUES (7, N'SFP28_SR', 3, N'', 0)
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName], [TypeID], [ItemDescription], [IgnoreFlag]) VALUES (8, N'CFP4_0407', 2, N'', 0)
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName], [TypeID], [ItemDescription], [IgnoreFlag]) VALUES (10, N'NewName', 7, N'Description', 0)
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName], [TypeID], [ItemDescription], [IgnoreFlag]) VALUES (42, N'New11Name', 9, N'Description', 0)
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName], [TypeID], [ItemDescription], [IgnoreFlag]) VALUES (50, N'NewName45', 2, N'Description', 0)
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName], [TypeID], [ItemDescription], [IgnoreFlag]) VALUES (58, N'SFP_LR', 3, N'SFPLRMCoef', 0)
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName], [TypeID], [ItemDescription], [IgnoreFlag]) VALUES (59, N'Tr_LR4', 1, N'Description', 0)
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName], [TypeID], [ItemDescription], [IgnoreFlag]) VALUES (61, N'NewqName', 1, N'Description', 1)
INSERT [dbo].[GlobalManufactureCoefficientsGroup] ([ID], [ItemName], [TypeID], [ItemDescription], [IgnoreFlag]) VALUES (63, N'New', 2, N'Description', 1)
SET IDENTITY_INSERT [dbo].[GlobalManufactureCoefficientsGroup] OFF
/****** Object:  Table [dbo].[UserInfo]    Script Date: 09/21/2015 15:16:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LoginName] [nvarchar](50) NOT NULL,
	[LoginPassword] [nvarchar](50) NOT NULL,
	[TrueName] [nvarchar](20) NOT NULL CONSTRAINT [DF_UserInfo_TrueName]  DEFAULT (''),
	[lastLoginTime] [datetime] NOT NULL CONSTRAINT [DF_UserInfo_CreatTime]  DEFAULT (getdate()),
	[lastComputerName] [nvarchar](50) NOT NULL CONSTRAINT [DF_UserInfo_lastComputerName]  DEFAULT (''),
	[lastLogOffTime] [datetime] NOT NULL CONSTRAINT [DF_UserInfo_lastLoginOffTime]  DEFAULT ('2000/1/1 12:00:00'),
	[lastIP] [nvarchar](20) NOT NULL CONSTRAINT [DF_UserInfo_lastIP]  DEFAULT ('0.0.0.0'),
	[Remarks] [nvarchar](50) NULL CONSTRAINT [DF_UserInfo_Remarks]  DEFAULT (''),
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_UserInfo] UNIQUE NONCLUSTERED 
(
	[LoginName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[UserInfo] ON
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (2, N'terry.yin', N'terry.se', N'ysz', CAST(0x0000A3FE00A12584 AS DateTime), N'INPCSZ0228', CAST(0x0000A3FE00A12EE4 AS DateTime), N'10.160.80.46', N'测试12')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (5, N'Leo', N'leo.se', N'陈江鹏', CAST(0x0000A3D400DCFB5B AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'测试00')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (7, N'Admin', N'www.innolight.com', N'BackGroundOwner', CAST(0x0000A3F800F16918 AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'不能被删除')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (8, N'ATSPlan', N'111111', N'x', CAST(0x0000A4A400BC474F AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (9, N'ProductionInfo', N'111111', N'x', CAST(0x0000A4A400EBDEA3 AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (10, N'MSAInfo', N'111111', N'x', CAST(0x0000A4A400ECE064 AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (11, N'Read', N'111111', N'ReadOnly', CAST(0x0000A4A400ED0280 AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (12, N'Add', N'111111', N'x', CAST(0x0000A4A400EE43E7 AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (13, N'Edit', N'111111', N'', CAST(0x0000A4A400EE5F22 AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (14, N'delete', N'111111', N'x', CAST(0x0000A4A400EE6ED8 AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (17, N'guest', N'111111', N'', CAST(0x0000A4AE011B9DBA AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (18, N'LabTest', N'111111', N'', CAST(0x0000A4CE00AB18A9 AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (23, N'addPN', N'111111', N'', CAST(0x0000A4EA0104A798 AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (24, N'edit&addPN', N'111111', N'', CAST(0x0000A4EB00B744A8 AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (25, N'editPN', N'111111', N'', CAST(0x0000A4EB00B904BF AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'')
INSERT [dbo].[UserInfo] ([ID], [LoginName], [LoginPassword], [TrueName], [lastLoginTime], [lastComputerName], [lastLogOffTime], [lastIP], [Remarks]) VALUES (26, N'testRead', N'111111', N'', CAST(0x0000A4EB00F4F612 AS DateTime), N'', CAST(0x00008EAC00C5C100 AS DateTime), N'0.0.0.0', N'')
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
/****** Object:  Table [dbo].[UserRoleTable]    Script Date: 09/21/2015 15:16:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRoleTable]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserRoleTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL CONSTRAINT [DF__UserRoleT__UserI__679450C0]  DEFAULT ('0'),
	[RoleID] [int] NOT NULL CONSTRAINT [DF__UserRoleT__RoleI__688874F9]  DEFAULT ('0'),
 CONSTRAINT [PK_UserRoleTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[UserRoleTable] ON
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (3, 2, 5)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (19, 2, 2)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (22, 7, 3)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (23, 5, 2)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (24, 8, 6)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (33, 12, 16)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (34, 13, 14)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (35, 14, 15)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (36, 11, 17)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (37, 2, 3)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (38, 10, 8)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (49, 17, 4)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (51, 5, 3)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (53, 18, 6)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (54, 18, 7)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (55, 18, 9)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (56, 24, 19)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (57, 25, 21)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (58, 26, 6)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (59, 26, 7)
INSERT [dbo].[UserRoleTable] ([ID], [UserID], [RoleID]) VALUES (60, 25, 6)
SET IDENTITY_INSERT [dbo].[UserRoleTable] OFF
/****** Object:  Table [dbo].[GlobalAllTestModelList]    Script Date: 09/21/2015 15:16:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalAllTestModelList]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GlobalAllTestModelList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL CONSTRAINT [DF_GlobalAllTestModelList_Name]  DEFAULT (''),
	[ShowName] [nvarchar](30) NOT NULL CONSTRAINT [DF_GlobalAllTestModelList_ShowName]  DEFAULT (''),
	[ItemDescription] [nvarchar](50) NULL CONSTRAINT [DF_GlobalAllTestModelList_Description]  DEFAULT (''),
 CONSTRAINT [PK_GlobalAllTestModelList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalAllTestModelList] UNIQUE NONCLUSTERED 
(
	[ItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalAllTestModelList_ShowNameUnique] UNIQUE NONCLUSTERED 
(
	[ShowName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[GlobalAllTestModelList] ON
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (1, 1, N'AdjustEye', N'调整光眼图', N'调整眼图!')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (2, 1, N'AdjustTxPowerDmi', N'调整TxPowerDmi', N'校准TXPwr')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (3, 2, N'TestTxEye', N'测试光眼图', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (4, 2, N'TestTxPowerDmi', N'测试TxPowerDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (5, 2, N'TestIBiasDmi', N'测试IBiasDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (6, 3, N'AdjustLos', N'调整RxLos', N'AdjustLos')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (7, 3, N'AdjustRxDmi', N'调整RxPowerDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (8, 4, N'TestRXLosAD', N'测试LosADLosH', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (9, 4, N'TestRxPowerDmi', N'测试RXPowerDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (10, 4, N'TestBer', N'测试灵敏度', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (11, 5, N'AdjustVccDmi', N'调整VccDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (12, 5, N'AdjustTempDmi', N'调整TempDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (13, 6, N'TestTempDmi', N'测试TempDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (14, 6, N'TestVccDmi', N'测试VCCDmi', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (15, 6, N'TestIcc', N'测试电流', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (16, 3, N'AdjustAPD', N'调整APD', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (17, 8, N'AlarmWarning', N'测试警报和报警', N'')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (23, 4, N'TestRxEye', N'测试电眼图', N'TestEleEye...')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (24, 2, N'TestTransfer', N'测试传输', N'TestTransfer  无输入参数 ；')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (28, 10, N'Testx', N'软件人员Demo调试', N'xEeee')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (33, 1, N'NewModelNamex', N'NewShowNamex', N'Description')
INSERT [dbo].[GlobalAllTestModelList] ([ID], [PID], [ItemName], [ShowName], [ItemDescription]) VALUES (37, 3, N'AdjustLosMata37044', N'调整LosMata37044', N'临时TestModel')
SET IDENTITY_INSERT [dbo].[GlobalAllTestModelList] OFF
/****** Object:  Table [dbo].[GlobalManufactureCoefficients]    Script Date: 09/21/2015 15:16:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalManufactureCoefficients]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GlobalManufactureCoefficients](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_GlobalManufactureMemory_PID]  DEFAULT ((0)),
	[ItemTYPE] [nvarchar](30) NOT NULL CONSTRAINT [DF_GlobalManufactureMemory_TYPE]  DEFAULT (''),
	[ItemName] [nvarchar](30) NOT NULL CONSTRAINT [DF_GlobalManufactureMemory_Name]  DEFAULT (''),
	[Channel] [tinyint] NOT NULL CONSTRAINT [DF_GlobalManufactureMemory_Channel]  DEFAULT ('0'),
	[Page] [tinyint] NOT NULL CONSTRAINT [DF_GlobalManufactureMemory_Page]  DEFAULT ('0'),
	[StartAddress] [int] NOT NULL CONSTRAINT [DF_GlobalManufactureMemory_StartAddress]  DEFAULT ('0'),
	[Length] [tinyint] NOT NULL CONSTRAINT [DF_GlobalManufactureMemory_Length]  DEFAULT ('0'),
	[Format] [nvarchar](25) NOT NULL CONSTRAINT [DF_GlobalManufactureMemory_Format]  DEFAULT (''),
 CONSTRAINT [PK_GlobalManufactureMemory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalManufactureCoefficients_Unique] UNIQUE NONCLUSTERED 
(
	[ItemName] ASC,
	[Page] ASC,
	[PID] ASC,
	[StartAddress] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GlobalManufactureCoefficients]') AND name = N'IX_GlobalManufactureCoefficients')
CREATE NONCLUSTERED INDEX [IX_GlobalManufactureCoefficients] ON [dbo].[GlobalManufactureCoefficients] 
(
	[ItemName] ASC,
	[ItemTYPE] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
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
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (121, 4, N'Coefficient', N'DmiVccCoefC', 0, 5, 132, 4, N'IEEE754')
GO
print 'Processed 100 total records'
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
GO
print 'Processed 200 total records'
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
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (344, 4, N'ADC', N'RX2RAWADC', 0, 4, 166, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (345, 4, N'Coefficient', N'RXADCORSLOPCOEFB', 2, 6, 240, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (346, 4, N'Coefficient', N'RXADCORSLOPCOEFC', 2, 6, 244, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (347, 4, N'Coefficient', N'RXADCOROFFSCOEFB', 2, 6, 248, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (348, 4, N'Coefficient', N'RXADCOROFFSCOEFC', 2, 6, 252, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (349, 7, N'Firmware', N'FirmwareRev', 1, 0, 1, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (350, 7, N'ADC', N'VccAdc', 1, 1, 146, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (351, 7, N'ADC', N'TemperatureAdc', 1, 1, 138, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (352, 7, N'ADC', N'RxPowerAdc', 1, 1, 144, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (353, 7, N'ADC', N'TxBiasAdc', 1, 1, 140, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (354, 7, N'ADC', N'TxPowerAdc', 1, 1, 142, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (355, 7, N'Coefficient', N'DmiVccCoefB', 1, 1, 168, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (356, 7, N'Coefficient', N'DmiVccCoefC', 1, 1, 172, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (357, 7, N'Coefficient', N'DmiTempCoefB', 1, 1, 160, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (358, 7, N'Coefficient', N'DmiTempCoefC', 1, 1, 164, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (359, 7, N'Coefficient', N'DmiRxpowerCoefA', 1, 1, 200, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (360, 7, N'Coefficient', N'DmiRxpowerCoefB', 1, 1, 204, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (361, 7, N'Coefficient', N'DmiRxpowerCoefC', 1, 1, 208, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (362, 7, N'Coefficient', N'DEBUGINTERFACE', 1, 1, 128, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (363, 7, N'APC config', N'APCControll', 1, 1, 255, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (364, 7, N'Coefficient', N'TxTargetModDacCoefA', 1, 1, 224, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (365, 7, N'Coefficient', N'TxTargetModDacCoefB', 1, 1, 228, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (366, 7, N'Coefficient', N'TxTargetModDacCoefC', 1, 1, 232, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (367, 7, N'Coefficient', N'TxTargetBiasDacCoefA', 1, 1, 212, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (368, 7, N'Coefficient', N'TxTargetBiasDacCoefB', 1, 1, 216, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (369, 7, N'Coefficient', N'TxTargetBiasDacCoefC', 1, 1, 220, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (370, 7, N'Coefficient', N'CloseLoopCoefA', 1, 1, 236, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (371, 7, N'Coefficient', N'CloseLoopCoefB', 1, 1, 240, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (372, 7, N'Coefficient', N'CloseLoopCoefC', 1, 1, 244, 4, N'IEEE754')
GO
print 'Processed 300 total records'
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (373, 7, N'Coefficient', N'DmiTxPowerSlopCoefA', 1, 1, 176, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (374, 7, N'Coefficient', N'DmiTxPowerSlopCoefB', 1, 1, 180, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (375, 7, N'Coefficient', N'DmiTxPowerSlopCoefC', 1, 1, 184, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (376, 7, N'Coefficient', N'DmiTxPowerOffsetCoefA', 1, 1, 188, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (377, 7, N'Coefficient', N'DmiTxPowerOffsetCoefB', 1, 1, 192, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (378, 7, N'Coefficient', N'DmiTxPowerOffsetCoefC', 1, 1, 196, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (379, 8, N'Firmware', N'FirmwareRev', 0, 0, 36864, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (380, 8, N'ADC', N'VccAdc', 0, 0, 36868, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (381, 8, N'ADC', N'TemperatureAdc', 0, 0, 36866, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (382, 8, N'ADC', N'RxPowerAdc', 1, 0, 36870, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (383, 8, N'ADC', N'RxPowerAdc', 2, 0, 36872, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (384, 8, N'ADC', N'RxPowerAdc', 3, 0, 36874, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (385, 8, N'ADC', N'RxPowerAdc', 4, 0, 36876, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (386, 8, N'ADC', N'TxBiasAdc', 1, 0, 36878, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (387, 8, N'ADC', N'TxBiasAdc', 2, 0, 36880, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (388, 8, N'ADC', N'TxBiasAdc', 3, 0, 36882, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (389, 8, N'ADC', N'TxBiasAdc', 4, 0, 36884, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (390, 8, N'ADC', N'TxPowerAdc', 1, 0, 36886, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (391, 8, N'ADC', N'TxPowerAdc', 2, 0, 36888, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (392, 8, N'ADC', N'TxPowerAdc', 3, 0, 36890, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (393, 8, N'ADC', N'TxPowerAdc', 4, 0, 36892, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (394, 8, N'ADC', N'LATEMPERATUREADC', 1, 0, 36894, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (395, 8, N'ADC', N'LATEMPERATUREADC', 2, 0, 36896, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (396, 8, N'ADC', N'LATEMPERATUREADC', 3, 0, 36898, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (397, 8, N'ADC', N'LATEMPERATUREADC', 4, 0, 36900, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (398, 8, N'Coefficient', N'DmiVccCoefA', 0, 0, 37388, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (399, 8, N'Coefficient', N'DmiVccCoefB', 0, 0, 37392, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (400, 8, N'Coefficient', N'DmiVccCoefC', 0, 0, 37396, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (401, 8, N'Coefficient', N'DmiTempCoefA', 0, 0, 37376, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (402, 8, N'Coefficient', N'DmiTempCoefB', 0, 0, 37380, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (403, 8, N'Coefficient', N'DmiTempCoefC', 0, 0, 37384, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (404, 8, N'Coefficient', N'DmiRxpowerCoefA', 1, 0, 37400, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (405, 8, N'Coefficient', N'DmiRxpowerCoefB', 1, 0, 37404, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (406, 8, N'Coefficient', N'DmiRxpowerCoefC', 1, 0, 37408, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (407, 8, N'Coefficient', N'DmiRxpowerCoefA', 2, 0, 37412, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (408, 8, N'Coefficient', N'DmiRxpowerCoefB', 2, 0, 37416, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (409, 8, N'Coefficient', N'DmiRxpowerCoefC', 2, 0, 37420, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (410, 8, N'Coefficient', N'DmiRxpowerCoefA', 3, 0, 37424, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (411, 8, N'Coefficient', N'DmiRxpowerCoefB', 3, 0, 37428, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (412, 8, N'Coefficient', N'DmiRxpowerCoefC', 3, 0, 37432, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (413, 8, N'Coefficient', N'DmiRxpowerCoefA', 4, 0, 37436, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (414, 8, N'Coefficient', N'DmiRxpowerCoefB', 4, 0, 37440, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (415, 8, N'Coefficient', N'DmiRxpowerCoefC', 4, 0, 37444, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (416, 8, N'Coefficient', N'DmiTxpowerCoefA', 1, 0, 37448, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (417, 8, N'Coefficient', N'DmiTxpowerCoefB', 1, 0, 37452, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (418, 8, N'Coefficient', N'DmiTxpowerCoefC', 1, 0, 37456, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (419, 8, N'Coefficient', N'DmiTxpowerCoefA', 2, 0, 37460, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (420, 8, N'Coefficient', N'DmiTxpowerCoefB', 2, 0, 37464, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (421, 8, N'Coefficient', N'DmiTxpowerCoefC', 2, 0, 37468, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (422, 8, N'Coefficient', N'DmiTxpowerCoefA', 3, 0, 37472, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (423, 8, N'Coefficient', N'DmiTxpowerCoefB', 3, 0, 37476, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (424, 8, N'Coefficient', N'DmiTxpowerCoefC', 3, 0, 37480, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (425, 8, N'Coefficient', N'DmiTxpowerCoefA', 4, 0, 37484, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (426, 8, N'Coefficient', N'DmiTxpowerCoefB', 4, 0, 37488, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (427, 8, N'Coefficient', N'DmiTxpowerCoefC', 4, 0, 37492, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (428, 8, N'Coefficient', N'DMITXAUXCOEFA', 1, 0, 37496, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (429, 8, N'Coefficient', N'DMITXAUXCOEFB', 1, 0, 37500, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (430, 8, N'Coefficient', N'DMITXAUXCOEFC', 1, 0, 37504, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (431, 8, N'Coefficient', N'DMITXAUXCOEFA', 2, 0, 37508, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (432, 8, N'Coefficient', N'DMITXAUXCOEFB', 2, 0, 37512, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (433, 8, N'Coefficient', N'DMITXAUXCOEFC', 2, 0, 37516, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (434, 8, N'Coefficient', N'DMITXAUXCOEFA', 3, 0, 37520, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (435, 8, N'Coefficient', N'DMITXAUXCOEFB', 3, 0, 37524, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (436, 8, N'Coefficient', N'DMITXAUXCOEFC', 3, 0, 37528, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (437, 8, N'Coefficient', N'DMITXAUXCOEFA', 4, 0, 37532, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (438, 8, N'Coefficient', N'DMITXAUXCOEFB', 4, 0, 37536, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (439, 8, N'Coefficient', N'DMITXAUXCOEFC', 4, 0, 37540, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (440, 8, N'Coefficient', N'DMILATMPCOEFA', 1, 0, 37544, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (441, 8, N'Coefficient', N'DMILATMPCOEFB', 1, 0, 37548, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (442, 8, N'Coefficient', N'DMILATMPCOEFC', 1, 0, 37552, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (443, 8, N'Coefficient', N'DMILATMPCOEFA', 2, 0, 37556, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (444, 8, N'Coefficient', N'DMILATMPCOEFB', 2, 0, 37560, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (445, 8, N'Coefficient', N'DMILATMPCOEFC', 2, 0, 37564, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (446, 8, N'Coefficient', N'DMILATMPCOEFA', 3, 0, 37568, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (447, 8, N'Coefficient', N'DMILATMPCOEFB', 3, 0, 37572, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (448, 8, N'Coefficient', N'DMILATMPCOEFC', 3, 0, 37576, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (449, 8, N'Coefficient', N'DMILATMPCOEFA', 4, 0, 37580, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (450, 8, N'Coefficient', N'DMILATMPCOEFB', 4, 0, 37584, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (451, 8, N'Coefficient', N'DMILATMPCOEFC', 4, 0, 37588, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (452, 8, N'Coefficient', N'SETPOINT1', 0, 0, 37592, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (453, 8, N'Coefficient', N'COEFP1', 0, 0, 37596, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (454, 8, N'Coefficient', N'COEFI1', 0, 0, 37600, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (455, 8, N'Coefficient', N'COEFD1', 0, 0, 37604, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (456, 8, N'Coefficient', N'SETPOINT2', 0, 0, 37608, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (457, 8, N'Coefficient', N'COEFP2', 0, 0, 37612, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (458, 8, N'Coefficient', N'COEFI2', 0, 0, 37616, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (459, 8, N'Coefficient', N'COEFD2', 0, 0, 37620, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (460, 8, N'APC config', N'DEBUGINTERFACE', 0, 0, 37114, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (461, 8, N'Coefficient', N'DMIRXOFFSET', 1, 0, 37656, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (462, 8, N'Coefficient', N'DMIRXOFFSET', 2, 0, 37658, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (463, 8, N'Coefficient', N'DMIRXOFFSET', 3, 0, 37660, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (464, 8, N'Coefficient', N'DMIRXOFFSET', 4, 0, 37662, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (473, 58, N'Firmware', N'FirmwareRev', 0, 0, 121, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (474, 58, N'ADC', N'VccAdc', 1, 1, 146, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (475, 58, N'ADC', N'TemperatureAdc', 1, 1, 138, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (476, 58, N'ADC', N'RxPowerAdc', 1, 1, 144, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (477, 58, N'ADC', N'TxBiasAdc', 1, 1, 140, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (478, 58, N'ADC', N'TxPowerAdc', 1, 1, 142, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (479, 58, N'Coefficient', N'DmiVccCoefB', 1, 1, 168, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (480, 58, N'Coefficient', N'DmiVccCoefC', 1, 1, 172, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (481, 58, N'Coefficient', N'DmiTempCoefB', 1, 1, 160, 4, N'IEEE754')
GO
print 'Processed 400 total records'
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (483, 58, N'Coefficient', N'DmiTempCoefC', 1, 1, 164, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (484, 58, N'Coefficient', N'REFERENCETEMPERATURECOEF', 1, 1, 176, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (485, 58, N'Coefficient', N'TXPPROPORTIONLESSCOEF', 1, 1, 178, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (486, 58, N'Coefficient', N'TXPPROPORTIONGREATCOEF', 1, 1, 182, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (487, 58, N'Coefficient', N'TXPFITSCOEFA', 1, 1, 186, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (488, 58, N'Coefficient', N'TXPFITSCOEFB', 1, 1, 190, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (489, 58, N'Coefficient', N'TXPFITSCOEFC', 1, 1, 194, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (490, 58, N'Coefficient', N'DmiRxpowerCoefA', 1, 1, 200, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (491, 58, N'Coefficient', N'DmiRxpowerCoefB', 1, 1, 204, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (492, 58, N'Coefficient', N'DmiRxpowerCoefC', 1, 1, 208, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (493, 58, N'Coefficient', N'SETPOINT', 1, 1, 212, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (494, 58, N'Coefficient', N'COEFP', 1, 1, 216, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (495, 58, N'Coefficient', N'COEFI', 1, 1, 220, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (496, 58, N'Coefficient', N'COEFD', 1, 1, 224, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (497, 58, N'Coefficient', N'TxTargetModDacCoefA', 1, 1, 228, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (498, 58, N'Coefficient', N'TxTargetModDacCoefB', 1, 1, 232, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (499, 58, N'Coefficient', N'TxTargetModDacCoefC', 1, 1, 236, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (500, 58, N'Coefficient', N'TXEQCOEFA', 1, 1, 240, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (501, 58, N'Coefficient', N'TXEQCOEFB', 1, 1, 244, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (502, 58, N'Coefficient', N'TXEQCOEFC', 1, 1, 248, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (503, 58, N'threshold', N'RXADCTHRESHOLD', 1, 1, 252, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (504, 58, N'APC config', N'APCControll', 1, 1, 255, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (505, 59, N'Firmware', N'FirmwareRev', 0, 4, 128, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (506, 59, N'ADC', N'VccAdc', 0, 4, 130, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (507, 59, N'ADC', N'Vcc2Adc', 0, 4, 132, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (508, 59, N'ADC', N'Vcc3Adc', 0, 4, 134, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (509, 59, N'ADC', N'TemperatureAdc', 0, 4, 136, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (510, 59, N'ADC', N'Temperature2Adc', 0, 4, 138, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (511, 59, N'ADC', N'RxPowerAdc', 1, 4, 140, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (512, 59, N'ADC', N'RxPowerAdc', 2, 4, 142, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (513, 59, N'ADC', N'RxPowerAdc', 3, 4, 144, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (514, 59, N'ADC', N'RxPowerAdc', 4, 4, 146, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (516, 59, N'ADC', N'TxBiasAdc', 1, 4, 148, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (517, 59, N'ADC', N'TxBiasAdc', 2, 4, 150, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (518, 59, N'ADC', N'TxBiasAdc', 3, 4, 152, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (519, 59, N'ADC', N'TxBiasAdc', 4, 4, 154, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (520, 59, N'ADC', N'TECTemperatureAdc', 1, 4, 164, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (521, 59, N'ADC', N'TECTemperatureAdc', 2, 4, 166, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (522, 59, N'ADC', N'TECTemperatureAdc', 3, 4, 168, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (523, 59, N'ADC', N'TECTemperatureAdc', 4, 4, 170, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (524, 59, N'ADC', N'TECCurrentAdc', 1, 4, 172, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (525, 59, N'ADC', N'TECCurrentAdc', 2, 4, 174, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (526, 59, N'ADC', N'TECCurrentAdc', 3, 4, 176, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (527, 59, N'ADC', N'TECCurrentAdc', 4, 4, 178, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (528, 59, N'ADC', N'TecVoltageAdc', 1, 4, 180, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (529, 59, N'ADC', N'TECVoltageAdc', 2, 4, 182, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (530, 59, N'ADC', N'TecVoltageAdc', 3, 4, 184, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (531, 59, N'ADC', N'TecVoltageAdc', 4, 4, 186, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (532, 59, N'Coefficient', N'DEBUGINTERFACE', 0, 4, 200, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (533, 59, N'Coefficient', N'DmiVccCoefB', 0, 5, 128, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (534, 59, N'Coefficient', N'DmiVccCoefc', 0, 5, 132, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (535, 59, N'Coefficient', N'DmiVcc2CoefB', 0, 5, 136, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (536, 59, N'Coefficient', N'DmiVcc2Coefc', 0, 5, 140, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (537, 59, N'Coefficient', N'DmiVcc3CoefB', 0, 5, 144, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (538, 59, N'Coefficient', N'DmiVcc3Coefc', 0, 5, 148, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (539, 59, N'Coefficient', N'DmiTempCoefB', 0, 5, 152, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (540, 59, N'Coefficient', N'DmiTempCoefc', 0, 5, 156, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (541, 59, N'Coefficient', N'DmiTemp2CoefB', 0, 5, 160, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (542, 59, N'Coefficient', N'DmiTemp2Coefc', 0, 5, 164, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (543, 59, N'Coefficient', N'DmiRxpowerCoefA', 1, 5, 168, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (544, 59, N'Coefficient', N'DmiRxpowerCoefb', 1, 5, 172, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (545, 59, N'Coefficient', N'DmiRxpowerCoefc', 1, 5, 176, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (546, 59, N'Coefficient', N'DmiRxpowerCoefA', 2, 5, 180, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (547, 59, N'Coefficient', N'DmiRxpowerCoefb', 2, 5, 184, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (548, 59, N'Coefficient', N'DmiRxpowerCoefc', 2, 5, 188, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (549, 59, N'Coefficient', N'DmiRxpowerCoefA', 3, 5, 192, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (550, 59, N'Coefficient', N'DmiRxpowerCoefb', 3, 5, 196, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (551, 59, N'Coefficient', N'DmiRxpowerCoefc', 3, 5, 200, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (552, 59, N'Coefficient', N'DmiRxpowerCoefA', 4, 5, 204, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (553, 59, N'Coefficient', N'DmiRxpowerCoefb', 4, 5, 208, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (554, 59, N'Coefficient', N'DmiRxpowerCoefc', 4, 5, 212, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (555, 59, N'threshold', N'RXADCTHRESHOLD', 1, 5, 240, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (556, 59, N'threshold', N'RXADCTHRESHOLD', 2, 5, 241, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (557, 59, N'threshold', N'RXADCTHRESHOLD', 3, 5, 242, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (558, 59, N'threshold', N'RXADCTHRESHOLD', 4, 5, 243, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (559, 59, N'Coefficient', N'REFERENCETEMPERATURECOEF', 0, 6, 128, 2, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (560, 59, N'Coefficient', N'TxpProportionLessCoef', 1, 6, 130, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (561, 59, N'Coefficient', N'TxpProportionLessCoef', 2, 6, 134, 4, N'U16')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (563, 59, N'Coefficient', N'TxpProportionLessCoef', 3, 6, 138, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (564, 59, N'Coefficient', N'TxpProportionLessCoef', 4, 6, 142, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (565, 59, N'Coefficient', N'TxpProportionGreatCoef', 1, 6, 146, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (566, 59, N'Coefficient', N'TxpProportionGreatCoef', 2, 6, 150, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (567, 59, N'Coefficient', N'TxpProportionGreatCoef', 3, 6, 154, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (568, 59, N'Coefficient', N'TxpProportionGreatCoef', 4, 6, 158, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (569, 59, N'Coefficient', N'TXPFITSCOEFA', 1, 6, 162, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (570, 59, N'Coefficient', N'TXPFITSCOEFb', 1, 6, 166, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (571, 59, N'Coefficient', N'TXPFITSCOEFc', 1, 6, 170, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (572, 59, N'Coefficient', N'TXPFITSCOEFA', 2, 6, 174, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (573, 59, N'Coefficient', N'TXPFITSCOEFb', 2, 6, 178, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (574, 59, N'Coefficient', N'TXPFITSCOEFc', 2, 6, 182, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (575, 59, N'Coefficient', N'TXPFITSCOEFA', 3, 6, 186, 4, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (576, 61, N'Firmware', N'NewParam', 0, 0, 0, 1, N'0')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (577, 61, N'Firmware', N'NewParam1', 0, 0, 0, 1, N'0')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (579, 63, N'Firmware', N'NewParam', 1, 1, 1, 1, N'IEEE754')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (581, 7, N'threshold', N'RXADCTHRESHOLD', 1, 1, 252, 1, N'U8')
INSERT [dbo].[GlobalManufactureCoefficients] ([ID], [PID], [ItemTYPE], [ItemName], [Channel], [Page], [StartAddress], [Length], [Format]) VALUES (582, 58, N'Coefficient', N'DebugInterface', 1, 1, 128, 2, N'U16')
SET IDENTITY_INSERT [dbo].[GlobalManufactureCoefficients] OFF
/****** Object:  StoredProcedure [dbo].[InsertRunRecord]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertRunRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'--修改存储过程 InsertRunRecord
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
--@Remark ntext,	Remark	--150305
CREATE PROCEDURE [dbo].[InsertRunRecord]
 @ID int OUTPUT,
 @SN nvarchar(30),  
 @PID int,
 @StartTime datetime,
 @EndTime datetime, --140721
 @FWRev nvarchar(5), --141027 
 @IP nvarchar(50) ,--141027
 @LightSource nvarchar(100), --141219
 @Remark ntext	--150305
AS
 
BEGIN
   
SET XACT_ABORT ON 
BEGIN TRANSACTION 
INSERT INTO  TopoRunRecordTable ("SN", "PID","StartTime","EndTime","FWRev","IP","LightSource","Remark") 
	VALUES(@SN, @PID,@StartTime,@EndTime,@FWRev,@IP,@LightSource,@Remark);
set @ID = (Select Ident_Current(''TopoRunRecordTable''))
COMMIT TRANSACTION 
SET XACT_ABORT OFF 
print @id
return @ID

END
' 
END
GO
/****** Object:  Table [dbo].[GlobalMSADefintionInf]    Script Date: 09/21/2015 15:16:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalMSADefintionInf]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GlobalMSADefintionInf](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_GlobalMSADefintionInf_PID]  DEFAULT ((0)),
	[FieldName] [nvarchar](30) NOT NULL CONSTRAINT [DF_GlobalMSADefintionInf_FieldName]  DEFAULT (''),
	[Channel] [tinyint] NOT NULL CONSTRAINT [DF_GlobalMSADefintionInf_Channel]  DEFAULT ('0'),
	[SlaveAddress] [int] NOT NULL CONSTRAINT [DF_GlobalMSADefintionInf_SlaveAddress]  DEFAULT ('0'),
	[Page] [tinyint] NOT NULL CONSTRAINT [DF_GlobalMSADefintionInf_Page]  DEFAULT ('0'),
	[StartAddress] [int] NOT NULL CONSTRAINT [DF_GlobalMSADefintionInf_StartAddress]  DEFAULT ('0'),
	[Length] [tinyint] NOT NULL CONSTRAINT [DF_GlobalMSADefintionInf_Length]  DEFAULT ('0'),
	[Format] [nvarchar](10) NOT NULL CONSTRAINT [DF_GlobalMSADefintionInf_Format]  DEFAULT (''),
 CONSTRAINT [PK_GlobalMSADefintionInf] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalMSADefintionInf] UNIQUE NONCLUSTERED 
(
	[FieldName] ASC,
	[Channel] ASC,
	[Page] ASC,
	[SlaveAddress] ASC,
	[StartAddress] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
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
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (49, 11, N'1', 1, 1, 1, 1, 1, N'1')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (50, 16, N'3', 3, 2, 2, 2, 4, N'1')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (52, 16, N'4', 1, 2, 2, 2, 4, N'1')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (53, 16, N'3', 2, 2, 2, 2, 4, N'1')
INSERT [dbo].[GlobalMSADefintionInf] ([ID], [PID], [FieldName], [Channel], [SlaveAddress], [Page], [StartAddress], [Length], [Format]) VALUES (54, 16, N'3', 1, 2, 2, 2, 4, N'1')
SET IDENTITY_INSERT [dbo].[GlobalMSADefintionInf] OFF
/****** Object:  Table [dbo].[GlobalProductionType]    Script Date: 09/21/2015 15:16:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalProductionType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GlobalProductionType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](25) NOT NULL CONSTRAINT [DF_GlobalProductionType_Name]  DEFAULT (''),
	[MSAID] [int] NOT NULL CONSTRAINT [DF_GlobalProductionType_MSAID]  DEFAULT ('0'),
	[IgnoreFlag] [bit] NOT NULL CONSTRAINT [DF_GlobalProductionType_IgnoreFlag]  DEFAULT ('false'),
 CONSTRAINT [PK_GlobalProductionType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalProductionType] UNIQUE NONCLUSTERED 
(
	[ItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[GlobalProductionType] ON
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID], [IgnoreFlag]) VALUES (1, N'QSFP28', 3, 0)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID], [IgnoreFlag]) VALUES (2, N'CFP4', 2, 0)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID], [IgnoreFlag]) VALUES (3, N'SFP28', 1, 0)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID], [IgnoreFlag]) VALUES (4, N'XFP', 1, 0)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID], [IgnoreFlag]) VALUES (5, N'q', 1, 1)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID], [IgnoreFlag]) VALUES (6, N'1', 9, 1)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID], [IgnoreFlag]) VALUES (7, N'12', 1, 1)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID], [IgnoreFlag]) VALUES (8, N'a1', 1, 0)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID], [IgnoreFlag]) VALUES (9, N'0601Type', 12, 1)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID], [IgnoreFlag]) VALUES (10, N'123', 1, 1)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID], [IgnoreFlag]) VALUES (11, N'wer', 1, 1)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID], [IgnoreFlag]) VALUES (12, N'1234', 16, 1)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID], [IgnoreFlag]) VALUES (13, N'890', 4, 1)
INSERT [dbo].[GlobalProductionType] ([ID], [ItemName], [MSAID], [IgnoreFlag]) VALUES (18, N'a2', 9, 1)
SET IDENTITY_INSERT [dbo].[GlobalProductionType] OFF
/****** Object:  Table [dbo].[RoleFunctionTable]    Script Date: 09/21/2015 15:16:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RoleFunctionTable]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RoleFunctionTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL DEFAULT ('0'),
	[FunctionID] [int] NOT NULL DEFAULT ('0'),
 CONSTRAINT [PK_RoleFunctionTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[RoleFunctionTable] ON
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (1, 1, 1)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (2, 1, 2)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (3, 1, 3)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (4, 1, 5)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (8, 3, 1)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (9, 3, 2)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (10, 3, 3)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (11, 3, 4)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (12, 3, 5)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (13, 3, 6)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (15, 5, 1)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (16, 5, 2)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (17, 5, 3)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (18, 5, 4)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (28, 3, 9)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (29, 3, 10)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (30, 2, 1)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (36, 3, 8)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (42, 3, 12)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (43, 3, 13)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (47, 3, 7)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (48, 3, 27)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (49, 3, 26)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (50, 3, 25)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (51, 3, 24)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (52, 3, 23)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (53, 3, 22)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (54, 3, 21)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (55, 3, 20)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (56, 3, 19)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (57, 3, 18)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (58, 3, 17)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (59, 3, 16)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (60, 3, 15)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (61, 3, 14)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (62, 5, 27)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (63, 1, 4)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (64, 1, 28)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (65, 3, 28)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (66, 5, 28)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (73, 5, 5)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (74, 5, 9)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (75, 5, 26)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (76, 5, 53)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (77, 5, 23)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (78, 5, 18)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (79, 5, 16)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (80, 6, 1)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (81, 7, 5)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (82, 8, 9)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (83, 9, 14)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (84, 10, 18)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (85, 11, 22)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (86, 12, 26)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (87, 13, 53)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (88, 14, 2)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (89, 14, 6)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (90, 14, 10)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (91, 14, 15)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (92, 14, 19)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (93, 14, 23)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (94, 14, 125)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (95, 15, 4)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (96, 15, 8)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (97, 15, 13)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (98, 15, 17)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (99, 15, 21)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (100, 15, 25)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (102, 16, 3)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (103, 16, 7)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (104, 16, 12)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (105, 16, 20)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (106, 16, 24)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (107, 16, 53)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (108, 17, 1)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (109, 17, 5)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (110, 17, 9)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (111, 17, 18)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (112, 17, 22)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (113, 17, 26)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (114, 3, 53)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (115, 17, 14)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (116, 16, 16)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (120, 3, 124)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (121, 3, 125)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (123, 17, 124)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (124, 19, 6)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (125, 19, 7)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (126, 21, 6)
INSERT [dbo].[RoleFunctionTable] ([ID], [RoleID], [FunctionID]) VALUES (127, 3, 104)
SET IDENTITY_INSERT [dbo].[RoleFunctionTable] OFF
/****** Object:  Table [dbo].[RegisterFormula]    Script Date: 09/21/2015 15:16:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RegisterFormula]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RegisterFormula](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ChipID] [int] NOT NULL,
	[ItemName] [nvarchar](50) NOT NULL CONSTRAINT [DF_RegisterFormula_ItemName]  DEFAULT (''),
	[WriteFormula] [nvarchar](200) NOT NULL CONSTRAINT [DF_RegisterFormula_WriteFormula]  DEFAULT (''),
	[AnalogueUnit] [nvarchar](20) NOT NULL CONSTRAINT [DF_RegisterFormula_AnalogueUnit]  DEFAULT (''),
	[ReadFormula] [nvarchar](200) NOT NULL CONSTRAINT [DF_Table_1_WriteFormula1]  DEFAULT (''),
 CONSTRAINT [PK_RegisterFormula] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_RegisterFormula] UNIQUE NONCLUSTERED 
(
	[ChipID] ASC,
	[ItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BlockAscxInfo]    Script Date: 09/21/2015 15:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlockAscxInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BlockAscxInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FuncBlockID] [int] NOT NULL CONSTRAINT [DF_BlockAscxInfo_FuncBlockID]  DEFAULT ((0)),
	[AscxID] [int] NOT NULL CONSTRAINT [DF_BlockAscxInfo_AscxID]  DEFAULT ((0)),
 CONSTRAINT [PK_BlockAscxInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalMSADefintionInf]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalMSADefintionInf]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_GlobalMSADefintionInf] 
@ID	int	OUTPUT,
@PID	int,
@FieldName	nvarchar(30),
@Channel	tinyint	,
@SlaveAddress	int	,
@Page	tinyint	,
@StartAddress	int	,
@Length	tinyint	,
@Format	nvarchar(10),
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo	nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''MSAInfo''
set @OPItemStr = ''MSAParams:''
set @OPItemNameStr =''FieldName'' + @FieldName +''+CH='' + Ltrim(str(@Channel))
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  GlobalMSADefintionInf ([PID]
			   ,[FieldName]
			   ,[Channel]
			   ,[SlaveAddress]
			   ,[Page]
			   ,[StartAddress]
			   ,[Length]
			   ,[Format])
	Values(@PID,@FieldName,@Channel,@SlaveAddress,@Page,@StartAddress,@Length,@Format) ;
	set @ID = (Select Ident_Current(''GlobalMSADefintionInf''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  GlobalMSADefintionInf
	set 
	PID=@PID,FieldName=	@FieldName,Channel= @Channel,SlaveAddress= @SlaveAddress,
	Page= @Page,StartAddress= @StartAddress,[Length]= @Length,Format= @Format
	from GlobalMSADefintionInf where id =@id

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  GlobalMSADefintionInf 
	where GlobalMSADefintionInf.id=@ID
	
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalMSA]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalMSA]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_GlobalMSA] 
@ID	int	OUTPUT,
@ItemName	nvarchar(25),
@AccessInterface	nvarchar(25),
@SlaveAddress	int,
@IgnoreFlag	bit,
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr int OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''MSAInfo''
set @OPItemStr = ''MSA:''
set @OPItemNameStr =@ItemName
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  GlobalMSA ([ItemName]
			   ,[AccessInterface]
			   ,[SlaveAddress]
			   ,[IgnoreFlag])
	Values(@ItemName,@AccessInterface,@SlaveAddress,@IgnoreFlag) ;
	set @ID = (Select Ident_Current(''GlobalMSA''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  GlobalMSA
	set 
	ItemName=@ItemName,AccessInterface=@AccessInterface,SlaveAddress=@SlaveAddress,IgnoreFlag= @IgnoreFlag
	from GlobalMSA where id =@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	set @ID =@id;
	end
--delete
else if (@RowState=2)
	BEGIN
	delete  GlobalMSA 
	where GlobalMSA.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
    set @ID =@id;
	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalManufactureCoefficientsGroup]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalManufactureCoefficientsGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_GlobalManufactureCoefficientsGroup] 
@ID int OUTPUT,
@ItemName	nvarchar(30)	,
@TypeID	int	,
@ItemDescription	nvarchar(200)	,
@IgnoreFlag	bit,
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''MCoefGroup''
set @OPItemStr = ''GroupGroup:''
set @OPItemNameStr =@ItemName
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  GlobalManufactureCoefficientsGroup ([ItemName]
			   ,[TypeID]
			   ,[ItemDescription]
			   ,[IgnoreFlag])
	Values(@ItemName,@TypeID,@ItemDescription,@IgnoreFlag) ;
	set @ID = (Select Ident_Current(''GlobalManufactureCoefficientsGroup''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  GlobalManufactureCoefficientsGroup
	set 
	ItemName=@ItemName,TypeID= @TypeID,
	ItemDescription= @ItemDescription,IgnoreFlag= @IgnoreFlag
	from GlobalManufactureCoefficientsGroup where id =@id

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  GlobalManufactureCoefficientsGroup 
	where GlobalManufactureCoefficientsGroup.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalManufactureCoefficients]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalManufactureCoefficients]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_GlobalManufactureCoefficients] 
@ID int OUTPUT,
@PID int,
@ItemTYPE	nvarchar(30),
@ItemName	nvarchar(30)	,
@Channel	tinyint	,
@Page		tinyint	,
@StartAddress			int	,
@Length			tinyint	,
@Format	nvarchar(25),
@RowState tinyint,
@OPlogPID int,
@TracingInfo nvarchar(max),
@myErr int OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50);
set @BlockType = ''MCoefGroup''
set @OPItemStr = ''GroupParams:''
set @OPItemNameStr =''ItemName=''+@ItemName +''+CH='' +ltrim(str(@Channel));
--AddNew
if (@RowState=0)
BEGIN
INSERT INTO  GlobalManufactureCoefficients ([PID]
           ,[ItemTYPE]
           ,[ItemName]
           ,[Channel]
           ,[Page]
           ,[StartAddress]
           ,[Length]
           ,[Format])
Values(@PID,@ItemTYPE,@ItemName,@Channel,@Page,@StartAddress,@Length,@Format) ;
set @ID = (Select Ident_Current(''GlobalManufactureCoefficients''))

INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
end
--Update
else if (@RowState=1)
BEGIN
update  GlobalManufactureCoefficients
set 
PID=@PID,ItemTYPE=@ItemTYPE,ItemName=@ItemName,Channel= @Channel,
Page= @Page,StartAddress= @StartAddress,[Length]= @Length,Format= @Format
from GlobalManufactureCoefficients where id =@id

INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

end
--delete
else if (@RowState=2)
BEGIN
delete  GlobalManufactureCoefficients 
where GlobalManufactureCoefficients.id=@ID

INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalAllTestModelList]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalAllTestModelList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_GlobalAllTestModelList] 
@ID int OUTPUT,
@PID	int,
@ItemName	nvarchar(30),
@ShowName	nvarchar(30),
@ItemDescription	nvarchar(50),
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @BlockType nvarchar(50)
set @BlockType=''AppModel''

declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
set @OPItemStr = ''GlobalModel:''
set @OPItemNameStr =@ItemName

--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  GlobalAllTestModelList ([PID]
			   ,[ItemName]
			   ,[ShowName]
			   ,[ItemDescription])
	Values(@PID,@ItemName,@ShowName,@ItemDescription) ;
	set @ID = (Select Ident_Current(''GlobalAllTestModelList''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
		update  GlobalAllTestModelList
		set 
		PID =@PID,
		ItemName=@ItemName,
		ShowName=@ShowName,
		ItemDescription=@ItemDescription
		from GlobalAllTestModelList where id =@id

		INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  GlobalAllTestModelList 
	where GlobalAllTestModelList.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_RegisterFormula]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_RegisterFormula]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Pro_RegisterFormula] 
@ID	int	OUTPUT,
@ChipID	int,
@ItemName	nvarchar(50),
@WriteFormula	nvarchar(200),
@AnalogueUnit	nvarchar(20),
@ReadFormula	nvarchar(200),
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr int OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''ChipInfo''
set @OPItemStr = ''RegisterFormula:''
set @OPItemNameStr = @ItemName;
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  RegisterFormula ([ChipID]
           ,[ItemName]
           ,[WriteFormula]
           ,[AnalogueUnit]
           ,[ReadFormula])
	Values(@ChipID,@ItemName,@WriteFormula,@AnalogueUnit,@ReadFormula) ;
	set @ID = (Select Ident_Current(''RegisterFormula''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  RegisterFormula
	set 
	[ChipID]=@ChipID,[ItemName]=@ItemName,[WriteFormula]=@WriteFormula,[AnalogueUnit]=@AnalogueUnit,[ReadFormula]=@ReadFormula
	from RegisterFormula where id =@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	set @ID =@id;
	end
--delete
else if (@RowState=2)
	BEGIN
	delete  RegisterFormula 
	where RegisterFormula.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
    set @ID =@id;
	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalSpecs]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalSpecs]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_GlobalSpecs] 
@ID int OUTPUT,
@ItemName	nvarchar(30),
@Unit	nvarchar(50),
@ItemDescription	nvarchar(4000),
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo	nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @BlockType nvarchar(50)
set @BlockType=''GlobalSpecs''
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
set @OPItemStr = ''GlobalSpecs:''
set @OPItemNameStr =@ItemName
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  GlobalSpecs ([ItemName]
			   ,[Unit]
			   ,[ItemDescription])
	Values(@ItemName,@Unit,@ItemDescription) ;
	set @ID = (Select Ident_Current(''GlobalSpecs''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  GlobalSpecs
	set 

	ItemName=@ItemName,
	Unit=@Unit,
	ItemDescription=@ItemDescription
	from GlobalSpecs where id =@id

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  GlobalSpecs 
	where GlobalSpecs.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalProductionType]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalProductionType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_GlobalProductionType] 
@ID int OUTPUT,
@ItemName	nvarchar(25),
@MSAID	int	,
@IgnoreFlag	bit	,
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @BlockType nvarchar(50)
set @BlockType=''ProductionInfo''
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
set @OPItemStr = ''PNType:''
set @OPItemNameStr =@ItemName
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  GlobalProductionType ([ItemName]
			   ,[MSAID]
			   ,[IgnoreFlag])
	Values(@ItemName,@MSAID,@IgnoreFlag) ;
	set @ID = (Select Ident_Current(''GlobalProductionType''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  GlobalProductionType
	set 
	ItemName=@ItemName,
	MSAID=@MSAID,
	IgnoreFlag=@IgnoreFlag
	from GlobalProductionType where id =@id

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  GlobalProductionType 
	where GlobalProductionType.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  Table [dbo].[GlobalProductionName]    Script Date: 09/21/2015 15:16:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalProductionName]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GlobalProductionName](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_GlobalProductionName_PID]  DEFAULT ((0)),
	[PN] [nvarchar](35) NOT NULL CONSTRAINT [DF_GlobalProductionName_PN]  DEFAULT (''),
	[ItemName] [nvarchar](200) NOT NULL CONSTRAINT [DF_GlobalProductionName_Name]  DEFAULT (''),
	[Channels] [tinyint] NOT NULL CONSTRAINT [DF_GlobalProductionName_Channels]  DEFAULT ('4'),
	[Voltages] [tinyint] NOT NULL CONSTRAINT [DF_GlobalProductionName_Voltages]  DEFAULT ((0)),
	[Tsensors] [tinyint] NOT NULL CONSTRAINT [DF_GlobalProductionName_Tsensors]  DEFAULT ((0)),
	[MCoefsID] [int] NOT NULL CONSTRAINT [DF_GlobalProductionName_MGroupID]  DEFAULT ('0'),
	[IgnoreFlag] [bit] NOT NULL CONSTRAINT [DF_GlobalProductionName_IgnoreFlag]  DEFAULT ('false'),
	[OldDriver] [tinyint] NOT NULL CONSTRAINT [DF_GlobalProductionName_IgnoreFlag1]  DEFAULT ((0)),
	[TEC_Present] [tinyint] NOT NULL CONSTRAINT [DF_GlobalProductionName_TEC_Present]  DEFAULT ((0)),
	[Couple_Type] [tinyint] NOT NULL CONSTRAINT [DF_GlobalProductionName_Couple_Type]  DEFAULT ((0)),
	[APC_Type] [tinyint] NOT NULL CONSTRAINT [DF_GlobalProductionName_APC_Type]  DEFAULT ((0)),
	[BER] [tinyint] NOT NULL CONSTRAINT [DF_GlobalProductionName_BER(exp)]  DEFAULT ((0)),
	[MaxRate] [tinyint] NOT NULL CONSTRAINT [DF_GlobalProductionName_MaxRate(G)]  DEFAULT ((0)),
	[Publish_PN] [nvarchar](50) NOT NULL CONSTRAINT [DF_GlobalProductionName_Publish_PN]  DEFAULT (''),
	[NickName] [nvarchar](50) NOT NULL CONSTRAINT [DF_GlobalProductionName_NickName]  DEFAULT (''),
	[IbiasFormula] [nvarchar](max) NOT NULL CONSTRAINT [DF_GlobalProductionName_IbiasFormula]  DEFAULT (''),
	[IModFormula] [nvarchar](max) NOT NULL CONSTRAINT [DF_GlobalProductionName_IbiasFormula1]  DEFAULT (''),
	[UsingCelsiusTemp] [bit] NOT NULL CONSTRAINT [DF_GlobalProductionName_CurvingXParamters]  DEFAULT ('false'),
	[RxOverLoadDBm] [real] NOT NULL CONSTRAINT [DF_GlobalProductionName_RxOverLoadDBm]  DEFAULT ((0)),
 CONSTRAINT [PK_GlobalProductionName] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_UniquePN] UNIQUE NONCLUSTERED 
(
	[PN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GlobalProductionName]') AND name = N'IX_GlobalProductionName')
CREATE NONCLUSTERED INDEX [IX_GlobalProductionName] ON [dbo].[GlobalProductionName] 
(
	[MCoefsID] ASC,
	[PID] ASC,
	[NickName] ASC,
	[Publish_PN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'GlobalProductionName', N'COLUMN',N'ItemName'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对于机种的描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GlobalProductionName', @level2type=N'COLUMN',@level2name=N'ItemName'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'GlobalProductionName', N'COLUMN',N'Channels'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该产品的通道数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GlobalProductionName', @level2type=N'COLUMN',@level2name=N'Channels'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'GlobalProductionName', N'COLUMN',N'MCoefsID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'引用的系数组别的ID号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GlobalProductionName', @level2type=N'COLUMN',@level2name=N'MCoefsID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'GlobalProductionName', N'COLUMN',N'TEC_Present'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: not TEC present;N:N TEC Present' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GlobalProductionName', @level2type=N'COLUMN',@level2name=N'TEC_Present'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'GlobalProductionName', N'COLUMN',N'Couple_Type'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: DC;1: AC' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GlobalProductionName', @level2type=N'COLUMN',@level2name=N'Couple_Type'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'GlobalProductionName', N'COLUMN',N'APC_Type'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: None;1: Open-Loop;2: Close-Loop;3: PID Close-Loop' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GlobalProductionName', @level2type=N'COLUMN',@level2name=N'APC_Type'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'GlobalProductionName', N'COLUMN',N'UsingCelsiusTemp'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'true:realTemp false:TempADC' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GlobalProductionName', @level2type=N'COLUMN',@level2name=N'UsingCelsiusTemp'
GO
SET IDENTITY_INSERT [dbo].[GlobalProductionName] ON
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (6, 1, N'TR_CGR4_V4', N'V4', 4, 1, 0, 4, 0, 1, 0, 0, 3, 12, 3, N'TR-CGR4-V04', N'CGR4-V00', N'(IBIAS(MA) - (IMODDAC / 1023.0 * 48)) / 78 * 1023.0', N'', 0, -5)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (7, 1, N'TR_CSR4_AOC(5002812)', N'AOC', 4, 1, 1, 6, 0, 0, 0, 0, 0, 0, 0, N'', N'', N'', N'', 0, 0)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (8, 3, N'TR_SFP28_SR', N'SFP_SR', 1, 1, 0, 7, 0, 1, 0, 1, 2, 12, 4, N'12', N'13', N'', N'', 0, 0)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (9, 3, N'TR_SFP28_LR', N'SFP_LR', 1, 1, 1, 58, 0, 1, 0, 0, 3, 12, 4, N'1', N'1', N'(IBIAS(MA) - (IMODDAC / 1023.0 * 48)) / 78 * 1023.0', N'', 1, 0)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (10, 2, N'CFP4-LR4-N01', N'CFP_LR4', 4, 1, 1, 8, 0, 1, 0, 0, 0, 12, 0, N'1', N'1', N'', N'', 0, 0)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (12, 1, N'TR_SR4_Nxx', N'SR4', 4, 1, 1, 6, 1, 1, 0, 1, 1, 12, 4, N'SR4_TEST', N'SR4_TEST(NICKNAME)', N'10', N'20', 1, 0)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (27, 1, N'TR_SR4_New', N'New SR4', 4, 1, 1, 6, 1, 0, 0, 1, 1, 12, 28, N'TR_SR4_NewPN', N'TR_SR4_NewNickName', N'10', N'20', 0, 0)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (29, 1, N'testtest', N'test', 2, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, N'1', N'1', N'1', N'', 0, 1)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (34, 1, N'1222', N'1', 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, N'V', N'V', N'V', N'V', 0, 5)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (35, 1, N'SR4-V3-MACOM', N'QSPF28 SR2 V2 with MACOM Scheme', 4, 1, 1, 6, 0, 1, 0, 1, 1, 12, 4, N'1', N'1', N'', N'', 0, 2)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (36, 1, N'TR_LR4', N'LR4', 4, 1, 1, 59, 0, 1, 1, 0, 0, 12, 4, N'1', N'1', N'', N'', 1, 2)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (38, 3, N'TR_SFP28_SR-AOC', N'0', 1, 1, 0, 7, 0, 1, 0, 0, 0, 12, 4, N'1', N'1', N'', N'', 0, 0)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (39, 8, N'678678', N'67868', 4, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, N'76868686', N'686868', N'', N'', 0, -1)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (41, 8, N'111', N'1', 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, N'1', N'1', N'1', N'1', 0, 1)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (43, 1, N'TR-PSM4-Open', N'0', 4, 1, 1, 6, 0, 1, 0, 0, 1, 0, 4, N'0', N'0', N'', N'', 0, 0)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (52, 1, N'a', N'2', 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, N'1', N'1', N'1', N'1', 0, 1)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (60, 1, N'TR-CSR4-Semtech', N'P2-V03', 4, 1, 0, 6, 0, 1, 0, 0, 1, 12, 3, N'1', N'1', N'', N'', 1, 4)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (61, 3, N'TR_SFP28_SR-V2', N'No MPD', 1, 1, 0, 7, 0, 1, 0, 1, 1, 12, 3, N'1', N'1', N'', N'', 0, -5)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (62, 1, N'TR-SR4-V3', N'v3', 4, 1, 1, 6, 0, 1, 0, 0, 1, 12, 3, N'1', N'1', N'', N'', 0, 3)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (63, 1, N'111111111111', N'aa', 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, N'1', N'1', N'', N'', 1, -5)
INSERT [dbo].[GlobalProductionName] ([ID], [PID], [PN], [ItemName], [Channels], [Voltages], [Tsensors], [MCoefsID], [IgnoreFlag], [OldDriver], [TEC_Present], [Couple_Type], [APC_Type], [BER], [MaxRate], [Publish_PN], [NickName], [IbiasFormula], [IModFormula], [UsingCelsiusTemp], [RxOverLoadDBm]) VALUES (68, 1, N'CGR4-BD', N'BD', 4, 1, 0, 4, 0, 1, 0, 0, 3, 12, 4, N'1', N'1', N'(IBIAS(MA) - (IMODDAC / 1023.0 * 48)) / 78 * 1023.0', N'', 1, 3)
SET IDENTITY_INSERT [dbo].[GlobalProductionName] OFF
/****** Object:  StoredProcedure [dbo].[Pro_GlobalAllEquipmentList]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalAllEquipmentList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_GlobalAllEquipmentList] 
@ID int OUTPUT,
@ItemName	nvarchar(30),
@ShowName	nvarchar(30),
@ItemType	nvarchar(30),
@ItemDescription	nvarchar(50),
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @BlockType nvarchar(50)
set @BlockType=''Equipment''
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
set @OPItemStr = ''GlobalEquipment:''
set @OPItemNameStr =@ItemName
--AddNew
if (@RowState=0)
BEGIN
	INSERT INTO  GlobalAllEquipmentList ([ItemName]
			   ,[ShowName]
			   ,[ItemType]
			   ,[ItemDescription])
	Values(@ItemName,@ShowName,@ItemType,@ItemDescription) ;
	set @ID = (Select Ident_Current(''GlobalAllEquipmentList''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
end
--Update
else if (@RowState=1)
	BEGIN
	update  GlobalAllEquipmentList
	set 

	ItemName=@ItemName,
	ShowName=@ShowName,
	ItemType=@ItemType,
	ItemDescription=@ItemDescription
	from GlobalAllEquipmentList where id =@id

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  GlobalAllEquipmentList 
	where GlobalAllEquipmentList.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalAllAppModelList]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalAllAppModelList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_GlobalAllAppModelList] 
@ID int OUTPUT,
@ItemName	nvarchar(30),
@ItemDescription	nvarchar(50),
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @BlockType nvarchar(50)
set @BlockType=''AppModel''
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
set @OPItemStr = ''APP:''
set @OPItemNameStr =@ItemName
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  GlobalAllAppModelList ([ItemName],[ItemDescription])
	Values(@ItemName,@ItemDescription) ;
	set @ID = (Select Ident_Current(''GlobalAllAppModelList''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
end
--Update
else if (@RowState=1)
	BEGIN
	update  GlobalAllAppModelList
	set 

	ItemName=@ItemName,
	ItemDescription=@ItemDescription
	from GlobalAllAppModelList where id =@id

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  GlobalAllAppModelList 
	where GlobalAllAppModelList.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_ChipBaseInfo]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_ChipBaseInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_ChipBaseInfo] 
@ID	int	OUTPUT,
@ItemName	nvarchar(50),
@Channels	tinyint,
@Description nvarchar(500),
@Width	tinyint,
@LittleEndian	bit,
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr int OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''ChipInfo''
set @OPItemStr = ''ChipBaseInfo:''
set @OPItemNameStr = @ItemName
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  ChipBaseInfo ([ItemName]
           ,[Channels]
           ,[Description]
           ,[Width]
           ,[LittleEndian])
	Values(@ItemName,@Channels,@Description,@Width,@LittleEndian) ;
	set @ID = (Select Ident_Current(''ChipBaseInfo''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  ChipBaseInfo
	set 
	ItemName=@ItemName,Channels=@Channels,[Description]=@Description,[Width]=@Width,[LittleEndian]=@LittleEndian
	from ChipBaseInfo where id =@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	set @ID =@id;
	end
--delete
else if (@RowState=2)
	BEGIN
	delete  ChipBaseInfo 
	where ChipBaseInfo.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
    set @ID =@id;
	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[InsertLogRecord]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertLogRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'--修改存储过程 [InsertMyLogRecord]
--存储过程 [InsertMyLogRecord]
--
--向TopoLogRecord 添加一条新记录，并返回该记录的ID。
--TopoLogRecord  字段 ID PID RunRecordID StartTime EndTime TestLog Temp Voltage Channel Result
--
--参数		方向	类型		描述
--@ID		OUT	Int		新记录ID
--@CtrlType		IN	tinyint		@CtrlType	--150413 Deleted
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
 @CtrlType tinyint,	--150414
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

INSERT INTO  TopoLogRecord VALUES(@RunRecordID , @StartTime,@EndTime,@TestLog,@Temp,@Voltage,@Channel,@Result,@CtrlType);
set @ID = (Select Ident_Current(''TopoLogRecord''))

COMMIT TRANSACTION 
SET XACT_ABORT OFF 

print @id
return @ID
END
' 
END
GO
/****** Object:  Table [dbo].[ChipRegister]    Script Date: 09/21/2015 15:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChipRegister]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ChipRegister](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FormulaID] [int] NOT NULL,
	[AccessType] [int] NOT NULL CONSTRAINT [DF_Table_1_ItemName_2]  DEFAULT ((0)),
	[Address] [int] NOT NULL CONSTRAINT [DF_Table_1_WriteFormula]  DEFAULT ((0)),
	[StartBit] [int] NOT NULL CONSTRAINT [DF_Table_1_AnalogueUnit]  DEFAULT ((0)),
	[Length] [int] NOT NULL CONSTRAINT [DF_Table_1_ReadFormula]  DEFAULT ((0)),
	[ChipLine] [int] NOT NULL CONSTRAINT [DF_ChipRegister_ChannelNumber]  DEFAULT ((0)),
 CONSTRAINT [PK_ChipRegister] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_ChipRegister] UNIQUE NONCLUSTERED 
(
	[FormulaID] ASC,
	[ChipLine] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[GlobalManufactureChipsetInitialize]    Script Date: 09/21/2015 15:16:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalManufactureChipsetInitialize]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GlobalManufactureChipsetInitialize](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_ManufactureChipsetInitialize_PID_1]  DEFAULT ((0)),
	[DriveType] [tinyint] NOT NULL CONSTRAINT [DF_ManufactureChipsetInitialize_DriveType_1]  DEFAULT ((0)),
	[ChipLine] [tinyint] NOT NULL CONSTRAINT [DF_ManufactureChipsetInitialize_ChipLine_1]  DEFAULT ((0)),
	[RegisterAddress] [int] NOT NULL CONSTRAINT [DF_ManufactureChipsetInitialize_RegisterAddress_1]  DEFAULT ((0)),
	[Length] [tinyint] NOT NULL CONSTRAINT [DF_ManufactureChipsetInitialize_Length_1]  DEFAULT ((0)),
	[ItemValue] [int] NOT NULL CONSTRAINT [DF_ManufactureChipsetInitialize_ItemVaule]  DEFAULT ((0)),
	[Endianness] [bit] NOT NULL CONSTRAINT [DF_GlobalManufactureChipsetInitialize_Endianness]  DEFAULT ('false'),
 CONSTRAINT [PK_ManufactureChipsetInitialize_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalManufactureChipsetInitialize] UNIQUE NONCLUSTERED 
(
	[DriveType] ASC,
	[RegisterAddress] ASC,
	[ChipLine] ASC,
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[GlobalManufactureChipsetInitialize] ON
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (15, 6, 3, 2, 2, 4, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (17, 34, 2, 2, 2, 2, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (19, 6, 2, 2, 2, 2, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (22, 60, 3, 1, 0, 1, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (23, 60, 3, 1, 23, 1, 3, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (24, 60, 3, 1, 80, 1, 4, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (25, 60, 3, 1, 256, 1, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (26, 60, 3, 1, 279, 1, 3, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (27, 60, 3, 1, 336, 1, 4, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (28, 60, 3, 1, 512, 1, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (29, 60, 3, 1, 535, 1, 3, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (30, 60, 3, 1, 592, 1, 4, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (31, 60, 3, 1, 768, 1, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (32, 60, 3, 1, 791, 1, 3, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (33, 60, 3, 1, 848, 1, 4, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (34, 60, 3, 1, 1068, 1, 255, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (35, 60, 3, 1, 1078, 1, 63, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (36, 60, 3, 1, 1080, 1, 63, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (37, 60, 3, 2, 3, 1, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (38, 60, 3, 2, 4, 1, 14, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (39, 60, 3, 2, 5, 1, 8, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (40, 60, 3, 2, 259, 1, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (41, 60, 3, 2, 260, 1, 14, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (42, 60, 3, 2, 261, 1, 8, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (43, 60, 3, 2, 515, 1, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (44, 60, 3, 2, 516, 1, 14, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (45, 60, 3, 2, 517, 1, 8, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (46, 60, 3, 2, 771, 1, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (47, 60, 3, 2, 772, 1, 14, 0)
INSERT [dbo].[GlobalManufactureChipsetInitialize] ([ID], [PID], [DriveType], [ChipLine], [RegisterAddress], [Length], [ItemValue], [Endianness]) VALUES (48, 60, 3, 2, 773, 1, 8, 0)
SET IDENTITY_INSERT [dbo].[GlobalManufactureChipsetInitialize] OFF
/****** Object:  Table [dbo].[GlobalManufactureChipsetControl]    Script Date: 09/21/2015 15:16:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalManufactureChipsetControl]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GlobalManufactureChipsetControl](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL CONSTRAINT [DF_ManufactureChipsetInitialize_PID]  DEFAULT ((0)),
	[ItemName] [nvarchar](20) NOT NULL CONSTRAINT [DF_ManufactureChipsetInitialize_ItemName]  DEFAULT (''),
	[ModuleLine] [tinyint] NOT NULL CONSTRAINT [DF_ManufactureChipsetInitialize_ModuleLine]  DEFAULT ((0)),
	[ChipLine] [tinyint] NOT NULL CONSTRAINT [DF_ManufactureChipsetInitialize_ChipLine]  DEFAULT ((0)),
	[DriveType] [tinyint] NOT NULL CONSTRAINT [DF_ManufactureChipsetInitialize_DriveType]  DEFAULT ((0)),
	[RegisterAddress] [int] NOT NULL CONSTRAINT [DF_ManufactureChipsetInitialize_RegisterAddress]  DEFAULT ((0)),
	[Length] [int] NOT NULL CONSTRAINT [DF_ManufactureChipsetInitialize_Length]  DEFAULT ((1)),
	[Endianness] [bit] NOT NULL CONSTRAINT [DF_GlobalManufactureChipsetControl_Endianness]  DEFAULT ('false'),
 CONSTRAINT [PK_ManufactureChipsetInitialize] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalManufactureChipsetControl] UNIQUE NONCLUSTERED 
(
	[DriveType] ASC,
	[ChipLine] ASC,
	[ItemName] ASC,
	[PID] ASC,
	[RegisterAddress] ASC,
	[ModuleLine] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[GlobalManufactureChipsetControl] ON
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (29, 6, N'BiasDac', 1, 1, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (30, 6, N'BiasDac', 2, 2, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (31, 6, N'BiasDac', 3, 3, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (32, 6, N'BiasDac', 4, 4, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (33, 6, N'ModDac', 1, 1, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (34, 6, N'ModDac', 2, 2, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (35, 6, N'ModDac', 3, 3, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (36, 6, N'ModDac', 4, 4, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (37, 6, N'LOSDac', 1, 2, 3, 769, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (38, 6, N'LOSDac', 2, 2, 3, 513, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (39, 6, N'LosDac', 3, 2, 3, 257, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (40, 6, N'LOSDac', 4, 2, 3, 1, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (41, 7, N'BiasDac', 1, 1, 0, 16, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (42, 7, N'BiasDac', 2, 1, 0, 19, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (43, 7, N'BiasDac', 3, 1, 0, 22, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (44, 7, N'BiasDac', 4, 1, 0, 25, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (45, 7, N'ModDac', 1, 1, 0, 17, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (46, 7, N'ModDac', 2, 1, 0, 20, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (47, 7, N'ModDac', 3, 1, 0, 23, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (48, 7, N'ModDac', 4, 1, 0, 26, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (49, 8, N'BIASDAC', 1, 2, 0, 19, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (50, 8, N'MODDAC', 1, 2, 0, 20, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (51, 9, N'BIASDAC', 1, 2, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (52, 9, N'MODDAC', 1, 2, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (53, 27, N'BiasDac', 1, 1, 0, 16, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (54, 27, N'BiasDac', 2, 1, 0, 19, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (55, 27, N'BiasDac', 3, 1, 0, 22, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (56, 27, N'BiasDac', 4, 1, 0, 25, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (57, 27, N'ModDac', 1, 1, 0, 17, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (58, 27, N'ModDac', 2, 1, 0, 20, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (59, 27, N'ModDac', 3, 1, 0, 23, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (60, 27, N'ModDac', 4, 1, 0, 26, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (62, 34, N'2', 2, 2, 2, 2, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (67, 43, N'BIASDAC', 4, 1, 3, 1046, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (68, 43, N'BIASDAC', 1, 1, 3, 278, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (69, 43, N'MODDAC', 4, 1, 3, 1048, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (70, 43, N'BIASDAC', 2, 1, 3, 534, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (71, 43, N'BIASDAC', 3, 1, 3, 790, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (72, 43, N'MODDAC', 1, 1, 3, 280, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (73, 43, N'MODDAC', 2, 1, 3, 536, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (74, 43, N'MODDAC', 3, 1, 3, 792, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (75, 34, N'1', 1, 1, 1, 1, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (76, 36, N'BIASDac', 1, 1, 2, 16, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (77, 36, N'BIASDac', 2, 1, 2, 17, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (78, 36, N'BIASDac', 3, 1, 2, 18, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (79, 36, N'BIASDac', 4, 1, 2, 19, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (80, 36, N'EADac', 1, 1, 2, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (81, 36, N'EADac', 2, 1, 2, 2, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (82, 36, N'EADac', 3, 1, 2, 1, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (83, 36, N'EADac', 4, 1, 2, 0, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (84, 60, N'BIASDAC', 1, 1, 3, 792, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (85, 60, N'BIASDAC', 2, 1, 3, 536, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (86, 60, N'BIASDAC', 3, 1, 3, 280, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (87, 60, N'BIASDAC', 4, 1, 3, 24, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (88, 60, N'MODDAC', 1, 1, 3, 793, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (89, 60, N'MODDAC', 2, 1, 3, 537, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (90, 60, N'MODDAC', 3, 1, 3, 281, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (91, 60, N'MODDAC', 4, 1, 3, 25, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (92, 61, N'BIASDAC', 1, 2, 0, 19, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (93, 61, N'MODDAC', 1, 2, 0, 20, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (94, 62, N'BIASDAC', 1, 1, 3, 24, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (95, 62, N'MODDAC', 1, 1, 3, 25, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (96, 62, N'BIASDAC', 2, 1, 3, 280, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (97, 62, N'MODDAC', 2, 1, 3, 281, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (98, 62, N'BIASDAC', 3, 1, 3, 536, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (99, 62, N'MODDAC', 3, 1, 3, 537, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (100, 62, N'BIASDAC', 4, 1, 3, 792, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (101, 62, N'MODDAC', 4, 1, 3, 793, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (102, 29, N'1', 1, 1, 0, 1, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (116, 36, N'VLDDAC', 1, 1, 2, 4, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (117, 36, N'VLDDAC', 2, 1, 2, 4, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (118, 36, N'VLDDAC', 3, 1, 2, 4, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (119, 36, N'VLDDAC', 4, 1, 2, 4, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (120, 36, N'VCDAC', 4, 1, 2, 8, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (121, 36, N'VCDAC', 3, 1, 2, 9, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (122, 36, N'VCDAC', 2, 1, 2, 10, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (123, 36, N'VCDAC', 1, 1, 2, 11, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (124, 35, N'BIASDAC', 1, 1, 3, 66, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (125, 35, N'MODDAC', 1, 1, 3, 70, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (126, 35, N'BIASDAC', 2, 1, 3, 67, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (127, 35, N'MODDAC', 2, 1, 3, 71, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (128, 35, N'BIASDAC', 3, 1, 3, 68, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (129, 35, N'BIASDAC', 4, 1, 3, 69, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (130, 35, N'MODDAC', 3, 1, 3, 72, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (131, 35, N'MODDAC', 4, 1, 3, 73, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (132, 36, N'VGDAC', 1, 1, 2, 13, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (133, 36, N'VGDAC', 2, 1, 2, 12, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (134, 36, N'VGDAC', 3, 1, 2, 13, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (135, 36, N'VGDAC', 4, 1, 2, 12, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (136, 36, N'CROSSDAC', 1, 1, 3, 75, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (137, 36, N'CROSSDAC', 2, 1, 3, 65, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (138, 36, N'CROSSDAC', 3, 1, 3, 55, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (139, 36, N'CROSSDAC', 4, 1, 3, 45, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (140, 68, N'BIASDAC', 1, 1, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (141, 68, N'BIASDAC', 2, 2, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (142, 68, N'BIASDAC', 3, 3, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (143, 68, N'BIASDAC', 4, 4, 0, 5, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (144, 68, N'MODDAC', 1, 1, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (145, 68, N'MODDAC', 2, 2, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (146, 68, N'MODDAC', 3, 3, 0, 3, 2, 0)
GO
print 'Processed 100 total records'
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (147, 68, N'MODDAC', 4, 4, 0, 3, 2, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (148, 68, N'LOSDAC', 4, 2, 3, 1, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (149, 68, N'LOSDAC', 3, 2, 3, 257, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (150, 68, N'LOSDAC', 2, 2, 3, 513, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (151, 68, N'LOSDAC', 1, 2, 3, 769, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (152, 35, N'LOSDAC', 1, 2, 3, 33, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (153, 35, N'LOSDAC', 2, 2, 3, 33, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (154, 35, N'LOSDAC', 3, 2, 3, 33, 1, 0)
INSERT [dbo].[GlobalManufactureChipsetControl] ([ID], [PID], [ItemName], [ModuleLine], [ChipLine], [DriveType], [RegisterAddress], [Length], [Endianness]) VALUES (155, 35, N'LOSDAC', 4, 2, 3, 33, 1, 0)
SET IDENTITY_INSERT [dbo].[GlobalManufactureChipsetControl] OFF
/****** Object:  Table [dbo].[PNChipMap]    Script Date: 09/21/2015 15:16:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PNChipMap]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PNChipMap](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PNID] [int] NOT NULL CONSTRAINT [DF_Table_1_ItemName_1]  DEFAULT ((0)),
	[ChipID] [int] NOT NULL CONSTRAINT [DF_Table_1_AccessInterface]  DEFAULT ((0)),
	[ChipRoleID] [int] NOT NULL CONSTRAINT [DF_Table_1_SlaveAddress]  DEFAULT ((0)),
	[ChipDirection] [bit] NOT NULL CONSTRAINT [DF_PNChipMap_ChipDirection]  DEFAULT ((0)),
 CONSTRAINT [PK_PNChipMap] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_PNChipMap] UNIQUE NONCLUSTERED 
(
	[PNID] ASC,
	[ChipID] ASC,
	[ChipRoleID] ASC,
	[ChipDirection] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'PNChipMap', N'COLUMN',N'ChipRoleID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: LDD 1: AMP 2: DAC 3: CDR' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PNChipMap', @level2type=N'COLUMN',@level2name=N'ChipRoleID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'PNChipMap', N'COLUMN',N'ChipDirection'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:TX;1:RX' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PNChipMap', @level2type=N'COLUMN',@level2name=N'ChipDirection'
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalProductionName]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalProductionName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Pro_GlobalProductionName] 
@ID int OUTPUT,
@PID	int	,
@PN	nvarchar(35),
@ItemName	nvarchar(200),
@Channels	tinyint	,
@Voltages	tinyint	,
@Tsensors	tinyint	,
@MCoefsID	int	,
@IgnoreFlag	bit	,
@OldDriver	tinyint	,
@TEC_Present	tinyint	,
@Couple_Type	tinyint	,
@APC_Type	tinyint	,
@BER	tinyint	,
@MaxRate	tinyint	,
@Publish_PN	nvarchar(50),
@NickName	nvarchar(50),
@IbiasFormula	nvarchar(max),
@IModFormula	nvarchar(max),
@UsingCelsiusTemp	bit,
@RxOverLoadDBm	real,
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT

AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

--declare @beginTime datetime
--declare @endTime datetime
--declare @ms real

declare @BlockType nvarchar(50)
set @BlockType=''ProductionInfo''
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
set @OPItemStr = ''PN:''
set @OPItemNameStr =@PN;

--set @beginTime = getdate()
set @myErr=@@ERROR
set @myErrMsg='''';
--AddNew
if (@RowState=0)
BEGIN
	INSERT INTO  GlobalProductionName ([PID]
			   ,[PN]
			   ,[ItemName]
			   ,[Channels]
			   ,[Voltages]
			   ,[Tsensors]
			   ,[MCoefsID]
			   ,[IgnoreFlag]
			   ,[OldDriver]
			   ,[TEC_Present]
			   ,[Couple_Type]
			   ,[APC_Type]
			   ,[BER]
			   ,[MaxRate]
			   ,[Publish_PN]
			   ,[NickName]
			   ,[IbiasFormula]
			   ,[IModFormula]
			   ,[UsingCelsiusTemp]
			   ,[RxOverLoadDBm])
			   Values
	(@PID,@PN,@ItemName,@Channels,@Voltages,@Tsensors,@MCoefsID,@IgnoreFlag,@OldDriver,
	@TEC_Present,@Couple_Type,@APC_Type,@BER,@MaxRate,@Publish_PN,@NickName,
	@IbiasFormula,@IModFormula,@UsingCelsiusTemp,@RxOverLoadDBm
	) ;
	set @ID = (Select Ident_Current(''GlobalProductionName''))
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了'' + @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  GlobalProductionName 
	set pid=@PID,
	PN=@PN,
	ItemName=@ItemName,
	Channels=@Channels,
	Voltages=@Voltages,
	Tsensors=@Tsensors,
	MCoefsID=@MCoefsID,
	IgnoreFlag=@IgnoreFlag,
	OldDriver=@OldDriver,
	TEC_Present=@TEC_Present,
	Couple_Type=@Couple_Type,APC_Type=@APC_Type,BER=@BER,MaxRate=@MaxRate,Publish_PN=@Publish_PN,NickName=@NickName,
	IbiasFormula=@IbiasFormula,IModFormula=@IModFormula,UsingCelsiusTemp=@UsingCelsiusTemp,RxOverLoadDBm=@RxOverLoadDBm 
	from GlobalProductionName where id =@id
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了'' + @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  GlobalProductionName 
	where GlobalProductionName.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了'' + @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end

--set @endTime = getDate()
--set @ms=(select datediff(ms, @beginTime, @endTime))
--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_ChipRegister]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_ChipRegister]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_ChipRegister] 
@ID	int	OUTPUT,
@FormulaID	int,
@AccessType	int,
@Address	int,
@StartBit	int,
@Length	int,
@ChipLine	int,
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr int OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''ChipInfo''
set @OPItemStr = ''ChipRegister:''
set @OPItemNameStr = ltrim(str(@Address))
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  ChipRegister ([FormulaID]
           ,[AccessType]
           ,[Address]
           ,[StartBit]
           ,[Length]
           ,[ChipLine])
	Values(@FormulaID,@AccessType,@Address,@StartBit,@Length,@ChipLine) ;
	set @ID = (Select Ident_Current(''ChipRegister''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  ChipRegister
	set 
	FormulaID=@FormulaID,AccessType=@AccessType,Address=@Address,StartBit=@StartBit,Length=@Length,ChipLine=@ChipLine
	from ChipRegister where id =@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	set @ID =@id;
	end
--delete
else if (@RowState=2)
	BEGIN
	delete  ChipRegister 
	where ChipRegister.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
    set @ID =@id;
	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  Table [dbo].[UserPNAction]    Script Date: 09/21/2015 15:16:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserPNAction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserPNAction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[PNID] [int] NOT NULL,
	[AddPlan] [bit] NOT NULL CONSTRAINT [DF_GlobalUserPNAction_AddPlan]  DEFAULT ('false'),
	[ModifyPN] [bit] NOT NULL CONSTRAINT [DF_GlobalUserPNAction_ModifyPNInfo]  DEFAULT ('false'),
 CONSTRAINT [PK_GlobalUserPNAction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalUserPNAction] UNIQUE NONCLUSTERED 
(
	[PNID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'UserPNAction', N'COLUMN',N'AddPlan'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可以在该PN下新增TestPlan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPNAction', @level2type=N'COLUMN',@level2name=N'AddPlan'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'UserPNAction', N'COLUMN',N'ModifyPN'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可以修改该PN' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPNAction', @level2type=N'COLUMN',@level2name=N'ModifyPN'
GO
SET IDENTITY_INSERT [dbo].[UserPNAction] ON
INSERT [dbo].[UserPNAction] ([ID], [UserID], [PNID], [AddPlan], [ModifyPN]) VALUES (17, 8, 29, 1, 0)
INSERT [dbo].[UserPNAction] ([ID], [UserID], [PNID], [AddPlan], [ModifyPN]) VALUES (22, 11, 29, 0, 1)
INSERT [dbo].[UserPNAction] ([ID], [UserID], [PNID], [AddPlan], [ModifyPN]) VALUES (23, 8, 63, 1, 0)
SET IDENTITY_INSERT [dbo].[UserPNAction] OFF
/****** Object:  Table [dbo].[UserPlanAction]    Script Date: 09/21/2015 15:16:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserPlanAction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserPlanAction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[PlanID] [int] NOT NULL,
	[ModifyPlan] [bit] NOT NULL CONSTRAINT [DF_Table_1_AddPlan]  DEFAULT ('false'),
	[DeletePlan] [bit] NOT NULL CONSTRAINT [DF_Table_1_ModifyPlan1]  DEFAULT ('false'),
	[RunPlan] [bit] NOT NULL CONSTRAINT [DF_GlobalUserPlanAction_RunPlan]  DEFAULT ('false'),
 CONSTRAINT [PK_GlobalUserPlanAction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalUserPlanAction] UNIQUE NONCLUSTERED 
(
	[PlanID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'UserPlanAction', N'COLUMN',N'ModifyPlan'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可以修改该TestPlan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPlanAction', @level2type=N'COLUMN',@level2name=N'ModifyPlan'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'UserPlanAction', N'COLUMN',N'DeletePlan'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可以删除该TestPlan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPlanAction', @level2type=N'COLUMN',@level2name=N'DeletePlan'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'UserPlanAction', N'COLUMN',N'RunPlan'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可以运行该TestPlan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPlanAction', @level2type=N'COLUMN',@level2name=N'RunPlan'
GO
SET IDENTITY_INSERT [dbo].[UserPlanAction] ON
INSERT [dbo].[UserPlanAction] ([ID], [UserID], [PlanID], [ModifyPlan], [DeletePlan], [RunPlan]) VALUES (4, 11, 161, 1, 1, 1)
INSERT [dbo].[UserPlanAction] ([ID], [UserID], [PlanID], [ModifyPlan], [DeletePlan], [RunPlan]) VALUES (5, 11, 162, 1, 1, 1)
INSERT [dbo].[UserPlanAction] ([ID], [UserID], [PlanID], [ModifyPlan], [DeletePlan], [RunPlan]) VALUES (6, 11, 56, 1, 0, 0)
INSERT [dbo].[UserPlanAction] ([ID], [UserID], [PlanID], [ModifyPlan], [DeletePlan], [RunPlan]) VALUES (7, 11, 58, 0, 1, 0)
INSERT [dbo].[UserPlanAction] ([ID], [UserID], [PlanID], [ModifyPlan], [DeletePlan], [RunPlan]) VALUES (8, 8, 164, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[UserPlanAction] OFF
/****** Object:  StoredProcedure [dbo].[Pro_TopoTestPlan]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_TopoTestPlan]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_TopoTestPlan] 
@ID int	OUTPUT,
@PID	int ,
@ItemName	nvarchar(30),
@SWVersion	nvarchar(30),
@HWVersion	nvarchar(30),
@USBPort	tinyint,
@IsChipInitialize	bit,
@IsEEPROMInitialize bit,
@IgnoreBackupCoef	bit,
@SNCheck	bit,
@IgnoreFlag	bit,
@ItemDescription	nvarchar(200),
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo	nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 
declare @VersionMax int; 
set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''ATSPlan''
set @OPItemStr = ''TestPlan:''
set @OPItemNameStr = @ItemName
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  TopoTestPlan ([PID]
			   ,[ItemName]
			   ,[SWVersion]
			   ,[HWVersion]
			   ,[USBPort]
			   ,[IsChipInitialize]
			   ,[IsEEPROMInitialize]
			   ,[IgnoreBackupCoef]
			   ,[SNCheck]
			   ,[IgnoreFlag]
			   ,[ItemDescription],[Version])
	Values(@PID,@ItemName,@SWVersion,@HWVersion,@USBPort,@IsChipInitialize,
		@IsEEPROMInitialize,@IgnoreBackupCoef,@SNCheck,@IgnoreFlag,@ItemDescription,0) ;
	set @ID = (Select Ident_Current(''TopoTestPlan''))
	set @VersionMax =0;
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	set @VersionMax = (select [version] from TopoTestPlan where id =@id)+1;
	update  TopoTestPlan
	set 
	PID=@PID,ItemName=@ItemName,SWVersion=@SWVersion,HWVersion=@HWVersion,USBPort=@USBPort,
		IsChipInitialize=@IsChipInitialize,IsEEPROMInitialize=@IsEEPROMInitialize,
		IgnoreBackupCoef=@IgnoreBackupCoef,SNCheck=@SNCheck,IgnoreFlag=@IgnoreFlag,ItemDescription=@ItemDescription,[Version]=@VersionMax
	from TopoTestPlan where id =@id

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  TopoTestPlan 
	where TopoTestPlan.id=@ID
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalManufactureChipsetInitialize]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalManufactureChipsetInitialize]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_GlobalManufactureChipsetInitialize] 
@ID int OUTPUT,
@PID	int ,
@DriveType	tinyint	,
@ChipLine	tinyint	,
@RegisterAddress	int	,
@Length	tinyint	,
@ItemValue	int	,
@Endianness bit,
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''ProductionInfo''
set @OPItemStr = ''ChipsetInitialize:''
set @OPItemNameStr = ''DriveType='' + ltrim(str(@DriveType)) + ''+ChipLine='' + ltrim(str(@ChipLine))
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  GlobalManufactureChipsetInitialize ([PID]
			   ,[DriveType]
			   ,[ChipLine]
			   ,[RegisterAddress]
			   ,[Length]
			   ,[ItemValue]
			   ,[Endianness])
	Values(@PID,@DriveType,@ChipLine,@RegisterAddress,@Length,@ItemValue,@Endianness) ;
	set @ID = (Select Ident_Current(''GlobalManufactureChipsetInitialize''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
end
--Update
else if (@RowState=1)
	BEGIN
	update  GlobalManufactureChipsetInitialize
	set 
	PID=@PID,DriveType= @DriveType,ChipLine=@ChipLine,RegisterAddress=@RegisterAddress,
	[Length]=@Length,ItemValue=@ItemValue,Endianness=@Endianness
	from GlobalManufactureChipsetInitialize where id =@id

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

end
--delete
else if (@RowState=2)
	BEGIN
	delete  GlobalManufactureChipsetInitialize 
	where GlobalManufactureChipsetInitialize.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalManufactureChipsetControl]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalManufactureChipsetControl]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_GlobalManufactureChipsetControl] 
@ID int OUTPUT,
@PID	int ,
@ItemName	nvarchar(20),
@ModuleLine	tinyint	,
@ChipLine	tinyint	,
@DriveType	tinyint	,
@RegisterAddress	int	,
@Length	tinyint	,
@Endianness bit,
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @BlockType nvarchar(50)
set @BlockType=''ProductionInfo''

declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
set @OPItemStr = ''ChipsetControl:''
set @OPItemNameStr = ''ItemName='' + @ItemName + ''+ModuleLine='' + ltrim(str(@ModuleLine)) + ''+ChipLine='' + ltrim(str(@ChipLine))
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  GlobalManufactureChipsetControl ([PID]
			   ,[ItemName]
			   ,[ModuleLine]
			   ,[ChipLine]
			   ,[DriveType]
			   ,[RegisterAddress]
			   ,[Length]
			   ,[Endianness])
	Values(@PID,@ItemName,@ModuleLine,@ChipLine,@DriveType,@RegisterAddress,@Length,@Endianness) ;
	set @ID = (Select Ident_Current(''GlobalManufactureChipsetControl''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
end
--Update
else if (@RowState=1)
	BEGIN
	update  GlobalManufactureChipsetControl
	set 
	PID=@PID,ItemName=@ItemName,ModuleLine=@ModuleLine,ChipLine=@ChipLine,
	DriveType= @DriveType,RegisterAddress=@RegisterAddress,
	[Length]=@Length,Endianness=@Endianness
	from GlobalManufactureChipsetControl where id =@id

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  GlobalManufactureChipsetControl 
	where GlobalManufactureChipsetControl.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_PNChipMap]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_PNChipMap]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_PNChipMap] 
@ID	int	OUTPUT,
@PNID	int,
@ChipID	int,
@ChipRoleID	int,
@ChipDirection	bit,
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr int OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''ChipInfo''
set @OPItemStr = ''PNChipMap:''
set @OPItemNameStr = (select ChipBaseInfo.ItemName from ChipBaseInfo where ID = @ChipID)
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  PNChipMap ([PNID]
           ,[ChipID]
           ,[ChipRoleID]
           ,[ChipDirection])
	Values(@PNID,@ChipID,@ChipRoleID,@ChipDirection) ;
	set @ID = (Select Ident_Current(''PNChipMap''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  PNChipMap
	set 
	[PNID]=@PNID,[ChipID]=@ChipID,[ChipRoleID]=@ChipRoleID,[ChipDirection]=@ChipDirection
	from PNChipMap where id =@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	set @ID =@id;
	end
--delete
else if (@RowState=2)
	BEGIN
	delete  PNChipMap 
	where PNChipMap.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
    set @ID =@id;
	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_TopoMSAEEPROMSet]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_TopoMSAEEPROMSet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_TopoMSAEEPROMSet] 
@ID int	OUTPUT,
@PID	int ,
@ItemName	nvarchar(50),
@Data0	nvarchar(512),
@CRCData0	tinyint,
@Data1	nvarchar(512),
@CRCData1	tinyint,
@Data2	nvarchar(512),
@CRCData2	tinyint,
@Data3	nvarchar(512),
@CRCData3	tinyint,
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo	nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 
set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''ProductionInfo''
set @OPItemStr = ''TopoMSAEEPROMSet:''
set @OPItemNameStr = @ItemName
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  TopoMSAEEPROMSet ([PID]
           ,[ItemName]
           ,[Data0]
           ,[CRCData0]
           ,[Data1]
           ,[CRCData1]
           ,[Data2]
           ,[CRCData2]
           ,[Data3]
           ,[CRCData3])
	Values(@PID,@ItemName,@Data0,@CRCData0,@Data1,@CRCData1,@Data2,@CRCData2,@Data3,@CRCData3) ;	
	set @ID = (Select Ident_Current(''TopoMSAEEPROMSet''))
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  TopoMSAEEPROMSet
	set 
	PID=@PID,ItemName=@ItemName,
	Data0=@Data0,CRCData0=@CRCData0,
	Data1=@Data1,CRCData1=@CRCData1,
	Data2=@Data2,CRCData2=@CRCData2,
	Data3=@Data3,CRCData3=@CRCData3
	from TopoMSAEEPROMSet where id =@id
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  TopoMSAEEPROMSet 
	where TopoMSAEEPROMSet.id=@ID
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_UserPNAction]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_UserPNAction]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[Pro_UserPNAction] 
@ID int	OUTPUT,
@UserID	int,
@PNID	int,
@AddPlan	bit,
@ModifyPN	bit,
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo	nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 
declare @VersionMax int; 
set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
declare @UserName nvarchar(50)
set @BlockType=''ProductionInfo''
set @OPItemStr = ''PN:''
set @OPItemNameStr = (select PN from GlobalProductionName where ID=@PNID);
set @UserName= (select LoginName from UserInfo where ID=@UserID);
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  UserPNAction ([UserID]
           ,[PNID]
           ,[AddPlan]
           ,[ModifyPN])
	Values(@UserID,@PNID,@AddPlan,@ModifyPN) ;
	set @ID = (Select Ident_Current(''UserPNAction''))
	
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的人员:''+ @UserName +''的权限信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	
	update  UserPNAction
	set 
	UserID=@UserID,PNID=@PNID,AddPlan=@AddPlan,ModifyPN=@ModifyPN
	from UserPNAction where id =@ID
		
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的人员:''+ @UserName +''的权限信息'',@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  UserPNAction 
	where UserPNAction.id=@ID	
	
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的人员:''+ @UserName +''的权限信息'',@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch

' 
END
GO
/****** Object:  Table [dbo].[ChannelMap]    Script Date: 09/21/2015 15:16:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChannelMap]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ChannelMap](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PNChipID] [int] NOT NULL CONSTRAINT [DF_Table_1_PNID]  DEFAULT ((0)),
	[ModuleLine] [int] NOT NULL CONSTRAINT [DF_Table_1_ChipID]  DEFAULT ((0)),
	[ChipLine] [int] NOT NULL CONSTRAINT [DF_ChannelMap_ChipLine]  DEFAULT ((0)),
 CONSTRAINT [PK_ChannelMap] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_ChannelMap] UNIQUE NONCLUSTERED 
(
	[PNChipID] ASC,
	[ModuleLine] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_ChannelMap]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_ChannelMap]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_ChannelMap] 
@ID	int	OUTPUT,
@PNChipID	int,
@ModuleLine	int,
@ChipLine	int,
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr int OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''ChipInfo''
set @OPItemStr = ''ChannelMap:''
set @OPItemNameStr = ltrim(str(@ModuleLine))
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  ChannelMap ([PNChipID]
           ,[ModuleLine]
           ,[ChipLine])
	Values(@PNChipID,@ModuleLine,@ChipLine) ;
	set @ID = (Select Ident_Current(''ChannelMap''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  ChannelMap
	set 
	PNChipID=@PNChipID,ModuleLine=@ModuleLine,ChipLine=@ChipLine
	from ChannelMap where id =@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	set @ID =@id;
	end
--delete
else if (@RowState=2)
	BEGIN
	delete  ChannelMap 
	where ChannelMap.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
    set @ID =@id;
	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_UserPlanAction]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_UserPlanAction]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[Pro_UserPlanAction] 
@ID int	OUTPUT,
@UserID	int,
@PlanID	int,
@ModifyPlan	bit,
@DeletePlan	bit,
@RunPlan	bit,
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo	nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 
declare @VersionMax int; 
set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
declare @UserName nvarchar(50)
set @BlockType=''ATSPlan''
set @OPItemStr = ''TestPlan:''
set @OPItemNameStr = (select ItemName from TopoTestPlan where ID=@PlanID);
set @UserName= (select LoginName from UserInfo where ID=@UserID);
set @VersionMax = (select [version] from TopoTestPlan where id =@PlanID)+1;
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  UserPlanAction ([UserID]
           ,[PlanID]
           ,[ModifyPlan]
           ,[DeletePlan]
           ,[RunPlan])
	Values(@UserID,@PlanID,@ModifyPlan,@DeletePlan,@RunPlan) ;
	set @ID = (Select Ident_Current(''UserPlanAction''))
	--新增更新TestPlan的Version	
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@PlanID
	
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的人员:''+ @UserName +''的权限信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	
	update  UserPlanAction
	set 
	UserID=@UserID,PlanID=@PlanID,ModifyPlan=@ModifyPlan,DeletePlan=@DeletePlan,RunPlan=@RunPlan
	from UserPlanAction where id =@ID
	
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@PlanID
	
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的人员:''+ @UserName +''的权限信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  UserPlanAction 
	where UserPlanAction.id=@ID
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@PlanID
	
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的人员:''+ @UserName +''的权限信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch

' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_TopoTestControl]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_TopoTestControl]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_TopoTestControl] 
@ID int	OUTPUT,
@PID	int	,
@ItemName	nvarchar(50),
@SEQ	int,
@Channel	tinyint,
@Temp	real,
@Vcc	real,
@Pattent	tinyint,
@DataRate nvarchar(50),
@CtrlType	tinyint,
@TempOffset	real,
@TempWaitTimes real,
@IgnoreFlag	bit,
@ItemDescription	nvarchar(200),
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo	nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

declare @VersionMax int;
declare @TestPlanID int; 
set @TestPlanID = @PID; 
set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''ATSPlan''
set @OPItemStr = ''TestControl:''
set @OPItemNameStr = @ItemName
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  TopoTestControl ([PID]
           ,[ItemName]
           ,[SEQ]
           ,[Channel]
           ,[Temp]
           ,[Vcc]
           ,[Pattent]
           ,[DataRate]
           ,[CtrlType]
           ,[TempOffset]
           ,[TempWaitTimes]
           ,[ItemDescription]
           ,[IgnoreFlag])
	Values(@PID,@ItemName,@SEQ,@Channel,@Temp,@Vcc,@Pattent,@DataRate,@CtrlType,
			@TempOffset,@TempWaitTimes,@ItemDescription,@IgnoreFlag) ;
	set @ID = (Select Ident_Current(''TopoTestControl''))
	--新增更新TestPlan的Version	
	set @VersionMax = (select [version] from TopoTestPlan where id =@TestPlanID)+1;
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@TestPlanID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  TopoTestControl
	set 
	PID=@PID,ItemName=@ItemName,SEQ=@SEQ,Channel=@Channel,Temp=@Temp,Vcc=@Vcc,Pattent=@Pattent,DataRate=@DataRate,
		CtrlType=@CtrlType,TempOffset=@TempOffset,TempWaitTimes=@TempWaitTimes,ItemDescription=@ItemDescription,IgnoreFlag=@IgnoreFlag
	from TopoTestControl where id =@id
	--新增更新TestPlan的Version	
	set @VersionMax = (select [version] from TopoTestPlan where id =@TestPlanID)+1;
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@TestPlanID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  TopoTestControl 
	where TopoTestControl.id=@ID
	--新增更新TestPlan的Version	
	set @VersionMax = (select [version] from TopoTestPlan where id =@TestPlanID)+1;
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@TestPlanID
	
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_TopoPNSpecsParams]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_TopoPNSpecsParams]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_TopoPNSpecsParams] 
@ID int OUTPUT,
@PID	int ,
@SID	int,
@Typical	float,
@SpecMin	float,
@SpecMax	float,
@Channel	tinyint,
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 
declare @VersionMax int; 
declare @TestPlanID int; 
set @TestPlanID = @PID; 
set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''ATSPlan''
set @OPItemStr = ''SpecsParam:''
set @OPItemNameStr =(select itemName from GlobalSpecs where ID = @SID)
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  TopoPNSpecsParams ([PID]
			   ,[SID]
			   ,[Typical]
			   ,[SpecMin]
			   ,[SpecMax]
			   ,[Channel])
	Values(@PID,@SID,@Typical,@SpecMin,@SpecMax,@Channel) ;
	set @ID = (Select Ident_Current(''TopoPNSpecsParams''))
--新增更新TestPlan的Version
	set @VersionMax = (select [version] from TopoTestPlan where id =@TestPlanID)+1;
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@TestPlanID
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  TopoPNSpecsParams
	set 
	PID=@PID,SID=@SID,Typical=@Typical,SpecMin=@SpecMin,SpecMax=@SpecMax,Channel=@Channel
	from TopoPNSpecsParams where id =@id
--新增更新TestPlan的Version
	set @VersionMax = (select [version] from TopoTestPlan where id =@TestPlanID)+1;
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@TestPlanID
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  TopoPNSpecsParams 
	where TopoPNSpecsParams.id=@ID
--新增更新TestPlan的Version
	set @VersionMax = (select [version] from TopoTestPlan where id =@TestPlanID)+1;
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@TestPlanID
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_TopoManufactureConfigInit]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_TopoManufactureConfigInit]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_TopoManufactureConfigInit] 
@ID int OUTPUT,
@PID	int ,
@SlaveAddress	int,
@Page	tinyint,
@StartAddress	int,
@Length	tinyint,
@ItemValue	int,
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo	nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 
declare @VersionMax int; 
declare @TestPlanID int; 
set @TestPlanID = @PID; 
set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''ATSPlan''
set @OPItemStr = ''ConfigInit:''
set @OPItemNameStr = ''SlaveAddr=''+Ltrim(str(@SlaveAddress)) +''+Page='' + Ltrim(str(@Page)) +''+Addr='' +Ltrim(str(@StartAddress))
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  TopoManufactureConfigInit ([PID]
			   ,[SlaveAddress]
			   ,[Page]
			   ,[StartAddress]
			   ,[Length]
			   ,[ItemValue])
	Values(@PID,@SlaveAddress,@Page,@StartAddress,@Length,@ItemValue) ;
	set @ID = (Select Ident_Current(''TopoManufactureConfigInit''))
--新增更新TestPlan的Version
	set @VersionMax = (select [version] from TopoTestPlan where id =@TestPlanID)+1;
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@TestPlanID
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  TopoManufactureConfigInit
	set 
	PID=@PID,SlaveAddress=@SlaveAddress,Page=@Page,StartAddress=@StartAddress,[Length]=@Length,ItemValue=@ItemValue
	from TopoManufactureConfigInit where id =@id
--新增更新TestPlan的Version
	set @VersionMax = (select [version] from TopoTestPlan where id =@TestPlanID)+1;
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@TestPlanID
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  TopoManufactureConfigInit 
	where TopoManufactureConfigInit.id=@ID
--新增更新TestPlan的Version
	set @VersionMax = (select [version] from TopoTestPlan where id =@TestPlanID)+1;
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@TestPlanID
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_TopoEquipmentWithParams]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_TopoEquipmentWithParams]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Pro_TopoEquipmentWithParams] 
@ID int OUTPUT,
@PID int ,
@GID int,
@SEQ	int,
@Role	tinyint,
@Params varchar(max),
@RowState tinyint,
@OPlogPID int,
@TracingInfo nvarchar(max),
@myErr int OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

declare @VersionMax int; 
declare @TestPlanID int; 
set @TestPlanID = @PID; 
set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''ATSPlan''
set @OPItemStr = ''Equipment:''
set @OPItemNameStr = (select itemName from GlobalAllEquipmentList where ID =@GID)
--AddNew
if (@RowState=0)
BEGIN
	INSERT INTO  TopoEquipment ([PID]
			   ,[GID]
			   ,[Seq]
			   ,[Role])
	Values(@PID,@GID,@Seq,@Role) ;
	set @ID = (Select Ident_Current(''TopoEquipment''));
	if (LEN(@Params)>0)	
	BEGIN
		INSERT INTO  TopoEquipmentParameter select * from f_splitModelstr(@ID,@Params,''|'',''#'');
	end
	--新增更新TestPlan的Version	
	set @VersionMax = (select [version] from TopoTestPlan where id =@TestPlanID)+1;
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@TestPlanID
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);
end
--Update
else if (@RowState=1)
BEGIN
	update  TopoEquipment
	set 
	PID=@PID,GID=@GID,Seq=@Seq,[role]=@Role
	from TopoEquipment where id =@id
	
	if (LEN(@Params)>0)	
	BEGIN
		--利用游标进行更新资料
		declare MyCursor cursor    --声明一个游标，查询满足条件的数据
			local for select * from f_splitModelstr(@ID,@Params,''|'',''#'')
		open MyCursor    --打开
	    
		declare @MyPID int, @MyGID int ,@MyValue  nvarchar(max)    --声明一个变量，用于读取游标中的值
		   fetch next from MyCursor into @MyPID,@MyGID,@MyValue
	    
		while @@fetch_status=0    --循环读取
			begin
			--print str(@MyPID  + ''_'' + @MyGID  + ''_'' + @MyValue)        
			declare @queryExistStrs nvarchar(max)
			IF  EXISTS (select * from TopoEquipmentParameter where PID=@MyPID and GID =@MyGID)
				BEGIN
					update TopoEquipmentParameter set ItemValue=@MyValue from TopoEquipmentParameter where PID=@MyPID and GID =@MyGID
				end 
			else
				BEGIN
					INSERT INTO  TopoEquipmentParameter (PID,GID,ItemValue) values(@MyPID,@MyGID,@MyValue)
				end 
			fetch next from MyCursor into @MyPID,@MyGID,@MyValue
			end    
		close MyCursor    --关闭    
		deallocate MyCursor    --删除	
    end 
    --新增更新TestPlan的Version	
	set @VersionMax = (select [version] from TopoTestPlan where id =@TestPlanID)+1;
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@TestPlanID
    
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);

end
--delete
else if (@RowState=2)
BEGIN
delete  TopoEquipment 
where TopoEquipment.id=@ID
--新增更新TestPlan的Version	
	set @VersionMax = (select [version] from TopoTestPlan where id =@TestPlanID)+1;
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@TestPlanID

INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);

end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  Table [dbo].[GlobalAllEquipmentParamterList]    Script Date: 09/21/2015 15:16:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalAllEquipmentParamterList]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GlobalAllEquipmentParamterList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL CONSTRAINT [DF_GlobalAllEquipmentParamterList_FieldName]  DEFAULT (''),
	[ItemType] [nvarchar](10) NOT NULL CONSTRAINT [DF_GlobalAllEquipmentParamterList_TypeofValue]  DEFAULT (''),
	[ItemValue] [nvarchar](255) NOT NULL CONSTRAINT [DF_GlobalAllEquipmentParamterList_DefaultValue]  DEFAULT (''),
	[NeedSelect] [bit] NOT NULL CONSTRAINT [DF_GlobalAllEquipmentParamterList_NeedSelect]  DEFAULT ('false'),
	[Optionalparams] [nvarchar](max) NOT NULL CONSTRAINT [DF_GlobalAllEquipmentParamterList_Optionalparams]  DEFAULT (''),
	[ItemDescription] [nvarchar](500) NOT NULL CONSTRAINT [DF_GlobalAllEquipmentParamterList_Description]  DEFAULT (''),
 CONSTRAINT [PK_GlobalAllEquipmentParamterList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalAllEquipmentParamterList] UNIQUE NONCLUSTERED 
(
	[PID] ASC,
	[ItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[GlobalAllEquipmentParamterList] ON
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (1, 1, N'Addr', N'byte', N'5', 0, N'', N'GPIB地址（1~30）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (2, 1, N'IOType', N'string', N'GPIB', 0, N'', N'接口类型（GPIB或USB）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (3, 1, N'Reset', N'bool', N'false', 0, N'', N'是否复位(true是;false否)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (4, 1, N'Name', N'string', N'E3631', 0, N'', N'仪器名称（E3631）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (5, 1, N'DutChannel', N'string', N'1', 0, N'', N'DUT通道')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (6, 1, N'OptSourceChannel', N'byte', N'2', 0, N'', N'光源通道')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (7, 1, N'DutVoltage', N'double', N'3.3', 0, N'', N'DUT电压值(单位V)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (8, 1, N'DutCurrent', N'double', N'1.5', 0, N'', N'DUT限流(单位A)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (9, 1, N'OptVoltage', N'double', N'3.3', 0, N'', N'光源电压(单位V)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (10, 1, N'OptCurrent', N'double', N'1.5', 0, N'', N'光源限流(单位A)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (11, 2, N'Addr', N'byte', N'20', 0, N'', N'GPIB地址（1~30）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (12, 2, N'IOType', N'string', N'GPIB', 0, N'', N'接口类型（GPIB或USB）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (13, 2, N'Reset', N'bool', N'false', 0, N'', N'是否复位(true是，false否)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (14, 2, N'Name', N'string', N'AQ2211Atten', 0, N'', N'仪器名称（AQ2211Atten）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (15, 2, N'TOTALCHANNEL', N'byte', N'4', 0, N'', N'模块的总通道数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (16, 2, N'AttValue', N'double', N'5', 0, N'', N'衰减值')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (17, 2, N'AttSlot', N'string', N'1,2,3,4', 0, N'', N'插槽数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (18, 2, N'WAVELENGTH', N'string', N'1270,1290,1310,1330', 0, N'', N'通道1,2,3,4对应波长数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (22, 2, N'AttChannel', N'byte', N'1', 0, N'', N'衰减器通道数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (23, 3, N'Addr', N'byte', N'15', 0, N'', N'GPIB地址（1~30）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (24, 3, N'IOType', N'string', N'GPIB', 0, N'', N'接口类型（GPIB或USB）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (25, 3, N'Reset', N'bool', N'false', 0, N'', N'是否复位(true是;false否)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (26, 3, N'Name', N'string', N'D86100', 0, N'', N'仪器名称（D86100）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (27, 3, N'OptChannel', N'byte', N'1', 0, N'', N'光通道号（1-4）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (28, 3, N'ElecChannel', N'byte', N'2', 0, N'', N'电通道号（1-4）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (29, 3, N'Scale', N'double', N'0.00095', 0, N'', N'周期')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (30, 3, N'Offset', N'double', N'0.00001', 0, N'', N'偏移量')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (31, 3, N'opticalMaskName', N'string', N'10GbE_10_3125_May02.msk', 0, N'', N'眼图模板 10GbE_10_3125_May02.msk')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (32, 3, N'DcaAtt', N'double', N'1.8', 0, N'', N'衰减补偿')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (33, 3, N'FilterFreq', N'double', N'10.3125', 0, N'', N'滤波器速率（10.3125 Gb/s）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (34, 3, N'Percentage', N'byte', N'0', 0, N'', N'EMM（0-100）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (35, 3, N'DcaDataRate', N'double', N'10312500000', 0, N'', N'数据速率（10312500000）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (36, 3, N'DcaWavelength', N'byte', N'2', 0, N'', N'波长选择（1 850,2 1310,3 1550 default 850）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (37, 3, N'DcaThreshold', N'string', N'80,50,20', 0, N'', N'门限80,50,20')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (38, 3, N'TriggerBwlimit', N'byte', N'2', 0, N'', N'触发带宽限制（ 0 HIGH\1 LOW\2 DIV）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (39, 4, N'ElecSwitchChannel', N'byte', N'1', 0, N'', N'电开关通道数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (40, 4, N'Name', N'string', N'ElectricalSwitch', 0, N'', N'仪器名称（ElectricalSwitch）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (41, 4, N'Addr', N'byte', N'0', 0, N'', N'USB地址 （0）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (42, 4, N'IOType', N'string', N'USB', 0, N'', N'接口类型（GPIB或USB）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (43, 4, N'Reset', N'bool', N'false', 0, N'', N'是否复位(true是;false否)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (44, 5, N'Addr', N'byte', N'14', 0, N'', N'GPIB地址（1~30）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (45, 5, N'IOType', N'string', N'GPIB', 0, N'', N'接口类型（GPIB或USB）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (46, 5, N'Reset', N'bool', N'false', 0, N'', N'是否复位(true是;false否)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (47, 5, N'Name', N'string', N'N490xPPG', 0, N'', N'仪器名称（N490XPPG）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (48, 5, N'PRBS', N'byte', N'31', 0, N'', N'PRBS码型（7,15,23,31..）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (49, 5, N'BertDataRate', N'double', N'10312500000', 0, N'', N'误码仪速率')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (50, 5, N'TriggerDRatio', N'double', N'16', 0, N'', N'触发比率 2')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (51, 5, N'TriggerMode', N'string', N'Custom', 0, N'', N'触发模式 （DCL，PATT）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (52, 5, N'ClockHigVoltage', N'double', N'0.5', 0, N'', N'时钟高电平电压值')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (53, 5, N'ClockLowVoltage', N'double', N'-0.5', 0, N'', N'时钟低电平电压值')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (54, 5, N'DataHigVoltage', N'double', N'0.5', 0, N'', N'数据高电平电压值')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (55, 5, N'DataLowVoltage', N'double', N'-0.5', 0, N'', N'数据低电平电压值')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (56, 6, N'Addr', N'byte', N'5', 0, N'', N'GPIB地址（1~30）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (57, 6, N'IOType', N'string', N'GPIB', 0, N'', N'接口类型（GPIB或USB）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (58, 6, N'Reset', N'bool', N'false', 0, N'', N'是否复位(true是;false否)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (59, 6, N'Name', N'string', N'N490xED', 0, N'', N'仪器名称（N490XED）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (60, 6, N'PRBS', N'byte', N'31', 0, N'', N'(7,15,23,31..)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (61, 6, N'CDRSwitch', N'bool', N'false', 0, N'', N'CDR 开关')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (62, 6, N'CDRFreq', N'double', N'10312500000', 0, N'', N'CDR频率')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (63, 7, N'Addr', N'byte', N'20', 0, N'', N'GPIB地址')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (64, 7, N'IOType', N'string', N'GPIB', 0, N'', N'GPIB或USB 接口类型')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (65, 7, N'Reset', N'bool', N'false', 0, N'', N'是否复位(true是;false否)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (66, 7, N'Name', N'string', N'AQ2011OpticalSwitch', 0, N'', N'仪器名字AQ2011OpticalSwitch')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (67, 7, N'OpticalSwitchSlot', N'byte', N'3', 0, N'', N'光开关插槽数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (68, 7, N'SwitchChannel', N'byte', N'1', 0, N'', N'光开关所在通道数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (69, 7, N'ToChannel', N'byte', N'1', 0, N'', N'要切换的通道数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (70, 8, N'Addr', N'byte', N'', 0, N'', N'GPIB地址（1~30）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (71, 8, N'IOType', N'string', N'', 0, N'', N'接口类型（GPIB或USB）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (72, 8, N'Reset', N'bool', N'false', 0, N'', N'是否复位(true是;false否)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (73, 8, N'Name', N'string', N'', 0, N'', N'仪器名称（AQ2011PowerMeter）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (74, 8, N'PowerMeterSlot', N'byte', N'', 0, N'', N'功率计插槽数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (75, 8, N'PowerMeterWavelength', N'u32', N'', 0, N'', N'功率计波长')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (77, 8, N'UnitType', N'byte', N'', 0, N'', N'功率单位（0 "dBm",1 "W"）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (78, 9, N'Addr', N'byte', N'23', 0, N'', N'GPIB地址（1~30）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (79, 9, N'IOType', N'string', N'GPIB', 0, N'', N'接口类型（GPIB或USB）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (80, 9, N'Reset', N'bool', N'false', 0, N'', N'是否复位(true是;false否)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (81, 9, N'Name', N'string', N'TPO4300', 0, N'', N'仪器名称（TPO4300）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (82, 9, N'FLSE', N'int', N'14', 0, N'', N'流量设置')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (83, 9, N'ULIM', N'double', N'90', 0, N'', N'温度上限')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (84, 9, N'LLIM', N'double', N'-20', 0, N'', N'温度上限')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (85, 9, N'Sensor', N'byte', N'1', 0, N'', N'温度传感器选择（0 No Sensor,1 T,2 k,3 rtd,4 diode ）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (86, 10, N'Addr', N'string', N'7', 0, N'', N'地址')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (87, 10, N'IOType', N'string', N'GPIB', 0, N'', N'接口类型:GPIB or USB')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (88, 10, N'Reset', N'bool', N'false', 0, N'', N'是否需要复位设备，false=不需要，true=需要')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (89, 10, N'Name', N'string', N'FLEX86100', 0, N'', N'仪器名称  FLEX86100')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (90, 10, N'configFilePath', N'string', N'1', 0, N'', N'仪器内部配置文件地址')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (91, 10, N'FlexDcaDataRate', N'double', N'25.78125e+9', 0, N'', N'示波器速率')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (92, 10, N'FilterSwitch', N'byte', N'1', 0, N'', N'滤波器开关，1=开，0=关
')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (93, 10, N'FlexFilterFreq', N'double', N'10.3125', 0, N'', N'滤波器速率默认为 10.3125 或 25.78125')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (94, 10, N'triggerSource', N'byte', N'0', 0, N'', N'触发源选择（0=FrontPannel,1=FreeRun)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (95, 10, N'FlexTriggerBwlimit', N'byte', N'2', 0, N'', N'触发信号带宽（0=FILTered，1=EDGE，2=CLOCK）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (96, 10, N'opticalSlot', N'byte', N'1', 0, N'', N'光口所处槽位')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (97, 10, N'elecSlot', N'byte', N'2', 0, N'', N'电口所处槽位')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (98, 10, N'FlexOptChannel', N'string', N'1A,1B,1C,1D', 0, N'', N'光口通道（1A,1B,1C,1D）根据opticalSlot确定')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (99, 10, N'FlexElecChannel', N'string', N'1A,1B,1C,1D', 0, N'', N'口通道（1A,1B,1C,1D）根据Slot确定')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (100, 10, N'FlexDcaWavelength', N'byte', N'2', 0, N'', N'波长选择波长选择0=850,1=1310,2=1550
')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (101, 10, N'opticalAttSwitch', N'byte', N'1', 0, N'', N'光口补偿开关')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (102, 10, N'FlexDcaAtt', N'double', N'0', 0, N'', N'光口补偿值（单位dB)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (103, 10, N'erFactor', N'double', N'0', 0, N'', N'ER修正值（单位%）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (104, 10, N'FlexScale', N'double', N'300', 0, N'', N'屏幕显示比例(单位uW/div）')
GO
print 'Processed 100 total records'
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (105, 10, N'FlexOffset', N'double', N'300', 0, N'', N'屏幕显示偏移(单位uW)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (106, 10, N'Threshold', N'byte', N'0', 0, N'', N'RiseFallTime阈值点（0=80,50,20);(1=90,50,10)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (107, 10, N'reference', N'byte', N'0', 0, N'', N'RiseFallTime参考点（0=OneZero，1=TopBase）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (108, 10, N'precisionTimebaseModuleSlot', N'byte', N'3', 0, N'', N'精准时基单元槽位')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (109, 10, N'precisionTimebaseSynchMethod', N'byte', N'1', 0, N'', N'精准时基同步方式（0=OLIN，1=FAST）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (110, 10, N'precisionTimebaseRefClk', N'double', N'6.445e+9', 0, N'', N'精准时基单元参考时钟（单位bps）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (111, 10, N'rapidEyeSwitch', N'byte', N'1', 0, N'', N'快速眼图模式开关')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (112, 10, N'marginType', N'byte', N'1', 0, N'', N'模板余量测试自动手动选择（0=手动，1=自动）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (113, 10, N'marginHitType', N'byte', N'0', 0, N'', N'自动模板余量测试判决方式选择（0=碰撞点数，1=碰撞比例）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (114, 10, N'marginHitRatio', N'double', N'5e-5', 0, N'', N'模板余量测试自动碰撞比例')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (115, 10, N'marginHitCount', N'int', N'0', 0, N'', N'模板余量测试自动碰撞数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (116, 10, N'acqLimitType', N'byte', N'0', 0, N'', N'眼图累积方式选择（0=wavefors,1=samples)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (117, 10, N'acqLimitNumber', N'int', N'100', 0, N'', N'眼图累积数量')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (118, 10, N'opticalMaskName', N'string', N'c:\scope\masks\10GBE_10_3125_MAY02.MSK', 0, N'', N'光眼图模板名称')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (119, 10, N'elecMaskName', N'string', N'c:\scope\masks\10GBE_10_3125_MAY02.MSK', 0, N'', N'电眼图模板名称')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (122, 11, N'Addr', N'string', N'1', 0, N'', N'地址')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (123, 11, N'IOType', N'string', N'GPIB', 0, N'', N'接口类型:GPIB,usb')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (124, 11, N'Reset', N'bool', N'false', 0, N'', N'是否需要复位')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (125, 11, N'Name', N'string', N'MP1800PPG', 0, N'', N'仪器名称:MP1800PPG')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (126, 11, N'dataRate', N'string', N'25.78125', 0, N'', N'PPG速率（Gbps)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (127, 11, N'dataLevelGuardAmpMax', N'double', N'1', 0, N'', N'输出保护幅度最大值（单位mV')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (128, 11, N'dataLevelGuardOffsetMax', N'double', N'0', 0, N'', N'输出保护最大偏移量（单位mV）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (129, 11, N'dataLevelGuardOffsetMin', N'double', N'0', 0, N'', N'输出保护最小偏移量（单位mV）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (130, 11, N'dataLevelGuardSwitch', N'byte', N'1', 0, N'', N'输出保护开关')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (131, 11, N'dataAmplitude', N'double', N'0', 0, N'', N'输出单端幅度（单位mV）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (132, 11, N'dataCrossPoint', N'double', N'50', 0, N'', N'输出数据信号交叉点')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (133, 11, N'configFilePath', N'string', N'""', 0, N'', N'仪器中配置文件的地址')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (134, 11, N'slot', N'byte', N'1', 0, N'', N'PPG所处槽位')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (135, 11, N'clockSource', N'byte', N'0', 0, N'', N'PPG码型选择（0=PRBS,1=Zero Subsitution,2=Data,3=Alternate,4=Mixed Data,5=Sequense）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (136, 11, N'auxOutputClkDiv', N'byte', N'4', 0, N'', N'辅助输出是时钟信号的几分频')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (137, 11, N'prbsLength', N'byte', N'31', 0, N'', N'PRBS码型长度:31,7')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (138, 11, N'patternType', N'byte', N'0', 0, N'', N'（0=PRBS,1=Zero Subsitution,2=Data,3=Alternate,4=Mixed Data,5=Sequense）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (139, 11, N'dataSwitch', N'byte', N'1', 0, N'', N'数据输出开关')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (140, 11, N'dataTrackingSwitch', N'byte', N'1', 0, N'', N'DATA /DATA跟踪开关')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (141, 11, N'dataAcModeSwitch', N'byte', N'1', 0, N'', N'输出模式选择(0=DC，1=AC）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (142, 11, N'dataLevelMode', N'byte', N'0', 0, N'', N'输出电平模式选择（0=VARiable,1=NECL,2=PCML,3=NCML,4=SCFL,5=LVPecl,6=LVDS200,7=LVDS400）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (143, 11, N'clockSwitch', N'byte', N'1', 0, N'', N'时钟输出开关')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (144, 11, N'outputSwitch', N'byte', N'1', 0, N'', N'总输出开关')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (145, 12, N'Addr', N'string', N'1', 0, N'', N'设备地址')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (146, 12, N'IOType', N'string', N'GPIB', 0, N'', N'接口类型,USB')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (147, 12, N'Reset', N'bool', N'false', 0, N'', N'是否需要复位')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (148, 12, N'Name', N'string', N'MP1800ED', 0, N'', N'仪器名称')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (149, 12, N'slot', N'byte', N'3', 0, N'', N'ED所处槽位')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (150, 12, N'totalChannel', N'byte', N'4', 0, N'', N'ED总通道数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (151, 12, N'currentChannel', N'byte', N'1', 0, N'', N'数据输入接口类型')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (152, 12, N'dataInputInterface', N'byte', N'2', 0, N'', N'数据输入接口类型')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (153, 12, N'prbsLength', N'byte', N'31', 0, N'', N'PRBS码型长度')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (154, 12, N'errorResultZoom', N'byte', N'1', 0, N'', N'0=ZoomIn(显示详细误码信息),1=ZoomOut(只显示误码率和误码数)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (155, 12, N'edGatingMode', N'byte', N'1', 0, N'', N'累积模式（0=REPeat,1=SINGle,2=UNTimed）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (156, 12, N'edGatingUnit', N'byte', N'0', 0, N'', N'累积单位（0=TIME,1=CLOCk,2=ERRor,3=BLOCk）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (157, 12, N'edGatingTime', N'int', N'5', 0, N'', N'累积数量')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (158, 10, N'ERFACTORSWITCH', N'byte', N'1', 0, N'', N'是否启用ER修正因子')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (159, 3, N'WaveformCount', N'int', N'700', 0, N'', N'Waveform累计点
')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (160, 11, N'TotalChannel', N'Byte', N'4', 0, N'', N'MP1800PPG的PPG通道总数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (161, 3, N'elecMaskName', N'string', N'""', 0, N'', N'电眼图模板 10GbE_10_3125_May02.msk')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (162, 1, N'voltageoffset', N'string', N'0', 0, N'', N'voltageoffset')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (163, 1, N'currentoffset', N'string', N'0', 0, N'', N'currentoffset')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (176, 8, N'PowerMeterChannel', N'byte', N'0', 0, N'', N'功率计通道数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (177, 1, N'opendelay', N'int', N'2000', 0, N'', N'Power ON 延迟时间')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (178, 1, N'closedelay', N'int', N'500', 0, N'', N'Power OFF 延迟时间')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (179, 2, N'opendelay', N'int', N'1000', 0, N'', N'opendelay(unit:ms)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (180, 2, N'closedelay', N'int', N'1000', 0, N'', N'closedelay(unit:ms)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (181, 2, N'setattdelay', N'int', N'100', 0, N'', N'setattdelay(unit:ms)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (182, 3, N'setscaledelay', N'int', N'1000', 0, N'', N'setscaledelay(unit:ms)
')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (183, 10, N'flexsetscaledelay', N'int', N'1000', 0, N'', N'flexsetscaledelay(unit:ms)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (184, 6, N'patternfile', N'string', N'@"C:\02"', 0, N'', N'patternfile path')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (185, 12, N'patternfile', N'string', N'@"C:\02"', 0, N'', N'patternfile path')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (186, 5, N'patternfile', N'string', N'@"C:\02"', 0, N'', N'patternfile path')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (187, 11, N'patternfile', N'string', N'@"C:\02"', 0, N'', N'patternfile path')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (190, 13, N'TestEQPrmtr3', N'string', N'3', 0, N'', N'TestEQPrmtr3')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (191, 10, N'DiffSwitch', N'byte', N'0', 0, N'', N'电通道的差分信号状态开启=1 或 关闭=0')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (192, 10, N'BandWidth', N'byte', N'1', 0, N'', N'通道的相应带宽设置:BandWidth索引-->1,2,3,4...')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (193, 14, N'Addr', N'byte', N'20', 0, N'', N'GPIB地址（1~30）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (194, 14, N'IOType', N'string', N'GPIB', 0, N'', N'接口类型（GPIB或USB）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (195, 14, N'Reset', N'bool', N'false', 0, N'', N'是否复位(true是;false否)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (196, 14, N'Name', N'string', N'MAP200Atten', 0, N'', N'仪器名称（MAP200Atten）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (197, 14, N'TOTALCHANNEL', N'byte', N'4', 0, N'', N'模块的总通道数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (198, 14, N'AttValue', N'double', N'20', 0, N'', N'衰减值')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (199, 14, N'AttSlot', N'string', N'1,2,3,4', 0, N'', N'插槽数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (200, 14, N'WAVELENGTH', N'string', N'1270,1290,1310,1330', 0, N'', N'通道1,2,3,4对应波长数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (202, 14, N'opendelay', N'int', N'1000', 0, N'', N'opendelay(unit:ms)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (203, 14, N'closedelay', N'int', N'1000', 0, N'', N'closedelay(unit:ms)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (204, 14, N'setattdelay', N'int', N'1000', 0, N'', N'setattdelay(unit:ms)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (205, 14, N'DeviceChannel', N'string', N'1,1,1,1', 0, N'', N'每个衰减器所用的Device通道')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (206, 15, N'Addr', N'byte', N'20', 0, N'', N'GPIB地址')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (207, 15, N'IOType', N'string', N'GPIB', 0, N'', N'GPIB或USB 接口类型')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (208, 15, N'Reset', N'bool', N'false', 0, N'', N'是否复位(true是;false否)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (209, 15, N'Name', N'string', N'MAP200OpticalSwitch', 0, N'', N'仪器名字MAP200OpticalSwitch')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (210, 15, N'OpticalSwitchSlot', N'byte', N'3', 0, N'', N'光开关插槽数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (211, 15, N'ToChannel', N'byte', N'1', 0, N'', N'要切换的通道数')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (241, 25, N'Addr', N'string', N'1', 0, N'', N'GPIB地址（1~30）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (242, 25, N'IOType', N'string', N'USB', 0, N'', N'接口类型（GPIB或USB）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (243, 25, N'Reset', N'bool', N'false', 0, N'', N'是否复位(true是;false否)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (244, 25, N'Name', N'string', N'Inno25GBertPPG', 0, N'', N'仪器名称（Inno25GBertPPG）')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (245, 25, N'DataRate', N'string', N'25.78', 0, N'', N'PPG速率(Gbps)25.78或28')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (246, 25, N'PPGPattern', N'byte', N'0', 0, N'', N'PPG码型选择(prbs31=0,prbs9/23/15/7=1/5/6/7)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (247, 25, N'Swing', N'byte', N'4', 0, N'', N'Swing(Swing_0=0,Swing_25/50/75/100=1/2/3/4)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (248, 25, N'PPGInvert', N'byte', N'0', 0, N'', N'PPGInvert(Inverted=1,NO_Inverted=0)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (249, 25, N'TotalChannel', N'byte', N'4', 0, N'', N'TotalChannel')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (250, 26, N'Addr', N'string', N'1', 0, N'', N'设备地址')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (251, 26, N'IOType', N'string', N'USB', 0, N'', N'接口类型,USB')
GO
print 'Processed 200 total records'
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (252, 26, N'Reset', N'bool', N'false', 0, N'', N'是否需要复位(true是;false否)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (253, 26, N'Name', N'string', N'Inno25GBertED', 0, N'', N'仪器名称')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (254, 26, N'EDPattern', N'byte', N'2', 0, N'', N'EDPattern(prbs9=0,prbs15=1,prbs31=2)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (255, 26, N'edGatingTime', N'int', N'5', 0, N'', N'累积时间')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (256, 26, N'EDInvert', N'byte', N'0', 0, N'', N'EDInvert(NO_Inverted=0,Inverted=1)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (257, 26, N'dataRate', N'string', N'25.78', 0, N'', N'dataRate(25.78或者28)')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (258, 26, N'TotalChannel', N'byte', N'4', 0, N'', N'TotalChannel')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (260, 25, N'TriggerOutputList', N'string', N'1,2,3,4', 1, N'', N'自制Bert的PPGTrigger通道数组')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (261, 26, N'TriggerOutputList', N'string', N'1,2,3,4', 1, N'', N'自制BERT的EDTrigger通道数组')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (263, 28, N'1', N'double', N'1e3', 0, N'', N'Description')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (271, 28, N'a', N'bool', N'1', 0, N'', N'Description')
INSERT [dbo].[GlobalAllEquipmentParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (274, 28, N'11', N'string', N'NewValue', 0, N'', N'Description')
SET IDENTITY_INSERT [dbo].[GlobalAllEquipmentParamterList] OFF
/****** Object:  Table [dbo].[GlobalTestModelParamterList]    Script Date: 09/21/2015 15:16:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalTestModelParamterList]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GlobalTestModelParamterList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[ItemName] [nvarchar](30) NOT NULL,
	[ItemType] [nvarchar](10) NOT NULL,
	[ItemValue] [nvarchar](255) NOT NULL CONSTRAINT [DF_GlobalTestModelParamterList_ItemValue]  DEFAULT ('-32768'),
	[NeedSelect] [bit] NOT NULL CONSTRAINT [DF_GlobalTestModelParamterList_ItemValue1]  DEFAULT ('false'),
	[Optionalparams] [nvarchar](max) NOT NULL CONSTRAINT [DF_GlobalTestModelParamterList_ItemDescription1]  DEFAULT (''),
	[ItemDescription] [nvarchar](200) NOT NULL CONSTRAINT [DF_GlobalTestModelParamterList_ItemDescription]  DEFAULT (''),
 CONSTRAINT [PK_GlobalTestModelParamterList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_GlobalTestModelParamterList] UNIQUE NONCLUSTERED 
(
	[PID] ASC,
	[ItemName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GlobalTestModelParamterList]') AND name = N'IX_GlobalTestModelParamterList_1')
CREATE NONCLUSTERED INDEX [IX_GlobalTestModelParamterList_1] ON [dbo].[GlobalTestModelParamterList] 
(
	[ItemType] ASC,
	[ItemValue] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GlobalTestModelParamterList] ON
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (1, 1, N'CrossInitializationArray', N'ArrayList', N'1', 0, N'0,0,0,0;1,1,1,1', N'固定crossing值')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (3, 1, N'IbiasTuneStep', N'double', N'1', 0, N'', N'调IBIAS步长(有公式的时候为物理量)')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (4, 1, N'ImodTuneStep', N'byte', N'0', 0, N'', N'调IMOD步长(数字量)')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (6, 1, N'ImodInitializationArray', N'ArrayList', N'0,0,0', 0, N'', N'调整光功率前设置固定的mod值')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (7, 1, N'IBiasInitializationArray', N'ArrayList', N'0,0,0', 0, N'', N'调整ER前设置固定的IBias值')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (8, 1, N'PIDCoefArray', N'ArrayList', N'0,0,0,0', 0, N'', N'四个通道PID系数')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (9, 1, N'SleepTime', N'UInt16', N'50', 0, N'xxx1', N'读取光功率之前的延时时间,单位ms')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (10, 16, N'AttStep', N'double', N'0', 0, N'', N'设置入射光时在灵敏度值的基础上减去该步长')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (11, 16, N'SetPoints', N'ArrayList', N'0,0,0,0', 0, N'', N'一组值，用于设置APD DAC')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (12, 16, N'TuneStep', N'byte', N'0', 0, N'', N'微调APDDAC的步长')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (13, 6, N'IsAdjustLosA', N'Bool', N'true', 0, N'', N'是否调整LOSA')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (14, 6, N'LOSA_DAC_Start', N'UInt16', N'0', 0, N'', N'LOSA寄存器起始值')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (15, 6, N'LOSA_DAC_Max', N'UInt16', N'3', 0, N'', N'LOSA寄存器上限')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (16, 6, N'LOSA_DAC_Min', N'UInt16', N'0', 0, N'', N'LOSA寄存器下限')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (17, 6, N'LOSA_TuneStep', N'byte', N'0', 0, N'', N'LOSA调整步长')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (18, 6, N'IsAdjustLosD', N'Bool', N'false', 0, N'', N'是否调整LOSD')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (19, 6, N'LOSD_DAC_Start', N'UInt16', N'0', 0, N'', N'LOSD寄存器起始值')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (20, 6, N'LOSD_DAC_Max', N'UInt16', N'255', 0, N'', N'LOSD寄存器上限')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (21, 6, N'LOSD_DAC_Min', N'UInt16', N'0', 0, N'', N'LOSD寄存器下限')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (22, 6, N'LOSD_TuneStep', N'byte', N'0', 0, N'', N'LOSD调整步长')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (23, 2, N'BiasSetPoints', N'ArrayList', N'2.3,4.5', 0, N'', N'一组值，用于设置IBIAS寄存器')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (24, 2, N'FixedIModDACArray', N'ArrayList', N'0,0,0,0', 0, N'', N'固定IMOD寄存器值')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (25, 2, N'IsTracingErr', N'Bool', N'false', 0, N'etett', N'erttt')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (26, 2, N'IsNewAlgorithm', N'Bool', N'true', 0, N'', N'errrrrree')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (27, 2, N'HighestCalTemp', N'double', N'0', 0, N'', N'最高校准温度')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (28, 2, N'LowestCalTemp', N'double', N'0', 0, N'', N'最低校准温度')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (29, 2, N'SleepTime', N'UInt16', N'50', 0, N'', N'读取光功率之前的延时时间')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (30, 7, N'RxPowerArrayList(DBM)', N'ArrayList', N'0,0,0,0', 0, N'', N'一组值，用于设置接收端光功率')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (32, 11, N'VCCArrayList(V)', N'ArrayList', N'3.1,3.3,3.5', 0, N'', N'一组值，用于设置电源电压')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (34, 10, N'CsenAlignRXPWR(DBM)', N'double', N'-7', 0, N'', N'自动对齐时设定的接收端光功率')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (36, 10, N'SEARCHTARGETBERUL', N'double', N'0.00001', 0, N'', N'误码率的上限')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (37, 10, N'SEARCHTARGETBERLL', N'double', N'0.00000001', 0, N'', N'误码率的下限')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (38, 10, N'SEARCHTARGETRXPOWERLL', N'double', N'-15', 0, N'', N'寻找误码率点的光功率下限')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (39, 10, N'SEARCHTARGETRXPOWERUL', N'double', N'-10', 0, N'', N'寻找误码率点的光功率上限')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (40, 10, N'COEFCsenSUBSTEP(DBM)', N'double', N'0.3', 0, N'', N'推算灵敏度找误码点时减小光功率的步长')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (41, 10, N'IsBerQuickTest', N'Bool', N'false', 0, N'', N'快速测试还是具体值?')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (42, 10, N'COEFCsenADDSTEP(DBM)', N'double', N'0.5', 0, N'', N'推算灵敏度找误码点时增大光功率的步长')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (43, 17, N'RxPowerPoint(DBM)', N'double', N'-20', 0, N'', N'测试入射光大小')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (44, 8, N'IsLosDetail', N'Bool', N'true', 0, N'', N'是否测具体值?')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (45, 8, N'LosADTuneStep', N'double', N'0.3', 0, N'', N'调整步长')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (46, 9, N'RxPowerArrlist(DBM)', N'ArrayList', N'0,0,0,0', 0, N'', N'一组值，用于设定接收端光功率')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (47, 23, N'InputRxPwr(dBm)', N'double', N'-10', 0, N'', N'用于设定测试电眼图的接收端输入光功率')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (48, 24, N'SingleOrMulti', N'Bool', N'false', 0, N'', N'True=单通道测试；False=多通道测试')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (49, 10, N'IsOpticalSourceUnitOMA', N'Bool', N'false', 0, N'', N'光源是否是OMA输入')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (50, 7, N'SleepTime', N'UInt16', N'50', 0, N'', N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (51, 7, N'ReadRXADCCount', N'byte', N'0', 0, N'', N'读取无光时RXADC之后的增量')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (52, 7, N'MinRxPower', N'byte', N'40', 0, N'', N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (53, 7, N'TempCorrelVCCArrayList(V)', N'ArrayList', N'0', 0, N'', N'一组值，用于设置电源电压')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (54, 7, N'TempVccCorrelChannelNames', N'ArrayList', N'0.0,0.0,0.0', 0, N'', N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (55, 10, N'CsenStartingRXPWR(DBM)', N'double', N'-13', 0, N'', N'开始灵敏度测试时设定接收端光功率')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (74, 28, N'123', N'byte', N'0', 0, N'', N'Description')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (79, 1, N'AdjustERUL', N'double', N'5', 0, N'', N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (80, 1, N'AdjustERLL', N'double', N'3', 0, N'', N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (81, 1, N'AdjustTxPowerUL', N'double', N'3', 0, N'', N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (82, 1, N'AdjustTxPowerLL', N'double', N'3', 0, N'', N'')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (85, 10, N'SearchTargetStep', N'double', N'0.5', 1, N'搜寻目标值步长', N'Description')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (86, 6, N'LosAInputPower', N'double', N'-12', 0, N'', N'LosA调整时候的入射光')
INSERT [dbo].[GlobalTestModelParamterList] ([ID], [PID], [ItemName], [ItemType], [ItemValue], [NeedSelect], [Optionalparams], [ItemDescription]) VALUES (87, 37, N'LosAInputPower', N'double', N'-12', 0, N'', N'Description')
SET IDENTITY_INSERT [dbo].[GlobalTestModelParamterList] OFF
/****** Object:  StoredProcedure [dbo].[Pro_CopyTestPlan]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_CopyTestPlan]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Pro_CopyTestPlan] 
@SourcePlanID	int,
@NewPlanPID	int,
@NewPlanName	nvarchar(30),
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr	nvarchar(max)	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

--print Sysdatetime();
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION
declare @BlockType nvarchar(30);
set @BlockType=''ATSPlan''

declare @newTestPlanID int;
declare @newCtrlID int;
declare @newModelID int;
declare @newModelParamID int;
declare @newEquipID int;
declare @newEquipParamID int;
declare @newPNSpecsParamsID int;
declare @newConfigInitID int;

set @myErr=@@ERROR
set @myErrMsg='''';
if not exists(select * from TopoTestPlan where ItemName= @NewPlanName 
	and PID = @NewPlanPID)
	--(select ID from GlobalProductionName where PN=@SourcePN))
	BEGIN		
		declare MyTestPlanCursor cursor local   --声明一个游标，查询满足条件的数据
			for select * from TopoTestPlan where ID=@SourcePlanID
		open MyTestPlanCursor    --打开
		
		--声明变量，用于读取游标中的值
		declare @ID int,
			@PID int, 
			@ItemName nvarchar(30) ,
			@SWVersion nvarchar(30) ,  
			@HWVersion nvarchar(30) ,
			@USBPort tinyint,
			@IsChipInitialize bit,
			@IsEEPROMInitialize bit,
			@IgnoreBackupCoef bit,
			@SNCheck bit,
			@IgnoreFlag bit,
			@ItemDescription nvarchar(200),
			@Version int
		fetch next from MyTestPlanCursor into @ID,@PID,@ItemName,@SWVersion,@HWVersion,@USBPort,
		@IsChipInitialize,@IsEEPROMInitialize,@IgnoreBackupCoef,@SNCheck,@IgnoreFlag,@ItemDescription,@Version
		print @SourcePlanID;
		
		while @@fetch_status=0    --循环读取
			begin			
				INSERT INTO [TopoTestPlan]([PID]
			   ,[ItemName]
			   ,[SWVersion]
			   ,[HWVersion]
			   ,[USBPort]
			   ,[IsChipInitialize]
			   ,[IsEEPROMInitialize]
			   ,[IgnoreBackupCoef]
			   ,[SNCheck]
			   ,[IgnoreFlag]
			   ,[ItemDescription]
			   ,[Version])					   
				Values(@NewPlanPID,@NewPlanName,@SWVersion,@HWVersion,@USBPort,
				@IsChipInitialize,@IsEEPROMInitialize,@IgnoreBackupCoef,@SNCheck,@IgnoreFlag,@ItemDescription,0)				
				set @newTestPlanID = (Select Ident_Current(''TopoTestPlan''));
				print ''@newTestPlanID='' + ltrim(str(@newTestPlanID))
					--准备进行FlowCtrl Equipment Config PNSpecsParams的复制
					
					-->>执行FlowCtrl的复制
					--声明一个游标，查询满足条件的数据
					declare CtrlCursor cursor local for select * from TopoTestControl where PID=@SourcePlanID
					open CtrlCursor    --打开
					declare @Ctrl_ID int	,
							@Ctrl_PID	int	,
							@Ctrl_ItemName	nvarchar(50),
							@Ctrl_SEQ	int,
							@Ctrl_Channel	tinyint,
							@Ctrl_Temp	real,
							@Ctrl_Vcc	real,
							@Ctrl_Pattent	tinyint,
							@Ctrl_DataRate nvarchar(50),
							@Ctrl_CtrlType	tinyint,
							@Ctrl_TempOffset	real,
							@Ctrl_TempWaitTimes real,
							@Ctrl_ItemDescription	nvarchar(200),							
							@Ctrl_IgnoreFlag	bit
					fetch next from CtrlCursor into @Ctrl_ID,@Ctrl_PID,@Ctrl_ItemName,@Ctrl_SEQ,@Ctrl_Channel,@Ctrl_Temp,
					@Ctrl_Vcc,@Ctrl_Pattent,@Ctrl_DataRate,@Ctrl_CtrlType,@Ctrl_TempOffset,@Ctrl_TempWaitTimes,@Ctrl_ItemDescription,@Ctrl_IgnoreFlag
					
					while @@fetch_status=0    --循环读取
						begin
						INSERT INTO  TopoTestControl ([PID]
							   ,[ItemName]
							   ,[SEQ]
							   ,[Channel]
							   ,[Temp]
							   ,[Vcc]
							   ,[Pattent]
							   ,[DataRate]
							   ,[CtrlType]
							   ,[TempOffset]
							   ,[TempWaitTimes]
							   ,[ItemDescription]
							   ,[IgnoreFlag])
						Values(@newTestPlanID,@Ctrl_ItemName,@Ctrl_SEQ,@Ctrl_Channel,@Ctrl_Temp,@Ctrl_Vcc,@Ctrl_Pattent,@Ctrl_DataRate
								,@Ctrl_CtrlType,@Ctrl_TempOffset,@Ctrl_TempWaitTimes,@Ctrl_ItemDescription,@Ctrl_IgnoreFlag) ;
						set @newCtrlID = (Select Ident_Current(''TopoTestControl''))
						print ''@newCtrlID='' + ltrim(str(@newCtrlID))	
						--准备TestModel的复制
							--声明一个游标，查询满足条件的数据
							declare ModelCursor cursor local for select * from TopoTestModel where PID=@Ctrl_ID
							open ModelCursor    --打开
							declare @Model_ID	int ,
									@Model_PID	int ,
									@Model_GID	int,
									@Model_Seq	int,
									@Model_IgnoreFlag	bit,
									@Model_Failbreak	bit
							fetch next from ModelCursor into @Model_ID,@Model_PID,@Model_GID,@Model_Seq,@Model_IgnoreFlag,@Model_Failbreak
							
							while @@fetch_status=0    --循环读取
								begin
								INSERT INTO  TopoTestModel([PID]
									   ,[GID]
									   ,[Seq]
									   ,[IgnoreFlag]
									   ,[Failbreak])
								Values(@newCtrlID,@Model_GID,@Model_Seq,@Model_IgnoreFlag,@Model_Failbreak);
								set @newModelID = (Select Ident_Current(''TopoTestModel''));
								print ''@newModelID='' + ltrim(str(@newModelID))	
									--准备ModelParams的复制
									--声明一个游标，查询满足条件的数据
									declare ModelParamsCursor cursor local for select * from TopoTestParameter where PID=@Model_ID
									open ModelParamsCursor    --打开
									declare @ModelParams_ID	int ,
											@ModelParams_PID	int ,
											@ModelParams_GID	int,
											@ModelParams_ItemValue	nvarchar(255)
									fetch next from ModelParamsCursor into @ModelParams_ID,@ModelParams_PID,@ModelParams_GID,@ModelParams_ItemValue
									
									while @@fetch_status=0    --循环读取
										begin
										INSERT INTO  TopoTestParameter (PID,GID,ItemValue)
										Values(@newModelID,@ModelParams_GID,@ModelParams_ItemValue);
										set @ModelParams_ID = (Select Ident_Current(''TopoTestParameter''));
										-------------------------
										--已经没有子表了,准备结束
										-------------------------
										fetch next from ModelParamsCursor into @ModelParams_ID,@ModelParams_PID,@ModelParams_GID,@ModelParams_ItemValue
	
										end    
									close ModelParamsCursor    --关闭    
									deallocate ModelParamsCursor    --删除						
									--TestModelParams END<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
								
								fetch next from ModelCursor into @Model_ID,@Model_PID,@Model_GID,@Model_Seq,@Model_IgnoreFlag,@Model_Failbreak
					
								end    
							close ModelCursor    --关闭    
							deallocate ModelCursor    --删除						
						--TestModel END<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
						
						fetch next from CtrlCursor into @Ctrl_ID,@Ctrl_PID,@Ctrl_ItemName,@Ctrl_SEQ,@Ctrl_Channel,@Ctrl_Temp,
						@Ctrl_Vcc,@Ctrl_Pattent,@Ctrl_DataRate,@Ctrl_CtrlType,@Ctrl_TempOffset,@Ctrl_TempWaitTimes,@Ctrl_ItemDescription,@Ctrl_IgnoreFlag
		
						end    
					close CtrlCursor    --关闭    
					deallocate CtrlCursor    --删除
					--TestCtrl END<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
					
					-->>执行Equipment的复制
					--声明一个游标，查询满足条件的数据
					declare EquipCursor cursor local for select * from TopoEquipment where PID=@SourcePlanID
					open EquipCursor    --打开
					declare @Equip_ID int ,
							@Equip_PID int ,
							@Equip_GID int,
							@Equip_SEQ	int,
							@Equip_Role	tinyint					
					fetch next from EquipCursor into @Equip_ID,@Equip_PID,@Equip_GID,@Equip_SEQ,@Equip_Role
					
					while @@fetch_status=0    --循环读取
						begin
						INSERT INTO  TopoEquipment ([PID]
							   ,[GID]
							   ,[Seq]
							   ,[Role])
						Values(@newTestPlanID,@Equip_GID,@Equip_SEQ,@Equip_Role) ;
						set @newEquipID = (Select Ident_Current(''TopoEquipment''))
						print ''@newEquipID='' + ltrim(str(@newEquipID))	
							--准备TestEquipParam的复制
							--声明一个游标，查询满足条件的数据
							declare EquipParamCursor cursor local for select * from TopoEquipmentParameter where PID=@Equip_ID
							open EquipParamCursor    --打开
							declare @EquipParam_ID	int ,
									@EquipParam_PID	int ,
									@EquipParam_GID	int ,
									@EquipParam_ItemValue	nvarchar(255)
							fetch next from EquipParamCursor into @EquipParam_ID,@EquipParam_PID,@EquipParam_GID,@EquipParam_ItemValue
							
							while @@fetch_status=0    --循环读取
								begin
								INSERT INTO  TopoEquipmentParameter (PID,GID,ItemValue)
								Values(@newEquipID,@EquipParam_GID,@EquipParam_ItemValue);
								set @newEquipParamID = (Select Ident_Current(''TopoEquipmentParameter''));									
								-------------------------
								--已经没有子表了,准备结束
								-------------------------								
								fetch next from EquipParamCursor into @EquipParam_ID,@EquipParam_PID,@EquipParam_GID,@EquipParam_ItemValue
		
								end    
							close EquipParamCursor    --关闭    
							deallocate EquipParamCursor    --删除						
							--TestEquipParam END<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
						
						fetch next from EquipCursor into @Equip_ID,@Equip_PID,@Equip_GID,@Equip_SEQ,@Equip_Role	
						end    
					close EquipCursor    --关闭    
					deallocate EquipCursor    --删除
					--TestEquip END<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
					
					-->>执行ConfigInit的复制
					--声明一个游标，查询满足条件的数据
					declare ConfigInitCursor cursor local for select * from TopoManufactureConfigInit where PID=@SourcePlanID
					open ConfigInitCursor    --打开
					declare @ConfigInit_ID int ,
							@ConfigInit_PID	int ,
							@ConfigInit_SlaveAddress	int,
							@ConfigInit_Page	tinyint,
							@ConfigInit_StartAddress	int,
							@ConfigInit_Length	tinyint,
							@ConfigInit_ItemValue	int				
					fetch next from ConfigInitCursor into 
					@ConfigInit_ID,@ConfigInit_PID,@ConfigInit_SlaveAddress,@ConfigInit_Page,@ConfigInit_StartAddress,@ConfigInit_Length,@ConfigInit_ItemValue
					
					while @@fetch_status=0    --循环读取
						begin
						INSERT INTO  TopoManufactureConfigInit ([PID]
							   ,[SlaveAddress]
							   ,[Page]
							   ,[StartAddress]
							   ,[Length]
							   ,[ItemValue])
						Values(@newTestPlanID,@ConfigInit_SlaveAddress,@ConfigInit_Page,@ConfigInit_StartAddress,@ConfigInit_Length,@ConfigInit_ItemValue) ;
						set @newConfigInitID = (Select Ident_Current(''TopoManufactureConfigInit''))	
						print ''@newConfigInitID='' + ltrim(str(@newConfigInitID))					
						fetch next from ConfigInitCursor into 
						@ConfigInit_ID,@ConfigInit_PID,@ConfigInit_SlaveAddress,@ConfigInit_Page,@ConfigInit_StartAddress,@ConfigInit_Length,@ConfigInit_ItemValue
		
						end    
					close ConfigInitCursor    --关闭    
					deallocate ConfigInitCursor    --删除
					--TestConfigInit END<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
					
					-->>执行PNSpecsParams的复制
					--声明一个游标，查询满足条件的数据
					declare PNSpecsParamsCursor cursor local for select * from TopoPNSpecsParams where PID=@SourcePlanID
					open PNSpecsParamsCursor    --打开
					declare @PNSpecsParams_ID int ,
							@PNSpecsParams_PID	int, 
							@PNSpecsParams_SID	int,
							@PNSpecsParams_Typical	float,
							@PNSpecsParams_SpecMin	float,
							@PNSpecsParams_SpecMax	float,
							@PNSpecsParams_Channel	tinyint
					fetch next from PNSpecsParamsCursor into 
					@PNSpecsParams_ID,@PNSpecsParams_PID,@PNSpecsParams_SID,@PNSpecsParams_Typical,@PNSpecsParams_SpecMin,@PNSpecsParams_SpecMax,@PNSpecsParams_Channel
					
					while @@fetch_status=0    --循环读取
						begin
						INSERT INTO  TopoPNSpecsParams ([PID]
							   ,[SID]
							   ,[Typical]
							   ,[SpecMin]
							   ,[SpecMax]
							   ,[Channel])						
						Values(@newTestPlanID,@PNSpecsParams_SID,@PNSpecsParams_Typical,@PNSpecsParams_SpecMin,@PNSpecsParams_SpecMax,@PNSpecsParams_Channel) ;
						set @newPNSpecsParamsID = (Select Ident_Current(''TopoPNSpecsParams''))
						print ''@newPNSpecsParamsID='' + ltrim(str(@newPNSpecsParamsID))						
						fetch next from PNSpecsParamsCursor into 
						@PNSpecsParams_ID,@PNSpecsParams_PID,@PNSpecsParams_SID,@PNSpecsParams_Typical,@PNSpecsParams_SpecMin,@PNSpecsParams_SpecMax,@PNSpecsParams_Channel

						end    
					close PNSpecsParamsCursor    --关闭    
					deallocate PNSpecsParamsCursor    --删除
					--TestPNSpecsParams END<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
					
				fetch next from MyTestPlanCursor into @ID,@PID,@ItemName,@SWVersion,@HWVersion,@USBPort,
				@IsChipInitialize,@IsEEPROMInitialize,@IgnoreBackupCoef,@SNCheck,@IgnoreFlag,@ItemDescription,@Version	
			end    
		close MyTestPlanCursor    --关闭    
		deallocate MyTestPlanCursor    --删除	
		--TestPlan END<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<	
		set @myErr = @newTestPlanID; --将操作的结果返回
		INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了TestPlan:''
			+ @NewPlanName +''的信息;TestPlan_Version=0'',@TracingInfo);
	end
else
	BEGIN
		set @myErr=-2;
		set @myErrMsg=''已有名称为:''+ @NewPlanName+ ''的TestPlan,请确认!'';
	end
--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@newTestPlanID))--输出受影响的ID
--PRINT @myErr
--print Sysdatetime();
END try

begin catch
	--select 
	--       error_number() as ''number'',
	--       error_line() as ''line'',
	--       error_message() as ''message'',
	--       error_severity() as ''severity'',
	--       error_state()    as ''state''    
    set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
    --print Sysdatetime();
end catch' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_CopyFlowCtrl]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_CopyFlowCtrl]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[Pro_CopyFlowCtrl] 
@SourceCtrlID	int,
@NewCtrlPID	int,
@NewCtrlName	nvarchar(30),
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr	nvarchar(max)	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

--print Sysdatetime();
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION
declare @BlockType nvarchar(30);
set @BlockType=''ATSPlan''

declare @VersionMax int;
declare @newCtrlID int;
declare @newModelID int;
declare @newModelParamID int;

set @myErr=@@ERROR
set @myErrMsg='''';
if not exists(select * from TopoTestControl where ItemName= @NewCtrlName 
	and PID = @NewCtrlPID)
	--(select ID from GlobalProductionName where PN=@SourcePN))
	BEGIN		
		-->>执行FlowCtrl的复制
		--声明一个游标，查询满足条件的数据
		declare CtrlCursor cursor local for select * from TopoTestControl where ID=@SourceCtrlID
		open CtrlCursor    --打开
		declare @Ctrl_ID int	,
				@Ctrl_PID	int	,
				@Ctrl_ItemName	nvarchar(50),
				@Ctrl_SEQ	int,
				@Ctrl_Channel	tinyint,
				@Ctrl_Temp	real,
				@Ctrl_Vcc	real,
				@Ctrl_Pattent	tinyint,
				@Ctrl_DataRate nvarchar(50),
				@Ctrl_CtrlType	tinyint,
				@Ctrl_TempOffset	real,
				@Ctrl_TempWaitTimes real,
				@Ctrl_ItemDescription	nvarchar(200),							
				@Ctrl_IgnoreFlag	bit
		fetch next from CtrlCursor into @Ctrl_ID,@Ctrl_PID,@Ctrl_ItemName,@Ctrl_SEQ,@Ctrl_Channel,@Ctrl_Temp,
		@Ctrl_Vcc,@Ctrl_Pattent,@Ctrl_DataRate,@Ctrl_CtrlType,@Ctrl_TempOffset,@Ctrl_TempWaitTimes,@Ctrl_ItemDescription,@Ctrl_IgnoreFlag
		
		while @@fetch_status=0    --循环读取
			begin
			if exists(select MAX([SEQ]) from TopoTestControl where PID=@NewCtrlPID)
				begin
					set @Ctrl_SEQ=(select MAX([SEQ]) from TopoTestControl where PID=@NewCtrlPID)+1;
				end
			else	
				begin
					set @Ctrl_SEQ=1;
				end
			INSERT INTO  TopoTestControl ([PID]
				   ,[ItemName]
				   ,[SEQ]
				   ,[Channel]
				   ,[Temp]
				   ,[Vcc]
				   ,[Pattent]
				   ,[DataRate]
				   ,[CtrlType]
				   ,[TempOffset]
				   ,[TempWaitTimes]
				   ,[ItemDescription]
				   ,[IgnoreFlag])
			Values(@NewCtrlPID,@NewCtrlName,@Ctrl_SEQ,@Ctrl_Channel,@Ctrl_Temp,@Ctrl_Vcc,@Ctrl_Pattent,@Ctrl_DataRate
					,@Ctrl_CtrlType,@Ctrl_TempOffset,@Ctrl_TempWaitTimes,@Ctrl_ItemDescription,@Ctrl_IgnoreFlag) ;
			set @newCtrlID = (Select Ident_Current(''TopoTestControl''))
			print ''@newCtrlID='' + ltrim(str(@newCtrlID))	
			--准备TestModel的复制
				--声明一个游标，查询满足条件的数据
				declare ModelCursor cursor local for select * from TopoTestModel where PID=@Ctrl_ID
				open ModelCursor    --打开
				declare @Model_ID	int ,
						@Model_PID	int ,
						@Model_GID	int,
						@Model_Seq	int,
						@Model_IgnoreFlag	bit,
						@Model_Failbreak	bit
				fetch next from ModelCursor into @Model_ID,@Model_PID,@Model_GID,@Model_Seq,@Model_IgnoreFlag,@Model_Failbreak
				
				while @@fetch_status=0    --循环读取
					begin
					INSERT INTO  TopoTestModel([PID]
						   ,[GID]
						   ,[Seq]
						   ,[IgnoreFlag]
						   ,[Failbreak])
					Values(@newCtrlID,@Model_GID,@Model_Seq,@Model_IgnoreFlag,@Model_Failbreak);
					set @newModelID = (Select Ident_Current(''TopoTestModel''));
					print ''@newModelID='' + ltrim(str(@newModelID))	
						--准备ModelParams的复制
						--声明一个游标，查询满足条件的数据
						declare ModelParamsCursor cursor local for select * from TopoTestParameter where PID=@Model_ID
						open ModelParamsCursor    --打开
						declare @ModelParams_ID	int ,
								@ModelParams_PID	int ,
								@ModelParams_GID	int,
								@ModelParams_ItemValue	nvarchar(255)
						fetch next from ModelParamsCursor into @ModelParams_ID,@ModelParams_PID,@ModelParams_GID,@ModelParams_ItemValue
						
						while @@fetch_status=0    --循环读取
							begin
							INSERT INTO  TopoTestParameter (PID,GID,ItemValue)
							Values(@newModelID,@ModelParams_GID,@ModelParams_ItemValue);
							set @ModelParams_ID = (Select Ident_Current(''TopoTestParameter''));
							-------------------------
							--已经没有子表了,准备结束
							-------------------------
							fetch next from ModelParamsCursor into @ModelParams_ID,@ModelParams_PID,@ModelParams_GID,@ModelParams_ItemValue

							end    
						close ModelParamsCursor    --关闭    
						deallocate ModelParamsCursor    --删除						
						--TestModelParams END<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
					
					fetch next from ModelCursor into @Model_ID,@Model_PID,@Model_GID,@Model_Seq,@Model_IgnoreFlag,@Model_Failbreak
		
					end    
				close ModelCursor    --关闭    
				deallocate ModelCursor    --删除						
			--TestModel END<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
			
			fetch next from CtrlCursor into @Ctrl_ID,@Ctrl_PID,@Ctrl_ItemName,@Ctrl_SEQ,@Ctrl_Channel,@Ctrl_Temp,
			@Ctrl_Vcc,@Ctrl_Pattent,@Ctrl_DataRate,@Ctrl_CtrlType,@Ctrl_TempOffset,@Ctrl_TempWaitTimes,@Ctrl_ItemDescription,@Ctrl_IgnoreFlag

			end    
		close CtrlCursor    --关闭    
		deallocate CtrlCursor    --删除
		--TestCtrl END<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		set @myErr = @newCtrlID; --将操作的结果返回
		
		--更新TestPlan的Version	
		set @VersionMax = (select [version] from TopoTestPlan where id =@NewCtrlPID)+1;
		update  TopoTestPlan
		set 
		[Version]=@VersionMax
		from TopoTestPlan where id =@NewCtrlPID

		INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了TestControl:''+ @NewCtrlName +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);
	end
else
	BEGIN
		set @myErr=-2;
		set @myErrMsg=''已有名称为:''+ @NewCtrlName+ ''的TestCtrl,请确认!'';
	end
--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@newTestPlanID))--输出受影响的ID
--PRINT @myErr
--print Sysdatetime();
END try

begin catch
	--select 
	--       error_number() as ''number'',
	--       error_line() as ''line'',
	--       error_message() as ''message'',
	--       error_severity() as ''severity'',
	--       error_state()    as ''state''    
    set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
    --print Sysdatetime();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalAllEquipmentParamterList]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalAllEquipmentParamterList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_GlobalAllEquipmentParamterList]
@ID int OUTPUT,
@PID int ,
@ItemName	nvarchar(30),
@ItemType	nvarchar(10),
@ItemValue	nvarchar(255),
@NeedSelect	bit,
@Optionalparams	nvarchar(max),
@ItemDescription	nvarchar(200),
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';

declare @BlockType nvarchar(50)
set @BlockType=''Equipment''

declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
set @OPItemStr = ''GlobalEquipmentParams:''
set @OPItemNameStr =@ItemName
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  GlobalAllEquipmentParamterList ([PID]
			   ,[ItemName]
			   ,[ItemType]
			   ,[ItemValue]
			   ,[NeedSelect]
			   ,[Optionalparams]
			   ,[ItemDescription]) 
	Values(@PID,@ItemName,@ItemType,@ItemValue,@NeedSelect,@Optionalparams,@ItemDescription) ;
	set @ID = (Select Ident_Current(''GlobalAllEquipmentParamterList''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  GlobalAllEquipmentParamterList
	
	set 
	PID=@PID,ItemName=@ItemName,ItemType= @ItemType,ItemValue=@ItemValue
	,NeedSelect=@NeedSelect,Optionalparams=@Optionalparams,ItemDescription=@ItemDescription
	from GlobalAllEquipmentParamterList where id =@id

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  GlobalAllEquipmentParamterList 
	where GlobalAllEquipmentParamterList.id=@ID

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Pro_TopoTestModelWithParams]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_TopoTestModelWithParams]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_TopoTestModelWithParams] 
@ID	int OUTPUT,
@PID	int ,
@GID	int,
@Seq	int,
@IgnoreFlag	bit,
@Failbreak	bit,
@Params	varchar(Max),
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 
declare @VersionMax int; 
declare @TestPlanID int; 
set @TestPlanID =(select PID from TopoTestControl where ID= @PID);

set @myErr=@@ERROR
set @myErrMsg='''';
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
declare @BlockType nvarchar(50)
set @BlockType=''ATSPlan''
set @OPItemStr = ''TestModel:''
set @OPItemNameStr = (select ItemName from GlobalAllTestModelList where ID  = @GID)
--AddNew
if (@RowState=0)
BEGIN
	INSERT INTO  TopoTestModel ([PID]
			   ,[GID]
			   ,[Seq]
			   ,[IgnoreFlag]
			   ,[Failbreak])
	Values(@PID,@GID,@Seq,@IgnoreFlag,@Failbreak) ;
	set @ID = (Select Ident_Current(''TopoTestModel''));
	if (LEN(@Params)>0)	
	BEGIN
		INSERT INTO  TopoTestParameter select * from f_splitModelstr(@ID,@Params,''|'',''#'');
	end
		
	--新增更新TestPlan的Version	
	set @VersionMax = (select [version] from TopoTestPlan where id =@TestPlanID)+1;
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@TestPlanID
	
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);
end
--Update
else if (@RowState=1)
BEGIN
	update  TopoTestModel
	set 
	PID=@PID,GID=@GID,Seq=@Seq,IgnoreFlag=@IgnoreFlag,Failbreak=@Failbreak
	from TopoTestModel where id =@id
	
	--利用游标进行更新资料
	if (LEN(@Params)>0)	
	BEGIN
		declare MyCursor cursor    --声明一个游标，查询满足条件的数据
			local for select * from f_splitModelstr(@ID,@Params,''|'',''#'')
		--select * from f_splitModelstr(121,''9#60|23#0,0|24#123|'',''|'',''#'')
		open MyCursor    --打开
	    
		declare @MyPID int, @MyGID int ,@MyValue  nvarchar(max)    --声明一个变量，用于读取游标中的值
		   fetch next from MyCursor into @MyPID,@MyGID,@MyValue
	    
		while @@fetch_status=0    --循环读取
			begin
			--print str(@MyPID  + ''_'' + @MyGID  + ''_'' + @MyValue)        
			declare @queryExistStrs nvarchar(max)
			IF  EXISTS (select * from TopoTestParameter where PID=@MyPID and GID =@MyGID)
				BEGIN
					update TopoTestParameter set ItemValue=@MyValue from TopoTestParameter where PID=@MyPID and GID =@MyGID
				end 
			else
				BEGIN
					INSERT INTO  TopoTestParameter (PID,GID,ItemValue) values(@MyPID,@MyGID,@MyValue)
				end 
			fetch next from MyCursor into @MyPID,@MyGID,@MyValue
			end    
		close MyCursor    --关闭    
		deallocate MyCursor    --删除
    end 
    
    --新增更新TestPlan的Version	
	set @VersionMax = (select [version] from TopoTestPlan where id =@TestPlanID)+1;
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@TestPlanID	
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);

end
--delete
else if (@RowState=2)
BEGIN
delete  TopoTestModel 
where TopoTestModel.id=@ID

--新增更新TestPlan的Version	
	set @VersionMax = (select [version] from TopoTestPlan where id =@TestPlanID)+1;
	update  TopoTestPlan
	set 
	[Version]=@VersionMax
	from TopoTestPlan where id =@TestPlanID
INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'' + '';TestPlan_Version='' + Ltrim(str(@VersionMax)),@TracingInfo);

end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  Trigger [Trg_Add_TopoEqParamOnGid]    Script Date: 09/21/2015 15:16:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[Trg_Add_TopoEqParamOnGid]'))
EXEC dbo.sp_executesql @statement = N'--For Add a row record
CREATE TRIGGER [dbo].[Trg_Add_TopoEqParamOnGid]
   ON  [dbo].[GlobalAllEquipmentParamterList]
   AFTER insert
AS 
BEGIN	
	if exists(select TopoEquipment.ID from TopoEquipment,inserted where TopoEquipment.GID =inserted.PID)
	BEGIN			
		declare MyCursor cursor    --声明一个游标，查询满足条件的数据	
		for	(select TopoEquipment.ID,inserted.id,inserted.ItemValue from TopoEquipment,inserted 
		where TopoEquipment.GID =inserted.PID)
		
		open MyCursor    --打开
		declare @MyPID int, @MyGID int ,@MyValue  nvarchar(max)--声明一个变量，用于读取游标中的值
		fetch next from MyCursor into @MyPID,@MyGID,@MyValue
	    
		while @@fetch_status=0    --循环读取
		begin
			--print ltrim(str(@MyPID))  + ''_'' + ltrim(str(@MyGID)) + ''_'' + @MyValue)) 
			INSERT INTO  TopoEquipmentParameter (PID,GID,ItemValue) values(@MyPID,@MyGID,@MyValue)
			fetch next from MyCursor into @MyPID,@MyGID,@MyValue
		end    
		close MyCursor    --关闭    
		deallocate MyCursor    --删除
	end
end
'
GO
/****** Object:  Trigger [Trg_Del_TopoEqParamOnGid]    Script Date: 09/21/2015 15:16:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[Trg_Del_TopoEqParamOnGid]'))
EXEC dbo.sp_executesql @statement = N'--For Delete a row record
Create TRIGGER [dbo].[Trg_Del_TopoEqParamOnGid]
   ON  [dbo].[GlobalAllEquipmentParamterList]
   AFTER DELETE
AS 
BEGIN
	delete TopoEquipmentParameter
	from TopoEquipmentParameter ,deleted 
	where TopoEquipmentParameter.gid=deleted.id
end
'
GO
/****** Object:  Trigger [Trg_Add_TopoModelParamOnGid]    Script Date: 09/21/2015 15:16:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[Trg_Add_TopoModelParamOnGid]'))
EXEC dbo.sp_executesql @statement = N'--For Add a row record
Create TRIGGER [dbo].[Trg_Add_TopoModelParamOnGid]
   ON  [dbo].[GlobalTestModelParamterList]
   AFTER insert
AS 
BEGIN	
	if exists(select TopoTestModel.ID from TopoTestModel,inserted where TopoTestModel.GID =inserted.PID)
	BEGIN			
		declare MyCursor cursor    --声明一个游标，查询满足条件的数据	
		for	(select TopoTestModel.ID,inserted.id,inserted.ItemValue from TopoTestModel,inserted 
		where TopoTestModel.GID =inserted.PID)
		
		open MyCursor    --打开
		declare @MyPID int, @MyGID int ,@MyValue  nvarchar(max)--声明一个变量，用于读取游标中的值
		fetch next from MyCursor into @MyPID,@MyGID,@MyValue
	    
		while @@fetch_status=0    --循环读取
		begin
			--print ltrim(str(@MyPID))  + ''_'' + ltrim(str(@MyGID)) + ''_'' + @MyValue)) 
			INSERT INTO  TopoTestParameter (PID,GID,ItemValue) values(@MyPID,@MyGID,@MyValue)
			fetch next from MyCursor into @MyPID,@MyGID,@MyValue
		end    
		close MyCursor    --关闭    
		deallocate MyCursor    --删除
	end
end
'
GO
/****** Object:  Trigger [Trg_Del_TopoModelParamOnGid]    Script Date: 09/21/2015 15:16:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[Trg_Del_TopoModelParamOnGid]'))
EXEC dbo.sp_executesql @statement = N'--For Delete a row record
Create TRIGGER [dbo].[Trg_Del_TopoModelParamOnGid]
   ON  [dbo].[GlobalTestModelParamterList]
   AFTER DELETE
AS 
BEGIN
	delete TopoTestParameter
	from TopoTestParameter ,deleted 
	where TopoTestParameter.gid=deleted.id
end
'
GO
/****** Object:  StoredProcedure [dbo].[Pro_GlobalTestModelParamterList]    Script Date: 09/21/2015 15:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pro_GlobalTestModelParamterList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[Pro_GlobalTestModelParamterList] 
@ID int OUTPUT,
@PID	int ,
@ItemName	nvarchar(30),
@ItemType	nvarchar(10),
@ItemValue	nvarchar(255),
@NeedSelect bit,
@Optionalparams nvarchar(max),
@ItemDescription	nvarchar(200),
@RowState	tinyint,
@OPlogPID	int,
@TracingInfo nvarchar(max),
@myErr	int	OUTPUT,
@myErrMsg	nvarchar(max)	OUTPUT
AS
 
BEGIN try

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
--指定语句不能读取已由其他事务修改但尚未提交的行，并且指定，其他任何事务都不能在当前事务完成之前修改由当前事务读取的数据。

SET XACT_ABORT ON 
--指定当 Transact-SQL 语句出现运行时错误时，SQL Server 是否自动回滚到当前事务。

BEGIN TRANSACTION 

set @myErr=@@ERROR
set @myErrMsg='''';
declare @BlockType nvarchar(50)
set @BlockType=''AppModel''
declare @OPItemStr  nvarchar(50);
declare @OPItemNameStr  nvarchar(max);
set @OPItemStr = ''GlobalModelParams:''
set @OPItemNameStr =@ItemName
--AddNew
if (@RowState=0)
	BEGIN
	INSERT INTO  GlobalTestModelParamterList([PID]
			   ,[ItemName]
			   ,[ItemType]
			   ,[ItemValue]
			   ,[NeedSelect]
			   ,[Optionalparams]
			   ,[ItemDescription]) 
	Values(@PID,@ItemName,@ItemType,@ItemValue,@NeedSelect,@Optionalparams,@ItemDescription) ;
	set @ID = (Select Ident_Current(''GlobalTestModelParamterList''))

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Added'',''新增了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);
	end
--Update
else if (@RowState=1)
	BEGIN
	update  GlobalTestModelParamterList
	set 
	PID=@PID,ItemName=@ItemName,ItemType= @ItemType,ItemValue=@ItemValue
	,NeedSelect=@NeedSelect,Optionalparams=@Optionalparams,ItemDescription=@ItemDescription
	from GlobalTestModelParamterList where id =@id

	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Modified'',''修改了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end
--delete
else if (@RowState=2)
	BEGIN
	delete  GlobalTestModelParamterList 
	where GlobalTestModelParamterList.id=@ID
	INSERT INTO  OperationLogs values(@OPlogPID,CONVERT(varchar(30), GETDATE(), 120),@BlockType,''Deleted'',''删除了''+ @OPItemStr + @OPItemNameStr +''的信息'',@TracingInfo);

	end

--开始提交
COMMIT TRANSACTION 
SET XACT_ABORT OFF 

--print ''ID='' + ltrim(str(@id))--输出受影响的ID
--PRINT @myErr
END try

begin catch
	set @myErr= error_number() *-1;
    set @myErrMsg=error_message();
end catch
' 
END
GO
/****** Object:  ForeignKey [FK_BlockAscxInfo_AscxFile]    Script Date: 09/21/2015 15:16:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BlockAscxInfo_AscxFile]') AND parent_object_id = OBJECT_ID(N'[dbo].[BlockAscxInfo]'))
ALTER TABLE [dbo].[BlockAscxInfo]  WITH CHECK ADD  CONSTRAINT [FK_BlockAscxInfo_AscxFile] FOREIGN KEY([AscxID])
REFERENCES [dbo].[AscxFile] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BlockAscxInfo_AscxFile]') AND parent_object_id = OBJECT_ID(N'[dbo].[BlockAscxInfo]'))
ALTER TABLE [dbo].[BlockAscxInfo] CHECK CONSTRAINT [FK_BlockAscxInfo_AscxFile]
GO
/****** Object:  ForeignKey [FK_BlockAscxInfo_FunctionTable]    Script Date: 09/21/2015 15:16:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BlockAscxInfo_FunctionTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[BlockAscxInfo]'))
ALTER TABLE [dbo].[BlockAscxInfo]  WITH CHECK ADD  CONSTRAINT [FK_BlockAscxInfo_FunctionTable] FOREIGN KEY([FuncBlockID])
REFERENCES [dbo].[FunctionTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BlockAscxInfo_FunctionTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[BlockAscxInfo]'))
ALTER TABLE [dbo].[BlockAscxInfo] CHECK CONSTRAINT [FK_BlockAscxInfo_FunctionTable]
GO
/****** Object:  ForeignKey [FK_ChannelMap_PNChipMap]    Script Date: 09/21/2015 15:16:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChannelMap_PNChipMap]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChannelMap]'))
ALTER TABLE [dbo].[ChannelMap]  WITH CHECK ADD  CONSTRAINT [FK_ChannelMap_PNChipMap] FOREIGN KEY([PNChipID])
REFERENCES [dbo].[PNChipMap] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChannelMap_PNChipMap]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChannelMap]'))
ALTER TABLE [dbo].[ChannelMap] CHECK CONSTRAINT [FK_ChannelMap_PNChipMap]
GO
/****** Object:  ForeignKey [FK_ChipRegister_RegisterFormula]    Script Date: 09/21/2015 15:16:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChipRegister_RegisterFormula]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChipRegister]'))
ALTER TABLE [dbo].[ChipRegister]  WITH CHECK ADD  CONSTRAINT [FK_ChipRegister_RegisterFormula] FOREIGN KEY([FormulaID])
REFERENCES [dbo].[RegisterFormula] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ChipRegister_RegisterFormula]') AND parent_object_id = OBJECT_ID(N'[dbo].[ChipRegister]'))
ALTER TABLE [dbo].[ChipRegister] CHECK CONSTRAINT [FK_ChipRegister_RegisterFormula]
GO
/****** Object:  ForeignKey [FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList]    Script Date: 09/21/2015 15:16:27 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalAllEquipmentParamterList]'))
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList]  WITH CHECK ADD  CONSTRAINT [FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalAllEquipmentList] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalAllEquipmentParamterList]'))
ALTER TABLE [dbo].[GlobalAllEquipmentParamterList] CHECK CONSTRAINT [FK_GlobalAllEquipmentParamterList_GlobalAllEquipmentList]
GO
/****** Object:  ForeignKey [FK_GlobalAllTestModelList_GlobalAllAppModelList]    Script Date: 09/21/2015 15:16:27 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalAllTestModelList_GlobalAllAppModelList]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalAllTestModelList]'))
ALTER TABLE [dbo].[GlobalAllTestModelList]  WITH CHECK ADD  CONSTRAINT [FK_GlobalAllTestModelList_GlobalAllAppModelList] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalAllAppModelList] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalAllTestModelList_GlobalAllAppModelList]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalAllTestModelList]'))
ALTER TABLE [dbo].[GlobalAllTestModelList] CHECK CONSTRAINT [FK_GlobalAllTestModelList_GlobalAllAppModelList]
GO
/****** Object:  ForeignKey [FK_GlobalManufactureChipsetControl_GlobalProductionName]    Script Date: 09/21/2015 15:16:27 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalManufactureChipsetControl_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalManufactureChipsetControl]'))
ALTER TABLE [dbo].[GlobalManufactureChipsetControl]  WITH CHECK ADD  CONSTRAINT [FK_GlobalManufactureChipsetControl_GlobalProductionName] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionName] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalManufactureChipsetControl_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalManufactureChipsetControl]'))
ALTER TABLE [dbo].[GlobalManufactureChipsetControl] CHECK CONSTRAINT [FK_GlobalManufactureChipsetControl_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_GlobalManufactureChipsetInitialize_GlobalProductionName]    Script Date: 09/21/2015 15:16:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalManufactureChipsetInitialize_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalManufactureChipsetInitialize]'))
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize]  WITH CHECK ADD  CONSTRAINT [FK_GlobalManufactureChipsetInitialize_GlobalProductionName] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionName] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalManufactureChipsetInitialize_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalManufactureChipsetInitialize]'))
ALTER TABLE [dbo].[GlobalManufactureChipsetInitialize] CHECK CONSTRAINT [FK_GlobalManufactureChipsetInitialize_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable]    Script Date: 09/21/2015 15:16:28 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalManufactureCoefficients]'))
ALTER TABLE [dbo].[GlobalManufactureCoefficients]  WITH CHECK ADD  CONSTRAINT [FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalManufactureCoefficientsGroup] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalManufactureCoefficients]'))
ALTER TABLE [dbo].[GlobalManufactureCoefficients] CHECK CONSTRAINT [FK_GlobalManufactureMemory_GlobalManufactureMemoryGroupTable]
GO
/****** Object:  ForeignKey [FK_GlobalMSADefintionInf_GlobalMSA]    Script Date: 09/21/2015 15:16:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalMSADefintionInf_GlobalMSA]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalMSADefintionInf]'))
ALTER TABLE [dbo].[GlobalMSADefintionInf]  WITH CHECK ADD  CONSTRAINT [FK_GlobalMSADefintionInf_GlobalMSA] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalMSA] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalMSADefintionInf_GlobalMSA]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalMSADefintionInf]'))
ALTER TABLE [dbo].[GlobalMSADefintionInf] CHECK CONSTRAINT [FK_GlobalMSADefintionInf_GlobalMSA]
GO
/****** Object:  ForeignKey [FK_GlobalProductionName_GlobalManufactureCoefficientsGroup]    Script Date: 09/21/2015 15:16:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalProductionName_GlobalManufactureCoefficientsGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalProductionName]'))
ALTER TABLE [dbo].[GlobalProductionName]  WITH CHECK ADD  CONSTRAINT [FK_GlobalProductionName_GlobalManufactureCoefficientsGroup] FOREIGN KEY([MCoefsID])
REFERENCES [dbo].[GlobalManufactureCoefficientsGroup] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalProductionName_GlobalManufactureCoefficientsGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalProductionName]'))
ALTER TABLE [dbo].[GlobalProductionName] CHECK CONSTRAINT [FK_GlobalProductionName_GlobalManufactureCoefficientsGroup]
GO
/****** Object:  ForeignKey [FK_GlobalProductionName_GlobalProductionType]    Script Date: 09/21/2015 15:16:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalProductionName_GlobalProductionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalProductionName]'))
ALTER TABLE [dbo].[GlobalProductionName]  WITH CHECK ADD  CONSTRAINT [FK_GlobalProductionName_GlobalProductionType] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalProductionType] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalProductionName_GlobalProductionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalProductionName]'))
ALTER TABLE [dbo].[GlobalProductionName] CHECK CONSTRAINT [FK_GlobalProductionName_GlobalProductionType]
GO
/****** Object:  ForeignKey [FK_GlobalProductionType_GlobalMSA]    Script Date: 09/21/2015 15:16:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalProductionType_GlobalMSA]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalProductionType]'))
ALTER TABLE [dbo].[GlobalProductionType]  WITH CHECK ADD  CONSTRAINT [FK_GlobalProductionType_GlobalMSA] FOREIGN KEY([MSAID])
REFERENCES [dbo].[GlobalMSA] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalProductionType_GlobalMSA]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalProductionType]'))
ALTER TABLE [dbo].[GlobalProductionType] CHECK CONSTRAINT [FK_GlobalProductionType_GlobalMSA]
GO
/****** Object:  ForeignKey [FK_GlobalTestModelParamterList_GlobalAllTestModelList]    Script Date: 09/21/2015 15:16:29 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalTestModelParamterList_GlobalAllTestModelList]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalTestModelParamterList]'))
ALTER TABLE [dbo].[GlobalTestModelParamterList]  WITH CHECK ADD  CONSTRAINT [FK_GlobalTestModelParamterList_GlobalAllTestModelList] FOREIGN KEY([PID])
REFERENCES [dbo].[GlobalAllTestModelList] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalTestModelParamterList_GlobalAllTestModelList]') AND parent_object_id = OBJECT_ID(N'[dbo].[GlobalTestModelParamterList]'))
ALTER TABLE [dbo].[GlobalTestModelParamterList] CHECK CONSTRAINT [FK_GlobalTestModelParamterList_GlobalAllTestModelList]
GO
/****** Object:  ForeignKey [FK_PNChipMap_ChipBaseInfo]    Script Date: 09/21/2015 15:16:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PNChipMap_ChipBaseInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[PNChipMap]'))
ALTER TABLE [dbo].[PNChipMap]  WITH CHECK ADD  CONSTRAINT [FK_PNChipMap_ChipBaseInfo] FOREIGN KEY([ChipID])
REFERENCES [dbo].[ChipBaseInfo] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PNChipMap_ChipBaseInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[PNChipMap]'))
ALTER TABLE [dbo].[PNChipMap] CHECK CONSTRAINT [FK_PNChipMap_ChipBaseInfo]
GO
/****** Object:  ForeignKey [FK_PNChipMap_GlobalProductionName]    Script Date: 09/21/2015 15:16:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PNChipMap_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[PNChipMap]'))
ALTER TABLE [dbo].[PNChipMap]  WITH CHECK ADD  CONSTRAINT [FK_PNChipMap_GlobalProductionName] FOREIGN KEY([PNID])
REFERENCES [dbo].[GlobalProductionName] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PNChipMap_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[PNChipMap]'))
ALTER TABLE [dbo].[PNChipMap] CHECK CONSTRAINT [FK_PNChipMap_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_RegisterFormula_ChipBaseInfo]    Script Date: 09/21/2015 15:16:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RegisterFormula_ChipBaseInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[RegisterFormula]'))
ALTER TABLE [dbo].[RegisterFormula]  WITH CHECK ADD  CONSTRAINT [FK_RegisterFormula_ChipBaseInfo] FOREIGN KEY([ChipID])
REFERENCES [dbo].[ChipBaseInfo] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RegisterFormula_ChipBaseInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[RegisterFormula]'))
ALTER TABLE [dbo].[RegisterFormula] CHECK CONSTRAINT [FK_RegisterFormula_ChipBaseInfo]
GO
/****** Object:  ForeignKey [FK_RoleFunctionTable_FunctionTable]    Script Date: 09/21/2015 15:16:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleFunctionTable_FunctionTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleFunctionTable]'))
ALTER TABLE [dbo].[RoleFunctionTable]  WITH CHECK ADD  CONSTRAINT [FK_RoleFunctionTable_FunctionTable] FOREIGN KEY([FunctionID])
REFERENCES [dbo].[FunctionTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleFunctionTable_FunctionTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleFunctionTable]'))
ALTER TABLE [dbo].[RoleFunctionTable] CHECK CONSTRAINT [FK_RoleFunctionTable_FunctionTable]
GO
/****** Object:  ForeignKey [FK_RoleFunctionTable_RolesTable]    Script Date: 09/21/2015 15:16:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleFunctionTable_RolesTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleFunctionTable]'))
ALTER TABLE [dbo].[RoleFunctionTable]  WITH CHECK ADD  CONSTRAINT [FK_RoleFunctionTable_RolesTable] FOREIGN KEY([RoleID])
REFERENCES [dbo].[RolesTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RoleFunctionTable_RolesTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[RoleFunctionTable]'))
ALTER TABLE [dbo].[RoleFunctionTable] CHECK CONSTRAINT [FK_RoleFunctionTable_RolesTable]
GO
/****** Object:  ForeignKey [FK_GlobalUserPlanAction_TopoTestPlan]    Script Date: 09/21/2015 15:16:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPlanAction_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPlanAction]'))
ALTER TABLE [dbo].[UserPlanAction]  WITH CHECK ADD  CONSTRAINT [FK_GlobalUserPlanAction_TopoTestPlan] FOREIGN KEY([PlanID])
REFERENCES [dbo].[TopoTestPlan] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPlanAction_TopoTestPlan]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPlanAction]'))
ALTER TABLE [dbo].[UserPlanAction] CHECK CONSTRAINT [FK_GlobalUserPlanAction_TopoTestPlan]
GO
/****** Object:  ForeignKey [FK_GlobalUserPlanAction_UserInfo]    Script Date: 09/21/2015 15:16:30 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPlanAction_UserInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPlanAction]'))
ALTER TABLE [dbo].[UserPlanAction]  WITH CHECK ADD  CONSTRAINT [FK_GlobalUserPlanAction_UserInfo] FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPlanAction_UserInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPlanAction]'))
ALTER TABLE [dbo].[UserPlanAction] CHECK CONSTRAINT [FK_GlobalUserPlanAction_UserInfo]
GO
/****** Object:  ForeignKey [FK_GlobalUserPNAction_GlobalProductionName]    Script Date: 09/21/2015 15:16:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPNAction_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPNAction]'))
ALTER TABLE [dbo].[UserPNAction]  WITH CHECK ADD  CONSTRAINT [FK_GlobalUserPNAction_GlobalProductionName] FOREIGN KEY([PNID])
REFERENCES [dbo].[GlobalProductionName] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPNAction_GlobalProductionName]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPNAction]'))
ALTER TABLE [dbo].[UserPNAction] CHECK CONSTRAINT [FK_GlobalUserPNAction_GlobalProductionName]
GO
/****** Object:  ForeignKey [FK_GlobalUserPNAction_UserInfo]    Script Date: 09/21/2015 15:16:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPNAction_UserInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPNAction]'))
ALTER TABLE [dbo].[UserPNAction]  WITH CHECK ADD  CONSTRAINT [FK_GlobalUserPNAction_UserInfo] FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GlobalUserPNAction_UserInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPNAction]'))
ALTER TABLE [dbo].[UserPNAction] CHECK CONSTRAINT [FK_GlobalUserPNAction_UserInfo]
GO
/****** Object:  ForeignKey [FK_UserRoleTable_RolesTable]    Script Date: 09/21/2015 15:16:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoleTable_RolesTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoleTable]'))
ALTER TABLE [dbo].[UserRoleTable]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleTable_RolesTable] FOREIGN KEY([RoleID])
REFERENCES [dbo].[RolesTable] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoleTable_RolesTable]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoleTable]'))
ALTER TABLE [dbo].[UserRoleTable] CHECK CONSTRAINT [FK_UserRoleTable_RolesTable]
GO
/****** Object:  ForeignKey [FK_UserRoleTable_UserInfo]    Script Date: 09/21/2015 15:16:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoleTable_UserInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoleTable]'))
ALTER TABLE [dbo].[UserRoleTable]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleTable_UserInfo] FOREIGN KEY([UserID])
REFERENCES [dbo].[UserInfo] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserRoleTable_UserInfo]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserRoleTable]'))
ALTER TABLE [dbo].[UserRoleTable] CHECK CONSTRAINT [FK_UserRoleTable_UserInfo]
GO
