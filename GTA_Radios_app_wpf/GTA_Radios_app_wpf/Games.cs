using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA_Radios_app_wpf
{
    class Games { 
        public int id { get; set; }
            public string Name { get; set; }
            public DateTime DateRelease { get; set; }
            public string TimeOfAction { get; set; }
            public int NumberOfDLC { get; set; }
            public int IsDLC { get; set; }
            public int NumberOfStations { get; set; }
            public string Plot { get; set; }
            public string Cover { get; set; }

   
    }
}
