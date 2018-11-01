using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    [ComVisible(true)]
    [Guid("8E2C74B2-8B52-4C12-8FCF-23F86DE02EE4")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Calculator: ICalculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
