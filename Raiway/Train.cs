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
        public Train(int number, string startStation, string endStation, List<string> intermediateStations, DateTime departureTime, DateTime arrivalTime, double distance)
        {
            Number = number;
            StartStation = startStation;
            EndStation = endStation;
            IntermediateStations = intermediateStations ?? new List<string>();
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            Distance = distance;
        }
        public Train() { }

        public static List<Train> ReadTrainsFromFile()
        {
            List<Train> trains = new List<Train>();
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Roman PC\Desktop\Палітєх 2-й курс\Курсач\Raiway\Raiway\bin\Debug\trains.txt");
            foreach (string line in lines)
            {
                string[] words = line.Split(' ');
                Train train = new Train();
                train.Number = Convert.ToInt32(words[0]);
                train.StartStation = words[1];
                train.EndStation = words[2];
                train.IntermediateStations = words[3].Split(',').ToList();
                string[] departureDateTime = words[4].Split('.');
                train.DepartureTime = new DateTime(
                    Convert.ToInt32(departureDateTime[0]),
                    Convert.ToInt32(departureDateTime[1]),
                    Convert.ToInt32(departureDateTime[2]),
                    Convert.ToInt32(departureDateTime[3]),
                    Convert.ToInt32(departureDateTime[4]),
                    Convert.ToInt32(departureDateTime[5])
                );

                string[] arrivalDateTime = words[5].Split('.');
                train.ArrivalTime = new DateTime(
                    Convert.ToInt32(arrivalDateTime[0]),
                    Convert.ToInt32(arrivalDateTime[1]),
                    Convert.ToInt32(arrivalDateTime[2]),
                    Convert.ToInt32(arrivalDateTime[3]),
                    Convert.ToInt32(arrivalDateTime[4]),
                    Convert.ToInt32(arrivalDateTime[5])
                );
                train.Distance = Convert.ToDouble(words[6]);
                trains.Add(train);
            }
            return trains;
        }
        public static void DisplayListOfTrainsInDataGrid(List<Train> trains, DataGrid trainDataGrid)
        {
            trainDataGrid.ItemsSource = null;
            trainDataGrid.ItemsSource = trains;
        }
        public static double getAverageSpeed(Train train)
        {
            return train.Distance / (train.DepartureTime.Hour + (double)train.DepartureTime.Minute / 60);
        }

        public static List<Train> SortTrainsByAverageSpeed(List<Train> trains)
        {
            trains.Sort((train1, train2) => getAverageSpeed(train1).CompareTo(getAverageSpeed(train2)));
            return trains;
        }
        public static List<Train> SearchLeaveTrainsThroughTime(List<Train> trains, string enteredText, DateTime startDateTime, DateTime endDateTime)
        {
            var result = trains
                .Where(train => train.StartStation.Equals(enteredText, StringComparison.OrdinalIgnoreCase) &&
                                train.DepartureTime >= startDateTime &&
                                train.DepartureTime <= endDateTime)
                .ToList();

            return result;
        }
        public static List<Train> SearchArriveTrainsThroughTime(List<Train> trains, string enteredText, DateTime startDateTime, DateTime endDateTime)
        {
            var result = trains
                .Where(train => train.EndStation.Equals(enteredText, StringComparison.OrdinalIgnoreCase) &&
                                train.DepartureTime >= startDateTime &&
                                train.DepartureTime <= endDateTime)
                .ToList();

            return result;
        }
        public static List<Train> SearchThroughStations(List<Train> trains, string enteredText)
        {
            var result = trains
                .Where(train => train.StartStation.Equals(enteredText, StringComparison.OrdinalIgnoreCase) ||
                                               train.EndStation.Equals(enteredText, StringComparison.OrdinalIgnoreCase) ||
                                                                              train.IntermediateStations.Contains(enteredText, StringComparer.OrdinalIgnoreCase))
                .ToList();
            return result;
        }
        //mthd 
        public static List<Train> GroupTrains(List<Train> trains)
        {
            List<Train> groupedTrains = new List<Train>();

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
            return groupedTrains;
        }


        public static List<Train> getListOfTrains(List<Train> trains)
        {
            return trains;
        }
    }
}
