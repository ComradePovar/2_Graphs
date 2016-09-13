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
    }
}
