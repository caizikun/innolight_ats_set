using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using ATS;

namespace Maintain
{
    public class ConfigXmlIO : XmlIO
    {
        private XElement NodeDataSource;
        private XElement NodeDataState;
        public ConfigXmlIO()
        {
        }

        public ConfigXmlIO(string StrPath)
            : base(StrPath)
        {
            try
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
                    NodeDataSource = GetNode("Node", "Server", "Server");
                    NodeDataState = GetNode("Node", "ShowState", "State");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public override bool BuildXml()
        {
            try
            {
                XDocument xdoc = null;

                xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                                new XElement("Config"));
                xdoc.Save(filePath);
                doc = XDocument.Load(filePath);
                XmlLoadElment = XElement.Load(filePath);
                //-----------------------------------DataSource

                NodeDataSource = CreatNode("Node", "Server", "Server");
                AddElement(NodeDataSource, new XElement("ServerName", @"INPCSZ0518\ATS_HOME")); //140718_1
                AddElement(NodeDataSource, new XElement("ATSDBName", @"ATSHOME")); //140911_0
                AddElement(NodeDataSource, new XElement("EDVTDBName", @"EDVTHOME")); //140911_0

                ////新增默认密码 FOR ATS:ATSUser ,EDVTUser  //140918_0
                //AddElement(NodeDataSource, new XElement("ATSUser", @"03cc04e804d804f506b605e306a5"));         //140918_0  
                //AddElement(NodeDataSource, new XElement("EDVTUser", @"040803f8050504e604f406b505e206a4"));    //140918_0
                //AddElement(NodeDataSource, new XElement("ATSPWD", @"05ac06c806b802070620066a0669067702e302c402d202fe"));      //140918_0
                //AddElement(NodeDataSource, new XElement("EDVTPWD", @"05e805d806e506c60206061f06690668067602e202c302d102fd")); //140918_0

                //新增默认密码 FOR Maintain   //140918_0
                AddElement(NodeDataSource, new XElement("ATSUser", @"03cc04e804d8047d05a8061f066906c205a4061b066504ed06ae05db069d")); //140918_0
                AddElement(NodeDataSource, new XElement("EDVTUser", @"040803f8050504e6047c05a7061e066806c105a3061a066404ec06ad05da069c")); //140918_0
                AddElement(NodeDataSource, new XElement("ATSPWD", @"05ac06c806b8065d0206061f066906680676")); //140918_0
                AddElement(NodeDataSource, new XElement("EDVTPWD", @"05e805d806e506c6065c0205061e066806670675")); //140918_0

                ////新增默认密码 FOR BackGround   //140918_0
                //AddElement(NodeDataSource, new XElement("ATSUser", @"03cc04e804d803d805a805c5063c05ff06a3067506ce066405cd")); //140918_0
                //AddElement(NodeDataSource, new XElement("EDVTUser", @"040803f8050504e603d705a705c4063b05fe06a2067406cd066305cc")); //140918_0
                //AddElement(NodeDataSource, new XElement("ATSPWD", @"03cc04e804d803d80206061f066906680676")); //140918_0
                //AddElement(NodeDataSource, new XElement("EDVTPWD", @"040803f8050504e603d70205061e066806670675")); //140918_0

                NodeDataState = CreatNode("Node", "ShowState", "State");
                
                AddElement(NodeDataState, new XElement("showBtnLogin", "True"));
                AddElement(NodeDataState, new XElement("showBtnExport", "false"));
                AddElement(NodeDataState, new XElement("showBtnChangePwd", "false"));

                return true;
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public string ServerName
        {
            set
            {
                ModifyElement(NodeDataSource, "ServerName", value);
            }
            get
            {
                return ReadElementValue(NodeDataSource, "ServerName");
            }
        }


        public string ATSDBName    //140911_0
        {
            set
            {
                ModifyElement(NodeDataSource, "ATSDBName", value);
            }
            get
            {
                return ReadElementValue(NodeDataSource, "ATSDBName");
            }
        }

        public string EDVTDBName    //140911_0
        {
            set
            {
                ModifyElement(NodeDataSource, "EDVTDBName", value);
            }
            get
            {
                return ReadElementValue(NodeDataSource, "EDVTDBName");
            }
        }

        public string EDVTUser    //140918_0
        {
            set
            {
                ModifyElement(NodeDataSource, "EDVTUser", value);
            }
            get
            {
                return ReadElementValue(NodeDataSource, "EDVTUser");
            }
        }

        public string EDVTPWD    //140918_0
        {
            set
            {
                ModifyElement(NodeDataSource, "EDVTPWD", value);
            }
            get
            {
                return ReadElementValue(NodeDataSource, "EDVTPWD");
            }
        }

        public string ATSUser    //140918_0
        {
            set
            {
                ModifyElement(NodeDataSource, "ATSUser", value);
            }
            get
            {
                return ReadElementValue(NodeDataSource, "ATSUser");
            }
        }

        public string ATSPWD    //140918_0
        {
            set
            {
                ModifyElement(NodeDataSource, "ATSPWD", value);
            }
            get
            {
                return ReadElementValue(NodeDataSource, "ATSPWD");
            }
        }
        
        public bool showBtnLogin
        {
            set
            {
                ModifyElement(NodeDataState, "showBtnLogin", value.ToString());
            }
            get
            {
                return (ReadElementValue(NodeDataState, "showBtnLogin").ToString().ToUpper() == "TRUE" ? true : false);
            }
        }
        public bool showBtnExport
        {
            set
            {
                ModifyElement(NodeDataState, "showBtnExport", value.ToString());
            }
            get
            {
                return (ReadElementValue(NodeDataState, "showBtnExport").ToString().ToUpper() == "TRUE" ? true : false);
            }
        }
        public bool showBtnChangePwd
        {
            set
            {
                ModifyElement(NodeDataState, "showBtnChangePwd", value.ToString());
            }
            get
            {
                return (ReadElementValue(NodeDataState, "showBtnChangePwd").ToString().ToUpper() == "TRUE" ? true : false);
            }
        }
        
    }


}
