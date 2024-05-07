using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1
{
    public class IDStorage
    {
        private List<string> sensorMatches = new List<string>();

        public IDStorage()
        {
            UpdateSensorsIDs();
        }

        public int GetSensorNumber(string sensorID)
        {
            if (sensorMatches.Contains(sensorID))
                return sensorMatches.IndexOf(sensorID) + 1;
            return -1;
        }

        public string GetSensorID(int sensorNumber)
        {
            if (sensorMatches.Count >= sensorNumber)
                return sensorMatches[sensorNumber - 1];
            return null;
        }

        public void UpdateSensorsIDs()
        {
            if (Properties.Settings.Default.idCSV.Length > 0)
            {
                sensorMatches.Clear();
                sensorMatches = Properties.Settings.Default.idCSV.Split(',').ToList();
            }
        }
        public bool IsEmpty()
        {
            return sensorMatches.Count == 0;
        }
    }
}
