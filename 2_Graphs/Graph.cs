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
            double segmentLength = (upperBound - lowerBound) / (pointsCount - 1);
            model.Series.Clear(); 
            SetInterpolationPoints(segmentLength, pointsCount);
            double[] d = new double[pointsCount];
            for (int i = 0; i < pointsCount; i++)
            {
                d[i] = d1(i * segmentLength + lowerBound);
            }
            alglib.spline1dbuildcubic(x, y, out s1);
            alglib.spline1dbuildcubic(x, y, x.Length, 2, d2(0), 2, d2(2), out s2);
            //alglib.spline1dbuildcatmullrom(x, y, x.Length, 0, 0.1, out s3);
            //alglib.spline1dbuildcubic(x, y, x.Length, 2, 1, 2, 1 , out s3);
            polynomial.BuildSplineStart(x, y, x.Length);
            alglib.spline1dbuildcubic(x, y, x.Length, 2, 0, 2, 0, out s4);
           
            PlotFunctionGraphAsync(segmentLength, M);
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
        private void PlotFunctionGraphAsync(double segmentLength, int M)
        {
            model.Series.Add(new FunctionSeries(f, lowerBound, upperBound,
            segmentLength / M, $"ln(1 + x^2)/(1+x^2) c параметром M={M}")
            {
                Color = OxyColors.Black
            });
            model.Series.Add(new FunctionSeries(interpolate1, lowerBound, upperBound,
                segmentLength / M, $"Куб. сплайн с параболическими концевыми участками")
            {
                Color = OxyColors.Red
            });
            model.Series.Add(new FunctionSeries(interpolate2, lowerBound, upperBound,
                segmentLength / M, $"Куб. сплайн с точными граничными условиями")
            {
                Color = OxyColors.Purple
            });
            model.Series.Add(new FunctionSeries(polynomial.Interpolate, lowerBound, upperBound,
                segmentLength / M, $"Куб. сплайн с начальными условиями")
            {
                Color = OxyColors.Gray
            });
            model.Series.Add(new FunctionSeries(interpolate4, lowerBound, upperBound,
                segmentLength / M, $"Естественный куб. сплайн")
            {
                Color = OxyColors.Yellow
            });
            plot.Model = model;
            plot.InvalidatePlot(true);
            btnDraw.Enabled = true;
        }
    }
}
