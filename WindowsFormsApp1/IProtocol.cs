using System.Collections.Generic;

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

        void Restore(List<Sensor> restoredSensors);
        void UpdateSensors();
    }
}
