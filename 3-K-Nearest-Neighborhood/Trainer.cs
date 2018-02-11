#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace KNearestNeighborhood {
    public sealed class Trainer {

        private TrainingSet m_set;

        private List<TrainingResult> m_results;

        private int m_orderCount = 0;

        private int m_refX1 = -1;
        private int m_refX2 = -1;

        public Trainer(TrainingSet set) {
            this.m_set = set;
            this.m_results = new List<TrainingResult>();
        }

        public void Train(int X1, int X2, int orderCount) {
            this.m_refX1 = X1;
            this.m_refX2 = X2;
            this.m_orderCount = orderCount;
            this.CalculateDistances(X1, X2);
            this.OrderDistances(orderCount);
        }

        private void CalculateDistances(int X1, int X2) {
            List<double> distances = new List<double>(this.m_set.Samples.Count);
            for (int i = 0; i < this.m_set.Samples.Count; i++) {
                TrainingSample sample = this.m_set.Samples[i];

                double distance = this.FindDistance(X1, X2, sample.X1, sample.X2);
                distances.Add(distance);
                m_results.Add(new TrainingResult(sample.X1, sample.X2, distance, i, sample.Output));
            }
        }

        private void OrderDistances(int count) {
            this.m_results = this.m_results.OrderByDescending(x => x.Distance).ToList();
            this.m_results.Reverse();
        }

        public double FindDistance(int refX1, int refX2, int X1, int X2) {
            return Math.Sqrt(Math.Pow((double)(X1 - refX1), 2.0d) + Math.Pow((double)(X2 - refX2), 2.0d));
        }

        public void PrintResult() {
            Console.WriteLine($"{m_set.Param1}\t\t\t {m_set.Param2}\t\t {m_set.Param3}\t\t\t ROW");
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            int totalTrue = 0;
            int totalFalse = 0;

            for (int i = 0; i < this.m_results.Count; i++) {
                TrainingResult result = this.m_results[i];

                if (result.Result) {
                    totalTrue++;
                } else {
                    totalFalse++;
                }

                Console.WriteLine($"{i}. ROW X1 = {result.X1},  \t X2 = {result.X2},\tVALUE = {result.Result.ToString().ToUpper()},\t\tDISTANCE = [{Math.Round(result.Distance, 4)}]");

                if(i == this.m_orderCount - 1) {
                    Console.WriteLine("---------------------------------------------------------------------------------------------");
                }
            }

            Console.WriteLine("---------------------------------------------------------------------------------------------");
            if (totalTrue > totalFalse) {
                Console.WriteLine($"RESULT = Because the total number of GOOD is more than BAD, [{m_refX1}, {m_refX2}] class of = [GOOD]");
            } else if (totalTrue < totalFalse) {
                Console.WriteLine($"RESULT = Because the total number of BAD is more than GOOD, [{m_refX1}, {m_refX2}] class of = [BAD]");
            } else {
                Console.WriteLine($"RESULT = Total GOOD number equals BAD");
            }
        }

    }
}
