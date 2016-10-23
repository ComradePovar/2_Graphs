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
        private NewtonPolynomial polynomial;
        private Func<double, double> f = x => Math.Log(1 + x * x) / (1 + x * x);
        private double lowerBound = 0;
        private double upperBound = 2;

        public Graph()
        {
            InitializeComponent();
            polynomial = new NewtonPolynomial()
            {
                Function = f
            };
            model = new PlotModel()
            {
                LegendPlacement = LegendPlacement.Outside
                
            };
            LinearAxis ax = new LinearAxis()
            {
                FilterMinValue = 1,
                FilterMaxValue = 1
            };
            //model.Axes.Add(ax);
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

            btnDraw.Enabled = false;
            double segmentLength = (upperBound - lowerBound) / segmentCount;
            model.Series.Clear();
            SetInterpolationPoints(segmentLength, segmentCount);
            PlotFunctionGraphAsync(segmentLength, M);
            PlotPolynomialGraphAsync(segmentLength, M);
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
            double[] interPoints = new double[segmentCount + 1];
            if (segmentCount < 50)
            {
                ScatterSeries coloredPoints = new ScatterSeries
                {
                    MarkerFill = OxyColors.DarkGreen,
                    MarkerType = MarkerType.Circle,
                    MarkerSize = 3,
                    Title = "Узлы интерполяции"
                };

                for (int i = 0; i < interPoints.Length; i++)
                {
                    interPoints[i] = segmentLength * i - lowerBound;
                    coloredPoints.Points.Add(new ScatterPoint(interPoints[i], f(interPoints[i])));
                }

                model.Series.Add(coloredPoints);
            }
            else
            {
                for (int i = 0; i < interPoints.Length; i++)
                {
                    interPoints[i] = segmentLength * i - lowerBound;
                }
            }
            polynomial.InterPoints = interPoints;                
        }
        private async void PlotPolynomialGraphAsync(double segmentLength, int M)
        {
            await Task.Run(() =>
            { 
                model.Series.Add(new FunctionSeries(polynomial.Interpolate, lowerBound, upperBound,
                    segmentLength / M, $"Многочлен Ньютона степени {polynomial.Degree}"));
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
