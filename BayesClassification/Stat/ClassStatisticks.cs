using System.Collections.Generic;
using System.Linq;
using BayesClassification.Models;

namespace BayesClassification.Stat
{
    public class ClassStatisticks
    {
        public Classification Class { get; set; }
        public IList<Patient> Patients { get; set; }
        public double ClassProbability { get; set; }
        public Dictionary<int, double> FeaturesStatisticks = new Dictionary<int, double>();

        public ClassStatisticks(IList<Patient> patients, Classification @class)
        {
            this.Patients = patients.Where(x => x.Classification == @class).ToList();
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
                    FeaturesStatisticks.Add(i, featureProbability);
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
    }
}