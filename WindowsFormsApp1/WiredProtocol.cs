using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    class WiredProtocol : IProtocol
    {
        private List<Sensor> sensors = new List<Sensor>();
        private SerialPort sp485 = new SerialPort();
        public static IModbusSerialMaster ModBUS;
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
        public WiredProtocol(string portName)
        {
            if (sp485.IsOpen)
            {
                sp485.Close();
                ModBUS.Dispose();
            }
            sp485.PortName = portName;
            sp485.Open();
            sp485.DiscardOutBuffer();
            sp485.DiscardInBuffer();
            ModBUS = ModbusSerialMaster.CreateRtu(sp485);
            for (ushort i = 1; i < 33; i++)
            {
                if (MODRead(i) != 0)
                {
                    sensors.Add(new Sensor(i, false));
                }

            }
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

        public Sensor GetSensor(int sensorNumber)
        {
            return sensors[sensorNumber];
        }
        public List<ChartPoint> ReadTemperatures(int sensorNumber)
        {
            return sensors[sensorNumber].measurements;
        }

        private ushort MODRead(ushort startAdress)
        {
            ushort[] result;
            try
            {
                result = ModBUS.ReadHoldingRegisters(1, startAdress, 1);
            }
            catch
            {
                return 0;
            }
            return (ushort)result[0];
        }

        public bool IsActiveSensor(int sensorNumber)
        {
            return sensors.Exists(s => (s.SensorNumber == sensorNumber));
        }
        public void UpdateSensors()
        {
            foreach (Sensor sensor in sensors)
            {
                if (!sensor.IsExternal)
                {
                    sensor.measurements.Add(new ChartPoint(MODRead(sensor.SensorNumber) / 10.0, DateTime.Now));                    
                }
                else
                {
                    sensor.measurements.Add(new ChartPoint(MODRead(sensor.SensorNumber) / 10.0, DateTime.Now));                    
                }
            }
        }
    }
}
