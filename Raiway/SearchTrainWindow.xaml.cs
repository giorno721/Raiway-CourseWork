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
        // Ініціалізація вікна пошуку поїздів
        public SearchTrainWindow(DataGrid dataGrid, List<Train> trains)
        {
            InitializeComponent();
            _trainDataGrid = dataGrid;           
            _trains = trains;
        }
        // Кнопка пошуку поїздів
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string stationName = StationTextBox.Text;         
            SearchTrains(stationName);
            ButtonClickEvent?.Invoke(this, EventArgs.Empty);
        }
        // Пошук поїздів
        private void SearchTrains(string stationName)
        {
            string fileReadPath = System.IO.Path.Combine(@"C:\Users\Roman PC\Desktop\Палітєх 2-й курс\Курсач\Raiway\Raiway\bin\Debug", "trainsRead.txt");
            FileManager fileManager = new FileManager(_trains, fileReadPath);
            _trains = fileManager.ReadList();
            List<Train> CopyTrains = _trains;
            _trains = Train.SearchThroughStations(_trains, stationName);
            if(_trains == null)
            {            
                _trains = CopyTrains;
                return;
            }
            TrainsTextBox.Clear();
            if (_trains != null)
            {
                foreach (Train tr in _trains)
                {
                    TrainsTextBox.Text += tr.Number + " ";
                }
            }
            else
                return;
        }
        // Повернення списку поїздів
        public List<Train> getList()
        {
            return _trains;
        }
    }
}
