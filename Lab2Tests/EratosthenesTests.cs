using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Lab2.Tests
{
    [TestClass()]
    public class EratosthenesTests
    {
        Eratosthenes eAlgorithm;

        [TestInitialize]
        public void Initialize()
        {
            eAlgorithm = new Eratosthenes();
        }

        [TestMethod()]
        [DataRow(2, new bool[] { false, false, true })]
        [DataRow(5, new bool[] { false, false, true, true, false, true })]
        [DataRow(7, new bool[] { false, false, true, true, false, true, false, true })]
        public void MarkPrimeNumbers_MarksPrime_WhenMaxIsMoreThanOne(int _max, bool[] _expected)
        {
            bool[] actual;

            actual = eAlgorithm.MarkPrimeNumbers(_max);

            CollectionAssert.AreEqual(_expected, actual);
        }

        [TestMethod()]
        [DataRow(1)]
        [DataRow(0)]
        [DataRow(-2)]
        public void MarkPrimeNumbers_ThrowsArgumentOutOfRangeException_WhenMaxIsLessEqualOne(int _max)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => eAlgorithm.MarkPrimeNumbers(_max));
        }


        [TestMethod()]
        public void GetMarkedNumberList_GetsPrimeNumsList_WhenPrimeNumArrayLengthIsMoreThanThree()
        {
            ArrayList expectedList = new ArrayList();
            ArrayList actualList   = new ArrayList();
            bool[]    markedArr    = new bool[] { false, false, true, true, false, true };

            expectedList.Add(2);
            expectedList.Add(3);
            expectedList.Add(5);

            actualList = eAlgorithm.GetMarkedNumberList(markedArr);

            var comparer = Comparer<object>.Create((a, b) => ((int)a).Equals((int)b) ? 0 : 1);

            CollectionAssert.AreEqual(actualList, expectedList, comparer);
        }

        [TestMethod()]
        [DataRow(new bool[] {false, false})]
        [DataRow(new bool[] {false})]
        [DataRow(new bool[] {})]
        public void GetMarkedNumberList_ThrowsArgumentException_WhenPrimeNumArrayLengthIsLessThanThree(bool[] _markedArr)
        {
            Assert.ThrowsException<ArgumentException>(() => eAlgorithm.GetMarkedNumberList(_markedArr));
        }
    }
}