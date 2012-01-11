namespace SharpSoSMS
{
   /// <summary>
   /// Contains the error message returned from SoSMS.
   /// </summary>
   public class SoSMSResponseError
   {
       /// <summary>
       /// Initializes a new instance of the <see cref="SoSMSResponseError"/> class.
       /// </summary>
       /// <param name="message">The message.</param>
       public SoSMSResponseError(string message)
       {
           this.Message = message;
       }

       public string Message { get; private set; }

       /// <summary>
       /// Returns a <see cref="System.String"/> that represents this instance.
       /// </summary>
       /// <returns>
       /// A <see cref="System.String"/> that represents this instance.
       /// </returns>
       public override string ToString()
       {
           return this.Message;
       }
   }
}