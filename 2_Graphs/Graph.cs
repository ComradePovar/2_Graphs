using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.Axes;

namespace _2_Graphs
{
    public partial class Graph : Form
    {
        private PlotModel model;
        private OLSPolynomial polynomial;
        private Func<double, double> f = x => Math.Log(1 + x * x) / (1 + x * x);
        private double lowerBound = 0;
        private double upperBound = 2;
        //private Func<double, double> f = x => (1 + Math.Sin(Math.Pow(x, 3)) * Math.Pow(x, 4)) / (1 + Math.Pow(x, 4));
        //private double lowerBound = -3;
        //private double upperBound = 3;
        public Graph()
        {
            InitializeComponent();
            polynomial = new OLSPolynomial()
            {
                Function = f
            };
            model = new PlotModel()
            {
                LegendPlacement = LegendPlacement.Outside
                
            };
            LinearAxis ax = new LinearAxis()
            {
                FilterMinValue = -1,
                FilterMaxValue = 1
            };
            //model.Axes.Add(ax);
        }
        private void btnDraw_Click(object sender, EventArgs e)
        {
            int M, pointsCount;
            try
            {
                pointsCount = GetValue(tbN);
                M = GetValue(tbM);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            btnDraw.Enabled = false;
            double segmentLength = (upperBound - lowerBound) / pointsCount;
            model.Series.Clear();
            SetInterpolationPoints(segmentLength, pointsCount, GetValue(textBox1));
            polynomial.SetInterpolationCoefficients();
            Console.Clear();
            for (int i = 0; i < polynomial._coefficients.Length; i++)
            {
                Console.WriteLine("c[" + i + "] = " + polynomial._coefficients[i]);
            }
            PlotFunctionGraphAsync(segmentLength, M);
            PlotPolynomialGraphAsync(segmentLength, M);
        }
        private int GetValue(TextBox tb)
        {
            int value;
            if (!Int32.TryParse(tb.Text, out value) || value < 0)
            {
                throw new ArgumentException($"Неверное значение параметра {tb.Tag}.");
            }
            return value;
        }
        private void SetInterpolationPoints(double segmentLength, int pointsCount, int degree)
        {
            double[] interPoints = new double[pointsCount + 1];
            polynomial.InterPointsCount = pointsCount + 1;
            polynomial.Degree = degree;

            for (int i = 0; i < interPoints.Length; i++)
                interPoints[i] = segmentLength * i + lowerBound;

            polynomial.InterPoints = interPoints;                
        }
        private async void PlotPolynomialGraphAsync(double segmentLength, int M)
        {
            await Task.Run(() =>
            { 
                model.Series.Add(new FunctionSeries(polynomial.Interpolate, lowerBound, upperBound,
                    segmentLength / M, $"Многочлен н. ср. кв. прибл. степени {polynomial.Degree}"));
                plot.Model = model;
                plot.InvalidatePlot(true);
            });

            btnDraw.Enabled = true;
        }
        private async void PlotFunctionGraphAsync(double segmentLength, int M)
        {
            await Task.Run(() =>
            {
               model.Series.Insert(0, new FunctionSeries(f, lowerBound, upperBound,
                segmentLength / M, $"ln(1 + x^2)/(1+x^2) c параметром M={M}"));
               plot.Model = model;
               plot.InvalidatePlot(true);
            });
        }
    }
}
