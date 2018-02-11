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

namespace Apriori {
    class Program {
        static void Main(string[] args) {
            TrainingSet set = new TrainingSet("Müşteri", "Aldığı Ürünler");
            set.AddSample(new TrainingSample(1, new List<string>() { "Şeker" , "Çay"    , "Ekmek"                          }));
            set.AddSample(new TrainingSample(2, new List<string>() { "Ekmek" , "Peynir" , "Zeytin"  , "Makarna"            }));
            set.AddSample(new TrainingSample(3, new List<string>() { "Şeker" , "Peynir" , "Deterjan", "Ekmek"  , "Makarna" }));
            set.AddSample(new TrainingSample(4, new List<string>() { "Ekmek" , "Peynir" , "Çay"     , "Makarna"            }));
            set.AddSample(new TrainingSample(5, new List<string>() { "Peynir", "Makarna", "Şeker"   , "Bira"               }));
            set.Lock();

            //Trainin paramaters (set, support(eşik), trust(eşik))
            Trainer trainer = new Trainer(set, 60, 75);
            trainer.Train();

            Console.ReadKey();
        }
    }
}
