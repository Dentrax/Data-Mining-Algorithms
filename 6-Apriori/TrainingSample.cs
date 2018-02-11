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
    public struct TrainingSample {
        public int Observation { get; private set; }

        public List<String> Products { get; private set; }

        public TrainingSample(int observation, List<String> products) {
            this.Observation = observation;
            this.Products = products;
        }
    }
}
