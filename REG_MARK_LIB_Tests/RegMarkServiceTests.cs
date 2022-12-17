using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using REG_MARK_LIB;

namespace REG_MARK_LIB_Tests
{
    [TestClass]
    public class RegMarkServiceTests
    {
        [TestMethod]
        public void TestCheckMarkOleg()
        {
            var mark = RegMarkService.TranslateMark("М058КА716");
            Assert.AreEqual(mark, "m058ka716");
            Assert.IsTrue(RegMarkService.CheckMark(mark));
        }
    }
}
