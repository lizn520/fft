using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eto;
using Eto.OxyPlot;
using OxyPlot.WindowsForms;
using OxyPlot.Series;
using OxyPlot;
namespace FFTanalyse
{
    public partial class drawWindow : Form
    {
        private PlotView plot1;
        private PlotModel plotmodel;
        public drawWindow(PlotModel plotmodel)
        {
            InitializeComponent();
            plot1 = new PlotView();
            this.plotmodel = plotmodel;
            this.Text = plotmodel.Title;
            this.SuspendLayout();
            // 
            // plot1
            // 
            this.plot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plot1.Location = new System.Drawing.Point(0, 0);
            this.plot1.Name = "plot1";
            this.plot1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plot1.Size = this.Size;
            this.plot1.TabIndex = 0;
            this.plot1.Text = "plot1";
            this.plot1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plot1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plot1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;

            this.Controls.Add(this.plot1 );
            this.ResumeLayout(false);
        }

        private void draw_Paint(object sender, PaintEventArgs e)
        {
            // OxyPlot.PlotModel myModel = new OxyPlot.PlotModel { Title = "Example 1" };
            //myModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            this.plot1.Model = plotmodel;
        }

        private void drawWindow_Load(object sender, EventArgs e)
        {
            
           
        }
    }
}
