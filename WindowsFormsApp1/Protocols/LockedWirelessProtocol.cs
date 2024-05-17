using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    class LockedWirelessProtocol : IProtocol
    {
        private SerialPort serialPort = new SerialPort();
        private List<Sensor> sensors = new List<Sensor>();
        private IDStorage idStorage;
        private byte _id;
        public string PortName
        {
            get
            {
                return serialPort.PortName;
            }
            set
            {
                serialPort.PortName = value;
            }
        }

        public int BaudRate
        {
            get
            {
                return serialPort.BaudRate;
            }
            set
            {
                serialPort.BaudRate = value;
            }
        }
        public bool ModBusOpen
        {
            get
            {
                return serialPort.IsOpen;
            }
        }

        public LockedWirelessProtocol(string portName, IDStorage iDStorage)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
            serialPort.PortName = portName;
            serialPort.BaudRate = 9600;
            serialPort.Handshake = Handshake.None;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Open();
            serialPort.DiscardOutBuffer();
            serialPort.DiscardInBuffer();
            serialPort.ReadTimeout = 1000;
            var _ = new Backup(sensors, Properties.Settings.Default.interval);
            idStorage = iDStorage;
            SetIDs();
            MatchIDs(idStorage);
            SetIDs();
            GetIDs();
        }

        public void Dispose()
        {

        }

        public void Close()
        {
            serialPort.Close();
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
            SetIDs();
            byte[] info = new byte[8];
            info[0] = 0x01;
            info[1] = 0x03;
            info[2] = 0x00;
            info[3] = 0x01;
            info[4] = 0x00;
            info[5] = 0x50;
            info = AddCRC(info, 6);
            byte[] response = SendCommand(info, 8, 165);
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
        }

        private void GetIDs()
        {
            byte[] info = new byte[5];
            byte[] response = new byte[125];
            info[0] = 0x01;
            info[1] = 0x0A;
            info[2] = 0x00;
            info = AddCRC(info, 3);
            var result = SendCommand(info, 5, 125);
        }


        private void SetIDs()
        {
            byte[] info = new byte[5];
            info[0] = 0xFF;
            info[1] = 0x06;
            info[2] = 0x01;
            info = AddCRC(info, 3);
            SendCommand(info, 5, 5);
        }

        private void MatchIDs(IDStorage storage)
        {
            byte[] info = new byte[25];
            info[0] = 0x01;
            info[1] = 0x09;
            info[2] = 0x14;
            for (byte i = 0; i < 20; i++)
            {
                info[i + 3] = (byte)(i + 1);
            }
            info = AddCRC(info, 23);
            SendCommand(info, 25, 5);

            info = new byte[105];
            info[0] = 0x01;
            info[1] = 0x08;
            info[2] = 100;
            for (byte i = 1; i < 21; i++)
            {
                var id = storage.GetSensorID(i);
                if (id != string.Empty)
                {
                    var idNumber = Convert.ToInt32(id);
                    info[3 + (i - 1) * 5] = ConvertToHexBytes(idNumber / 1000000);
                    info[4 + (i - 1) * 5] = ConvertToHexBytes(idNumber / 10000 % 100);
                    info[5 + (i - 1) * 5] = ConvertToHexBytes(idNumber / 100 % 100);
                    info[6 + (i - 1) * 5] = ConvertToHexBytes(idNumber % 100);
                }
                info[7 + (i - 1) * 5] = i;
            }
            info = AddCRC(info, 103);
            SendCommand(info, 105, 5);
        }

        private byte[] SendCommand(byte[] data, int length, int expectedOutputLength)
        {
            serialPort.Write(data, 0, length);
            byte[] response = new byte[expectedOutputLength];
            var x = 3;

            while (!WaitForBytes(expectedOutputLength) && x > 0)
            {
                x--;
                serialPort.Write(data, 0, length);
            }

            if (x > 0 || WaitForBytes(expectedOutputLength))
            {
                serialPort.Read(response, 0, serialPort.BytesToRead);
                return response;
            }
            return null;
        }

        private bool WaitForBytes(int bytesCount)
        {
            int x = 100;
            while (serialPort.BytesToRead < bytesCount && x > 0)
            {
                x--;
                Thread.Sleep(10);
            }
            if (x == 0)
            {
                return false;
            }
            return true;
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