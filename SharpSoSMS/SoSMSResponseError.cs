namespace SharpSoSMS
{
    /// <summary>
    /// Contains the error message returned from SoSMS.
    /// </summary>
    public class SoSMSResponseError
    {
        private readonly string message;


        /// <summary>
        /// Initializes a new instance of the <see cref="SoSMSResponseError"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SoSMSResponseError(string message)
        {
            this.message = message;
        }


        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Message
        {
            get { return this.message; }
        }


        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.message;
        }
    }
}
