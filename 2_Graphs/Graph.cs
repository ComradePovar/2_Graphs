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
        private CubicSpline polynomial;
        private Func<double, double> f = x => Math.Log(1 + x * x) / (1 + x * x);
        private double lowerBound = 0;
        private double upperBound = 2;
        public Graph()
        {
            InitializeComponent();
            polynomial = new CubicSpline();
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
                x = new double[pointsCount];
                y = new double[pointsCount];
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            btnDraw.Enabled = false;
            double segmentLength = (upperBound - lowerBound) / (pointsCount-1);
            model.Series.Clear();
            SetInterpolationPoints(segmentLength, pointsCount);
            if (radioButton1.Checked)
                polynomial.BuildSplineNatural(x, y, x.Length);
            else if (radioButton2.Checked)
                polynomial.BuildSplineParabolic(x, y, x.Length);
            else
                polynomial.BuildSplineExact(x, y, x.Length);
            PlotFunctionGraphAsync(segmentLength, M);
            //PlotPolynomialGraphAsync(segmentLength, M);
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
        private double[] x;
        private double[] y;
        private void SetInterpolationPoints(double segmentLength, int pointsCount)
        {
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = segmentLength * i + lowerBound;
                y[i] = f(x[i]);
            }            
        }
        private async void PlotPolynomialGraphAsync(double segmentLength, int M)
        {
            await Task.Run(() =>
            { 
                model.Series.Add(new FunctionSeries(polynomial.Interpolate, lowerBound, upperBound,
                    segmentLength / M, $"Многочлен н. ср. кв. прибл. степени N"));
                plot.Model = model;
                plot.InvalidatePlot(true);
            });

            btnDraw.Enabled = true;
        }
        private void PlotFunctionGraphAsync(double segmentLength, int M)
        {

            model.Series.Insert(0, new FunctionSeries(f, lowerBound, upperBound,
            segmentLength / M, $"ln(1 + x^2)/(1+x^2) c параметром M={M}"));
            model.Series.Add(new FunctionSeries(polynomial.Interpolate, lowerBound, upperBound,
                segmentLength / M, $"Многочлен н. ср. кв. прибл. степени N"));
            plot.Model = model;
            plot.InvalidatePlot(true);
            btnDraw.Enabled = true;
        }
    }
}
