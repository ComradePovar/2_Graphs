namespace _2_Graphs
{
    sealed partial class Graph
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.plot = new OxyPlot.WindowsForms.PlotView();
            this.grpBox = new System.Windows.Forms.GroupBox();
            this.btnDraw = new System.Windows.Forms.Button();
            this.tbM = new System.Windows.Forms.TextBox();
            this.lbM = new System.Windows.Forms.Label();
            this.lbN = new System.Windows.Forms.Label();
            this.tbN = new System.Windows.Forms.TextBox();
            this.grpBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // plot
            // 
            this.plot.Dock = System.Windows.Forms.DockStyle.Top;
            this.plot.Location = new System.Drawing.Point(0, 0);
            this.plot.Name = "plot";
            this.plot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plot.Size = new System.Drawing.Size(1184, 681);
            this.plot.TabIndex = 0;
            this.plot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // grpBox
            // 
            this.grpBox.Controls.Add(this.btnDraw);
            this.grpBox.Controls.Add(this.tbM);
            this.grpBox.Controls.Add(this.lbM);
            this.grpBox.Controls.Add(this.lbN);
            this.grpBox.Controls.Add(this.tbN);
            this.grpBox.Location = new System.Drawing.Point(12, 676);
            this.grpBox.Name = "grpBox";
            this.grpBox.Size = new System.Drawing.Size(1171, 43);
            this.grpBox.TabIndex = 1;
            this.grpBox.TabStop = false;
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(1055, 11);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(104, 26);
            this.btnDraw.TabIndex = 4;
            this.btnDraw.Text = "Начертить";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // tbM
            // 
            this.tbM.Location = new System.Drawing.Point(314, 17);
            this.tbM.Name = "tbM";
            this.tbM.Size = new System.Drawing.Size(59, 20);
            this.tbM.TabIndex = 3;
            this.tbM.Tag = "M";
            // 
            // lbM
            // 
            this.lbM.AutoSize = true;
            this.lbM.Location = new System.Drawing.Point(235, 20);
            this.lbM.Name = "lbM";
            this.lbM.Size = new System.Drawing.Size(73, 13);
            this.lbM.TabIndex = 2;
            this.lbM.Text = "Параметр M:";
            // 
            // lbN
            // 
            this.lbN.AutoSize = true;
            this.lbN.Location = new System.Drawing.Point(6, 20);
            this.lbN.Name = "lbN";
            this.lbN.Size = new System.Drawing.Size(112, 13);
            this.lbN.TabIndex = 1;
            this.lbN.Text = "Количество узлов N:";
            // 
            // tbN
            // 
            this.tbN.Location = new System.Drawing.Point(124, 17);
            this.tbN.Name = "tbN";
            this.tbN.Size = new System.Drawing.Size(59, 20);
            this.tbN.TabIndex = 0;
            this.tbN.Tag = "N";
            // 
            // Graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 716);
            this.Controls.Add(this.grpBox);
            this.Controls.Add(this.plot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Graph";
            this.Text = "Graph";
            this.grpBox.ResumeLayout(false);
            this.grpBox.PerformLayout();
            this.ResumeLayout(false);

        }

        private OxyPlot.WindowsForms.PlotView plot;
        #endregion

        private System.Windows.Forms.GroupBox grpBox;
        private System.Windows.Forms.Label lbN;
        private System.Windows.Forms.TextBox tbN;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.TextBox tbM;
        private System.Windows.Forms.Label lbM;
        static alglib.spline1dinterpolant s1;
        static alglib.spline1dinterpolant s2;
        static alglib.spline1dinterpolant s3;
        static alglib.spline1dinterpolant s4;

        System.Func<double, double> d1 = x => ((2 * x * (System.Math.Log(x * x + 1) - 1)) / System.Math.Pow(x * x + 1, 2));
        System.Func<double, double> d2 = x => (-10 * x * x + (6 * x * x - 2) * System.Math.Log(x * x + 1) + 2) / System.Math.Pow(x * x + 1, 3);
        System.Func<double, double> interpolate1 = x => alglib.spline1dcalc(s1, x);
        System.Func<double, double> interpolate2 = x => alglib.spline1dcalc(s2, x);
        System.Func<double, double> interpolate3 = x => alglib.spline1dcalc(s3, x);
        System.Func<double, double> interpolate4 = x => alglib.spline1dcalc(s4, x);
        private void BuildSplines()
        {
            alglib.spline1dbuildcubic(x, y, out s1);
            alglib.spline1dbuildcubic(x, y, x.Length, 1,0, 1, 0, out s2);
            polynomial.BuildSplineStart(x, y, x.Length);
            alglib.spline1dbuildcubic(x, y, x.Length, 2, 0, 2, 0, out s4);
        }
    }
}

