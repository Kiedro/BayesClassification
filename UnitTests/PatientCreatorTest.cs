using System.Collections;
using System.Collections.Generic;
using BayesClassification;
using BayesClassification.Models;
using Xunit;

namespace UnitTests
{
    public class PatientCreatorTest
    {
        [Fact]
        public void CreatePatients_ThyramidDataset_7200Patients()
        {
            string[][] result = DataReader.LoadCsv("Data/csvResult.dat");
            string[][] features = DataReader.LoadCsv("Data/csvFeatures.dat");

            IList<Patient> patients = PatientCreator.Create(result, features);

            Assert.Equal(7200, patients.Count);
        }

        [Fact]
        public void CreatePatients_ThyramidDataset_PatientHas21Features()
        {
            string[][] result = DataReader.LoadCsv("Data/csvResult.dat");
            string[][] features = DataReader.LoadCsv("Data/csvFeatures.dat");

            IList<Patient> patients = PatientCreator.Create(result, features);

            foreach (Patient patient in patients)
            {
                Assert.Equal(21, patient.Features.Count);
            }
        }

        [Theory]
        [MemberData(nameof(GetResult))]
        public void GetClassFromArray_ResultArray_PatienClass(Classification expected, params string[] result)
        {
            var classification = PatientCreator.GetClassFromArray(result);

            Assert.Equal(expected, classification);
        }

        public static IEnumerable<object[]> GetResult
        {
            get
            {
                yield return new object[] { Classification.Normal, new string[] { "1", "0", "0" } };
                yield return new object[] { Classification.Hyperfunction, new string[] { "0", "1", "0" } };
                yield return new object[] { Classification.Subnormal, new string[] { "0", "0", "1" } };
            }
        }
    }
}