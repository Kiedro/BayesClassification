using BayesClassification.Models;
using BayesClassification.Stat;
using Xunit;

namespace UnitTests
{
    public class ContinousFeatureProbabilityTests
    {
        [Theory]
        [InlineData(0, 0.1, 0.05)]
        [InlineData(0.9, 1, 1)]
        [InlineData(0.5, 0.7, 0.6)]
        [InlineData(0, 0.1, 0)]
        public void IsInRange_ValueInRange_True(double min, double max, double value)
        {
            ContinousFeaturesRanges.AddIfMinMax(new Feature { Id = 1, Value = 0 });
            var continousFeatureStat = new ContinousFeatureProbability(1, min, max);

            bool isInRange = continousFeatureStat.IsInRange(value);

            Assert.True(isInRange);
        }

        [Theory]
        [InlineData(0.1, 0.2, 0.1)]
        [InlineData(0.9, 1, 0.8)]
        [InlineData(0.5, 0.7, 0.71)]
        public void IsInRange_ValueOutOfRange_False(double min, double max, double value)
        {
            ContinousFeaturesRanges.AddIfMinMax(new Feature {Id = 1, Value = 0});
            var continousFeatureStat = new ContinousFeatureProbability(1, min, max);

            bool isInRange = continousFeatureStat.IsInRange(value);

            Assert.False(isInRange);
        }
    }
}
