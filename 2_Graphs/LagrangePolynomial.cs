using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Graphs
{
    class LagrangePolynomial
    {
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }

        public double[] FuncValues { get; private set; }
        public double X { get; set; }
        public double[] InterPoints { get; set; }
        public int Degree { get; set; }
        private Func<double, double> f;

        public LagrangePolynomial() { }
        public LagrangePolynomial(Func<double, double> function, double lowerBound, double upperBound)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
            f = function;
        }
        public LagrangePolynomial(int degree, double[] interPoints)
        {
            Degree = degree;
            InterPoints = interPoints;
        }
        public double InterpolatePolynomial(double x)
        {
            FuncValues = new double[Degree + 1];
            for (int i = 0; i < FuncValues.Length; i++)
                FuncValues[i] = f(InterPoints[i]);

            double p = 0;
            for (int i = 0; i < FuncValues.Length; i++)
            {
                double l = 1;
                for (int j = 0; j < InterPoints.Length; j++)
                {
                    if (i == j)
                        continue;
                    l *= (x - InterPoints[j]) / (InterPoints[i] - InterPoints[j]);
                }
                p += FuncValues[i] * l;
            }

            return p;
        }
    }
}
