using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    public enum State
    {
        WaitingForStart,
        ObkatkaStarted,
        ObkatkaEnded
    }
    [Serializable]
    public struct ChartPoint
    {
        public double temp;
        public DateTime time;

        public ChartPoint(double temp, DateTime time)
        {
            this.temp = temp;
            this.time = time;
        }
    }
    [Serializable]
    public class Sensor
    {

        public List<ChartPoint> measurements;
        public bool IsExternal { get; set; }
        public ushort SensorNumber { get; set; }
        public string Name { get; set; }
        public string SerialNum { get; set; }
        public string FreonMark { get; set; }
        public string FreonQuantity { get; set; }
        public string AddInfo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public State state { get; set; }
        public int id { get; set; }

        public Sensor(ushort num, bool isExternal)
        {
            measurements = new List<ChartPoint>();
            SensorNumber = num;
            IsExternal = isExternal;
            StartTime = DateTime.MinValue;
            StopTime = DateTime.MaxValue;
        }

        public DateTime[] GetTimeArray()
        {
            return measurements.FindAll(s => (s.time > StartTime) && (s.time < StopTime)).Select(item => item.time).ToArray();
        }
        public double[] GetTempArray()
        {
            return measurements.FindAll(s => (s.time > StartTime) && (s.time < StopTime)).Select(item => item.temp).ToArray();
        }

        public DateTime[] GetTimeArray(DateTime startTime, DateTime stopTime)
        {
            return measurements.FindAll(s => (s.time > startTime) && (s.time < stopTime)).Select(item => item.time).ToArray();
        }
        public double[] GetTempArray(DateTime startTime, DateTime stopTime)
        {
            return measurements.FindAll(s => (s.time > startTime) && (s.time < stopTime)).Select(item => item.temp).ToArray();
        }

        public double GetLastMeasurement()
        {
            return measurements.Last().temp;
        }
    }
}
