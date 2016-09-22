using System;
using System.Linq;
using System.Windows.Forms;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.Axes;

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
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
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
                throw new ArgumentException($"Неверное значение параметра {tb.Tag}.");
            }
            return value;
        }
        private void SetInterpolationPoints(double segmentLength, int segmentCount)
        {
            ScatterSeries coloredPoints = new ScatterSeries
            {
                MarkerFill = OxyColors.DarkGreen,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                Title = "Узлы интерполяции"
            };

            double[] interPoints = new double[segmentCount + 1];
            for (int i = 0; i < interPoints.Length; i++)
            {
                interPoints[i] = segmentLength * i;
                coloredPoints.Points.Add(new ScatterPoint(interPoints[i], f(interPoints[i])));
            }
            polynomial.InterPoints = interPoints;
            model.Series.Add(coloredPoints);
        }
        private void PlotGraphs(double segmentLength, int M)
        {
            model.Series.Insert(0, new FunctionSeries(f, polynomial.LowerFunctionBound,
                polynomial.UpperFunctionBound, segmentLength / M, $"ln(1 + x^2)/(1+x^2) c параметром M={M}"));
            model.Series.Add(new FunctionSeries(polynomial.GetPolynomialValue, polynomial.LowerFunctionBound,
                polynomial.UpperFunctionBound, segmentLength / M, $"Многочлен Лагранжа степени {polynomial.Degree}"));
            LinearAxis ax = new LinearAxis()
            {
                FilterMaxValue = 1,
                FilterMinValue = -1
            };
            model.Axes.Clear();
            model.Axes.Add(ax);
            plot.Model = model;
            plot.InvalidatePlot(true);
        }
        private void btnError_Click(object sender, EventArgs e)
        {
            try
            {
                Errors errors = new _2_Graphs.Errors(GetValue(tbM), ref polynomial);
                errors.Show();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
