using System;
using System.Net;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpSoSMS;
using System.Net.Moles;
using System.Text;
using System.IO;

namespace Tests
{
    
    [TestClass]
    public class SoSMSClientTest
    {
        public SoSMSClientTest()
        {
            ConfigurationManager.AppSettings["SoSMS.AuthToken"] = "123456";
            client = new SoSMSClient();
        }

        private SoSMSClient client;

        [TestMethod]
        [HostType("Moles")]
        public void ShouldGetAccountBalance()
        {
            MockHttpResponse(XmlMockedResponses.AccountBalance(), HttpStatusCode.OK);
            Assert.AreEqual(106, client.GetBalance().Value);
        }

        [TestMethod]
        [HostType("Moles")]
        public void ShouldGetTheInfoAboutAMessage()
        {
            MockHttpResponse(XmlMockedResponses.Message(), HttpStatusCode.OK);

            SoSMSMessage message = client.GetMessage(1002);
            Assert.AreEqual("Test", message.Text);

            SoSMSMessageDispach dispach = message.Dispaches[0];
            Assert.AreEqual("Processando", dispach.Status);
            Assert.AreEqual("(00) 0000-0000", dispach.PhoneNumber);
        }

        [TestMethod]
        [HostType("Moles")]
        public void ShouldSendAMessageToContacts()
        {
            MockHttpResponse(XmlMockedResponses.Message(), HttpStatusCode.OK);

            SoSMSMessage message = client.SendMessage("Test", "Joao:0000000000,Luisa:9999999999,Mario:8888888888");
            Assert.IsNotNull(message.Id);
            Assert.AreEqual("Test", message.Text);

            SoSMSMessageDispach dispach = message.Dispaches[0];
            Assert.AreEqual("Processando", dispach.Status);
            Assert.AreEqual("(00) 0000-0000", dispach.PhoneNumber);

            dispach = message.Dispaches[1];
            Assert.AreEqual("Processando", dispach.Status);
            Assert.AreEqual("(99) 9999-9999", dispach.PhoneNumber);

            dispach = message.Dispaches[2];
            Assert.AreEqual("Processando", dispach.Status);
            Assert.AreEqual("(88) 8888-8888", dispach.PhoneNumber);
        }

        [TestMethod]
        [HostType("Moles")]
        public void ShouldSendAMessageToContactsUsingAArray()
        {
            MockHttpResponse(XmlMockedResponses.Message(), HttpStatusCode.OK);

            string[] contacts = {"Joao:0000000000","Luisa:9999999999","Mario:8888888888"};
            SoSMSMessage message = client.SendMessage("Test", contacts);
            Assert.IsNotNull(message.Id);
            Assert.AreEqual("Test", message.Text);

            SoSMSMessageDispach dispach = message.Dispaches[0];
            Assert.AreEqual("Processando", dispach.Status);
            Assert.AreEqual("(00) 0000-0000", dispach.PhoneNumber);

            dispach = message.Dispaches[1];
            Assert.AreEqual("Processando", dispach.Status);
            Assert.AreEqual("(99) 9999-9999", dispach.PhoneNumber);

            dispach = message.Dispaches[2];
            Assert.AreEqual("Processando", dispach.Status);
            Assert.AreEqual("(88) 8888-8888", dispach.PhoneNumber);
        }

        private void MockHttpResponse(string responseBody, HttpStatusCode statusCode)
        {
            var mockedWebResponse = new MHttpWebResponse();
            MHttpWebRequest.AllInstances.GetResponse = (x) =>
            {
                return mockedWebResponse;
            };

            mockedWebResponse.StatusCodeGet = () =>
            {
                return statusCode;
            };

            mockedWebResponse.GetResponseStream = () =>
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(responseBody);
                MemoryStream stream = new MemoryStream(byteArray);
                return stream;
            };
        }
    }
}

