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
            deserializer = new SoSMSResponseDeserializer(typeof(SoSMSMessage), MockMessageResponse());
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
            deserializer = new SoSMSResponseDeserializer(typeof(SoSMSBalance), MockAccountCreditsResponse());
            SoSMSBalance balance = (SoSMSBalance) deserializer.Deserialize();
            Assert.AreEqual(106, balance.Value);
        }

        protected string MockAccountCreditsResponse()
        {
            return @"<accountCredits>
                       <value>106</value>
                     </accountCredits>";
        }
        protected string MockMessageResponse()
        {
            return @"<message>
                      <id type=""integer"">1002</id>
                      <text>Test</text>
                      <message-count>3</message-count>
                      <message-dispaches>
                        <message-dispach>
                          <status>Processando</status>
                          <phone-number>(00) 0000-0000</phone-number>
                        </message-dispach>
                        <message-dispach>
                          <status>Processando</status>
                          <phone-number>(99) 9999-9999</phone-number>
                        </message-dispach>
                        <message-dispach>
                          <status>Processando</status>
                          <phone-number>(88) 8888-8888</phone-number>
                        </message-dispach>
                      </message-dispaches>
                    </message>";
        }
    }
}
