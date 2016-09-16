namespace _2_Graphs
{
    partial class Errors
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
            this.tbMax = new System.Windows.Forms.TextBox();
            this.lbMax = new System.Windows.Forms.Label();
            this.lbMin = new System.Windows.Forms.Label();
            this.tbMin = new System.Windows.Forms.TextBox();
            this.grpBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // plot
            // 
            this.plot.Location = new System.Drawing.Point(12, 12);
            this.plot.Name = "plot";
            this.plot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plot.Size = new System.Drawing.Size(760, 360);
            this.plot.TabIndex = 0;
            this.plot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // grpBox
            // 
            this.grpBox.Controls.Add(this.btnDraw);
            this.grpBox.Controls.Add(this.tbMax);
            this.grpBox.Controls.Add(this.lbMax);
            this.grpBox.Controls.Add(this.lbMin);
            this.grpBox.Controls.Add(this.tbMin);
            this.grpBox.Location = new System.Drawing.Point(12, 378);
            this.grpBox.Name = "grpBox";
            this.grpBox.Size = new System.Drawing.Size(760, 71);
            this.grpBox.TabIndex = 1;
            this.grpBox.TabStop = false;
            this.grpBox.Text = "График величины погрешности полинома степени не выше n";
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(676, 19);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(78, 46);
            this.btnDraw.TabIndex = 5;
            this.btnDraw.Text = "Нарисовать";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // tbMax
            // 
            this.tbMax.Location = new System.Drawing.Point(293, 38);
            this.tbMax.Name = "tbMax";
            this.tbMax.Size = new System.Drawing.Size(55, 20);
            this.tbMax.TabIndex = 4;
            this.tbMax.Tag = "максимальной степени";
            // 
            // lbMax
            // 
            this.lbMax.Location = new System.Drawing.Point(179, 30);
            this.lbMax.Name = "lbMax";
            this.lbMax.Size = new System.Drawing.Size(108, 35);
            this.lbMax.TabIndex = 3;
            this.lbMax.Text = "Максимальная степень полинома:";
            // 
            // lbMin
            // 
            this.lbMin.Location = new System.Drawing.Point(6, 30);
            this.lbMin.Name = "lbMin";
            this.lbMin.Size = new System.Drawing.Size(106, 35);
            this.lbMin.TabIndex = 2;
            this.lbMin.Text = "Минимальная степень полинома:";
            // 
            // tbMin
            // 
            this.tbMin.Location = new System.Drawing.Point(118, 38);
            this.tbMin.Name = "tbMin";
            this.tbMin.Size = new System.Drawing.Size(55, 20);
            this.tbMin.TabIndex = 0;
            this.tbMin.Tag = "минимальной степени";
            // 
            // Errors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.grpBox);
            this.Controls.Add(this.plot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Errors";
            this.Text = "Errors";
            this.grpBox.ResumeLayout(false);
            this.grpBox.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.GroupBox grpBox;
        private System.Windows.Forms.TextBox tbMax;
        private System.Windows.Forms.Label lbMax;
        private System.Windows.Forms.Label lbMin;
        private System.Windows.Forms.TextBox tbMin;
        private System.Windows.Forms.Button btnDraw;
        private OxyPlot.WindowsForms.PlotView plot;
    }
}