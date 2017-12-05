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

        #region Config.XML Node~
        public string ServerName
        {
            set
            {
                if (ModifyElement(NodeDataSource, "ServerName", value))
                {
                    MessageBox.Show("Set ServerName=" + value.ToString() + " Failed");
                }
            }
            get
            {
                string ss = ReadElementValue(NodeDataSource, "ServerName");
                return (ss == null ? "" : ss);
            }
        }

        public string ATSDBName    //140911_0
        {
            set
            {
                if (!ModifyElement(NodeDataSource, "ATSDBName", value))
                {
                    MessageBox.Show("Set ATSDBName=" + value.ToString() + " Failed");
                }
            }
            get
            {
                string ss = ReadElementValue(NodeDataSource, "ATSDBName");
                return (ss == null ? "" : ss);
            }
        }

        public string EDVTDBName    //140911_0
        {
            set
            {
                if (!ModifyElement(NodeDataSource, "EDVTDBName", value))
                {
                    MessageBox.Show("Set EDVTDBName=" + value.ToString() + " Failed");
                }
            }
            get
            {
                string ss = ReadElementValue(NodeDataSource, "EDVTDBName");
                return (ss == null ? "" : ss);
            }
        }

        public string EDVTUser    //140918_0
        {
            set
            {
                if (!ModifyElement(NodeDataSource, "EDVTUser", value))
                {
                    MessageBox.Show("Set EDVTUser=" + value.ToString() + " Failed");
                }
            }
            get
            {
                string ss = ReadElementValue(NodeDataSource, "EDVTUser");
                return (ss == null ? "" : ss);
            }
        }

        public string EDVTPWD    //140918_0
        {
            set
            {
                if (!ModifyElement(NodeDataSource, "EDVTPWD", value))
                {
                    MessageBox.Show("Set EDVTPWD=" + value.ToString() + " Failed");
                }

            }
            get
            {
                string ss = ReadElementValue(NodeDataSource, "EDVTPWD");
                return (ss == null ? "" : ss);
            }
        }

        public string ATSUser    //140918_0
        {
            set
            {
                if (!ModifyElement(NodeDataSource, "ATSUser", value))
                {
                    MessageBox.Show("Set ATSUser=" + value.ToString() + " Failed");
                }
            }
            get
            {
                string ss = ReadElementValue(NodeDataSource, "ATSUser");
                return (ss == null ? "" : ss);
            }
        }

        public string ATSPWD    //140918_0
        {
            set
            {
                if (!ModifyElement(NodeDataSource, "ATSPWD", value))
                {
                    MessageBox.Show("Set ATSPWD=" + value.ToString() + " Failed");
                }
            }
            get
            {
                string ss = ReadElementValue(NodeDataSource, "ATSPWD");
                return (ss == null ? "" : ss);
            }
        }

        public bool showBtnLogin
        {
            set
            {
                if (!ModifyElement(NodeDataState, "showBtnLogin", value.ToString()))
                {
                    MessageBox.Show("Set showBtnLogin=" + value.ToString() + " Failed");
                }
            }
            get
            {
                string ss = ReadElementValue(NodeDataState, "showBtnLogin");
                return (ss == null ? false : (ss.ToString().ToUpper() == "TRUE" ? true : false));
            }
        }
        public bool showBtnExport
        {
            set
            {
                if (!ModifyElement(NodeDataState, "showBtnExport", value.ToString()))
                {
                    MessageBox.Show("Set showBtnExport=" + value.ToString() + " Failed");
                }
            }
            get
            {
                string ss = ReadElementValue(NodeDataState, "showBtnExport");
                return (ss == null ? false : (ss.ToString().ToUpper() == "TRUE" ? true : false));
            }
        }
        public bool showBtnChangePwd
        {
            set
            {
                if (!ModifyElement(NodeDataState, "showBtnChangePwd", value.ToString()))
                {
                    MessageBox.Show("Set showBtnChangePwd=" + value.ToString() + " Failed");
                }
            }
            get
            {
                string ss = ReadElementValue(NodeDataState, "showBtnChangePwd");
                return (ss == null ? false : (ss.ToString().ToUpper() == "TRUE" ? true : false));
            }
        }
        #endregion
    }
}
