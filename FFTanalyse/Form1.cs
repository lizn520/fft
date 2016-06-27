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
        ArrayList datarraylist = new ArrayList();
        
        void FFT(ref Complex [] dat)
        {
            int M = 3;
            int N = (int)Math.Pow(2, 3);
            DataInvert(ref dat);
            for (int L = 1; L <= M; L++)
            {
                int B = (int)Math.Pow(2, L - 1);
                for (int J = 0; J < B; J++) 
                {
                    int P = (int)Math.Pow(2, M - L)*J;
                    int S = (int)Math.Pow(2, L);
                    for(int k=J;k<=N-1;k+=S)
                    {
                        Complex WnP = new Complex(2 * Math.PI * P / N);
                        Complex T = dat[k] + (dat[k + B] * WnP);
                        dat[k + B] = dat[k] - (dat[k + B] * WnP);
                        dat[k] = T;
                    }
                }
            }
        }
        void DataInvert(ref Complex [] dat)
        {
            //data count "M" must be 2^N
            int N = dat.Length ;
            int LH = N / 2;
            int K;
            int J = LH;
            int N1 = N - 2;
            for (int I = 1; I < N1; I++) 
            {
                if(I<J)
                {
                    Complex  Temp = dat[I];
                    dat[I] = dat[J];
                    dat[J] = Temp;
                }
                K = LH;
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
            

            Complex[] dat = new Complex[8];
            for (int i = 0; i < 8; i++)
                dat[i]=(new Complex(1, i));

            DataInvert(ref dat);
            for (int i = 0; i < dat.Length; i++)
            {
                //Complex temp = (Complex)dat[i];
                PrintOutbox(dat[i].ToString(0));
            }
            FFT(ref dat);
            PrintOutbox("FFT:");
            for (int i = 0; i < dat.Length; i++)
            {
                //Complex temp = (Complex)dat[i];
                PrintOutbox(dat[i].ToString(0));
            }

        }
        void PrintOutbox(string outstring)
        {
            this.OutBox.AppendText(outstring + "\n");
        }
    }
}
