using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BayesClassification.Models;
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
            IList<Patient> patients = PatientCreator.Create(patientClasses, patientFeatures);
            var classStat = new ClassStatisticks(patients, Classification.Normal);
        }

    }
}
