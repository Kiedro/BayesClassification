using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayesClassification
{
    // Load a CSV file into an array of rows and columns.
    // Assume there may be blank lines but every line has
    // the same number of fields.
    public class DataReader
    {
        public static string[][] LoadCsv(string filename)
        {
            // Get the file's text.
            string whole_file = System.IO.File.ReadAllText(filename);

            // Split into lines.
            whole_file = whole_file.Replace('\n', '\r');
            string[] lines = whole_file.Split(new char[] { '\r' },
                StringSplitOptions.RemoveEmptyEntries);

            // See how many rows and columns there are.
            int num_rows = lines.Length;
            int num_cols = lines[0].Split(',').Length;

            // Allocate the data array.
            string[][] values = new string[num_cols][];
            for (int i = 0; i < num_cols; i++)
            {
                values[i] = new string[num_rows];
            }
            // Load the array.
            for (int r = 0; r < num_rows; r++)
            {
                string[] line_r = lines[r].Split(',');
                for (int c = 0; c < num_cols; c++)
                {
                    values[c][r] = line_r[c];
                }
            }

            // Return the values.
            return values;
        }
    }
}
