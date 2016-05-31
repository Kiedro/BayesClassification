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

        public static void ShowConfusionMatrix(ConfusionMatrixData confusionMatrixData)
        {
            int sum = 0;
            Console.WriteLine("----------------");
            Console.WriteLine();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sum += confusionMatrixData.ConfusionMatrix[i, j];
                    if (i == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write("{0:D5}   ", confusionMatrixData.ConfusionMatrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Suma pacjentów: {0}", sum);
            Console.WriteLine();
        }

    }


}
