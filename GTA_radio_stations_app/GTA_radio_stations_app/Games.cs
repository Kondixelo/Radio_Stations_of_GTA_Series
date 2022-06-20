using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_radio_stations_app
{
    public class Games
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string TimeOfAction { get; set; }
        public int Is_DLC { get; set; }
        public int NumberOfDLC { get; set; }
        public int NumberOfStations { get; set; }
        public string Plot { get; set; }
        //public string Cover { get; set; }


        public string FullInfo
        {
            get { return $"{Name}"; } 
        }

    }
}
