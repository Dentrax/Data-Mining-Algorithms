#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;

namespace NearestNeighbor {
    class Program {
        static void Main(string[] args) {
            TrainingSet set = new TrainingSet("OBSERVATION", "X1", "X2");
            set.AddSample(new TrainingSample(1, 4, 2));
            set.AddSample(new TrainingSample(2, 6, 4));
            set.AddSample(new TrainingSample(3, 5, 1));
            set.AddSample(new TrainingSample(4, 10, 6));
            set.AddSample(new TrainingSample(5, 11, 8));
            set.Lock();

            Trainer trainer = new Trainer(set);
            trainer.Train();

            Console.ReadKey();
        }
    }
}
