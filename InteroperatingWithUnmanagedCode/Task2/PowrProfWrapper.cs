using System.Runtime.InteropServices;

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
        public /*void*/ int TurnOnSleepMode()
        {
            bool bHibernate = true;
            bool bForce = true;
            bool bWakeupEventsDisabled = true;

            Task1.PowrProfWrapper.SetSuspendState(bHibernate, bForce, bWakeupEventsDisabled);

            return 1;
        }
    }
}
