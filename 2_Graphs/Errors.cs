using System;
using System.Linq;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace _2_Graphs
{
    public partial class Errors : Form
    {
        private LagrangePolynomial polynomial;
        private PlotModel model;
        private int M;

        public Errors()
        {
            InitializeComponent();
        }
        public Errors(int M, ref LagrangePolynomial polynomial)
        {
            InitializeComponent();
            model = new PlotModel()
            {
                LegendPlacement = LegendPlacement.Outside

            };
            this.M = M;
            this.polynomial = polynomial;
        }
        private async void btnDraw_Click(object sender, EventArgs e)
        {            
            try
            {
                btnDraw.Enabled = false;
                int minDegree = GetValue(tbMin);
                int maxDegree = GetValue(tbMax);
                if (maxDegree < minDegree)
                    throw new ArgumentException("Минимальная степень многочлена должна быть меньше максимальной.");
                model.Series.Clear();
                await Task.Run(() => CalculateAndPlot(minDegree, maxDegree));
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                btnDraw.Enabled = true;
            }
        }
        private int GetValue(TextBox tb)
        {
            int value;
            if (!Int32.TryParse(tb.Text, out value) || value < 1)
            {
                throw new ArgumentException($"Неверное значение {tb.Tag}.");
            }
            return value;
        }
        public delegate void ChangeUI(string data);
        private void PlotErrorGraph(int minDegree, int maxDegree, double[] errors)
        {
            model.Series.Insert(0, new FunctionSeries(x => errors[(int)x - minDegree], minDegree, maxDegree, maxDegree - minDegree + 1,
                "величина погрешности")
            {
                Color = OxyColors.Red
            });    
            plot.Model = model;
            plot.InvalidatePlot(true);
        }
        private Task CalculateAndPlot(int minDegree, int maxDegree)
        {
            var tcs = new TaskCompletionSource<object>();
            double[] maxErrors = GetMaxErrors(minDegree, maxDegree);
            int minIndex = 0;
            int maxIndex = 0;
            for (int i = 1; i < maxErrors.Length; i++)
            {
                if (maxErrors[minIndex] > maxErrors[i])
                    minIndex = i;
                if (maxErrors[maxIndex] < maxErrors[i])
                    maxIndex = i;
            }

            PlotErrorGraph(minDegree, maxDegree, maxErrors);
            MessageBox.Show($"Минимальная погрешность равна {maxErrors[minIndex]} при степени полинома {minIndex + minDegree}.");
            MessageBox.Show($"Максимальная погрешность равна {maxErrors[maxIndex]} при степени полинома {maxIndex + minDegree}.");

            tcs.SetResult(null);
            return tcs.Task;
        }
        private double[] GetMaxErrors(int minDegree, int maxDegree)
        {
            ScatterSeries coloredPoints = new ScatterSeries
            {
                MarkerFill = OxyColors.DarkRed,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                Title = "степени полинома"
            };
            double[] maxErrors = new double[maxDegree - minDegree + 1];

            double range = polynomial.UpperFunctionBound - polynomial.LowerFunctionBound;
            for (int i = minDegree; i <= maxDegree; i++)
            {
                double segmentLength = range / i;

                double[] interPoints = new double[i + 1];
                for (int j = 0; j < interPoints.Length; j++)
                {
                    interPoints[j] = segmentLength * j;
                }
                polynomial.InterPoints = interPoints;

                double[] errors = new double[(i + 1) * M];
                for (int j = 0; j < errors.Length; j++)
                {
                    errors[j] = polynomial.Function(segmentLength * j / M);
                    errors[j] = Math.Abs(errors[j] - polynomial.GetPolynomialValue(segmentLength * j / M));
                }
                maxErrors[i - minDegree] = errors.Max();
                coloredPoints.Points.Add(new ScatterPoint(i, maxErrors[i - minDegree]));
            }

            model.Series.Add(coloredPoints);
            return maxErrors;
        }
    }
}
