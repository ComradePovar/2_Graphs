using System;

namespace _2_Graphs
{
    public class OLSPolynomial : IPolynomialInterpolation
    {
        private double[] _funcValues;
        private double[] _interPoints;
        private int _degree;
        private Func<double, double> _function;
        public double[] _coefficients;
        public int InterPointsCount { get; set; }
        public double[] FuncValues
        {
            get
            {
                return _funcValues;
            }
            private set
            {
                _funcValues = value;
            }
        }
        public double[] InterPoints
        {
            get
            {
                return _interPoints;
            }
            set
            {
                _interPoints = value;                  
            }
        }
        public int Degree
        {
            get
            {
                return _degree;
            }
            set
            {
                _degree = value;
            }
        }
        public Func<double, double> Function
        {
            get
            {
                return _function;
            }
            set
            {
                _function = value;
            }
        }

        public OLSPolynomial() { }
        public void SetInterpolationCoefficients()
        {

                FuncValues = new double[InterPointsCount];
                for (int i = 0; i < FuncValues.Length; i++)
                    FuncValues[i] = _function(InterPoints[i]);

            _coefficients = new double[Degree + 1];
            double[,] tempMatrix = new double[Degree + 1, Degree + 2];

            // Ai,j = SUM from i=0 to Degree (x^i * x^j)
            // Ai, n-1 = SUM from i = 0 to Degree (f(Xi) * x^i)
            for (int i = 0; i <= Degree; i++)
                for (int j = 0; j <= Degree; j++)
                {
                    double sumA = 0, sumB = 0;
                    for (int k = 0; k < InterPointsCount; k++)
                    {
                        sumA += Math.Pow(InterPoints[k], i) * Math.Pow(InterPoints[k], j);
                        sumB += FuncValues[k] * Math.Pow(InterPoints[k], i);
                    }
                    tempMatrix[i, j] = sumA;
                    tempMatrix[i, Degree + 1] = sumB;
                }

            for (int i = 0; i <= Degree; i++)
            {
                for (int j = i; j <= Degree; j++)
                {
                    double denominator = tempMatrix[j, i];
                    for (int k = 0; k <= Degree + 1; k++)
                        tempMatrix[j, k] /= denominator;
                }

                for (int j = i + 1; j < Degree + 1; j++)
                    for (int k = i; k < Degree + 1 + 1; k++)
                        tempMatrix[j, k] -= tempMatrix[i, k];
            }

            for (int i = Degree; i > 0; i--)
                for (int j = 0; j < i; j++)
                {
                    double denominator = tempMatrix[j, i];
                    for (int k = i; k < Degree + 1 + 1; k++)
                        tempMatrix[j, k] -= tempMatrix[i, k] * denominator;
                }

            for (int i = 0; i <= Degree; i++)
                _coefficients[i] = tempMatrix[i, Degree + 1];
        }

        public void InitPolynomial(int degree, double[] x, double[] y)
        {
            _degree = degree;
            _interPoints = x;
            _funcValues = y;
        }

        public double Interpolate(double x)
        {
            double res = 0;
            for (int i = 0; i <= Degree; i++)
                res += _coefficients[i] * Math.Pow(x, i);

            double maxValue = 3;
            double minValue = -3;
            if (res > maxValue)
                return maxValue;
            if (res < minValue)
                return minValue;

            return res;
        }
    }
}
