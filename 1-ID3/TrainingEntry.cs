#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace ID3 {
    public sealed class TrainingEntry {
        public string Name { get; set; }

        public string[] Values { get; set; }

        public TrainingEntry(string name, string[] values) {
            this.Name = name;
            this.Values = values;
        }
    }
}
