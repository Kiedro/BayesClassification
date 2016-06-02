using System.Collections.Generic;
using BayesClassification;
using BayesClassification.Models;
using Xunit;

namespace UnitTests
{
    public class CalcBayesTests
    {
        [Fact]
        public void LijBayesClassifier_BayesValues_NormalClass()
        {
            Dictionary<Classification, double> bayesValues = new Dictionary<Classification, double>
            {
                {Classification.Normal, 0.2},
                {Classification.Hyperfunction, 0.5},
                {Classification.Subnormal, 0.9},
            };
            CalcBayes bayes = new CalcBayes(null);

            var result = bayes.LijBayesClassifier(bayesValues);

            Assert.Equal(Classification.Subnormal, result);

        }
    }
}
