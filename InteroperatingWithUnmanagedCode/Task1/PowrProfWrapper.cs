using System;
using System.Runtime.InteropServices;

namespace Task1
{
    /// <summary>
    /// Wrapper for the API which powrprof.dll provides.
    /// </summary>
    public static class PowrProfWrapper
    {
        /// <summary>
        /// Sets or retrieves power information.
        /// </summary>
        /// <param name="InformationLevel">
        /// The information level requested. This value indicates the specific power information to be set or retrieved.
        /// This parameter must be one of the following <see cref="POWER_INFORMATION_LEVEL"/> enumeration type values.
        /// </param>
        /// <param name="lpInputBuffer"></param>
        /// <param name="nInputBufferSize"></param>
        /// <param name="lpOutputBuffer"></param>
        /// <param name="nOutputBufferSize"></param>
        /// <returns></returns>
        [DllImport("powrprof.dll", EntryPoint = "CallNtPowerInformation", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 CallNtPowerInformation(
            POWER_INFORMATION_LEVEL InformationLevel,
            IntPtr lpInputBuffer,
            UInt32 nInputBufferSize,
            IntPtr lpOutputBuffer,
            UInt32 nOutputBufferSize);
    }
}
