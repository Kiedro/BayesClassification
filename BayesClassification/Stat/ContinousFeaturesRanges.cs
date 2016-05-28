using System.Collections.Generic;
using BayesClassification.Models;

namespace BayesClassification.Stat
{
    public static class ContinousFeaturesRanges
    {
        public static Dictionary<int, Range> Ranges = new Dictionary<int, Range>();
        public static int Buckets { get; set; }

        public static double FeatureRange(int id)
        {
            return Ranges[id].Max - Ranges[id].Min;
        }

        public static void AddIfMinMax(Feature feature)
        {
            var range = Ranges.ContainsKey(feature.Id) ? Ranges[feature.Id] : new Range();

            if (range.Min > feature.Value)
            {
                range.Min = feature.Value;
            }
            if (range.Max < feature.Value)
            {
                range.Max = feature.Value;
            }

            Ranges[feature.Id] = range;
        }
    }

    public class Range
    {
        public double Min { get; set; }
        public double Max { get; set; }

        public Range(double min, double max)
        {
            Min = min;
            Max = max;
        }

        public Range()
        {      
            Min = double.MaxValue;
            Max = double.MinValue;
        }
    }
}