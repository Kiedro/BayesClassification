using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            };
            features2 = new List<Feature>
            {
                new Feature {Id = 2, Type = FeatureType.Binary, Value = 0},
                new Feature {Id = 3, Type = FeatureType.Binary, Value = 1},
                new Feature {Id = 4, Type = FeatureType.Binary, Value = 0},
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
        public void CreateClassStatisticks_PacientCollectionWithBinaryFeatures_FeaturesStas()
        {
            var classStat = new ClassStatisticks(patients, Classification.Normal);

            Assert.Equal(0.667, classStat.FeaturesStatisticks[2], 3);
        }
    }
}
