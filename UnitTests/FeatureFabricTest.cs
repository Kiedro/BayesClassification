﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    class FeatureFabricTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(17)]
        [InlineData(18)]
        [InlineData(19)]
        [InlineData(20)]
        [InlineData(21)]
        public void Feature_BinaryInput_BinaryType(int id)
        {
            string binaryData = "1";

            Feature binaryFeature = new Feature(id, binaryData);

            Assert.Equal(FeatureType.Binary, binaryFeature.Type);
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        public void Feature_BinaryInput_BoolValue(string inputData, bool expected)
        {
            int id = 17;
            Feature binaryFeature = new Feature(id, inputData);

            Assert.Equal(expected, binaryFeature.Value);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(16)]
        public void Feature_Continuous_ContinuousType(int id)
        {
            string continuousData = "0.5";

            Feature continuousFeature = new Feature(id, continuousData);

            Assert.Equal(FeatureType.Continuous, continuousFeature.Type);
        }

        [Theory]
        [InlineData("0.5", 0.5)]
        [InlineData("0.2", 0.2)]
        public void Feature_ContinuousInput_DoubleValue(string inputData, bool expected)
        {
            int id = 17;
            Feature continuousData = new Feature(id, inputData);

            Assert.Equal(expected, continuousData.Value);
        }
    }
}
