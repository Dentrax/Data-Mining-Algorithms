#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace KNearestNeighborhood {
    public sealed class TrainingResult {
        public int X1 { get; private set; }

        public int X2 { get; private set; }

        public double Distance { get; private set; }

        public int Order { get; private set; }

        public bool Result { get; private set; }

        public TrainingResult(int x1, int x2, double distance, int order, bool result) {
            this.X1 = x1;
            this.X2 = x2;
            this.Distance = distance;
            this.Order = order;
            this.Result = result;
        }

    }
}
