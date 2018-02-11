#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System.Collections.Generic;

namespace NearestNeighbor {
    public struct ObservationData {

        public List<int>[] X;

        public List<int>[] Y;

        public ObservationData(List<int>[] x, List<int>[] y) {
            this.X = x;
            this.Y = y;
        }

        public void AddX(int x_index, int data) {
            this.X[x_index].Add(data);
            this.X[x_index].Sort();
        }

        public void RemoveX(int x_index, int data) {
            this.X[x_index].Remove(data);
        }

        public void Move(int index, int to_index) {
            this.AddX(to_index - 1, index);
            this.AddY(to_index - 1, index);

            this.X[index - 1].Clear();
            this.Y[index - 1].Clear();
        }

        public void AddY(int y_index, int data) {
            this.Y[y_index].Add(data);
            this.Y[y_index].Sort();
        }

        public void RemoveY(int y_index, int data) {
            this.X[y_index].Remove(data);
        }

    }
}
