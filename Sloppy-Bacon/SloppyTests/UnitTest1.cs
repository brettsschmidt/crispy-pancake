using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sloppy_Bacon;

namespace SloppyTests
{
    [TestClass]
    public class BurntMatrixOpsTests
    {
        [TestMethod]
        public void BasicMatixTests()
        {
            //Matrixes are backwards?
            var matrix2 = new float[,] { { 2f, 3f, 4f, 5f }, { 6f, 7f, 8f, 9f } };
            var matrix1 = new float[,] { { 2f, 3f }, { 2f, 3f }, { 4f, 5f }, { 6f, 7f } };

            var resultM = new float[,] { { 22f, 22f, 38f, 54f }, { 27f, 27f, 47f, 67f }, { 32f, 32f, 56f, 80f }, { 37f, 37f, 65f, 93f } };

            Assert.AreEqual(2, matrix2.Rank);
            Assert.AreEqual(2, matrix1.GetLength(0));

            var resultT = BurntMatrixOps.MMult(matrix1, matrix2);



            Assert.AreEqual(resultT.Rank, resultM.Rank);
            Assert.AreEqual(resultM, resultT);


        }
    }
}
