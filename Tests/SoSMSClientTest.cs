using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpSoSMS;

namespace Tests
{

    [TestClass]
    public class SoSMSClientTest
    {
        public SoSMSClientTest()
        {
            client = new SoSMSClient();
        }

        private SoSMSClient client;

        [TestMethod]
        public void ShouldGetAccountBalance()
        {
            Assert.AreEqual(6, client.GetBalance().Value);
        }
    }
}
