using System;

namespace _2_Graphs
{
    public class NewtonPolynomial : IPolynomialInterpolation
    {
        private double[] _funcValues;
        private double[] _interPoints;
        private int _degree;
        private Func<double, double> _function;

        public double[] FuncValues
        {
            get
            {
                return _funcValues;
            }
            private set
            {
                _funcValues = value;
                _degree = _funcValues.Length - 1;
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
                _degree = _interPoints.Length - 1;
                if (_function != null)
                {
                    FuncValues = new double[_interPoints.Length];
                    for (int i = 0; i < _interPoints.Length; i++)
                        FuncValues[i] = _function(_interPoints[i]);
                }                    
            }
        }
        public int Degree
        {
            get
            {
                return _degree;
            }
            private set
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
                if (InterPoints != null)
                    for (int i = 0; i < FuncValues.Length; i++)
                        FuncValues[i] = _function(InterPoints[i]);
            }
        }

        public NewtonPolynomial() { }
        public double Interpolate(double x)
        {
            if (FuncValues == null)
            {
                if (_degree == 0)
                    throw new MissingMemberException("We require more vespen gas.");
                FuncValues = new double[_degree + 1];
                for (int i = 0; i < FuncValues.Length; i++)
                    FuncValues[i] = _function(InterPoints[i]);
            }
            if (InterPoints == null)
                throw new MissingMemberException("We require more minerals.");

            double[][] dividedDiff = new double[_degree + 1][];
            dividedDiff[0] = FuncValues;
            double res = dividedDiff[0][0];

            for (int i = 1; i <= _degree; i++)
            {
                dividedDiff[i] = new double[_degree + 1 - i];
                for (int j = 0; j < dividedDiff[i].Length; j++)
                {
                    dividedDiff[i][j] = (dividedDiff[i - 1][j + 1] - dividedDiff[i - 1][j]) /
                                   (InterPoints[j + i] - InterPoints[j]);
                }

                double temp = 1;
                for (int j = 0; j < i; j++)
                    temp *= (x - InterPoints[j]);

                res += dividedDiff[i][0] * temp;
            }


            double maxValue = 1;
            double minValue = -1;
            if (res > maxValue)
                return maxValue;
            if (res < minValue)
                return minValue;

            return res;
        }
        public void InitPolynomial(int degree, double[] x, double[] y)
        {
            _degree = degree;
            _interPoints = x;
            _funcValues = y;
        }
    }
}
