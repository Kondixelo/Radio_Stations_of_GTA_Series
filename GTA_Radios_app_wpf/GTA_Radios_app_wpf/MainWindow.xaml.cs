using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GTA_Radios_app_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string backButtonDirection;
        private int buttonGameID;
        private int buttonStationID;
        private int leftImageIndex = 2;
        private int rightImageIndex = 1;
        public MainWindow()
        {
            InitializeComponent();
            StartInfoBox();
            StartItemInfoBox();
            ShowGames();
            ChangeBackground();
            OpacityEffect();
        }

        private void OpacityEffect()
        {
            DispatcherTimer opacityTimer = new DispatcherTimer();
            opacityTimer.Interval = TimeSpan.FromSeconds(16);
            opacityTimer.Tick += OpacityImage_Ticker;
            opacityTimer.Start();
        }

        private void OpacityImage_Ticker(object sender, EventArgs e)
        {
            BlinkingImage(RightImageBorder, 2000);
            BlinkingImage(LeftImageBorder, 2000);
        }
        public void BlinkingImage(Border image, int lenght)
        {
            DoubleAnimation opacityAnim = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromMilliseconds(lenght)),
                AutoReverse = true
            };
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(opacityAnim);
            Storyboard.SetTarget(opacityAnim, image);
            Storyboard.SetTargetProperty(opacityAnim, new PropertyPath("Opacity"));
            storyboard.Begin(image);
            IncludeDelay();
        }

        public void ChangeBackground()
        {
            Random r = new Random();
            rightImageIndex = r.Next(1, 27);
            leftImageIndex = r.Next(1, 27);
            while (leftImageIndex == rightImageIndex)
            {
                leftImageIndex = r.Next(1, 27);
            }
            var filenameRight = @"C:\csharp\Radio_Stations_of_GTA_Series\GTA_Radios_app_wpf\GTA_Radios_app_wpf\Backgrounds\gta" + rightImageIndex + ".png";
            //var filenameLeft = @"pack://application:,,,/GTA_Radios_app_wpf;GTA_Radios_app_wpf\Backgrounds\gta" + leftImageIndex + ".png";
            var filenameLeft = @"C:\csharp\Radio_Stations_of_GTA_Series\GTA_Radios_app_wpf\GTA_Radios_app_wpf\Backgrounds\gta"  + leftImageIndex + ".png";
            RightImageBrush.ImageSource = new BitmapImage(new Uri(filenameRight, UriKind.Absolute));
            LefttImageBrush.ImageSource = new BitmapImage(new Uri(filenameLeft, UriKind.Absolute));
        }
        public async void IncludeDelay()
        {
            await Task.Delay(2000);
            ChangeBackground();
        }



        public void ShowGames()
        {
            BackButton.IsEnabled = false;
            MainList.Items.Clear();
            string query = "SELECT * FROM GTAbase.dbo.Games ";
            string connectionPath = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GTAbase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection sql = new SqlConnection(connectionPath))
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand(query, sql);
                cmd.CommandType = CommandType.Text;
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    Games gamesObject = new Games();
                    gamesObject.id = Convert.ToInt32(rdr["ID"]);
                    gamesObject.Name = rdr["Name"].ToString();
                    gamesObject.DateRelease = Convert.ToDateTime(rdr["Release_date"]);
                    gamesObject.TimeOfAction = rdr["Time_of_action"].ToString();
                    gamesObject.NumberOfDLC = Convert.ToInt32(rdr["Number_of_DLC"]);
                    gamesObject.IsDLC = Convert.ToInt32(rdr["Is_DLC"]);
                    gamesObject.NumberOfStations = Convert.ToInt32(rdr["Number_of_stations"]);
                    gamesObject.Plot = rdr["Plot"].ToString();

                    ListBoxItem itemB = new ListBoxItem();
                    itemB.FontSize = 40;
                    itemB.Foreground = Brushes.Cornsilk;
                    itemB.Name = "GameID" + gamesObject.id.ToString(); ;
                    itemB.Content = gamesObject.Name + "|" + gamesObject.IsDLC + "|" + gamesObject.NumberOfStations;
                    MainList.Items.Add(itemB);
                }
            }
        }

        void ShowStations(int gameID)
        {
            MainList.Items.Clear();
            ItemInfoBox.Inlines.Clear();
            backButtonDirection = "games";
            buttonGameID = gameID;
            BackButton.IsEnabled = true;
            TextBoxGameInfo(gameID);
            string query = "SELECT * FROM GTAbase.dbo.Stations WHERE Game_ID = " + gameID;
            string connectionPath = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GTAbase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection sql = new SqlConnection(connectionPath))
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand(query, sql);
                cmd.CommandType = CommandType.Text;
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Stations stationsObject = new Stations();
                    stationsObject.Station_id = Convert.ToInt32(rdr["ID"]);
                    stationsObject.Station_Name = rdr["Name"].ToString();
                    stationsObject.Station_GameID = Convert.ToInt32(rdr["Game_ID"]);
                    stationsObject.Station_OrderInGame = Convert.ToInt32(rdr["Order_in_game"]);
                    stationsObject.Station_MusicGenre = rdr["Music_genre"].ToString();
                    stationsObject.Station_NumberOfTracks = Convert.ToInt32(rdr["Number_of_tracks"]);
                    stationsObject.Station_IsUserStation = Convert.ToInt32(rdr["Is_user_station"]);

                    ListBoxItem itemB = new ListBoxItem();
                    itemB.FontSize = 40;
                    itemB.Foreground = Brushes.Cornsilk;
                    itemB.Name = "StationID" + stationsObject.Station_id.ToString(); ;
                    itemB.Content = stationsObject.Station_Name;
                    MainList.Items.Add(itemB);
                }
            }
        }

        void ShowTracks(int stationID)
        {
            ItemInfoBox.Inlines.Clear();
            buttonStationID = stationID;
            MainList.Items.Clear();

            backButtonDirection = "stations";
            TextBoxStationInfo(stationID);
            string query = "SELECT * FROM GTAbase.dbo.Tracks WHERE Station_ID = " + stationID;
            string connectionPath = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GTAbase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection sql = new SqlConnection(connectionPath))
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand(query, sql);
                cmd.CommandType = CommandType.Text;
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Tracks tracksObject = new Tracks();
                    tracksObject.Track_id = Convert.ToInt32(rdr["ID"]);
                    tracksObject.Track_author = rdr["Author"].ToString();
                    tracksObject.Track_title = rdr["Title"].ToString();
                    tracksObject.Track_station_id = Convert.ToInt32(rdr["Station_ID"]);
                    tracksObject.Track_order_in_station = Convert.ToInt32(rdr["Order_in_station"]);

                    ListBoxItem itemB = new ListBoxItem();
                    itemB.FontSize = 40;
                    itemB.Foreground = Brushes.Cornsilk;
                    itemB.Name = "TrackID" + tracksObject.Track_id.ToString(); ;
                    itemB.Content = tracksObject.Track_author + " - " + tracksObject.Track_title;
                    MainList.Items.Add(itemB);


                }
            }
        }

        void TextBoxGameInfo(int gameID)
        {
            string query = "SELECT * FROM GTAbase.dbo.Games WHERE ID=" + gameID;
            string connectionPath = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GTAbase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection sql = new SqlConnection(connectionPath))
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand(query, sql);
                cmd.CommandType = CommandType.Text;
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Games gamesObject = new Games();
                    gamesObject.id = Convert.ToInt32(rdr["ID"]);
                    gamesObject.Name = rdr["Name"].ToString();
                    gamesObject.DateRelease = Convert.ToDateTime(rdr["Release_date"]);
                    gamesObject.TimeOfAction = rdr["Time_of_action"].ToString();
                    gamesObject.NumberOfDLC = Convert.ToInt32(rdr["Number_of_DLC"]);
                    gamesObject.IsDLC = Convert.ToInt32(rdr["Is_DLC"]);
                    gamesObject.NumberOfStations = Convert.ToInt32(rdr["Number_of_stations"]);
                    gamesObject.Plot = rdr["Plot"].ToString();
                    gamesObject.Cover = rdr["Cover"].ToString();

                    //title
                    Run run = new Run("Title: ");
                    run.Foreground = Brushes.LightSteelBlue;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(gamesObject.Name + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //release
                    run = new Run("Release: ");
                    run.Foreground = Brushes.LightSteelBlue;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(gamesObject.DateRelease.ToString("dd-M-yyyy") + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //Timeline
                    run = new Run("Timeline: ");
                    run.Foreground = Brushes.LightSteelBlue;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(gamesObject.TimeOfAction + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //isDLC
                    if (gamesObject.IsDLC != 0)
                    {
                        string query2 = "SELECT * FROM GTAbase.dbo.Games WHERE ID=" + gamesObject.IsDLC;
                        string connectionPath2 = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GTAbase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                        using (SqlConnection sql2 = new SqlConnection(connectionPath2))
                        {
                            sql.Open();
                            SqlCommand cmd2 = new SqlCommand(query, sql);
                            cmd.CommandType = CommandType.Text;
                            SqlDataReader rdr2 = cmd.ExecuteReader();

                            run = new Run("DLCs:");
                            run.Foreground = Brushes.LightSteelBlue;
                            ItemInfoBox.Inlines.Add(run);
                            run = new Run(" This game is DLC for:");
                            run.Foreground = Brushes.LightYellow;
                            ItemInfoBox.Inlines.Add(run);

                            while (rdr2.Read())
                            {
                                Games MainGame = new Games();
                                MainGame.Name = rdr2["Name"].ToString();
                                run = new Run(" " + MainGame.Name); //<------------------dokonczyc, odwolac sie do bazy games i wypisac gre do ktorej nalezy to dlc
                                run.Foreground = Brushes.LightYellow;
                                ItemInfoBox.Inlines.Add(run);
                            }

                        }
                    }

                    //DLCs
                    if (gamesObject.NumberOfDLC == 0)
                    {
                        run = new Run("DLCs: ");
                        run.Foreground = Brushes.LightSteelBlue;
                        ItemInfoBox.Inlines.Add(run);
                        run = new Run(gamesObject.NumberOfDLC.ToString() + "\r\n");
                        run.Foreground = Brushes.LightYellow;
                        ItemInfoBox.Inlines.Add(run);
                    }
                    else
                    {
                            string query3= "SELECT * FROM GTAbase.dbo.Games WHERE Is_DLC=" + gameID;
                            string connectionPath3 = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GTAbase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                        using (SqlConnection sql3 = new SqlConnection(connectionPath3))
                        {
                            sql3.Open();
                            SqlCommand cmd3 = new SqlCommand(query3, sql3);
                            cmd.CommandType = CommandType.Text;
                            SqlDataReader rdr3 = cmd.ExecuteReader();

                            run = new Run("DLCs:");
                            run.Foreground = Brushes.LightSteelBlue;
                            ItemInfoBox.Inlines.Add(run);
                            run = new Run(" " + gamesObject.NumberOfDLC.ToString() + ":");
                            run.Foreground = Brushes.LightYellow;
                            ItemInfoBox.Inlines.Add(run);

                            while (rdr3.Read())
                            {
                                Games DLCs = new Games();
                                DLCs.Name = rdr3["Name"].ToString();
                                run = new Run(" " + DLCs.Name);
                                run.Foreground = Brushes.LightYellow;
                                ItemInfoBox.Inlines.Add(run);
                            }
                        }
                    }

                    //Stations
                    run = new Run("Stations: ");
                    run.Foreground = Brushes.LightSteelBlue;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(gamesObject.NumberOfStations.ToString() + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //Plot
                    run = new Run("Plot: ");
                    run.Foreground = Brushes.LightSteelBlue;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(gamesObject.Plot + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //Cover
                    try
                    {
                        var CoverImage = @"C:\csharp\Radio_Stations_of_GTA_Series\GTA_Radios_app_wpf\GTA_Radios_app_wpf\Covers\Games\" + gamesObject.Cover;
                        CoverPlace.Source = new BitmapImage(new Uri(CoverImage, UriKind.Absolute));
                        AltText.Visibility = Visibility.Hidden;
                    }
                    catch
                    {
                        CoverPlace.Source = null;
                        AltText.Visibility = Visibility.Visible;
                    }
                    
                }

            }

        }


        void TextBoxStationInfo(int stationID)
        {
            string query = "SELECT * FROM GTAbase.dbo.Stations WHERE ID = " + stationID;
            string connectionPath = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GTAbase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection sql = new SqlConnection(connectionPath))
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand(query, sql);
                cmd.CommandType = CommandType.Text;
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Stations stationsObject = new Stations();
                    stationsObject.Station_id = Convert.ToInt32(rdr["ID"]);
                    stationsObject.Station_Name = rdr["Name"].ToString();
                    stationsObject.Station_GameID = Convert.ToInt32(rdr["Game_ID"]);
                    stationsObject.Station_OrderInGame = Convert.ToInt32(rdr["Order_in_game"]);
                    stationsObject.Station_MusicGenre = rdr["Music_genre"].ToString();
                    stationsObject.Station_NumberOfTracks = Convert.ToInt32(rdr["Number_of_tracks"]);
                    stationsObject.Station_IsUserStation = Convert.ToInt32(rdr["Is_user_station"]);
                    stationsObject.Station_Cover = rdr["Cover"].ToString();
                    try
                    {
                        var CoverImage = @"C:\csharp\Radio_Stations_of_GTA_Series\GTA_Radios_app_wpf\GTA_Radios_app_wpf\Covers\Stations\" + stationsObject.Station_Cover;
                        CoverPlace.Source = new BitmapImage(new Uri(CoverImage, UriKind.Absolute));
                        AltText.Visibility = Visibility.Hidden;
                    }
                    catch
                    {
                        CoverPlace.Source = null;
                        AltText.Visibility = Visibility.Visible;
                    }
                    ItemInfoBox.Inlines.Clear();


                    //Name
                    Run run = new Run("Name: ");
                    run.Foreground = Brushes.LightSteelBlue;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(stationsObject.Station_Name + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //Order in game
                    run = new Run("Order in game: ");
                    run.Foreground = Brushes.LightSteelBlue;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(stationsObject.Station_OrderInGame + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);
                    
                    if (stationsObject.Station_IsUserStation == 1)
                    {
                        //
                        run = new Run("Custom radio station: ");
                        run.Foreground = Brushes.LightSteelBlue;
                        ItemInfoBox.Inlines.Add(run);
                        run = new Run("Allows players to play their songs" + "\r\n");
                        run.Foreground = Brushes.LightYellow;
                        ItemInfoBox.Inlines.Add(run);

                    }
                    else
                    {
                        //Music genre
                        run = new Run("Genre: ");
                        run.Foreground = Brushes.LightSteelBlue;
                        ItemInfoBox.Inlines.Add(run);
                        run = new Run(stationsObject.Station_MusicGenre + "\r\n");
                        run.Foreground = Brushes.LightYellow;
                        ItemInfoBox.Inlines.Add(run);

                        //Tracks
                        run = new Run("Number of tracks: ");
                        run.Foreground = Brushes.LightSteelBlue;
                        ItemInfoBox.Inlines.Add(run);
                        run = new Run(stationsObject.Station_NumberOfTracks + "\r\n");
                        run.Foreground = Brushes.LightYellow;
                        ItemInfoBox.Inlines.Add(run);
                    }
                }
            }

        }



        private void ListBoxMouseEnter(object sender, MouseEventArgs e)
        {
            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);

            if (lbi != null)
            {
                lbi.Foreground = Brushes.Red;
            }
        }

      

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int itemID;
            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
            ListBox lbA = e.Source as ListBox;
            if (lbi != null)
            {
                
                string itemType = lbi.Name.Substring(0, 1);
                if (itemType == "G") //game
                {
                    lbA.Items.Clear();
                    itemID = Convert.ToInt32(lbi.Name.Substring(6, (lbi.Name.Length - 6)));
                    ShowStations(itemID);
                }
                if (itemType == "S") //station
                {
                    lbA.Items.Clear();
                    itemID = Convert.ToInt32(lbi.Name.Substring(9, (lbi.Name.Length - 9)));
                    ShowTracks(itemID);
                }
            }
        }

        private void MenuButtonClick(object sender, RoutedEventArgs e)
        {
            ItemInfoBox.Inlines.Clear();
            BackButton.IsEnabled = false;
            StartInfoBox();
            StartItemInfoBox();
            ShowGames();
        }

        private void QuitButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }


        void StartInfoBox()
        {
            InfoBox.Text = "Welcome to application \"Radio stations of the games of Grand Theft Auto series\". Choose a game, then a station or search station or a track.";
        }
        void StartItemInfoBox()
        {
            var CoverImage = @"C:\csharp\Radio_Stations_of_GTA_Series\GTA_Radios_app_wpf\GTA_Radios_app_wpf\Covers\Games\mainlogo.png";
            CoverPlace.Source = new BitmapImage(new Uri(CoverImage, UriKind.Absolute));
            AltText.Visibility = Visibility.Hidden;
        }


        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            ItemInfoBox.Inlines.Clear();
            if (backButtonDirection == "games")
            {
                BackButton.IsEnabled = false;
                StartInfoBox();
                StartItemInfoBox();
                ShowGames();
            }
            if (backButtonDirection == "stations")
            {
                TextBoxStationInfo(buttonStationID);
                ShowStations(buttonGameID);
            }
        }
    } 
}
