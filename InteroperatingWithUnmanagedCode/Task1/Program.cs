using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = Marshal.SizeOf<SYSTEM_BATTERY_STATE>();
            IntPtr batteryStatePtr = Marshal.AllocCoTaskMem(size);

            var info = PowrProfWrapper.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.SystemBatteryState,
                IntPtr.Zero,
                0,
                batteryStatePtr,
                (uint) size);

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }
    }
}
