using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;

namespace ObkatkaCom
{
    class WirelessProtocol : IProtocol
    {
        private SerialPort sp485 = new SerialPort();
        public static ModbusSerialMaster ModBUS;
        private List<Sensor> sensors = new List<Sensor>();
        private Backup backup;
        private IDStorage idStorage;

        public string PortName 
        {
            get
            {
                return sp485.PortName;
            }
            set 
            {
                sp485.PortName = value;
            } 
        }

        public int BaudRate 
        { 
            get
            {
                return sp485.BaudRate;
            }
            set
            {
                sp485.BaudRate = value;
            }
        }
        public bool ModBusOpen
        {
            get
            {
                return sp485.IsOpen;
            }
        }
        public WirelessProtocol(string portName, IDStorage iDStorage)
        {
            if (sp485 != null && sp485.IsOpen)
            {
                sp485.Close();
                ModBUS.Dispose();
            }
            sp485.PortName = portName;
            sp485.BaudRate = 115200;
            sp485.Open();
            sp485.DiscardOutBuffer();
            sp485.DiscardInBuffer();
            ModBUS = ModbusSerialMaster.CreateAscii(sp485);
            sp485.ReadTimeout = 100;
            backup = new Backup(sensors, Properties.Settings.Default.interval);
            idStorage = iDStorage;
        }

        public void Dispose()
        {
            ModBUS.Dispose();
        }
        public void Close()
        {
            sp485.Close();
        }
        public void Restore(List<Sensor> restoredSensors)
        {
            sensors = restoredSensors;
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

        public Sensor GetSensor(int sensorNumber)
        {
            return sensors.Find(s => s.SensorNumber == sensorNumber);
        }

        public void UpdateSensors()
        {
            while (sp485.BytesToRead > 0)
                UpdateTemperature(sp485.ReadLine());
        }

        private void UpdateTemperature(string str)
        {
            string id = str.Substring(str.IndexOf("ID=")+3,8);
            string temperature = str.Substring(str.IndexOf("TEP=") + 4, 5);
            string voltage = str.Substring(str.IndexOf("V=") + 2, 4);
            var format = new NumberFormatInfo();
            format.NegativeSign = "-";
            format.NumberDecimalSeparator = ".";
            format.PositiveSign = "+";
            double temp = double.Parse(temperature,format);
            Sensor sensorToUpdate = sensors.Find(sensor => sensor.id == id);
            if (sensorToUpdate == null)
            {
                if (idStorage.GetSensorNumber(id) == 1)
                    sensors.Add(new Sensor(1, true));
                else
                    sensors.Add(new Sensor((ushort)(idStorage.GetSensorNumber(id)), false));
                sensors[sensors.Count - 1].id = id;
                sensors[sensors.Count - 1].measurements.Add(new ChartPoint(temp, DateTime.Now));
                sensors[sensors.Count - 1].Voltage = float.Parse(voltage);
            }
            else
            {
                sensorToUpdate.measurements.Add(new ChartPoint(temp, DateTime.Now));
            }
        }
    }
}