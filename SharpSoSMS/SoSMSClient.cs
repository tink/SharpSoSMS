using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace SharpSoSMS
{
    /// <summary>
    /// The client responsible for communicating to the SoSMS service.
    /// </summary>
    public class SoSMSClient
    {
        private const string sosmsUri = "http://sosms.com.br/api/";
        private readonly SoSMSConfiguration configuration;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SoSMSClient"/> class.
        /// </summary>
        public SoSMSClient()
        {
            configuration = new SoSMSConfiguration();
        }

        /// <summary>
        ///  Returns the balance with the ammout of credits remaining in the account
        /// </summary>
        /// <returns>SoSMSBalance</returns>
        public SoSMSBalance GetBalance()
        {
            string url = sosmsUri + "/users/credits.xml?auth_token=" + configuration.AuthToken;
            return (SoSMSBalance) SendGetRequest(url, typeof(SoSMSBalance));
        }

        /// <summary>
        ///  Send a message to contacts
        /// </summary>
        /// <param name="text">The text message. Should be 140 chars at max.</param>
        /// <param name="contacts">
        ///  A string representing the contacts list in the format: "FirstContact:1188888888,SecondContact:2299999999".
        ///  The name and the phone number should be separated by ':', and contacts by ';'.
        /// </param>
        /// <returns>The created message</returns>
        public SoSMSMessage SendMessage(string text, string contacts)
        {
            string url = sosmsUri + "/messages.xml?auth_token=" + configuration.AuthToken;
            string parameters =  "message[text]=" + text;
                   parameters += "&message[contacts]=" + contacts;
            return (SoSMSMessage) SendPostRequest(url, parameters, typeof(SoSMSMessage));
        }

        /// <summary>
        ///  Send a message to each contact in the array
        /// </summary>
        /// <param name="text">The text message. Should be 140 chars at max.</param>
        /// <param name="contacts">
        ///  An array of strings representing the contacts list. Each contact should be in the format: "FirstContact:1188888888".
        ///  The name and the phone number should be separated by ':'.
        /// </param>
        /// <returns>The created message</returns>
        public SoSMSMessage SendMessage(string text, string[] contacts)
        {
            return SendMessage(text, String.Join(",", contacts));
        }

        /// <summary>
        ///  Returns the message
        /// </summary>
        /// <param name="id">The id of message.</param>
        /// <returns>SoSMSMessage</returns>
        public SoSMSMessage GetMessage(int id)
        {
            string url = sosmsUri + "/messages/" + id + ".xml?auth_token=" + configuration.AuthToken;
            return (SoSMSMessage) SendGetRequest(url, typeof(SoSMSMessage));
        }

        private Object SendGetRequest(string url, Type deserializerType)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            return GetResponseAndDeserializeIt(request, deserializerType);
        }

        private Object SendPostRequest(string url, string parameters, Type deserializerType)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(parameters);

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            return GetResponseAndDeserializeIt(request, deserializerType);
        }

        private Object GetResponseAndDeserializeIt(HttpWebRequest request, Type deserializerType)
        {
            HttpWebResponse response = null;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException exception)
            {
                response = exception.Response as HttpWebResponse;
            }

            return new SoSMSResponseDeserializer(deserializerType, ReadContent(response)).Deserialize();
        }

        private string ReadContent(HttpWebResponse response)
        {
            using (var stream = response.GetResponseStream())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
