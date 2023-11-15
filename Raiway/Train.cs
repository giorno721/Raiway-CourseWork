using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Railway
{
    public class Train
    {
        public int Number { get; set; }
        public string StartStation { get; set; }
        public string EndStation { get; set; }
        public List<string> IntermediateStations { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public double Distance { get; set; }
        public double AverageSpeed { get; set; }
        public Train(int number, string startStation, string endStation, List<string> intermediateStations, DateTime departureTime, DateTime arrivalTime, double distance)
        {
            Number = number;
            StartStation = startStation;
            EndStation = endStation;
            IntermediateStations = intermediateStations ?? new List<string>();
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            Distance = distance;
            AverageSpeed = 0;
        }
        public Train() { }

        public static void DisplayListOfTrainsInDataGrid(List<Train> trains, DataGrid trainDataGrid)
        {
            trainDataGrid.ItemsSource = null;
            trainDataGrid.ItemsSource = trains;
        }

        public static double getAverageSpeed(Train train)
        {
            train.AverageSpeed = train.Distance/((train.ArrivalTime - train.DepartureTime).TotalHours);
            train.AverageSpeed = Math.Round(train.AverageSpeed, 2);
            return train.AverageSpeed;
        }

        public static List<Train> SortTrainsByAverageSpeed(List<Train> trains)
        {
            try
            {
                if (trains == null || trains.Count == 0)
                {
                    throw new InvalidOperationException("Розклад пустий!");
                }
                trains.Sort((train1, train2) => getAverageSpeed(train1).CompareTo(getAverageSpeed(train2)));
            }
            catch (InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Помилка: " + ex.Message, "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }          
            return trains;
        }
        public static List<Train> SearchLeaveTrainsThroughTime(List<Train> trains, string enteredText, DateTime startDateTime, DateTime endDateTime)
        {
            List<Train> copyOfTrains = trains;
            var result = new List<Train>();
            try
            {
                if (trains == null || trains.Count == 0)
                {
                    throw new InvalidOperationException("Розклад пустий!");
                }
                if(enteredText == null || enteredText == "" || enteredText == " ")
                {
                    throw new InvalidOperationException("Поле початкової станції пусте!");
                }
                if (startDateTime.ToString() == "01.01.0001 0:00:00" || endDateTime.ToString() == "31.12.9999 23:59:59")
                {
                    throw new InvalidOperationException("Поле дати пусте!");
                }
                if (startDateTime == (DateTime)endDateTime || startDateTime > endDateTime) 
                {
                    throw new InvalidOperationException("Невірно введено дату!");
                }
                result = trains
              .Where(train => train.StartStation.Equals(enteredText, StringComparison.OrdinalIgnoreCase) &&
                              train.DepartureTime >= startDateTime &&
                              train.DepartureTime <= endDateTime)
              .ToList();
                if(result.Count == 0) 
                {
                    throw new InvalidOperationException("Потягів із такими параметрами немає!");                
                }
            }
            catch (InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return copyOfTrains;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Помилка: " + ex.Message, "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return copyOfTrains;
            }
            return result;
        }
        public static List<Train> SearchArriveTrainsThroughTime(List<Train> trains, string enteredText, DateTime startDateTime, DateTime endDateTime)
        {
            List<Train> copyOfTrains = new List<Train>(trains);
            var result = new List<Train>();
            try
            {
                if (trains == null || trains.Count == 0)
                {
                    throw new InvalidOperationException("Розклад пустий!");
                }
                if (enteredText == null || enteredText.Trim() == "")
                {
                    throw new InvalidOperationException("Поле кінцевої станції пусте!");
                }
                if (startDateTime == DateTime.MinValue || endDateTime == DateTime.MaxValue)
                {
                    throw new InvalidOperationException("Поле дати пусте!");
                }
                if (startDateTime >= endDateTime)
                {
                    throw new InvalidOperationException("Невірно введено дату!");
                }

                result = trains
                    .Where(train => train.EndStation.Equals(enteredText, StringComparison.OrdinalIgnoreCase) &&
                                    train.ArrivalTime >= startDateTime &&
                                    train.ArrivalTime <= endDateTime)
                    .ToList();

                if (result.Count == 0)
                {
                    throw new InvalidOperationException("Потягів із такими параметрами немає!");
                }
            }
            catch (InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return copyOfTrains;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Помилка: " + ex.Message, "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return copyOfTrains;
            }
            return result;
        }

        public static List<Train> SearchThroughStations(List<Train> trains, string enteredText)
        {
            var result = new List<Train>();
            try
            {
                if (trains == null || trains.Count == 0)
                {
                    throw new InvalidOperationException("Розклад пустий!");
                }
                if (enteredText == null || enteredText.Trim() == "")
                {
                    throw new InvalidOperationException("Поле станції пусте!");
                }
                result = trains
                .Where(train => train.StartStation.Equals(enteredText, StringComparison.OrdinalIgnoreCase) ||
                                               train.EndStation.Equals(enteredText, StringComparison.OrdinalIgnoreCase) ||
                                                                              train.IntermediateStations.Contains(enteredText, StringComparer.OrdinalIgnoreCase))
                .ToList();
                if (result.Count == 0)
                {
                    throw new InvalidOperationException("Потягів із такими параметрами немає!");
                }
            }
            catch (InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Помилка: " + ex.Message, "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }
         

            return result;
        }
        public static List<Train> GroupTrains(List<Train> trains)
        {
            List<Train> groupedTrains = new List<Train>();
            List<Train> copyOfTrains = trains;

            try
            {
                if (trains == null || trains.Count == 0)
                {
                    throw new InvalidOperationException("Розклад пустий!");
                }
                foreach (Train train1 in trains)
                {
                    bool isGrouped = false;

                    foreach (Train train2 in groupedTrains)
                    {
                        if (train1.Number == train2.Number)
                        {
                            if (train1.IntermediateStations.Intersect(train2.IntermediateStations).Any() &&
                            (train1.IntermediateStations.Contains(train2.StartStation) || train1.IntermediateStations.Contains(train2.EndStation)))
                            {
                                isGrouped = true;
                                train2.StartStation = train1.StartStation;
                                train2.IntermediateStations = train2.IntermediateStations.Union(train1.IntermediateStations).ToList();
                                train2.EndStation = train1.EndStation;
                                train2.Distance = train1.Distance;
                                train2.DepartureTime = train1.DepartureTime;
                                train2.ArrivalTime = train1.ArrivalTime;
                                break;
                            }
                            else
                            {
                                isGrouped = true;
                            }
                        }
                    }

                    if (!isGrouped)
                    {
                        groupedTrains.Add(train1);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return copyOfTrains;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Помилка: " + ex.Message, "Помилка", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return copyOfTrains;
            }
            return groupedTrains;
        }


        public static List<Train> getListOfTrains(List<Train> trains)
        {
            return trains;
        }
    }
}
