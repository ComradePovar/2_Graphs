using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot.Series;
using OxyPlot;

namespace _2_Graphs
{
    public partial class Graph : Form
    {
        private PlotModel model;
        private LagrangePolynomial polynomial;
        private Func<double, double> f = x => Math.Log(1 + x * x) / (1 + x * x);
        private double lowerBound = 0;
        private double upperBound = 2;

        public Graph()
        {
            InitializeComponent();
            polynomial = new LagrangePolynomial()
            {
                Function = f,
                LowerFunctionBound = lowerBound,
                UpperFunctionBound = upperBound
            };
            model = new PlotModel()
            {
                LegendPlacement = LegendPlacement.Outside
                
            };
        }
        private void btnDraw_Click(object sender, EventArgs e)
        {
            int M, segmentCount;
            try
            {
                segmentCount = GetValue(tbN);
                M = GetValue(tbM);
            }
            catch (ArgumentException)
            {
                return;
            }

            double segmentLength = (polynomial.UpperFunctionBound - polynomial.LowerFunctionBound) / segmentCount;
            model.Series.Clear();
            SetInterpolationPoints(segmentLength, segmentCount);
            PlotGraphs(segmentLength, M);
        }
        private int GetValue(TextBox tb)
        {
            int value;
            if (!Int32.TryParse(tb.Text, out value) || value < 1)
            {
                MessageBox.Show($"Неверное значение параметра {tb.Tag}.");
                throw new ArgumentException();
            }
            return value;
        }
        private void SetInterpolationPoints(double segmentLength, int segmentCount)
        {
            ScatterSeries coloredPoints = new ScatterSeries
            {
                MarkerFill = OxyColors.DarkGreen,
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                Title = "Узлы интерполяции"
            };

            double[] interPoints = new double[segmentCount + 1];
            for (int i = 0; i < interPoints.Length; i++)
            {
                interPoints[i] = segmentLength * i;
                coloredPoints.Points.Add(new ScatterPoint(interPoints[i], f(interPoints[i])));
            }
            polynomial.InterPoints = interPoints;
            //model.Series.Add(coloredPoints);
        }
        double[] errors;
        private void PlotGraphs(double segmentLength, int M)
        {
            double k = segmentLength / M;
            errors = new double[(int)(2/k)];
            model.Series.Add(new FunctionSeries(f, polynomial.LowerFunctionBound,
                polynomial.UpperFunctionBound, segmentLength / M, $"ln(1 + x^2)/(1+x^2) c параметром M={M}"));

            for (int i = 0; i < errors.Length; i++)
            {
                errors[i] = f(i * k);
            }
            model.Series.Add(new FunctionSeries(polynomial.GetPolynomialValue, polynomial.LowerFunctionBound,
                polynomial.UpperFunctionBound, segmentLength / M, $"Многочлен Лагранжа степени {polynomial.Degree}"));
            for(int i = 0; i < errors.Length; i++)
            {
                errors[i] = Math.Abs(errors[i] - polynomial.GetPolynomialValue(i*k));
            }
            string[] str_errors = new string[errors.Length];
            for (int i = 0; i < errors.Length; i++)
            {
                str_errors[i] = errors[i].ToString();
            }
            System.IO.File.WriteAllLines(@"H:\Projects\C#\Lab1\2_Graphs\2_Graphs\TextFile1.txt", str_errors);
            plot.Model = model;
            plot.InvalidatePlot(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int M, segmentCount;
            model.Series.Clear();
            try
            {
                segmentCount = GetValue(tbN);
                M = GetValue(tbM);
            }
            catch (ArgumentException)
            {
                return;
            }

            double segmentLength = (polynomial.UpperFunctionBound - polynomial.LowerFunctionBound) / segmentCount;
            SetInterpolationPoints(segmentLength, segmentCount);
            double k = segmentLength / M;
            errors = new double[(int)(2 / k)];
            for (int i = 0; i < errors.Length; i++)
            {
                errors[i] = f(i * k);
            }
            for (int i = 0; i < errors.Length; i++)
            {
                errors[i] = Math.Abs(errors[i] - polynomial.GetPolynomialValue(i * k));
            }

            model.Series.Add(new FunctionSeries(err, 0, errors.Length - 1, errors.Length, "errors"));
            plot.Model = model;
            plot.InvalidatePlot(true);
        }
        private double err(double index)
        {
            return errors[(int)index];
        }
    }
}
