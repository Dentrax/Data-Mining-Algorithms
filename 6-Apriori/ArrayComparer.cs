#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System.Collections.Generic;

namespace Apriori {
    public sealed class ArrayComparer : IEqualityComparer<string[]> {
        public bool Equals(string[] x, string[] y) {
            if(x[0] == y[0]) {
                return true;
            } else {
                return false;
            }
        }

        public int GetHashCode(string[] obj) {
            return obj[0].GetHashCode() + obj[1].GetHashCode();
        }
    }
}
