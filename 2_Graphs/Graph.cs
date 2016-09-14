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
        private const double incrementX = 0.00001;

        public Graph()
        {
            InitializeComponent();
            polynomial = new LagrangePolynomial(x => Math.Log(1 + x * x) / (1 + x * x), 0, 2);
            model = new PlotModel()
            {
                LegendPlacement = LegendPlacement.Outside
            };

            model.Series.Add(new FunctionSeries(x => Math.Log(1 + x * x) / (1 + x * x), polynomial.LowerBound,
                polynomial.UpperBound, incrementX, "ln(1 + x^2)/(1+x^2)"));
            plot.Model = model;
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            int segmentCount, M;
            if (!Int32.TryParse(tbN.Text, out segmentCount))
            {
                MessageBox.Show("Неверное значение количества отрезков.");
                return;
            }
            if (!Int32.TryParse(tbM.Text, out M))
            {
                MessageBox.Show("Неверное значение параметра M.");
                return;
            }

            polynomial.Degree = segmentCount;
            double segmentLength = (polynomial.UpperBound-polynomial.LowerBound) / segmentCount;
            double[] interPoints = new double[polynomial.Degree + 1];
            for (int i = 0; i < interPoints.Length; i++)
            {
                interPoints[i] = segmentLength * i;
            }
            polynomial.InterPoints = interPoints;

            if (model.Series.Count > 3)
                model.Series.RemoveAt(1);
            model.Series.Add(new FunctionSeries(polynomial.InterpolatePolynomial, polynomial.LowerBound,
                polynomial.UpperBound, segmentLength, $"Многочлен Лагранжа степени {polynomial.Degree}"));
            plot.Model = model;
            plot.InvalidatePlot(true);
        }
    }
}
