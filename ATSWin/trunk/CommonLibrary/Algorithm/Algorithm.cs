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
            if (length == 2 && dimension == 2)
            {
                n = 2;
            }
            if (length <= 1)
            {
                n = 1;
            }
            //dimension次方程需要求 dimension+1个 系数
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
            if (length == 2 && dimension == 2)
            {
                double[] temp = ComputGauss(Guass, n);
                double[] coef = new double[temp.Length + 1];
                for (byte i = 0; i < temp.Length; i++)
                {
                    coef[i] = temp[i];
                }
                return coef;
            }
            if (length <= 1)
            {
                if (dimension == 1)
                {
                    double[] temp = ComputGauss(Guass, n);
                    double[] coef = new double[temp.Length + 1];
                    for (byte i = 0; i < temp.Length; i++)
                    {
                        coef[i] = temp[i];
                    }
                    return coef;
                }
                if (dimension == 2)
                {
                    double[] temp = ComputGauss(Guass, n);
                    double[] coef = new double[temp.Length + 2];
                    for (byte i = 0; i < temp.Length; i++)
                    {
                        coef[i] = temp[i];
                    }
                    return coef;
                }

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
                //if ((arr1[i] != 0 || n1 != 0) && (arr2[i] != 0 || n2 != 0))//屏蔽掉arr1[i]=0的情况
                if ((arr2[i] != 0 || n2 != 0))
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
            if (n == 1)
            {
                intercept = 0;
                slop = yList[0];
                return yList[0];
            }
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
            slop = α;
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
                if (inputStructArray[i].FiledName.ToUpper() == filename.ToUpper().Trim())
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
                return "";
            }
            if (inputArraylist.Count == 0)
            {
                return "";
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
            string segregatestring = "";
            for (byte i = 0; i < segregatechars.Length; i++)
            {
                segregatestring += segregatechars[i];
            }
            if (inputString == null)
            {
                return null;
            }
            if (inputString.Length == 0)
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
        public byte[] ObjectTOByteArray(object inputData, byte length, bool LittleEndian=false)
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
       
        public bool IBiasMAtoIBiasDAC(string PN, float iBiasMA, UInt32 IModDAC, out UInt32 IBiasDAC)
        {
            PN = PN.ToUpper().Trim();
            switch (PN)
            {
                case "TR_CGR4_N01":
                case "TR_CGR4_V4":
                case "TR-PSM":
                case "TR_CGR4_N00":
                    {
                        if (iBiasMA < 0)
                        {
                            IBiasDAC = 0;
                            return false;
                        }
                        IBiasDAC = Convert.ToUInt32((iBiasMA - (IModDAC / 1023.0 * 48)) / 78 * 1023.0);

                    }
                    break;
                case "TR_SFP28_LR":
                    {
                        if (iBiasMA < 0)
                        {
                            IBiasDAC = 0;
                            return false;
                        }
                        IBiasDAC = Convert.ToUInt32((iBiasMA - (IModDAC / 1023.0 * 48)) / 78 * 1023.0);

                    }
                    break;
                case "TR_CSR4":
                    {
                        if (iBiasMA < 2.5)
                        {
                            IBiasDAC = 0;
                            return false;
                        }
                        IBiasDAC = Convert.ToUInt32((iBiasMA - 2.5) * 10);

                    }
                    break;
                case "SR4-V2":
                    {
                        if (iBiasMA < 2.5)
                        {
                            IBiasDAC = 0;
                            return false;
                        }
                        IBiasDAC = Convert.ToUInt32((iBiasMA - 2.5) * 10);

                    }
                    break;
                default:
                    {
                        if (iBiasMA < 0)
                        {
                            IBiasDAC = 0;
                            return false;
                        }
                        //IBiasDAC = Convert.ToUInt32((iBiasMA - (IModDAC / 1023.0 * 48)) / 78 * 1023.0);
                        IBiasDAC = Convert.ToUInt32(iBiasMA);
                    }
                    break;

            }
            return true;
        }
        public ArrayList Parse(string expression)
        {
            char ch, ch1;
            Stack myStack = new Stack();
            ArrayList bArray = new ArrayList();
            bArray.Clear();
            StringBuilder tempSring = new StringBuilder();
            tempSring.Clear();
            expression = expression.Trim();
            expression = expression.Replace(" ", "");
            expression = expression.Replace("[", "(");
            expression = expression.Replace("]", ")");
            expression = expression.Replace("{", "(");
            expression = expression.Replace("}", ")");
            char[] A = expression.ToCharArray(); //将字符串转成字符数组，要注意的是，不能有大于10的数存在

            UInt32 OperandCount = 0;
            for (int i = 0; i < A.Length; i++)
            {
                ch = A[i];
                if (i == A.Length - 1 && IsOperand(ch))
                {
                    OperandCount++;
                    tempSring.Append(A, (int)(i - OperandCount + 1), (int)OperandCount);
                    IsOperand(ch);
                    string temstr = Convert.ToString(tempSring);
                    bArray.Add(temstr);
                    tempSring.Clear();
                    OperandCount = 0;
                    continue;
                }
                if (IsOperand(ch) && i <= A.Length - 1) //如果是操作数，直接放入B中
                {
                    OperandCount++;
                    continue;
                }
                else
                {
                    if (ch == '(') //如果是'('，将它放入堆栈中
                        myStack.Push(ch);
                    else if (ch == ')') //如果是')'
                    {
                        tempSring.Append(A, (int)(i - OperandCount), (int)OperandCount);
                        string temstr = Convert.ToString(tempSring);
                        bArray.Add(temstr);
                        tempSring.Clear();
                        OperandCount = 0;
                        while (!IsEmpty(myStack)) //不停地弹出堆栈中的内容，直到遇到'('
                        {
                            ch = (char)myStack.Pop();
                            if (ch == '(')
                                break;
                            else
                                bArray.Add(ch);
                        }
                    }
                    else //既不是'('，也不是')'，是其它操作符，比如+, -, *, /之类的
                    {
                        tempSring.Append(A, (int)(i - OperandCount), (int)OperandCount);
                        string temstr = Convert.ToString(tempSring);
                        bArray.Add(temstr);
                        tempSring.Clear();
                        OperandCount = 0;
                        if (!IsEmpty(myStack))
                        {
                            do
                            {
                                ch1 = (char)myStack.Pop();//弹出栈顶元素
                                if (Priority(ch) > Priority(ch1)) //如果栈顶元素的优 先级小于读取到的操作符
                                {
                                    myStack.Push(ch1);//将栈顶元素放回堆栈
                                    myStack.Push(ch);//将读取到的操作符放回堆栈
                                    break;
                                }
                                else//如果栈顶元素的优先级比较高或者两者相等时
                                {
                                    bArray.Add(ch1);
                                    if (IsEmpty(myStack))
                                    {
                                        myStack.Push(ch); //将读取到的操作符压入堆栈中
                                        break;
                                    }
                                }
                            } while (!IsEmpty(myStack));
                        }
                        else //如果堆栈为空，就把操作符放入堆栈中
                        {
                            myStack.Push(ch);
                        }
                    }
                }
            }

            while (!IsEmpty(myStack))
                bArray.Add(myStack.Pop());
            for (int i = 0; i < bArray.Count; i++)
            {
                Console.WriteLine(bArray[i].ToString());
            }
            return bArray;
        }
        //对两个值利用运算符计算结果
        public double GetValue(string op, double ch1, double ch2)
        {
            switch (op)
            {
                case "+":
                    return ch2 + ch1;
                case "-":
                    return ch2 - ch1;
                case "*":
                    return ch2 * ch1;
                case "/":
                    return ch2 / ch1;
                default:
                    return 0;
            }
        }

        //判断堆栈是否为空
        public bool IsEmpty(Stack st)
        {
            return st.Count == 0 ? true : false;
        }
        public bool searchRealValue(double ibaisorimod, UInt32 ibiasdac, UInt32 imoddac, string serchStr, out string expression)
        {
            string outStr = "";
            expression = serchStr;
            if (serchStr == "" || serchStr == " ")
            {
                outStr = "";
                return false;
            }
            else
            {
                {
                    serchStr = serchStr.Replace(" ", "");
                    serchStr = serchStr.ToUpper();
                    expression = serchStr;
                    if (!serchStr.Contains("IBIAS(MA)") && !serchStr.Contains("IMOD(MA)"))
                    {
                        MessageBox.Show("There are not have\"IBIAS(MA)\" IMOD(MA)");
                        expression = serchStr;
                        return false;

                    }
                    if (serchStr.Contains("IBIAS(MA)"))
                    {
                        outStr = Convert.ToString(ibaisorimod);
                        serchStr = serchStr.Replace("IBIAS(MA)", outStr);
                        expression = serchStr;
                    }

                    if (serchStr.Contains("IMOD(MA)"))
                    {
                        outStr = Convert.ToString(ibaisorimod);
                        serchStr = serchStr.Replace("IMOD(MA)", outStr);
                        expression = serchStr;
                    }
                    if (serchStr.Contains("IMODDAC"))
                    {
                        outStr = Convert.ToString(imoddac);
                        serchStr = serchStr.Replace("IMODDAC", outStr);
                        expression = serchStr;
                    }
                    if (serchStr.Contains("IBIASDAC"))
                    {
                        outStr = Convert.ToString(ibiasdac);
                        serchStr = serchStr.Replace("IBIASDAC", outStr);
                        expression = serchStr;
                    }
                }
                return true;
            }
        }
        //判断是否是操作数
        public bool IsOperand(char ch)
        {
            char[] operators = { '+', '-', '*', '/', '(', ')' };
            for (int i = 0; i < operators.Length; i++)
                if (ch == operators[i])
                    return false;
            return true;
        }
        public bool IsOperandStr(string ch)
        {
            string[] operators = { "+", "-", "*", "/", "(", ")" };
            for (int i = 0; i < operators.Length; i++)
                if (ch == operators[i])
                    return false;
            return true;
        }
        //返回运算符的优先级
        public int Priority(char ch)
        {
            int priority;

            switch (ch)
            {
                case '+':
                    priority = 1;
                    break;
                case '-':
                    priority = 1;
                    break;
                case '*':
                    priority = 2;
                    break;
                case '/':
                    priority = 2;
                    break;
                default:
                    priority = 0;
                    break;
            }

            return priority;
        }
        public double Calculate(ArrayList bArray, double ibais, UInt32 ibiasdac, UInt32 imoddac)
        {
            double no1, no2, ret;
            Stack myStack = new Stack();
            myStack.Clear();
            string ch;
            for (int i = 0; i < bArray.Count; i++)
            {
                if (bArray[i] == null || Convert.ToString(bArray[i]) == "")
                {
                    bArray.RemoveAt(i);
                }
            }
            string[] aStr = new string[bArray.Count];
            for (int i = 0; i < bArray.Count; i++)
            {
                aStr[i] = Convert.ToString(bArray[i]);
                aStr[i] = aStr[i].Replace(" ", "");
                aStr[i] = aStr[i].ToUpper();
            }
            myStack.Clear();

            for (int i = 0; i < aStr.Length; i++)
            {
                ch = aStr[i];
                if (IsOperandStr(ch))//如果是操作数，直接 压入栈
                {
                    myStack.Push(ch);
                }
                else //如果是操作符，就弹出两个数字来进行运算
                {
                    string str1 = Convert.ToString(myStack.Pop());
                    no1 = Convert.ToDouble(str1);
                    string str2 = Convert.ToString(myStack.Pop());
                    no2 = Convert.ToDouble(str2);
                    ret = GetValue(ch, no1, no2);
                    myStack.Push(ret);//将结果压入栈
                }
            }

            return Convert.ToDouble(myStack.Pop());//弹出最后的运算结果
        }
        public double AnalyticalExpression(string expression, double ibaisorimod, UInt32 ibiasdac = 0, UInt32 imoddac = 0)
        {
            searchRealValue(ibaisorimod, ibiasdac, imoddac, expression, out expression);
            ArrayList temArrlist = Parse(expression);
            double temResult = Calculate(temArrlist, ibaisorimod, ibiasdac, imoddac);
            return temResult;
        }
        public double CalculateOMA(double PVG, double ER)
        {
            double oma = 0;
            try
            {
                ER = Math.Pow(10, ER / 10.0);
                PVG = ChangeDbmtoUw(PVG);
                oma = 2 * PVG * (ER - 1) / (ER + 1);
                oma = ChangeUwtoDbm(oma);
                if (double.IsInfinity(oma) || double.IsNaN(oma))
                {
                    oma = -100000;
                }
                return oma;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return oma;
            }


        }
        public double ISNaNorIfinity(double input)
        {
            if (double.IsInfinity(input) || double.IsNaN(input))
            {
                return -100000;
            } 
            else
            {
                return input;
            }
        }
        public bool FindDataInDataTable(DataTable inputDT, string filterExpression,out DataRow[] drData)
        {
            try
            {
                drData = inputDT.Select(filterExpression);
                if (drData.Length == 0 || drData==null)
                {
                    return false;
                }
                return true;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
           
            
        }
        public double CalculateFromOMAtoDBM(double oma, double ER)
        {
            double dbm = 0;
            try
            {
                ER = Math.Pow(10, ER / 10.0);
                oma = ChangeDbmtoUw(oma);
                dbm = oma * (ER + 1) / (2 * (ER - 1));
                dbm = ChangeUwtoDbm(dbm);
                if (double.IsInfinity(oma) || double.IsNaN(oma))
                {
                    dbm = -100000;
                }
                return dbm;
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                return oma;
            }


        }
        public SpecTableStruct[] FindDataInDataTable(DataTable inputDT, SpecTableStruct[] inputSList,string currentChannel)
        {
            string selectStr1 = "";
            string selectStr2 = "";  
            DataRow[] drData;
           
            try
            {
                for (int i = 0; i < inputSList.Length; i++)
                {
                    selectStr1 = "FullName='" + inputSList[i].ItemName + "'" + " AND " + "Channel='" + currentChannel + "'";
                    selectStr2 = "FullName='" + inputSList[i].ItemName + "'" + " AND " + "Channel='0'";
                    if (FindDataInDataTable(inputDT, selectStr1, out drData))
                    {
                        if (drData.Length==1)
                        {
                            inputSList[i].TypicalValue = Convert.ToDouble(drData[0]["Typical"]);
                            inputSList[i].MinValue = Convert.ToDouble(drData[0]["SpecMin"]);
                            inputSList[i].MaxValue = Convert.ToDouble(drData[0]["SpecMax"]);
                        }
                        else
                        {
                            return null;
                        }
                       
                    }
                    else if (FindDataInDataTable(inputDT, selectStr2, out drData))
                    {
                        if (drData.Length == 1)
                        {
                            inputSList[i].TypicalValue = Convert.ToDouble(drData[0]["Typical"]);
                            inputSList[i].MinValue = Convert.ToDouble(drData[0]["SpecMin"]);
                            inputSList[i].MaxValue = Convert.ToDouble(drData[0]["SpecMax"]);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                   
                }
                return inputSList;
            }
            catch (System.Exception ex)
            {
            	throw ex;
            }
        }
        public bool GetSpec(DataTable dtSpec, string FiledName,byte CurrentChannel, out Double Max, out double Min)
        {
            Max = 32768;
            Min = -32767;
            byte Channel;
            // "FullName='" + FiledName + "'",
            try
            {
                DataTable dtSelect;
                if (GetDataTable(dtSpec, "FullName='" + FiledName + "'", out dtSelect))// 寻找项目名称是否存在输出结构体中
                {
                    if (dtSelect.Rows.Count == 1)
                    {
                        Channel = Convert.ToByte(dtSelect.Rows[0]["Channel"]);

                        if (Channel == 0 || Channel == CurrentChannel)
                        {
                            Min = Convert.ToDouble(dtSelect.Rows[0]["SpecMin"]);
                            Max = Convert.ToDouble(dtSelect.Rows[0]["SpecMax"]);
                        }

                    }
                    else
                    {
                        for (int i = 0; i < dtSelect.Rows.Count; i++)
                        {
                            Channel = Convert.ToByte(dtSelect.Rows[i]["Channel"]);

                            if (Channel == 0 || Channel == CurrentChannel)
                            {
                                Min = Convert.ToDouble(dtSelect.Rows[i]["SpecMin"]);
                                Max = Convert.ToDouble(dtSelect.Rows[i]["SpecMax"]);
                            }
                        }
                    }


                }
            }
            catch
            {


            }
            finally
            {

            }
            if (Max != 32768 || Min != -32767)
            {
                return true;
            }
            else
            {
                return false;
            }




        }
        public bool GetSpec(DataTable dtSpec, string FiledName, byte CurrentChannel, out Double Max, out Double TypcalValue, out double Min)
        {
            Max = 32768;
            Min = -32767;
            TypcalValue = -32767;

            byte Channel;
            // "FullName='" + FiledName + "'",
            try
            {
                DataTable dtSelect;
                if (GetDataTable(dtSpec, "FullName='" + FiledName + "'", out dtSelect))// 寻找项目名称是否存在输出结构体中
                {
                    if (dtSelect.Rows.Count == 1)
                    {
                        Channel = Convert.ToByte(dtSelect.Rows[0]["Channel"]);

                        if (Channel == 0 || Channel == CurrentChannel)
                        {
                            Min = Convert.ToDouble(dtSelect.Rows[0]["SpecMin"]);
                            Max = Convert.ToDouble(dtSelect.Rows[0]["SpecMax"]);
                            TypcalValue = Convert.ToDouble(dtSelect.Rows[0]["Typical"]);
                        }

                    }
                    else
                    {
                        for (int i = 0; i < dtSelect.Rows.Count; i++)
                        {
                            Channel = Convert.ToByte(dtSelect.Rows[i]["Channel"]);

                            if (Channel == 0 || Channel == CurrentChannel)
                            {
                                Min = Convert.ToDouble(dtSelect.Rows[i]["SpecMin"]);
                                Max = Convert.ToDouble(dtSelect.Rows[i]["SpecMax"]);
                                TypcalValue = Convert.ToDouble(dtSelect.Rows[0]["Typical"]);
                            }
                        }
                    }


                }
            }
            catch
            {


            }
            finally
            {

            }
            if (Max != 32768 || Min != -32767)
            {
                return true;
            }
            else
            {
                return false;
            }




        }
       
        public bool GetDataTable(DataTable inputDT, string filterExpression, out DataTable dt)
        {
            try
            {

                dt = inputDT.Clone();
                DataRow[] drData = inputDT.Select(filterExpression);


                for (int i = 0; i <= drData.Length - 1; i++)
                {
                    dt.ImportRow((DataRow)drData[i]);
                }
                return true;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }


        }
       
        /// <summary>
        /// 由小到大排序数组
        /// </summary>
        /// <param name="ListData">要排序的数组</param>
        /// <returns></returns>
        public Double[] SortingArray(Double[] ListData)
        {
            for (int i = 0; i < ListData.Length;i++ )
            {
                Double t = ListData[i];
                 int  j=i;
                while((j>0)&&(ListData[j-1]>t))
                {
                    ListData[j] = ListData[j - 1];
                    --j;
                }
                ListData[j] = t;
            }
            return ListData;
        }

        /// <summary>
        /// 计算RxResponsivity
        /// </summary>
        /// <param name="RxInputPower">RX入射光</param>
        /// <param name="RxPowerAdc">RxPowerAdc</param>
        /// <param name="URef">参考电压</param>
        /// <param name="Rrssi">采样电阻</param>
        /// <param name="Resolution">数位分辨率</param>
        /// <param name="Ratio">电压放大比例</param>
        /// <returns></returns>
        public Double CalculateRxResponsivity(Double RxInputPower,Double RxPowerAdc,double URef,double Rrssi,double Resolution,double Ratio)
        {
            double Rxinput_uw = ChangeDbmtoUw(RxInputPower);
             Double Responsivity=0;
             double Urssi = RxPowerAdc*Ratio * URef / (Math.Pow(2, Resolution) - 1);//V
             double Irssi = (1E+6)*Urssi / Rrssi;//Uint  UA

             Responsivity = Irssi / Rxinput_uw;
          //   Responsivity = (RxPowerAdc / 1996.3125 )/ Math.Pow(10, (RxInputPower / 10));
             return Responsivity;
        }
       
        /// <summary>
        /// DAC是否需要进行位处理
        /// </summary>
        /// <param name="length">字节长度</param>
        /// <param name="StartBit">起始位</param>
        /// <param name="EndBit">结束位</param>
        /// <returns>True=处理;falas=不处理</returns>
        public bool BitNeedManage(int length, int StartBit, int EndBit)
        {
            int Bitlength = EndBit - StartBit + 1;

            if (length * 8 > Bitlength)// 位数不够,拼凑而成
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 对于要写入DAC的值进行位计算
        /// </summary>
        /// <param name="WriteData">要写的数值</param>
        /// <param name="ReadData">已经从寄存器读出的数值</param>
        /// <param name="length">字节长度</param>
        /// <param name="StartBit">起始位</param>
        /// <param name="EndBit">结束位</param>
        /// <param name="MucType">Mcu类型,1:8bit,2:16bit</param>
        /// <returns></returns>
        public int WriteJointBitValue(int WriteData, int ReadData, int length, int StartBit, int EndBit,int MucType=1)
        {

            //  BitManage( length, StartBit, EndBit);
            int A = 0;

            for (int i = 0; i < length * 8 * MucType; i++)
            {
                if (i < StartBit || i > EndBit)//如果是在它的位置之外,那么全部写0
                {
                    A += Convert.ToInt32(Math.Pow(2, i));

                }
            }

            int b = ReadData & A;//吧要写的bit写0

            int c = b + WriteData * Convert.ToInt32(Math.Pow(2, StartBit));

            return c;
        }
        /// <summary>
        /// 对于读出DAC的值进行位计算
        /// </summary>
        /// <param name="ReadData">已经从寄存器读出的数值</param>
        /// <param name="length">字节长度</param>
        /// <param name="StartBit">起始位</param>
        /// <param name="EndBit">结束位</param>
        /// <param name="MucType">Mcu类型,1:8bit,2:16bit</param>
        /// <returns></returns>
         public int ReadJointBitValue(int ReadData, int length, int StartBit, int EndBit,int MucType=1)
        {

            //  BitManage( length, StartBit, EndBit);
            int A = 0;

            for (int i = 0; i < length * 8 * MucType; i++)
            {
                if (i >= StartBit && i <= EndBit)//如果是在它的位置之外,那么全部写0
                {
                    A += Convert.ToInt32(Math.Pow(2, i));

                }
            }

            int b = ReadData & A;//吧要写的bit写0

            int c = b / Convert.ToInt32(Math.Pow(2, StartBit));

            return c;
        }



        /// <summary>
        /// 查找配置参数
        /// </summary>
        /// <param name="InputList">输入参数数组</param>
        /// <param name="Strfield">RxPowerAdc</param>
        /// <param name="flagNum">True:表示数据;false:表示字符</param>
        /// <param name="Rrssi">采样电阻</param>
        /// <param name="Resolution">数位分辨率</param>
        /// <param name="Ratio">电压放大比例</param>
        /// <returns></returns>
        public bool Getconf(TestModeEquipmentParameters[] InputList, string Strfield, bool flagNum, out string goal)
        {
            goal = null;
            int index;

            if (FindFileName(InputList, Strfield, out index))
            {
                if (flagNum)
                {
                    double temp = Convert.ToDouble(InputList[index].DefaultValue);

                    if (double.IsInfinity(temp) || double.IsNaN(temp))
                    {

                        return false;
                    }
                    else
                    {
                        goal = InputList[index].DefaultValue;

                    }
                }
                else
                {
                    goal = InputList[index].DefaultValue;
                }

                return true;
            }

            return false;
        }


        /// <summary> 
        /// 单开各通道，求系数矩阵的逆，coeffMatrix * single = Y 求得coeffMatrix = Y*（single的逆）
        /// </summary> 
        /// <param name="singleChEnable">单开各通道，记录相应ADC值的矩阵，矩阵中各值已减去Offset值</param> 
        /// <param name="method">方法选择，0：通用方法；1：克莱姆法则</param> 
        /// <returns></returns> 
        public static double[,] CalculateCoeff(double[,] singleChEnable, byte method)
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
        public static int[] CalculateY(double[,] coeff, double[] allChEnable, byte method)
        {
            if (method == 0)
            {
                double[] buffMatrixY = MatrixMath.mult(coeff, allChEnable);
                int[] newMatrixY = new int[buffMatrixY.Length];
                for (int i = 0; i < buffMatrixY.Length; i++)
                {
                    newMatrixY[i] = (int)buffMatrixY[i];
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

            double D = MatrixMath.determinant(GG);

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
            Di = MatrixMath.determinant(G);
            double Yi = Di / D;

            return Yi;
        }

           ////
             //if (algorithm.FindFileName(InformationList, "WavelengthDacMax", out index))
             //           {
             //               double temp = Convert.ToDouble(InformationList[index].DefaultValue);

             //               if (double.IsInfinity(temp) || double.IsNaN(temp))
             //               {
             //                   logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
             //                   return false;
             //               }
             //               else
             //               {
             //                   if (temp > 0)
             //                   {
             //                       temp = -temp;
             //                   }
             //                   testBerStruct.CsenStartingRxPwr = temp;
             //               }



             //           }
           ////


  }
}
