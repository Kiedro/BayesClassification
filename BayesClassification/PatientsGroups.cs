using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayesClassification.Models
{
    public class PatientsGroups
    {
        public PatientsGroups(IList<Patient> patients)
        {
            int patients_count = patients.Count;
            Patient[] randPatients = new Patient[patients_count];
            patients.CopyTo(randPatients, 0);

            Randomizer.Randomize(randPatients);

            GroupA = new List<Patient>();
            for (int i = 0; i < patients_count/2; i++)
            {
                GroupA.Add(randPatients[i]);
            }

            GroupB = new List<Patient>();
            for (int i = patients_count / 2; i < patients_count; i++)
            {
                GroupB.Add(randPatients[i]);
            }
        }

        public IList<Patient> GroupA { get; set; }

        public IList<Patient> GroupB { get; set; }
    }

    public class Randomizer
    {
        public static void Randomize<T>(T[] items)
        {
            Random rand = new Random();

            for (int i = 0; i < items.Length - 1; i++)
            {
                int j = rand.Next(i, items.Length);
                T temp = items[i];
                items[i] = items[j];
                items[j] = temp;
            }
        }
    }
}
