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
            uint nInputBufferSize = 0;

            #region Get LastSleepTime.
            int info = PowrProfWrapper.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.LastSleepTime,
                IntPtr.Zero,
                nInputBufferSize,
                outputBuffer,
                (uint)size);

            Console.WriteLine($"LastSleeptime: {outputBuffer}\nOutput buffer receives a ULONGLONG that specifies the interrupt-time count, in 100-nanosecond units, at the last system sleep time.\n");
            #endregion

            #region Get LastWakeTime.
            info = PowrProfWrapper.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.LastWakeTime,
                IntPtr.Zero,
                nInputBufferSize,
                outputBuffer,
                (uint)size);

            Console.WriteLine($"LastWakeTime: {outputBuffer}\nOutput buffer buffer receives a ULONGLONG that specifies the interrupt-time count, in 100-nanosecond units, at the last system wake time.\n");
            #endregion

            #region Get SystemBatteryState.
            info = PowrProfWrapper.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.SystemBatteryState,
                IntPtr.Zero,
                nInputBufferSize,
                outputBuffer,
                (uint)size);

            var batteryState = Marshal.PtrToStructure(outputBuffer, typeof(SYSTEM_BATTERY_STATE));

            Console.WriteLine(batteryState.ToString());
            #endregion

            #region Get SystemPowerInformation.
            info = PowrProfWrapper.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.SystemPowerInformation,
                IntPtr.Zero,
                nInputBufferSize,
                outputBuffer,
                (uint)size);

            var powerInformation = Marshal.PtrToStructure(outputBuffer, typeof(SYSTEM_POWER_INFORMATION));

            Console.WriteLine(Environment.NewLine + powerInformation);
            #endregion

            #region Remove hibernation file
            int sizeOfInputBuffer = Marshal.SizeOf<UInt32>();
            uint sizeOfOutputBuffer = 0;
            IntPtr inputBuffer = Marshal.AllocHGlobal(sizeOfInputBuffer);

            // 0 - FALSE
            // 1 - TRUE
            Marshal.WriteInt32(inputBuffer, 0);

            // Note: the hiberfil.sys file is located at C:\. You should run the Task1.exe with admin
            // admin permission if you want to delete hiberfil.sys.
            info = PowrProfWrapper.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.SystemReserveHiberFile,
                inputBuffer,
                (uint)sizeOfInputBuffer,
                IntPtr.Zero,
                sizeOfOutputBuffer);

            Console.WriteLine($"The hiberfil.sys file which is located at C:\\ was successfully removed.");
            #endregion

            #region SetSuspendState
            Console.WriteLine("\nAttention! After pressing any key your computer will go the sleep mode.\nTap to continue...");
            Console.ReadKey();

            var result = PowrProfWrapper.SetSuspendState(true, true, true);
            #endregion

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }
    }
}
