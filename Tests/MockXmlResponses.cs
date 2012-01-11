using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    public class XmlMockedResponses
    {
        public static string AccountBalance() {
            return @"<accountCredits>
                       <value>106</value>
                     </accountCredits>";
        }

        public static string Message()
        {
            return @"<message>
                      <id type=""integer"">1002</id>
                      <text>Test</text>
                      <message-count>3</message-count>
                      <message-dispaches>
                        <message-dispach>
                          <status>Processando</status>
                          <phone-number>(00) 0000-0000</phone-number>
                        </message-dispach>
                        <message-dispach>
                          <status>Processando</status>
                          <phone-number>(99) 9999-9999</phone-number>
                        </message-dispach>
                        <message-dispach>
                          <status>Processando</status>
                          <phone-number>(88) 8888-8888</phone-number>
                        </message-dispach>
                      </message-dispaches>
                    </message>";
        }
    }
}
