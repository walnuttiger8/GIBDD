using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SB_UTILS;

namespace SB_UTILS_Tests
{
    [TestClass]
    public class TestFuncs
    {
        [TestMethod]
        public void TestComposition()
        {
            var a = Funcs.C((int x) => x).Then((int x) => (x + 2).ToString()).Then((s) => s + "_Haip").Then((string s) => s.ToLower()); 

            Assert.AreEqual(a.Call(1), "3_haip");
        }
    }
}
