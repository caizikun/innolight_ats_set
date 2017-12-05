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
    SFF8472_A0=4,
    SFF8472_A2=5,
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
        else if (pageNumber == (byte)(MSAPages.SFF8472_A0))
        {
            //objValues[0x00 - 0xFF]
            #region A0H 0-255 

            switch (Address)
            {
#region Byte Address = 0
                case 0:
                    {                      

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
                                currItemDescription = "SFP or SFP+";
                                break;
                            default:
                                currItemDescription = "";
                                break;
                        }
                        if (addrValue >= 0x04 && addrValue <= 0x7F)
                        {
                            currItemDescription = "Not used by this specification.These values are maintained in the Transceiver Managementsection of SFF-8024.";

                        }
                        else if (addrValue >= 0x80 && addrValue <= 0xFF)
                        {
                            currItemDescription = "Vendor specific";
                        }

                    }
                    break;
                        #endregion
#region Byte Address = 1
                case 1:
                    itemName = "Ext. Identifier";
                    addressAllDescription = "Extended identifier of type of transceiver";
                    switch (addrValue)
                    {
                        case 0x00:
                            currItemDescription = "GBIC definition is not specified or the GBIC definition is not compliant with a defined MOD_DEF. See product specification for details.";
                            break;
                        case 0x01:
                            currItemDescription = "GBIC is compliant with MOD_DEF 1";
                            break;
                        case 0x02:
                            currItemDescription = "GBIC is compliant with MOD_DEF 2";
                            break;
                        case 0x03:
                            currItemDescription = "GBIC is compliant with MOD_DEF 3";
                            break;
                        case 0x04:
                            currItemDescription = "GBIC/SFP function is defined by two-wire interface ID only";
                            break;
                        case 0x05:
                            currItemDescription = "GBIC is compliant with MOD_DEF 5";
                            break;
                        case 0x06:
                            currItemDescription = "GBIC is compliant with MOD_DEF 6";
                            break;
                        case 0x07:
                            currItemDescription = "GBIC is compliant with MOD_DEF 7";
                            break;
                            default:
                            {
                                currItemDescription = "Unallocated";
                            }
                            break;
                    }
                    break;
                #endregion

#region Byte Address = 2
                case 2:

                    itemName = "Connector";
                    addressAllDescription = "Code for connector type";
                    if (addrValue==7)
                    {
                        currItemDescription = "LC";
                    } 
                    else
                    {
                        currItemDescription = "Error";
                    }
                    
                    break;
               

                #endregion #region Byte Address = 3-10
#region Byte Address 3-10

#region Byte Address=3

                case 3:
                    {
                        itemName = "Transceiver";
                        addressAllDescription = "Code for electronic or optical compatibility";
                        switch (addrValue)
                        {
                            case 0x00:
                                currItemDescription = "1X Copper Passive";
                                break;
                            case 0x01:
                                currItemDescription = "1X Copper Active";
                                break;
                            case 0x02:
                                currItemDescription = "1X LX";
                                break;
                            case 0x03:
                                currItemDescription = "1X SX";
                                break;
                            case 0x04:
                                currItemDescription = "10G Base-SR";
                                break;
                            case 0x05:
                                currItemDescription = "10G Base-LR";
                                break;
                            case 0x06:
                                currItemDescription = "10G Base-LRM";
                                break;
                            case 0x07:
                                currItemDescription = "10G Base-ER";
                                break;
                                default:
                                currItemDescription = "";
                                break;
                        }
                    
                    }
                    
                    break;
                    #endregion
#region Byte Address=4

                case 4:
                    {
                        itemName = "Transceiver";
                        addressAllDescription = "Code for electronic or optical compatibility";
                        switch (addrValue)
                        {
                            case 0x00:
                                currItemDescription = "OC-48, short reach*2";
                                break;
                            case 0x01:
                                currItemDescription = "OC-48, intermediate reach *2";
                                break;
                            case 0x02:
                                currItemDescription = "OC-48, long reach*2";
                                break;
                            case 0x03:
                                currItemDescription = "SONET reach specifier bit 2";
                                break;
                            case 0x04:
                                currItemDescription = "SONET reach specifier bit 1";
                                break;
                            case 0x05:
                                currItemDescription = "OC-192, short reach *2";
                                break;
                            case 0x06:
                                currItemDescription = "ESCON SMF, 1310nm Laser";
                                break;
                            case 0x07:
                                currItemDescription = "ESCON MMF, 1310nm LED";
                                break;
                            default:
                                currItemDescription = "";
                                break;
                        }

                    }

                    break;
                    #endregion
#region Byte Address=5

                case 5:
                    {
                        itemName = "Transceiver";
                        addressAllDescription = "Code for electronic or optical compatibility";
                        switch (addrValue)
                        {
                            case 0x00:
                                currItemDescription = "OC-3, short reach *2";
                                break;
                            case 0x01:
                                currItemDescription = "OC-3, single mode, inter. reach *2";
                                break;
                            case 0x02:
                                currItemDescription = "OC-3, single mode, long reach *2";
                                break;
                            case 0x03:
                                currItemDescription = "Unallocated";
                                break;
                            case 0x04:
                                currItemDescription = "OC-12, short reach *2";
                                break;
                            case 0x05:
                                currItemDescription = "OC-12, single mode, inter. reach *2";
                                break;
                            case 0x06:
                                currItemDescription = "OC-12, single mode, long reach *2";
                                break;
                            case 0x07:
                                currItemDescription = "Unallocated";
                                break;
                            default:
                                currItemDescription = "";
                                break;
                        }

                    }

                    break;
                    #endregion
#region Byte Address=6

                case 6:
                    {
                        itemName = "Transceiver";
                        addressAllDescription = "Code for electronic or optical compatibility";
                        switch (addrValue)
                        {
                            case 0x00:
                                currItemDescription = "1000BASE-SX";
                                break;
                            case 0x01:
                                currItemDescription = "1000BASE-LX *3";
                                break;
                            case 0x02:
                                currItemDescription = "1000BASE-CX";
                                break;
                            case 0x03:
                                currItemDescription = "1000BASE-T";
                                break;
                            case 0x04:
                                currItemDescription = "100BASE-LX/LX10";
                                break;
                            case 0x05:
                                currItemDescription = "100BASE-FX";
                                break;
                            case 0x06:
                                currItemDescription = "BASE-BX10 *3";
                                break;
                            case 0x07:
                                currItemDescription = "BASE-PX *3";
                                break;
                            default:
                                currItemDescription = "";
                                break;
                        }

                    }

                    break;
                    #endregion
#region Byte Address=7

                case 7:
                    {
                        itemName = "Transceiver";
                        addressAllDescription = "Code for electronic or optical compatibility";
                        switch (addrValue)
                        {
                            case 0x00:
                                currItemDescription = "Electrical inter-enclosure (EL)";
                                break;
                            case 0x01:
                                currItemDescription = "Longwave laser (LC) *6";
                                break;
                            case 0x02:
                                currItemDescription = "Shortwave laser, linear Rx (SA) *7";
                                break;
                            case 0x03:
                                currItemDescription = "medium distance (M)";
                                break;
                            case 0x04:
                                currItemDescription = "long distance (L)";
                                break;
                            case 0x05:
                                currItemDescription = "intermediate distance (I)";
                                break;
                            case 0x06:
                                currItemDescription = "short distance (S)";
                                break;
                            case 0x07:
                                currItemDescription = "very long distance (V)";
                                break;
                            default:
                                currItemDescription = "";
                                break;
                        }

                    }

                    break;
               #endregion
#region Byte Address=8

                case 8:
                    {
                        itemName = "Transceiver";
                        addressAllDescription = "Code for electronic or optical compatibility";
                        switch (addrValue)
                        {
                            case 0x00:
                            case 0x01:
                                currItemDescription = "Unallocated";
                                break;                           
                            case 0x02:
                                currItemDescription = "Passive Cable *8";
                                break;
                            case 0x03:
                                currItemDescription = "Active Cable *8";
                                break;
                            case 0x04:
                                currItemDescription = "Longwave laser (LL) *5";
                                break;
                            case 0x05:
                                currItemDescription = "Shortwave laser with OFC (SL) *4";
                                break;
                            case 0x06:
                                currItemDescription = "Shortwave laser w/o OFC (SN) *7";
                                break;
                            case 0x07:
                                currItemDescription = "Electrical intra-enclosure (EL)";
                                break;
                            default:
                                currItemDescription = "";
                                break;
                        }

                    }

                    break;
                    #endregion
#region Byte Address=9

                case 9:
                    {
                        itemName = "Transceiver";
                        addressAllDescription = "Code for electronic or optical compatibility";
                        switch (addrValue)
                        {
                            case 0x00:
                                currItemDescription = "Single Mode (SM)";
                                break;
                            case 0x01:
                                currItemDescription = "Unallocated";
                                break;
                            case 0x02:
                                currItemDescription = "Multimode, 50um (M5, M5E)";
                                break;
                            case 0x03:
                                currItemDescription = "Multimode, 62.5um (M6)";
                                break;
                            case 0x04:
                                currItemDescription = "Video Coax (TV)";
                                break;
                            case 0x05:
                                currItemDescription = "Miniature Coax (MI)";
                                break;
                            case 0x06:
                                currItemDescription = "Twisted Pair (TP)";
                                break;
                            case 0x07:
                                currItemDescription = "Twin Axial Pair (TW)";
                                break;
                            default:
                                currItemDescription = "";
                                break;
                        }

                    }

                    break;
                    #endregion
#region Byte Address=10

                case 10:
                    {
                        itemName = "Transceiver";
                        addressAllDescription = "Code for electronic or optical compatibility";
                        switch (addrValue)
                        {
                            case 0x00:
                                currItemDescription = "100 MBytes/sec";
                                break;
                            case 0x01:
                                currItemDescription = "Unallocated";
                                break;
                            case 0x02:
                                currItemDescription = "200 MBytes/sec";
                                break;
                            case 0x03:
                                currItemDescription = "3200 MBytes/sec";
                                break;
                            case 0x04:
                                currItemDescription = "400 MBytes/sec";
                                break;
                            case 0x05:
                                currItemDescription = "1600 MBytes/sec";
                                break;
                            case 0x06:
                                currItemDescription = "800 MBytes/sec";
                                break;
                            case 0x07:
                                currItemDescription = "1200 MBytes/sec";
                                break;
                            default:
                                currItemDescription = "";
                                break;
                        }

                    }

                    break;
                    #endregion
                #endregion
#region region Byte Address = 11

                case 11:
                    {
                        itemName = "Encoding";
                        addressAllDescription = "Code for high speed serial encoding algorithm";
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
                            case 0x06:
                                currItemDescription = "64B/66B";
                                break;
                           
                            default:
                                currItemDescription = "";
                                break;
                        }

                    }

                    break;
#endregion
#region   Byte Address = 12
                
                case 12:
                    {
                        itemName = "BR, Nominal";
                        addressAllDescription = "Nominal signalling rate, units of 100MBd.";
                        if (addrValue == 0)
                        {
                            currItemDescription = "Error";
                        }
                        else
                        {
                            currItemDescription = Convert.ToString(addrValue * 100) + "Mbps";
                        }

                    }

                    break;
                #endregion
#region   Byte Address = 13
                case 13:
                    {
                        itemName = "Rate Identifier";
                        addressAllDescription = "Type of rate select functionality.";
                        switch (addrValue)
                        {
                            case 0x00:
                                currItemDescription = "Unspecified";
                                break;
                            case 0x01:
                                currItemDescription = "SFF-8079 (4/2/1G Rate_Select & AS0/AS1)";
                                break;
                            case 0x02:
                                currItemDescription = "SFF-8431 (8/4/2G Rx Rate_Select only)";
                                break;
                            case 0x03:
                                currItemDescription = "Unspecified";
                                break;
                            case 0x04:
                                currItemDescription = "SFF-8431 (8/4/2G Tx Rate_Select only)";
                                break;
                            case 0x05:
                                currItemDescription = "Unspecified";
                                break;
                            case 0x06:
                                currItemDescription = "SFF-8431 (8/4/2G Independent Rx & Tx Rate_select)";
                                break;
                            case 0x07:
                                currItemDescription = "Unspecified";
                                break;
                            case 0x08:
                                currItemDescription = "FC-PI-5 (16/8/4G Rx Rate_select only) High=16G only, Low=8G/4G";
                                break;
                            case 0x09:
                                currItemDescription = "Unspecified";
                                break;
                            case 0x0A:
                                currItemDescription = "FC-PI-5 (16/8/4G Independent Rx, Tx Rate_select) High=16G only, Low=8G/4G";
                                break;
                            case 0x0B:
                                currItemDescription = "Unspecified";
                                break;
                            case 0x0C:
                                currItemDescription = "FC-PI-6 (32/16/8G Independent Rx, Tx Rate_Select)High=32G only, Low = 16G/8G";
                                break;
                            case 0x0D:
                                currItemDescription = "Unspecified";
                                break;
                            case 0x0E:
                                currItemDescription = "10/8G Rx and Tx Rate_Select controlling the operation or locking modes of the internal signal"+
"conditioner, retimer or CDR, according to the logic table defined in Table 10-2, High Bit Rate"+
"(10G) =9.95-11.3 Gb/s; Low Bit Rate (8G) = 8.5 Gb/s. In this mode, the default value of bit 110.3"+
"(Soft Rate Select RS(0), Table 9-11) and of bit 118.3 (Soft Rate Select RS(1), Table 10-1) is 1.";
                                break;
                                case 0x0F:
                                currItemDescription = "Unspecified";
                                break;
                          default:
                                currItemDescription = "Unallocated";
                                break;
                       }

                    }

                    break;
                #endregion
#region  Byte Address=14

                case 14:
                    {
                        itemName = "Length(SMF,km)";
                        addressAllDescription = "Link length supported for single mode fiber, units of km";
                        if (addrValue==0)
                        {
                            currItemDescription = "9/125 SM fiber not supported";
                        }
                        else if (addrValue == 255)
                        {
                            currItemDescription = ">254km";

                        }
                        else
                        {
                            currItemDescription = Convert.ToString(addrValue) + "km";

                        }

                    }
                    break;
#endregion
#region Byte Address=15

                case 15:
                    {
                        itemName = "Length (SMF)";
                        addressAllDescription = "Link length supported for single mode fiber, units of 100 m";
                        if (addrValue == 0)
                        {
                            currItemDescription = "9/125 SM fiber not supported";
                        }
                        else if (addrValue == 255)
                        {
                            currItemDescription = ">25.4km";

                        }
                        else
                        {
                            currItemDescription = Convert.ToString(addrValue*100) + "m";

                        }

                    }
                    break;
#endregion
#region  Byte Address=16

                case 16:
                    {
                        itemName = "Length (50um)";
                        addressAllDescription = "Link length supported for 50 um OM2 fiber, units of 10 m";
                        if (addrValue == 0)
                        {
                            currItemDescription = "50/125um MM fiber (OM2) not supported";
                        }
                        else if (addrValue == 255)
                        {
                            currItemDescription = ">2.54km";

                        }
                        else
                        {
                            currItemDescription = Convert.ToString(addrValue * 10) + "m";

                        }

                    }
                    break;
#endregion
#region Byte Address=17

                case 17:
                    {
                        itemName = "Length (62.5um)";
                        addressAllDescription = "Link length supported for 62.5 um OM1 fiber, units of 10 m";
                        if (addrValue == 0)
                        {
                            currItemDescription = "62.5/125um MM fiber (OM1) not supported";
                        }
                        else if (addrValue == 255)
                        {
                            currItemDescription = ">2.54km";

                        }
                        else
                        {
                            currItemDescription = Convert.ToString(addrValue * 10) + "m";

                        }

                    }
                    break;
#endregion
#region ByteAddress=18

                case 18:
                    {
                        itemName = "Length (OM4 or copper cable)";
                        addressAllDescription = "Link length supported for 50um OM4 fiber, units of 10m."+
                            "Alternatively copper or direct attach cable, units of m";
                        if (addrValue == 0)
                        {
                            currItemDescription = "Copper not supported";
                        }
                        
                        else
                        {
                            currItemDescription ="";

                        }

                    }
                    break;
#endregion
#region Byte Address=19

                case 19:
                    {
                        itemName = "Length (OM3)";
                        addressAllDescription = "Link length supported for 50 um OM3 fiber, units of 10 m";
                        if (addrValue == 0)
                        {
                            currItemDescription = "50/125um MM fiber (OM3) not supported";
                        }

                        else if (addrValue == 255)
                        {
                            currItemDescription = ">2.54 Km";

                        }
                        else
                        {
                            currItemDescription = Convert.ToString(addrValue * 10) + "m";
                        }

                    }
                    break;
#endregion
#region Byte Address=20-35

                case 20:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                        
                    }
                    break;
                case 21:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                case 22:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                case 23:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                case 24:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                case 25:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                case 26:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                case 27:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                case 28:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                case 29:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                case 30:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                case 31:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                case 32:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                case 33:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                case 34:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                case 35:
                    {
                        itemName = "Vendor name";
                        addressAllDescription = "SFP vendor name";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                #endregion
#region Byte Address=36

                case 36:
                    {
                        itemName = "Transceiver";
                        addressAllDescription = "Code for electronic or optical compatibility";
                         currItemDescription = "Reserved";
                        

                    }
                    break;
                #endregion
#region Byte Address=37-39

                case 37:
                case 38:
                case 39:
                    {
                        itemName = "Vendor OUI";
                        addressAllDescription = "SFP vendor IEEE company ID";
                        currItemDescription = Convert.ToString(objValues[37] & objValues[38] & objValues[39],2);
                    }
                    break;
                #endregion
#region Byte Address=40-55

                case 40:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                case 41:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                case 42:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                case 43:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                case 44:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                case 45:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                case 46:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                case 47:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                case 48:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                case 49:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                case 50:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                case 51:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                case 52:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                case 53:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                case 54:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                case 55:
                    {
                        itemName = "Vendor PN";
                        addressAllDescription = "Part number provided by SFP vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;
                #endregion
#region Byte Address=56-59

                case 56:
                    {
                        itemName = "Vendor rev";
                        addressAllDescription = "Revision level for part number provided by vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;

                case 57:
                    {
                        itemName = "Vendor rev";
                        addressAllDescription = "Revision level for part number provided by vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;

                case 58:
                    {
                        itemName = "Vendor rev";
                        addressAllDescription = "Revision level for part number provided by vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;

                case 59:
                    {
                        itemName = "Vendor rev";
                        addressAllDescription = "Revision level for part number provided by vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();
                    }
                    break;




                #endregion
#region Byte Address=60-61

                case 60:
                case 61:
                    {
                        itemName = "Wavelength";
                        addressAllDescription = "Laser wavelength";
                        currItemDescription = Convert.ToString(objValues[60] * 256 + objValues[61]) + "nm";
                    }
                    break;

                #endregion
#region Byte Address=62

                case 62:
                    {
                        itemName = "Unallocated";
                        addressAllDescription = "";
                        currItemDescription = "Reserved";
                    }
                    break;

                #endregion
#region Byte Address=63

                case 63:
                    {
                        itemName = "CC_BASE";
                        addressAllDescription = "Check code for Base ID Fields (addresses 0 to 62)";
                        UInt32 sum = 0;
                        for (int i = 0; i < 63;i++ )
                        {
                            sum += objValues[i];
                        }
                        
                        if (sum%256==addrValue)
                        {
                            currItemDescription = "Verified ok";
                        } 
                        else
                        {
                            currItemDescription = "Error";
                        }
                        
                        
                    }
                    break;

                #endregion
#region Byte Address=64

                case 64:
                    {
                        itemName = "Options";
                        addressAllDescription = "Indicates which optional transceiver signals are implemented";

                       
                       int bit5 = (addrValue & 32) == 32 ? 1 : 0;
                       int bit4 = (addrValue & 16) == 16 ? 1 : 0;
                       int bit3 = (addrValue & 8 )== 8 ? 1 : 0;
                       int bit2 = (addrValue & 4) == 4 ? 1 : 0;
                       int bit1 = (addrValue & 2) == 2 ? 1 : 0;
                       int bit0 =( addrValue &1)  == 1 ? 1 : 0;
                       if (bit5==1)
                        {
                            currItemDescription = "Power Level 3 requirement";

                        } 
                        else
                        {
                            if (bit1==1)
                           {
                               currItemDescription = "Power Level 2 requirement";
                           } 
                           else
                           {
                               currItemDescription = "Power Level 1 (or unspecified) requirements";
                           }
                            
                        }
                        if (bit4==1)
                        {
                            currItemDescription =currItemDescription+ "paging is implemented and byte 127d of device address A2h is used for page selection";
                        }
                        if (bit3==1)
                        {
                            currItemDescription = currItemDescription + "the transceiver has an internal retimer or Clock and Data Recovery (CDR) circuit";
                        }
                        if (bit2 == 1)
                        {
                            currItemDescription = currItemDescription + "a cooled laser transmitter implementation";
                        }
                        else
                        {

                            currItemDescription = currItemDescription + "a conventional uncooled (or unspecified) laser implementation";
                        }
                        if (bit0 ==1)
                        {
                            currItemDescription = currItemDescription + "a linear receiver output";
                        }
                        else
                        {

                            currItemDescription = currItemDescription + "a conventional limiting (or unspecified) receiver output";
                        }
                    }
                    break;

                #endregion
#region Byte Address=65

                case 65:
                    {
                        itemName = "Options";
                        addressAllDescription = "Indicates which optional transceiver signals are implemented";
                        int bit7 = (addrValue & 64) == 128 ? 1 : 0;
                        int bit6= (addrValue & 128) == 64 ? 1 : 0;
                        int bit5 = (addrValue & 32) == 32 ? 1 : 0;
                        int bit4 = (addrValue & 16) == 16 ? 1 : 0;
                        int bit3 = (addrValue & 8) == 8 ? 1 : 0;
                        int bit2 = (addrValue & 4) == 4 ? 1 : 0;
                        int bit1 = (addrValue & 2) == 2 ? 1 : 0;
                        int bit0 = (addrValue & 1) == 1 ? 1 : 0;
                        if (bit7==1)
                        {
                            currItemDescription = "Receiver decision threshold implemented";
                        }
                        if (bit6 == 1)
                        {
                            currItemDescription =currItemDescription+ "the transmitter wavelength/frequency is" + "tunable in accordance with SFF-8690";

                        }
                        if (bit5 == 1)
                        {
                            currItemDescription = currItemDescription + "RATE_SELECT functionality is implemented";

                        }
                        if (bit4 == 1)
                        {
                            currItemDescription = currItemDescription + "TX_DISABLE is implemented and disables the high speed serial output";

                        }
                        if (bit3 == 1)
                        {
                            currItemDescription = currItemDescription + "TX_FAULT signal implemented";

                        }
                        if (bit2 == 1)
                        {
                            currItemDescription = currItemDescription + "Loss of Signal implemented, signal inverted from standard definition in SFP MSA";

                        }
                        if (bit1 == 1)
                        {
                            currItemDescription = currItemDescription + "Loss of Signal implemented, signal as defined in SFP MSA";

                        }
                    }
                    break;

                #endregion
#region Byte Address=66

                case 66:
                    {
                        itemName = "BR, max";
                        addressAllDescription = "Upper bit rate margin, units of %";
                        currItemDescription = Convert.ToString(addrValue / 100) + "%";
                    }
                    break;

                #endregion
#region Byte Address=67

                case 67:
                    {
                        itemName = "BR, min";
                        addressAllDescription = "Lower bit rate margin, units of %";
                        currItemDescription = Convert.ToString(addrValue / 100) + "%";
                    }
                    break;

                #endregion
#region Byte Address=68-83

                case 68:
                case 69:
                case 70:
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80:
                case 81:
                case 82:
                case 83:                
                    {
                        itemName = "Vendor SN";
                        addressAllDescription = "Serial number provided by vendor";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(addrValue)).ToString();

                    }
                    break;
                #endregion
#region Byte Address=84-91
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90:
                case 91:
                    {
                        itemName = "Date code";
                        addressAllDescription = "Vendor's manufacturing date code";
                        currItemDescription = Convert.ToChar(Convert.ToInt64(objValues[84])).ToString() + Convert.ToChar(Convert.ToInt64(objValues[85])).ToString() + "/" +
                         Convert.ToChar(Convert.ToInt64(objValues[86])).ToString() + Convert.ToChar(Convert.ToInt64(objValues[87])).ToString() + "/" +
                         Convert.ToChar(Convert.ToInt64(objValues[88])).ToString() + Convert.ToChar(Convert.ToInt64(objValues[89])).ToString() + "/" +
                         Convert.ToChar(Convert.ToInt64(objValues[90])).ToString() + Convert.ToChar(Convert.ToInt64(objValues[91])).ToString();

                    }
                    break;
                #endregion
#region Byte Address=92
                case 92:
                    {
                        itemName = "DiagnosticMonitoring Type";
                        addressAllDescription = "Indicates which type of diagnostic monitoring is implemented";
                        
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1)=="1")
                        {
                            currItemDescription = "Diagnostic implemented:";
                        }
                        else
                        {
                            currItemDescription = "Diagnostic not implemented";

                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Externally calibrated";

                         }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Internally calibrated";

                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Received power measurement type average power";

                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Received power measurement type OMA";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "I2C addressing change required";

                        }
                    }
                    break;
 #endregion
#region Byte Address=93
                case 93:
                    {
                        itemName = "Enhanced Options";
                        addressAllDescription = "Indicates which optional enhanced features are implemented";

                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                        {
                            currItemDescription = "Optional Alarm/warning flags implemented";
                        }
                        
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Optional soft TX_DISABLE control and monitoring implemented";

                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Optional soft TX_FAULT monitoring implemented";

                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Optional soft RX_LOS monitoring implemented";

                        }
                        
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Optional soft RATE_SELECT control and monitoring implemented";

                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Optional Application Select control implemented per";

                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Optional soft Rate Select control implemented";

                        }
                    }
                    break;
                #endregion
#region Byte Address=94
                case 94:
                    {
                        itemName = "SFF-8472 Compliance";
                        addressAllDescription = "Indicates which revision of SFF-8472 the transceiver complies with.";
                        switch (addrValue)
                        {
                            case 0x00:
                                currItemDescription = "Digital diagnostic functionality not included or undefined.";
                                break;
                            case 0x01:
                                currItemDescription = "Includes functionality described in Rev 9.3 of SFF-8472.";
                                break;
                            case 0x02:
                                currItemDescription = "Includes functionality described in Rev 9.5 of SFF-8472.";
                                break;
                            case 0x03:
                                currItemDescription = "Includes functionality described in Rev 10.2 of SFF-8472.";
                                break;
                            case 0x04:
                                currItemDescription = "Includes functionality described in Rev 10.4 of SFF-8472.";
                                break;
                            case 0x05:
                                currItemDescription = "Includes functionality described in Rev 11.0 of SFF-8472.";
                                break;
                            case 0x06:
                                currItemDescription = "Includes functionality described in Rev 11.3 of SFF-8472.";
                                break;
                            case 0x07:
                                currItemDescription = "Includes functionality described in Rev 11.4 of SFF-8472.";
                                break;
                            case 0x08:
                                currItemDescription = "Includes functionality described in Rev 12.0 of SFF-8472.";
                                break;
                            default:
                                currItemDescription = "Unallocated";
                                break;
                        }
                    }
                    break;
                #endregion
#region Byte Address=95
                case 95:
                    {
                        itemName = "CC_EXT";
                        addressAllDescription = "Check code for the Extended ID Fields (addresses 64 to 94)";
                        UInt32 sum = 0;
                        for (int i = 64; i < 95; i++)
                        {
                            sum += objValues[i];
                        }

                        if (sum % 256 == addrValue)
                        {
                            currItemDescription = "Verified ok";
                        }
                        else
                        {
                            currItemDescription = "Error";
                        }
                    }
                    break;
                #endregion
#region default

                    default:
                    {
                        if (Address >= 96 & Address<=127)
                      {
                          #region Byte Address = 96-127
                itemName = "Vendor Specific";
                addressAllDescription = "Vendor Specific EEPROM";
                currItemDescription = (addrValue).ToString("X");
                #endregion
                      }
                        else if (Address >= 128 & Address <= 255)
                      {
                          #region Byte Address = 128-255
                          itemName = "Reserved";
                          addressAllDescription = "Reserved for SFF-8079";
                          currItemDescription = (addrValue).ToString("X");
                          #endregion
                      }
                        else
                        {
                            itemName = "";
                            addressAllDescription = "";
                            currItemDescription = "";
                        }
                    }
                    break;
#endregion

            }

           

            #endregion
        }
        else if (pageNumber == (byte)(MSAPages.SFF8472_A2))
        {
            //objValues[0x00 - 0xFF] 
            #region A2H 0-255

            switch (Address)
            {
 #region Byte Address = 0-1
                case 0:              
                    {

                        itemName = "Temp High Alarm";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (addrValue > Convert.ToByte(127))
                        {
                            tempaw = ((addrValue * 256 + objValues[1]) - 65536) / 256.0;

                        }
                        else
                        {
                            tempaw = (addrValue * 256 + objValues[1]) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";
                        

                    }
                    break;
                case 1:
                    {

                        itemName = "Temp High Alarm";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (objValues[0] > Convert.ToByte(127))
                        {
                            tempaw = ((objValues[0] * 256 + addrValue) - 65536) / 256.0;

                        }
                        else
                        {
                            tempaw = (objValues[0] * 256 + addrValue) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";


                    }
                    break;
                #endregion
 #region Byte Address = 2-3
                case 2:
               
                    {
                        itemName = "Temp Low Alarm";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (addrValue > Convert.ToByte(127))
                        {
                            tempaw = ((addrValue * 256 + objValues[3]) - 65536) / 256.0;

                        }
                        else
                        {
                            tempaw = (addrValue * 256 + objValues[3]) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";
                    }
                    break;
                case 3:
                    {
                        itemName = "Temp Low Alarm";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (objValues[2] > Convert.ToByte(127))
                        {
                            tempaw = ((objValues[2] * 256 + addrValue) - 65536) / 256.0;
                           
                        }
                        else
                        {
                            tempaw = (objValues[2] * 256 +addrValue) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";
                    }
                    break;
                #endregion

 #region Byte Address = 4-5
                case 4:                
                    {
                        itemName = "Temp High Warning";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (addrValue > Convert.ToByte(127))
                        {
                            tempaw = ((addrValue * 256 + objValues[5]) - 65536) / 256.0;

                        }
                        else
                        {
                            tempaw = (addrValue * 256 + objValues[5]) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";
                    }
                    
                   
                    break;
                     case 5:
                    {
                        itemName = "Temp High Warning";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (objValues[4] > Convert.ToByte(127))
                        {
                            tempaw = ((objValues[4] * 256 +addrValue) - 65536) / 256.0;

                        }
                        else
                        {
                            tempaw = (objValues[4] * 256 +addrValue) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";
                    }
                    
                   
                    break;

                #endregion 
#region Byte Address=6-7

                case 6:
               
                    {
                        itemName = "Temp Low Warning";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (addrValue> Convert.ToByte(127))
                        {
                            tempaw = ((addrValue * 256 + objValues[7]) - 65536) / 256.0;

                        }
                        else
                        {
                            tempaw = (addrValue * 256 + objValues[7]) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";
                    }
                    break;
                     case 7:
                    {
                        itemName = "Temp Low Warning";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (objValues[6] > Convert.ToByte(127))
                        {
                            tempaw = ((objValues[6] * 256 + addrValue) - 65536) / 256.0;

                        }
                        else
                        {
                            tempaw = (objValues[6] * 256 +addrValue) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";
                    }
                    break;
                #endregion
#region Byte Address=8-9

                case 8:
              
                    {
                        itemName = "Voltage High Alarm";
                        addressAllDescription = "MSB at low address";
                        double vccaw;
                        vccaw = (addrValue * 256 + objValues[9]) * 0.0001;
                        vccaw = Math.Round(vccaw, 4);
                        currItemDescription = Convert.ToString(vccaw) + "V";
                    }
                    break;
                        case 9:
                    {
                        itemName = "Voltage High Alarm";
                        addressAllDescription = "MSB at low address";
                        double vccaw;
                        vccaw = (objValues[8] * 256 + addrValue) * 0.0001;
                        vccaw = Math.Round(vccaw, 4);
                        currItemDescription = Convert.ToString(vccaw) + "V";
                    }
                    break;
                #endregion
#region Byte Address=10-11
                case 10:               
                    {
                        itemName = "Voltage Low Alarm";
                        addressAllDescription = "MSB at low address";
                        double vccaw;
                        vccaw = (addrValue * 256 + objValues[11]) * 0.0001;
                        vccaw = Math.Round(vccaw, 4);
                        currItemDescription = Convert.ToString(vccaw) + "V";
                    }
                    break;
                      case 11:
                    {
                        itemName = "Voltage Low Alarm";
                        addressAllDescription = "MSB at low address";
                        double vccaw;
                        vccaw = (objValues[10] * 256 +addrValue) * 0.0001;
                        vccaw = Math.Round(vccaw, 4);
                        currItemDescription = Convert.ToString(vccaw) + "V";
                    }
                    break;
                #endregion
#region Byte Address=12-13
                case 12:
                
                    {
                        itemName = "Voltage High Warning";
                        addressAllDescription = "MSB at low address";
                        double vccaw;
                        vccaw = (addrValue * 256 + objValues[13]) * 0.0001;
                        vccaw = Math.Round(vccaw, 4);
                        currItemDescription = Convert.ToString(vccaw) + "V";
                    }
                    break;
                     case 13:
                    {
                        itemName = "Voltage High Warning";
                        addressAllDescription = "MSB at low address";
                        double vccaw;
                        vccaw = (objValues[12] * 256 + addrValue) * 0.0001;
                        vccaw = Math.Round(vccaw, 4);
                        currItemDescription = Convert.ToString(vccaw) + "V";
                    }
                    break;
                #endregion
#region Byte Address=14-15
                case 14:
               
                    {
                        itemName = "Voltage Low Warning";
                        addressAllDescription = "MSB at low address";
                        double vccaw;
                        vccaw = (addrValue * 256 + objValues[15]) * 0.0001;
                        vccaw = Math.Round(vccaw, 4);
                        currItemDescription = Convert.ToString(vccaw) + "V";
                    }
                    break;
                        case 15:
                    {
                        itemName = "Voltage Low Warning";
                        addressAllDescription = "MSB at low address";
                        double vccaw;
                        vccaw = (objValues[14] * 256 +addrValue) * 0.0001;
                        vccaw = Math.Round(vccaw, 4);
                        currItemDescription = Convert.ToString(vccaw) + "V";
                    }
                    break;
                #endregion
#region Byte Address=16-17
                case 16:
               
                    {
                        itemName = "Bias High Alarm";
                        addressAllDescription = "MSB at low address";
                        double biasaw;
                        biasaw = (addrValue * 256 + objValues[17]) * 0.002;
                        biasaw = Math.Round(biasaw, 4);
                        currItemDescription = Convert.ToString(biasaw) + "mA";
                    }
                    break;
                     case 17:
                    {
                        itemName = "Bias High Alarm";
                        addressAllDescription = "MSB at low address";
                        double biasaw;
                        biasaw = (objValues[16] * 256 + addrValue) * 0.002;
                        biasaw = Math.Round(biasaw, 4);
                        currItemDescription = Convert.ToString(biasaw) + "mA";
                    }
                    break;
                #endregion
#region Byte Address=18-19
                case 18:
                
                    {
                        itemName = "Bias Low Alarm";
                        addressAllDescription = "MSB at low address";
                        double biasaw;
                        biasaw = (addrValue * 256 + objValues[19]) * 0.002;
                        biasaw = Math.Round(biasaw, 4);
                        currItemDescription = Convert.ToString(biasaw) + "mA";
                    }
                    break;
                     case 19:
                    {
                        itemName = "Bias Low Alarm";
                        addressAllDescription = "MSB at low address";
                        double biasaw;
                        biasaw = (objValues[18] * 256 + addrValue) * 0.002;
                        biasaw = Math.Round(biasaw, 4);
                        currItemDescription = Convert.ToString(biasaw) + "mA";
                    }
                    break;
                #endregion
#region Byte Address=20-21
                case 20:
               
                    {
                        itemName = "Bias High Warning";
                        addressAllDescription = "MSB at low address";
                        double biasaw;
                        biasaw = (addrValue * 256 + objValues[21]) * 0.002;
                        biasaw = Math.Round(biasaw, 4);
                        currItemDescription = Convert.ToString(biasaw) + "mA";
                    }
                    break;
                        case 21:
                    {
                        itemName = "Bias High Warning";
                        addressAllDescription = "MSB at low address";
                        double biasaw;
                        biasaw = (objValues[20] * 256 + addrValue) * 0.002;
                        biasaw = Math.Round(biasaw, 4);
                        currItemDescription = Convert.ToString(biasaw) + "mA";
                    }
                    break;
                #endregion
#region Byte Address=22-23
                case 22:
             
                    {
                        itemName = "Bias Low Warning";
                        addressAllDescription = "MSB at low address";
                        double biasaw;
                        biasaw = (addrValue * 256 + objValues[23]) * 0.002;
                        biasaw = Math.Round(biasaw, 4);
                        currItemDescription = Convert.ToString(biasaw) + "mA";
                    }
                    break;
                     case 23:
                    {
                        itemName = "Bias Low Warning";
                        addressAllDescription = "MSB at low address";
                        double biasaw;
                        biasaw = (objValues[22] * 256 + addrValue) * 0.002;
                        biasaw = Math.Round(biasaw, 4);
                        currItemDescription = Convert.ToString(biasaw) + "mA";
                    }
                    break;
                #endregion
#region Byte Address=24-25
                case 24:               
                    {
                        itemName = "TX Power High Alarm";
                        addressAllDescription = "MSB at low address";
                        double txpaw, txpdbm;
                        txpaw = (addrValue * 256 + objValues[25]) * 0.1;
                        txpaw = Math.Round(txpaw, 4);
                        txpdbm = 10 * (Math.Log10((addrValue * 256 + objValues[25]) * (1E-4)));
                        txpdbm = Math.Round(txpdbm, 4);
                        currItemDescription = Convert.ToString(txpaw) + "uW" + "(" + Convert.ToString(txpdbm) + "dbm" + ")";
                    }
                    break;
                        case 25:
                    {
                        itemName = "TX Power High Alarm";
                        addressAllDescription = "MSB at low address";
                        double txpaw, txpdbm;
                        txpaw = (objValues[24] * 256 + addrValue) * 0.1;
                        txpaw = Math.Round(txpaw, 4);
                        txpdbm = 10 * (Math.Log10((objValues[24] * 256 + addrValue) * (1E-4)));
                        txpdbm = Math.Round(txpdbm, 4);
                        currItemDescription = Convert.ToString(txpaw) + "uW" + "(" + Convert.ToString(txpdbm) + "dbm" + ")";
                    }
                    break;
                #endregion
#region Byte Address=26-27
                case 26:              
                    {
                        itemName = "TX Power Low Alarm";
                        addressAllDescription = "MSB at low address";
                        double txpaw,txpdbm;
                        txpaw = (addrValue * 256 + objValues[27]) * 0.1;                      
                        txpaw = Math.Round(txpaw, 4);
                       txpdbm= 10 * (Math.Log10((addrValue * 256 + objValues[27]) * (1E-4)));
                       txpdbm = Math.Round(txpdbm, 4);
                       currItemDescription = Convert.ToString(txpaw) + "uW" + "(" + Convert.ToString(txpdbm)+"dbm"+")";
                    }
                    break;
                     case 27:
                    {
                        itemName = "TX Power Low Alarm";
                        addressAllDescription = "MSB at low address";
                        double txpaw,txpdbm;
                        txpaw = (objValues[26] * 256 + addrValue) * 0.1;                      
                        txpaw = Math.Round(txpaw, 4);
                       txpdbm= 10 * (Math.Log10((objValues[26] * 256 +addrValue) * (1E-4)));
                       txpdbm = Math.Round(txpdbm, 4);
                       currItemDescription = Convert.ToString(txpaw) + "uW" + "(" + Convert.ToString(txpdbm)+"dbm"+")";
                    }
                    break;
                #endregion
#region Byte Address=28-29
                case 28:
               
                    {
                        itemName = "TX Power High Warning";
                        addressAllDescription = "MSB at low address";
                        double txpaw, txpdbm;
                        txpaw = (addrValue * 256 + objValues[29]) * 0.1;
                        txpaw = Math.Round(txpaw, 4);
                        txpdbm = 10 * (Math.Log10((addrValue * 256 + objValues[29]) * (1E-4)));
                        txpdbm = Math.Round(txpdbm, 4);
                        currItemDescription = Convert.ToString(txpaw) + "uW" + "(" + Convert.ToString(txpdbm) + "dbm" + ")";
                    }
                    break;
                      case 29:
                    {
                        itemName = "TX Power High Warning";
                        addressAllDescription = "MSB at low address";
                        double txpaw, txpdbm;
                        txpaw = (objValues[28] * 256 + addrValue) * 0.1;
                        txpaw = Math.Round(txpaw, 4);
                        txpdbm = 10 * (Math.Log10((objValues[28] * 256 + addrValue) * (1E-4)));
                        txpdbm = Math.Round(txpdbm, 4);
                        currItemDescription = Convert.ToString(txpaw) + "uW" + "(" + Convert.ToString(txpdbm) + "dbm" + ")";
                    }
                    break;
                #endregion
#region Byte Address=30-31
                case 30:                
                    {
                        itemName = "TX Power Low Warning";
                        addressAllDescription = "MSB at low address";
                        double txpaw, txpdbm;
                        txpaw = (addrValue * 256 + objValues[31]) * 0.1;
                        txpaw = Math.Round(txpaw, 4);
                        txpdbm = 10 * (Math.Log10((addrValue * 256 + objValues[31]) * (1E-4)));
                        txpdbm = Math.Round(txpdbm, 4);
                        currItemDescription = Convert.ToString(txpaw) + "uW" + "(" + Convert.ToString(txpdbm) + "dbm" + ")";
                    }
                    break;
              case 31:
                    {
                        itemName = "TX Power Low Warning";
                        addressAllDescription = "MSB at low address";
                        double txpaw, txpdbm;
                        txpaw = (objValues[30] * 256 +addrValue) * 0.1;
                        txpaw = Math.Round(txpaw, 4);
                        txpdbm = 10 * (Math.Log10((objValues[30] * 256 + addrValue) * (1E-4)));
                        txpdbm = Math.Round(txpdbm, 4);
                        currItemDescription = Convert.ToString(txpaw) + "uW" + "(" + Convert.ToString(txpdbm) + "dbm" + ")";
                    }
                    break;
                #endregion
#region Byte Address=32-33
                case 32:
               
                    {
                        itemName = "RX Power High Alarm";
                        addressAllDescription = "MSB at low address";
                        double rxpaw, rxpdbm;
                        rxpaw = (addrValue * 256 + objValues[33]) * 0.1;
                        rxpaw = Math.Round(rxpaw, 4);
                        rxpdbm = 10 * (Math.Log10((addrValue * 256 + objValues[33]) * (1E-4)));
                        rxpdbm = Math.Round(rxpdbm, 4);
                        currItemDescription = Convert.ToString(rxpaw) + "uW" + "(" + Convert.ToString(rxpdbm) + "dbm" + ")";
                    }
                    break;
                       case 33:
                    {
                        itemName = "RX Power High Alarm";
                        addressAllDescription = "MSB at low address";
                        double rxpaw, rxpdbm;
                        rxpaw = (objValues[32] * 256 + addrValue) * 0.1;
                        rxpaw = Math.Round(rxpaw, 4);
                        rxpdbm = 10 * (Math.Log10((objValues[32] * 256 +addrValue) * (1E-4)));
                        rxpdbm = Math.Round(rxpdbm, 4);
                        currItemDescription = Convert.ToString(rxpaw) + "uW" + "(" + Convert.ToString(rxpdbm) + "dbm" + ")";
                    }
                    break;
                #endregion
#region Byte Address=34-35
                case 34:
                
                    {
                        itemName = "RX Power Low Alarm";
                        addressAllDescription = "MSB at low address";
                        double rxpaw, rxpdbm;
                        rxpaw = (addrValue * 256 + objValues[35]) * 0.1;
                        rxpaw = Math.Round(rxpaw, 4);
                        rxpdbm = 10 * (Math.Log10((addrValue * 256 + objValues[35]) * (1E-4)));
                        rxpdbm = Math.Round(rxpdbm, 4);
                        currItemDescription = Convert.ToString(rxpaw) + "uW" + "(" + Convert.ToString(rxpdbm) + "dbm" + ")";
                    }
                    break;
                     case 35:
                    {
                        itemName = "RX Power Low Alarm";
                        addressAllDescription = "MSB at low address";
                        double rxpaw, rxpdbm;
                        rxpaw = (objValues[34] * 256 + addrValue) * 0.1;
                        rxpaw = Math.Round(rxpaw, 4);
                        rxpdbm = 10 * (Math.Log10((objValues[34] * 256 + addrValue) * (1E-4)));
                        rxpdbm = Math.Round(rxpdbm, 4);
                        currItemDescription = Convert.ToString(rxpaw) + "uW" + "(" + Convert.ToString(rxpdbm) + "dbm" + ")";
                    }
                    break;
                #endregion
#region Byte Address=36-37
                case 36:
              
                    {
                        itemName = "RX Power High Warning";
                        addressAllDescription = "MSB at low address";
                        double rxpaw, rxpdbm;
                        rxpaw = (addrValue * 256 + objValues[37]) * 0.1;
                        rxpaw = Math.Round(rxpaw, 4);
                        rxpdbm = 10 * (Math.Log10((addrValue * 256 + objValues[37]) * (1E-4)));
                        rxpdbm = Math.Round(rxpdbm, 4);
                        currItemDescription = Convert.ToString(rxpaw) + "uW" + "(" + Convert.ToString(rxpdbm) + "dbm" + ")";
                    }
                    break;
                     case 37:
                    {
                        itemName = "RX Power High Warning";
                        addressAllDescription = "MSB at low address";
                        double rxpaw, rxpdbm;
                        rxpaw = (objValues[36] * 256 + addrValue) * 0.1;
                        rxpaw = Math.Round(rxpaw, 4);
                        rxpdbm = 10 * (Math.Log10((objValues[36] * 256 + addrValue) * (1E-4)));
                        rxpdbm = Math.Round(rxpdbm, 4);
                        currItemDescription = Convert.ToString(rxpaw) + "uW" + "(" + Convert.ToString(rxpdbm) + "dbm" + ")";
                    }
                    break;
                #endregion
#region Byte Address=38-39
                case 38:
               
                    {
                        itemName = "RX Power Low Warning";
                        addressAllDescription = "MSB at low address";
                        double rxpaw, rxpdbm;
                        rxpaw = (addrValue * 256 + objValues[39]) * 0.1;
                        rxpaw = Math.Round(rxpaw, 4);
                        rxpdbm = 10 * (Math.Log10((addrValue * 256 + objValues[39]) * (1E-4)));
                        rxpdbm = Math.Round(rxpdbm, 4);
                        currItemDescription = Convert.ToString(rxpaw) + "uW" + "(" + Convert.ToString(rxpdbm) + "dbm" + ")";
                    }
                    break;
                       case 39:
                    {
                        itemName = "RX Power Low Warning";
                        addressAllDescription = "MSB at low address";
                        double rxpaw, rxpdbm;
                        rxpaw = (objValues[38] * 256 + addrValue) * 0.1;
                        rxpaw = Math.Round(rxpaw, 4);
                        rxpdbm = 10 * (Math.Log10((objValues[38] * 256 + addrValue) * (1E-4)));
                        rxpdbm = Math.Round(rxpdbm, 4);
                        currItemDescription = Convert.ToString(rxpaw) + "uW" + "(" + Convert.ToString(rxpdbm) + "dbm" + ")";
                    }
                    break;
                #endregion
#region Byte Address = 40-41
                case 40:
                    {

                        itemName = "Optional Laser Temp High Alarm";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (addrValue > Convert.ToByte(127))
                        {
                            tempaw = ((addrValue * 256 + objValues[41]) - 65536) / 256.0;

                        }
                        else
                        {
                            tempaw = (addrValue * 256 + objValues[41]) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";


                    }
                    break;
                    case 41:
                    {

                        itemName = "Optional Laser Temp High Alarm";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (objValues[40] > Convert.ToByte(127))
                        {
                            tempaw = ((objValues[40] * 256 + addrValue) - 65536) / 256.0;

                        }
                        else
                        {
                            tempaw = (objValues[40] * 256 + addrValue) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";


                    }
                    break;
                #endregion
#region Byte Address = 42-43
                case 42:
               
                    {

                        itemName = "Optional Laser Temp Low Alarm";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (addrValue > Convert.ToByte(127))
                        {
                            tempaw = ((addrValue * 256 + objValues[43]) - 65536) / 256.0;

                        }
                        else
                        {
                            tempaw = (addrValue * 256 + objValues[43]) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";


                    }
                    break;
                    case 43:
                    {

                        itemName = "Optional Laser Temp Low Alarm";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (objValues[42] > Convert.ToByte(127))
                        {
                            tempaw = ((objValues[42] * 256 + addrValue) - 65536) / 256.0;

                        }
                        else
                        {
                            tempaw = (objValues[42] * 256 + addrValue) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";


                    }
                    break;
                #endregion
#region Byte Address = 44-45
                case 44:
                
                    {

                        itemName = "Optional Laser Temp High Warning";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (addrValue > Convert.ToByte(127))
                        {
                            tempaw = ((addrValue * 256 + objValues[45]) - 65536) / 256.0;

                        }
                        else
                        {
                            tempaw = (addrValue * 256 + objValues[45]) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";


                    }
                    break;
                       case 45:
                    {

                        itemName = "Optional Laser Temp High Warning";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (objValues[44] > Convert.ToByte(127))
                        {
                            tempaw = ((objValues[44] * 256 + addrValue) - 65536) / 256.0;

                        }
                        else
                        {
                            tempaw = (objValues[44] * 256 + addrValue) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";


                    }
                    break;
                #endregion
#region Byte Address = 46-47
                case 46:
               
                    {

                        itemName = "Optional Laser Temp Low Warning";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (addrValue > Convert.ToByte(127))
                        {
                            tempaw = ((addrValue * 256 + objValues[47]) - 65536) / 256.0;

                        }
                        else
                        {
                            tempaw = (addrValue * 256 + objValues[47]) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";


                    }
                    break;
                       case 47:
                    {

                        itemName = "Optional Laser Temp Low Warning";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (objValues[46] > Convert.ToByte(127))
                        {
                            tempaw = ((objValues[46] * 256 +addrValue) - 65536) / 256.0;

                        }
                        else
                        {
                            tempaw = (objValues[46] * 256 +addrValue) / 256.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "℃";


                    }
                    break;
                #endregion             
#region Byte Address = 48-49
                case 48:
                
                    {

                        itemName = "Optional TEC Current High Alarm";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (addrValue > Convert.ToByte(127))
                        {
                            tempaw = ((addrValue * 256 + objValues[49]) - 65536) / 10.0;

                        }
                        else
                        {
                            tempaw = (addrValue * 256 + objValues[49]) / 10.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "mA";


                    }
                    break;
                      case 49:
                    {

                        itemName = "Optional TEC Current High Alarm";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (objValues[48] > Convert.ToByte(127))
                        {
                            tempaw = ((objValues[48] * 256 +addrValue) - 65536) / 10.0;

                        }
                        else
                        {
                            tempaw = (objValues[48] * 256 +addrValue) / 10.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "mA";


                    }
                    break;
                #endregion             
#region Byte Address = 50-51
                case 50:
               
                    {

                        itemName = "Optional TEC Current Low Alarm";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (addrValue > Convert.ToByte(127))
                        {
                            tempaw = ((addrValue * 256 + objValues[51]) - 65536) / 10.0;

                        }
                        else
                        {
                            tempaw = (addrValue * 256 + objValues[51]) / 10.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "mA";


                    }
                    break;
                     case 51:
                    {

                        itemName = "Optional TEC Current Low Alarm";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (objValues[50] > Convert.ToByte(127))
                        {
                            tempaw = ((objValues[50] * 256 + addrValue) - 65536) / 10.0;

                        }
                        else
                        {
                            tempaw = (objValues[50] * 256 + addrValue) / 10.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "mA";


                    }
                    break;
                #endregion             
#region Byte Address = 52-53
                case 52:
                
                    {

                        itemName = "Optional TEC Current High Warning";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (addrValue > Convert.ToByte(127))
                        {
                            tempaw = ((addrValue * 256 + objValues[53]) - 65536) / 10.0;

                        }
                        else
                        {
                            tempaw = (addrValue * 256 + objValues[53]) / 10.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "mA";


                    }
                    break;
                     case 53:
                    {

                        itemName = "Optional TEC Current High Warning";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (objValues[52] > Convert.ToByte(127))
                        {
                            tempaw = ((objValues[52] * 256 + addrValue) - 65536) / 10.0;

                        }
                        else
                        {
                            tempaw = (objValues[52] * 256 + addrValue) / 10.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "mA";


                    }
                    break;
                #endregion                           
#region Byte Address = 54-55
                case 54:
               
                    {

                        itemName = "Optional TEC Current Low Warning";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (addrValue > Convert.ToByte(127))
                        {
                            tempaw = ((addrValue * 256 + objValues[55]) - 65536) / 10.0;

                        }
                        else
                        {
                            tempaw = (addrValue * 256 + objValues[55]) / 10.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "mA";


                    }
                    break;
                       case 55:
                    {

                        itemName = "Optional TEC Current Low Warning";
                        addressAllDescription = "MSB at low address";
                        double tempaw;
                        if (objValues[54] > Convert.ToByte(127))
                        {
                            tempaw = ((objValues[54] * 256 + addrValue) - 65536) / 10.0;

                        }
                        else
                        {
                            tempaw = (objValues[54] * 256 + addrValue) / 10.0;

                        }

                        tempaw = Math.Round(tempaw, 4);
                        currItemDescription = Convert.ToString(tempaw) + "mA";


                    }
                    break;
                #endregion             
#region Byte Address = 56-59
                case 56:
               
                    {

                        itemName = "Ext Cal Constants Rx_PWR(4)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                        bcoef[0]=addrValue;
                        for (int i=1;i<bcoef.Length;i++)
                        {
                            bcoef[i]=objValues[56+i];
                        }
                     float rxpE=  BitConverter.ToSingle(bcoef, 0);
                     currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                      case 57:
                    {

                        itemName = "Ext Cal Constants Rx_PWR(4)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                        for (int i=0;i<bcoef.Length;i++)
                        {
                            bcoef[i]=objValues[56+i];
                        }
                        bcoef[1]=addrValue;
                     float rxpE=  BitConverter.ToSingle(bcoef, 0);
                     currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                        case 58:
                    {

                        itemName = "Ext Cal Constants Rx_PWR(4)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                        for (int i=0;i<bcoef.Length;i++)
                        {
                            bcoef[i]=objValues[56+i];
                        }
                        bcoef[2]=addrValue;
                     float rxpE=  BitConverter.ToSingle(bcoef, 0);
                     currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                   case 59:
                    {

                        itemName = "Ext Cal Constants Rx_PWR(4)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                        for (int i=0;i<bcoef.Length-1;i++)
                        {
                            bcoef[i]=objValues[56+i];
                        }
                        bcoef[3]=addrValue;
                     float rxpE=  BitConverter.ToSingle(bcoef, 0);
                     currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                #endregion             
 #region Byte Address = 60-63
                case 60:
               
                    {

                        itemName = "Ext Cal Constants Rx_PWR(3)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                        bcoef[0]=addrValue; 
                        for (int i = 1; i < bcoef.Length; i++)
                        {
                            bcoef[i] = objValues[60 + i];
                        }
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                        case 61:
                    {

                        itemName = "Ext Cal Constants Rx_PWR(3)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                       
                        for (int i = 0; i < bcoef.Length; i++)
                        {
                            bcoef[i] = objValues[60 + i];
                        }
                         bcoef[1]=addrValue; 
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                 case 62:
                    {

                        itemName = "Ext Cal Constants Rx_PWR(3)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                       
                        for (int i = 0; i < bcoef.Length; i++)
                        {
                            bcoef[i] = objValues[60 + i];
                        }
                         bcoef[2]=addrValue; 
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
               case 63:
                    {

                        itemName = "Ext Cal Constants Rx_PWR(3)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                       
                        for (int i = 0; i < bcoef.Length-1; i++)
                        {
                            bcoef[i] = objValues[60 + i];
                        }
                         bcoef[3]=addrValue; 
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                #endregion
 #region Byte Address = 64-67
                case 64:
               
                    {

                        itemName = "Ext Cal Constants Rx_PWR(2)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                         bcoef[0]=addrValue;
                        for (int i = 1; i < bcoef.Length; i++)
                        {
                            bcoef[i] = objValues[64 + i];
                        }
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
               case 65:
                    {
                        itemName = "Ext Cal Constants Rx_PWR(2)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                      
                        for (int i =0; i < bcoef.Length; i++)
                        {
                            bcoef[i] = objValues[64 + i];
                        }
                           bcoef[1]=addrValue;
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                 case 66:
                    {
                        itemName = "Ext Cal Constants Rx_PWR(2)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                      
                        for (int i =0; i < bcoef.Length; i++)
                        {
                            bcoef[i] = objValues[64 + i];
                        }
                           bcoef[2]=addrValue;
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                      case 67:
                    {
                        itemName = "Ext Cal Constants Rx_PWR(2)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                      
                        for (int i =0; i < bcoef.Length-1; i++)
                        {
                            bcoef[i] = objValues[64 + i];
                        }
                           bcoef[3]=addrValue;
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                #endregion
 #region Byte Address = 68-71
                case 68:
              
                    {

                        itemName = "Ext Cal Constants Rx_PWR(1)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                         bcoef[0] =addrValue;
                        for (int i = 1; i < bcoef.Length; i++)
                        {
                            bcoef[i] = objValues[68 + i];
                        }
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                     case 69:
               
                    {

                        itemName = "Ext Cal Constants Rx_PWR(1)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                        for (int i = 0; i < bcoef.Length; i++)
                        {
                            bcoef[i] = objValues[68 + i];
                        }
                         bcoef[1]=addrValue;
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
             case 70:
                    {

                        itemName = "Ext Cal Constants Rx_PWR(1)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                        for (int i = 0; i < bcoef.Length; i++)
                        {
                            bcoef[i] = objValues[68 + i];
                        }
                         bcoef[2]=addrValue;
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                      case 71:
                    {

                        itemName = "Ext Cal Constants Rx_PWR(1)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                        for (int i = 0; i < bcoef.Length-1; i++)
                        {
                            bcoef[i] = objValues[68 + i];
                        }
                         bcoef[3]=addrValue;
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                #endregion
 #region Byte Address = 72-75
                case 72:
               
                    {

                        itemName = "Ext Cal Constants Rx_PWR(0)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                         bcoef[0]=addrValue;
                        for (int i = 1; i < bcoef.Length; i++)
                        {
                            bcoef[i] = objValues[72 + i];
                        }
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                case 73:
              
                    {

                        itemName = "Ext Cal Constants Rx_PWR(0)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                        for (int i = 0; i < bcoef.Length; i++)
                        {
                            bcoef[i] = objValues[72 + i];
                        }
                         bcoef[1] =addrValue;
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                      case 74:
              
                    {

                        itemName = "Ext Cal Constants Rx_PWR(0)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                        for (int i = 0; i < bcoef.Length; i++)
                        {
                            bcoef[i] = objValues[72 + i];
                        }
                         bcoef[2] =addrValue;
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                 case 75:
              
                    {
                        itemName = "Ext Cal Constants Rx_PWR(0)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[4];
                        for (int i = 0; i < bcoef.Length-1; i++)
                        {
                            bcoef[i] = objValues[72 + i];
                        }
                         bcoef[3] =addrValue;
                        float rxpE = BitConverter.ToSingle(bcoef, 0);
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                #endregion
 #region Byte Address = 76-77
                case 76:
              
                    {

                        itemName = "Ext Cal Constants Tx_I(Slope))";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                       
                        int rxpE = addrValue * 256 + objValues[77];
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                      case 77:
                    {

                        itemName = "Ext Cal Constants Tx_I(Slope))";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                       
                        int rxpE =objValues[76] * 256 +addrValue;
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                #endregion
 #region Byte Address = 78-79
                case 78:
              
                    {

                        itemName = "Ext Cal Constants Tx_I(Offset)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        
                        int rxpE = addrValue * 256 + objValues[79];
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                     case 79:
                    {

                        itemName = "Ext Cal Constants Tx_I(Offset)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        byte[] bcoef = new byte[2];
                        
                        int rxpE = objValues[78] * 256 +addrValue;
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                #endregion
 #region Byte Address = 80-81
                case 80:
               
                    {
                        itemName = "Ext Cal Constants Tx_PWR(Slope)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                      
                        int rxpE =addrValue * 256 +objValues[81];
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                       case 81:
                    {

                        itemName = "Ext Cal Constants Tx_PWR(Slope)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                       
                        int rxpE = objValues[80] * 256 + addrValue;
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                #endregion
#region Byte Address = 82-83
                case 82:
             
                    {

                        itemName = "Ext Cal Constants Tx_PWR(Offset)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                      
                        int rxpE = addrValue * 256 + objValues[83];
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                     case 83:
                    {

                        itemName = "Ext Cal Constants Tx_PWR(Offset)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        
                        int rxpE = objValues[82] * 256 + addrValue;
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                #endregion
 #region Byte Address = 84-85
                case 84:
                    {

                        itemName = "Ext Cal Constants T (Slope)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                       
                        int rxpE = addrValue * 256 + objValues[85];
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                     case 85:
                    {

                        itemName = "Ext Cal Constants T (Slope)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                       
                        int rxpE = objValues[84] * 256 + addrValue;
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                #endregion
#region Byte Address = 86-87
                case 86:
              
                    {

                        itemName = "Ext Cal Constants T (Offset)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                       
                        int rxpE = addrValue* 256 + objValues[87];
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                     case 87:
                    {

                        itemName = "Ext Cal Constants T (Offset)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                                             
                        int rxpE = objValues[86] * 256 + addrValue;
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                #endregion
#region Byte Address = 88-89
                case 88:
              
                    {

                        itemName = "Ext Cal Constants V (Slope)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                       
                        int rxpE = addrValue * 256 + objValues[89];
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                      case 89:
                    {

                        itemName = "Ext Cal Constants V (Slope)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                       
                        int rxpE = objValues[88] * 256 + addrValue;
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                #endregion
#region Byte Address = 90-91
                case 90:
                
                    {

                        itemName = "Ext Cal Constants V (Offset)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                       
                        int rxpE = addrValue * 256 + objValues[91];
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                        case 91:
                    {

                        itemName = "Ext Cal Constants V (Offset)";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                       
                        int rxpE = objValues[90] * 256 + addrValue;
                        currItemDescription = Convert.ToString(rxpE);
                    }
                    break;
                #endregion             
#region Byte Address = 92-94
                case 92:
                case 93:
                case 94:
                    {

                        itemName = "Unallocated";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        
                        currItemDescription = Convert.ToString(addrValue);
                    }
                    break;
                #endregion             
#region Byte Address = 95
                case 95:
               
                    {

                        itemName = "Checksum";
                        addressAllDescription = "Diagnostic calibration constants for optional External Calibration";
                        UInt64 sum = 0;
                        for (int i = 0; i < 94; i++)
                        {
                            sum += objValues[i];
                        }

                        if (sum % 256 == addrValue)
                        {
                            currItemDescription = "Verified ok";
                        }
                        else
                        {
                            currItemDescription = "Error";
                        }
                       
                    }
                    break;
                #endregion             
#region Byte Address = 96-97
                case 96:
              
                    {

                        itemName = "DiagnosticsTemperatureA/D";
                        addressAllDescription = "Diagnostic Monitor Data";
                        UInt16 adc;
                        adc = (UInt16)((addrValue) * 256 + objValues[97]);


                        currItemDescription = Convert.ToString(adc);


                    }
                    break;
                     case 97:
                    {

                        itemName = "DiagnosticsTemperatureA/D";
                        addressAllDescription = "Diagnostic Monitor Data";
                        UInt16 adc;
                        adc = (UInt16)((objValues[96]) * 256 + addrValue);


                        currItemDescription = Convert.ToString(adc);


                    }
                    break;
                #endregion
#region Byte Address = 98-99
                case 98:
               
                    {

                        itemName = "DiagnosticsVccA/D";
                        addressAllDescription = "Diagnostic Monitor Data";
                        UInt16 adc;
                        adc = (UInt16)((addrValue) * 256 + objValues[99]);


                        currItemDescription = Convert.ToString(adc);


                    }
                    break;
                          case 99:
                    {

                        itemName = "DiagnosticsVccA/D";
                        addressAllDescription = "Diagnostic Monitor Data";
                        UInt16 adc;
                        adc = (UInt16)((objValues[98]) * 256 + addrValue);


                        currItemDescription = Convert.ToString(adc);


                    }
                    break;
                #endregion
#region Byte Address = 100-101
                case 100:
               
                    {

                        itemName = "DiagnosticsTX BiasA/D";
                        addressAllDescription = "Diagnostic Monitor Data";
                        UInt16 adc;
                        adc = (UInt16)((addrValue) * 256 + objValues[101]);


                        currItemDescription = Convert.ToString(adc);


                    }
                    break;
                      case 101:
                    {

                        itemName = "DiagnosticsTX BiasA/D";
                        addressAllDescription = "Diagnostic Monitor Data";
                        UInt16 adc;
                        adc = (UInt16)((objValues[100]) * 256 +addrValue);


                        currItemDescription = Convert.ToString(adc);


                    }
                    break;
                #endregion
#region Byte Address = 102-103
                case 102:
               
                    {

                        itemName = "DiagnosticsTX PowerA/D";
                        addressAllDescription = "Diagnostic Monitor Data";
                        UInt16 adc;
                        adc = (UInt16)((addrValue) * 256 + objValues[103]);


                        currItemDescription = Convert.ToString(adc);


                    }
                    break;
                    case 103:
                    {

                        itemName = "DiagnosticsTX PowerA/D";
                        addressAllDescription = "Diagnostic Monitor Data";
                        UInt16 adc;
                        adc = (UInt16)((objValues[102]) * 256 + addrValue);


                        currItemDescription = Convert.ToString(adc);


                    }
                    break;
                #endregion
#region Byte Address = 104-105
                case 104:
               
                    {

                        itemName = "DiagnosticsRX PowerA/D";
                        addressAllDescription = "Diagnostic Monitor Data";
                        UInt16 adc;
                        adc = (UInt16)((addrValue) * 256 + objValues[105]);


                        currItemDescription = Convert.ToString(adc);


                    }
                    break;
                     case 105:
                    {

                        itemName = "DiagnosticsRX PowerA/D";
                        addressAllDescription = "Diagnostic Monitor Data";
                        UInt16 adc;
                        adc = (UInt16)((objValues[104]) * 256 + addrValue);


                        currItemDescription = Convert.ToString(adc);


                    }
                    break;
                #endregion
#region Byte Address = 106-107
                case 106:
               
                    {

                        itemName = "DiagnosticsOptionalLaserTemp/Wavelength A/D";
                        addressAllDescription = "Diagnostic Monitor Data";
                        UInt16 adc;
                        adc = (UInt16)((addrValue) * 256 + objValues[107]);


                        currItemDescription = Convert.ToString(adc);


                    }
                    break;
                      case 107:
                    {

                        itemName = "DiagnosticsOptionalLaserTemp/Wavelength A/D";
                        addressAllDescription = "Diagnostic Monitor Data";
                        UInt16 adc;
                        adc = (UInt16)((objValues[106]) * 256 + addrValue);


                        currItemDescription = Convert.ToString(adc);


                    }
                    break;
                #endregion
#region Byte Address = 108-109
                case 108:
               
                    {

                        itemName = "DiagnosticsOptional TEC current A/D";
                        addressAllDescription = "Diagnostic Monitor Data";
                        UInt16 adc;
                        adc = (UInt16)((addrValue) * 256 + objValues[109]);


                        currItemDescription = Convert.ToString(adc);


                    }
                    break;
                       case 109:
                    {

                        itemName = "DiagnosticsOptional TEC current A/D";
                        addressAllDescription = "Diagnostic Monitor Data";
                        UInt16 adc;
                        adc = (UInt16)((objValues[108]) * 256 + addrValue);


                        currItemDescription = Convert.ToString(adc);


                    }
                    break;
                #endregion
#region Byte Address = 110
                case 110:
               
                    {

                        itemName = "Status/Control";
                        addressAllDescription = "Optional Status and Control Bits";
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                        {
                            currItemDescription = " TX Disable Input Pin=1";
                        }
                        else
                        {
                            currItemDescription = " TX Disable Input Pin=0";
                        }

                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Soft TX Disable select=1";

                        }
                        else
                        {

                            currItemDescription = currItemDescription + "Soft TX Disable select=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Digital state of SFP input pin=1";

                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Digital state of SFP input pin=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "SFP Rate_Select Input Pin=1";

                        }
                        else
                        {
                            currItemDescription = currItemDescription + "SFP Rate_Select Input Pin=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Software Rate Select Control=1";

                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Software Rate Select Control=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "TX Fault Output Pin=1";

                        }
                        else
                        {
                            currItemDescription = currItemDescription + "TX Fault Output Pin=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "RX_LOS Output Pin=1";

                        }
                        else
                        {
                            currItemDescription = currItemDescription + "RX_LOS Output Pin=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Data_Ready_Bar State=1";

                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Data_Ready_Bar State=0";
                        }
                    }
                    break;
                #endregion
#region Byte Address = 111
                case 111:
                    {

                        itemName = "Reserved";
                        addressAllDescription = "Reserved";
                        currItemDescription = "Reserved for SFF-8079";
                        
                    }
                    break;
                #endregion
#region Byte Address = 112
                case 112:
                
                    {

                        itemName = "Alarm Flags";
                        addressAllDescription = "Diagnostic Alarm Flag Status Bits";
                     
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                        {
                            currItemDescription = " Temp High Alarm=1";
                        }
                        else
                        {
                            currItemDescription = " Temp High Alarm=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                        {
                            currItemDescription = currItemDescription + " Temp Low Alarm=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + " Temp Low Alarm=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                        {
                            currItemDescription = currItemDescription + " Vcc High Alarm=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + " Vcc High Alarm=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                        {
                            currItemDescription = currItemDescription + " Vcc Low Alarm=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + " Vcc Low Alarm=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                        {
                            currItemDescription = currItemDescription + " TX Bias High Alarm=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + " TX Bias High Alarm=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                        {
                            currItemDescription = currItemDescription + " TX Bias Low Alarm=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + " TX Bias Low Alarm=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                        {
                            currItemDescription = currItemDescription + " TX Power High Alarm=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + " TX Power High Alarm=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                        {
                            currItemDescription = currItemDescription + " TX Power Low Alarm=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + " TX Power Low Alarm=0";
                        }
                    }
                    break;
                #endregion
#region Byte Address = 113
                case 113:
                    {

                        itemName = "Alarm Flags";
                        addressAllDescription = "Diagnostic Alarm Flag Status Bits";

                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                        {
                            currItemDescription = " RX Power High Alarm=1";
                        }
                        else
                        {
                            currItemDescription = " RX Power High Alarm=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                        {
                            currItemDescription = currItemDescription + " RX Power Low Alarm=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + " RX Power Low Alarm=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                        {
                            currItemDescription = currItemDescription + " Optional Laser TempHigh Alarm=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + " Optional Laser TempHigh Alarm=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Optional Laser Temp Low Alarm=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Optional Laser Temp Low Alarm=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Optional TEC current High Alarm=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Optional TEC current High Alarm=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Optional TEC current Low Alarm=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Optional TEC current Low Alarm=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1" || Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "0")
                        {
                            currItemDescription = currItemDescription + "Reserved Alarm";
                        }

                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1" || Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "0")
                        {
                            currItemDescription = currItemDescription + "Reserved Alarm";
                        }
                        
                        
                    }
                    break;
                #endregion
#region Byte Address = 114
                case 114:
                    {

                        itemName = "Unallocated";
                        addressAllDescription = "Unallocated";

                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 4).Contains("1") && Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 4).Contains("1")==false)
                        {
                            currItemDescription = "Tx input equalization control RATE=HIGH";
                        }

                        else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 4).Contains("1") && Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 4).Contains("1") == false)
                        {
                            currItemDescription = "Tx input equalization control RATE=LOW";
                        }
                        else
                        {
                            currItemDescription = "Unallocated";
                        }                  
                       
                       
                       
                    }
                    break;
                #endregion
#region Byte Address = 115
                case 115:
                    {

                        itemName = "CDR Unlocked";
                        addressAllDescription = "Optional flags indicating that Tx or Rx CDR is unlocked";

                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 4).Contains("1") && Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 4).Contains("1") == false)
                        {
                            currItemDescription = "RX output emphasis control RATE=HIGH";
                        }

                        else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 4).Contains("1") && Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 4).Contains("1") == false)
                        {
                            currItemDescription = "RX output emphasis control RATE=LOW";
                        }
                        else
                        {
                            currItemDescription = "Unallocated";
                        }



                    }
                    break;
                #endregion
#region Byte Address = 116
                case 116:
                    {

                        itemName = "Warning Flags";
                        addressAllDescription = "Diagnostic Warning Flag Status Bits";

                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                        {
                            currItemDescription = " Temp High Warning=1";
                        }
                        else
                        {
                            currItemDescription = " Temp High Warning=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Temp Low Warning=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Temp Low Warning=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Vcc High Warning=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Vcc High Warning=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Vcc Low Warning=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Vcc Low Warning=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "TX Bias High Warning=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "TX Bias High Warning=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "TX Bias Low Warning=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "TX Bias Low Warning=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "TX Power High Warning=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "TX Power High Warning=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "TX Power Low Warning=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "TX Power Low Warning=0";
                        }

                    }
                    break;
                #endregion
#region Byte Address = 117
                case 117:
                    {

                        itemName = "Warning Flags";
                        addressAllDescription = "Diagnostic Warning Flag Status Bits";

                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                        {
                            currItemDescription = "RX Power High Warning=1";
                        }
                        else
                        {
                            currItemDescription = "RX Power High Warning=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "RX Power Low Warning=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "RX Power Low Warning=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Optional Laser Temp High Warning=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Optional Laser Temp High Warning=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Optional Laser Temp Low Warning=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Optional Laser Temp Low Warning=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Optional TEC current High Warning=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Optional TEC current High Warning=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Optional TEC current Low Warning=1";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Optional TEC current Low Warning=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1" || Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "0")
                        {
                            currItemDescription = currItemDescription + "Reserved Warning";
                        }

                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1" || Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "0")
                        {
                            currItemDescription = currItemDescription + "Reserved Warning";
                        }
                        
                    }
                    break;
                #endregion
#region Byte Address = 118
                case 118:
                    {

                        itemName = "Ext Status/Control";
                        addressAllDescription = "Extended module control and status bytes";

                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                        {
                            currItemDescription = "software Tx rate control.=1";
                        }
                        else
                        {
                            currItemDescription = "software Tx rate control.=0";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "Power Level 2 or 3 operation (1.5 or 2.0 Wattmax)";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Power Level 1 operation (1.0 Watt max)";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "enables Power Level 2 or 3 (1.5 or 2.0 Watt max)";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "enables Power Level 1 only (1.0 Watt max)";
                        }
                       
                        
                       

                    }
                    break;
                #endregion
#region Byte Address = 119
                case 119:
                    {

                        itemName = "Ext Status/Control";
                        addressAllDescription = "Extended module control and status bytes";

                        
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                        {
                            currItemDescription = "loss of lock of the Tx CDR.";
                        }
                        else
                        {
                            currItemDescription = "Tx CDR is locked";
                        }
                        if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                        {
                            currItemDescription = currItemDescription + "loss of lock of the Rx CDR";
                        }
                        else
                        {
                            currItemDescription = currItemDescription + "Rx CDR is locked";
                        }




                    }
                    break;
                #endregion
#region Byte Address = 120-126
                case 120:
                case 121:
                case 122:
                case 123:
                case 124:
                case 125:
                case 126:

                    {

                        itemName = "Vendor Specific";
                        addressAllDescription = "Vendor Specific";
                        currItemDescription ="Vendor specific memory addresses";
                    }
                    break;
                #endregion
 #region Byte Address = 127
                case 127:
                    {

                        itemName = "Table Select";
                        addressAllDescription = "Defines the page number for subsequent reads and writes to locations A2h<128-255>";
                                                
                        currItemDescription = "page" + Convert.ToString(addrValue);
                       
                       
                    }
                    break;
                #endregion
#region default

                default:
                    {
                        if (objValues[127]==0||objValues[127]==1)
                        {
                            if (Address >= 128 & Address <= 247)
                            {
                                #region Byte Address = 128-247
                                itemName = "User EEPROM";
                                addressAllDescription = "User writable non-volatile memory";
                                currItemDescription = (addrValue).ToString("X");
                                #endregion
                            }
                            else if (Address >= 248 & Address <= 255)
                            {
                                #region Byte Address = 248-255
                                itemName = "Vendor Control";
                                addressAllDescription = "Vendor specific control addresses";
                                currItemDescription = (addrValue).ToString("X");
                                #endregion
                            }
                        }
                        else if (objValues[127] == 2)
                        {
                            if (Address >= 128 & Address <= 129)
                            {
                                itemName = "Reserved";
                                addressAllDescription = "Reserved for SFF-8690 (Tunable Transmitter)";
                                currItemDescription = (addrValue).ToString("X");
                            }
                            else if (Address ==130)
                            {
                                itemName = "Reserved";
                                addressAllDescription = "Reserved for future receiver controls";
                                currItemDescription = (addrValue).ToString("X");
                            }
                            else if (Address == 131)
                            {
                                itemName = "Rx Decision Threshold";
                                addressAllDescription = "RDT value setting";
                                currItemDescription = (addrValue).ToString("X");
                            }
                            else if (Address >= 132 && Address <=172)
                            {
                                itemName = "Reserved";
                                addressAllDescription = "Reserved for SFF-8690";
                                currItemDescription = (addrValue).ToString("X");
                            }
                            else if (Address >= 173 && Address <= 255)
                            {
                                itemName = "Unallocated";
                                addressAllDescription = "Unallocated";
                                currItemDescription = (addrValue).ToString("X");
                            }
                        }
                       
                        else
                        {
                            itemName = "Unallocated";
                            addressAllDescription = "";
                            currItemDescription = "";
                        }
                    }
                    break;
                #endregion

            }



            #endregion
            
        }
        return currItemDescription;
    }
}