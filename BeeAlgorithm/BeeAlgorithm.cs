using MathParser;
using org.mariuszgromada.math.mxparser;

namespace BeeAlgorithm
{
    public class BeeAlgorithm
    {
        private Parser _parser;

        public BeeAlgorithm()
        {
            this._parser = new();
        }

        public double[] GetResultArguments(
            string expression,
            int countScoutBees,
            int countEliteBees,
            int countDefaultBees,
            int countLocalSearchPoints,
            int countElitePoint,
            double delta,
            int countIterations,
            double lowerBound,
            double upperBound)
        {

            _parser.ConvertFunc(expression);

            List<double[]> points = new();

            for (int i = 0; i < countScoutBees; i++)
            {
                points.Add(RandomPoint(lowerBound, upperBound, _parser.ArgumentsNumber));
            }

            for (int k = 0; k < countIterations; k++)
            {
                points.OrderBy(point => _parser.Calculate(point));

                LocalSearch(
                    countLocalSearchPoints,
                    countElitePoint,
                    countEliteBees,
                    countDefaultBees,
                    points, delta);

                for (int i = countLocalSearchPoints; i < countScoutBees; i++)
                {
                    points[i] = RandomPoint(lowerBound, upperBound, _parser.ArgumentsNumber);
                }
            }

            var t =  points.MinBy(point => _parser.Calculate(point));

            return t;
        }

        public double GetResult(double[] arguments)
            => _parser.Calculate(arguments);

        public string[] GetNamesArguments()
        {
            string[] names = new string[_parser.ArgumentsNumber];

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = _parser.ArgumentName(i);
            }

            return names;
        }

        private void LocalSearch(
            int countLocalSearchPoints, 
            int countElitePoint, 
            int countEliteBees, 
            int countDefaultBees,
            List<double[]> points,
            double delta)
        {
            for (int i = 0; i < countLocalSearchPoints; i++)
            {
                int r = i < countElitePoint ? countEliteBees : countDefaultBees;

                double[] temp = points[i];

                for (int j = 0; j < r; j++)
                {
                    var center = points[i];

                    double[] randomLocalityPoint = RandomLocalityPoint(center, delta);

                    if (_parser.Calculate(randomLocalityPoint) < _parser.Calculate(points[i]))
                    {
                        temp = randomLocalityPoint;
                    }
                }

                points[i] = temp;
            }
        }

        private double[] RandomLocalityPoint(double[] center, double delta)
        {
            int countArgument = center.Length;

            double[] res = new double[countArgument];

            Random random = new Random();

            for (int i = 0; i < countArgument; i++)
            {
                res[i] = random.NextDouble() * (2 * delta) + (center[i] - delta);
            }

            return res;
        }

        private double[] RandomPoint(double lowerBound, double upperBound, int countArgument)
        {
            double[] res = new double[countArgument];

            Random random = new Random();

            for (int i = 0; i < countArgument; i++)
            {
                res[i] = random.NextDouble() * (upperBound - lowerBound) + lowerBound;
            }

            return res;
        }
    }
}