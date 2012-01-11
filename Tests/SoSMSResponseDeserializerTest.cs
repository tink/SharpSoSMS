using SharpSoSMS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass]
    public class SoSMSResponseDeserializerTest
    {
        private SoSMSResponseDeserializer deserializer;

        [TestMethod]
        public void ShoudDeserializeAMessageXml()
        {
            deserializer = new SoSMSResponseDeserializer(typeof(SoSMSMessage), XmlMockedResponses.Message());
            SoSMSMessage message = (SoSMSMessage) deserializer.Deserialize();
            
            Assert.AreEqual(1002, message.Id);
            Assert.AreEqual("Test", message.Text);
            Assert.AreEqual(3, message.Dispaches.Length);
            
            Assert.AreEqual("Processando", message.Dispaches[0].Status);
            Assert.AreEqual("(00) 0000-0000", message.Dispaches[0].PhoneNumber);
        }


        [TestMethod]
        public void ShouldReturnTheBalanceBasedOnXml()
        {
            deserializer = new SoSMSResponseDeserializer(typeof(SoSMSBalance), XmlMockedResponses.AccountBalance());
            SoSMSBalance balance = (SoSMSBalance) deserializer.Deserialize();
            Assert.AreEqual(106, balance.Value);
        }

        
    }
}
