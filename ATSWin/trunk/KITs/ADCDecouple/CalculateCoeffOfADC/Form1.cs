using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace CalculateCoeffOfADC
{
    public partial class Form1 : Form
    {
        int[] O = new int[4];    //存放关闭所有通道，各通道的offset值
        double[,] A = new double[4, 4];   //依次单独开启一个通道，记录各通道的ADC值（并减去对应offset值） 
        double[,] GG = new double[4, 4];  //记录各通道对其他通道影响因子矩阵的逆矩阵

        double[] X = new double[4];   //存放开启所有通道，各通道的ADC值（并减去对应offset值）
        int[] Y = new int[4];   //存放开启所有通道，各通道的除去其他通道影响的ADC值

        Algorithm algorithm = new Algorithm();

        public IOPort USBIO = new IOPort();
        int deviceIndex = 0;

        public Form1()
        {
            InitializeComponent();
            cbMethod.SelectedIndex = 0;
        }

        //计算逆矩阵，并转化成IEEE754(Hex)
        private void button1_Click(object sender, EventArgs e)
        {
            labelG1.Text = "g11,g12,g13,g14：   ";
            labelG2.Text = "g21,g22,g23,g24：   ";
            labelG3.Text = "g31,g32,g33,g34：   ";
            labelG4.Text = "g41,g42,g43,g44：   ";

            O[0] = Convert.ToInt32(tbO1.Text.Trim());
            O[1] = Convert.ToInt32(tbO2.Text.Trim());
            O[2] = Convert.ToInt32(tbO3.Text.Trim());
            O[3] = Convert.ToInt32(tbO4.Text.Trim());

            A[0, 0] = Convert.ToInt32(tbA11.Text.Trim()) - O[0];
            A[0, 1] = Convert.ToInt32(tbA12.Text.Trim()) - O[0];
            A[0, 2] = Convert.ToInt32(tbA13.Text.Trim()) - O[0];
            A[0, 3] = Convert.ToInt32(tbA14.Text.Trim()) - O[0];
            A[1, 0] = Convert.ToInt32(tbA21.Text.Trim()) - O[1];
            A[1, 1] = Convert.ToInt32(tbA22.Text.Trim()) - O[1];
            A[1, 2] = Convert.ToInt32(tbA23.Text.Trim()) - O[1];
            A[1, 3] = Convert.ToInt32(tbA24.Text.Trim()) - O[1];
            A[2, 0] = Convert.ToInt32(tbA31.Text.Trim()) - O[2];
            A[2, 1] = Convert.ToInt32(tbA32.Text.Trim()) - O[2];
            A[2, 2] = Convert.ToInt32(tbA33.Text.Trim()) - O[2];
            A[2, 3] = Convert.ToInt32(tbA34.Text.Trim()) - O[2];
            A[3, 0] = Convert.ToInt32(tbA41.Text.Trim()) - O[3];
            A[3, 1] = Convert.ToInt32(tbA42.Text.Trim()) - O[3];
            A[3, 2] = Convert.ToInt32(tbA43.Text.Trim()) - O[3];
            A[3, 3] = Convert.ToInt32(tbA44.Text.Trim()) - O[3];

            if (cbMethod.SelectedIndex == 0)
            {               
                GG = Algorithm.CalculateCoeff(A, 0);               
            }
            else
            {
                GG = Algorithm.CalculateCoeff(A, 1);
            }

            byte[] g11 = algorithm.FloatToIEE754((float)GG[0, 0]);
            tbG11_0.Text = g11[0].ToString("X");
            tbG11_1.Text = g11[1].ToString("X");
            tbG11_2.Text = g11[2].ToString("X");
            tbG11_3.Text = g11[3].ToString("X");
            byte[] g12 = algorithm.FloatToIEE754((float)GG[0, 1]);
            tbG12_0.Text = g12[0].ToString("X");
            tbG12_1.Text = g12[1].ToString("X");
            tbG12_2.Text = g12[2].ToString("X");
            tbG12_3.Text = g12[3].ToString("X");
            byte[] g13 = algorithm.FloatToIEE754((float)GG[0, 2]);
            tbG13_0.Text = g13[0].ToString("X");
            tbG13_1.Text = g13[1].ToString("X");
            tbG13_2.Text = g13[2].ToString("X");
            tbG13_3.Text = g13[3].ToString("X");
            byte[] g14 = algorithm.FloatToIEE754((float)GG[0, 3]);
            tbG14_0.Text = g14[0].ToString("X");
            tbG14_1.Text = g14[1].ToString("X");
            tbG14_2.Text = g14[2].ToString("X");
            tbG14_3.Text = g14[3].ToString("X");
           
            byte[] g21 = algorithm.FloatToIEE754((float)GG[1, 0]);
            tbG21_0.Text = g21[0].ToString("X");
            tbG21_1.Text = g21[1].ToString("X");
            tbG21_2.Text = g21[2].ToString("X");
            tbG21_3.Text = g21[3].ToString("X");
            byte[] g22 = algorithm.FloatToIEE754((float)GG[1, 1]);
            tbG22_0.Text = g22[0].ToString("X");
            tbG22_1.Text = g22[1].ToString("X");
            tbG22_2.Text = g22[2].ToString("X");
            tbG22_3.Text = g22[3].ToString("X");
            byte[] g23 = algorithm.FloatToIEE754((float)GG[1, 2]);
            tbG23_0.Text = g23[0].ToString("X");
            tbG23_1.Text = g23[1].ToString("X");
            tbG23_2.Text = g23[2].ToString("X");
            tbG23_3.Text = g23[3].ToString("X");
            byte[] g24 = algorithm.FloatToIEE754((float)GG[1, 3]);
            tbG24_0.Text = g24[0].ToString("X");
            tbG24_1.Text = g24[1].ToString("X");
            tbG24_2.Text = g24[2].ToString("X");
            tbG24_3.Text = g24[3].ToString("X");

            byte[] g31 = algorithm.FloatToIEE754((float)GG[2, 0]);
            tbG31_0.Text = g31[0].ToString("X");
            tbG31_1.Text = g31[1].ToString("X");
            tbG31_2.Text = g31[2].ToString("X");
            tbG31_3.Text = g31[3].ToString("X");
            byte[] g32 = algorithm.FloatToIEE754((float)GG[2, 1]);
            tbG32_0.Text = g32[0].ToString("X");
            tbG32_1.Text = g32[1].ToString("X");
            tbG32_2.Text = g32[2].ToString("X");
            tbG32_3.Text = g32[3].ToString("X");
            byte[] g33 = algorithm.FloatToIEE754((float)GG[2, 2]);
            tbG33_0.Text = g33[0].ToString("X");
            tbG33_1.Text = g33[1].ToString("X");
            tbG33_2.Text = g33[2].ToString("X");
            tbG33_3.Text = g33[3].ToString("X");
            byte[] g34 = algorithm.FloatToIEE754((float)GG[2, 3]);
            tbG34_0.Text = g34[0].ToString("X");
            tbG34_1.Text = g34[1].ToString("X");
            tbG34_2.Text = g34[2].ToString("X");
            tbG34_3.Text = g34[3].ToString("X");

            byte[] g41 = algorithm.FloatToIEE754((float)GG[3, 0]);
            tbG41_0.Text = g41[0].ToString("X");
            tbG41_1.Text = g41[1].ToString("X");
            tbG41_2.Text = g41[2].ToString("X");
            tbG41_3.Text = g41[3].ToString("X");
            byte[] g42 = algorithm.FloatToIEE754((float)GG[3, 1]);
            tbG42_0.Text = g42[0].ToString("X");
            tbG42_1.Text = g42[1].ToString("X");
            tbG42_2.Text = g42[2].ToString("X");
            tbG42_3.Text = g42[3].ToString("X");
            byte[] g43 = algorithm.FloatToIEE754((float)GG[3, 2]);
            tbG43_0.Text = g43[0].ToString("X");
            tbG43_1.Text = g43[1].ToString("X");
            tbG43_2.Text = g43[2].ToString("X");
            tbG43_3.Text = g43[3].ToString("X");
            byte[] g44 = algorithm.FloatToIEE754((float)GG[3, 3]);
            tbG44_0.Text = g44[0].ToString("X");
            tbG44_1.Text = g44[1].ToString("X");
            tbG44_2.Text = g44[2].ToString("X");
            tbG44_3.Text = g44[3].ToString("X");

            labelG1.Text += Math.Round(GG[0, 0], 4).ToString().PadLeft(5, ' ') + "      ";
            labelG1.Text += Math.Round(GG[0, 1], 4).ToString().PadLeft(5, ' ') + "      ";
            labelG1.Text += Math.Round(GG[0, 2], 4).ToString().PadLeft(5, ' ') + "      ";
            labelG1.Text += Math.Round(GG[0, 3], 4).ToString().PadLeft(5, ' ') + "      ";

            labelG2.Text += Math.Round(GG[1, 0], 4).ToString().PadLeft(5, ' ') + "      ";
            labelG2.Text += Math.Round(GG[1, 1], 4).ToString().PadLeft(5, ' ') + "      ";
            labelG2.Text += Math.Round(GG[1, 2], 4).ToString().PadLeft(5, ' ') + "      ";
            labelG2.Text += Math.Round(GG[1, 3], 4).ToString().PadLeft(5, ' ') + "      ";

            labelG3.Text += Math.Round(GG[2, 0], 4).ToString().PadLeft(5, ' ') + "      ";
            labelG3.Text += Math.Round(GG[2, 1], 4).ToString().PadLeft(5, ' ') + "      ";
            labelG3.Text += Math.Round(GG[2, 2], 4).ToString().PadLeft(5, ' ') + "      ";
            labelG3.Text += Math.Round(GG[2, 3], 4).ToString().PadLeft(5, ' ') + "      ";

            labelG4.Text += Math.Round(GG[3, 0], 4).ToString().PadLeft(5, ' ') + "      ";
            labelG4.Text += Math.Round(GG[3, 1], 4).ToString().PadLeft(5, ' ') + "      ";
            labelG4.Text += Math.Round(GG[3, 2], 4).ToString().PadLeft(5, ' ') + "      ";
            labelG4.Text += Math.Round(GG[3, 3], 4).ToString().PadLeft(5, ' ') + "      ";
          
        }

        //计算去干扰后的ADC值
        private void button2_Click(object sender, EventArgs e)
        {
            X[0] = Convert.ToInt32(tbX1.Text.Trim()) - O[0];
            X[1] = Convert.ToInt32(tbX2.Text.Trim()) - O[1];
            X[2] = Convert.ToInt32(tbX3.Text.Trim()) - O[2];
            X[3] = Convert.ToInt32(tbX4.Text.Trim()) - O[3];

            if (cbMethod.SelectedIndex == 0)
            {
                Y = Algorithm.CalculateY(GG, X, 0);
            }
            else
            {
                Y = Algorithm.CalculateY(GG, X, 1);
            }

            tbY1.Text = Y[0].ToString();
            tbY2.Text = Y[1].ToString();
            tbY3.Text = Y[2].ToString();
            tbY4.Text = Y[3].ToString();

        }

        //将Offset及逆矩阵各系数写入模块
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = true;
                
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;

                // QSFP28 page8
                Engmod(8);

                //写Offset，U16
                //144-145:  通道全关时的CH1 A/D.  大字节序.
                //146-147:  通道全关时的CH2 A/D.  大字节序.
                //148-149:  通道全关时的CH3 A/D.  大字节序.
                //150-151:  通道全关时的CH4 A/D.  大字节序.
                flag = SetCoef(deviceIndex, 0xA0, 144, O[0].ToString(), 2);
                if (flag == true)
                {
                    SetCoef(deviceIndex, 0xA0, 146, O[1].ToString(), 2);
                    SetCoef(deviceIndex, 0xA0, 148, O[2].ToString(), 2);
                    SetCoef(deviceIndex, 0xA0, 150, O[3].ToString(), 2);

                    //写逆矩阵G'，IEEE754
                    //152-155: G11,  156-159: G12,  160-163: G13,  164-167: G14
                    //168-171: G21,  172-175: G22,  176-179: G23,  180-183: G24
                    //184-187: G31,  188-191: G32,  192-195: G33,  196-199: G34
                    //200-203: G41,  204-207: G42,  208-211: G43,  212-215: G44
                    SetCoef(deviceIndex, 0xA0, 152, GG[0, 0].ToString(), 1);
                    SetCoef(deviceIndex, 0xA0, 156, GG[0, 1].ToString(), 1);
                    SetCoef(deviceIndex, 0xA0, 160, GG[0, 2].ToString(), 1);
                    SetCoef(deviceIndex, 0xA0, 164, GG[0, 3].ToString(), 1);
                    SetCoef(deviceIndex, 0xA0, 168, GG[1, 0].ToString(), 1);
                    SetCoef(deviceIndex, 0xA0, 172, GG[1, 1].ToString(), 1);
                    SetCoef(deviceIndex, 0xA0, 176, GG[1, 2].ToString(), 1);
                    SetCoef(deviceIndex, 0xA0, 180, GG[1, 3].ToString(), 1);
                    SetCoef(deviceIndex, 0xA0, 184, GG[2, 0].ToString(), 1);
                    SetCoef(deviceIndex, 0xA0, 188, GG[2, 1].ToString(), 1);
                    SetCoef(deviceIndex, 0xA0, 192, GG[2, 2].ToString(), 1);
                    SetCoef(deviceIndex, 0xA0, 196, GG[2, 3].ToString(), 1);
                    SetCoef(deviceIndex, 0xA0, 200, GG[3, 0].ToString(), 1);
                    SetCoef(deviceIndex, 0xA0, 204, GG[3, 1].ToString(), 1);
                    SetCoef(deviceIndex, 0xA0, 208, GG[3, 2].ToString(), 1);
                    SetCoef(deviceIndex, 0xA0, 212, GG[3, 3].ToString(), 1);

                    MessageBox.Show("Offset及逆矩阵系数G'已写入模块！");
                }
                else
                {
                    MessageBox.Show("写入过程出错，请检查！");
                }

                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
            }
            catch
            {
                MessageBox.Show("写入过程出错，请检查！");
            }
        }

        public void Engmod(byte engpage)
        {
            byte[] buff = new byte[5];
            buff[0] = 0xca;
            buff[1] = 0x2d;
            buff[2] = 0x81;
            buff[3] = 0x5f;
            buff[4] = engpage;
            USBIO.WrtieReg(deviceIndex, 0xA0, 123, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        }

        public bool SetCoef(int deviceIndex, int deviceAddress, int regAddress, string coef, byte format, int phycialAdress = 0, int mdiomode = 0)
        {//1 ieee754;2 UInt16;3 UInt8
            bool flag = false;
            try
            {
                switch (format)
                {
                    case 1:
                        float fcoef = Convert.ToSingle(coef);
                        flag = floattoieee(deviceIndex, deviceAddress, regAddress, fcoef, phycialAdress, mdiomode);
                        break;
                    case 2:
                        //UInt16 u16coef = Convert.ToUInt16(Convert.ToDouble(coef));
                        UInt16 u16coef = (UInt16)(Convert.ToDouble(coef));
                        flag = u16tobyte(deviceIndex, deviceAddress, regAddress, u16coef, phycialAdress, mdiomode);
                        break;                  
                    default:
                        break;
                }
                return flag;
            }
            catch (Exception error)
            {
                //MessageBox.Show(error.ToString());
                return false;
            }

        }

        public bool floattoieee(int deviceIndex, int deviceAddress, int regAddress, float fcoef, int phycialAdress = 0, int mdiomode = 0)
        {
            bool flag = false;
            byte[] bcoef = new byte[4];
            UInt16[] bcoefmdio = new UInt16[4];
            System.Threading.Thread.Sleep(100);
            bcoef = BitConverter.GetBytes(fcoef);
            System.Threading.Thread.Sleep(100);
            
            if (mdiomode == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    bcoefmdio[i] = (UInt16)(bcoef[i]);
                }

                USBIO.WriteMDIO(deviceIndex, deviceAddress, phycialAdress, regAddress, IOPort.MDIOSoftHard.SOFTWARE, bcoefmdio);
                System.Threading.Thread.Sleep(200);

                for (int i = 0; i < 4; i++)
                {
                    UInt16[] rcoef = new UInt16[4];

                    rcoef = USBIO.ReadMDIO(deviceIndex, deviceAddress, phycialAdress, regAddress, IOPort.MDIOSoftHard.SOFTWARE, 4);
                    if ((bcoefmdio[0] != rcoef[0]) || (bcoefmdio[1] != rcoef[1]) || (bcoefmdio[2] != rcoef[2]) || (bcoefmdio[3] != rcoef[3]))
                    {
                        if (i > 2)
                        {
                            return false;
                        }
                        USBIO.WriteMDIO(deviceIndex, deviceAddress, phycialAdress, regAddress, IOPort.MDIOSoftHard.SOFTWARE, bcoefmdio);
                        System.Threading.Thread.Sleep(100);
                    }
                    else
                    {
                        flag = true;
                        break;
                    }

                }
            }
            else
            {
                USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, bcoef);
                System.Threading.Thread.Sleep(200);

                for (int i = 0; i < 4; i++)
                {
                    byte[] rcoef = new byte[4];
                    rcoef = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 4);
                    if ((bcoef[0] != rcoef[0]) || (bcoef[1] != rcoef[1]) || (bcoef[2] != rcoef[2]) || (bcoef[3] != rcoef[3]))
                    {
                        USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, bcoef);
                        System.Threading.Thread.Sleep(200);
                    }
                    else
                    {
                        flag = true;
                        break;
                    }

                }
            }
            return flag;
        }
       
        public bool u16tobyte(int deviceIndex, int deviceAddress, int regAddress, UInt16 coef, int phycialAdress = 0, int mdiomode = 0)
        {
            bool flag = false;
            byte[] buff = new byte[2];
            UInt16[] bcoefmdio = new UInt16[2];
            buff[0] = (byte)((coef >> 8) & 0xff);
            buff[1] = (byte)(coef & 0xff);
            bcoefmdio[0] = (UInt16)(buff[0]);
            bcoefmdio[1] = (UInt16)(buff[1]);
            if (mdiomode == 1)
            {
                USBIO.WriteMDIO(deviceIndex, deviceAddress, phycialAdress, regAddress, IOPort.MDIOSoftHard.SOFTWARE, bcoefmdio);
                System.Threading.Thread.Sleep(200);

                for (int i = 0; i < 4; i++)
                {
                    UInt16[] rcoef = new UInt16[2];
                    rcoef = USBIO.ReadMDIO(deviceIndex, deviceAddress, phycialAdress, regAddress, IOPort.MDIOSoftHard.SOFTWARE, 2);
                    if ((bcoefmdio[0] != rcoef[0]) || (bcoefmdio[1] != rcoef[1]))
                    {
                        USBIO.WriteMDIO(deviceIndex, deviceAddress, phycialAdress, regAddress, IOPort.MDIOSoftHard.SOFTWARE, bcoefmdio);
                        System.Threading.Thread.Sleep(200);
                    }
                    else
                    {
                        flag = true;
                        break;
                    }

                }

            }
            else
            {
                USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                System.Threading.Thread.Sleep(200);

                for (int i = 0; i < 4; i++)
                {
                    byte[] rcoef = new byte[2];
                    rcoef = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 2);
                    if ((buff[0] != rcoef[0]) || (buff[1] != rcoef[1]))
                    {
                        USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                        System.Threading.Thread.Sleep(200);
                    }
                    else
                    {
                        flag = true;
                        break;
                    }

                }
            }
            return flag;
        }
    
    }
}
