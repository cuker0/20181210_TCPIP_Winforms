using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20181210_TCPIP_Winforms
{
    static class BCCHelper
    {
        public static void CalculateAndAppendBcc(ref byte[] data)
        {
            byte bcc = 0;

            foreach (byte dataByte in data)
            {
                bcc ^= dataByte;
            }
            Array.Resize(ref data,data.Length + 1);
            data[data.Length - 1] = bcc;
        }
    }
}
