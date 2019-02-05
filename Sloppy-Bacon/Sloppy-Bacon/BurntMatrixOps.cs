using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sloppy_Bacon
{
    public class BurntMatrixOps
    {

        public static float DotProduct(float[] firstMat, float[] secMat)
        {
            if(firstMat.Length != secMat.Length)
            {
                throw new Exception("Lengths are not equal.");
            }

            float product = 0;
            for(int i = 0; i < firstMat.Length; i++)
            {
                product = product + (firstMat[i] * firstMat[i]);
            }
            return product;

        }

        public static float[,] MMult(float[,] firstMat, float[,] secMat)
        {
            if(firstMat.Rank == secMat.GetLength(0))
            {
                var product = new float[firstMat.GetLength(0), secMat.Rank];
                for(int rows = 0; rows < product.GetLength(0); rows++)
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
