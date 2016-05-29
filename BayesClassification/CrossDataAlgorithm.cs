using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BayesClassification.Models;
using BayesClassification.Stat;

namespace BayesClassification
{
    public class CrossDataAlgorithm
    {
        private IList<int> _featureIds;
        private readonly int _actualRandomNmb;

        public ConfusionMatrixCreator ConfusionMatrixCreator;
        public IList<ConfusionMatrixData> ConfusionMatrixDatas;
        public IList<PatientsGroups> PatientsGroupsList;
        

        public CrossDataAlgorithm(IList<Patient> patients, int repetitions, IList<int> featureIds)
        {
            _featureIds = featureIds;
            ConfusionMatrixDatas = new List<ConfusionMatrixData>();
            ConfusionMatrixCreator = new ConfusionMatrixCreator();

            PatientsGroupsList = new List<PatientsGroups>();
            for (int i = 0; i < repetitions; i++)
            {
                PatientsGroupsList.Add(new PatientsGroups(patients));
            }

            _actualRandomNmb = 0;
            foreach (var patientsGroups in PatientsGroupsList)
            {
                _actualRandomNmb++;
                DoubleCrossValidation(patientsGroups);
            }
        }

        private void DoubleCrossValidation(PatientsGroups groups)
        {
            Classificate(groups.GroupA, groups.GroupB, Group.B);
            Classificate(groups.GroupB, groups.GroupA, Group.A);
        }

        public void Classificate(IList<Patient> learningGroup, IList<Patient> testGroup, Group testGroupLetter)
        {
            var allStatistics = new List<ClassStatistics>
            {
                new ClassStatistics(learningGroup, Classification.Normal),
                new ClassStatistics(learningGroup, Classification.Hyperfunction),
                new ClassStatistics(learningGroup, Classification.Subnormal)
            };


            //obliczenia bayesa porownanie statystyk dla grupy testowej
            var calcBayes = new CalcBayes(allStatistics);
            calcBayes.ClassificatePatients(testGroup);

            //porownanie statystyk dla grupy testowej
            ConfusionMatrixDatas.Add(
                ConfusionMatrixCreator.CreateConfusionMatrix(
                    testGroup, _actualRandomNmb, testGroupLetter));
        }
    }


    public enum Group
    {
        A,
        B
    }
}
