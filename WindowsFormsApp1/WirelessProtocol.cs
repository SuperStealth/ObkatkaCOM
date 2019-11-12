﻿using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    class WirelessProtocol : IProtocol
    {
        private SerialPort sp485 = new SerialPort();
        public static ModbusSerialMaster ModBUS;
        private List<Sensor> sensors = new List<Sensor>();
        private Backup backup;

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
        public WirelessProtocol(string portName)
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
        }
        public bool ModBusOpen
        {
            get
            {
                return sp485.IsOpen;
            }
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
            int id = Convert.ToInt32(str.Substring(str.IndexOf("ID=")+3,8));
            string temperature = str.Substring(str.IndexOf("TEP=") + 4, 5);
            var format = new NumberFormatInfo();
            format.NegativeSign = "-";
            format.NumberDecimalSeparator = ".";
            format.PositiveSign = "+";
            double temp = double.Parse(temperature,format);
            Sensor sensor = sensors.Find(s => s.id == id);
            if (sensor == null)
            {
                if (id - Properties.Settings.Default.startId == 0)
                    sensors.Add(new Sensor((ushort)(id - Properties.Settings.Default.startId + 1), true));
                else
                    sensors.Add(new Sensor((ushort)(id - Properties.Settings.Default.startId + 1), false));
                sensors[sensors.Count - 1].id = id;
                sensors[sensors.Count - 1].measurements.Add(new ChartPoint(temp, DateTime.Now));
            }
            else
            {
                sensor.measurements.Add(new ChartPoint(temp, DateTime.Now));
            }
        }
    }
}