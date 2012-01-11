using System;
using System.Collections.Generic;
using System.Xml;

namespace SharpSoSMS
{
    /// <summary>
    /// Contains the extension method to send exceptions to Airbrake.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Builds the errors from the <see cref="XmlReader"/>.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>
        /// An <see cref="IEnumerable{SoSMSResponseError}"/>.
        /// </returns>
        internal static IEnumerable<SoSMSResponseError> BuildErrors(this XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.LocalName == "error")
                            yield return new SoSMSResponseError(reader.ReadElementContentAsString());
                        break;
                }
            }
        }

    }
}