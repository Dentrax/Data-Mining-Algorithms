#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ID3 {
    public sealed class TrainingSet {
        public string OutputName { get; private set; }

        public TrainingEntry[] Entries { get; private set; }

        public List<TrainingSample> Samples { get; private set; }

        private bool m_canAddSample = true;

        public TrainingSet(TrainingSet other) {
            this.OutputName = other.OutputName;
            this.Entries = other.Entries;
            this.Samples = other.Samples;
        }

        public TrainingSet(TrainingSet other, List<TrainingSample> samples) {
            this.OutputName = other.OutputName;
            this.Entries = other.Entries;
            this.Samples = samples;
        }

        public TrainingSet (string outputName, params TrainingEntry[] entries) {
            this.Samples = new List<TrainingSample>();
            this.OutputName = outputName;
            this.Entries = entries;
        }

        public int GetDataCount() {
            return this.Samples.Count;
        }

        public int GetC1() {
            return (from n in this.Samples where n.Output == true select n).Count();
        }

        public int GetC2() {
            return (from n in this.Samples where n.Output == false select n).Count();
        }

        public double GetP1() {
            return (double)this.GetC1() / (double)this.GetDataCount();
        }

        public double GetP2() {
            return (double)this.GetC2() / (double)this.GetDataCount();
        }

        public string GetSamplesAsString() {

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < this.Samples.Count; i++) {

                TrainingSample current = this.Samples[i];

                sb.AppendLine(string.Join(",", current.Samples));

            }

            return sb.ToString();
        }

        public void AddSample(TrainingSample sample) {
            if (this.m_canAddSample) {
                if (!this.Samples.Contains(sample)) {
                    this.Samples.Add(sample);
                } else {
                    throw new Exception("Already contains value");
                }
            } else {
                throw new Exception("Training set was locked");
            }
        }

        public void Lock() {
            this.m_canAddSample = false;
        }

    }
}
