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
            return (SoSMSBalance) SendRequest(url, typeof(SoSMSBalance));
        }

        /// <summary>
        ///  Returns the message
        /// </summary>
        /// <returns>SoSMSMessage</returns>
        public SoSMSBalance GetMessage(int id)
        {
            string url = sosmsUri + "/messages/" + id + "?auth_token=" + configuration.AuthToken;
            return (SoSMSBalance) SendRequest(url, typeof(SoSMSMessage));
        }

        private Object SendRequest(string url, Type deserializerType)
        {
            HttpWebResponse response = null;
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
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
