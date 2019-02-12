using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liberated_Lox
{
    public class BurntMatrixOps
    {
        public static float[][] MatCreate(int rows, int cols)
        {
            float[][] result = new float[rows][];
            for(int i = 0; i < rows; i++)
            {
                result[i] = new float[cols];
            }
            return result;
        }

        public static float[][] MatFromArray(float[] arr, int rows, int cols)
        {
            float[][] result = MatCreate(rows, cols);
            int k = 0;
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    result[i][j] = arr[k++];
                }
            }
            return result;
        }

        public static float[][] MatCopy(float[][] mat)
        {
            var result = new float[mat.Length][];
            for(int i = 0; i < mat.Length; i++)
            {
                result[i] = new float[mat[i].Length];
                for (int j = 0; j < mat[i].Length; j++)
                {
                    result[i][j] = mat[i][j];
                }
            }
            return result;
        }

        public static float[][] MatTanh(float[][] m)
        {
            var rows = m.Length;
            var cols = m[0].Length;
            var result = MatCreate(rows, cols);
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    result[i][j] = Tanh(m[i][j]);
                }
            }
            return result;
        }

        public static float Tanh(float x)
        {
            if(x < -10.0)
            {
                return -1.0f;
            }
            else if(x > 10.0)
            {
                return 1.0f;
            }
            return (float)(Math.Tanh(x));
        }

        //Element-wise Multiplication: Hadamard Function
        public static float[][] MatHada(float[][] a, float[][] b)
        {
            var rows = a.Length;
            var cols = a[0].Length;
            float[][] result = MatCreate(rows, cols);
            for(var i = 0; i < rows; i++)
            {
                for(var j = 0; j < cols; j++)
                {
                    result[i][j] = a[i][j] * b[i][j];
                }
            }
            return result;
        }

        public static float[,] PointAdd(float[,] firstMat, float[,] secMat)
        {
            var newMat = new float[firstMat.GetLength(0), firstMat.Rank];
            if (firstMat.GetLength(0) == secMat.GetLength(0) && secMat.Rank == firstMat.Rank)
            {
                for (int i = 0; i < newMat.GetLength(0); i++)
                {
                    for (int o = 0; o < newMat.Rank; o++)
                    {
                        newMat[i, o] = firstMat[i, o] + secMat[i, o];
                    }
                }
            }
            return newMat;
        }


        public static float DotProduct(float[] firstMat, float[] secMat)
        {
            if (firstMat.Length != secMat.Length)
            {
                throw new Exception("Lengths are not equal.");
            }

            float product = 0;
            for (int i = 0; i < firstMat.Length; i++)
            {
                product = product + (firstMat[i] * firstMat[i]);
            }
            return product;

        }

        public static float[,] MMult(float[,] firstMat, float[,] secMat)
        {
            if (firstMat.Rank == secMat.GetLength(0))
            {
                var product = new float[firstMat.GetLength(0), secMat.Rank];
                for (int rows = 0; rows < product.GetLength(0); rows++)
                {
                    for (int col = 0; col < product.Rank; col++)
                    {
                        var curcell = 0f;
                        for (int i = 0; i < firstMat.Rank; i++)
                        {
                            curcell += firstMat[rows, i] * secMat[i, col];
                        }
                        product[rows, col] = curcell;
                    }
                }
                return product;
            }
            else
            {
                throw new Exception("Columns and rows did not match.");
            }

        }


    }
}
