using Railway;
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

namespace Raiway
{
    /// <summary>
    /// Interaction logic for SearchTrainWindow.xaml
    /// </summary>
    public partial class SearchTrainWindow : Window
    {
        private DataGrid _trainDataGrid;
        private List<Train> _trains;
        public event EventHandler ButtonClickEvent;
        public SearchTrainWindow(DataGrid dataGrid, List<Train> trains)
        {
            InitializeComponent();
            _trainDataGrid = dataGrid;
            _trains = trains;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string stationName = StationTextBox.Text;         
            SearchTrains(stationName);
            ButtonClickEvent?.Invoke(this, EventArgs.Empty);
        }
        private void SearchTrains(string stationName)
        {
            _trains = Train.SearchThroughStations(_trains, stationName);
            Train.DisplayListOfTrainsInDataGrid(_trains, _trainDataGrid);
        }
        public List<Train> getList()
        {
            return _trains;
        }
    }
}
