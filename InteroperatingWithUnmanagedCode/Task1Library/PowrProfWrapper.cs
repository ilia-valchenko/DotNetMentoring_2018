using System;
using System.Runtime.InteropServices;

namespace Task1Library
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
        /// <param name="lpInputBuffer">
        /// A pointer to an optional input buffer. The data type of this buffer depends on
        /// the information level requested in the InformationLevel parameter.
        /// </param>
        /// <param name="nInputBufferSize">
        /// The size of the input buffer, in bytes.
        /// </param>
        /// <param name="lpOutputBuffer">
        /// A pointer to an optional output buffer. The data type of this buffer depends on
        /// the information level requested in the InformationLevel parameter. If the buffer
        /// is too small to contain the information, the function returns STATUS_BUFFER_TOO_SMALL.
        /// </param>
        /// <param name="nOutputBufferSize">
        /// The size of the output buffer, in bytes. Depending on the information level requested,
        /// this may be a variably sized buffer.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is STATUS_SUCCESS.
        /// </returns>
        [DllImport(
            "powrprof.dll",
            EntryPoint = "CallNtPowerInformation",
            CallingConvention = CallingConvention.StdCall,
            SetLastError = true)]
        public static extern NtStatus CallNtPowerInformation(
            POWER_INFORMATION_LEVEL InformationLevel,
            IntPtr lpInputBuffer,
            UInt32 nInputBufferSize,
            IntPtr lpOutputBuffer,
            UInt32 nOutputBufferSize);

        /// <summary>
        /// Suspends the system by shutting power down. Depending on the Hibernate parameter,
        /// the system either enters a suspend (sleep) state or hibernation (S4).
        /// </summary>
        /// <param name="bHibernate">
        /// If this parameter is TRUE, the system hibernates.
        /// If the parameter is FALSE, the system is suspended.</param>
        /// <param name="bForce">
        /// This parameter has no effect.
        /// </param>
        /// <param name="bWakeupEventsDisabled">
        /// If this parameter is TRUE, the system disables all wake events.
        /// If the parameter is FALSE, any system wake events remain enabled.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero.
        /// </returns>
        [DllImport("Powrprof.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(
            bool bHibernate,
            bool bForce,
            bool bWakeupEventsDisabled);
    }
}
