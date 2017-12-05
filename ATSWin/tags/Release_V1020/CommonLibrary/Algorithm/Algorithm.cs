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
        public bool IBiasMAtoIBiasDAC(string PN,float iBiasMA,UInt32 IModDAC, out UInt32 IBiasDAC)
        {
            PN = PN.ToUpper().Trim();
            switch (PN)
            {
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
             default:
                    {
                        if (iBiasMA < 0)
                        {
                            IBiasDAC = 0;
                            return false;
                        }
                        IBiasDAC = Convert.ToUInt32((iBiasMA - (IModDAC / 1023.0 * 48)) / 78 * 1023.0);
                       
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
        public bool searchRealValue(double ibais, UInt32 ibiasdac, UInt32 imoddac, string serchStr, out string expression)
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
                    if (serchStr.Contains("IBIAS(MA)"))
                    {
                        outStr = Convert.ToString(ibais);
                        serchStr = serchStr.Replace("IBIAS(MA)", outStr);
                        expression = serchStr;
                    }
                    else
                    {
                        // MessageBox.Show("There are not have\"IBIAS(MA)\"");
                        expression = serchStr;
                        return false;
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
        public object AnalyticalExpression(string expression, double ibais, UInt32 ibiasdac, UInt32 imoddac)
        {
            searchRealValue(ibais, ibiasdac, imoddac, expression, out expression);
            ArrayList temArrlist = Parse(expression);
            double temResult = Calculate(temArrlist, ibais, ibiasdac, imoddac);
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
    }
}
