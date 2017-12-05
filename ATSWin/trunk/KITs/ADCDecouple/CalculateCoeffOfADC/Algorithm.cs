using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace CalculateCoeffOfADC
{

  public  class Algorithm
  {
      public byte[] FloatToIEE754(float inputfloat)
        {
            try
            {
                //float tempfloat = float.Parse(inputfloat.ToString());
                return BitConverter.GetBytes(inputfloat);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
            
        }

        /// <summary> 
        /// 单开各通道，求系数矩阵的逆，coeffMatrix * single = Y 求得coeffMatrix = Y*（single的逆）
        /// </summary> 
        /// <param name="singleChEnable">单开各通道，记录相应ADC值的矩阵，矩阵中各值已减去Offset值</param> 
        /// <param name="method">方法选择，0：通用方法；1：克莱姆法则</param> 
        /// <returns></returns> 
        public static double[,] CalculateCoeff(double[,] singleChEnable,byte method)
        {
            if (method == 0)        //通用方法
            {
                double[,] newMatrixY = new double[singleChEnable.GetLength(0), singleChEnable.GetLength(1)];

                for (int i = 0; i < singleChEnable.GetLength(0); i++)
                {
                    for (int j = 0; j < singleChEnable.GetLength(1); j++)
                    {
                        if (j == i)
                        {
                            newMatrixY[i, j] = singleChEnable[i, j];
                        }
                        else
                        {
                            newMatrixY[i, j] = 0;
                        }
                    }
                }

                double[,] temp = new double[singleChEnable.GetLength(0), singleChEnable.GetLength(1)];
                for (int i = 0; i < singleChEnable.GetLength(0); i++)
                {
                    for (int j = 0; j < singleChEnable.GetLength(1); j++)
                    {
                        temp[i, j] = singleChEnable[i, j];
                    }
                }
                //先计算矩阵的行列式的值是否等于0，如果不等于0则该矩阵是可逆的  
                double result = MatrixMath.mathDeterminantCalculation(temp);

                if (result == 0)
                {
                    throw new Exception("矩阵不可逆");
                }

                double[,] newMatrixCoeff = MatrixMath.mult(newMatrixY, MatrixMath.getInverseMatrix(singleChEnable, result));

                return newMatrixCoeff;
            }
            else
            {
                singleChEnable = MatrixMath.invert(singleChEnable);
                double[,] newMatrixCoeff = new double[4, 4];

                newMatrixCoeff[0, 0] = CalculateDeterminant(singleChEnable, 0, 0);
                newMatrixCoeff[0, 1] = CalculateDeterminant(singleChEnable, 1, 0);
                newMatrixCoeff[0, 2] = CalculateDeterminant(singleChEnable, 2, 0);
                newMatrixCoeff[0, 3] = CalculateDeterminant(singleChEnable, 3, 0);
                newMatrixCoeff[1, 0] = CalculateDeterminant(singleChEnable, 0, 1);
                newMatrixCoeff[1, 1] = CalculateDeterminant(singleChEnable, 1, 1);
                newMatrixCoeff[1, 2] = CalculateDeterminant(singleChEnable, 2, 1);
                newMatrixCoeff[1, 3] = CalculateDeterminant(singleChEnable, 3, 1);
                newMatrixCoeff[2, 0] = CalculateDeterminant(singleChEnable, 0, 2);
                newMatrixCoeff[2, 1] = CalculateDeterminant(singleChEnable, 1, 2);
                newMatrixCoeff[2, 2] = CalculateDeterminant(singleChEnable, 2, 2);
                newMatrixCoeff[2, 3] = CalculateDeterminant(singleChEnable, 3, 2);
                newMatrixCoeff[3, 0] = CalculateDeterminant(singleChEnable, 0, 3);
                newMatrixCoeff[3, 1] = CalculateDeterminant(singleChEnable, 1, 3);
                newMatrixCoeff[3, 2] = CalculateDeterminant(singleChEnable, 2, 3);
                newMatrixCoeff[3, 3] = CalculateDeterminant(singleChEnable, 3, 3);

                return newMatrixCoeff;
            }
        }

        //Y = coeffMatrix * all 求得全开每个通道的值
        public static int[] CalculateY(double[,] coeff, double[] allChEnable,byte method)
        {
            if (method == 0)
            {
                double[] ddnewMatrixY = MatrixMath.mult(coeff, allChEnable);
                int[] newMatrixY = new int[ddnewMatrixY.Length];
                 for (int i = 0; i < ddnewMatrixY.Length; i++)
                {
                    newMatrixY[i] = (int)ddnewMatrixY[i];
                }
                
                return newMatrixY;
            }
            else
            {
                int[] newMatrixY = new int[4];
                newMatrixY[0] = (int)(coeff[0, 0] * allChEnable[0] + coeff[0, 1] * allChEnable[1] + coeff[0, 2] * allChEnable[2] + coeff[0, 3] * allChEnable[3]);
                newMatrixY[1] = (int)(coeff[1, 0] * allChEnable[0] + coeff[1, 1] * allChEnable[1] + coeff[1, 2] * allChEnable[2] + coeff[1, 3] * allChEnable[3]);
                newMatrixY[2] = (int)(coeff[2, 0] * allChEnable[0] + coeff[2, 1] * allChEnable[1] + coeff[2, 2] * allChEnable[2] + coeff[2, 3] * allChEnable[3]);
                newMatrixY[3] = (int)(coeff[3, 0] * allChEnable[0] + coeff[3, 1] * allChEnable[1] + coeff[3, 2] * allChEnable[2] + coeff[3, 3] * allChEnable[3]);

                return newMatrixY;
            }
            
        }

        /// <summary> 
        /// 依据克莱姆法则，替换矩阵GG第index列的第cnt行的值,并计算行列式，将其与GG行列式的值相除
        /// </summary> 
        private static double CalculateDeterminant(double[,] GG, int index, int cnt)
        {
            double[,] G = new double[4, 4];
            double Di = 0;

            double D = MatrixMath.Determinant(GG);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j == index)
                    {
                        if (i == cnt)
                        {
                            G[i, j] = GG[cnt, cnt];
                        }
                        else
                        {
                            G[i, j] = 0;
                        }
                    }
                    else
                    {
                        G[i, j] = GG[i, j];
                    }
                }
            }
            Di = MatrixMath.Determinant(G);
            double Yi = Di / D;

            return Yi;
        }

  }
}
