using System;
using System.Xml.Serialization;

namespace SharpSoSMS
{
    public class SoSMSMessageDispach
    {
        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("phone-number")]
        public string PhoneNumber { get; set; }
    }
}
