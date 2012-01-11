using System;
using System.Configuration;
using System.Web;

namespace SharpSoSMS
{
    /// <summary>
    /// Configuration class for SoSMS.
    /// </summary>
    class SoSMSConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoSMSConfiguration"/> class.
        /// </summary>
        public SoSMSConfiguration()
        {
            AuthToken = ConfigurationManager.AppSettings["SoSMS.AuthToken"];

            ProjectRoot = HttpContext.Current != null
                              ? HttpContext.Current.Request.ApplicationPath
                              : Environment.CurrentDirectory;
        }

        /// <summary>
        /// Gets or sets the project root. By default set to  <see cref="HttpRequest.ApplicationPath"/>
        /// if <see cref="HttpContext.Current"/> is not null, else <see cref="Environment.CurrentDirectory"/>. 
        /// </summary>
        /// <remarks>
        /// Only set this if you need to override the default project root.
        /// </remarks>
        /// <value>
        /// The project root.
        /// </value>
        public string ProjectRoot { get; set; }

        /// <summary>
        /// Gets or sets the AuthToken.
        /// </summary>
        /// <value>
        /// The Auth Token.
        /// </value>
        public string AuthToken { get; set; }
    }
}
