using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ASCXTestPlanInfor : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //TBItemName.Attributes.Add
    } 
#region GSFocus
  
#endregion
 #region ConfigColumNameText
    public string TH2Text
    {
        get
        {
            return this.TH2.Text;
        }
        set
        {
            this.TH2.Text = value;
        }
    }
    public string TH3Text
    {
        get
        {
            return this.TH3.Text;
        }
        set
        {
            this.TH3.Text = value;
        }
    }
    public string TH4Text
    {
        get
        {
            return this.TH4.Text;
        }
        set
        {
            this.TH4.Text = value;
        }
    }
    public string TH5Text
    {
        get
        {
            return this.TH5.Text;
        }
        set
        {
            this.TH5.Text = value;
        }
    }
    public string TH6Text
    {
        get
        {
            return this.TH6.Text;
        }
        set
        {
            this.TH6.Text = value;
        }
    }
    public string TH7Text
    {
        get
        {
            return this.TH7.Text;
        }
        set
        {
            this.TH7.Text = value;
        }
    }
    public string TH8Text
    {
        get
        {
            return this.TH8.Text;
        }
        set
        {
            this.TH8.Text = value;
        }
    }
    public string TH9Text
    {
        get
        {
            return this.TH9.Text;
        }
        set
        {
            this.TH9.Text = value;
        }
    }
    public string TH10Text
    {
        get
        {
            return this.TH10.Text;
        }
        set
        {
            this.TH10.Text = value;
        }
    }
    public string TH11Text
    {
        get
        {
            return this.TH11.Text;
        }
        set
        {
            this.TH11.Text = value;
        }
    }
    public string TH12Text
    {
        get
        {
            return this.TH12.Text;
        }
        set
        {
            this.TH12.Text = value;
        }
    }
    public string TH13Text
    {
        get
        {
            return this.TH13.Text;
        }
        set
        {
            this.TH13.Text = value;
        }
    }
    public string TH14Text
    {
        get
        {
            return this.TH14.Text;
        }
        set
        {
            this.TH14.Text = value;
        }
    }
    public string TH15Text
    {
        get
        {
            return this.TH15.Text;
        }
        set
        {
            this.TH15.Text = value;
        }
    }
    #endregion
#region ConfigText
    public string TBItemNameText
    {
        get
        {
            return TBItemName.Text;
        }
        set
        {
            TBItemName.Text = value;
        }

    }
    public string TBSWVersionText
    {
        get
        {
            return TBSWVersion.Text;
        }
        set
        {
            TBSWVersion.Text = value;
        }

    }
    public string TBHwVersionText
    {
        get
        {
            return TBHwVersion.Text;
        }
        set
        {
            TBHwVersion.Text = value;
        }

    }
    public string TBUSBPortText
    {
        get
        {
            return this.TBUSBPort.Text;
        }
        set
        {
            this.TBUSBPort.Text = value;
        }

    }    
    public string TBDescriptionText
    {
        get
        {
            return this.Description.Text;
        }
        set
        {
            this.Description.Text = value;
        }

    }
    public string TBVersionText
    {
        get
        {
            return this.TextVersion.Text;
        }
        set
        {
            this.TextVersion.Text = value;
        }

    }
#endregion
#region ConfigSelecIndex
    public int DDIsChipIniSelectedIndex
    {
        get
        {
            return DDIsChipIni.SelectedIndex;
        }
        set
        {
            DDIsChipIni.SelectedIndex = value;
        }
    }
    public int DDIgnoreFlagSelectedIndex
    {
        get
        {
            return DDIgnoreFlag.SelectedIndex;
        }
        set
        {
            DDIgnoreFlag.SelectedIndex = value;
        }
    }
    public int DDIsEEPROMIniSelectedIndex
    {
        get
        {
            return DDIsEEPROMIni.SelectedIndex;
        }
        set
        {
            DDIsEEPROMIni.SelectedIndex = value;
        }
    }
    public int DDIgnoreBackupCoefSelectedIndex
    {
        get
        {
            return DDIgnoreBackupCoef.SelectedIndex;
        }
        set
        {
            DDIgnoreBackupCoef.SelectedIndex = value;
        }
    }
    public int DDCheckSNCoefSelectedIndex
    {
        get
        {
            return DDSNCheck.SelectedIndex;
        }
        set
        {
            DDSNCheck.SelectedIndex = value;
        }
    }
    public int DDCheckPNCoefSelectedIndex
    {
        get
        {
            return DDPNCheck.SelectedIndex;
        }
        set
        {
            DDPNCheck.SelectedIndex = value;
        }
    }
    public int DDCheckSWCoefSelectedIndex
    {
        get
        {
            return DDSWCheck.SelectedIndex;
        }
        set
        {
            DDSWCheck.SelectedIndex = value;
        }
    }
    public int DDCDROnSelectedIndex
    {
        get
        {
            return DDCDROn.SelectedIndex;
        }
        set
        {
            DDCDROn.SelectedIndex = value;
        }
    }
#endregion
#region  ConfigEnable
    public bool EnableTBItemName
    {
        get
        {
            return TBItemName.Enabled;
        }
        set
        {
            TBItemName.Enabled = value;
        }
    }

    public bool EnableTBSWVersion
    {
        get
        {
            return TBSWVersion.Enabled;
        }
        set
        {
            TBSWVersion.Enabled = value;
        }
    }
    public bool EnableTBHwVersion
    {
        get
        {
            return TBHwVersion.Enabled;
        }
        set
        {
            TBHwVersion.Enabled = value;
        }
    }
    public bool EnableTBUSBPort
    {
        get
        {
            return TBUSBPort.Enabled;
        }
        set
        {
            TBUSBPort.Enabled = value;
        }
    }
    public bool EnableDDIsChipIni
    {
        get
        {
            return DDIsChipIni.Enabled;
        }
        set
        {
            DDIsChipIni.Enabled = value;
        }
    }
    public bool EnableDDIsEEPROMIni
    {
        get
        {
            return DDIsEEPROMIni.Enabled;
        }
        set
        {
            DDIsEEPROMIni.Enabled = value;
        }
    }
    public bool EnableDDIgnoreBackupCoef
    {
        get
        {
            return DDIgnoreBackupCoef.Enabled;
        }
        set
        {
            DDIgnoreBackupCoef.Enabled = value;
        }
    }
    public bool EnableDDSNCheck
    {
        get
        {
            return DDSNCheck.Enabled;
        }
        set
        {
            DDSNCheck.Enabled = value;
        }
    }
    public bool EnableDDPNCheck
    {
        get
        {
            return DDPNCheck.Enabled;
        }
        set
        {
            DDPNCheck.Enabled = value;
        }
    }
    public bool EnableDDSWCheck
    {
        get
        {
            return DDSWCheck.Enabled;
        }
        set
        {
            DDSWCheck.Enabled = value;
        }
    }
    public bool EnableTBDescription
    {
        get
        {
            return Description.Enabled;
        }
        set
        {
            Description.Enabled = value;
        }
    }
    public bool EnableDDIgnoreFlag
    {
        get
        {
            return DDIgnoreFlag.Enabled;
        }
        set
        {
            DDIgnoreFlag.Enabled = value;
        }
    }
    public bool EnableTextVersion
    {
        get
        {
            return TextVersion.Enabled;
        }
        set
        {
            TextVersion.Enabled = value;
        }
    }
    public bool EnableDDCDROn
    {
        get
        {
            return DDCDROn.Enabled;
        }
        set
        {
            DDCDROn.Enabled = value;
        }
    }
#endregion
#region ConfigExpression
    public string configExpression
    {
        get
        {
            return REV.ValidationExpression;
        }
        set
        {
            REV.ValidationExpression = value;
        }
    }
#endregion
   
  
}