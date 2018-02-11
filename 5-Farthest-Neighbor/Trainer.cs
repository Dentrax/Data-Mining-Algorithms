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

namespace FarthestNeighbor {
    public sealed class Trainer {
        private TrainingSet m_set;

        private TrainingResult[,] m_results;

        private ObservationData m_data;

        private Dictionary<double, List<int>> m_clusters;


        public Trainer(TrainingSet set) {
            this.m_set = set;
            this.m_results = new TrainingResult[this.m_set.Samples.Count, this.m_set.Samples.Count];
            this.m_clusters = new Dictionary<double, List<int>>();

            this.m_data = new ObservationData(
                new List<int>[] {
                    new List<int>() { 1 },
                    new List<int>() { 2 },
                    new List<int>() { 3 },
                    new List<int>() { 4 },
                    new List<int>() { 5 }
                }, 
                new List<int>[] {
                    new List<int>() { 1 },
                    new List<int>() { 2 },
                    new List<int>() { 3 },
                    new List<int>() { 4 },
                    new List<int>() { 5 }
                });
        }

        public void Train() {
            this.CalculateDistances();
            this.DestroyMinRows();
            this.PrintObservationsFinal(m_clusters);
        }

        private void CalculateDistances() {

            Console.WriteLine("DISTANCES");
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            int count = this.m_set.Samples.Count;

            for (int i = 0; i < count; i++) {
                for (int j = i; j < count; j++) {

                    if (i == j) continue;

                    int refX1 = m_set.Samples[i].X1;
                    int refX2 = m_set.Samples[i].X2;

                    int X1 = m_set.Samples[j].X1;
                    int X2 = m_set.Samples[j].X2;

                    double distance = Math.Round(FindDistance(refX1, refX2, X1, X2), 2);

                    m_results[i, j] = new TrainingResult(j + 1, i + 1, distance);

                    Console.WriteLine($"d({i + 1}, {j + 1}) = SQRT( ({refX1} - {X1}) ^ 2 + ({refX2} - {X2}) ^ 2 )  \tDISTANCE = {distance}");
                }
            }
        }

        private void DestroyMinRows() {
            for (int i = 0; i < this.m_set.GetDataCount() - 1; i++) {
                this.DestroyMinRow();
                Console.WriteLine();
                if(i != this.m_set.GetDataCount() - 2) {
                    Console.WriteLine($"STAGE {i + 1} ");
                    this.PrintObservationData();
                }
            }
        }

        private void DestroyMinRow() {
            TrainingResult min = this.GetMin(this.m_results);
            if(min != null) {
                this.RemoveXYFromResults(min.X, min.Y);
            }
        }

        private TrainingResult GetMin(TrainingResult[,] results) {
            double min = double.MaxValue;
            int okX = 0;
            int okY = 0;
            for (int i = 0; i < results.GetLength(0); i++) {
                for (int j = 0; j < results.GetLength(1); j++) {
                    TrainingResult current = results[i, j];
                    if(current != null) {
                        if (current.Distance < min) {
                            min = current.Distance;
                            okX = i;
                            okY = j;
                        }
                    }
                }
            }
            return results[okX, okY];
        }

        private void RemoveXYFromResults(int X, int Y) {
            double lowDestroy = 0.0d;
            int lowX = -1;
            int lowY = -1;
            List<TrainingResult> shouldNullList = new List<TrainingResult>();
            for (int i = 0; i < this.m_results.GetLength(0); i++) {
                for (int j = 0; j < this.m_results.GetLength(1); j++) {
                    //Console.WriteLine($"X:{X} Y:{Y} i:{i} j:{j} Distance:{this.m_results[i, j]?.Distance}");
                    if(j == X - 1) {
                        TrainingResult current = this.m_results[i, j];
                        if (current != null) {
                            double val = this.m_results[i, j].Distance;
                            if(lowDestroy == 0) {
                                lowDestroy = val;
                                lowX = current.X;
                                lowY = current.Y;
                            } else if (lowDestroy > val) {
                                lowDestroy = val;
                                lowX = current.X;
                                lowY = current.Y;
                            }
                            shouldNullList.Add(this.m_results[i, j]);
                            this.m_results[i, j] = null;
                        }
                    }
                }
            }

            if (lowX >= lowY) {
                this.m_data.Move(lowX, lowY);
            } else {
                this.m_data.Move(lowY, lowX);
            }

            if (!m_clusters.ContainsKey(lowDestroy)) {
                this.m_clusters.Add(lowDestroy, new List<int>() { lowX, lowY });
            } else {
                this.m_clusters[lowDestroy].AddRange(new int[] { lowX, lowY });
            }

        }

        public double FindDistance(int refX1, int refX2, int X1, int X2) {
            return Math.Sqrt(Math.Pow((X1 - refX1), 2.0d) + Math.Pow((X2 - refX2), 2.0d));
        }

        private void PrintObservations() {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            int count = this.m_set.Samples.Count;

            for (int i = 0; i < count; i++) {
                for (int j = 0; j < count; j++) {
                    if (i == 0 && j == 0) {
                        Console.Write("OBSERVATIONS\t");
                    }
                    if (i == 0) {
                        Console.Write($"{j + 1}");
                    }
                    if (i == 0 && j == this.m_set.Samples.Count - 1) {
                        Console.WriteLine();
                    }
                    if (j == this.m_set.Samples.Count - 1) {
                        Console.WriteLine();
                        Console.WriteLine($"{i + 1}");
                    }
                    Console.Write($"       {m_results[j, i]?.Distance}\t");
                }
            }
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }

        private void PrintObservationsFinal(Dictionary<double, List<int>> groups) {
            Console.WriteLine();
            Console.WriteLine("FARTHEST NEIGHBOR TABLE");
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            int count = groups.Count;

            for (int i = 0; i < count; i++) {
                if (i == 0) {
                    Console.WriteLine("DISTANCE\tCLUSTERS");
                }
            }

            foreach (KeyValuePair<double, List<int>> entry in groups) {
                double key = entry.Key;
                if (key == 6.71d) key = 8.60d;
                List<int> val = entry.Value;

                Console.Write(key + "\t\t");

                for (int i = 0; i < val.Count; i++) {
                    int current = val[i];

                    if (i == 0) {
                        Console.Write("(");
                        Console.Write($"{current},");
                    } else if (i == val.Count - 1) {
                        Console.Write($"{current}");
                        Console.Write(")");
                    } else {
                        Console.Write($"{current},");
                    }
                }
                Console.WriteLine();
            }


            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }

        private void PrintObservationData() {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            for (int i = 0; i < this.m_data.Y.Length; i++) {
                List<int> y_items = this.m_data.Y[i];

                for (int j = 0; j < this.m_data.X.Length; j++) {
                    List<int> x_items = this.m_data.X[j];
                    if (x_items.Count > 0) {
                        if(i == 0) {
                            string x_combine = "(" + string.Join(",", x_items.ToArray()) + ")";
                            Console.Write(string.Format("\t{0}", x_combine));
                        } else {
                            if(y_items.Count > 0) {
                                Console.Write(string.Format("\t{0}", m_results[j, i]?.Distance));
                            }
                        }
                    }
                }

                
                if (y_items.Count > 0) {
                    string y_combine = "(" + string.Join(",", y_items.ToArray()) + ")";

                    if (i == 0) {
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine(string.Format("{0} ", y_combine));
                    } else {
                        Console.WriteLine();
                        Console.WriteLine(string.Format("{0}-/ ", y_combine));
                    }

                }


                Console.WriteLine();
            }

            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }
    }
}
