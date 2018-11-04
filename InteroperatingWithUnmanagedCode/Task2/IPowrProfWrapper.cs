using System.Runtime.InteropServices;

namespace Task2
{
    [ComVisible(true)]
    [Guid("7D0BC2EF-2542-4C0C-A84E-20715B73C157")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IPowrProfWrapper
    {
        /*void*/ int TurnOnSleepMode();

        // test
        int Sum(int a, int b);
    }
}