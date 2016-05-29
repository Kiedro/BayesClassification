using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayesClassification.Stat
{
    public class ConfusionMatrixData
    {
        public int[,] ConfusionMatrix;
        public int RandomNmb;
        public Group Group;

        public ConfusionMatrixData(int[,] confusionMatrix, int randomNmb, Group group)
        {
            ConfusionMatrix = confusionMatrix;
            RandomNmb = randomNmb;
            Group = group;
        }
    }
}
