using System;
using System.Runtime.InteropServices;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = Marshal.SizeOf<SYSTEM_BATTERY_STATE>();
            IntPtr outputBuffer = Marshal.AllocCoTaskMem(size);

            #region Get LastSleepTime.
            int info = PowrProfWrapper.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.LastSleepTime,
                IntPtr.Zero,
                0,
                outputBuffer,
                (uint)size);

            Console.WriteLine($"LastSleeptime: {outputBuffer}\nOutput buffer receives a ULONGLONG that specifies the interrupt-time count, in 100-nanosecond units, at the last system sleep time.\n"); 
            #endregion

            #region Get LastWakeTime.
            info = PowrProfWrapper.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.LastWakeTime,
                IntPtr.Zero,
                0,
                outputBuffer,
                (uint)size);

            Console.WriteLine($"LastWakeTime: {outputBuffer}\nOutput buffer buffer receives a ULONGLONG that specifies the interrupt-time count, in 100-nanosecond units, at the last system wake time.\n"); 
            #endregion

            // Get SystemBatteryState.

            var test = PowrProfWrapper.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.SystemBatteryState,
                IntPtr.Zero,
                0,
                outputBuffer,
                (uint)size);

            var batteryStruct = Marshal.PtrToStructure(outputBuffer, typeof(SYSTEM_BATTERY_STATE));

            Console.WriteLine($"SystemBatteryState: {outputBuffer}\n");

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }
    }
}
