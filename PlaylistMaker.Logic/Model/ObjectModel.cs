using System.Linq;

namespace PlaylistMaker.Logic.Model
{
    public struct ObjectModel
    {
        public bool IsNull;

        public string Result;

        public string[] Results;

        public Composition[] Compositions;

        public override string ToString()
        {
            var result = "";
            if (Results != null && Results.Length != 0) result = Results.Aggregate(result, (current, val) => current + val + "\n");
            if (Compositions != null && Compositions.Length != 0) result = Compositions.Aggregate(result, (current, val) => current + val.ToString() + "\n");
            return result;
        }
    }
}
