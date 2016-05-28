using System.Globalization;
using BayesClassification.Stat;

namespace BayesClassification.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public FeatureType Type { get; set; }

        public static Feature Create(int featureId, string value)
        {
            Feature feature;
            if (featureId == 1 || featureId >= 17)
            {
                feature = new ContinuousFeature();
                feature.Type = FeatureType.Continuous;
            }
            else
            {
                feature = new BinaryFeature();
                feature.Type = FeatureType.Binary;
            }
            feature.Id = featureId;
            feature.Value = double.Parse(value, CultureInfo.InvariantCulture);
            if (feature.Type == FeatureType.Continuous)
            {
                ContinousFeaturesRanges.AddIfMinMax(feature);
            }
            return feature;
        }
    }

    public enum FeatureType
    {
        Binary = 0,
        Continuous     
    }
}