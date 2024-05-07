using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WindowsFormsApp1
{
    [Serializable]
    class Backup
    {
        
        private List<Sensor> _sensors;
        public Backup(List<Sensor> sensors, int interval)
        {
            System.Windows.Forms.Timer refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 5000;
            refreshTimer.Tick += RefreshBackup;
            refreshTimer.Start();
            _sensors = sensors;
        }

        private void RefreshBackup(object myObject,
                                            EventArgs myEventArgs)
        {
            IFormatter formatter = new BinaryFormatter();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backup.bkp");
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, this);
            stream.Close();
        }

        public static List<Sensor> Restore(string fileName)
        {
            IFormatter formatter = new BinaryFormatter();
            string path = fileName;
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            Backup backup = (Backup)formatter.Deserialize(stream);
            stream.Close();
            return backup._sensors;
        }

    }
}
