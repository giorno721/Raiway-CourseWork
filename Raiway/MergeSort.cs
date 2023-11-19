using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Railway
{
    internal class MergeSort
    {
        // Сортування злиттям
        public static List<Train> Sort(List<Train> trains)
        {
            if (trains.Count <= 1)
            {
                return trains;
            }

            int middle = trains.Count / 2;
            List<Train> left = new List<Train>();
            List<Train> right = new List<Train>();

            for (int i = 0; i < middle; i++)
                left.Add(trains[i]);

            for (int i = middle; i < trains.Count; i++)
                right.Add(trains[i]);

            left = Sort(left);
            right = Sort(right);

            return Merge(left, right);
        }

        // Злиття
        private static List<Train> Merge(List<Train> left, List<Train> right)
        {
            List<Train> result = new List<Train>();
            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (string.Compare(left[0].StartStation, right[0].StartStation) <= 0)
                    {
                        result.Add(left[0]);
                        left.RemoveAt(0);
                    }
                    else
                    {
                        result.Add(right[0]);
                        right.RemoveAt(0);
                    }
                }
                else if (left.Count > 0)
                {
                    result.Add(left[0]);
                    left.RemoveAt(0);
                }
                else if (right.Count > 0)
                {
                    result.Add(right[0]);
                    right.RemoveAt(0);
                }
            }

            return result;
        }
    }
}
