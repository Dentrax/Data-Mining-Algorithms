#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;

namespace KNearestNeighborhood {
    class Program {
        static void Main(string[] args) {
            //GOOD -> true, BAD = false
            TrainingSet set = new TrainingSet("X1", "X2", "Y");
            set.AddSample(new TrainingSample(2, 4, false));
            set.AddSample(new TrainingSample(3, 6, true));
            set.AddSample(new TrainingSample(3, 4, true));
            set.AddSample(new TrainingSample(4, 10, false));
            set.AddSample(new TrainingSample(5, 8, false));
            set.AddSample(new TrainingSample(6, 3, true));
            set.AddSample(new TrainingSample(7, 9, true));
            set.AddSample(new TrainingSample(9, 7, false));
            set.AddSample(new TrainingSample(11, 7, false));
            set.AddSample(new TrainingSample(10, 2, false));
            set.Lock();

            Trainer trainer = new Trainer(set);
            trainer.Train(8, 4, 4);
            trainer.PrintResult();

            Console.ReadKey();
        }
    }
}
