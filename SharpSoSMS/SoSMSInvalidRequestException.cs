using System;
using System.Collections.Generic;

namespace SharpSoSMS
{
    [Serializable()]
    class SoSMSInvalidRequestException : System.Exception
    {
        public SoSMSResponseError[] Errors { get; set; }
        public SoSMSInvalidRequestException(SoSMSResponseError[] errors)
        {
            this.Errors = errors;
        }

        public override string ToString()
        {
            string errorMessage = "";
            foreach (SoSMSResponseError error in Errors)
            {
                errorMessage += error.ToString() + "\n";
            }
            return errorMessage;
        }
    }
}
