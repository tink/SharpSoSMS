using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Linq;

namespace SharpSoSMS
{
    public class SoSMSResponseDeserializer
    {
        private string content;
        private Type baseElementType;

        private IEnumerable<SoSMSResponseError> errors;

        public SoSMSResponseDeserializer(Type baseElementType, string content)
        {
            this.content = content;
            this.baseElementType = baseElementType;
        }

        public Object Deserialize()
        {
            using (var stringReader = new StringReader(this.content))
            {
                using (var xml = XmlReader.Create(stringReader))
                {
                    xml.MoveToContent();

                    switch (xml.LocalName)
                    {
                        case "errors":
                            this.errors = xml.BuildErrors();
                            throw new SoSMSInvalidRequestException(this.errors.ToArray());
                        default:
                            XmlSerializer serializer = new XmlSerializer(this.baseElementType, "");
                            byte[] byteArray = Encoding.UTF8.GetBytes(this.content);
                            MemoryStream stream = new MemoryStream(byteArray);
                            return serializer.Deserialize(stream);
                    }
                }
            }
        }
    }
}
