using System.Collections.Generic;
using BayesClassification.Models;

namespace BayesClassification
{
    public class Patient
    {
        public Patient(Classification patientClass, IList<Feature> features)
        {
            Classification = patientClass;
            Features = features;
        }

        public IList<Feature> Features { get; set; }
        public Classification Classification { get; set; }
    }
}

namespace BayesClassification.Models
{
    public enum Classification
    {
        Normal = 0,
        Hyperfunction,
        Subnormal
    }
}