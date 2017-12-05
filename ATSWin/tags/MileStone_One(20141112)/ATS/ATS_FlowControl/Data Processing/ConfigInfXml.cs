using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace ATS
{
    public class ConfigXmlIO:XmlIO
    {
        
       

       // private XDocument doc;

        //private string filePath;
        // private XElement XmlLoadElment;
        private XElement NodeDataSource;
        private XElement NodeEquipmentOffset;
       // private XElement XmlLoadElment;
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
                NodeEquipmentOffset = GetNode("Node", "Item", "EquipmentOffset");
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
            //-----------------------------------DataSource

            NodeDataSource = CreatNode("Node","Item","DataSource");
            AddElement(NodeDataSource, new XElement("Level", "0"));
            AddElement(NodeDataSource, new XElement("Type", "Access"));
            AddElement(NodeDataSource, new XElement("LocationPath", filePath));
            AddElement(NodeDataSource, new XElement("SqlPath", @"INPCSZ0518\ATS_HOME"));

            // AddElement(NodeDataSource, new XElement("Level", "0"));
            //-----------------------------------DataSource               
            NodeEquipmentOffset = CreatNode("Node", "Item", "EquipmentOffset");

            AddElement(NodeEquipmentOffset, new XElement("Scope", "0,0,0,0"));
            AddElement(NodeEquipmentOffset, new XElement("Attennuator", "0,0,0,0"));
            AddElement(NodeEquipmentOffset, new XElement("Vcc", "0"));
            AddElement(NodeEquipmentOffset, new XElement("Icc", "0"));

            return true;
        }

        #region  DataBase
        public string DataBaseUserLever
        {
           
            set
            {
              //  GetNode("Node", "Item", "DataSource");
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
                else// Sql
                {
                    ModifyElement(NodeDataSource, "SqlPath", value);
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

                //return ReadElementValue("DataSource", "Path");
            }
        }
        #endregion
        #region EquipmentOffset

        public string ScopeOffset
        {

            set
            {
                ModifyElement(NodeEquipmentOffset, "Scope", value);
            }
            get
            {
                return ReadElementValue(NodeEquipmentOffset, "Scope");
            }//GetNode("Node", "Item", "EquipmentOffset")
        }
        public string AttennuatorOffset
        {
            set
            {
                ModifyElement(NodeEquipmentOffset, "Attennuator", value);

            }
            get
            {
                return ReadElementValue(NodeEquipmentOffset, "Attennuator");
            }

        }
        //public string VccOffset
        //{

        //    set
        //    {
        //        ModifyElement(NodeEquipmentOffset, "Vcc", value);
        //    }
        //    get
        //    {
        //        return ReadElementValue(GetNode("Node", "Item", "EquipmentOffset"), "Vcc");
        //    }
        //}
        public string IccOffset
        {

            set
            {
                ModifyElement(NodeEquipmentOffset, "Icc", value);
            }
            get
            {
                return ReadElementValue(GetNode("Node", "Item", "EquipmentOffset"), "Icc");
            }
        }

        #endregion

    }
}
