#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;

namespace KMeans {
    class Program {
        static void Main(string[] args) {
            TrainingSet set = new TrainingSet("OBSERVATIONS", "VARIABLE 1", "VARIABLE 2", "CLUSTER");
            set.AddSample(new TrainingSample("X1",            4,           2,              ClusterType.C1));
            set.AddSample(new TrainingSample("X2",            6,           4,              ClusterType.C1));
            set.AddSample(new TrainingSample("X3",            5,           1,              ClusterType.C2));
            set.AddSample(new TrainingSample("X4",            10,          6,              ClusterType.C1));
            set.AddSample(new TrainingSample("X5",            11,          8,              ClusterType.C2));
            set.Lock();

            //Training parameters (set, start k = 2)
            Trainer trainer = new Trainer(set, 2);
            trainer.Train();

            Console.ReadKey();
        }
    }
}
