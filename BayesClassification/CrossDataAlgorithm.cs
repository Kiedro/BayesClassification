using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BayesClassification.Models;
using BayesClassification.Stat;

namespace BayesClassification
{
    public class CrossDataAlgorithm
    {
        private IList<int> _featureIds;

        public CrossDataAlgorithm(IList<Patient> patients, int repetitions, IList<int> featureIds)
        {
            _featureIds = featureIds;

            var patientsGroupsList = new List<PatientsGroups>();
            for (int i = 0; i < repetitions; i++)
            {
                patientsGroupsList.Add(new PatientsGroups(patients));
            }

            foreach (var patientsGroups in patientsGroupsList)
            {
                DoubleCrossValidation(patientsGroups);
            }
        }

        private void DoubleCrossValidation(PatientsGroups groups)
        {
            Classificate(groups.GroupA, groups.GroupB);
            Classificate(groups.GroupB, groups.GroupA);
        }

        public void Classificate(IList<Patient> learningGroup, IList<Patient> testGroup)
        {
            var statNormal = new ClassStatistics(learningGroup, Classification.Normal);
            var statHyper = new ClassStatistics(learningGroup, Classification.Hyperfunction);
            var statSubnormal = new ClassStatistics(learningGroup, Classification.Subnormal);


            //teraz obliczenia bayesa i porownanie statystyk dla grupy testowej
            var statNormalValues = new List<double>();
            for (int i=1; i<=21; i++)
            {
                statNormalValues.Add(statNormal.FeaturesStatisticks(i));
            } 
            
            //FeaturesStatisticks
        }

        public IList<double> GetStatisticValues(ClassStatistics statistics)
        {
            var result = new List<double>();

            foreach (var id in _featureIds)
            {
                if (id > 1 && id < 17)
                {
                    //result.Add(statistics.FeaturesStatisticks());
                }
                else
                {
                    
                }
            }

            return result;
        }

        

    }
}
