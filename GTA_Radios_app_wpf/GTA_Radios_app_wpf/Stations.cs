using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_Radios_app_wpf
{
    class Stations
    {
        public int Station_id { get; set; }
        public string Station_Name { get; set; }
        public int Station_GameID { get; set; }
        public int Station_OrderInGame { get; set; }
        public string Station_MusicGenre { get; set; }
        public int Station_NumberOfTracks { get; set; }
        public int Station_IsUserStation { get; set; }
        public string Station_Cover { get; set; }
    }
}
