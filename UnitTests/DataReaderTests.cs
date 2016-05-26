using BayesClassification;
using Xunit;

namespace UnitTests
{
    public class DataReaderTests
    {
        [Fact]
        public void ReadPatientClasses()
        {
            var patientClasses = DataReader.LoadCsv("Data/csvResult.dat");

            var classesCount = patientClasses[0].Length;
            var observationsCount = patientClasses.Length;

            Assert.Equal(3, classesCount);
            Assert.Equal(7200, observationsCount);
        }

        [Fact]
        public void ReadPatientFeatures()
        {
            var patientFeatures = DataReader.LoadCsv("Data/csvFeatures.dat");

            var featuresCount = patientFeatures.GetLength(0);
            var observationsCount = patientFeatures.GetLength(1);

            Assert.Equal(21, featuresCount);
            Assert.Equal(7200, observationsCount);
        }
    }
}
