using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Task1
{
    [ComVisible(true)]
    [Guid("69E39A4B-7106-41A6-B5CF-3A6FA0B4E6D5")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface ICalculator
    {
        int Add(int a, int b);
    }
}
