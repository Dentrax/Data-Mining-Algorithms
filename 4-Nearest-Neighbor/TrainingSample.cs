#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================

#endregion
namespace NearestNeighbor {
    public struct TrainingSample {
        public int Observation { get; private set; }

        public int X1 { get; private set; }

        public int X2 { get; private set; }

        public TrainingSample(int observation, int x1, int x2) {
            this.Observation = observation;
            this.X1 = x1;
            this.X2 = x2;
        }
    }
}
