using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    public interface IProtocol
    {
        bool IsActiveSensor(int sensorNumber);
        string PortName { get; set; }
        int BaudRate { get; set; }
        List<ChartPoint> ReadTemperatures(int sensorNumber);
        Sensor GetSensor(int sensorNumber);      
        bool ModBusOpen { get; }
        void Dispose();
        void Close();
        void UpdateSensors();
    }
}
