using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class LockedWirelessProtocol : IProtocol
    {
        private SerialPort sp485 = new SerialPort();
        private List<Sensor> sensors = new List<Sensor>();
        private IDStorage idStorage;
        private byte _id;
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

        public LockedWirelessProtocol(string portName, IDStorage iDStorage)
        {
            if (sp485 != null && sp485.IsOpen)
            {
                sp485.Close();
            }
            sp485.PortName = portName;
            sp485.BaudRate = 9600;
            sp485.Handshake = Handshake.None;
            sp485.Parity = Parity.None;
            sp485.DataBits = 8;
            sp485.StopBits = StopBits.One;
            sp485.Open();
            sp485.DiscardOutBuffer();
            sp485.DiscardInBuffer();
            sp485.ReadTimeout = 100;
            var _ = new Backup(sensors, Properties.Settings.Default.interval);
            idStorage = iDStorage;
            //SetIDs();
            MatchIDs(idStorage);
        }

        public void Dispose()
        {

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
            byte[] info = new byte[8];
            info[0] = 0x01;
            info[1] = 0x03;
            info[2] = 0x00;
            info[3] = 0x01;
            info[4] = 0x00;
            info[5] = 0x50;
            info = AddCRC(info, 6);
            sp485.Write(info, 0, 8);
            Task.Run(ReadValues);
        }

        private void SetIDs()
        {
            byte[] info = new byte[5];
            byte[] response = new byte[5];
            info[0] = 0xFF;
            info[1] = 0x06;
            info[2] = 0x01;
            info = AddCRC(info, 3);
            sp485.Write(info, 0, 5);
            sp485.Read(response, 0, sp485.BytesToRead);
        }

        private void MatchIDs(IDStorage storage)
        {
            byte[] info = new byte[128];
            info[0] = 0x01;
            info[1] = 0x08;
            info[2] = 3 * 5;
            for (byte i = 0; i < 3; i++)
            {
                var id = storage.GetSensorID(i + 1);

                var idNumber = i + 1;
                if (id != string.Empty)
                    idNumber = Convert.ToInt32(id);
                    
                info[3 + i * 5] = ConvertToHexBytes(idNumber / 1000000);
                info[4 + i * 5] = ConvertToHexBytes(idNumber / 10000 % 100);
                info[5 + i * 5] = ConvertToHexBytes(idNumber / 100 % 100);
                info[6 + i * 5] = ConvertToHexBytes(idNumber % 100);
                info[7 + i * 5] = (byte)(i + 1);
            }

            info = AddCRC(info, 18);
            sp485.Write(info, 0, 20);
            byte[] response = new byte[5];
            var _ = sp485.Read(response, 0, sp485.BytesToRead);
        }

        private byte ConvertToHexBytes(int number)
        {
            return (byte)(number % 10 + number / 10 * 16);
        }

        private void UpdateTemperature(string id, double temp)
        {
            Sensor sensorToUpdate = sensors.Find(sensor => sensor.id == id);
            if (sensorToUpdate == null)
            {
                if (idStorage.GetSensorNumber(id) == 1)
                    sensors.Add(new Sensor(1, true));
                else
                    sensors.Add(new Sensor((ushort)(idStorage.GetSensorNumber(id)), false));
                sensors[sensors.Count - 1].id = id;
                sensors[sensors.Count - 1].measurements.Add(new ChartPoint(temp, DateTime.Now));
            }
            else
            {
                sensorToUpdate.measurements.Add(new ChartPoint(temp, DateTime.Now));
            }
        }

        private Task ReadValues()
        {

            Task.Delay(100);
            byte[] response = new byte[330];
            var _ = sp485.Read(response, 0, sp485.BytesToRead);
            if (response[0] == 0x01 && response[1] == 0x03)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (response[3 + i * 8] != 0xFF)
                    {
                        short temperature = (short)(response[3 + i * 8] << 8 | response[4 + i * 8]);
                        UpdateTemperature(idStorage.GetSensorID(i + 1), temperature / 10.0);
                    }
                }
            }
            return Task.CompletedTask;
        }

        private byte[] AddCRC(byte[] data, int len)
        {
            var crc = ModRTU_CRC(data, len);
            data[len] = (byte)(crc % 256);
            data[len + 1] = (byte)(crc / 256);
            return data;
        }

        // Compute the MODBUS RTU CRC
        UInt16 ModRTU_CRC(byte[] buf, int len)
        {
            UInt16 crc = 0xFFFF;

            for (int pos = 0; pos < len; pos++)
            {
                crc ^= (UInt16)buf[pos];          // XOR byte into least sig. byte of crc

                for (int i = 8; i != 0; i--)      // Loop over each bit
                {
                    if ((crc & 0x0001) != 0)        // If the LSB is set
                    {
                        crc >>= 1;                    // Shift right and XOR 0xA001
                        crc ^= 0xA001;
                    }
                    else                            // Else LSB is not set
                    {
                        crc >>= 1;                    // Just shift right
                    }
                }
            }
            // Note, this number has low and high bytes swapped, so use it accordingly (or swap bytes)
            return crc;
        }
    }
}