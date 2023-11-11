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

        }
    }
}
