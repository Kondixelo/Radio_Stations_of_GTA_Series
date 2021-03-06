using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Media;
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
    public partial class MainWindow : Window
    {
        private string backButtonDirection;
        private string chosenGame;
        private int buttonGameID;
        private int buttonStationID;
        private int leftImageIndex = 2;
        private int rightImageIndex = 1;
        private int messageCount = 0;
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
            int countFiles = Directory.GetFiles("Backgrounds").Length;
            rightImageIndex = r.Next(1, countFiles);
            leftImageIndex = r.Next(1, countFiles);
            while (leftImageIndex == rightImageIndex)
            {
                leftImageIndex = r.Next(1, 27);
            }
            try
            {
                var filenameRight = "Backgrounds/gta" + rightImageIndex + ".png";
                var filenameLeft = "Backgrounds/gta" + leftImageIndex + ".png";
                RightImageBrush.ImageSource = new BitmapImage(new Uri(filenameRight, UriKind.RelativeOrAbsolute));
                LefttImageBrush.ImageSource = new BitmapImage(new Uri(filenameLeft, UriKind.RelativeOrAbsolute));
            }
            catch
            {
                MessageBox.Show("Nie udalo sie wczytac tla");
            }


        }
        public async void IncludeDelay()
        {
            await Task.Delay(2000);
            ChangeBackground();
        }

        public void Search()
        {
            ItemInfoBox.Text = "";
            StartItemInfoBox();
            string searchBoxPhrase;
            searchBoxPhrase = SearchBox.Text;

            if (searchBoxPhrase != "")
            {
                ShowStations(0, searchBoxPhrase);
                ShowTracks(0, searchBoxPhrase);
            }
        }

        public void ShowGames()
        {
            StartInfoBox();
            BackButton.IsEnabled = false;
            MainList.Items.Clear();
            string query = "SELECT * FROM [dbo].[Games];";
            //string connectionPath = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GTA_base;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            DataTable tbl = Database_class.Get_DataTable(query);
            Games gamesObject = new Games();
            
            foreach (DataRow rdr in tbl.Rows)
            {
                gamesObject.id = Convert.ToInt32(rdr["ID"]);
                gamesObject.Name = rdr["Name"].ToString();
                gamesObject.DateRelease = Convert.ToDateTime(rdr["Release_date"]);
                gamesObject.TimeOfAction = rdr["Time_of_action"].ToString();
                gamesObject.NumberOfDLC = Convert.ToInt32(rdr["Number_of_DLC"]);
                gamesObject.IsDLC = Convert.ToInt32(rdr["Is_DLC"]);
                gamesObject.NumberOfStations = Convert.ToInt32(rdr["Number_of_stations"]);
                gamesObject.Plot = rdr["Plot"].ToString();

                ListBoxItem itemB = new ListBoxItem();
                itemB.FontSize = 20;
                itemB.Foreground = Brushes.AliceBlue;
                itemB.Name = "GameID" + gamesObject.id.ToString(); ;
                itemB.Content = "GTA " + gamesObject.Name;
                MainList.Items.Add(itemB);
            }

            /*
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
                    itemB.FontSize = 20;
                    itemB.Foreground = Brushes.AliceBlue;
                    itemB.Name = "GameID" + gamesObject.id.ToString(); ;
                    itemB.Content = "GTA " + gamesObject.Name;
                    MainList.Items.Add(itemB);
                }
            }
            */

        }

        public void ShowStations(int gameID, string searchPhrase)
        {
            backButtonDirection = "games";
            buttonGameID = gameID;
            BackButton.IsEnabled = true;
            MainList.Items.Clear();
            string query = "SELECT * FROM [dbo].[Stations] WHERE Game_ID = " + gameID + " ORDER BY Order_in_game ASC";
            //string connectionPath = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GTAbase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            if (searchPhrase != null)
            {
                query = "SELECT * FROM [dbo].[Stations] WHERE Name LIKE '%" + searchPhrase + "%' OR Music_genre LIKE '%" + searchPhrase + "%'";
            }
            else
            {
                ItemInfoBox.Inlines.Clear();
                TextBoxGameInfo(gameID);
            }

            try
            {
                DataTable tbl = Database_class.Get_DataTable(query);
                if (searchPhrase != null && tbl.Rows.Count != 0)
                {
                    ListBoxItem headerStations = new ListBoxItem();
                    headerStations.Content = "Stations";
                    headerStations.HorizontalAlignment = HorizontalAlignment.Center;
                    headerStations.FontSize = 30;
                    headerStations.Foreground = Brushes.AntiqueWhite;
                    headerStations.Focusable = false;
                    headerStations.IsEnabled = false;
                    MainList.Items.Add(headerStations);
                }
                Stations stationsObject = new Stations();
                foreach (DataRow rdr in tbl.Rows)
                {
                    stationsObject.Station_id = Convert.ToInt32(rdr["ID"]);
                    stationsObject.Station_Name = rdr["Name"].ToString();
                    stationsObject.Station_GameID = Convert.ToInt32(rdr["Game_ID"]);
                    stationsObject.Station_OrderInGame = Convert.ToInt32(rdr["Order_in_game"]);
                    stationsObject.Station_MusicGenre = rdr["Music_genre"].ToString();
                    stationsObject.Station_NumberOfTracks = Convert.ToInt32(rdr["Number_of_tracks"]);
                    stationsObject.Station_IsUserStation = Convert.ToInt32(rdr["Is_user_station"]);

                    ListBoxItem itemB = new ListBoxItem();
                    itemB.FontSize = 20;
                    itemB.Foreground = Brushes.AliceBlue;
                    itemB.Name = "StationID" + stationsObject.Station_id.ToString(); ;
                    if (searchPhrase != null)
                    {
                        itemB.Content = stationsObject.Station_Name;
                    }
                    else
                    {
                        itemB.Content = stationsObject.Station_OrderInGame + ". " + stationsObject.Station_Name;
                    }
                    MainList.Items.Add(itemB);
                }
            }
            catch
            {
                MessageWrongPhrase();
            }
            

            /*
            using (SqlConnection sql = new SqlConnection(connectionPath))
            {        
                try
                {
                    sql.Open();
                    SqlCommand cmd = new SqlCommand(query, sql);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (searchPhrase != null && rdr.HasRows)
                    {
                        ListBoxItem headerStations = new ListBoxItem();
                        headerStations.Content = "Stations";
                        headerStations.HorizontalAlignment = HorizontalAlignment.Center;
                        headerStations.FontSize = 30;
                        headerStations.Foreground = Brushes.AntiqueWhite;
                        headerStations.Focusable = false;
                        headerStations.IsEnabled = false;
                        MainList.Items.Add(headerStations);
                    }
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
                        itemB.FontSize = 20;
                        itemB.Foreground = Brushes.AliceBlue;
                        itemB.Name = "StationID" + stationsObject.Station_id.ToString(); ;
                        if (searchPhrase != null)
                        {
                            itemB.Content = stationsObject.Station_Name;
                        }
                        else
                        {
                            itemB.Content = stationsObject.Station_OrderInGame + ". " + stationsObject.Station_Name;
                        }
                        MainList.Items.Add(itemB);
                    }
                }
                catch 
                {
                    MessageWrongPhrase();
                }
                
            }
            */
        }

        void ShowTracks(int stationID, string searchPhrase)
        {
            buttonStationID = stationID;
            backButtonDirection = "stations";
            string query = "SELECT * FROM [dbo].[Tracks] WHERE Station_ID = " + stationID + " ORDER BY Order_in_station ASC";
            //string connectionPath = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GTAbase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            if (searchPhrase != null)
            {
                query = "SELECT * FROM [dbo].[Tracks] WHERE Author LIKE '%" + searchPhrase + "%' OR Title LIKE '%" + searchPhrase + "%'";
            }
            else
            {
                ItemInfoBox.Inlines.Clear();
                MainList.Items.Clear();
                TextBoxStationInfo(stationID);
            }

            try
            {
                DataTable tbl = Database_class.Get_DataTable(query);
                if (searchPhrase != null && tbl.Rows.Count != 0)
                {
                    ListBoxItem headerTracks = new ListBoxItem();
                    headerTracks.Content = "Tracks";
                    headerTracks.HorizontalAlignment = HorizontalAlignment.Center;
                    headerTracks.FontSize = 30;
                    headerTracks.Foreground = Brushes.Red;
                    headerTracks.Focusable = false;
                    headerTracks.IsEnabled = false;
                    MainList.Items.Add(headerTracks);
                }
                Tracks tracksObject = new Tracks();
                foreach (DataRow rdr in tbl.Rows)
                {
                    tracksObject.Track_id = Convert.ToInt32(rdr["ID"]);
                    tracksObject.Track_author = rdr["Author"].ToString();
                    tracksObject.Track_title = rdr["Title"].ToString();
                    tracksObject.Track_station_id = Convert.ToInt32(rdr["Station_ID"]);
                    tracksObject.Track_order_in_station = Convert.ToInt32(rdr["Order_in_station"]);

                    ListBoxItem itemB = new ListBoxItem();
                    itemB.FontSize = 20;
                    itemB.Foreground = Brushes.AliceBlue;
                    itemB.Name = "TrackID" + tracksObject.Track_id.ToString(); ;
                    if (searchPhrase != null)
                    {
                        itemB.Content = tracksObject.Track_author + " - " + tracksObject.Track_title;
                    }
                    else
                    {
                        itemB.Content = tracksObject.Track_order_in_station + ". " + tracksObject.Track_author + " - " + tracksObject.Track_title;
                    }

                    MainList.Items.Add(itemB);
                }
            }
            catch
            {
                MessageWrongPhrase();
            }
            /*
            using (SqlConnection sql = new SqlConnection(connectionPath))
            {
                try
                {
                    sql.Open();
                    SqlCommand cmd = new SqlCommand(query, sql);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    SearchInfoBox(searchPhrase);
                    if (searchPhrase != null && rdr.HasRows)
                    {
                        ListBoxItem headerTracks = new ListBoxItem();
                        headerTracks.Content = "Tracks";
                        headerTracks.HorizontalAlignment = HorizontalAlignment.Center;
                        headerTracks.FontSize = 30;
                        headerTracks.Foreground = Brushes.Red;
                        headerTracks.Focusable = false;
                        headerTracks.IsEnabled = false;
                        MainList.Items.Add(headerTracks);
                    }
                    while (rdr.Read())
                    {
                        Tracks tracksObject = new Tracks();
                        tracksObject.Track_id = Convert.ToInt32(rdr["ID"]);
                        tracksObject.Track_author = rdr["Author"].ToString();
                        tracksObject.Track_title = rdr["Title"].ToString();
                        tracksObject.Track_station_id = Convert.ToInt32(rdr["Station_ID"]);
                        tracksObject.Track_order_in_station = Convert.ToInt32(rdr["Order_in_station"]);

                        ListBoxItem itemB = new ListBoxItem();
                        itemB.FontSize = 20;
                        itemB.Foreground = Brushes.AliceBlue;
                        itemB.Name = "TrackID" + tracksObject.Track_id.ToString(); ;
                        if (searchPhrase != null)
                        {
                            itemB.Content = tracksObject.Track_author + " - " + tracksObject.Track_title;
                        }
                        else
                        {
                            itemB.Content = tracksObject.Track_order_in_station + ". " + tracksObject.Track_author + " - " + tracksObject.Track_title;
                        }

                        MainList.Items.Add(itemB);
                    }
                }
                catch
                {
                    MessageWrongPhrase();
                }
            }
            */
        }

        void TextBoxGameInfo(int gameID)
        {
            string query = "SELECT * FROM [dbo].[Games] WHERE ID=" + gameID;
            //string connectionPath = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GTAbase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                DataTable tbl = Database_class.Get_DataTable(query);
                Games gamesObject = new Games();
                foreach (DataRow rdr in tbl.Rows)
                {
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
                    run.Foreground = Brushes.LightCyan;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(gamesObject.Name + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //release
                    run = new Run("Release: ");
                    run.Foreground = Brushes.LightCyan;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(gamesObject.DateRelease.ToString("dd-M-yyyy") + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //Timeline
                    run = new Run("Timeline: ");
                    run.Foreground = Brushes.LightCyan;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(gamesObject.TimeOfAction + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //isDLC
                    if (gamesObject.IsDLC != 0)
                    {
                        string query2 = "SELECT * FROM GTA_base.dbo.Games WHERE ID=" + gamesObject.IsDLC;
                        //string connectionPath2 = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GTAbase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                        try
                        {
                            DataTable tbl2 = Database_class.Get_DataTable(query2);
                            run = new Run("DLCs:");
                            run.Foreground = Brushes.LightCyan;
                            ItemInfoBox.Inlines.Add(run);
                            run = new Run(" This game is DLC for: GTA ");
                            run.Foreground = Brushes.LightYellow;
                            ItemInfoBox.Inlines.Add(run);

                            Games MainGame = new Games();
                            foreach (DataRow rdr2 in tbl2.Rows)
                            {
                                MainGame.Name = rdr2["Name"].ToString();
                                run = new Run(" " + MainGame.Name + "\r\n");
                                run.Foreground = Brushes.LightYellow;
                                ItemInfoBox.Inlines.Add(run);
                            }
                        }
                        catch
                        {
                            MessageProgramError();
                        }
                        /*
                        using (SqlConnection sql2 = new SqlConnection(connectionPath2))
                        {
                            sql.Open();
                            SqlCommand cmd2 = new SqlCommand(query, sql);
                            cmd.CommandType = CommandType.Text;
                            SqlDataReader rdr2 = cmd.ExecuteReader();

                            run = new Run("DLCs:");
                            run.Foreground = Brushes.LightCyan;
                            ItemInfoBox.Inlines.Add(run);
                            run = new Run(" This game is DLC for:");
                            run.Foreground = Brushes.LightYellow;
                            ItemInfoBox.Inlines.Add(run);

                            while (rdr2.Read())
                            {
                                Games MainGame = new Games();
                                MainGame.Name = rdr2["Name"].ToString();
                                run = new Run(" " + MainGame.Name);
                                run.Foreground = Brushes.LightYellow;
                                ItemInfoBox.Inlines.Add(run);
                            }

                        }
                        */
                    }
                    else
                    {
                        //DLCs
                        if (gamesObject.NumberOfDLC == 0)
                        {
                            run = new Run("DLCs: ");
                            run.Foreground = Brushes.LightCyan;
                            ItemInfoBox.Inlines.Add(run);
                            run = new Run(gamesObject.NumberOfDLC.ToString() + "\r\n");
                            run.Foreground = Brushes.LightYellow;
                            ItemInfoBox.Inlines.Add(run);
                        }
                        else
                        {
                            string query3 = "SELECT * FROM GTA_base.dbo.Games WHERE Is_DLC=" + gameID;
                            //string connectionPath3 = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GTAbase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                            try
                            {
                                DataTable tbl3 = Database_class.Get_DataTable(query3);
                                run = new Run("DLCs:");
                                run.Foreground = Brushes.LightCyan;
                                ItemInfoBox.Inlines.Add(run);
                                run = new Run(" " + gamesObject.NumberOfDLC.ToString() + ":");
                                run.Foreground = Brushes.LightYellow;
                                ItemInfoBox.Inlines.Add(run);

                                Games DLCs = new Games();
                                foreach (DataRow rdr3 in tbl3.Rows)
                                {
                                    DLCs.Name = rdr3["Name"].ToString();
                                    run = new Run(" " + DLCs.Name + "\r\n");
                                    run.Foreground = Brushes.LightYellow;
                                    ItemInfoBox.Inlines.Add(run);
                                }
                            }
                            catch
                            {
                                MessageProgramError();
                            }

                            /*
                            using (SqlConnection sql3 = new SqlConnection(connectionPath3))
                            {
                                sql3.Open();
                                SqlCommand cmd3 = new SqlCommand(query3, sql3);
                                cmd.CommandType = CommandType.Text;
                                SqlDataReader rdr3 = cmd.ExecuteReader();

                                run = new Run("DLCs:");
                                run.Foreground = Brushes.LightCyan;
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
                            */
                        }
                    }

                    

                    //Stations
                    run = new Run("Stations: ");
                    run.Foreground = Brushes.LightCyan;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(gamesObject.NumberOfStations.ToString() + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //Plot
                    run = new Run("Plot: ");
                    run.Foreground = Brushes.LightCyan;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(gamesObject.Plot + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //Cover
                    try
                    {
                        var CoverImage = "Covers/Games/" + gamesObject.Cover;
                        CoverPlace.Source = new BitmapImage(new Uri(CoverImage, UriKind.RelativeOrAbsolute));
                        AltText.Visibility = Visibility.Hidden;
                    }
                    catch
                    {
                        CoverPlace.Source = null;
                        AltText.Visibility = Visibility.Visible;
                    }

                }
            }
            catch
            {
                MessageProgramError();
            }
            
            /*
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
                    run.Foreground = Brushes.LightCyan;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(gamesObject.Name + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //release
                    run = new Run("Release: ");
                    run.Foreground = Brushes.LightCyan;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(gamesObject.DateRelease.ToString("dd-M-yyyy") + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //Timeline
                    run = new Run("Timeline: ");
                    run.Foreground = Brushes.LightCyan;
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
                            run.Foreground = Brushes.LightCyan;
                            ItemInfoBox.Inlines.Add(run);
                            run = new Run(" This game is DLC for:");
                            run.Foreground = Brushes.LightYellow;
                            ItemInfoBox.Inlines.Add(run);

                            while (rdr2.Read())
                            {
                                Games MainGame = new Games();
                                MainGame.Name = rdr2["Name"].ToString();
                                run = new Run(" " + MainGame.Name);
                                run.Foreground = Brushes.LightYellow;
                                ItemInfoBox.Inlines.Add(run);
                            }

                        }
                    }

                    //DLCs
                    if (gamesObject.NumberOfDLC == 0)
                    {
                        run = new Run("DLCs: ");
                        run.Foreground = Brushes.LightCyan;
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
                            run.Foreground = Brushes.LightCyan;
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
                    run.Foreground = Brushes.LightCyan;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(gamesObject.NumberOfStations.ToString() + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //Plot
                    run = new Run("Plot: ");
                    run.Foreground = Brushes.LightCyan;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(gamesObject.Plot + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //Cover
                    try
                    {
                        var CoverImage = @"C:\csharp\Radio_Stations_of_GTA_Series\GTA_Radios_app_wpf\GTA_Radios_app_wpf\Covers\Games\" + gamesObject.Cover; //<----------poprawic sciezki
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
            */

        }

        void TextBoxStationInfo(int stationID)
        {
            string query = "SELECT * FROM [dbo].[Stations] WHERE ID = " + stationID;
            //string connectionPath = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GTAbase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            try
            {
                DataTable tbl = Database_class.Get_DataTable(query);
                Stations stationsObject = new Stations();
                foreach (DataRow rdr in tbl.Rows)
                {
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
                        var CoverImage = "Covers/Stations/" + stationsObject.Station_Cover;
                        CoverPlace.Source = new BitmapImage(new Uri(CoverImage, UriKind.RelativeOrAbsolute));
                        AltText.Visibility = Visibility.Hidden;
                    }
                    catch
                    {
                        CoverPlace.Source = null;
                        AltText.Visibility = Visibility.Visible;
                    }
                    ItemInfoBox.Inlines.Clear();

                    try
                    {
                        string query2 = "SELECT * FROM [dbo].[Games] WHERE ID = " + stationsObject.Station_GameID;
                        DataTable tbl2 = Database_class.Get_DataTable(query2);
                        foreach(DataRow rdr2 in tbl2.Rows)
                        {
                            Run run2 = new Run("Game: ");
                            run2.Foreground = Brushes.LightCyan;
                            ItemInfoBox.Inlines.Add(run2);
                            run2 = new Run("GTA " +rdr2["Name"] + "\r\n");
                            run2.Foreground = Brushes.LightYellow;
                            ItemInfoBox.Inlines.Add(run2);
                        }
                    }
                    catch
                    {
                        MessageProgramError();
                    }

                    //Name
                    Run run = new Run("Name: ");
                    run.Foreground = Brushes.LightCyan;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(stationsObject.Station_Name + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //Order in game
                    run = new Run("Order in game: ");
                    run.Foreground = Brushes.LightCyan;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(stationsObject.Station_OrderInGame + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    if (stationsObject.Station_IsUserStation == 1)
                    {
                        //
                        run = new Run("Custom radio station: ");
                        run.Foreground = Brushes.LightCyan;
                        ItemInfoBox.Inlines.Add(run);
                        run = new Run("Allows players to play their songs" + "\r\n");
                        run.Foreground = Brushes.LightYellow;
                        ItemInfoBox.Inlines.Add(run);

                    }
                    else
                    {
                        //Music genre
                        run = new Run("Genre: ");
                        run.Foreground = Brushes.LightCyan;
                        ItemInfoBox.Inlines.Add(run);
                        run = new Run(stationsObject.Station_MusicGenre + "\r\n");
                        run.Foreground = Brushes.LightYellow;
                        ItemInfoBox.Inlines.Add(run);

                        //Tracks
                        run = new Run("Number of tracks: ");
                        run.Foreground = Brushes.LightCyan;
                        ItemInfoBox.Inlines.Add(run);
                        run = new Run(stationsObject.Station_NumberOfTracks + "\r\n");
                        run.Foreground = Brushes.LightYellow;
                        ItemInfoBox.Inlines.Add(run);
                    }
                }
            }
            catch
            {
                MessageProgramError();
            }
            
            /*
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
                        var CoverImage = @"C:\csharp\Radio_Stations_of_GTA_Series\GTA_Radios_app_wpf\GTA_Radios_app_wpf\Covers\Stations\" + stationsObject.Station_Cover; //<----------poprawic sciezki
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
                    run.Foreground = Brushes.LightCyan;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(stationsObject.Station_Name + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);

                    //Order in game
                    run = new Run("Order in game: ");
                    run.Foreground = Brushes.LightCyan;
                    ItemInfoBox.Inlines.Add(run);
                    run = new Run(stationsObject.Station_OrderInGame + "\r\n");
                    run.Foreground = Brushes.LightYellow;
                    ItemInfoBox.Inlines.Add(run);
                    
                    if (stationsObject.Station_IsUserStation == 1)
                    {
                        //
                        run = new Run("Custom radio station: ");
                        run.Foreground = Brushes.LightCyan;
                        ItemInfoBox.Inlines.Add(run);
                        run = new Run("Allows players to play their songs" + "\r\n");
                        run.Foreground = Brushes.LightYellow;
                        ItemInfoBox.Inlines.Add(run);

                    }
                    else
                    {
                        //Music genre
                        run = new Run("Genre: ");
                        run.Foreground = Brushes.LightCyan;
                        ItemInfoBox.Inlines.Add(run);
                        run = new Run(stationsObject.Station_MusicGenre + "\r\n");
                        run.Foreground = Brushes.LightYellow;
                        ItemInfoBox.Inlines.Add(run);

                        //Tracks
                        run = new Run("Number of tracks: ");
                        run.Foreground = Brushes.LightCyan;
                        ItemInfoBox.Inlines.Add(run);
                        run = new Run(stationsObject.Station_NumberOfTracks + "\r\n");
                        run.Foreground = Brushes.LightYellow;
                        ItemInfoBox.Inlines.Add(run);
                    }
                }
            }
            */

        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SoundPlayer player = new SoundPlayer("Sounds/menu_up.wav");
            player.Load();
            player.Play();
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
                    ShowStations(itemID,null);
                    GameInfoBox(lbi.Content.ToString());
                    chosenGame = lbi.Content.ToString();
                }
                if (itemType == "S") //station
                {
                    lbA.Items.Clear();
                    itemID = Convert.ToInt32(lbi.Name.Substring(9, (lbi.Name.Length - 9)));
                    ShowTracks(itemID, null);
                    StationInfoBox(lbi.Content.ToString());
                }
            }
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            SoundPlayer player = new SoundPlayer("Sounds/menu_down.wav");
            player.Load();
            player.Play();
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
                GameInfoBox(chosenGame);
                TextBoxStationInfo(buttonStationID);
                ShowStations(buttonGameID, null);
            }
            if (buttonGameID == 0)
            {
                if (buttonStationID == 0)
                {
                    ShowGames();
                }
                else
                {
                    Search();
                }
            }  
        }

        private void MenuButtonClick(object sender, RoutedEventArgs e)
        {
            SoundPlayer player = new SoundPlayer("Sounds/menu_down.wav");
            player.Load();
            player.Play();
            ItemInfoBox.Inlines.Clear();
            BackButton.IsEnabled = false;
            StartInfoBox();
            StartItemInfoBox();
            ShowGames();
        }

        private void QuitButtonClick(object sender, RoutedEventArgs e)
        {
            SoundPlayer player = new SoundPlayer("Sounds/menu_down.wav");
            player.Load();
            player.Play();
            Close();
        }
        
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer player = new SoundPlayer("Sounds/menu_up.wav");
            player.Load();
            player.Play();
            Search();
        }

        public void StartInfoBox()
        {
            InfoBox.Text = "Welcome to application \"Radio stations of the games of Grand Theft Auto series\". Choose a game, then a station or search station or a track.";
        }

        public void GameInfoBox(string game)
        {
            InfoBox.Text = "List of radio stations in \"" + game + "\"";
        }

        public void StationInfoBox(string station)
        {
            string firstChar = station.Substring(0, 1);
            try
            {
                Convert.ToInt32(firstChar);
                InfoBox.Text = "List of tracks in \"" + station.Substring(station.IndexOf('.') + 2) + "\" station";
            }
            catch
            {
                InfoBox.Text = "List of tracks in \"" + station + "\" station";
            }
            
        }

        public void SearchInfoBox(string searchPhrase)
        {
            InfoBox.Text = "Search results for phrase \"" + searchPhrase + "\"";
        }
        public void StartItemInfoBox()
        {
            var CoverImage = "Covers/Basics/mainlogo.png";
            CoverPlace.Source = new BitmapImage(new Uri(CoverImage, UriKind.RelativeOrAbsolute));
            AltText.Visibility = Visibility.Hidden;
        }

        public void MessageWrongPhrase()
        {
            messageCount++;
            if (messageCount == 2)
            {
                ShowGames();
                MessageBox.Show("Nieprawidłowa nazwa");
                messageCount = 0;
            }
        }

        public void MessageProgramError()
        {
            MessageBox.Show("Program error");
        }

        private void ListBoxItem_MouseEnter(object sender, MouseEventArgs e)
        {
            MediaPlayer mp = new MediaPlayer();
            mp.Open(new Uri("Sounds/menu_enterMouse.wav", UriKind.RelativeOrAbsolute));
            mp.Play();
        }

        private void SearchBoxTextChange(object sender, TextChangedEventArgs e)
        {
            MediaPlayer mp = new MediaPlayer();
            mp.Open(new Uri("Sounds/menu_enterMouse.wav",UriKind.RelativeOrAbsolute));
            mp.Play();
        }
    } 
}
