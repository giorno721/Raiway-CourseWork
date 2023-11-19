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
    /// Interaction logic for AddRouteWindow.xaml
    /// </summary>
    public partial class AddRouteWindow : Window
    {
        internal Train NewTrain { get; private set; }
        public event EventHandler ButtonClickEvent;

        public AddRouteWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Перевірка на пусті значення для полів
            if (string.IsNullOrWhiteSpace(NumberTextBox.Text) ||
                string.IsNullOrWhiteSpace(StartStationTextBox.Text) ||
                string.IsNullOrWhiteSpace(EndStationTextBox.Text) ||
                string.IsNullOrWhiteSpace(IntermediateStationsTextBox.Text) ||
                string.IsNullOrWhiteSpace(DepartureDatePicker.Text) ||
                string.IsNullOrWhiteSpace(ArrivalDatePicker.Text) ||
                string.IsNullOrWhiteSpace(DistanceTextBox.Text))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Читання даних з вікна
            if (!int.TryParse(NumberTextBox.Text, out int number))
            {
                MessageBox.Show("Невірний формат номера потягу", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string startStation = StartStationTextBox.Text;
            string endStation = EndStationTextBox.Text;

            // Перевірка на відсутність проміжних станцій
            List<string> intermediateStations = new List<string>(IntermediateStationsTextBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            if (!DateTime.TryParse(DepartureDatePicker.SelectedDate?.ToString(), out DateTime departureDate))
            {
                MessageBox.Show("Невірний формат часу відправлення", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(DepartureHourTextBox.Text, out int departureHour) ||
                !int.TryParse(DepartureMinuteTextBox.Text, out int departureMinute) ||
                !int.TryParse(DepartureSecondTextBox.Text, out int departureSecond))
            {
                MessageBox.Show("Невірний формат часу відправлення", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!DateTime.TryParse(ArrivalDatePicker.SelectedDate?.ToString(), out DateTime arrivalDate))
            {
                MessageBox.Show("Невірний формат часу прибуття", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(ArrivalHourTextBox.Text, out int arrivalHour) ||
                !int.TryParse(ArrivalMinuteTextBox.Text, out int arrivalMinute) ||
                !int.TryParse(ArrivalSecondTextBox.Text, out int arrivalSecond))
            {
                MessageBox.Show("Невірний формат часу прибуття", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Перевірка часу виїзду
            if (departureHour < 0 || departureHour >= 24 ||
                departureMinute < 0 || departureMinute >= 60 ||
                departureSecond < 0 || departureSecond >= 60)
            {
                MessageBox.Show("Невірний формат часу відправлення", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Перевірка часу прибуття
            if (arrivalHour < 0 || arrivalHour >= 24 ||
                arrivalMinute < 0 || arrivalMinute >= 60 ||
                arrivalSecond < 0 || arrivalSecond >= 60)
            {
                MessageBox.Show("Невірний формат часу прибуття", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Перевірка, щоб дата прибуття була не раніше за дату виїзду
            DateTime departureTime = new DateTime(
                departureDate.Year,
                departureDate.Month,
                departureDate.Day,
                departureHour,
                departureMinute,
                departureSecond
            );

            DateTime arrivalTime = new DateTime(
                arrivalDate.Year,
                arrivalDate.Month,
                arrivalDate.Day,
                arrivalHour,
                arrivalMinute,
                arrivalSecond
            );
            if (arrivalTime < departureTime)
            {
                MessageBox.Show("Дата прибуття не може бути раніше за дату виїзду", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!double.TryParse(DistanceTextBox.Text, out double distance))
            {
                MessageBox.Show("Невірний формат відстані", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Створення нового потягу
            NewTrain = new Train
            {
                Number = number,
                StartStation = startStation,
                EndStation = endStation,
                IntermediateStations = intermediateStations,
                DepartureTime = departureTime,
                ArrivalTime = arrivalTime,
                Distance = distance
            };

            MessageBox.Show("Новий потяг успішно додано!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

            // Виклик події для оновлення інтерфейсу
            ButtonClickEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
