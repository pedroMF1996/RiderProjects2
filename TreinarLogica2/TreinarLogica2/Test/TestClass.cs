
using System;

namespace TreinarLogica2.Test
{
    public class TestClass
    {
        public static byte[] VerificaByteArray(byte[] msg, string exceptionMsg)
        {
            if (string.IsNullOrEmpty(Convert.ToString(msg)))
            {
                throw new Exception(exceptionMsg);
            }
            return msg;
        }

        public static string VerificaString(string msg, string exceptionMsg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                throw new Exception(exceptionMsg);
            }
            return msg;
        }
    }
}
