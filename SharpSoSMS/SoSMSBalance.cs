using System;
using System.Xml.Serialization;

namespace SharpSoSMS
{
    [XmlRoot("accountCredits", Namespace = "")]
    public class SoSMSBalance
    {
        [XmlElement("value")]
        public int Value { get; set; }
    }
}
