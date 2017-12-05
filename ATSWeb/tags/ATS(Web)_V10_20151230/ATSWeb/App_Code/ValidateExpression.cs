using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
/// <summary>
///ValidateExpression 的摘要说明
/// </summary>
public class ValidateExpression
{
	public ValidateExpression()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    private string pre = "^(?!(";
    private string end = ")$).+$";
    private string mid = "|";
    private string FloatIntandCommaExpression = "^[+-]?\\d*(\\.\\d*)?(,?[+-]*?\\d+(\\.\\d*)?)+$";
    private string Twohexadecimalcharacters = "[\\da-fA-F]{2}";
    private string BoolExpression = "^0$|^1$|^true$|^false$";
    private string DoubleorScientificNotation = "^[-+]?(\\d{0,308}(\\.\\d{0,308})?|\\.\\d+)([eE]([-+]?([012]?\\d{1,2}|30[0-7])|-3([01]?[4-9]|[012]?[0-3])))?$";
    //浮点数或整数并且以逗号隔开的正则表达式
    public string GSPre
    {
        get
        {
            return pre;
        }
    }
    public string GSEnd
    {
        get
        {
            return end;
        }
    }
    public string GSMid
    {
        get
        {
            return mid;
        }
    }
   
    public string GFloatIntandCommaExpression
    {
        get
        {
          return FloatIntandCommaExpression;
        }
        
    }
    public string GTwohexadecimalcharacters
     {
        get
         {
             return Twohexadecimalcharacters;
         }
     }
    public string GBoolExpression
    {
        get
        {
            return BoolExpression;
        }
    }
    public string GDoubleorScientificNotation
    {
        get
        {
            return DoubleorScientificNotation;
        }
    }
}
public class algorithm
{
    public algorithm()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public byte CRC8(byte[] buffer)
    {
        byte crc = 0;
        for (int j = 0; j < buffer.Length; j++)
        {
            crc ^= buffer[j];
            for (int i = 0; i < 8; i++)
            {
                if ((crc & 0x01) != 0)
                {
                    crc >>= 1;
                    crc ^= 0x8c;
                }
                else
                {
                    crc >>= 1;
                }
            }
        }
        return crc;
    }
    public byte[] strToHexByte(string hexString)
    {
        hexString = hexString.Replace(" ", "");
        if ((hexString.Length % 2) != 0)
            hexString += " ";
        byte[] returnBytes = new byte[hexString.Length / 2];
        for (int i = 0; i < returnBytes.Length; i++)
        {
            if (!Regex.IsMatch(hexString.Substring(i * 2, 2), "^[0-9A-Fa-f]+$"))
            {
                returnBytes[i] = Convert.ToByte("FF",16);
            }
            else
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
           
        }
            
        return returnBytes;
    }
    public string byteToHexStr(byte[] bytes)
    {
        string returnStr = "";
        if (bytes != null)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                returnStr += bytes[i].ToString("X2");
            }
        }
        return returnStr;
    }
    public string byteToHexStr(byte bytes)
    {
        string returnStr = "";
        if (bytes != null)
        {
            returnStr = bytes.ToString("X2");
        }
        return returnStr;
    }
}
public enum MSAPages:byte
{
    SFF8636_A0H_Page0 = 0,
    SFF8636_A0H_Page1 = 1, 
    SFF8636_A0H_Page2 = 2, 
    SFF8636_A0H_Page3 = 3,   
}
public class analysisMSA
{
    public analysisMSA()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public string CurrItemDescription(byte[] objValues, string page, byte Address, byte addrValue, out string itemName, out string addressAllDescription)
    {
        string currItemDescription = "";
        itemName = "";
        addressAllDescription = "";
        byte pageNumber = Convert.ToByte(page);
        if (pageNumber == (byte)(MSAPages.SFF8636_A0H_Page0))
        {
            //objValues[0x80 - 0x80] 请特别注意: 因为A0H_Page0的地址 是 从128开始的
            #region  Upper Page 00h
            if (Address == 0x80)
            {
                #region Byte Address = 128

                itemName = "Identifier";
                addressAllDescription = "Identifier Type of free side device";

                switch (addrValue)
                {
                    case 0x00:
                        currItemDescription = "Unknown or unspecified";
                        break;
                    case 0x01:
                        currItemDescription = "GBIC";
                        break;
                    case 0x02:
                        currItemDescription = "Module/connector soldered to motherboard";
                        break;
                    case 0x03:
                        currItemDescription = "SFP";
                        break;
                    case 0x04:
                        currItemDescription = "300 pin XBI";
                        break;
                    case 0x05:
                        currItemDescription = "XENPAK";
                        break;
                    case 0x06:
                        currItemDescription = "XFP";
                        break;
                    case 0x07:
                        currItemDescription = "XFF";
                        break;
                    case 0x08:
                        currItemDescription = "XFP-E";
                        break;
                    case 0x09:
                        currItemDescription = "XPAK";
                        break;
                    case 0x0A:
                        currItemDescription = "X2";
                        break;
                    case 0x0B:
                        currItemDescription = "DWDM-SFP";
                        break;
                    //QSFP---------------------------------
                    case 0x0C:
                        currItemDescription = "QSFP";
                        break;
                    case 0x0D:
                        currItemDescription = "QSFP+";
                        break;
                    default:
                        currItemDescription = "ERROR";
                        break;
                }
                #endregion
            }
            else if (Address == 0x81)
            {
                #region Byte Address = 129
                //141023_0 以 '与'的方式来做好像有问题! TBD
                itemName = "Ext. Identifier";
                addressAllDescription = "Extended Identifier of free side device. "
                    + "Includes power classes, CLEI codes, CDR capability";

                switch (Convert.ToInt32(Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 2), 2))
                {
                    case 0:
                        currItemDescription = "Power Class 1 (1.5 W max. )";
                        break;
                    case 1:
                        currItemDescription = "Power Class 2 (2.0 W max. )";
                        break;
                    case 2:
                        currItemDescription = "Power Class 3 (2.5 W max. )";
                        break;
                    case 3:
                        currItemDescription = "Power Class 4 (3.5 W max. )";
                        break;
                }

                switch (Convert.ToInt32(Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 2), 2))
                {
                    case 0:
                        currItemDescription += "; unused (legacy setting)";
                        break;
                    case 1:
                        currItemDescription = "Power Class 5 (4.0 W max. )";
                        break;
                    case 2:
                        currItemDescription = "Power Class 6 (4.5 W max. )";
                        break;
                    case 3:
                        currItemDescription = "Power Class 7 (5.0 W max. )";
                        break;
                }

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                {
                    currItemDescription += "; CLEI code present in Page 02h";
                }
                else
                {
                    currItemDescription += "; No CLEI code present in Page 02h";
                }

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                {
                    currItemDescription += "; CDR present in TX";
                }
                else
                {
                    currItemDescription += "; No CDR in TX";
                }

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                {
                    currItemDescription += "; CDR present in RX";
                }
                else
                {
                    currItemDescription += "; No CDR in RX";
                }
                #endregion
            }
            else if (Address == 0x82)
            {
                #region Byte Address = 130
                itemName = "Connector, Media";
                addressAllDescription = "Code for media connector type";

                switch (addrValue)
                {
                    case 0x00:
                        currItemDescription = "Unknown or unspecified";
                        break;
                    case 0x01:
                        currItemDescription = "SC";
                        break;
                    case 0x02:
                        currItemDescription = "FC Style 1 copper connector";
                        break;
                    case 0x03:
                        currItemDescription = "FC Style 2 copper connector";
                        break;
                    case 0x04:
                        currItemDescription = "BNC/TNC";
                        break;
                    case 0x05:
                        currItemDescription = "FC coax headers";
                        break;
                    case 0x06:
                        currItemDescription = "Fiberjack";
                        break;
                    case 0x07:
                        currItemDescription = "LC";
                        break;
                    case 0x08:
                        currItemDescription = "MT-RJ";
                        break;
                    case 0x09:
                        currItemDescription = "MU";
                        break;
                    case 0x0A:
                        currItemDescription = "SG";
                        break;
                    case 0x0B:
                        currItemDescription = "Optical Pigtail";
                        break;
                    case 0x0C:
                        currItemDescription = "MPO";
                        break;
                    case 0x20:
                        currItemDescription = "HSSDC II";
                        break;
                    case 0x21:
                        currItemDescription = "Copper pigtail";
                        break;
                    case 0x22:
                        currItemDescription = "RJ45";
                        break;
                    case 0x23:
                        currItemDescription = "No separable connector";
                        break;
                    default:
                        currItemDescription = "Error";
                        break;
                }
                #endregion
            }
            else if (Address == 0x83)
            {
                #region Byte Address = 131

                itemName = "Specification Compliance";
                addressAllDescription = "Code for electronic or optical compatibility";

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                {
                    currItemDescription = "Extended";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                {
                    currItemDescription = "10GBASE-LRM";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                {
                    currItemDescription = "10GBASE-LR";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                {
                    currItemDescription = "10GBASE-SR";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                {
                    currItemDescription = "40GBASE-CR4";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                {
                    currItemDescription = "40GBASE-SR4";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                {
                    currItemDescription = "40GBASE-LR4";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                {
                    currItemDescription = "40G Active Cable (XLPPI)";
                }
                else
                {
                    currItemDescription = "";
                }
                #endregion
            }

            else if (Address == 0x84)
            {
                #region Byte Address = 132
                itemName = "Specification Compliance";
                addressAllDescription = "Code for electronic or optical compatibility";

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                {
                    currItemDescription = "OC 48, long reach";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                {
                    currItemDescription = "OC 48, intermediate reach";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                {
                    currItemDescription = "OC 48 short reach";
                }
                else
                {
                    currItemDescription = "";
                }
                #endregion
            }

            else if (Address == 0x85)
            {
                #region Byte Address = 133
                itemName = "Specification Compliance";
                addressAllDescription = "Code for electronic or optical compatibility";

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                {
                    currItemDescription = "Reserved SAS";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                {
                    currItemDescription = "SAS 12.0 Gbps";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                {
                    currItemDescription = "SAS 6.0 Gbps";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                {
                    currItemDescription = "SAS 3.0 Gbps";
                }
                else
                {
                    currItemDescription = "";
                }
                #endregion
            }

            else if (Address == 0x86)
            {
                #region Byte Address = 134
                itemName = "Specification Compliance";
                addressAllDescription = "Code for electronic or optical compatibility";

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                {
                    currItemDescription = "1000BASE-T";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                {
                    currItemDescription = "1000BASE-CX";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                {
                    currItemDescription = "1000BASE-LX";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                {
                    currItemDescription = "1000BASE-SX";
                }
                else
                {
                    currItemDescription = "";
                }
                #endregion
            }

            else if (Address == 0x87)
            {
                #region Byte Address = 135
                itemName = "Specification Compliance";
                addressAllDescription = "Code for electronic or optical compatibility";

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                {
                    currItemDescription = "Very long distance (V)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                {
                    currItemDescription = "Short distance (S)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                {
                    currItemDescription = "Intermediate distance (I)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                {
                    currItemDescription = "Long distance (L)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                {
                    currItemDescription = "Medium (M)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                {
                    currItemDescription = "Reserved";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                {
                    currItemDescription = "Longwave laser (LC)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                {
                    currItemDescription = "Electrical inter-enclosure (EL)";
                }
                else
                {
                    currItemDescription = "";
                }
                #endregion
            }

            else if (Address == 0x88)
            {
                #region Byte Address = 136
                itemName = "Specification Compliance";
                addressAllDescription = "Code for electronic or optical compatibility";

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                {
                    currItemDescription = "Electrical intra-enclosure";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                {
                    currItemDescription = "Shortwave laser w/o OFC (SN)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                {
                    currItemDescription = "Shortwave laser w OFC (SL)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                {
                    currItemDescription = "Longwave Laser (LL)";
                }
                else
                {
                    currItemDescription = "";
                }
                #endregion
            }
            else if (Address == 0x89)
            {
                #region Byte Address = 137
                itemName = "Specification Compliance";
                addressAllDescription = "Code for electronic or optical compatibility";

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                {
                    currItemDescription = "Twin Axial Pair (TW)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                {
                    currItemDescription = "Shielded Twisted Pair (TP)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                {
                    currItemDescription = "Miniature Coax (MI)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                {
                    currItemDescription = "Video Coax (TV)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                {
                    currItemDescription = "Multi-mode 62.5 m (M6)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                {
                    currItemDescription = "Multi-mode 50 m (M5)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                {
                    currItemDescription = "Multi-mode 50 um (OM3)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                {
                    currItemDescription = "Single Mode (SM)";
                }
                else
                {
                    currItemDescription = "";
                }
                #endregion
            }

            else if (Address == 0x8a)
            {
                #region Byte Address = 138
                itemName = "Specification Compliance";
                addressAllDescription = "Code for electronic or optical compatibility";

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                {
                    currItemDescription = "1200 MBps (per channel)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                {
                    currItemDescription = "800 MBps";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                {
                    currItemDescription = "1600 MBps (per channel)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                {
                    currItemDescription = "400 MBps";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                {
                    currItemDescription = "3200 MBps (per channel)";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                {
                    currItemDescription = "200 MBps";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                {
                    currItemDescription = "Extended";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                {
                    currItemDescription = "100 MBps";
                }
                else
                {
                    currItemDescription = "";
                }
                #endregion
            }
            else if (Address == 0x8b)
            {
                #region Byte Address = 139
                //00h Unspecified
                //01h 8B10B
                //02h 4B5B
                //03h NRZ
                //04h SONET Scrambled
                //05h 64B66B
                //06h Manchester
                //-FFh Reserved
                itemName = "Encoding";
                addressAllDescription = "Code for serial encoding algorithm.";

                switch (addrValue)
                {
                    case 0x00:
                        currItemDescription = "Unspecified";
                        break;
                    case 0x01:
                        currItemDescription = "8B10B";
                        break;
                    case 0x02:
                        currItemDescription = "4B5B";
                        break;
                    case 0x03:
                        currItemDescription = "NRZ";
                        break;
                    case 0x04:
                        currItemDescription = "SONET Scrambled";
                        break;
                    case 0x05:
                        currItemDescription = "64B66B";
                        break;
                    case 0x06:
                        currItemDescription = "Manchester";
                        break;
                    default:
                        currItemDescription = "Error";
                        break;
                }
                #endregion
            }

            else if (Address == 0x8c)
            {
                #region Byte Address = 140
                itemName = "BR, nominal";
                addressAllDescription = "Nominal bit rate, units of 100 Mbps."
                    + " For BR > 25.4G, set this to FFh and use Byte 222.";

                if (addrValue == 0x00)
                {
                    currItemDescription = "the bit rate is not specified and must be determined from the Module technology";
                }
                else if (addrValue == 0xFF)
                {
                    currItemDescription = "the bit rate exceeds 25.4Gb/s and Byte 222 must be used to determine nominal bit rate";
                }
                else
                {
                    currItemDescription = addrValue.ToString() + "00Mbps";
                }
                #endregion
            }

            else if (Address == 0x8d)
            {
                #region Byte Address = 141
                itemName = "Extended RateSelect Compliance";
                addressAllDescription = "Tags for extended rate select compliance";

                // 141119,TBD
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                {
                    currItemDescription = "QSFP+ Rate Select Version 2.";
                }
                else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                {
                    currItemDescription = "QSFP+ Rate Select Version 1.";
                }
                else
                {
                    currItemDescription = "Not applicable";
                }
                #endregion
            }

            else if (Address == 0x8e)
            {
                #region Byte Address = 142
                itemName = "Length(SMF)";
                addressAllDescription = "Link length supported for SMF fiber in km";

                if (addrValue == 0)
                {
                    currItemDescription = "Not supported";
                }
                else
                {
                    currItemDescription = addrValue.ToString() + "km";
                }
                #endregion
            }

            else if (Address == 0x8f)
            {
                #region Byte Address = 143
                itemName = "Length(OM3 50 um)";
                addressAllDescription = "Link length supported for EBW 50/125 um fiber (OM3), units of 2 m";

                if (addrValue == 0)
                {
                    currItemDescription = "Not supported";
                }
                else
                {
                    currItemDescription = (2 * addrValue).ToString() + "m";
                }
                #endregion
            }

            else if (Address == 0x90)
            {
                #region Byte Address = 144
                itemName = "Length(OM2 50 um)";
                addressAllDescription = "Link length supported for 50/125 um fiber (OM2), units of 1 m";

                if (addrValue == 0)
                {
                    currItemDescription = "Not supported";
                }
                else
                {
                    currItemDescription = addrValue.ToString() + "m";
                }
                #endregion
            }
            else if (Address == 0x91)
            {
                #region Byte Address = 145
                itemName = "Length(OM1 62.5 um)";
                addressAllDescription = "Link length supported for 62.5/125 um fiber (OM1), units of 1 m";

                if (addrValue == 0)
                {
                    currItemDescription = "Not supported";
                }
                else
                {
                    currItemDescription = addrValue.ToString() + "m";
                }
                #endregion
            }

            else if (Address == 0x92)
            {
                #region Byte Address = 146
                itemName = "Length(Passive copper or active cable or OM4 50 um)";
                addressAllDescription = "Link length supported for passive "
                    + "or active cable assembly (units of 1 m) "
                    + "or OM4 50/125um fiber (units of 2 m) as indicated by byte 147.";

                // TBD,141119
                if (addrValue == 0)
                {
                    currItemDescription = "Not supported";
                }
                else if (addrValue == 255)
                {
                    //currItemDescription = "Link Length(cooper) more than 255m";
                    currItemDescription = "the VCSEL free side device supports a link length greater than 508 meters or the Cable assembly has a link length greater than 254 meters.";
                }
                else
                {
                    currItemDescription = "For (OM4):" + (addrValue * 2).ToString() + "m; \n For Cable assembly (copper or AOC)" + (addrValue).ToString() + "m";
                }
                #endregion
            }

            else if (Address == 0x93)
            {
                #region Byte Address = 147
                itemName = "Device tech";
                addressAllDescription = "Device technology";

                // TRANSMITTER TECHNOLOGY (PAGE 00, BYTE 147, BITS 7-4) //有问题141023_0
                // 修正问题141023_0，修改人albert，2014-10-23 10:23:45
                // 思路：
                // 1、将addrValue转化为8位（不足8位需在左边补上0)二进制，提取指定位的二进制数值
                // 2、再将提取的二进制数值转换为十进制与协议标准进行比较
                switch (Convert.ToInt32(Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 4), 2))
                {
                    case 0:
                        currItemDescription = "850 nm VCSEL";
                        break;
                    case 1:
                        currItemDescription = "1310 nm VCSEL";
                        break;
                    case 2:
                        currItemDescription = "1550 nm VCSEL";
                        break;
                    case 3:
                        currItemDescription = "1310 nm FP";
                        break;
                    case 4:
                        currItemDescription = "1310 nm DFB";
                        break;
                    case 5:
                        currItemDescription = "1550 nm DFB";
                        break;
                    case 6:
                        currItemDescription = "1310 nm EML";
                        break;
                    case 7:
                        currItemDescription = "1550 nm EML";
                        break;
                    case 8:
                        currItemDescription = "Others";
                        break;
                    case 9:
                        currItemDescription = "1490 nm DFB";
                        break;
                    case 10:
                        currItemDescription = "Copper cable unequalized";
                        break;
                    case 11:
                        currItemDescription = "Copper cable passive equalized";
                        break;
                    case 12:
                        currItemDescription = "Copper cable, near and far end limiting active equalizers";
                        break;
                    case 13:
                        currItemDescription = "Copper cable, far end limiting active equalizers";
                        break;
                    case 14:
                        currItemDescription = "Copper cable, near end limiting active equalizers";
                        break;
                    case 15:
                        currItemDescription = "Copper cable, linear active equalizers";
                        break;
                }

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                {
                    currItemDescription += "; Active wavelength control";
                }
                else
                {
                    currItemDescription += "; No wavelength control";
                }

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                {
                    currItemDescription += "; Cooled transmitter";
                }
                else
                {
                    currItemDescription += "; Uncooled transmitter device";
                }

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                {
                    currItemDescription += "; APD detector";
                }
                else
                {
                    currItemDescription += "; Pin detector";
                }

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                {
                    currItemDescription += "; Transmitter tuneable";
                }
                else
                {
                    currItemDescription += "; Transmitter not tuneable";
                }
                #endregion
            }

            else if (Address >= 0x94 && Address <= 0xa3)
            {
                #region Byte Address = 148-163
                itemName = "Vendor name";
                addressAllDescription = "Free side device vendor name (ASCII)";
                currItemDescription = ((char)addrValue).ToString();
                #endregion
            }

            else if (Address == 0xa4)
            {
                #region Byte Address = 164
                itemName = "Extended Module";
                addressAllDescription = "Extended Module codes for InfiniBand";

                currItemDescription = "";

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                {
                    currItemDescription += "EDR ";
                }

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                {
                    currItemDescription += "FDR ";
                }

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                {
                    currItemDescription += "QDR ";
                }

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                {
                    currItemDescription += "DDR ";
                }

                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                {
                    currItemDescription += "SDR ";
                }
                #endregion
            }

            // 2014-10-16，165-167缺一个将十进制转换为16进制                
            else if (Address >= 0xa5 && Address <= 0xa7)
            {
                #region Byte Address = 165-167
                itemName = "Vendor OUI";
                addressAllDescription = "Free side device vendor IEEE company ID";
                currItemDescription = addrValue.ToString("X").PadLeft(2, '0');  //141024_0
                #endregion
            }

            else if (Address >= 0xa8 && Address <= 0xb7)
            {
                #region Byte Address = 168-183
                itemName = "Vendor PN";
                addressAllDescription = "Part number provided by free side device vendor(ASCII)";
                currItemDescription = ((char)addrValue).ToString();
                #endregion
            }

            else if (Address >= 0xb8 && Address <= 0xb9)
            {
                #region Byte Address = 184-185
                itemName = "Vendor rev";
                addressAllDescription = "Revision level for part number provided by vendor(ASCII)";
                currItemDescription = ((char)addrValue).ToString();
                #endregion
            }

            // 186-187波长计算，代码未完成
            else if (Address >= 0xba && Address <= 0xbb)
            {
                #region Byte Address = 186 ,187
                itemName = "Wavelength or Copper Cable Attenuation";
                addressAllDescription = "Nominal laser wavelength (wavelength=value/20 in nm) "
                    + "or copper cable attenuation in dB at 2.5 GHz (Adrs 186) and 5.0 GHz (Adrs 187)";
                currItemDescription = ((objValues[186 - 0x80] * 256 + objValues[187 - 0x80]) / 20).ToString() + "nm"; //141023_0
                if (Address == 0xba)
                {
                    currItemDescription +=
                    "; For copper cable: the copper cable attenuation at 2.5 GHz in" + addrValue.ToString() + "dB";
                }
                else if (Address == 0xbb)
                {
                    currItemDescription +=
                    "; For copper cable: the copper cable attenuation at 5 GHz in" + addrValue.ToString() + "dB";
                }
                #endregion
            }
            else if (Address >= 0xbc && Address <= 0xbd)
            {
                #region Byte Address = 188,189
                itemName = "Wavelength tolerance or Copper Cable Attenuation";
                addressAllDescription = "Guaranteed range of laser wavelength(+/- value) "
                    + "from nominal wavelength.(wavelength Tol.=value/200 in nm) or "
                    + "copper cable attenuation in dB at 7.0 GHz (Adrs 188) and 12GHz (Adrs 189)";
                currItemDescription = "Wavelength Tolerance (±" + ((objValues[188 - 0x80] * 256 + objValues[189 - 0x80]) / (float)200) + "nm)";

                if (Address == 0xbc)
                {
                    currItemDescription +=
                    "; For copper cable: the copper cable attenuation at 7.0 GHz in" + addrValue.ToString() + "dB";
                }
                else if (Address == 0xbd)
                {
                    currItemDescription +=
                    "; For copper cable: the copper cable attenuation at 12.9 GHz in" + addrValue.ToString() + "dB";
                }
                #endregion
            }
            else if (Address == 0xbe)
            {
                #region Byte Address = 190
                itemName = "Max case temp.";
                addressAllDescription = "Maximum case temperature in degrees C";
                currItemDescription = "Max Case Temp " + addrValue.ToString() + "°C";
                #endregion
            }
            else if (Address == 0xbf)
            {
                #region Byte Address = 191
                itemName = "CC_BASE";
                addressAllDescription = "Check code for base ID fields (bytes 128-190)";
                currItemDescription = "CS=" + addrValue.ToString("X");
                #endregion
            }
            else if (Address == 0xc0)
            {
                #region Byte Address = 192
                itemName = "Link codes";
                addressAllDescription = "Extended Specification Compliance Codes";
                currItemDescription = "The Extended Specification Compliance Codes";
                #endregion
            }

            else if (Address == 0xc1)
            {
                #region Byte Address = 193
                itemName = "Options";
                addressAllDescription = "Rate Select, TX Disable, TX Fault, LOS, Warning indicators "
                    + "for: Temperature, VCC, RX power, TX Bias, TX EQ, Adaptive TX EQ, "
                    + "RX EMPH, CDR Bypass, CDR LOL Flag.";

                // bit 7-3
                currItemDescription = "";

                // bit 3
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                {
                    currItemDescription += "TX Input Equalization Auto Adaptive Capable implemented";
                }
                else
                {
                    currItemDescription += "TX Input Equalization Auto Adaptive Capable not implemented";
                }

                // bit 2
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                {
                    currItemDescription += ",TX Input Equalization Fixed Programmable Settings implemented";
                }
                else
                {
                    currItemDescription += ",TX Input Equalization Fixed Programmable Settings not implemented";
                }

                // bit 1
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                {
                    currItemDescription += ",RX Output Emphasis Fixed Programmable Settings implemented";
                }
                else
                {
                    currItemDescription += ",RX Output Emphasis Fixed Programmable Settings not implemented";
                }

                // bit 0
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                {
                    currItemDescription += ",RX Output Amplitude Fixed Programmable Settings implemented";
                }
                else
                {
                    currItemDescription += ",RX Output Amplitude Fixed Programmable Settings not implemented";
                }
                #endregion

            }

            else if (Address == 0xc2)
            {
                #region Byte Address = 194
                itemName = "Options";
                addressAllDescription = "Rate Select, TX Disable, TX Fault, LOS, Warning indicators "
                    + "for: Temperature, VCC, RX power, TX Bias, TX EQ, Adaptive TX EQ, "
                    + "RX EMPH, CDR Bypass, CDR LOL Flag.";

                currItemDescription = "";

                // bit 7
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                {
                    currItemDescription += "TX CDR On/Off Control implemented, controllable.";
                }
                else
                {
                    currItemDescription += "TX CDR On/Off Control implemented, fixed.";
                }

                // bit 6
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                {
                    currItemDescription += " RX CDR On/Off Control implemented, controllable.";
                }
                else
                {
                    currItemDescription += " RX CDR On/Off Control implemented, fixed.";
                }

                // bit 5
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                {
                    currItemDescription += " Tx CDR Loss of Lock (LOL) Flag implemented.";
                }
                else
                {
                    currItemDescription += " Tx CDR Loss of Lock (LOL) Flag not implemented.";
                }

                // bit 4
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                {
                    currItemDescription += " Rx CDR Loss of Lock (LOL) Flag implemented.";
                }
                else
                {
                    currItemDescription += " Rx CDR Loss of Lock (LOL) Flag not implemented.";
                }

                // bit 3
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                {
                    currItemDescription += " Rx Squelch Disable implemented.";
                }
                else
                {
                    currItemDescription += " Rx Squelch Disable not implemented.";
                }

                // bit 2
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                {
                    currItemDescription += " Rx Output Disable capable implemented.";
                }
                else
                {
                    currItemDescription += " Rx Output Disable capable not implemented.";
                }

                // bit 1
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                {
                    currItemDescription += " Tx Squelch Disable implemented.";
                }
                else
                {
                    currItemDescription += " Tx Squelch Disable not implemented.";
                }

                // bit 0
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                {
                    currItemDescription += " Tx Squelch implemented.";
                }
                else
                {
                    currItemDescription += " Tx Squelch not implemented.";
                }
                #endregion
            }
            else if (Address == 0xc3)
            {
                #region Byte Address = 195
                itemName = "Options";
                addressAllDescription = "Rate Select, TX Disable, TX Fault, LOS, Warning indicators "
                    + "for: Temperature, VCC, RX power, TX Bias, TX EQ, Adaptive TX EQ, "
                    + "RX EMPH, CDR Bypass, CDR LOL Flag.";

                // bit 7
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                {
                    currItemDescription = "Memory page 02 provided implemented.";
                }
                else
                {
                    currItemDescription = "Memory page 02 provided not implemented.";
                }

                // bit 6
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                {
                    currItemDescription += " Memory Page 01h provided implemented.";
                }
                else
                {
                    currItemDescription += " Memory Page 01h provided not implemented.";
                }

                // bit 5
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                {
                    currItemDescription += " RATE_SELECT is implemented,"
                        + " active control of the select bits in the upper memory table is required to change rates";
                }
                else
                {
                    currItemDescription += " RATE_SELECT is implemented,"
                        + " no control of the rate select bits in the upper memory table is required.";
                }

                // bit 4
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                {
                    currItemDescription += " Tx_DISABLE is implemented and disables the serial output.";
                }
                else
                {
                    currItemDescription += " Tx_DISABLE is not implemented.";
                }

                // bit 3
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                {
                    currItemDescription += " Tx_FAULT signal implemented";
                }
                else
                {
                    currItemDescription += " Tx_FAULT signal not implemented";
                }

                // bit 2
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                {
                    currItemDescription += " Tx Squelch implemented to reduce Pave.";
                }
                else
                {
                    currItemDescription += " Tx Squelch implemented to reduce OMA.";
                }

                // bit 1
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                {
                    currItemDescription += " Tx Loss of Signal implemented";
                }
                else
                {
                    currItemDescription += " Tx Loss of Signal not implemented";
                }

                // bit 0, Reserved
                #endregion
            }

            else if (Address >= 0xc4 && Address <= 0xd3)
            {
                #region Byte Address = 196-211
                itemName = "Vendor SN";
                addressAllDescription = "Serial number provided by vendor (ASCII)";
                currItemDescription = ((char)addrValue).ToString();
                #endregion
            }

            else if (Address >= 0xd4 && Address <= 0xdb)
            {
                #region Byte Address =  212-219
                itemName = "Date Code";
                addressAllDescription = "Vendor's manufacturing date code";
                currItemDescription = ((char)addrValue).ToString();
                #endregion
            }
            else if (Address == 0xdc)
            {
                #region Byte Address = 220
                itemName = "Diagnostic Monitoring Type";
                addressAllDescription = "Indicates which type of diagnostic monitoring is"
                    + " implemented (if any) in the free side device. Bit 7-4,1,0 Reserved.";

                // bit 3
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                {
                    currItemDescription = "Received power measurements type: Average Power, ";
                }
                else
                {
                    currItemDescription = "Received power measurements type: OMA, ";
                }

                // bit 2
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                {
                    currItemDescription += "Transmitter power measurement supported.";
                }
                else
                {
                    currItemDescription += "Transmitter power measurement not supported.";
                }
                #endregion
            }

            else if (Address == 0xdd)
            {
                #region Byte Address = 221
                itemName = "Enhanced Options";
                addressAllDescription = "Indicates which optional enhanced features are implemented in the free side device.";

                // bit 3
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                {
                    currItemDescription = "rate selection is implemented using extended rate selection, ";
                }
                else
                {
                    currItemDescription = "the free side device does not support rate selection, ";
                }

                // bit 2
                if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                {
                    currItemDescription += "the free side device supports rate selection using application select table mechanism.";
                }
                else
                {
                    currItemDescription += "the free side device does not support application select and Page 01h does not exist.";
                }
                #endregion
            }
            else if (Address == 0xde)
            {
                #region Byte Address = 222
                itemName = "BR, nominal";
                addressAllDescription = "Nominal bit rate per channel, units of 250 Mbps. Complements Byte 140.";

                currItemDescription = (250 * addrValue).ToString() + "Mbps";
                #endregion
            }

            else if (Address == 0xdf)
            {
                #region Byte Address = 223
                itemName = "CC_EXT";
                addressAllDescription = "Check code for the Extended ID Fields (bytes 192-222)";
                currItemDescription = "CS2=" + addrValue.ToString("X");
                #endregion
            }

            else if (Address >= 0xe0 && Address <= 0xff)
            {
                #region Byte Address = 224-255
                itemName = "Vendor Specific";
                addressAllDescription = "Vendor Specific EEPROM";
                currItemDescription = (addrValue).ToString("X");
                #endregion
            }

            #endregion
        }
        else if (pageNumber == (byte)(MSAPages.SFF8636_A0H_Page3))
        {
            //objValues[0x80 - 0x80] 请特别注意: 因为A0H_Page3的地址 是 从128开始的
            #region Page 03h (Cable Assemblies)
            float mytemp;
            switch (Address)
            {
                #region Byte Address = 128
                case 128:
                    itemName = "Temp High Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    mytemp = objValues[0x80 - 0x80] + objValues[0x81 - 0x80] / (float)256;
                    if (mytemp > 128)
                        mytemp -= 256;
                    currItemDescription = mytemp.ToString("F4") + " °C";
                    break;
                #endregion

                #region Byte Address = 129
                case 129:
                    itemName = "Temp High Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    mytemp = objValues[0x80 - 0x80] + objValues[0x81 - 0x80] / (float)256;
                    if (mytemp > 128)
                        mytemp -= 256;
                    currItemDescription = mytemp.ToString("F4") + " °C";
                    break;
                #endregion

                #region Byte Address = 130
                case 130:
                    itemName = "Temp Low Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    mytemp = objValues[0x82 - 0x80] + objValues[0x83 - 0x80] / (float)256;
                    if (mytemp > 128)
                        mytemp -= 256;
                    currItemDescription = mytemp.ToString("F4") + " °C";
                    break;
                #endregion

                #region Byte Address = 131
                case 131:
                    itemName = "Temp Low Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    mytemp = objValues[0x82 - 0x80] + objValues[0x83 - 0x80] / (float)256;
                    if (mytemp > 128)
                        mytemp -= 256;
                    currItemDescription = mytemp.ToString("F4") + " °C";
                    break;
                #endregion

                #region Byte Address = 132
                case 132:
                    itemName = "Temp High Warning";
                    addressAllDescription = "MSB at lower byte address";
                    mytemp = objValues[0x84 - 0x80] + objValues[0x85 - 0x80] / (float)256;
                    if (mytemp > 128)
                        mytemp -= 256;
                    currItemDescription = mytemp.ToString("F4") + " °C";
                    break;
                #endregion

                #region Byte Address = 133
                case 133:
                    itemName = "Temp High Warning";
                    addressAllDescription = "MSB at lower byte address";
                    mytemp = objValues[0x84 - 0x80] + objValues[0x85 - 0x80] / (float)256;
                    if (mytemp > 128)
                        mytemp -= 256;
                    currItemDescription = mytemp.ToString("F4") + " °C";
                    break;
                #endregion

                #region Byte Address = 134
                case 134:
                    itemName = "Temp Low Warning";
                    addressAllDescription = "MSB at lower byte address";
                    mytemp = objValues[0x86 - 0x80] + objValues[0x87 - 0x80] / (float)256;
                    if (mytemp > 128)
                        mytemp -= 256;
                    currItemDescription = mytemp.ToString("F4") + " °C";
                    break;
                #endregion

                #region Byte Address = 135
                case 135:
                    itemName = "Temp Low Warning";
                    addressAllDescription = "MSB at lower byte address";
                    mytemp = objValues[0x86 - 0x80] + objValues[0x87 - 0x80] / (float)256;
                    if (mytemp > 128)
                        mytemp -= 256;
                    currItemDescription = mytemp.ToString("F4") + " °C";
                    break;
                #endregion

                #region Byte Address = 136-143
                case 136:
                case 137:
                case 138:
                case 139:
                case 140:
                case 141:
                case 142:
                case 143:
                    itemName = "Reserved";
                    break;
                #endregion

                #region Byte Address = 144
                case 144:
                    itemName = "Vcc High Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[144 - 0x80] * 256 + objValues[145 - 0x80]) * 0.0001).ToString("F4") + " V";
                    break;
                #endregion

                #region Byte Address = 145
                case 145:
                    itemName = "Vcc High Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[144 - 0x80] * 256 + objValues[145 - 0x80]) * 0.0001).ToString("F4") + " V";

                    break;
                #endregion

                #region Byte Address = 146
                case 146:
                    itemName = "Vcc Low Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[146 - 0x80] * 256 + objValues[147 - 0x80]) * 0.0001).ToString("F4") + " V";

                    break;
                #endregion

                #region Byte Address = 147
                case 147:
                    itemName = "Vcc Low Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[146 - 0x80] * 256 + objValues[147 - 0x80]) * 0.0001).ToString("F4") + " V";

                    break;
                #endregion

                #region Byte Address = 148
                case 148:
                    itemName = "Vcc High Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[148 - 0x80] * 256 + objValues[149 - 0x80]) * 0.0001).ToString("F4") + " V";

                    break;
                #endregion

                #region Byte Address = 149
                case 149:
                    itemName = "Vcc High Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[148 - 0x80] * 256 + objValues[149 - 0x80]) * 0.0001).ToString("F4") + " V";

                    break;
                #endregion

                #region Byte Address = 150
                case 150:
                    itemName = "Vcc Low Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[150 - 0x80] * 256 + objValues[151 - 0x80]) * 0.0001).ToString("F4") + " V";

                    break;
                #endregion

                #region Byte Address = 151
                case 151:
                    itemName = "Vcc Low Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[150 - 0x80] * 256 + objValues[151 - 0x80]) * 0.0001).ToString("F4") + " V";

                    break;
                #endregion

                #region Byte Address = 152-159
                case 152:
                case 153:
                case 154:
                case 155:
                case 156:
                case 157:
                case 158:
                case 159:
                    itemName = "Reserved";
                    break;
                #endregion

                #region Byte Address = 160-175
                case 160:
                case 161:
                case 162:
                case 163:
                case 164:
                case 165:
                case 166:
                case 167:
                case 168:
                case 169:
                case 170:
                case 171:
                case 172:
                case 173:
                case 174:
                case 175:
                    itemName = "Vendor Specific";
                    break;
                #endregion

                #region Byte Address = 176
                case 176:
                    itemName = "RX Power High Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[176 - 0x80] * 256 + objValues[177 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 177
                case 177:
                    itemName = "RX Power High Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[176 - 0x80] * 256 + objValues[177 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 178
                case 178:
                    itemName = "RX Power Low Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[178 - 0x80] * 256 + objValues[179 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 179
                case 179:
                    itemName = "RX Power Low Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[178 - 0x80] * 256 + objValues[179 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 180
                case 180:
                    itemName = "RX Power High Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[180 - 0x80] * 256 + objValues[181 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 181
                case 181:
                    itemName = "RX Power High Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[180 - 0x80] * 256 + objValues[181 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 182
                case 182:
                    itemName = "RX Power Low Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[182 - 0x80] * 256 + objValues[183 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 183
                case 183:
                    itemName = "RX Power Low Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[182 - 0x80] * 256 + objValues[183 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 184
                case 184:
                    itemName = "Tx Bias High Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[184 - 0x80] * 256 + objValues[185 - 0x80]) * 0.002).ToString("F2") + " mA";

                    break;
                #endregion

                #region Byte Address = 185
                case 185:
                    itemName = "Tx Bias High Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[184 - 0x80] * 256 + objValues[185 - 0x80]) * 0.002).ToString("F2") + " mA";

                    break;
                #endregion

                #region Byte Address = 186
                case 186:
                    itemName = "Tx Bias Low Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[186 - 0x80] * 256 + objValues[187 - 0x80]) * 0.002).ToString("F2") + " mA";

                    break;
                #endregion

                #region Byte Address = 187
                case 187:
                    itemName = "Tx Bias Low Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[186 - 0x80] * 256 + objValues[187 - 0x80]) * 0.002).ToString("F2") + " mA";

                    break;
                #endregion

                #region Byte Address = 188
                case 188:
                    itemName = "Tx Bias High Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[188 - 0x80] * 256 + objValues[189 - 0x80]) * 0.002).ToString("F2") + " mA";

                    break;
                #endregion

                #region Byte Address = 189
                case 189:
                    itemName = "Tx Bias High Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[188 - 0x80] * 256 + objValues[189 - 0x80]) * 0.002).ToString("F2") + " mA";

                    break;
                #endregion

                #region Byte Address = 190
                case 190:
                    itemName = "Tx Bias Low Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[190 - 0x80] * 256 + objValues[191 - 0x80]) * 0.002).ToString("F2") + " mA";

                    break;
                #endregion

                #region Byte Address = 191
                case 191:
                    itemName = "Tx Bias Low Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = ((objValues[190 - 0x80] * 256 + objValues[191 - 0x80]) * 0.002).ToString("F2") + " mA";

                    break;
                #endregion

                #region Byte Address = 192
                case 192:
                    itemName = "Tx Power High Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[192 - 0x80] * 256 + objValues[193 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 193
                case 193:
                    itemName = "Tx Power High Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[192 - 0x80] * 256 + objValues[193 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 194
                case 194:
                    itemName = "Tx Power Low Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[194 - 0x80] * 256 + objValues[195 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 195
                case 195:
                    itemName = "Tx Power Low Alarm";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[194 - 0x80] * 256 + objValues[195 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 196
                case 196:
                    itemName = "Tx Power High Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[196 - 0x80] * 256 + objValues[197 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 197
                case 197:
                    itemName = "Tx Power High Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[196 - 0x80] * 256 + objValues[197 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 198
                case 198:
                    itemName = "Tx Power Low Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[198 - 0x80] * 256 + objValues[199 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 199
                case 199:
                    itemName = "Tx Power Low Warning";
                    addressAllDescription = "MSB at lower byte address";
                    currItemDescription = (10 * Math.Log10(((objValues[198 - 0x80] * 256 + objValues[199 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                    break;
                #endregion

                #region Byte Address = 200-207
                case 200:
                case 201:
                case 202:
                case 203:
                case 204:
                case 205:
                case 206:
                case 207:
                    itemName = "Reserved";
                    break;
                #endregion

                #region Byte Address = 208-223
                case 208:
                case 209:
                case 210:
                case 211:
                case 212:
                case 213:
                case 214:
                case 215:
                case 216:
                case 217:
                case 218:
                case 219:
                case 220:
                case 221:
                case 222:
                case 223:
                    itemName = "Vendor Specific";
                    break;
                #endregion

                // 224-255, 2014-10-16，代码未完成
                //141017_0  TBD QSFP无需检查???
                #region Byte Address = 224~255
                #region Byte Address = 224
                case 224:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                //141017_0TBD
                #region Byte Address = 225
                case 225:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 226-233
                case 226:
                case 227:
                case 228:
                case 229:
                case 230:
                case 231:
                case 232:
                case 233:
                    itemName = "Vendor Specific";
                    break;
                #endregion

                //141017_0TBD
                #region Byte Address = 234
                case 234:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 235
                case 235:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 236
                case 236:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 237
                case 237:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                //141017_0TBD
                #region Byte Address = 238
                case 238:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 239
                case 239:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 240
                case 240:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 241
                case 241:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 242
                case 242:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 243
                case 243:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 244
                case 244:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 245
                case 245:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 246
                case 246:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 247
                case 247:
                    itemName = "";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 248-249
                case 248:
                case 249:
                    itemName = "Reserved";
                    addressAllDescription = "Reserved channel monitor masks set 4";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 250-251
                case 250:
                case 251:
                    itemName = "Reserved";
                    addressAllDescription = "Reserved channel monitor masks";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 252-253
                case 252:
                case 253:
                    itemName = "Reserved";
                    addressAllDescription = "Reserved channel monitor masks";
                    currItemDescription = "";
                    break;
                #endregion

                #region Byte Address = 254-255
                case 254:
                case 255:
                    itemName = "Reserved";
                    addressAllDescription = "";
                    currItemDescription = "";
                    break;
                #endregion
                #endregion
                default:
                    break;
            }
            #endregion
        }
        else if (pageNumber == (byte)(MSAPages.SFF8636_A0H_Page2))
        {
            //objValues[0x80 - 0x80] 请特别注意: 因为A0H_Page3的地址 是 从128开始的
            #region Page 02h (Cable Assemblies)
            itemName = "UserEEPROM Data";
            addressAllDescription = "UserEEPROM Data";
            #endregion
        }
        else if (pageNumber == (byte)(MSAPages.SFF8636_A0H_Page1))
        {
            //objValues[0x80 - 0x80] 请特别注意: 因为A0H_Page1的地址 是 从128开始的
            #region Page 01h (conditional on the state of bit 2 in byte 221.)

            switch (Address)
            {
                #region Byte Address = 128
                case 128:
                    itemName = "CC_APPS";
                    currItemDescription = "Check code for the AST: thecheck code shall be the low order bits of the sum of the " +
                        "contents of all the bytes from byte 129 to byte 255,inclusive.";
                    addressAllDescription = currItemDescription;
                    break;
                #endregion

                #region Byte Address = 129
                case 129:
                    itemName = "AST Table Length,TL (length - 1)";
                    addressAllDescription = "<Bit:7-6 Reserved> A 6 bit binary number. TL,specifies the offset of the last application table entry" +
                        " defined in bytes 130-255. TL is valid between 0 (1 entry) and 62 (for a total of 63 entries)";
                    if ((objValues[129 - 0x80] & 63) < 63)
                    {
                        currItemDescription = (130 + 2 * (objValues[129 - 0x80] & 63)) + "," + (131 + 2 * (objValues[129 - 0x80] & 63)) + "--> Application code TL";
                    }
                    else
                    {
                        currItemDescription = "Error<overflow>!!!" + "Address = " + (130 + 2 * (objValues[129 - 0x80] & 63))
                            + "," + (131 + 2 * (objValues[129 - 0x80] & 63)) + "; Application code TL Address> 255";
                    }
                    break;
                #endregion

                #region Byte Address = 130,131
                case 130:
                case 131:
                    itemName = "Application Code 0";
                    addressAllDescription = "Definition of first application supported";
                    currItemDescription = "";
                    break;

                #endregion

                default:
                    itemName = "";
                    addressAllDescription = "Bytes 130 to 256 contain the application code table entries";
                    currItemDescription = "";
                    break;
            }

            try
            {
                if (((objValues[129 - 0x80] & 63) != 63) && ((objValues[129 - 0x80] & 63) != 0))
                {
                    byte mytemp1 = Convert.ToByte(130 + 2 * (objValues[129 - 0x80] & 63));
                    byte mytemp2 = Convert.ToByte(131 + 2 * (objValues[129 - 0x80] & 63));

                    if (Address == mytemp1 || Address == mytemp2)
                    {
                        itemName = "Application code TL";
                        addressAllDescription = "130+2*TL 131+2*TL --> Application code TL ";
                        currItemDescription = "Application code TL";
                    }
                }
            }
            catch
            {
            }

            #endregion
        }
        return currItemDescription;
    }
}