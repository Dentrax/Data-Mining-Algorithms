#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================

#endregion

namespace KMeans {
    public struct TrainingSample {
        public string Observation { get; private set; }
        public int Variable1 { get; private set; }
        public int Variable2 { get; private set; }
        public ClusterType ClusterType { get; private set; }

        public TrainingSample(string observation, int variable1, int variable2, ClusterType clusterType) {
            this.Observation = observation;
            this.Variable1 = variable1;
            this.Variable2 = variable2;
            this.ClusterType = clusterType;
        }

        public void SetClusterType(ClusterType clusterType) {
            this.ClusterType = clusterType;
        }
    }
}
