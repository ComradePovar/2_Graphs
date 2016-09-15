using System;

namespace _2_Graphs
{
    abstract class Function
    {
        public double LowerFunctionBound { get; set; } = Double.MinValue;
        public double UpperFunctionBound { get; set; } = Double.MaxValue;
        protected Func<double, double> function;

    }
    abstract class PolynomialInterpolation : Function
    {
        public int Degree { get; protected set; }

        protected double[] funcValues;
        protected double[] interPoints;

        public abstract double GetPolynomialValue(double arg);
    }
    class LagrangePolynomial : PolynomialInterpolation
    {
        public double[] FuncValues
        {
            get
            {
                return funcValues;
            }
            set
            {
                funcValues = value;
                Degree = funcValues.Length - 1;
            }
        }
        public double[] InterPoints
        {
            get
            {
                return interPoints;
            }
            set
            {
                interPoints = value;
                Degree = interPoints.Length - 1;
                if (function != null)
                {
                    FuncValues = new double[interPoints.Length];
                    for (int i = 0; i < interPoints.Length; i++)
                        FuncValues[i] = function(interPoints[i]);
                }                    
            }
        }
        public Func<double, double> Function
        {
            get
            {
                return function;
            }
            set
            {
                function = value;
                if (InterPoints != null)
                    for (int i = 0; i < FuncValues.Length; i++)
                        FuncValues[i] = function(InterPoints[i]);
            }
        }

        public LagrangePolynomial() { }
        public override double GetPolynomialValue(double arg)
        {
            if (FuncValues == null)
            {
                if (Degree == 0)
                    throw new MissingMemberException("We require more vespen gas.");
                FuncValues = new double[Degree + 1];
                for (int i = 0; i < FuncValues.Length; i++)
                    FuncValues[i] = function(InterPoints[i]);
            }
            if (InterPoints == null)
                throw new MissingMemberException("We require more minerals.");

            double p = 0;
            for (int i = 0; i < FuncValues.Length; i++)
            {
                double l = 1;
                for (int j = 0; j < InterPoints.Length; j++)
                {
                    if (i == j)
                        continue;
                    l *= (arg - InterPoints[j]) / (InterPoints[i] - InterPoints[j]);
                }
                p += FuncValues[i] * l;
            }

            return p;
        }
    }
}
