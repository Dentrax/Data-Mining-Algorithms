#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace C45 {
    public sealed class EntryResult {
        public string SampleName { get; set; }

        public double EarningScale { get; set; }

        public EntryResult(string sampleName) {
            this.SampleName = sampleName;
        }
    }
}
