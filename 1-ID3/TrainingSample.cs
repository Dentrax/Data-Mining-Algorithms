#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace ID3 {
    public sealed class TrainingSample {
        public bool Output { get; private set; }

        public string[] Samples { get; private set; }

        public TrainingSample(bool output, params string[] samples) {
            this.Output = output;
            this.Samples = samples;
        }
    }
}
