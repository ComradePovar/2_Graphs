namespace _2_Graphs
{
    partial class Graph
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
            this.plot.MaximumSize = new System.Drawing.Size(800, 400);
            this.plot.Name = "plot";
            this.plot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plot.Size = new System.Drawing.Size(784, 400);
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
            this.grpBox.Location = new System.Drawing.Point(13, 406);
            this.grpBox.Name = "grpBox";
            this.grpBox.Size = new System.Drawing.Size(771, 43);
            this.grpBox.TabIndex = 1;
            this.grpBox.TabStop = false;
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(655, 17);
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
            this.lbN.Size = new System.Drawing.Size(158, 13);
            this.lbN.TabIndex = 1;
            this.lbN.Text = "Число частичных отрезков N:";
            // 
            // tbN
            // 
            this.tbN.Location = new System.Drawing.Point(170, 17);
            this.tbN.Name = "tbN";
            this.tbN.Size = new System.Drawing.Size(59, 20);
            this.tbN.TabIndex = 0;
            this.tbN.Tag = "N";
            // 
            // Graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.grpBox);
            this.Controls.Add(this.plot);
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.MinimumSize = new System.Drawing.Size(800, 500);
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
    }
}

