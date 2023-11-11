using Raiway;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Railway
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Train> trains;
        public MainWindow()
        {
            trains = Train.ReadTrainsFromFile();
            InitializeComponent();
            Train.DisplayListOfTrainsInDataGrid(trains, trainDataGrid);
        }

        private void sortByStartStationButton_Checked(object sender, RoutedEventArgs e)
        {
            trains = MergeSort.Sort(trains);
            Train.DisplayListOfTrainsInDataGrid(trains, trainDataGrid);
        }

        private void sortByAverageSpeedStationButton_Checked(object sender, RoutedEventArgs e)
        {
            trains = Train.SortTrainsByAverageSpeed(trains);
            Train.DisplayListOfTrainsInDataGrid(trains, trainDataGrid);
        }

        private void getTrainsLeaveStationButton_Checked(object sender, RoutedEventArgs e)
        {
            SearchLeaveTrainWindow searchLeaveTrainWindow = new SearchLeaveTrainWindow(trainDataGrid,trains);
            searchLeaveTrainWindow.ButtonClickEvent += (s, args) =>
            {
                
                trains = searchLeaveTrainWindow.getList();
            };
            searchLeaveTrainWindow.Show();
          
        }

        private void getTrainsArriveStationButton_Checked(object sender, RoutedEventArgs e)
        {
            SearchArriveTrainWindow searchArriveTrainWindow = new SearchArriveTrainWindow(trainDataGrid, trains);
            searchArriveTrainWindow.ButtonClickEvent += (s, args) =>
            {
                trains = searchArriveTrainWindow.getList();
            };
            searchArriveTrainWindow.Show();
        }

        private void getTrainsThroughStationButton_Checked(object sender, RoutedEventArgs e)
        {
            SearchTrainWindow searchTrainWindow = new SearchTrainWindow(trainDataGrid, trains);
            searchTrainWindow.ButtonClickEvent += (s, args) =>
            {
                trains = searchTrainWindow.getList();
            };
            searchTrainWindow.Show();
        }

        private void groupTrainsButton_Checked(object sender, RoutedEventArgs e)
        {
            trains = Train.GroupTrains(trains);
            Train.DisplayListOfTrainsInDataGrid(trains, trainDataGrid);
        }

        private void getFileListButton_Checked(object sender, RoutedEventArgs e)
        {
            trains = Train.ReadTrainsFromFile();
            Train.DisplayListOfTrainsInDataGrid(trains, trainDataGrid);
        }
    }
}
