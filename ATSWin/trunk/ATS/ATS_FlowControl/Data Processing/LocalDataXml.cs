using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Data;
using System.Collections;
using System.Xml;

namespace ATS
{

    public class TopoTestDataColums
    {
        public Byte Result;
        public string SpecMaX;
        public string SpecMin;
        public string ItemName;
        public string ItemValue;

        public TopoTestDataColums(byte bResult, string sSpecMaX, string sSpecMin, string sItemName, string sItemValue)
        {
            Result = bResult;
            SpecMaX = sSpecMaX;
            SpecMin = sSpecMin;
            ItemValue = sItemValue;
            ItemName = sItemName;
        }
    }
    public class ProcessTableColums
    {

        public string ModelName;
        public string ItemName;
        public string ItemValue;

        public ProcessTableColums(string sModelName, string sItemName, string SValue)
        {
            ModelName = sModelName;

            ItemName = sItemName;
            ItemValue = SValue;
        }
    }
    public class RunRecordTableColums
    {
        public string StrSN;
        public string StrPID;
        public string StrStartTime;
        public string StrEndTime;
        public string StrFw;
        public string StrIP;
        public string StrLightSource;
        public string StrLSER;
        public string StrRemark;



        public RunRecordTableColums(string SN, string Pid, string StartTime, string EndTime, string Fw, string ip, string LightSource, string Remark)
        {
            StrSN = SN;
            StrPID = Pid;
            StrStartTime = StartTime;
            StrEndTime = EndTime;
            StrFw = Fw;
            StrIP = ip;
            StrLightSource = LightSource;
            StrRemark = Remark;
        }
    }
    public class KeyAndValue
    {
        public string StrKey;
        public string StrValue;
        public KeyAndValue(string Skey, string Svalue)
        {
            StrKey = Skey;
            StrValue = Svalue;
        }
    } 

    public class LocatDataXml : XmlIO
    {
        XDocument doc;

       // private string filePath;
      //  private XElement XmlLoadElment;
        private XElement NodeSN;
        private XElement NodeTopoLog;
        private XElement NodeTestModel;
        private XElement NodeCoefBackUp;

        private XElement NodeTestModelProcessData;
        private XElement NodeTestModelTestData;
        //private XElement NodeEquipmentOffset;
        //private XElement NodePrtScPath;F

        // private XElement XmlLoadElment;
        public LocatDataXml()
        {
        }

        public LocatDataXml(string StrPath, RunRecordTableColums P)
        {
            filePath = StrPath;
            if (!File.Exists(filePath))
            {
                XmlLoadElment = new XElement("TopoRunRecordTable");
                XmlLoadElment.SetAttributeValue("SN", P.StrSN);
                XmlLoadElment.SetAttributeValue("StartTime", P.StrStartTime);
                XmlLoadElment.SetAttributeValue("EndTime", P.StrEndTime);
                XmlLoadElment.SetAttributeValue("FWRev", P.StrFw);
                XmlLoadElment.SetAttributeValue("IP", P.StrIP);
                XmlLoadElment.SetAttributeValue("LightSource", P.StrLightSource);
                XmlLoadElment.SetAttributeValue("Remark", P.StrRemark);
                XmlLoadElment.SetAttributeValue("PID", P.StrPID);
                XmlLoadElment.Save(filePath);
            }
            else
            {
               XmlLoadElment = XElement.Load(filePath);
            }
            NodeSN = XmlLoadElment;

        }
        public override bool BuildXml(RunRecordTableColums P)
        {

            


            XmlLoadElment = new XElement("TopoRunRecordTable");
            XmlLoadElment.SetAttributeValue("SN", P.StrSN);
            XmlLoadElment.SetAttributeValue("StartTime", P.StrStartTime);
            XmlLoadElment.SetAttributeValue("EndTime", P.StrEndTime);
            XmlLoadElment.SetAttributeValue("FWRev", P.StrFw);
            XmlLoadElment.SetAttributeValue("IP", P.StrIP);
            XmlLoadElment.SetAttributeValue("LightSource", P.StrLightSource);
            XmlLoadElment.SetAttributeValue("Remark", P.StrRemark);
            XmlLoadElment.SetAttributeValue("PID", P.StrRemark);
            //PID

            //xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), NodeSN);
            //xdoc.Save(filePath);
            //doc = XDocument.Load(filePath);
            //XmlLoadElment = XElement.Load(filePath);

            XmlLoadElment.Save(filePath);

            NodeSN = XmlLoadElment;
            return true;
        }
        #region  CreatNode



        //public XElement CreartSnNode(RunRecordTableColums P)
        //{

        //    NodeSN = CreatNode("TopoRunRecordTable");

        //    NodeSN.SetAttributeValue("SN", P.StrSN);
        //    NodeSN.SetAttributeValue("StartTime", P.StrStartTime);
        //    NodeSN.SetAttributeValue("EndTime", P.StrEndTime);
        //    NodeSN.SetAttributeValue("FWRev", P.StrFw);
        //    NodeSN.SetAttributeValue("IP", P.StrIP);
        //    NodeSN.SetAttributeValue("LightSource", P.StrLightSource);
        //    NodeSN.SetAttributeValue("Remark", P.StrRemark);

        //    XmlLoadElment.Save(filePath);
        //    return NodeSN;
        //}

        public XElement CreartTopoLogNode( string Temp, string Voltage, string Channel, string CtrlType, string StartTime)
        {

            NodeTopoLog = AddElement(NodeSN, "TopologRecord");
            //NodeTopoLog = new XElement("TopologRecord");
            List<KeyAndValue> T = new List<KeyAndValue>();

            T.Add(new KeyAndValue("Temp",Temp));
            T.Add(new KeyAndValue("Voltage", Voltage));
            T.Add(new KeyAndValue("Channel", Channel));
            T.Add(new KeyAndValue("CtrlType", CtrlType));
            T.Add(new KeyAndValue("StartTime", StartTime));

            //T.Add(new KeyAndValue("EndTime", EndTime));
            //T.Add(new KeyAndValue("Result", Result.ToString()));
            //T.Add(new KeyAndValue("TestLog", TestLog));



            AddAttribute(NodeTopoLog, T);

            //NodeSN.Add(NodeTopoLog);

            //NodeSN.Save(filePath);
          //  XmlLoadElment.Save(filePath);

            return NodeTopoLog;
        }
        public bool AddToplogInf(  string EndTime, byte Result, string TestLog)
        {

            NodeTopoLog.SetAttributeValue("EndTime", EndTime);
            NodeTopoLog.SetAttributeValue("Result", Result.ToString());
            NodeTopoLog.SetAttributeValue("TestLog", TestLog);
            return true;
        }

        public XElement CreartTestModelNode(string StrTestModelName)
        {

            NodeTestModel = AddElement(NodeTopoLog, "ModelName");
            ModifyAttributeValue(NodeTestModel, "ModelName", StrTestModelName);
       
            return NodeTestModel;
        }


        public XElement CreartProcessData( List<ProcessTableColums> K)
        {
            NodeTestModelProcessData = AddElement(NodeTestModel, "TopoProcData");
            ModifyAttributeValue(NodeTestModelProcessData, "TopoProcData", "TopoTestData");
          
            for (int i = 0; i < K.Count; i++)
            {
                XElement xe = new XElement("TopoProcData", new XAttribute("ModelName", K[i].ModelName), new XAttribute("ItemName", K[i].ItemName), new XAttribute("ItemValue", K[i].ItemValue));

                AddElement(NodeTestModelProcessData, xe);
            }

            return NodeTestModel;
        }

        public XElement CreartTestData(List<TopoTestDataColums> K)
        {
           string TableName = "TopoTestData";

           XElement   NodeTestData = AddElement(NodeTestModel, TableName);


            for (int i = 0; i < K.Count; i++)
            {
                XElement xe = new XElement(TableName, new XAttribute("Result", K[i].Result), new XAttribute("ItemName", K[i].ItemName), new XAttribute("ItemValue", K[i].ItemValue), new XAttribute("SpecMax", K[i].SpecMaX), new XAttribute("SpecMin", K[i].SpecMin));

                AddElement(NodeTestData, xe);
            }
            return NodeTestModel;
        }

        public XElement CreartCoefBackNode()
        {
            string TableName = "TopoTestCoefBackup";

            NodeCoefBackUp = AddElement(NodeSN, TableName);

            NodeCoefBackUp.SetAttributeValue("MCoef", "TopoTestCoefBackup");

            return NodeCoefBackUp;
        }

        public bool CreartCoefBackChildNode(string ItemValue,string ItemSize,string Page,string StartAddr)
        {
            try
            {
                AddElement(NodeCoefBackUp, new XElement("TopoTestCoefBackup", new XAttribute("ItemSize", ItemSize), new XAttribute("ItemValue", ItemValue), new XAttribute("Page", Page), new XAttribute("StartAddr", StartAddr)));
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        public bool ModifyRunRecordEndTime()
        {
            NodeSN.SetAttributeValue("EndTime", DateTime.Now.ToString());
            XmlLoadElment.Save(filePath);
            return true;
        }

        public bool ModifyAttributeValue(XElement Node, string Skey, string sValue)
        {
            Node.SetAttributeValue(Skey, sValue);
            XmlLoadElment.Save(filePath);
            return true;
        }

        private bool AddAttribute(XElement Node, List<KeyAndValue> T)
        {

            for (int i = 0; i < T.Count; i++)
            {

                Node.SetAttributeValue(T[i].StrKey, T[i].StrValue);

            }
            Node.Save(filePath);
            XmlLoadElment.Save(filePath);
            return true;
        }

#region  Terry Add

        public static List<string> AddSqlCmdLst(List<string> sqlCmdLst, List<string> addCmdLst)
        {
            if (addCmdLst != null)
            {
                foreach (string x in addCmdLst)
                {
                    sqlCmdLst.Add(x);
                }
            }
            return sqlCmdLst;
        }

        static List<string> GetCmdTxtListFromAttrs(List<KeyValue> listAttrs, bool is1stRun = false)
        {
            List<string> cmdTxtList = new List<string>();
            if (is1stRun)   //需要创建lstIdx_TableName
            {
                cmdTxtList.Add(@"Declare @LstIdx_TopoRunRecordTable int;");
                cmdTxtList.Add(@"Declare @LstIdx_TopoLogRecord int;");
                cmdTxtList.Add(@"Declare @LstIdx_TopoTestCoefBackup int;");
                cmdTxtList.Add(@"Declare @LstIdx_TopoTestData int;");
                cmdTxtList.Add(@"Declare @LstIdx_TopoProcData int;");
            }
            string sql1 = "Insert Into";
            string sql2 = ") Values(";
            string sql3 = ");";
            string tableName = "";
            //string sql = "Insert Into @TableName (UsersID,ProjectID) Values(@UsersID,@ProjectID)";
            for (int i = 0; i < listAttrs.Count; i++)
            {
                if (i == 0)
                {
                    tableName = listAttrs[i].NodeName.ToString();
                    sql1 += " " + listAttrs[i].NodeName + " (";
                }
                if (i < listAttrs.Count - 1)
                {
                    sql1 += listAttrs[i].Key + ",";
                    sql2 += "'" + listAttrs[i].Val + "',";
                }
                else
                {
                    sql1 += listAttrs[i].Key;
                    sql2 += "'" + listAttrs[i].Val + "'";
                }
            }
            string sql = sql1 + sql2 + sql3;

            cmdTxtList.Add(sql);

            if (!string.IsNullOrWhiteSpace(tableName))
            {
                cmdTxtList.Add(@"set @LstIdx_" + tableName + " = (Select Ident_Current('" + tableName + "'))");
            }

            return cmdTxtList;
        }

        static List<string> GetCmdTxtListFromAttrs(List<KeyValue> lstAttrs, List<KeyValue> fkLst)
        {
            List<string> cmdTxtList = new List<string>();
            string sql1 = "Insert Into";
            string sql2 = ") Values(";
            string sql3 = ");";
            string tableName = "";
            for (int i = 0; i < lstAttrs.Count; i++)
            {
                if (i == 0)
                {
                    tableName = lstAttrs[i].NodeName.ToString();
                    sql1 += " " + lstAttrs[i].NodeName + " (";
                }

                sql1 += lstAttrs[i].Key + ",";
                sql2 += "'" + lstAttrs[i].Val + "',";
            }


            for (int i = 0; i < fkLst.Count; i++)
            {
                if (i < fkLst.Count - 1)
                {
                    sql1 += fkLst[i].Key + ",";
                    sql2 += "'" + fkLst[i].Val + "',";
                }
                else
                {
                    sql1 += fkLst[i].Key;
                    sql2 += "(" + fkLst[i].Val + ")";
                }
            }
            string sql = sql1 + sql2 + sql3;
            cmdTxtList.Add(sql);
            if (!string.IsNullOrWhiteSpace(tableName))
            {
                cmdTxtList.Add(@"set @LstIdx_" + tableName + " = (Select Ident_Current('" + tableName + "'))");
            }

            return cmdTxtList;
        }

        static List<string> GetTopoRunRecordTableSql(XmlNode xNode)
        {
            List<string> pLst = new List<string>();
            if (xNode.Name.Trim().ToUpper() == "TopoRunRecordTable".ToUpper())
            {
                List<KeyValue> pAttrs = GetNodeAttrs(xNode);  //获取节点属性值
#if debug
                ReadXML.WriteLogs(pAttrs, "");
#endif
                pLst = GetCmdTxtListFromAttrs(pAttrs, true);
            }
            return pLst;
        }

        static List<string> GetTopoLogRecordSql(XmlNode xNode)
        {
            List<string> pLst = new List<string>();
            if (xNode.Name.Trim().ToUpper() == "TopoLogRecord".ToUpper())
            {
                List<KeyValue> pAttrs = GetNodeAttrs(xNode);  //获取节点属性值
#if DEBUG
                WriteLogs(pAttrs, "\t");
#endif
                KeyValue pFK1 = new KeyValue(xNode.Name, "RunRecordID", "@LstIdx_TopoRunRecordTable");
                List<KeyValue> pFKlst1 = new List<KeyValue>();
                pFKlst1.Add(pFK1);
                pLst = GetCmdTxtListFromAttrs(pAttrs, pFKlst1);
            }
            return pLst;
        }

        static List<string> GetTopoTestDataSql(XmlNode xNode)
        {
            List<string> pLst = new List<string>();
            if (xNode.Name.Trim().ToUpper() == "TopoTestData".ToUpper())
            {
                List<KeyValue> pAttrs = GetNodeAttrs(xNode);  //获取节点属性值
#if DEBUG
                WriteLogs(pAttrs, "\t\t");
#endif
                KeyValue pFK1 = new KeyValue(xNode.Name, "PID", "@LstIdx_TopoLogRecord");    //
                List<KeyValue> pFKlst1 = new List<KeyValue>();
                pFKlst1.Add(pFK1);
                pLst = GetCmdTxtListFromAttrs(pAttrs, pFKlst1);
            }
            return pLst;
        }

        static List<string> GetTopoProcDataSql(XmlNode xNode)
        {
            List<string> pLst = new List<string>();
            if (xNode.Name.Trim().ToUpper() == "TopoProcData".ToUpper())
            {
                List<KeyValue> pAttrs = GetNodeAttrs(xNode);  //获取节点属性值
#if DEBUG
                WriteLogs(pAttrs, "\t\t");
#endif
                KeyValue pFK1 = new KeyValue(xNode.Name, "PID", "@LstIdx_TopoLogRecord");
                List<KeyValue> pFKlst1 = new List<KeyValue>();
                pFKlst1.Add(pFK1);
                pLst = GetCmdTxtListFromAttrs(pAttrs, pFKlst1);
            }
            return pLst;
        }

        static List<string> GetTopoTestCoefBackupSql(XmlNode xNode)
        {
            List<string> pLst = new List<string>();
            if (xNode.Name.Trim().ToUpper() == "TopoTestCoefBackup".ToUpper())
            {
                List<KeyValue> pAttrs = GetNodeAttrs(xNode);  //获取节点属性值
#if DEBUG
                WriteLogs(pAttrs, "\t");
#endif
                KeyValue pFK1 = new KeyValue(xNode.Name, "PID", "@LstIdx_TopoRunRecordTable");
                List<KeyValue> pFKlst1 = new List<KeyValue>();
                pFKlst1.Add(pFK1);
                pLst = GetCmdTxtListFromAttrs(pAttrs, pFKlst1);
            }
            return pLst;
        }

        public static List<string> GetSqlCmdFromXml(string path)
        {
            List<string> sqlLst = new List<string>();
            XmlNodeList XLst = GetNodesByRoot(path);
            foreach (XmlNode pNode in XLst)
            {
                sqlLst = GetTopoRunRecordTableSql(pNode); //获取根节点属性值 TopoRunRecordTable

                foreach (XmlNode p1stChildNode in pNode.ChildNodes)  //TopoLog1,MCoef
                {
                    sqlLst = AddSqlCmdLst(sqlLst, GetTopoLogRecordSql(p1stChildNode));  //获取一级节点属性值 TopoLogRecord

                    foreach (XmlNode p2ndChildNode in p1stChildNode.ChildNodes)
                    {
                        sqlLst = AddSqlCmdLst(sqlLst, GetTopoTestCoefBackupSql(p2ndChildNode)); //获取二级节点属性值 TopoTestCoefBackup需要保存,Model无需保存
                        foreach (XmlNode p3ndChildNode in p2ndChildNode.ChildNodes)
                        {
                            //获取三级节点属性值   data&proc无需保存        
                            //List<XMLTest.KeyValue> p4thListAttrs = ReadXML.GetNodeAttrs(p3ndChildNode); 

                            foreach (XmlNode p4thChildNode in p3ndChildNode.ChildNodes)  //获取四级节点   //data+proc
                            {
                                sqlLst = AddSqlCmdLst(sqlLst, GetTopoTestDataSql(p4thChildNode));
                                sqlLst = AddSqlCmdLst(sqlLst, GetTopoProcDataSql(p4thChildNode));
                            }
                        }
                    }
                }
            }
            return sqlLst;
        }

        public static XmlNodeList GetNodesByRoot(string strFilePath)
        {
            List<KeyValue> myLst = new List<KeyValue>();
            XmlDocument doc = new XmlDocument();
            doc.Load(strFilePath);
            XmlNodeList myNodeList = doc.SelectNodes(doc.DocumentElement.Name);

            return myNodeList;
        }

        public static List<KeyValue> GetNodeValue(string strFilePath, string nodeName)
        {
            List<KeyValue> myLst = new List<KeyValue>();
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            doc.Load(strFilePath);
            //XmlNode xNode;
            //xNode = doc.SelectSingleNode(nodeName);
            //使用xpath表达式选择文档中所有的子节点
            XmlNodeList myNodeList = doc.SelectNodes(nodeName);
            if (myNodeList != null)
            {
                foreach (XmlNode pNode in myNodeList)
                {
                    foreach (XmlNode xNode in pNode.ChildNodes)
                    {
                        KeyValue pKeyValue = new KeyValue();
                        pKeyValue.NodeName = nodeName;
                        pKeyValue.Key = xNode.Name;
                        pKeyValue.Val = xNode.Value;
                        myLst.Add(pKeyValue);
                    }
                }
            }
            return myLst;
        }

        public static List<KeyValue> GetNodeAttrs(string strFilePath, string nodeName)
        {
            List<KeyValue> myLst = new List<KeyValue>();
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            doc.Load(strFilePath);
            //XmlNode xNode;
            //xNode = doc.SelectSingleNode(nodeName);
            //使用xpath表达式选择文档中所有的子节点
            XmlNodeList myNodeList = doc.SelectNodes(nodeName);
            if (myNodeList != null)
            {
                foreach (XmlNode pNode in myNodeList)
                {
                    foreach (XmlAttribute xAttr in pNode.Attributes)
                    {
                        KeyValue pKeyValue = new KeyValue();
                        pKeyValue.NodeName = nodeName;
                        pKeyValue.Key = xAttr.Name;
                        pKeyValue.Val = xAttr.Value;
                        myLst.Add(pKeyValue);
                    }
                }
            }
            return myLst;
        }

        public static List<KeyValue> GetNodeAttrs(XmlNode xNode)
        {
            List<KeyValue> myLst = new List<KeyValue>();
            foreach (XmlAttribute xAttr in xNode.Attributes)
            {
                KeyValue pKeyValue = new KeyValue();
                pKeyValue.NodeName = xNode.Name;
                pKeyValue.Key = xAttr.Name;
                pKeyValue.Val = xAttr.Value;
                myLst.Add(pKeyValue);
            }
            return myLst;
        }

        public static void WriteLogs(List<KeyValue> pAttrs, string xx = "")
        {
            StreamWriter sw = new StreamWriter(System.Windows.Forms.Application.StartupPath + @"\logs_" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
            if (pAttrs != null)
            {
                foreach (KeyValue pVal in pAttrs)
                {
                    sw.WriteLine(xx + pVal.NodeName + "-->" + pVal.Key + ":" + pVal.Val);
                }
                sw.WriteLine();
            }
            if (sw != null)
            {
                sw.Close();
            }
        }

#endregion


    }
    public class KeyValue
    {
        public object NodeName;
        public object Key;
        public object Val;

        public KeyValue()
        {

        }

        public KeyValue(object nodeName, object key, object val)
        {
            NodeName = nodeName;
            Key = key;
            Val = val;
        }
    }

}

   
