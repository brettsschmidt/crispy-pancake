using System;
using Liberated_Lox;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiberatedLoxTest
{
    [TestClass]
    public class BurntMatrixOpsTests
    {
        [TestMethod]
        public void MatProdTest()
        {
            var amat = GenerateAtMatrix();
            var bmat = GenerateBMatrix();
            var result = new float[4][];
            result[0] = new float[] { 40, 50, 60, 70};
            result[1] = new float[] { 54, 68, 82, 96};
            result[2] = new float[] { 68, 86, 104, 122};
            result[3] = new float[] { 82, 104, 126, 148 };

            var matProdR = BurntMatrixOps.MatProd(amat, bmat);
            

            for(int i = 0; i < matProdR.Length; i++)
            {
                for(int j = 0; j < matProdR[0].Length; j++)
                {
                    Assert.AreEqual(result[i][j], matProdR[i][j]);
                }
            }


        }

        private float[][] GenerateAtMatrix()
        {
            var amat = new float[4][];
            for (int i = 0; i < amat.Length; i++)
            {
                amat[i] = new float[4];
                for(int j = 0; j < amat[0].Length; j++)
                {
                    amat[i][j] = i + j +1;
                }
            }
            return amat;
        }

        private float[][] GenerateBMatrix()
        {
            var bmat = new float[4][];
            for (int i = 0; i < bmat.Length; i++)
            {
                bmat[i] = new float[4];
                for (int j = 0; j < bmat[0].Length; j++)
                {
                    bmat[i][j] = i + j + 2;
                }
            }
            return bmat;
        }

        

        [TestMethod]
        public void BasicMatixTests()
        {
            //Matrixes are backwards?
            


        }
    }
}
