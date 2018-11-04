using System;
using System.Runtime.InteropServices;
using Task1Library;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Get LastSleepTime.

            // Note: The last sleep time will be 0 if you have never
            // turn on sleep mode on your computer. It would be better
            // to run #region SetSuspendState first of all.

            int size = Marshal.SizeOf<ulong>();
            uint nInputBufferSize = 0;
            IntPtr outputBuffer = Marshal.AllocCoTaskMem(size);

            var status = PowrProfWrapper.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.LastSleepTime,
                IntPtr.Zero,
                nInputBufferSize,
                outputBuffer,
                (uint)size);

            ulong lastSleepTime = (ulong)Marshal.ReadInt64(outputBuffer);

            Console.WriteLine($"Status: {status}");
            Console.WriteLine($"LastSleeptime: {lastSleepTime}\nOutput buffer receives a ULONGLONG that specifies the interrupt-time count, in 100-nanosecond units, at the last system sleep time.\n");

            #endregion

            #region Get LastWakeTime.

            status = PowrProfWrapper.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.LastWakeTime,
                IntPtr.Zero,
                nInputBufferSize,
                outputBuffer,
                (uint)size);

            ulong lastWakeTime = (ulong)Marshal.ReadInt64(outputBuffer);

            Console.WriteLine($"Status: {status}");
            Console.WriteLine($"LastWakeTime: {lastWakeTime}\nOutput buffer buffer receives a ULONGLONG that specifies the interrupt-time count, in 100-nanosecond units, at the last system wake time.\n");

            #endregion

            #region Get SystemBatteryState.

            size = Marshal.SizeOf<SYSTEM_BATTERY_STATE>();
            outputBuffer = Marshal.AllocCoTaskMem(size);

            status = PowrProfWrapper.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.SystemBatteryState,
                IntPtr.Zero,
                nInputBufferSize,
                outputBuffer,
                (uint)size);

            var batteryState = Marshal.PtrToStructure(outputBuffer, typeof(SYSTEM_BATTERY_STATE));

            Console.WriteLine($"Status = {status}");
            Console.WriteLine(batteryState.ToString());

            #endregion

            #region Get SystemPowerInformation.

            size = Marshal.SizeOf<SYSTEM_POWER_INFORMATION>();
            outputBuffer = Marshal.AllocCoTaskMem(size);

            status = PowrProfWrapper.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.SystemPowerInformation,
                IntPtr.Zero,
                nInputBufferSize,
                outputBuffer,
                (uint)size);

            var powerInformation = Marshal.PtrToStructure(outputBuffer, typeof(SYSTEM_POWER_INFORMATION));

            Console.WriteLine($"\nStatus: {status}\n{powerInformation}");

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

            var statusResult = PowrProfWrapper.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.SystemReserveHiberFile,
                inputBuffer,
                (uint)sizeOfInputBuffer,
                IntPtr.Zero,
                sizeOfOutputBuffer);

            Console.WriteLine($"Status result = {statusResult}");

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
