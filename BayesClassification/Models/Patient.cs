using System.Collections.Generic;
using BayesClassification.Models;

namespace BayesClassification
{
    public class Patient
    {
        public Patient(Classification patientClass, IList<Feature> features)
        {
            RealClassification = patientClass;
            Features = features;
        }

        public IList<Feature> Features { get; set; }
        public Classification RealClassification { get; set; }
        public Classification BayesClassification { get; set; }
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