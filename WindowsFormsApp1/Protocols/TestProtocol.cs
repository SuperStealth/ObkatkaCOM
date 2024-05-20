using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObkatkaCom
{
    class TestProtocol : IProtocol
    {
        private List<Sensor> sensors = new List<Sensor>();
        private Backup backup;
        private IDStorage idStorage;
        Random rnd = new Random();

        public string PortName { get; set; }
        public int BaudRate { get; set; }

        public bool ModBusOpen { get; set; }

        public TestProtocol(string portName, IDStorage iDStorage)
        {
            System.Windows.Forms.Timer refreshTimer = new System.Windows.Forms.Timer();
            backup = new Backup(sensors, Properties.Settings.Default.interval);
            idStorage = iDStorage;
            refreshTimer.Interval = 500;
            refreshTimer.Tick += new EventHandler(RefreshBackup);
            refreshTimer.Start();
            sensors.Add(new Sensor((ushort)(idStorage.GetSensorNumber("12345678")), true));
            sensors.Add(new Sensor((ushort)(idStorage.GetSensorNumber("12345679")), false));
            sensors.Add(new Sensor((ushort)(idStorage.GetSensorNumber("12345677")), false));
        }

        private void RefreshBackup(object myObject,
                                            EventArgs myEventArgs)
        {
            UpdateSensors();
        }

        public void Close()
        {
            return;
        }

        public void Dispose()
        {
            return;
        }

        public Sensor GetSensor(int sensorNumber)
        {
            return sensors.Find(s => s.SensorNumber == sensorNumber);
        }

        public bool IsActiveSensor(int sensorNumber)
        {
            if (sensors.Find(s => s.SensorNumber == sensorNumber) != null)
                return true;
            return false;
        }

        public List<ChartPoint> ReadTemperatures(int sensorNumber)
        {
            return sensors.Find(s => s.SensorNumber == sensorNumber).measurements;
        }

        public void Restore(List<Sensor> restoredSensors)
        {
            sensors = restoredSensors;
        }

        public void UpdateSensors()
        {
            
            foreach (Sensor sensorToUpdate in sensors)
                sensorToUpdate.measurements.Add(new ChartPoint(rnd.Next(-30,-20) + sensorToUpdate.SensorNumber*10, DateTime.Now));
        }
    }
}
