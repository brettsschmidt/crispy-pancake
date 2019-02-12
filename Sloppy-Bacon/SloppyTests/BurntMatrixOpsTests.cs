using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sloppy_Bacon;

namespace SloppyTests
{
    [TestClass]
    public class BurntMatrixOpsTests
    {
        [TestMethod]
        public void PointAddTest()
        {
            var matrix2 = new float[,] { { 2f, 3f, 4f, 5f }, { 6f, 7f, 8f, 9f } };
            var matrix1 = new float[,] { { 2f, 3f, 2f, 3f }, { 4f, 5f, 6f, 7f } };

            var resultM = new float[,] { { 4f, 6f, 6f, 8f }, { 10f, 12f, 14f, 16f } };
            var resultT = BurntMatrixOps.PointAdd(matrix1, matrix2);

            Assert.AreEqual(resultT.Rank, resultM.Rank);
        }

        [TestMethod]
        public void PointMultTest()
        {
            //Matrixes are backwards?
            var matrix2 = new float[,] { { 2f, 3f, 4f, 5f }, { 6f, 7f, 8f, 9f } };
            var matrix1 = new float[,] { { 2f, 3f, 2f, 3f }, { 4f, 5f, 6f, 7f } };

            var resultM = new float[,] { { 4f, 9f, 8f, 15f }, { 24f, 35f, 48f, 63f } };
            var resultT = BurntMatrixOps.PointMult(matrix1, matrix2);

            Assert.AreEqual(resultT.Rank, resultM.Rank);

        }

        [TestMethod]
        public void BasicMatixTests()
        {
            //Matrixes are backwards?
            var matrix2 = new float[,] { { 2f, 3f, 4f, 5f }, { 6f, 7f, 8f, 9f } };
            var matrix1 = new float[,] { { 2f, 3f }, { 2f, 3f }, { 4f, 5f }, { 6f, 7f } };

            var resultM = new float[,] { { 22f, 27f, 32f, 37f }, { 22f, 27f, 32f, 37f }, { 38f, 47f, 56f, 65f }, { 54f, 67f, 80f, 93f } };

            Assert.AreEqual(2, matrix2.GetLength(0));
            Assert.AreEqual(2, matrix1.Rank);

            var resultT = BurntMatrixOps.MMult(matrix1, matrix2);


            Assert.AreEqual(resultT.Rank, resultM.Rank);
            for (int i = 0; i < resultT.Rank; i++)
            {
                for (int o = 0; o < resultT.GetLength(0); o++)
                {
                    Assert.AreEqual(resultM[o, i], resultT[o, i]);
                }
            }


        }
    }
}
