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

        public void ClassificatePatients(IList<Patient> patients)
        {
            foreach (var patient in patients)
            {
                var normalProb = GetBayesClassification(patient, Classification.Normal);
                var hyperProb = GetBayesClassification(patient, Classification.Hyperfunction);
                var subnormalProb = GetBayesClassification(patient, Classification.Subnormal);

                if (normalProb >= hyperProb && normalProb >= subnormalProb)
                {
                    patient.BayesClassification = Classification.Normal;
                }
                else if (hyperProb >= normalProb && hyperProb >= subnormalProb)
                {
                    patient.BayesClassification = Classification.Hyperfunction;
                }
                else if (subnormalProb >= normalProb && subnormalProb >= hyperProb)
                {
                    patient.BayesClassification = Classification.Subnormal;
                }
            }
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
    }
}
