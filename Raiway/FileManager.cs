using Railway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Raiway
{
    internal class FileManager
    {
        private string pathToRead;
        private string pathToWrite;
        private List<Train> trains = new List<Train>();

        public FileManager(List<Train> trains, string pathToRead)
        {
            this.trains = trains;
            this.pathToRead = pathToRead;
        }

        public FileManager(List<Train> trains, string pathToWrite, int k)
        {
            this.trains = trains;
            this.pathToWrite = pathToWrite;
        }

        public FileManager(List<Train> trains)
        {
            this.trains = trains;
        }

        public List<Train> ReadList()
        {
            List<Train> trains = new List<Train>();

            try
            {
                string[] lines = System.IO.File.ReadAllLines(pathToRead);

                if (lines.Length == 0)
                {
                    MessageBox.Show("Файл порожній.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }

                for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
                {
                    string line = lines[lineNumber];
                    string[] words = line.Split(' ');
                    Train train = new Train();

                    try
                    {
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
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка читання файлу у рядку {lineNumber + 1}: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка читання файлу: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            return trains;
        }



        public void PrintList()
        {
            try
            {
                string[] lines = new string[trains.Count];

                for (int i = 0; i < trains.Count; i++)
                {
                    string line = "";
                    line += trains[i].Number + " ";
                    line += trains[i].StartStation + " ";
                    line += trains[i].EndStation + " ";

                    for (int j = 0; j < trains[i].IntermediateStations.Count; j++)
                    {
                        line += trains[i].IntermediateStations[j] + ",";
                    }

                    line = line.Remove(line.Length - 1);
                    line += " ";
                    line += trains[i].DepartureTime.Year + ".";
                    line += trains[i].DepartureTime.Month + ".";
                    line += trains[i].DepartureTime.Day + ".";
                    line += trains[i].DepartureTime.Hour + ".";
                    line += trains[i].DepartureTime.Minute + ".";
                    line += trains[i].DepartureTime.Second + " ";
                    line += trains[i].ArrivalTime.Year + ".";
                    line += trains[i].ArrivalTime.Month + ".";
                    line += trains[i].ArrivalTime.Day + ".";
                    line += trains[i].ArrivalTime.Hour + ".";
                    line += trains[i].ArrivalTime.Minute + ".";
                    line += trains[i].ArrivalTime.Second + " ";
                    line += trains[i].Distance;
                    lines[i] = line;
                }

                System.IO.File.WriteAllLines(pathToWrite, lines);
                MessageBox.Show("Файл записано успішно.", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка запису файлу: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
