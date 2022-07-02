using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GTA_Radios_app_wpf
{
    /// <summary>
    /// Interaction logic for start_window.xaml
    /// </summary>
    public partial class start_window : Window
    {
        public start_window()
        {
            InitializeComponent();
            LoadingDelay();
        }

        public async void LoadingDelay()
        {
            await Task.Delay(5000);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
