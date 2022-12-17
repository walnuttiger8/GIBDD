using Microsoft.VisualStudio.TestTools.UnitTesting;
using VIN_LIB;

namespace VIN_LIB_Tests
{
    [TestClass]
    public class TestVinService
    {
        [TestMethod]
        public void TestExampleVINCode()
        {
            string vin = "JHMCM56557C404453";
            var service = new VINService();

            Assert.IsTrue(service.CheckVIN(vin));
            Assert.AreEqual(service.GetVINCountry(vin), "Япония");
        }

        [TestMethod]
        public void TestOlegVINCode()
        {
            string vin = "WAUZZZ44ZLN073333";
            var service = new VINService();

            Assert.IsFalse(service.CheckVIN(vin));
            Assert.AreEqual(service.GetVINCountry(vin), "Германия");
            Assert.AreEqual(service.GetTransportYear(vin), 2020);
        }
    }
}
