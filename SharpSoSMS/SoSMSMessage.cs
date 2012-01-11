using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SharpSoSMS
{
    [XmlRoot("message")]
    public class SoSMSMessage
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("text")]
        public string Text { get; set; }

        [XmlArray("message-dispaches"), XmlArrayItem(typeof(SoSMSMessageDispach), ElementName = "message-dispach")]
        public SoSMSMessageDispach[] Dispaches { get; set; }
    }
}
