#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;

namespace ID3 {
    class Program {
        static void Main(string[] args) {
            TrainingEntry entry1 = new TrainingEntry("AIR",      new string[] { "SUNNY", "RAINY", "CLOUDY" });
            TrainingEntry entry2 = new TrainingEntry("HEAT",     new string[] { "HOT",   "WARM",  "COLD"   });
            TrainingEntry entry3 = new TrainingEntry("HUMIDITY", new string[] { "HIGH",  "NORMAL"          });
            TrainingEntry entry4 = new TrainingEntry("WIND",     new string[] { "LIGHT", "STRONG"          });

            TrainingSet set = new TrainingSet("GAME", entry1, entry2, entry3, entry4);
            set.AddSample(new TrainingSample(false, "SUNNY", "HOT",  "HIGH",  "LIGHT"));
            set.AddSample(new TrainingSample(false, "SUNNY", "HOT",  "HIGH",  "STRONG"));
            set.AddSample(new TrainingSample(true,  "CLOUDY","HOT",  "HIGH",  "LIGHT"));
            set.AddSample(new TrainingSample(true,  "RAINY", "WARM", "HIGH",  "LIGHT"));
            set.AddSample(new TrainingSample(true,  "RAINY", "COLD", "NORMAL","LIGHT"));
            set.AddSample(new TrainingSample(false, "RAINY", "COLD", "NORMAL","STRONG"));
            set.AddSample(new TrainingSample(true,  "CLOUDY","COLD", "NORMAL","STRONG"));
            set.AddSample(new TrainingSample(false, "SUNNY", "WARM", "HIGH",  "LIGHT"));
            set.AddSample(new TrainingSample(true,  "SUNNY", "COLD", "NORMAL","LIGHT"));
            set.AddSample(new TrainingSample(true,  "RAINY", "WARM", "NORMAL","LIGHT"));
            set.AddSample(new TrainingSample(true,  "SUNNY", "WARM", "NORMAL","STRONG"));
            set.AddSample(new TrainingSample(true,  "CLOUDY","WARM", "HIGH",  "STRONG"));
            set.AddSample(new TrainingSample(true,  "CLOUDY","HOT",  "NORMAL","LIGHT"));
            set.AddSample(new TrainingSample(false, "RAINY", "WARM", "HIGH",  "STRONG"));
            set.Lock();

            Trainer trainer = new Trainer(set);
            trainer.TrainID3(set);

            Console.ReadKey();
        }
    }
}
