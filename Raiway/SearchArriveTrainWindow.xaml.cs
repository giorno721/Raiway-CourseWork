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
    /// Interaction logic for SearchArriveTrainWindow.xaml
    /// </summary>
    public partial class SearchArriveTrainWindow : Window
    {
        private DataGrid _trainDataGrid;
        private List<Train> _trains;
        public event EventHandler ButtonClickEvent;
        public SearchArriveTrainWindow(DataGrid trainDataGrid, List<Train> trains)
        {
            InitializeComponent();
            _trainDataGrid = trainDataGrid;
            _trains = trains;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string stationName = StationTextBox.Text;
            DateTime startDate = StartDatePicker.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = EndDatePicker.SelectedDate ?? DateTime.MaxValue;
            SearchTrains(stationName, startDate, endDate);
            ButtonClickEvent?.Invoke(this, EventArgs.Empty);
        }

        private void SearchTrains(string stationName, DateTime startDate, DateTime endDate)
        {
            _trains = Train.SearchArriveTrainsThroughTime(_trains, stationName, startDate, endDate);
            Train.DisplayListOfTrainsInDataGrid(_trains, _trainDataGrid);
        }
        public List<Train> getList()
        {
            return _trains;
        }
    }
}
