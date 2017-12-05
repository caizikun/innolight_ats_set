using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using Ivi.Visa.Interop;
using System.Collections;
using System.Text.RegularExpressions;
using ATS_Framework;
using System.Windows.Forms;
using System.IO;
namespace ATS_Framework
{

  public  class Algorithm
    {
        ///用最小二乘法拟合二元多次曲线
        ///</summary>
        ///<param name="arrX">已知点的x坐标集合</param>
        ///<param name="arrY">已知点的y坐标集合</param>
        ///<param name="length">已知点的个数</param>
        ///<param name="dimension">方程的最高次数</param>
        public double[] MultiLine(double[] arrX, double[] arrY, int length, int dimension) //二元多次线性方程拟合曲线
        {
            int n = dimension + 1;
            if (length <= 2 && dimension==2)
            {
                n = 2;
            }                             //dimension次方程需要求 dimension+1个 系数
            double[,] Guass = new double[n, n + 1];      //高斯矩阵 例如：y=a0+a1*x+a2*x*x
            for (int i = 0; i < n; i++)
            {
                int j;
                for (j = 0; j < n; j++)
                {
                    Guass[i, j] = SumArr(arrX, j + i, length);
                }
                Guass[i, j] = SumArr(arrX, i, arrY, 1, length);
            }
            if (length <= 2 && dimension == 2)
            {
                double[]temp=ComputGauss(Guass, n);
                double[] coef = new double[temp.Length + 1];
                for (byte i = 0; i < temp.Length;i++)
                {
                    coef[i] = temp[i];
                }
                return coef;
            }
            return ComputGauss(Guass, n);
        }
        public double[] MultiLine(int[] arrX, int[] arrY, int length, int dimension) //二元多次线性方程拟合曲线
        {
            int n = dimension + 1;                  //dimension次方程需要求 dimension+1个 系数
            if (length <= 2 && dimension == 2)
            {
                n = 2;
            } 
            double[,] Guass = new double[n, n + 1];      //高斯矩阵 例如：y=a0+a1*x+a2*x*x
            for (int i = 0; i < n; i++)
            {
                int j;
                for (j = 0; j < n; j++)
                {
                    Guass[i, j] = SumArr(arrX, j + i, length);
                }
                Guass[i, j] = SumArr(arrX, i, arrY, 1, length);
            }
            if (length <= 2 && dimension == 2)
            {
                double[] temp = ComputGauss(Guass, n);
                double[] coef = new double[temp.Length + 1];
                for (byte i = 0; i < temp.Length; i++)
                {
                    coef[i] = temp[i];
                }
                return coef;
            }
            return ComputGauss(Guass, n);

        }
        private double SumArr(double[] arr, int n, int length) //求数组的元素的n次方的和
        {
            double s = 0;
            for (int i = 0; i < length; i++)
            {
                if (arr[i] != 0 || n != 0)
                    s = s + Math.Pow(arr[i], n);
                else
                    s = s + 1;
            }
            return s;
        }
        private double SumArr(double[] arr1, int n1, double[] arr2, int n2, int length)
        {
            double s = 0;
            for (int i = 0; i < length; i++)
            {
                if ((arr1[i] != 0 || n1 != 0) && (arr2[i] != 0 || n2 != 0))
                    s = s + Math.Pow(arr1[i], n1) * Math.Pow(arr2[i], n2);
                else
                    s = s + 1;
            }
            return s;

        }
        private double SumArr(int[] arr, int n, int length) //求数组的元素的n次方的和
        {
            double s = 0;
            for (int i = 0; i < length; i++)
            {
                if (arr[i] != 0 || n != 0)
                    s = s + Math.Pow(arr[i], n);
                else
                    s = s + 1;
            }
            return s;
        }
        private double SumArr(int[] arr1, int n1, int[] arr2, int n2, int length)
        {
            double s = 0;
            for (int i = 0; i < length; i++)
            {
                if ((arr1[i] != 0 || n1 != 0) && (arr2[i] != 0 || n2 != 0))
                    s = s + Math.Pow(arr1[i], n1) * Math.Pow(arr2[i], n2);
                else
                    s = s + 1;
            }
            return s;

        }
        private double[] ComputGauss(double[,] Guass, int n)
        {
            int i, j;
            int k, m;
            double temp;
            double max;
            double s;
            double[] x = new double[n];

            for (i = 0; i < n; i++) x[i] = 0.0;//初始化


            for (j = 0; j < n; j++)
            {
                max = 0;

                k = j;
                for (i = j; i < n; i++)
                {
                    if (Math.Abs(Guass[i, j]) > max)
                    {
                        max = Guass[i, j];
                        k = i;
                    }
                }



                if (k != j)
                {
                    for (m = j; m < n + 1; m++)
                    {
                        temp = Guass[j, m];
                        Guass[j, m] = Guass[k, m];
                        Guass[k, m] = temp;

                    }
                }

                if (0 == max)
                {
                    // "此线性方程为奇异线性方程" 

                    return x;
                }


                for (i = j + 1; i < n; i++)
                {

                    s = Guass[i, j];
                    for (m = j; m < n + 1; m++)
                    {
                        Guass[i, m] = Guass[i, m] - Guass[j, m] * s / (Guass[j, j]);

                    }
                }


            }//结束for (j=0;j<n;j++)


            for (i = n - 1; i >= 0; i--)
            {
                s = 0;
                for (j = i + 1; j < n; j++)
                {
                    s = s + Guass[i, j] * x[j];
                }

                x[i] = (Guass[i, n] - s) / Guass[i, i];

            }

            return x;
        }//返回值是函数的系数

       
        // cense test algorithm
        public double LinearRegression(double[] x, double[] yList, out double  slop ,out double  intercept)
        {
            double result = 0;
            double β;
            double α;
            slop = 0;
            intercept = 0;
            int n = x.Length; //个数（n）
            if (n == 0) { return 0; }
            if (n == 1) { return yList[0]; }
            double predictX = x[n - 1] + 1; //预测指标值
            //∑XiYi
            double sumXiYi = 0;
            //∑Xi
            double sumXi = 0;
            //∑Yi
            double sumYi = 0;
            //∑（Xi二次方）
            double XiSqrtSum = 0;
            for (int i = 0; i < n; i++)
            {
                sumXiYi += x[i] * yList[i];
                sumXi += x[i];
                sumYi += yList[i];
                XiSqrtSum += x[i] * x[i];
            }
            //∑Xi∑Yi
            double sumXisumYi = sumXi * sumYi;
            //（∑Xi）二次方
            double sumXiSqrt = sumXi * sumXi;
            //β= (n * ∑XiYi - ∑Xi * ∑Yi) / (n * ∑(Xi二次方) - (∑Xi)二次方)
            β = ((n * sumXiYi) - (sumXi * sumYi)) / ((n * XiSqrtSum) - sumXiSqrt);
            //α= (∑Yi / n) - β*(∑Xi / n)
            α = (sumYi / n) - β * (sumXi / n);
            //预测结果 = α+β* 预测指标值
            slop=α;
            intercept = β;
             result = α + (β * predictX);    
            return Math.Round(result, 2);
        }
        public double[] Getlog10(double[] input)
        {
            double[] inPut = new double[input.Length];
            //inPut=input;
            for (byte i = 0; i < input.Length; i++)
            {
                inPut[i] = System.Math.Log10(input[i]);

            }
            return inPut;

        }
        public double Getlog10(double input)
        {
            double inPut = input;
            //inPut=input;
              inPut = System.Math.Log10(input);
           
            return inPut;

        }
        public double[] GetNegative(double[] input)
        {
            double[] inPut = new double[input.Length];
            for (byte i = 0; i < input.Length; i++)
            {
                inPut[i] = input[i] * (-1);

            }
            return inPut;
        }

       
        
        public float SelectMinValue(ArrayList inPutArray, out byte minIndex)// for cense test only
        {
            byte itemCount = 0;
            minIndex = 0;
            int firstitemindex = inPutArray.IndexOf(0);
            if (firstitemindex == -1)
            {
                float tempMinValue = float.Parse(inPutArray[0].ToString());
                for (byte i = 0; i < inPutArray.Count; i++)
                {
                    if (tempMinValue >= float.Parse(inPutArray[i].ToString()))
                    {
                        tempMinValue = float.Parse(inPutArray[i].ToString());
                        minIndex = i;
                    }

                }

            }
            else
            {
                itemCount = 1;
                int tempIndex = 0;
                do
                {
                    tempIndex = inPutArray.IndexOf(0, firstitemindex + itemCount);
                    if (tempIndex != -1)
                    {
                        itemCount++;
                    }

                } while (tempIndex != -1 && (tempIndex - (firstitemindex + itemCount)) == -1);
                if (itemCount % 2 == 0)
                {
                    minIndex = Convert.ToByte((itemCount / 2 - 1) + firstitemindex);
                }
                else
                {
                    minIndex = Convert.ToByte((itemCount / 2) + firstitemindex);
                }

            }

            return float.Parse(inPutArray[minIndex].ToString());
        }
       
        public double SelectMaxValue(ArrayList inPutArray, out byte maxIndex)
        {
            maxIndex = 0;
            int firstitemindex = inPutArray.IndexOf(0);
            float tempMinValue = float.Parse(inPutArray[0].ToString());
            for (byte i = 0; i < inPutArray.Count; i++)
            {
                if (tempMinValue < float.Parse(inPutArray[i].ToString()))
                {
                    tempMinValue = float.Parse(inPutArray[i].ToString());
                    maxIndex = i;
                }

            }
            return double.Parse(inPutArray[maxIndex].ToString());
        }
        
        
        public double ChangeUwtoDbm(double uw)
        {
            return Getlog10(uw / 1000) * 10;
        }
        public double ChangeDbmtoUw(double dbm)
        {
            return System.Math.Pow(10,dbm/10)*1000 ;
        }
       
        public byte[] Uint16DataConvertoBytes(UInt16 inputdata)
        {
            ArrayList tempArrlist = new ArrayList();
            if (inputdata>255)
            {
                tempArrlist = ArrayList.Adapter(BitConverter.GetBytes(inputdata));
                tempArrlist.Reverse();
                return (byte[])tempArrlist.ToArray(typeof(byte));
            }
            else
            {
                byte[]tempDataArray=new byte[1];
                tempDataArray[0]=Convert.ToByte(inputdata);
                return tempDataArray;
            }
        }
        public bool MonotonicIncreasingfun(double[] a, int n)
        {
            if (n == 1)
                return true;
            if (n == 2)
                return a[n - 1] > a[n - 2];
            return MonotonicIncreasingfun(a, n - 1) && (a[n - 1] > a[n - 2]);
        }
        public bool MonotonicDecreasingfun(double[] a, int n)
        {
            if (n == 1)
                return true;
            if (n == 2)
                return a[n - 1] < a[n - 2];
            return MonotonicDecreasingfun(a, n - 1) && (a[n - 1] < a[n - 2]);
        }
       
      /// <summary>
        // the filename will be find in inputStructArray 
        // return the index in the array of inputStructArray 
        //if the inputStructArray are not contain filename return -1
      /// </summary>
      /// <param name="inputStructArray"></param>
      /// <param name="filename"> </param>
      /// <param name="index"></param>
      /// <returns></returns>
        public bool FindFileName(TestModeEquipmentParameters[] inputStructArray, string filename, out int index)
        {
            for (byte i = 0; i < inputStructArray.Length; i++)
            {
                if (inputStructArray[i].FiledName.ToUpper() == filename.ToUpper())
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;
        }
        public bool FindFileName(DriverStruct[] inputStructArray, string filename, out int index)
        {
            for (byte i = 0; i < inputStructArray.Length; i++)
            {
                if (inputStructArray[i].FiledName.ToUpper() == filename.ToUpper())
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;
        }
        public bool FindFileName(DutStruct[] inputStructArray, string filename, out int index)
        {
            for (byte i = 0; i < inputStructArray.Length; i++)
            {
                if (inputStructArray[i].FiledName.ToUpper() == filename.ToUpper())
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;
        }

      /// <summary>
      ///  
      /// </summary>
      /// <param name="inputArraylist">the arraylist will be segregate</param>
      /// <param name="segregatestring">the string array list weill be segregate with </param>
      /// <returns></returns>
        public string ArrayListToStringArraySegregateByPunctuations(ArrayList inputArraylist, string segregatestring)
        {
            if (inputArraylist ==null)
            {
                return null;
            }
            if (inputArraylist.Count == 0)
            {
                return null;
            }
            string[] tempStrArr = new string[inputArraylist.Count];
            for (byte j = 0; j < inputArraylist.Count; j++)
            {
                tempStrArr[j] = inputArraylist[j].ToString().Trim();
            }
            return string.Join(segregatestring.Trim(), tempStrArr);
        }
        public ArrayList StringtoArraylistDeletePunctuations(string inputString, char[] segregatechars)
        {
            string segregatestring = null;
            for (byte i = 0; i < segregatechars.Length; i++)
            {
                segregatestring += segregatechars[i];
            }
            if (inputString == null)
            {
                return null;
            }
            if (!inputString.Contains(segregatestring) || inputString.Length == 0)
            {
                return null;
            }
            return ArrayList.Adapter(inputString.Split(segregatechars));

        }
        public byte[] FloatToIEE754(object inputfloat)
        {
            try
            {
                float tempfloat = float.Parse(inputfloat.ToString());
                return BitConverter.GetBytes(tempfloat);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
            
        }
        public byte[] INT16To2Bytes(object inputint16)
        {

            byte[] tempbyte = new byte[2];
            ArrayList tempArrlist = new ArrayList();
            try
            {
                Int16 tempfloat = (Int16)(Convert.ToDouble(inputint16.ToString()));
                tempArrlist = ArrayList.Adapter(BitConverter.GetBytes(tempfloat));
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.ToString());
            }
           
            tempArrlist.Reverse();
            return (byte[])tempArrlist.ToArray(typeof(byte));            
        }
      /// <summary>
      /// 
      /// </summary>
        /// <param name="scale">decimalism=0;Octal=1;hexadecimal=2;Binary=3</param>
      /// <param name="ssegregatestring"></param>
      /// <param name="inputarry"></param>
      /// <returns></returns>
        public string ByteArraytoString(byte scale, string ssegregatestring,byte[] inputarry)
        {
            if (inputarry==null)
            {
                return null;
            }
            if (inputarry.Length==0)
            {
                return null;
            }
            string[] tempStrArr = new string[inputarry.Length];
            for (byte j = 0; j < inputarry.Length; j++)
            {
                //decimalism=0;Octal=1;hexadecimal=2;Binary=3
                switch (scale)
                {
                    case 1:
                        {
                            tempStrArr[j] ="O "+ Convert.ToString(inputarry[j], 10).ToUpper();
                            break;
                        }
                    case 2:
                        {
                            tempStrArr[j] = "0X " + Convert.ToString(inputarry[j], 16).ToUpper();
                            break;
                        }
                    case 3:
                        {
                            tempStrArr[j] = "B " + Convert.ToString(inputarry[j], 2).ToUpper();
                            break;
                        }
                        default:
                        {
                            tempStrArr[j] = "D " + Convert.ToString(inputarry[j], 10).ToUpper();
                            break;
                        }
                }
                
            }
            return string.Join(ssegregatestring.Trim(), tempStrArr);
        }
        public string ByteArraytoString(byte scale, string ssegregatestring, UInt16[] inputarry)
        {
            if (inputarry == null)
            {
                return null;
            }
            if (inputarry.Length == 0)
            {
                return null;
            }
            string[] tempStrArr = new string[inputarry.Length];
            for (byte j = 0; j < inputarry.Length; j++)
            {
                //decimalism=0;Octal=1;hexadecimal=2;Binary=3
                switch (scale)
                {
                    case 1:
                        {
                            tempStrArr[j] = "O " + Convert.ToString(inputarry[j], 10).ToUpper();
                            break;
                        }
                    case 2:
                        {
                            tempStrArr[j] = "0X " + Convert.ToString(inputarry[j], 16).ToUpper();
                            break;
                        }
                    case 3:
                        {
                            tempStrArr[j] = "B " + Convert.ToString(inputarry[j], 2).ToUpper();
                            break;
                        }
                    default:
                        {
                            tempStrArr[j] = "D " + Convert.ToString(inputarry[j], 10).ToUpper();
                            break;
                        }
                }

            }
            return string.Join(ssegregatestring.Trim(), tempStrArr);
        }
        public string ByteArraytoString(byte scale, string ssegregatestring, UInt32[] inputarry)
        {
            if (inputarry == null)
            {
                return null;
            }
            if (inputarry.Length == 0)
            {
                return null;
            }
            string[] tempStrArr = new string[inputarry.Length];
            for (byte j = 0; j < inputarry.Length; j++)
            {
                //decimalism=0;Octal=1;hexadecimal=2;Binary=3
                switch (scale)
                {
                    case 1:
                        {
                            tempStrArr[j] = "O " + Convert.ToString(inputarry[j], 10).ToUpper();
                            break;
                        }
                    case 2:
                        {
                            tempStrArr[j] = "0X " + Convert.ToString(inputarry[j], 16).ToUpper();
                            break;
                        }
                    case 3:
                        {
                            tempStrArr[j] = "B " + Convert.ToString(inputarry[j], 2).ToUpper();
                            break;
                        }
                    default:
                        {
                            tempStrArr[j] = "D " + Convert.ToString(inputarry[j], 10).ToUpper();
                            break;
                        }
                }

            }
            return string.Join(ssegregatestring.Trim(), tempStrArr);
        }
        public byte[] ObjectTOByteArray(object inputData, byte length, bool LittleEndian)
        {
            ArrayList tempArrlist = new ArrayList();
            double tempDouble = Convert.ToDouble(inputData);
            try
            {
                switch (length)
                {
                    case 0:
                        break;
                    case 1:
                        {
                            byte tempData = (byte)tempDouble;
                            byte[] tempDataArray = new byte[1] { tempData };
                            tempArrlist = ArrayList.Adapter(tempDataArray);
                        }

                        break;
                    case 2:
                        {
                            UInt16 tempDatau16 = (UInt16)tempDouble;
                            tempArrlist = ArrayList.Adapter(BitConverter.GetBytes(tempDatau16));
                        }
                        break;
                    case 4:
                        {
                            UInt32 tempDatau32 = (UInt32)tempDouble;
                            tempArrlist = ArrayList.Adapter(BitConverter.GetBytes(tempDatau32));
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            if (LittleEndian == false)
            {
                tempArrlist.Reverse();
            }

            return (byte[])tempArrlist.ToArray(typeof(byte));
        }
      

    }
}
