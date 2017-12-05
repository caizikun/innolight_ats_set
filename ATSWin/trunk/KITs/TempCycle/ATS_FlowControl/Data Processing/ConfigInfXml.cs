using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Linq;
using System.IO;
using System.Data;
using ATS_Framework;
using System.Xml;

namespace ATS
{
    public class TempCycleXml:XmlIO
    {
        public bool isEquipmentInfExists = false;// whether  Equipment Inf exist in Xml that was Builded just now? when the Xml  was Builded just now,there is no Equipment Inf, only Empty Node
        
         public  struct XmlField
        {
            public XElement NodeDataSource;
            public XElement EquipmentListNodel;// Equipment Node
            public XElement ConditionListNodel;// Condition Node
          //  public XmlNode ConditionNodel;// Condition Node

        }

         public XmlField MyXmlField = new XmlField();
         private byte XmlType = 0;//0= Equipment Config,1=ConditionConfig
        public TempCycleXml()
        {

        }

        public TempCycleXml(string StrPath,byte Type=0)//0= Equipment Config,1=ConditionConfig
        {
            XmlType = Type;
            filePath = StrPath;
            if (!File.Exists(filePath))
            {
               
                BuildXml();
                isEquipmentInfExists = false;//Xiaoaoshigedainiao
            }
            else
            {
                doc = XDocument.Load(filePath);
                XmlLoadElment = XElement.Load(filePath);
                MyXmlField.NodeDataSource = GetNode("Node", "Item", "DataSource");
                isEquipmentInfExists = true;

             //   IEnumerable<XElement> rootNode = XmlLoadElment.Element("Equipment");
              
                //NodeEquipmentOffset = GetNode("Node", "Item", "EquipmentOffset");
            }
            if (XmlType == 0)
            {

                MyXmlField.EquipmentListNodel = XmlLoadElment.Element("EquipmentList");
              //  MyXmlField.ConditionListNodel = XmlLoadElment.Element("ConditionList");
            }
            else
            {
                MyXmlField.ConditionListNodel = XmlLoadElment.Element("ConditionList");
            }

        }

     
        override  public bool BuildXml()
        {

            XDocument xdoc = null;

            xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                            new XElement("Config"));
            xdoc.Save(filePath);
            doc = XDocument.Load(filePath);
            XmlLoadElment = XElement.Load(filePath);
            if (XmlType == 0)
            {

                CreatNode("EquipmentList");
            }
            else
            {


                CreatNode("ConditionList");
            }
            if (XmlType == 0)
            {

                MyXmlField.EquipmentListNodel = XmlLoadElment.Element("EquipmentList");
                //  MyXmlField.ConditionListNodel = XmlLoadElment.Element("ConditionList");
            }
            else
            {
                MyXmlField.ConditionListNodel = XmlLoadElment.Element("ConditionList");
            }
            return true;
        }

        #region  DataBase

        public String DbName = "ATS_V2";
        public string Username = "03cc04e804d8047d05a8061f066906c205a4061b066504ed06ae05db069d";
        public string PWD = "05ac06c806b8065d0206061f066906680676";
        public string DataBaseUserLever = "0";
        public string DatabasePath = @"10.160.142.241\ATS_HOME";
        //public string DatabasePath = @"INPCSZ0518\ATS_HOME";

        #endregion
        /// <summary> 
        /// 填写仪器信息
        /// </summary>
        /// <param name="dt">存放仪器信息的DataTable, 源自数据库默认值</param>
        /// <returns>元素节点</returns>
        public bool FitEquipmentToXml(DataTable dt )
        {
            FitEquipmentInputParameterToXml("E3631",dt);
            FitEquipmentInputParameterToXml("TPO4300", dt);

            FitEquipmentInputParameterToXml("INNO25GBERT_V2_ED", dt);
            FitEquipmentInputParameterToXml("INNO25GBERT_V2_PPG", dt);

            FitEquipmentInputParameterToXml("MP1800ED", dt);
            FitEquipmentInputParameterToXml("MP1800PPG", dt);

            FitEquipmentInputParameterToXml("N4960ED", dt);
            FitEquipmentInputParameterToXml("N4960PPG", dt);

            FitEquipmentInputParameterToXml("FLEX86100", dt);

          return true;
      }

        /// <summary> 
        /// 创建节点,示例CreatNode("Node","Item","DataSource")
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <param name="XAttributeName">属性名字</param>
        /// <param name="XAttributeValue">属性的值</param>
        /// <returns>元素节点</returns>
        public XElement CreatNode(string NodeName)
        {
            XElement db = new XElement(NodeName);
            XmlLoadElment.Add(db);
            XmlLoadElment.Save(filePath);
            return db;
        }

        /// <summary> 
        /// 创建节点,示例CreatNode("Node","Item","DataSource")
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <param name="XAttributeName">属性名字</param>
        /// <param name="XAttributeValue">属性的值</param>
        /// <returns>元素节点</returns>
        public XElement CreatNode(XElement Node,string XElementName, string XAttributeName, string XAttributeValue)
        {
            XElement db = new XElement(XElementName, new XAttribute(XAttributeName, XAttributeValue));
            Node.Add(db);
           // XmlLoadElment.Add(db);
            XmlLoadElment.Save(filePath);
            return db;
        }

      /// <summary>
      /// 将某个仪器信息填写到Xml EquipmentNode 中
      /// </summary>
      /// <param name="StrEqName">仪器名称,例如:MP1800PPG</param>
      /// <param name="dt">存放所有仪器参数的DataTable</param>
      /// <returns></returns>
      private bool FitEquipmentInputParameterToXml( string StrEqName ,DataTable dt)
      {  
          string StrNodeName = "Equipment";
          string StrItem = "Item";
        //  XElement myXElement0 = CreatNode(StrNodeName, StrItem, StrEqName);
          XElement myXElement = CreatNode(MyXmlField.EquipmentListNodel,"Equipment", StrItem, StrEqName);
    
          DataRow[] drArray;

          string SelectCondition = "EquipmentName='" + StrEqName+"'";

          drArray = dt.Select(SelectCondition);

            for (int i = 0; i < drArray.Length;i++ )
            {
                AddElement(myXElement, new XElement(drArray[i]["ItemName"].ToString(), drArray[i]["itemValue"].ToString()));
            }
            return true;
        }

      /// <summary> 
      /// 将某一行Condition填充到Xml 的ConditionNode 中
      /// </summary>
      /// <param name="SequenceNo">顺序号</param>
      /// <param name="HeadArray">表头数组</param>
      /// <param name="DataArray">值数组</param>
      /// <returns>元素节点</returns>
      public bool FitConditionInfToXml(string SequenceNo, string[] HeadArray, string[] DataArray)
      {

          string StrItem = "Sequence";
    
          XElement myXElement = CreatNode(MyXmlField.ConditionListNodel, "Condition", StrItem, SequenceNo);

         if (HeadArray.Length != DataArray.Length)
         {
             return false;
         }
         for (int i = 1; i < HeadArray.Length; i++)
        {
            AddElement(myXElement, new XElement(HeadArray[i], DataArray[i]));
            
        }
          return true;
      }

      /// <summary> 
      /// 删除Condition 的所有子节点
      /// </summary>
      /// <returns>是否成功</returns>
      public bool DeleteAllConditionNode()
      {
          var xnl = XmlLoadElment.Nodes();
          IEnumerable<XElement> rootNode = MyXmlField.ConditionListNodel.Elements("Condition");
          rootNode.Remove();
          XmlLoadElment.Save(filePath); 
          return false;

      }



      public TestModeEquipmentParameters[] GetEquipmenParameter(string StrName)
      {

          TestModeEquipmentParameters[] ParametersArray;

          XmlElement MyXmlElement = GetXmlNode(MyXmlField.EquipmentListNodel, "Equipment", "Item", StrName);

          XmlNodeList MyXmlNodeList = MyXmlElement.ChildNodes;

          ParametersArray = new TestModeEquipmentParameters[MyXmlNodeList.Count];

          for (int i = 0; i < MyXmlNodeList.Count; i++)
          {
              ParametersArray[i].FiledName = MyXmlNodeList[i].Name;
              ParametersArray[i].DefaultValue = MyXmlNodeList[i].InnerText;

          }

          return ParametersArray;
      }

   

    

      public DataTable GetConditionTable(DataTable dt)
      {



          XmlElement TempXmlElement = ToXmlElement(MyXmlField.ConditionListNodel); 
          //A.
          //XmlNode MyXmlElement = NodeCondition;

          XmlNodeList MyXmlNodeList = TempXmlElement.ChildNodes;

          for (int i = 0; i < MyXmlNodeList.Count; i++)
          {
              XmlNode TempNode = MyXmlNodeList[i];

              DataRow dr = dt.NewRow();
              //string a = TempNode.Attributes[0].Name;
              //string b = TempNode.Attributes[0].Value;

              dr[TempNode.Attributes[0].Name] = TempNode.Attributes[0].Value;

              foreach (XmlNode A in TempNode.ChildNodes)
              {
                  dr[A.Name] = A.InnerText;

              }
              dt.Rows.Add(dr);
          }

          return dt;
      }

      public XmlElement GetXmlNode(string NodeName, string XAttributeName, string XAttributeValue)
      {


          XmlElement db = null;

          try
          {

              var B = from kk in XmlLoadElment.Descendants(NodeName) where (string)kk.Attribute(XAttributeName) == XAttributeValue select kk;

              foreach (var XmlNode in B)
              {
                  return ToXmlElement(XmlNode);
              }
          }
          catch (System.Exception ex)
          {
              return db;
          }
          return db;


      }
    
      /// <summary> 
    /// 将某一个子节点 例如:GetXmlNode(MyXmlField.EquipmentListNodel,"Equipment", "Item", StrName);
    /// </summary>
    /// <param name="Node">父<节点,例如:EquipmentListNodel/param>
    /// <param name="ChildNodeName">子节点名称</param>
    /// <param name="XAttributeName">参数名称</param>
    /// <param name="XAttributeValue">值</param>
    /// <returns>元素节点</returns>
      public XmlElement GetXmlNode(XElement Node, string ChildNodeName, string XAttributeName, string XAttributeValue)
      {

          XmlElement db = null;

          try
          {
              var B = Node.Elements(ChildNodeName);
              var c = from kk in Node.Descendants(ChildNodeName) where (string)kk.Attribute(XAttributeName) == XAttributeValue select kk;

              foreach (var XmlNode in c)
              {
                  return ToXmlElement(XmlNode);
              }
          }
          catch (System.Exception ex)
          {
              return db;
          }
          return db;


      }

      /// <summary> 
    /// XElement转换为XmlElement 
    /// </summary> 
      public static XmlElement ToXmlElement(XElement xElement)
      {
          if (xElement == null) return null;

          XmlElement xmlElement = null;
          XmlReader xmlReader = null;
          try
          {
              xmlReader = xElement.CreateReader();
              var doc = new XmlDocument();
              xmlElement = doc.ReadNode(xElement.CreateReader()) as XmlElement;
          }
          catch
          {
          }
          finally
          {
              if (xmlReader != null) xmlReader.Close();
          }

          return xmlElement;
      } 
       
    }
}
