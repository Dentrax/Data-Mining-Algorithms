#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;

namespace C45 {
    class Program {
        static void Main(string[] args) {
            TrainingEntry entry1 = new TrainingEntry("AGE",      null);
            TrainingEntry entry2 = new TrainingEntry("MODEL",    new string[] { "X3",    "X5"      });
            TrainingEntry entry3 = new TrainingEntry("GENDER",   new string[] { "MALE", "FEMALE"   });

            TrainingSet set = new TrainingSet("SATISFIED", entry1, entry2, entry3);
            set.AddSample(new TrainingSample(false, 21, "X5", "MALE"));
            set.AddSample(new TrainingSample(true,  19, "X3", "FEMALE"));
            set.AddSample(new TrainingSample(false, 22, "X5", "MALE"));
            set.AddSample(new TrainingSample(true,  21, "X3", "MALE"));
            set.AddSample(new TrainingSample(true,  30, "X3", "MALE"));
            set.AddSample(new TrainingSample(false, 60, "X3", "FEMALE"));
            set.AddSample(new TrainingSample(false, 45, "X3", "FEMALE"));
            set.AddSample(new TrainingSample(false, 55, "X3", "MALE"));
            set.Lock();
            
            Trainer trainer = new Trainer(set);
            trainer.TrainID3(set);

            Console.ReadKey();
        }
    }
}
