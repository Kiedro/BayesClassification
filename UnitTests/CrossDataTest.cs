using System;
using System.Collections.Generic;
using System.Linq;
using BayesClassification;
using BayesClassification.Models;
using Xunit;

namespace UnitTests
{
    public class CrossDataTest
    {
        [Fact]
        public void PatientsGroupsTest_PatientsList_TwoPatientsList()
        {
            string[][] result = DataReader.LoadCsv("Data/csvResult.dat");
            string[][] features = DataReader.LoadCsv("Data/csvFeatures.dat");

            IList<Patient> patients = PatientCreator.Create(result, features);
            
            var patientsGroups = new PatientsGroups(patients);

            Assert.Equal(patients.Count() / 2, patientsGroups.GroupA.Count());
            Assert.Equal(patients.Count() / 2, patientsGroups.GroupB.Count()); 
        }
    }
}
