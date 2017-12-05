using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS_Framework
{
    public class MatrixMath
    {
        /**
         * 矩阵基本运算：加、减、乘、转置
         */
        private const int OPERATION_ADD = 1;
        private const int OPERATION_SUB = 2;
        private const int OPERATION_MUL = 4;

        /**
         * 矩阵加法
         * @param a 加数
         * @param b 被加数
         * @return 和
         */
        public static double[,] plus(double[,] a, double[,] b)
        {
            if (legalOperation(a, b, OPERATION_ADD))
            {
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        a[i, j] = a[i, j] + b[i, j];
                    }
                }
            }
            return a;
        }

        /**
         * 矩阵减法
         * @param a 减数
         * @param b 被减数
         * @return 差
         */
        public static double[,] substract(double[,] a, double[,] b)
        {
            if (legalOperation(a, b, OPERATION_SUB))
            {
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        a[i, j] = a[i, j] - b[i, j];
                    }
                }
            }
            return a;
        }

        /**
         * 判断矩阵行列是否符合运算
         * @param a 进行运算的矩阵
         * @param b 进行运算的矩阵
         * @param type 运算类型
         * @return 符合/不符合运算
         */
        private static bool legalOperation(double[,] a, double[,] b, int type)
        {
            bool legal = true;
            if (type == OPERATION_ADD || type == OPERATION_SUB)
            {
                if (a.GetLength(0) != b.GetLength(0) || a.GetLength(1) != b.GetLength(1))
                {
                    legal = false;
                }
            }
            else if (type == OPERATION_MUL)
            {
                if (a.GetLength(1) != b.GetLength(0))
                {
                    legal = false;
                }
            }
            return legal;
        }

        /**
         * 矩阵乘法
         * @param a 乘数
         * @param b 被乘数
         * @return 积
         */
        public static double[,] mult(double[,] a, double[,] b)
        {
            if (legalOperation(a, b, OPERATION_MUL))
            {
                double[,] result = new double[a.GetLength(0), b.GetLength(1)];
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    for (int j = 0; j < b.GetLength(1); j++)
                    {
                        result[i, j] = calculateSingleResult(a, b, i, j);
                    }
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        /**
         * 矩阵乘法
         * @param a 乘数
         * @param b 被乘数
         * @return 积
         */
        public static double[,] mult(double[,] a, int b)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] = a[i, j] * b;
                }
            }
            return a;
        }

        /**
         * 乘数矩阵的行元素与被乘数矩阵列元素积的和
         * @param a 乘数矩阵
         * @param b 被乘数矩阵
         * @param row 行
         * @param column 列
         * @return 值
         */
        private static double calculateSingleResult(double[,] a, double[,] b, int row, int column)
        {
            double result = 0.0;
            for (int k = 0; k < a.GetLength(1); k++)
            {
                result += a[row, k] * b[k, column];
            }
            return result;
        }

        /**
         * 矩阵的转置
         * @param a 要转置的矩阵
         * @return 转置后的矩阵
         */
        public static double[,] invert(double[,] a)
        {
            double[,] result = new double[a.GetLength(1), a.GetLength(0)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    result[j, i] = a[i, j];
                }
            }
            return result;
        }

        /**
         * 向量点乘
         * @param a 向量被乘数
         * @param b 向量乘数
         * @return 乘积
         */
        public static double dot(double[] a, double[] b)
        {
            double ret = 0;

            if (a.GetLength(0) != b.GetLength(0))
            {
                return Double.NaN;
            }

            for (int i = 0; i < a.GetLength(0); i++)
            {
                ret += a[i] * b[i];
            }
            return ret;
        }

        /**
         * 矩阵和向量之积
         * @param a 矩阵
         * @param b 向量
         * @return 乘积
         */
        public static double[] mult(double[,] a, double[] b)
        {
            int la = a.GetLength(0);
            int ra = 0;

            if (la != 0)
            {
                ra = a.GetLength(1);
            }

            double[] ret = new double[la];

            for (int i = 0; i < la; i++)
            {
                for (int j = 0; j < ra; j++)
                {
                    ret[i] += a[i, j] * b[j];
                }
            }
            return ret;
        }

        /**
         * 向量和矩阵之积
         * @param a 向量
         * @param b 矩阵
         * @return 乘积
         */
        public static double[] mult(double[] a, double[,] b)
        {
            int la = b.GetLength(0);
            int ra = 0;

            if (la != 0)
            {
                ra = b.GetLength(1);
            }
            double[] ret = new double[ra];

            for (int i = 0; i < ra; i++)
            {
                for (int j = 0; j < la; j++)
                {
                    ret[i] += a[j] * b[j, i];
                }
            }
            return ret;
        }

        /** 
         * 求可逆矩阵：伴随矩阵除以行列式值。伴随矩阵又代数余子式求得。 
         */
        /** 
         * 求传入的矩阵的逆矩阵 
         * @param value 需要转换的矩阵 
         * @return 逆矩阵 
         */
        public static double[,] getInverseMatrix(double[,] value, double result)
        {
            double[,] transferMatrix = new double[value.GetLength(0), value.GetLength(1)];  //
 
            for (int i = 0; i < value.GetLength(0); i++)
            {
                for (int j = 0; j < value.GetLength(1); j++)
                {
                    transferMatrix[j, i] = mathDeterminantCalculation(getNewMatrix(i, j, value));
                    if ((i + j) % 2 != 0)
                    {
                        transferMatrix[j, i] = -transferMatrix[j, i];
                    }
                    transferMatrix[j, i] /= result;
                }
            }
            return transferMatrix;
        }

        /*** 
         * 转换为上三角矩阵，求行列式的值
         * @param value 需要算的行列式 
         * @return 计算的结果 
         */
        public static double mathDeterminantCalculation(double[,] value)
        {
            if (value.GetLength(0) == 1)
            {
                //当行列式为1阶的时候就直接返回本身  
                return value[0, 0];
            }

            if (value.GetLength(0) == 2)
            {
                //如果行列式为二阶的时候直接进行计算  
                return value[0, 0] * value[1, 1] - value[0, 1] * value[1, 0];
            }

            //当行列式的阶数大于2时  
            double result = 1;
            double temp;
            for (int i = 0; i < value.GetLength(0); i++)
            {
                //检查数组对角线位置的数值是否是0，如果是零则对该数组进行调换，查找到一行不为0的进行调换  
                if (value[i, i] == 0)
                {
                    value = changeDeterminantNoZero(value, i, i);
                    result *= -1;
                }

                for (int j = 0; j < i; j++)
                {
                    //让开始处理的行的首位为0处理为三角形式  
                    //如果要处理的列为0则和自己调换一下位置，这样就省去了计算  
                    if (value[i, j] == 0)
                    {
                        continue;
                    }
                    //如果要是要处理的行是0则和上面的一行进行调换  
                    if (value[j, j] == 0)
                    {
                        for (int k = 0; k < value.GetLength(1); k++)
                        {
                            temp = value[i, k];
                            value[i, k] = value[i - 1, k];
                            value[i - 1, k] = temp;
                        }

                        result *= -1;
                        continue;
                    }

                    //将矩阵转换为上三角矩阵
                    double ratio = -(value[i, j] / value[j, j]);
                    double[] current = new double[value.GetLength(1)] ;
                    double[] front = new double[value.GetLength(1)] ;
                    for (int t = 0; t < value.GetLength(1); t++)
                    {
                        current[t] = value[i, t];
                        front[t] = value[j, t];
                    }

                    double[] data = addValue(current, front, ratio);

                    for (int t = 0; t < value.GetLength(1); t++)
                    {
                        value[i, t] = data[t];
                    }
                }
            }
            return mathValue(value, result);
        }

        /** 
         * 计算上三角矩阵行列式的结果 
         * @param value 
         * @return 
         */
        private static double mathValue(double[,] value, double result)
        {
            for (int i = 0; i < value.GetLength(0); i++)
            {
                //如果对角线上有一个值为0则全部为0，直接返回结果  
                if (value[i, i] == 0)
                {
                    return 0;
                }
                result *= value[i, i];
            }
            return result;
        }

        /*** 
         * 将i行之前的每一行乘以一个系数，使得从i行的第i列之前的数字置换为0 
         * @param currentRow 当前要处理的行 
         * @param frontRow i行之前的遍历的行 
         * @param ratio 要乘以的系数 
         * @return 将i行i列之前数字置换为0后的新的行 
         */
        private static double[] addValue(double[] currentRow, double[] frontRow, double ratio)
        {
            for (int i = 0; i < currentRow.GetLength(0); i++)
            {
                currentRow[i] += frontRow[i] * ratio;
            }
            return currentRow;
        }

        /** 
         * 指定列的位置是否为0，查找第一个不为0的位置的行进行位置调换，如果没有则返回原来的值 
         * @param determinant 需要处理的行列式 
         * @param line 要调换的行 
         * @param row 要判断的列 
         */
        private static double[,] changeDeterminantNoZero(double[,] determinant, int line, int column)
        {
            double temp;
            for (int i = line; i < determinant.GetLength(0); i++)
            {
                //进行行调换  
                if (determinant[i, column] != 0)
                {
                    for (int j = 0; j < determinant.GetLength(1); j++)
                    {
                        temp = determinant[line, j];
                        determinant[line, j] = determinant[i, j];
                        determinant[i, j] = temp;
                    }
                    return determinant;
                }
            }
            return determinant;
        }
        
        /** 
         * 转换为代数余子式 
         * @param row 行 
         * @param line 列 
         * @param matrix 要转换的矩阵 
         * @return 转换的代数余子式 
         */
        private static double[,] getNewMatrix(int row, int line, double[,] matrix)
        {
            double[,] newMatrix = new double[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
            int rowNum = 0, lineNum = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (i == row)
                {
                    continue;
                }
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (j == line)
                    {
                        continue;
                    }
                    newMatrix[rowNum, lineNum++ % (matrix.GetLength(1) - 1)] = matrix[i, j];
                }
                rowNum++;
            }
            return newMatrix;
        }

        /// <summary> 
        /// 递归计算行列式的值 
        /// </summary> 
        /// <param name="matrix">矩阵</param> 
        /// <returns></returns> 
        public static double determinant(double[,] matrix)
        {
            //二阶及以下行列式直接计算   
            if (Math.Sqrt(matrix.Length) == 0)
                return 0;
            else if (Math.Sqrt(matrix.Length) == 1)
                return matrix[0, 0];
            else if (Math.Sqrt(matrix.Length) == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }

            //对第一行使用“加边法”递归计算行列式的值  
            double sum = 0, sign = 1;
            for (int i = 0; i < Math.Sqrt(matrix.Length); i++)
            {
                double[,] matrixTemp = new double[Convert.ToInt32(Math.Sqrt(matrix.Length) - 1), Convert.ToInt32(Math.Sqrt(matrix.Length) - 1)];
                for (int j = 0; j < Math.Sqrt(matrixTemp.Length); j++)
                {
                    for (int k = 0; k < Math.Sqrt(matrixTemp.Length); k++)
                    {
                        matrixTemp[j, k] = matrix[j + 1, k >= i ? k + 1 : k];
                    }
                }
                sum += (matrix[0, i] * sign * determinant(matrixTemp));
                sign = sign * -1;
            }
            return sum;
        }
    }
}
