using System;
using System.Runtime.InteropServices;

namespace Task1
{
    /// <summary>
    /// Wrapper for the API which powrprof.dll provides.
    /// </summary>
    public static class PowrProfWrapper
    {
        [DllImport("powrprof.dll", EntryPoint = "CallNtPowerInformation", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 CallNtPowerInformation(
            POWER_INFORMATION_LEVEL InformationLevel,
            IntPtr lpInputBuffer,
            UInt32 nInputBufferSize,
            IntPtr lpOutputBuffer,
            UInt32 nOutputBufferSize);
    }
}
