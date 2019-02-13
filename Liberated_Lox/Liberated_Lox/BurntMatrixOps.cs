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
            for (int i = 0; i < rows; i++)
            {
                result[i] = new float[cols];
            }
            return result;
        }

        public static float[][] MatFromArray(float[] arr, int rows, int cols)
        {
            float[][] result = MatCreate(rows, cols);
            int k = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i][j] = arr[k++];
                }
            }
            return result;
        }
        public static float[][] MatProd(float[][] a, float[][] b)
        {
            int aRows = a.Length;
            int aCols = a[0].Length;
            int bRows = b.Length;
            int bCols = b[0].Length;
            if (aCols != bRows)
                throw new Exception("ACol != BRows");
            float[][] result = MatCreate(aRows, bCols);

            Parallel.For(0, aRows, i =>
            {
                for(int j = 0; j < bCols; j++)
                {
                    for(int k = 0; k < aCols; k++)
                    {
                        result[i][j] += a[i][k] * b[k][j];
                    }
                }
            }
            );
           

            return result;
        }

        public static float[][] MatCopy(float[][] mat)
        {
            var result = new float[mat.Length][];
            for (int i = 0; i < mat.Length; i++)
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
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i][j] = Tanh(m[i][j]);
                }
            }
            return result;
        }

        public static float Tanh(float x)
        {
            if (x < -10.0)
            {
                return -1.0f;
            }
            else if (x > 10.0)
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
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    result[i][j] = a[i][j] * b[i][j];
                }
            }
            return result;
        }

        public static float[][] MatSig(float[][] m)
        {
            // element-wise sigmoid
            int rows = m.Length; int cols = m[0].Length;

            float[][] result = MatCreate(rows, cols);
            for (int i = 0; i < rows; ++i) // each row
                for (int j = 0; j < cols; ++j) // each col
                    result[i][j] = Sigmoid(m[i][j]);
            return result;
        }

        public static float Sigmoid(float x)
        {
            if (x < -10.0) return 0.0f;
            else if (x > 10.0) return 1.0f;
            return (float)(1.0 / (1.0 + Math.Exp(-x)));
        }
        public static float[][] MatSum(float[][] a, float[][] b)
        {
            int rows = a.Length; int cols = a[0].Length;

            float[][] result = MatCreate(rows, cols);
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    result[i][j] = a[i][j] + b[i][j];
            return result;
        }

        public static float[][] MatSum(float[][] a, float[][] b, float[][] c)
        {
            int rows = a.Length; int cols = a[0].Length;

            float[][] result = MatCreate(rows, cols);
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    result[i][j] = a[i][j] + b[i][j] + c[i][j];
            return result;
        }
        public static void MatPrint(float[][] Mat, int dec, bool nl)
        {
            for (int i = 0; i < Mat.Length; ++i)
            {
                for (int j = 0; j < Mat[0].Length; ++j)
                {
                    Console.Write(Mat[i][j].ToString("F" + dec) + " ");
                }
                Console.WriteLine("");
            }
            if (nl == true) Console.WriteLine("");
        }

        public static float[][][] ComputeOutputs(float[][] xt, float[][] h_prev, float[][] c_prev,
      float[][] Wf, float[][] Wi, float[][] Wo, float[][] Wc,
      float[][] Uf, float[][] Ui, float[][] Uo, float[][] Uc,
      float[][] bf, float[][] bi, float[][] bo, float[][] bc)
        {
            float[][] ft = MatSig(MatSum(MatProd(Wf, xt), MatProd(Uf, h_prev), bf));
            float[][] it = MatSig(MatSum(MatProd(Wi, xt), MatProd(Ui, h_prev), bi));
            float[][] ot = MatSig(MatSum(MatProd(Wo, xt), MatProd(Uo, h_prev), bo));
            float[][] ct = MatSum(MatHada(ft, c_prev),
              MatHada(it, MatTanh(MatSum(MatProd(Wc, xt), MatProd(Uc, h_prev), bc))));
            float[][] ht = MatHada(ot, MatTanh(ct));

            float[][][] result = new float[2][][];
            result[0] = MatCopy(ht);
            result[1] = MatCopy(ct);
            return result;
        }


    }
}
