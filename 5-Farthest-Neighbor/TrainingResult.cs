#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace FarthestNeighbor {
    public sealed  class TrainingResult {
        public int X { get; private set; }

        public int Y { get; private set; }

        public double Distance { get; private set; }

        public TrainingResult(int x, int y, double distance) {
            this.X = x;
            this.Y = y;
            this.Distance = distance;
        }
    }
}
