using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFTanalyse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        void FFT(Complex[] dat)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Complex a = new Complex(1, 3);
            Complex b = new Complex(2, 2);
            Complex c = a * b;
           // MessageBox.Show(b.Length.ToString());
            MessageBox.Show(c.ToString(0)+" "+c.Angle.ToString ());

        }
    }
}
