using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
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
        ArrayList dat = new ArrayList();
        Complex[] FFT(Complex[] dat)
        {
            return null;
        }
        void DataInvert(ref ArrayList dat)
        {
            //data count "M" must be 2^N
            int N = dat.Count;
            int LH = N / 2;
           // int[] I = new int[N];
           // int[] J = new int[N];
            //for (int i = 0; i < N; i++) 
            //{
            //    I[i] = i;
            //    J[i] = i;
            //}

            int K;
            int J = LH;
            int N1 = N - 2;
            for (int I = 1; I < N1; I++) 
            {
                if(I<J)
                {
                    object Temp = dat[I];
                    dat[I] = dat[J];
                    dat[J] = Temp;
                }
                K = LH;
              //  J////= J[i - 1];
                while (J>=K)
                {
                    J -= K;
                    K /= 2;
                }
                J += K;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Complex a = new Complex(1, 3);
            Complex b = new Complex(2, 2); 
            Complex c = a * b;
            // MessageBox.Show(b.Length.ToString());
            // MessageBox.Show(c.ToString(0)+" "+c.Angle.ToString ());
            for(int i=0;i<8;i++)
            dat.Add(new Complex(1, i));
            DataInvert(ref dat);
            for (int i = 0; i < dat.Count; i++)
            {
                Complex temp = (Complex)dat[i];
                PrintOutbox(temp.ToString(0));
            }

        }
        void PrintOutbox(string outstring)
        {
            this.OutBox.AppendText(outstring + "\n");
        }
    }
}
