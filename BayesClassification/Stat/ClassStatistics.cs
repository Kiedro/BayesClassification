using System;
using System.Collections.Generic;
using System.Linq;
using BayesClassification.Models;

namespace BayesClassification.Stat
{
    public class ClassStatistics
    {
        public Classification Class { get; set; }
        public IList<Patient> Patients { get; set; }
        public double ClassProbability { get; set; }
        private Dictionary<int, double> BinaryFeaturesStatisticks = new Dictionary<int, double>();
        private IList<ContinousFeatureProbability> ContinousFeatureProbabilities = new List<ContinousFeatureProbability>();

        public ClassStatistics(IList<Patient> patients, Classification @class)
        {
            this.Patients = patients.Where(x => x.RealClassification == @class).ToList();
            ClassProbability = (double)Patients.Count / patients.Count;
            this.Class = @class;

            CalculateFeaturesStatisticks();
        }

        private void CalculateFeaturesStatisticks()
        {
            int featuresCount = Patients.First().Features.Count;
            IList<Feature> features = GetAllFeatures();

            foreach (Feature feature in Patients.First().Features)
            {
                int i = feature.Id;
                if (Patients.First().Features.First(x => x.Id == i).Type == FeatureType.Binary)
                {
                    double featureProbability = features.Where(x => x.Id == i).Sum(x => x.Value) / Patients.Count;
                    BinaryFeaturesStatisticks.Add(i, featureProbability);
                }

                else
                {
                    for (int j = 0; j < ContinousFeaturesRanges.Buckets; j++)
                    {
                        Range range = ContinousFeaturesRanges.Ranges[i];
                        double featureRange = ContinousFeaturesRanges.FeatureRange(i);
                        double offsetMin = featureRange / ContinousFeaturesRanges.Buckets * j;
                        double offsetMax = featureRange / ContinousFeaturesRanges.Buckets * (j + 1);
                        ContinousFeatureProbability bucket = new ContinousFeatureProbability(i, range.Min + offsetMin, range.Min + offsetMax);

                        bucket.Probability = (double)features.Count(x => x.Id == i && bucket.IsInRange(x.Value)) / Patients.Count;

                        ContinousFeatureProbabilities.Add(bucket);
                    }
                }
            }
        }

        private IList<Feature> GetAllFeatures()
        {
            IList<Feature> features = new List<Feature>();
            foreach (Patient patient in Patients)
            {
                foreach (Feature feature in patient.Features)
                {
                    features.Add(feature);
                }
            }

            return features;
        }

        public double FeaturesStatisticks(Feature feature)
        {
            if (feature.Type == FeatureType.Binary)
            {
                if (feature.Value == 1)
                {
                return BinaryFeaturesStatisticks[feature.Id];
                    
                }
                else if(feature.Value == 0)
                {
                    return 1 - BinaryFeaturesStatisticks[feature.Id];
                }
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                if (feature.Value < 0)
                    throw new ArgumentOutOfRangeException();
                return GetContinousProbability(feature.Id, feature.Value);
            }
        }

        private double GetContinousProbability(int id, double value)
        {
            return ContinousFeatureProbabilities.Single(x => x.Id == id && x.IsInRange(value)).Probability;
        }
    }
}