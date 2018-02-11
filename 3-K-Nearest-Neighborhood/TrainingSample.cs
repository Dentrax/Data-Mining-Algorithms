#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace KNearestNeighborhood {
    public struct TrainingSample {
        public int X1 { get; private set; }

        public int X2 { get; private set; }

        public bool Output { get; private set; }

        public TrainingSample(int x1, int x2, bool output) {
            this.X1 = x1;
            this.X2 = x2;
            this.Output = output;
        }
    }
}
