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
            polynomial = new LagrangePolynomial(f, lowerBound, upperBound);
            model = new PlotModel()
            {
                LegendPlacement = LegendPlacement.Outside
            };

        }
        private void btnDraw_Click(object sender, EventArgs e)
        {
            int M;
            try
            {
                polynomial.Degree = GetValue(tbN);
                M = GetValue(tbM);
            }
            catch (ArgumentException)
            {
                return;
            }

            double segmentLength = (polynomial.UpperBound-polynomial.LowerBound) / polynomial.Degree;
            SetInterpolationPoints(segmentLength);
            PlotGraphs(segmentLength, M);
        }
        private int GetValue(TextBox tb)
        {
            int value;
            if (!Int32.TryParse(tb.Text, out value) && value > 0)
            {
                MessageBox.Show("Неверное значение параметра M.");
                throw new ArgumentException();
            }
            return value;
        }
        private void SetInterpolationPoints(double segmentLength)
        {
            double[] interPoints = new double[polynomial.Degree + 1];
            for (int i = 0; i < interPoints.Length; i++)
            {
                interPoints[i] = segmentLength * i;
            }
            polynomial.InterPoints = interPoints;
        }
        private void PlotGraphs(double segmentLength, int M)
        {
            model.Series.Clear();
            model.Series.Add(new FunctionSeries(f, polynomial.LowerBound,
                polynomial.UpperBound, segmentLength / M, $"ln(1 + x^2)/(1+x^2) c параметром M={M}"));
            model.Series.Add(new FunctionSeries(polynomial.InterpolatePolynomial, polynomial.LowerBound,
                polynomial.UpperBound, segmentLength / M, $"Многочлен Лагранжа степени {polynomial.Degree}"));
            plot.Model = model;
            plot.InvalidatePlot(true);
        }
    }
}
