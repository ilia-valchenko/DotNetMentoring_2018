using System;
using System.Runtime.InteropServices;
using Task1Library;

namespace Task2
{
    // Note: Task2 assembly should be registered via RegAsm.exe by using the following
    // command: cmd> RegAsm.exe Task2.dll /tlb /codebase

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// ClassInterfaceType.None means that we won't generate a special interface
    /// for this class. It's just a class.
    /// </remarks>
    [ComVisible(true)]
    [Guid("AB559F03-D621-45E5-AF0F-D03F25FB5727")]
    [ClassInterface(ClassInterfaceType.None)]
    public class PowrProfWrapper : IPowrProfWrapper
    {
        /// <summary>
        /// Reserves or removes system hibernation file.
        /// </summary>
        /// <param name="reserve">
        /// The reserver. If parameter is set to TRUE then the hibernation file is reserved,
        /// if the parameter is set to FALSE then the hibernation file is removed.
        /// </param>
        /// <returns>
        /// Returns the numner which stands for one of Nt statuses.
        /// </returns>
        public uint ReserveHibernationFile(bool reserve)
        {
            int sizeOfInputBuffer = Marshal.SizeOf<UInt32>();
            uint sizeOfOutputBuffer = 0;
            IntPtr inputBuffer = Marshal.AllocHGlobal(sizeOfInputBuffer);
            Marshal.WriteInt32(inputBuffer, Convert.ToInt32(reserve));

            // Note: the hiberfil.sys file is located at C:\. You should run the Task1.exe with admin
            // admin permission if you want to delete hiberfil.sys.

            var statusResult = Task1Library.PowrProfWrapper.CallNtPowerInformation(
                POWER_INFORMATION_LEVEL.SystemReserveHiberFile,
                inputBuffer,
                (uint)sizeOfInputBuffer,
                IntPtr.Zero,
                sizeOfOutputBuffer);

            return (uint)statusResult;
        }

        /// <summary>
        /// Turns on sleep mode.
        /// </summary>
        /// <returns>
        /// Returns true if a sleep mode was successfully activated.
        /// </returns>
        public bool TurnOnSleepMode()
        {
            bool bHibernate = true;
            bool bForce = true;
            bool bWakeupEventsDisabled = true;

            return Task1Library.PowrProfWrapper.SetSuspendState(bHibernate, bForce, bWakeupEventsDisabled);
        }
    }
}
