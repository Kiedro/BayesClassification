using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BayesClassification.Models;

namespace BayesClassification.Stat
{
    public class ConfusionMatrixCreator
    {
        public ConfusionMatrixData CreateConfusionMatrix(IList<Patient> patients, int actualRandomNmb, Group groupLetter)
        {
            int size = 3;
            var confusionMatrix = new int[size, size];

            var classificationTypes = new List<Classification>
            {
                Classification.Normal,
                Classification.Hyperfunction,
                Classification.Subnormal
            };

            for (int i = 0; i < size; i++)
            {
                var actual = classificationTypes[i];
                for (int j = 0; j < size; j++)
                {
                    var predicted = classificationTypes[j];

                    confusionMatrix[i, j] = patients
                        .Count(p => p.RealClassification == actual
                                 && p.BayesClassification == predicted);
                }
            }


            return new ConfusionMatrixData(
                confusionMatrix, actualRandomNmb, groupLetter);
        }

        public void ShowConfusionMatrix(ConfusionMatrixData confusionMatrixData)
        {
            
        }
    }

    
}
