using System.Collections.Generic;
using BayesClassification.Stat;

namespace BayesClassification
{
    class Program
    {
        static void Main(string[] args)
        {
            var patientClasses = DataReader.LoadCsv("Data/csvResult.dat");
            var patientFeatures = DataReader.LoadCsv("Data/csvFeatures.dat");
            ContinousFeaturesRanges.Buckets = 10;
            int[] featuresIds = null;// new[] { 4, 5, 6, 20 };
            IList<Patient> patients = PatientCreator.Create(patientClasses, patientFeatures, featuresIds);

            var crossDataAlgorithm = new CrossDataAlgorithm(patients, 5);

            foreach (var confusionMatrixData in crossDataAlgorithm.ConfusionMatrixDatas)
            {
                ConfusionMatrixCreator.ShowConfusionMatrix(confusionMatrixData);
            }
        }

    }
}
