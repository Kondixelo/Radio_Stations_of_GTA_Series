using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
namespace GTA_radio_stations_app
{
    public class GamesDataAccess
    {
        public List<Games> GetGames(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("Csharp_learning")))
            {
                var output =  connection.Query<Games>($"select * from Games where Name='{name}'").ToList();
                return output;
            }
        }
    }
}
