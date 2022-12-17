using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using REG_MARK_LIB;

namespace REG_MARK_LIB_Tests
{
    [TestClass]
    public class RegNumberSeriesTests
    {
        [TestMethod]
        public void TestCreate()
        {
            var number = new RegNumberSeries("aab");
            Assert.AreEqual("aab", number.ToString());
        }

        [TestMethod]
        public void TestOperatorAdd()
        {
            var number = new RegNumberSeries("aaa");
            Assert.AreEqual("aab", (number + 1).ToString());
            Assert.AreEqual("aba", (number + 12).ToString());
        }
    }
}
