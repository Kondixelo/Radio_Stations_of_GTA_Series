using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTA_radio_stations_app
{
    public partial class Form1 : Form
    {
        List<Games> gamesList = new List<Games>();
        public Form1()
        {
            InitializeComponent();

            ListHolder.DataSource = gamesList;
            ListHolder.DisplayMember = "FullInfo";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            GamesDataAccess db = new GamesDataAccess();

            gamesList = db.GetGames(nameTextBox.Text);
        }
    }
}
