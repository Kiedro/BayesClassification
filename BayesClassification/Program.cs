using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayesClassification
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = DataReader.LoadCsv("Data/csvResult.dat");
            var features = DataReader.LoadCsv("Data/csvFeatures.dat");
        }

    
    }
}
