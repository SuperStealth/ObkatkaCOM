using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    public interface IProtocol
    {
        List<ChartPoint> ReadTemperatures();
    }
}
