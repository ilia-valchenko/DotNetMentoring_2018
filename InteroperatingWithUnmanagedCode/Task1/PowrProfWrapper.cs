using System;
using System.Runtime.InteropServices;

namespace Task1
{
    /// <summary>
    /// Wrapper for the API which powrprof.dll provides.
    /// </summary>
    internal static class PowrProfWrapper
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
