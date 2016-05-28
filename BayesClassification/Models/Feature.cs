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
            Feature feature = new Feature();
            feature.Id = featureId;
            feature.Value = double.Parse(value, CultureInfo.InvariantCulture);

            if (featureId == 1 || featureId >= 17)
            {
                feature.Type = FeatureType.Continuous;
                ContinousFeaturesRanges.AddIfMinMax(feature);
            }
            else
            {
                feature.Type = FeatureType.Binary;
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