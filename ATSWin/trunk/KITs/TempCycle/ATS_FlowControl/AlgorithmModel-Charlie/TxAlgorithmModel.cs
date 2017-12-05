using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using ATS_Framework;

namespace ATS
{
    //public class TxAlgorithmModel:AlgorithmModel
    //{

    //  public  AdjustEye pAdjustEye;
    //  public  TestEye pTestEye;
    //  public  CalTxDmi  pCalTxDmi;
    //  public  TestTxPowerDmi pTestTxDmi;
    //  public CalTxPower pCalTxPower;

    //  public ReadTempAdc pReadTempAdc;

    //  public bool bAdjustEye;
    //  public bool bTestEye;
    //  public bool bCalTxDmi;
    //  public bool bTestTxDmi;


    //  public ArrayList ArrayTxPowADC=new ArrayList();
    //  public ArrayList ArrayTxPower = new ArrayList();
    //  public ArrayList ArrayIbiasDAC = new ArrayList();
    //  public ArrayList ArrayImodDAC = new ArrayList();
    //  public ArrayList ArrayTempADC = new ArrayList();
    //  public ArrayList ArrayTempValue = new ArrayList();

    //  public int CurrentTemp;
      
    //  public int TempCount=0;


    //  private DataTable DTtxPowerAdcArray = new DataTable();
    //  private DataTable DTtxPoweruwArray = new DataTable();

    //  private DataTable DtTestEyeOutput = new DataTable();// Test Eye 的输出项目+测试值+及其对应的规格 一共四列

    // // public Eye_Diagram pTestEye;
      
    //  public TxAlgorithmModel(EquipmentList pEquipmentList,int i)
    //  {
    //      aEquipList = pEquipmentList;
    //  }

    //  public override void Adjust()
    //  {
    //        if (bAdjustEye)
    //        {

    //            ConditionConfigData pConditionConfigData = new ConditionConfigData();

    //            pConditionConfigData.Aout_Eye_Tune = true ;
    //            pConditionConfigData.Ibias_DAC_Max = 7890;
    //            pConditionConfigData.Ibias_DAC_Min = 400;
    //            pConditionConfigData.TxLOP_Target = 1265;
    //            pConditionConfigData.TxLOP_Tolerance = 50;
    //            pConditionConfigData.Ibias_DAC_Start_Value = 800;
    //            pConditionConfigData.Ibias_Method = 1;
    //            pConditionConfigData.IbiasStep = 5;

    //            pConditionConfigData.TxER_Target = 4.6;
    //            pConditionConfigData.TxER_Tolerance = 0.5;
    //            pConditionConfigData.Imod_DAC_Max = 5678;
    //            pConditionConfigData.Imod_DAC_Min = 300;
    //            pConditionConfigData.Imod_DAC_Start_Value = 800;
    //            pConditionConfigData.Imod_Method = 1;
    //            pConditionConfigData.ImodStep = 5;

    //           // if (pAdjustEye == null)
    //           // {
    //           //     pAdjustEye = new AdjustEye();
    //           //     pAdjustEye.SelectEquipment(aEquipList);
                  
    //           // }
              
    //           // pAdjustEye.AdjustEyeParameters = pConditionConfigData;
    //           // pAdjustEye.RunTest();
    //           // //------------------------------获取当前的TempADC值
    //           // if (pReadTempAdc==null)
    //           //{
    //           //    pReadTempAdc = new ReadTempAdc();
    //           //    pReadTempAdc.SelectEquipment(aEquipList);

    //           //}
    //           // pReadTempAdc.RunTest();
    //           // ArrayTempADC.Add(pReadTempAdc.CurrentTempAdc);

    //           // //------------------------------
    //           // if (pCalTxPower == null)
    //           // {
    //           //     pCalTxPower = new CalTxPower();
    //           //     pCalTxPower.SelectEquipment(aEquipList);
    //           // }

    //           // pCalTxPower.IbiasDacArray = pAdjustEye.IbiasDacArray;//获取Ibias Target值 .AdjustEye未给我对应的Target TxPowerADC值
               
    //           // if (ArrayTempValue.Count>1)
    //           //{
    //           //    pCalTxPower.RunTest();//完成系数的拟合

    //           //}
    //        }
    //      //--------------------------------------Cal Tx Dmi 一个温度下直接完成
    //        if (bCalTxDmi)
    //        {
    //            ConditionConfigData pConditionConfigData = new ConditionConfigData();

    //            //if (pCalTxDmi==null)
    //            //{
    //            //    pCalTxDmi = new CalTxDmi();
    //            //    pCalTxDmi.SelectEquipment(aEquipList);
    //            //    pCalTxDmi.SelectConfigData(pConditionConfigData);
    //            //}
    //            //DTtxPowerAdcArray=pAdjustEye.TxPowerAdcArray;
    //            //DTtxPoweruwArray=pAdjustEye.TxPoweruwArray;

    //            //pCalTxDmi.TxPowerAacArray = pAdjustEye.TxPowerAdcArray;
    //            //pCalTxDmi.TxPowerUwArray = pAdjustEye.TxPoweruwArray;

    //            //pCalTxDmi.RunTest();
    //        }

           


    //    }

    //  public override void Test()
    //  {
    //      DtTestEyeOutput.Clear();// 将数据清空
    //       ConditionConfigData pConditionConfigData = new ConditionConfigData();
    //      if (bTestEye)
    //      {
    //          if (pTestEye == null)
    //          {
    //              pTestEye = new TestEye(pdut);
    //              pTestEye.SelectEquipment(aEquipList);
    //          }

    //          DtTestEyeOutput.Columns.Add("ItemName");
    //          DtTestEyeOutput.Columns.Add("Value");
    //          DtTestEyeOutput.Columns.Add("SpecMax");
    //          DtTestEyeOutput.Columns.Add("SpecMin");

    //          for (byte i = 0; i < 10; i++)
    //          {
    //              DataRow RowEyeOutput = DtTestEyeOutput.NewRow();

    //              string Str_Item="AP";
    //              for (byte j = 0; j < DtTestEyeOutput.Columns.Count; j++)
    //              {
    //                  RowEyeOutput["ItemName"] = Str_Item;//AP
    //                  RowEyeOutput["Value"] = "AP";
    //                  if (Str_Item=="AP")
    //                  {
    //                      RowEyeOutput["Value"] = "AP";
    //                  }
                     
    //              }

    //              DtTestEyeOutput.Rows.Add(RowEyeOutput);

    //          }




    //         pTestEye.RunTest();




    //      }

    //  }

    //  public override bool RunTest()
    //  {

    //      ArrayTempValue.Add(CurrentTemp);

    //      if (bAdjustFlag)

    //      {
    //          Adjust();
    //      }
    //      if (bTestFlag)
    //      {
    //          Test();
    //      }
    //      return true;

    //  }
       
    //}
}
