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

namespace Apriori {
    public sealed class Trainer {
        private TrainingSet m_set;

        private int m_support;
        private int m_confidence;

        private int m_thresholdSupport;

        public Trainer(TrainingSet set, int support, int confidence) {
            this.m_set = set;
            this.m_support = support > 100 ? 100 : (support < 0 ? 0 : support);
            this.m_confidence = confidence > 100 ? 100 : (confidence < 0 ? 0 : confidence);

            this.m_thresholdSupport = this.m_set.Samples.Count * m_support / 100;
        }

        public void Train() {
            Dictionary<string, int> productCounts = this.CalculateProductCounts();

            Console.WriteLine($"support(threshold) = %{this.m_support}");
            Console.WriteLine($"trust(threshold)  = %{this.m_confidence}");
            Console.WriteLine();

            this.PrintCounts(productCounts, "Support Values");
            productCounts = this.RemoveThreshold(productCounts);
            this.PrintCounts(productCounts, "Products with equal to or greater than the threshold support value");

            Dictionary<string[], int> grouped = this.GroupProducts(productCounts);
            this.PrintGroups(grouped, "Support values for dual product groups");
            grouped = this.RemoveThreshold(grouped);
            this.PrintGroups(grouped, "Two product groups with equal to or greater than the threshold support value");
            grouped = this.MergeGroupProducts(grouped);
            this.PrintGroups(grouped, "Three product groups with equal to or greater than the threshold support value");
            this.PrintFinalValues(grouped);
        }

        private void PrintFinalValues(Dictionary<string[], int> group) {

            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("RESULTS");
            Console.WriteLine("--------------------");
            int index = 1;
            foreach (KeyValuePair<string[], int> product in group) {
                string[] keys = product.Key;

                for (int i = -1; i < keys.Length; i++) {
                    Console.Write($"RESULT {index} : ");

                    string[] ins;
                    string[] outs;

                    if (i == -1) {
                        ins = new string[] { keys[0], keys[1] };
                        outs = keys.Except(ins).ToArray();
                        PrintThresholdRule(keys, ins, outs);
                        index++;
                        Console.WriteLine();
                        continue;
                    }

                    ins = new string[] { keys[i] };
                    outs = keys.Except(ins).ToArray();

                    PrintThresholdRule(keys, ins, outs);

                    index++;
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }

        private void PrintThresholdRule(string[] keys, string[] ins, string[] outs) {
            int XYZ = GetGroupCountInSamples(keys);
            int N = GetGroupCountInSamples(ins);
            double result = (double)XYZ / (double)N * 100;
            Console.Write($"trust({string.Join(",", ins)} -> {string.Join(",", outs)})");
            Console.Write($"The probability of being [{string.Join(",", outs)}] on the product set [{string.Join(",", ins)}] \t%{result}");
        }

        private Dictionary<string[], int> MergeGroupProducts(Dictionary<string[], int> grouped) {
            Dictionary<string[], int> temp = new Dictionary<string[], int>(new ArrayComparer());
            List<string> datas = new List<string>();
            foreach (KeyValuePair<string[], int> main in grouped) {
                string[] keys = main.Key;
                for (int i = 0; i < keys.Length; i++) {
                    if (!datas.Contains(keys[i])) {
                        datas.Add(keys[i]);
                    }
                }
            }
            temp.Add(datas.ToArray(), GetGroupCountInSamples(datas.ToArray()));
            return temp;
        }

        private Dictionary<string[], int> GroupProducts(Dictionary<string, int> productCounts) {
            Dictionary<string[], int> temp = new Dictionary<string[], int>(new ArrayComparer());
            foreach (KeyValuePair<string, int> main in productCounts) {
                string mainKey = main.Key;
                foreach (KeyValuePair<string, int> sub in productCounts) {
                    string subKey = sub.Key;
                    if (!mainKey.Equals(subKey)) {
                        string[] head1 = new string[] { mainKey, subKey };
                        string[] head2 = head1.Reverse().ToArray();
                        if(!temp.ContainsKey(head1) && !temp.ContainsKey(head2)) {
                            temp.Add(head1, GetGroupCountInSamples(head1));
                        }
                    }
                }
            }
            return temp;
        }

        public int GetGroupCountInSamples(string[] head) {
            int temp = 0;
            for (int i = 0; i < this.m_set.Samples.Count; i++) {
                if(head.Except(this.m_set.Samples[i].Products).Count() == 0) {
                    temp++;
                }
            }
            return temp;
        }

        //support(A -> B) = count{X, Y, Z} / N
        public double GetGroupSupportThreshold(string[] A, string[] B) {
            double temp = 0.0d;

            return temp;
        }

        public void PrintCounts(Dictionary<string, int> productCounts, string title) {
            Console.WriteLine(title);
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("Products\t\tCount");
            Console.WriteLine("--------------------");
            foreach (KeyValuePair<string, int> product in productCounts) {
                Console.WriteLine($"count({product.Key})\t{product.Value}");
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }

        public void PrintGroups(Dictionary<string[], int> productCounts, string title) {
            Console.WriteLine(title);
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("Product\t\tCount");
            Console.WriteLine("--------------------");
            foreach (KeyValuePair<string[], int> product in productCounts) {
                Console.WriteLine($"{string.Join(",", product.Key)}\t{product.Value}");
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }

        public Dictionary<string, int> RemoveThreshold(Dictionary<string, int> productCounts) {
            return productCounts.Where(pair => pair.Value >= 3).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public Dictionary<string[], int> RemoveThreshold(Dictionary<string[], int> productCounts) {
            return productCounts.Where(pair => pair.Value >= 3).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public Dictionary<string, int> CalculateProductCounts() {
            Dictionary<string, int> temp = new Dictionary<string, int>();
            for (int i = 0; i < this.m_set.Samples.Count; i++) {
                for (int j = 0; j < m_set.Samples[i].Products.Count; j++) {
                    string product = m_set.Samples[i].Products[j];
                    if (temp.ContainsKey(product)) {
                        temp[product]++;
                    } else {
                        temp.Add(product, 1);
                    }
                }
            }
            return temp;
        }
    }
}
