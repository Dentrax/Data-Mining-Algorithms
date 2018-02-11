#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;

namespace FarthestNeighbor {
    class Program {
        static void Main(string[] args) {
            TrainingSet set = new TrainingSet("OBSERVATIONS", "X1", "X2");
            set.AddSample(new TrainingSample(1, 7, 8));
            set.AddSample(new TrainingSample(2, 4, 2));
            set.AddSample(new TrainingSample(3, 5, 3));
            set.AddSample(new TrainingSample(4, 8, 7));
            set.AddSample(new TrainingSample(5, 9, 9));
            set.Lock();

            Trainer trainer = new Trainer(set);
            trainer.Train();

            Console.ReadKey();
        }
    }
}
