using System;
using System.Collections;
using System.Collections.Generic;
using BayesClassification.Models;

namespace BayesClassification
{
    public class PatientCreator
    {
        public static IList<Patient> Create(string[][] result, string[][] features)
        {
            List<Patient> patients = new List<Patient>();

            for (int i = 0; i < result.Length; i++)
            {
                Classification patientClass = GetClassFromArray(result[i]);
                IList<Feature> patientFeatures = GetFeaturesForPatient(features[i]);
                patients.Add(new Patient(patientClass, patientFeatures));
            }
            return patients;
        }

        private static IList<Feature> GetFeaturesForPatient(string[] featuresStrings)
        {
            IList<Feature> features = new List<Feature>();
            for (int i = 1; i <= featuresStrings.Length; i++)
            {
                features.Add(Feature.Create(i, featuresStrings[i-1]));
            }
            return features;
        }

        public static Classification GetClassFromArray(string[] inputData)
        {
            int classification = Array.IndexOf(inputData, "1");
            return (Classification) classification;
        }
    }
}