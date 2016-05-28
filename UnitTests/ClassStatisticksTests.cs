using System;
using System.Collections.Generic;
using BayesClassification;
using BayesClassification.Models;
using BayesClassification.Stat;
using Xunit;

namespace UnitTests
{
    public class ClassStatisticksTests
    {
        private IList<Patient> patients;
        private IList<Feature> features;
        List<Feature> features2;
        public ClassStatisticksTests()
        {
            features = new List<Feature>
            {
                new Feature {Id = 2, Type  = FeatureType.Binary, Value = 1},
                new Feature {Id = 3, Type  = FeatureType.Binary, Value = 0},
                new Feature {Id = 4, Type  = FeatureType.Binary, Value = 1},
                Feature.Create(17, "0.25")
            };
            features2 = new List<Feature>
            {
                new Feature {Id = 2, Type = FeatureType.Binary, Value = 0},
                new Feature {Id = 3, Type = FeatureType.Binary, Value = 1},
                new Feature {Id = 4, Type = FeatureType.Binary, Value = 0},
                Feature.Create(17, "1")

            };

            patients = new List<Patient>
            {
                new Patient(Classification.Normal, features),
                new Patient(Classification.Hyperfunction, features),
                new Patient(Classification.Subnormal, features),
                new Patient(Classification.Normal, features),
                new Patient(Classification.Hyperfunction, features),
                new Patient(Classification.Subnormal, features),
                new Patient(Classification.Normal, features2),
                new Patient(Classification.Hyperfunction, features2),
                new Patient(Classification.Subnormal, features2),

            };
        }

        [Fact]
        public void CreateClassStatisticks_PacientCollections_APrioriStat()
        {
            var classStat = new ClassStatisticks(patients, Classification.Normal);

            Assert.Equal(0.333, classStat.ClassProbability, 3);
        }

        [Fact]
        public void CreateClassStatisticks_PacientCollectionWithFeatures_BinaryFeaturesStat()
        {
            var classStat = new ClassStatisticks(patients, Classification.Normal);

            Assert.Equal(0.667, classStat.FeaturesStatisticks(2), 3);
        }

        [Fact]
        public void CreateClassStatisticks_PacientCollectionWithFeatures_ContinuesFeaturesStat()
        {
            ContinousFeaturesRanges.Buckets = 10;
            var classStat = new ClassStatisticks(patients, Classification.Normal);

            Assert.Equal(0.667, classStat.FeaturesStatisticks(17, 0.25), 3);
        }

        [Fact]
        public void CreateClassStatisticks_PacientCollectionWithFeatures_Probability0()
        {
            ContinousFeaturesRanges.Buckets = 10;
            var classStat = new ClassStatisticks(patients, Classification.Normal);

            Assert.Equal(0, classStat.FeaturesStatisticks(17, 0.6), 3);
        }

    }
}
