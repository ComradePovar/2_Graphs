using System;

namespace _2_Graphs
{
    public interface IPolynomialInterpolation
    {
        double[] FuncValues { get; }
        double[] InterPoints { get; }
        int Degree { get; }
        Func<double, double> Function { get; }

        double Interpolate(double x);
        void InitPolynomial(int degree, double[] x, double[] y);
    }
}
