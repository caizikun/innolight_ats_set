using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using ATS_Framework;
using System.Xml;

namespace ATS
{
    public class ConfigXmlIO : XmlIO
    {

        // private XDocument doc;

        //private string filePath;
        // private XElement XmlLoadElment;
        private XElement NodeDataSource;
        private XElement NodeEquipment;

        public ConfigXmlIO()
        {
        }

        public ConfigXmlIO(string StrPath)
        {
            filePath = StrPath;
            if (!File.Exists(filePath))
            {
                BuildXml();
            }
            else
            {
                doc = XDocument.Load(filePath);
                XmlLoadElment = XElement.Load(filePath);
                NodeDataSource = GetNode("Node", "Item", "DataSource");
                NodeEquipment = XmlLoadElment.Element("EquipmentList");
                //ChildNodeDCA = GetNode("Equipment", "Power", "Power");
                //ChildNodeThermo = GetNode("Equipment", "Thermocontroller", "TPO4300");
                //ChildNodeOptSwitch = GetNode("Equipment", "OpticalSwitch", "AQ2211OpticalSwitch");
                //ChildNodeDCA = GetNode("Equipment", "DCA", "FLEX86100");
            }

        }

        override public bool BuildXml()
        {
            XDocument xdoc = null;

            xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                            new XElement("Config"));
            xdoc.Save(filePath);
            doc = XDocument.Load(filePath);
            XmlLoadElment = XElement.Load(filePath);
            //-----------------------------------DataSource

            NodeDataSource = CreatNode("Node", "Item", "DataSource");
            AddElement(NodeDataSource, new XElement("Level", "0"));
            AddElement(NodeDataSource, new XElement("Type", "Access"));
            AddElement(NodeDataSource, new XElement("LocationPath", filePath));
            AddElement(NodeDataSource, new XElement("SqlPath", @"INPCSZ0518\ATS_HOME"));
            AddElement(NodeDataSource, new XElement("DbName", @"ATS_V2"));
            AddElement(NodeDataSource, new XElement("User", @"03cc04e804d8047d05a8061f066906c205a4061b066504ed06ae05db069d")); //140918_0
            AddElement(NodeDataSource, new XElement("PWD", @"05ac06c806b8065d0206061f066906680676")); //140918_0

            //-----------------------------------Equipment 
            NodeEquipment = CreatNode("EquipmentList");

            XElement E3631XElement = CreatNode(NodeEquipment, "Node", "EquipE3631", "E3631");
            XElement DP811AXElement = CreatNode(NodeEquipment, "Node", "EquipDP811A", "DP811A");
            XElement AQ2211OptSwitchXElement = CreatNode(NodeEquipment, "Node", "EquipAQ2211OPTICALSWITCH", "AQ2211OPTICALSWITCH");
            XElement Tpo4300XElement = CreatNode(NodeEquipment, "Node", "EquipTPO4300", "TPO4300");
            XElement Scope86100XElement = CreatNode(NodeEquipment, "Node", "EquipFLEX86100", "FLEX86100");
            XElement DCAOffsetXElement = CreatNode(NodeEquipment, "Node", "DCAOffset", "DCAOffset");
            XElement XstreamSetXElement = CreatNode(NodeEquipment, "Node", "XstreamSet", "XstreamSet");

            AddElement(E3631XElement, new XElement("Addr", "5"));
            AddElement(DP811AXElement, new XElement("Addr", "5"));
            AddElement(Tpo4300XElement, new XElement("Addr", "23"));
            AddElement(AQ2211OptSwitchXElement, new XElement("OpticalSwitchSlot", "1"));
            AddElement(AQ2211OptSwitchXElement, new XElement("SwitchChannel", "1"));
            AddElement(AQ2211OptSwitchXElement, new XElement("Addr", "20"));

            AddElement(Scope86100XElement, new XElement("Addr", "7"));
            AddElement(Scope86100XElement, new XElement("FlexOptChannel", "1A"));
            AddElement(Scope86100XElement, new XElement("FlexDcaWavelength", "1"));
            AddElement(Scope86100XElement, new XElement("FilterSwitch", "1"));
            AddElement(Scope86100XElement, new XElement("FlexDcaDataRate", "25.78125e+9"));
            AddElement(Scope86100XElement, new XElement("opticalMaskName", @"C:\Program Files\Keysight\FlexDCA\Demo\Masks\Ethernet\025.78125 - 100GBASE-LR4_Tx_Optical_D31.mskx"));

            AddElement(DCAOffsetXElement, new XElement("Offset1", "1.1"));
            AddElement(DCAOffsetXElement, new XElement("Offset2", "1.2"));
            AddElement(DCAOffsetXElement, new XElement("Offset3", "1.3"));
            AddElement(DCAOffsetXElement, new XElement("Offset4", "1.4"));

            AddElement(XstreamSetXElement, new XElement("TempLow", "0"));
            AddElement(XstreamSetXElement, new XElement("TempAMB", "25"));
            AddElement(XstreamSetXElement, new XElement("TempHigh", "75"));


            return true;
        }

        public bool FitEquipmentInputParameterToXml(string StrNodeName, string StrEqName, string[] HeadArray, string[] DataArray)
        {
            string StrItem = "Equip" + StrEqName;
            XElement myXElement = CreatNode(NodeEquipment, StrNodeName, StrItem, StrEqName);
            if (HeadArray.Length != DataArray.Length)
            {
                return false;
            }

            for (int i = 0; i < HeadArray.Length; i++)
            {
                AddElement(myXElement, new XElement(HeadArray[i], DataArray[i]));
            }
            return true;
        }

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

        public bool DeleteSingleEquipmentNode(string XAttributeValue)
        {
            IEnumerable<XElement> rootNode = NodeEquipment.Elements().ToList();
            foreach (XElement Equipment in rootNode)
            {
                if (Equipment.Attribute("Equip" + XAttributeValue) != null)
                {
                    Equipment.Remove();
                }
            }
            return false;
        }

        public bool DeleteEquipmentNode(string TxEquipmentOrRxEquipment)
        {
            var xnl = XmlLoadElment.Nodes();
            IEnumerable<XElement> rootNode = NodeEquipment.Elements(TxEquipmentOrRxEquipment);
            rootNode.Remove();
            XmlLoadElment.Save(filePath);
            return false;
        }
        #region  DataBase

        public String DbName
        {
            set
            {

                ModifyElement(GetNode("Node", "Item", "DataSource"), "DbName", value);
            }
            get
            {
                return ReadElementValue(GetNode("Node", "Item", "DataSource"), "DbName");

            }

        }

        public string Username
        {
            set
            {

                ModifyElement(GetNode("Node", "Item", "DataSource"), "User", value);
            }
            get
            {
                return ReadElementValue(GetNode("Node", "Item", "DataSource"), "User");

            }
        }
        public string PWD
        {
            set
            {

                ModifyElement(GetNode("Node", "Item", "DataSource"), "PWD", value);
            }
            get
            {
                return ReadElementValue(GetNode("Node", "Item", "DataSource"), "PWD");

            }
        }


        public string DataBaseUserLever
        {

            set
            {
                ModifyElement(GetNode("Node", "Item", "DataSource"), "Level", value);
            }
            get
            {
                return ReadElementValue(GetNode("Node", "Item", "DataSource"), "Level");

            }

        }
        private string databaseType;
        public string DatabaseType
        {
            set
            {
                databaseType = value;
                ModifyElement(NodeDataSource, "Type", value);
            }
            get
            {
                return ReadElementValue(GetNode("Node", "Item", "DataSource"), "Type");
            }
        }
        public string DatabasePath
        {

            set
            {
                if (databaseType.ToUpper() == "LOCATIONDATABASE")
                {
                    ModifyElement(NodeDataSource, "LocationPath", value);
                }

            }
            get
            {
                if (databaseType.ToUpper() == "LOCATIONDATABASE")
                {
                    // ModifyElement(NodeDataSource, "LocationPath", value);
                    return ReadElementValue(GetNode("Node", "Item", "DataSource"), "LocationPath");
                }
                else// Sql
                {
                    // ModifyElement(NodeDataSource, "SqlPath", value);
                    return ReadElementValue(GetNode("Node", "Item", "DataSource"), "SqlPath");
                }
            }
        }
        #endregion

        #region Equipment
        public string E3631Addr
        {
            get
            {
                return ReadElementValue(GetNode("Node", "EquipE3631", "E3631"), "Addr");
            }
        }
        public string DP811AAddr
        {
            get
            {
                return ReadElementValue(GetNode("Node", "EquipE3631", "E3631"), "Addr");
            }
        }
        public string TPO4300Addr
        {
            get
            {
                return ReadElementValue(GetNode("Node", "EquipTPO4300", "TPO4300"), "Addr");
            }
        }
        public string AQ2211OptSwitchAddr
        {
            get
            {
                return ReadElementValue(GetNode("Node", "EquipAQ2211OPTICALSWITCH", "AQ2211OPTICALSWITCH"), "Addr");
            }
        }
        public string AQ2211OptSwitchSlot
        {
            get
            {
                return ReadElementValue(GetNode("Node", "EquipAQ2211OPTICALSWITCH", "AQ2211OPTICALSWITCH"), "OpticalSwitchSlot");
            }
        }
        public string FLEXAddr
        {
            get
            {
                return ReadElementValue(GetNode("Node", "EquipFLEX86100", "FLEX86100"), "Addr");
            }
        }
        public string FLEXOptChannel
        {
            get
            {
                return ReadElementValue(GetNode("Node", "EquipFLEX86100", "FLEX86100"), "FlexOptChannel");
            }
        }
        public string FLEX86100WaveLength
        {
            get
            {
                return ReadElementValue(GetNode("Node", "EquipFLEX86100", "FLEX86100"), "FlexDcaWavelength");
            }
        }
        public string FLEXFilterSwitch
        {
            get
            {
                return ReadElementValue(GetNode("Node", "EquipFLEX86100", "FLEX86100"), "FilterSwitch");
            }
        }
        public string FLEXDCADataRate
        {
            get
            {
                return ReadElementValue(GetNode("Node", "EquipFLEX86100", "FLEX86100"), "FlexDcaDataRate");
            }
        }
        public string FLEXMaskName
        {
            get
            {
                return ReadElementValue(GetNode("Node", "EquipFLEX86100", "FLEX86100"), "opticalMaskName");
            }
        }
        #endregion
        #region DCAOffset
        public string DCAOffset1
        {
            set
            {
                ModifyElement(NodeEquipment, "Offset1", value);
            }
            get
            {
                return ReadElementValue(GetNode("Node", "DCAOffset", "DCAOffset"), "Offset1");
            }
        }
        public string DCAOffset2
        {
            set
            {
                ModifyElement(NodeEquipment, "Offset2", value);
            }
            get
            {
                return ReadElementValue(GetNode("Node", "DCAOffset", "DCAOffset"), "Offset2");
            }
        }
        public string DCAOffset3
        {
            set
            {
                ModifyElement(NodeEquipment, "Offset3", value);
            }
            get
            {
                return ReadElementValue(GetNode("Node", "DCAOffset", "DCAOffset"), "Offset3");
            }
        }
        public string DCAOffset4
        {
            set
            {
                ModifyElement(NodeEquipment, "Offset4", value);
            }
            get
            {
                return ReadElementValue(GetNode("Node", "DCAOffset", "DCAOffset"), "Offset4");
            }
        }
        #endregion
        #region XstreamSet
        public string TempLow
        {
            set
            {
                ModifyElement(NodeEquipment, "TempLow", value);
            }
            get
            {
                return ReadElementValue(GetNode("Node", "XstreamSet", "XstreamSet"), "TempLow");
            }
        }
        public string TempAMB
        {
            set
            {
                ModifyElement(NodeEquipment, "TempAMB", value);
            }
            get
            {
                return ReadElementValue(GetNode("Node", "XstreamSet", "XstreamSet"), "TempAMB");
            }
        }
        public string TempHigh
        {
            set
            {
                ModifyElement(NodeEquipment, "TempHigh", value);
            }
            get
            {
                return ReadElementValue(GetNode("Node", "XstreamSet", "XstreamSet"), "TempHigh");
            }
        }
        #endregion
    }
}
