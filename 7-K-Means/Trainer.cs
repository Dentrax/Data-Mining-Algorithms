#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Linq;
using System.Text;

namespace KMeans {
    public sealed class Trainer {
        private TrainingSet m_set;

        private int m_k;

        public Trainer(TrainingSet set, int k) {
            this.m_set = set;
            this.m_k = k;
        }

        public void Train() {
            Console.ForegroundColor = ConsoleColor.White;
            PrintInit();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=============================================================================================");
            Console.WriteLine("STAGE 1");
            Console.WriteLine("=============================================================================================");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"M1 = [{CalculateMX(0)}, {CalculateMY(0)}]");
            Console.WriteLine($"M2 = [{CalculateMX(1)}, {CalculateMY(1)}]");
            Console.WriteLine($"e1^2 = {CalculateSubE(0)}");
            Console.WriteLine($"e2^2 = {CalculateSubE(1)}");
            Console.WriteLine($"E = {CalculateMasterE()}");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            CalculateStage();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=============================================================================================");
            Console.WriteLine("STAGE 2");
            Console.WriteLine("=============================================================================================");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"M1 = [{CalculateMX(0)}, {CalculateMY(0)}]");
            Console.WriteLine($"M2 = [{CalculateMX(1)}, {CalculateMY(1)}]");
            Console.WriteLine($"e1^2 = {CalculateSubE(0)}");
            Console.WriteLine($"e2^2 = {CalculateSubE(1)}");
            Console.WriteLine($"E = {CalculateMasterE()}");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            CalculateStage();

            Console.ResetColor();
        }

        private int GetMCount(int clusterIndex) {
            return this.m_set.Samples.Where(x => (int)x.ClusterType == clusterIndex).Count();
        }

        private double CalculateMX(int clusterIndex) {
            double temp = 0.0d;
            for (int i = 0; i < this.m_set.Samples.Count; i++) {
                TrainingSample sample = this.m_set.Samples[i];
                if((int)sample.ClusterType == clusterIndex) {
                    temp += sample.Variable1;
                }
            }
            return Math.Round(temp / GetMCount(clusterIndex), 2);
        }
        private double CalculateMY(int clusterIndex) {
            double temp = 0.0d;
            for (int i = 0; i < this.m_set.Samples.Count; i++) {
                TrainingSample sample = this.m_set.Samples[i];
                if ((int)sample.ClusterType == clusterIndex) {
                    temp += sample.Variable2;
                }
            }
            return Math.Round(temp / GetMCount(clusterIndex), 2);
        }


        private double CalculateSubE(int clusterIndex) {
            double temp = 0.0d;
           
            for (int i = 0; i < this.m_set.Samples.Count; i++) {
                TrainingSample sample = this.m_set.Samples[i];
                if ((int)sample.ClusterType == clusterIndex) {

                    int val1 = sample.Variable1;
                    int val2 = sample.Variable2;

                    double mx = CalculateMX(clusterIndex);
                    double my = CalculateMY(clusterIndex);

                    double distance = GetSqrtDistance(val1, mx, val2, my);

                    temp += distance;

                }
            }


            return temp;
        }

        private double CalculateMasterE() {
            double temp = 0.0d;
            int count = Enum.GetNames(typeof(ClusterType)).Length;
            for (int i = 0; i < count; i++) {
                temp += CalculateSubE(i);
            }
            return Math.Round(temp, 2);
        }

        private double GetSqrtDistance(int var1, double mx, int var2, double my) {
            return Math.Pow((double)var1 - mx, 2) + Math.Pow((double)var2 - my, 2);
        }

        private double GetOklidDistance(int var1, double mx, int var2, double my) {
            return Math.Round(Math.Sqrt(Math.Pow((double)var1 - mx, 2) + Math.Pow((double)var2 - my, 2)), 2);
        }

        private void PrintInit() {
            StringBuilder sb = new StringBuilder();
            int count = Enum.GetNames(typeof(ClusterType)).Length;
            sb.AppendLine("---------------------------------------------------------------------------------------------");
            sb.Append("Observations\tVar1\tVar2\tCluster");
            sb.AppendLine();
            sb.AppendLine("---------------------------------------------------------------------------------------------");
            for (int i = 0; i < this.m_set.Samples.Count; i++) {
                TrainingSample sample = this.m_set.Samples[i];
                sb.AppendLine($"{sample.Observation}\t\t{sample.Variable1}\t\t{sample.Variable2}\t\t{sample.ClusterType.ToString()}");
                sb.AppendLine();
            }
            sb.Append("---------------------------------------------------------------------------------------------");
            Console.WriteLine(sb.ToString());
        }

        private void CalculateStage() {
            TrainingSet temp = this.m_set;
            StringBuilder sb = new StringBuilder();
            int count = Enum.GetNames(typeof(ClusterType)).Length;
            sb.AppendLine("---------------------------------------------------------------------------------------------");
            sb.Append("Observations\t");
            for (int i = 0; i < count; i++) {
                sb.Append($"Distance from M{i+1}\t\t");
            }
            sb.Append("Cluster");
            sb.AppendLine();
            sb.AppendLine("---------------------------------------------------------------------------------------------");
            for (int i = 0; i < temp.Samples.Count; i++) {
                TrainingSample sample = temp.Samples[i];

                int val1 = sample.Variable1;
                int val2 = sample.Variable2;

                sb.Append(sample.Observation);
                sb.Append("\t\t");

                int lowestC = int.MaxValue;
                double lowestDis = double.MaxValue;
                for (int j = 0; j < count; j++) {
                    double mx = CalculateMX(j);
                    double my = CalculateMY(j);

                    double distance = GetOklidDistance(val1, mx, val2, my);

                    if(distance < lowestDis) {
                        lowestDis = distance;
                        lowestC = j;
                        ClusterType type = (ClusterType)Enum.ToObject(typeof(ClusterType), lowestC);
                        sample.SetClusterType(type);
                        this.m_set.Samples[i] = sample;
                    }

                    sb.Append($"d(M{j + 1}, {sample.Observation}) = {distance} \t");
                }
                sb.Append($"\tC{(int)sample.ClusterType + 1}");

                sb.AppendLine();
            }
            sb.Append("---------------------------------------------------------------------------------------------");
            Console.WriteLine(sb.ToString());
        }
    }
}
