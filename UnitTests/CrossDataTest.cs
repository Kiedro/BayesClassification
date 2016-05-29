using System;
using System.Collections.Generic;
using System.Linq;
using BayesClassification;
using BayesClassification.Models;
using BayesClassification.Stat;
using Xunit;

namespace UnitTests
{
    public class CrossDataTest
    {
        public string[][] Result;
        public string[][] Features;
        public IList<int> FeatureIds;

        public CrossDataTest()
        {
            Result = DataReader.LoadCsv("Data/csvResult.dat");
            Features = DataReader.LoadCsv("Data/csvFeatures.dat");
            ContinousFeaturesRanges.Buckets = 10;

            FeatureIds = new List<int>();
            for (int i = 1; i <= 21; i++)
            {
                FeatureIds.Add(i);
            }
        }

        [Fact]
        public void PatientsGroupsTest_PatientsList_TwoPatientsList()
        {
            IList<Patient> patients = PatientCreator.Create(Result, Features);
            
            var patientsGroups = new PatientsGroups(patients);

            Assert.Equal(patients.Count() / 2, patientsGroups.GroupA.Count());
            Assert.Equal(patients.Count() / 2, patientsGroups.GroupB.Count()); 
        }

        [Fact]
        public void ConfusionMatrixCreator_TrueClassPatient_ConfusionMatrix()
        {
            IList<Patient> patients = new List<Patient>
            {
                new Patient(Classification.Normal, null)
                {
                    BayesClassification = Classification.Normal,
                }
            };
            

            var cmc = new ConfusionMatrixCreator();
            var confM = cmc.CreateConfusionMatrix(patients, 1, Group.A);

            Assert.Equal(1, confM.ConfusionMatrix[0,0]);           
        }

        [Fact]
        public void ConfusionMatrixCreator_MismatchClassPatient_ConfusionMatrix()
        {
            IList<Patient> patients = new List<Patient>
            {
                new Patient(Classification.Normal, null)
                {
                    BayesClassification = Classification.Hyperfunction,
                }
            };


            var cmc = new ConfusionMatrixCreator();
            var confM = cmc.CreateConfusionMatrix(patients, 1, Group.A);

            Assert.Equal(0, confM.ConfusionMatrix[0, 0]);
            Assert.Equal(1, confM.ConfusionMatrix[0, 1]);
        }

    }
}
