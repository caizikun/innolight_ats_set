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

        double[] X = new double[4];   //存放开启所有通道，各通道的DAC值（并减去对应offset值）
        int[] Y = new int[4];   //存放开启所有通道，各通道的除去其他通道影响的DAC值

        Algorithm algorithm = new Algorithm();

        public Form1()
        {
            InitializeComponent();
            cbMethod.SelectedIndex = 0;
        }

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
            tbG11_0.Text = g11[0].ToString();
            tbG11_1.Text = g11[1].ToString();
            tbG11_2.Text = g11[2].ToString();
            tbG11_3.Text = g11[3].ToString();
            byte[] g12 = algorithm.FloatToIEE754((float)GG[0, 1]);
            tbG12_0.Text = g12[0].ToString();
            tbG12_1.Text = g12[1].ToString();
            tbG12_2.Text = g12[2].ToString();
            tbG12_3.Text = g12[3].ToString();
            byte[] g13 = algorithm.FloatToIEE754((float)GG[0, 2]);
            tbG13_0.Text = g13[0].ToString();
            tbG13_1.Text = g13[1].ToString();
            tbG13_2.Text = g13[2].ToString();
            tbG13_3.Text = g13[3].ToString();
            byte[] g14 = algorithm.FloatToIEE754((float)GG[0, 3]);
            tbG14_0.Text = g14[0].ToString();
            tbG14_1.Text = g14[1].ToString();
            tbG14_2.Text = g14[2].ToString();
            tbG14_3.Text = g14[3].ToString();
           
            byte[] g21 = algorithm.FloatToIEE754((float)GG[1, 0]);
            tbG21_0.Text = g21[0].ToString();
            tbG21_1.Text = g21[1].ToString();
            tbG21_2.Text = g21[2].ToString();
            tbG21_3.Text = g21[3].ToString();
            byte[] g22 = algorithm.FloatToIEE754((float)GG[1, 1]);
            tbG22_0.Text = g22[0].ToString();
            tbG22_1.Text = g22[1].ToString();
            tbG22_2.Text = g22[2].ToString();
            tbG22_3.Text = g22[3].ToString();
            byte[] g23 = algorithm.FloatToIEE754((float)GG[1, 2]);
            tbG23_0.Text = g23[0].ToString();
            tbG23_1.Text = g23[1].ToString();
            tbG23_2.Text = g23[2].ToString();
            tbG23_3.Text = g23[3].ToString();
            byte[] g24 = algorithm.FloatToIEE754((float)GG[1, 3]);
            tbG24_0.Text = g24[0].ToString();
            tbG24_1.Text = g24[1].ToString();
            tbG24_2.Text = g24[2].ToString();
            tbG24_3.Text = g24[3].ToString();

            byte[] g31 = algorithm.FloatToIEE754((float)GG[2, 0]);
            tbG31_0.Text = g31[0].ToString();
            tbG31_1.Text = g31[1].ToString();
            tbG31_2.Text = g31[2].ToString();
            tbG31_3.Text = g31[3].ToString();
            byte[] g32 = algorithm.FloatToIEE754((float)GG[2, 1]);
            tbG32_0.Text = g32[0].ToString();
            tbG32_1.Text = g32[1].ToString();
            tbG32_2.Text = g32[2].ToString();
            tbG32_3.Text = g32[3].ToString();
            byte[] g33 = algorithm.FloatToIEE754((float)GG[2, 2]);
            tbG33_0.Text = g33[0].ToString();
            tbG33_1.Text = g33[1].ToString();
            tbG33_2.Text = g33[2].ToString();
            tbG33_3.Text = g33[3].ToString();
            byte[] g34 = algorithm.FloatToIEE754((float)GG[2, 3]);
            tbG34_0.Text = g34[0].ToString();
            tbG34_1.Text = g34[1].ToString();
            tbG34_2.Text = g34[2].ToString();
            tbG34_3.Text = g34[3].ToString();

            byte[] g41 = algorithm.FloatToIEE754((float)GG[3, 0]);
            tbG41_0.Text = g41[0].ToString();
            tbG41_1.Text = g41[1].ToString();
            tbG41_2.Text = g41[2].ToString();
            tbG41_3.Text = g41[3].ToString();
            byte[] g42 = algorithm.FloatToIEE754((float)GG[3, 1]);
            tbG42_0.Text = g42[0].ToString();
            tbG42_1.Text = g42[1].ToString();
            tbG42_2.Text = g42[2].ToString();
            tbG42_3.Text = g42[3].ToString();
            byte[] g43 = algorithm.FloatToIEE754((float)GG[3, 2]);
            tbG43_0.Text = g43[0].ToString();
            tbG43_1.Text = g43[1].ToString();
            tbG43_2.Text = g43[2].ToString();
            tbG43_3.Text = g43[3].ToString();
            byte[] g44 = algorithm.FloatToIEE754((float)GG[3, 3]);
            tbG44_0.Text = g44[0].ToString();
            tbG44_1.Text = g44[1].ToString();
            tbG44_2.Text = g44[2].ToString();
            tbG44_3.Text = g44[3].ToString();

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

    }
}
