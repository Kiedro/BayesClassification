using System;
using System.Collections.Generic;
using System.Linq;
using BayesClassification.Models;
using BayesClassification.Stat;

namespace BayesClassification
{
    public class CalcBayes
    {
        private readonly IList<ClassStatistics> _statistics;

        public CalcBayes(IList<ClassStatistics> statistics)
        {
            _statistics = statistics;
        }

        public void ClassificatePatients(IList<Patient> patients, LossFunctions lossFunction = LossFunctions.ZeroOne)
        {
            foreach (var patient in patients)
            {
                IDictionary<Classification, double> pacientClassifications = new Dictionary<Classification, double>();

                foreach (Classification @class in Enum.GetValues(typeof(Classification)))
                {
                    pacientClassifications.Add(@class, GetBayesClassification(patient, @class));
                }

                if (lossFunction == LossFunctions.ZeroOne)
                {
                    patient.BayesClassification = pacientClassifications.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
                }
                else
                {
                    patient.BayesClassification = LijBayesClassifier(pacientClassifications);
                }
            }
        }

        public Classification LijBayesClassifier(IDictionary<Classification, double> pacientClassifications)
        {
            IDictionary < Classification, double> results = new Dictionary<Classification, double>();
            foreach (Classification @class in Enum.GetValues(typeof(Classification)))
            {
                double lossValue = 0;
                int loss;
                for (int i = 0; i < 3; i++)
                {
                    loss = Math.Abs((int) @class - i);
                    lossValue += loss*pacientClassifications[(Classification) i];
                }
                results[@class] = lossValue;
            }
            return results.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
        }


        public double GetBayesClassification(Patient patient, Classification classification)
        {
            var classStat = _statistics.Single(x => x.Class == classification);
            double classificationResult = classStat.ClassProbability;

            foreach (var feature in patient.Features)
            {
                classificationResult *= classStat.FeaturesProbabilities(feature);
            }

            return classificationResult;
        }

        public enum LossFunctions
        {
            ZeroOne = 0,
            Lij
        }
    }
}
