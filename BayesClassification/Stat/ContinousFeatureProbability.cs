namespace BayesClassification.Stat
{
    public class ContinousFeatureProbability
    {
        public int Id { get; set; }
        public double RangeMin { get; set; }
        public double RangeMax { get; set; }
        public double Probability { get; set; }

        public bool IsInRange(double value)
        {

            return ((value > RangeMin || RangeMin == ContinousFeaturesRanges.Ranges[Id].Min) && value <= RangeMax);
        }

        public ContinousFeatureProbability(int id, double min, double max)
        {
            Id = id;
            RangeMin = min;
            RangeMax = max;
        }
    }
}