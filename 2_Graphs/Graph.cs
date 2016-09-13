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
        PlotModel model;

        public Graph()
        {
            InitializeComponent();
            model = new PlotModel()
            {
                LegendPlacement = LegendPlacement.Outside
            };
            
            model.Series.Add()
        }
    }
}
