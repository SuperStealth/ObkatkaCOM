using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    class WiredProtocol : IProtocol
    {
        public List<ChartPoint> ReadTemperatures()
        {
            return new List<ChartPoint>();
        }
    }
}
