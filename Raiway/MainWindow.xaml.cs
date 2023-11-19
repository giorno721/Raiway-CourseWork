using Microsoft.Win32;
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
using System.IO;
using System.Windows.Threading;

namespace Railway
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Train> trains;
        string fileReadPath = System.IO.Path.Combine(@"C:\Users\Roman PC\Desktop\Палітєх 2-й курс\Курсач\Raiway\Raiway\bin\Debug", "trainsRead.txt");
        //string fileReadPath = System.IO.Path.Combine(@"C:\Users\Roman PC\Source\Repos\Raiway-CourseWork\Raiway\bin\Debug", "trainsRead.txt");

        // ІНціалізація головного вікна
        public MainWindow()
        {
            FileManager fileManager = new FileManager(trains,fileReadPath);
            trains = fileManager.ReadList();
            InitializeComponent();
            Train.DisplayListOfTrainsInDataGrid(trains, trainDataGrid);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        // Оновлення годинника
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            ClockTextBox.Text = DateTime.Now.ToString("G");
        }
        // Сортування за назвою початкової станції
        private void sortByStartStationButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (trains == null || trains.Count == 0)
                {
                    throw new InvalidOperationException("Розклад пустий!");
                }
            }
            catch (InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Помилка: " + ex.Message, "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            trains = MergeSort.Sort(trains);
            Train.DisplayListOfTrainsInDataGrid(trains, trainDataGrid);
        }
        // Сортування за середньою швидкістю
        private void sortByAverageSpeedStationButton_Checked(object sender, RoutedEventArgs e)
        {
            trains = Train.SortTrainsByAverageSpeed(trains);         
            Train.DisplayListOfTrainsInDataGrid(trains, trainDataGrid);
        }
        // Отримання потягів, які виїжджають зі станції
        private void getTrainsLeaveStationButton_Checked(object sender, RoutedEventArgs e)
        {
            // Створення та відображення вікна пошуку потягів за відправною станцією
            SearchLeaveTrainWindow searchLeaveTrainWindow = new SearchLeaveTrainWindow(trainDataGrid,trains);
            searchLeaveTrainWindow.ButtonClickEvent += (s, args) =>
            {
                
                trains = searchLeaveTrainWindow.getList();
            };
            searchLeaveTrainWindow.Show();
          
        }
        // Отримання потягів, які прибувають на станцію
        private void getTrainsArriveStationButton_Checked(object sender, RoutedEventArgs e)
        {
            // Створення та відображення вікна пошуку потягів за призначеною станцією
            SearchArriveTrainWindow searchArriveTrainWindow = new SearchArriveTrainWindow(trainDataGrid, trains);
            searchArriveTrainWindow.ButtonClickEvent += (s, args) =>
            {
                trains = searchArriveTrainWindow.getList();
            };
            searchArriveTrainWindow.Show();
        }
        // Отримання потягів, які проходять через станцію
        private void getTrainsThroughStationButton_Checked(object sender, RoutedEventArgs e)
        {
            List<Train> copyTrains = trains;
            // Створення та відображення вікна пошуку потягів за проміжною станцією
            SearchTrainWindow searchTrainWindow = new SearchTrainWindow(trainDataGrid, copyTrains);
            searchTrainWindow.ButtonClickEvent += (s, args) =>
            {
                copyTrains = searchTrainWindow.getList();
            };
            searchTrainWindow.Show();
        }
        // Групування потягів за номером та проміжними станціями
        private void groupTrainsButton_Checked(object sender, RoutedEventArgs e)
        {
            // Групування потягів за номером та проміжними станціями
            trains = Train.GroupTrains(trains);
            // Відображення відгрупованого списку у DataGrid
            Train.DisplayListOfTrainsInDataGrid(trains, trainDataGrid);
        }

        // Додавання нового потягу
        private void addTrainButton_Checked(object sender, RoutedEventArgs e)
        {
            AddRouteWindow addRouteWindow = new AddRouteWindow();
            addRouteWindow.ButtonClickEvent += (s, args) =>
            {
                Train train = addRouteWindow.NewTrain;
                trains.Add(train);
                Train.DisplayListOfTrainsInDataGrid(trains, trainDataGrid);
            };
            addRouteWindow.Show();
        }
        // Відкриття списку потягів з файлу
        private void openListFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                string file_name = openFileDialog.FileName;
                FileManager fileManager = new FileManager(trains, file_name);
                trains = fileManager.ReadList();
                Train.DisplayListOfTrainsInDataGrid(trains, trainDataGrid);
            }
         
        }
        // Збереження списку потягів у файл
        private void saveListToFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string file_name = openFileDialog.FileName;
                FileManager fileManager = new FileManager(trains, file_name,0);
                fileManager.PrintList();
            }
        }
        // Вихід з програми
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        // Відкриття вікна з інформацією
        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            openListFromFile_Click(sender, e);
        }
        // Мінімізація вікна
        private void SaveAsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            saveListToFile_Click((object)sender, e);
        }
        // Закриття програми
        private void CloseCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            exit_Click(sender, e);
        }

        // Відкриття вікна з інформацією
        private void info_Click(object sender, RoutedEventArgs e)
        {
            InformationMenu informationMenu = new InformationMenu();
            informationMenu.Show();
        }
        // Закриття програми
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            exit_Click(sender, e);
        }
        // Мінімізація вікна
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        // Сортування за назвою початкової станції (команда)
        private void sortByLeaveStation_Click(object sender, RoutedEventArgs e)
        {
            sortByStartStationButton_Checked(sender, e);
        }
        // // Сортування за середньою швидкістю (команда)
        private void sortByAverageSpeed_Click(object sender, RoutedEventArgs e)
        {
            sortByAverageSpeedStationButton_Checked(sender, e);
        }

        // Сортування за назвою кінцевої станції (команда)
        private void getTrainsLeaveStationButton_Click(object sender, RoutedEventArgs e)
        {
            getTrainsLeaveStationButton_Checked(sender, e);
        }
        // Отримання потягів, які виїжджають зі станції (команда)
        private void getTrainsArriveStationButton_Click(object sender, RoutedEventArgs e)
        {
            getTrainsArriveStationButton_Checked(sender, e);
        }
        // Отримання потягів, які проходять через станцію (команда)
        private void getTrainsThroughStationButton_Click(object sender, RoutedEventArgs e)
        {
            getTrainsThroughStationButton_Checked(sender, e);
        }
        // Групування потягів (команда)
        private void groupTrainsButton_Click(object sender, RoutedEventArgs e)
        {
            groupTrainsButton_Checked(sender, e);
        }
        // Додавання нового потягу (команда)
        private void addTrainButton_Click(object sender, RoutedEventArgs e)
        {
            addTrainButton_Checked(sender, e);
        }
    }
}
