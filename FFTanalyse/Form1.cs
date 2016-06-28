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
using OxyPlot;
using OxyPlot.Series;
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
            int M = (int)(Math.Log((float)dat.Length) / Math.Log(2f));
            int N = (int)Math.Pow(2, M);
            DataInvert(ref dat);
            for (int L = 1; L <= M; L++)
            {
                int B = (int)Math.Pow(2, L - 1);
                for (int J = 0; J <= B - 1; J++) 
                {
                    int P = (int)Math.Pow(2, M - L) * J;
                    int S = (int)Math.Pow(2, L);
                    for(int k=J;k<=N-1;k+=S)
                    {
                        Complex WnP = new Complex(2 * Math.PI * P / N);
                        Complex CB = dat[k + B] * WnP;
                        Complex T = dat[k] + CB;
                        dat[k + B] = dat[k] - CB;
                        dat[k] = T;
                    }
                }
            }
        }
        void IFFT(ref Complex[] dat)
        {
            for(int i=0;i<dat.Length;i++)
            {
                dat[i] = dat[i].GetConjugate();
            }
            FFT(ref dat);
            for (int i = 0; i < dat.Length; i++)
            {
                dat[i] = dat[i].GetConjugate() ;
                dat[i].Re /= dat.Length;
                dat[i].Im /= dat.Length;
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
            for (int I = 1; I <= N1; I++) 
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
        private List<drawWindow> drawWindowsList = new List<drawWindow>();
        private void button1_Click(object sender, EventArgs e)
        {
            int N = 64;
            Complex[] dat = new Complex[N];
            for (int i = 0; i < dat.Length; i++)
                dat[i] = (new Complex(i, 0));

            for (int i = 0; i < dat.Length; i++)
            {
                PrintOutbox(dat[i].ToString(0));
            }

            drawWindowsList.Add(new drawWindow(generateplotmodel(dat,"原始序列")));
            //DataInvert(ref dat);
            //PrintOutbox("倒序:");
            //for (int i = 0; i < dat.Length; i++)
            //{
            //    PrintOutbox(dat[i].ToString(0));
            //}
            FFT(ref dat);
            PrintOutbox("FFT:");
            for (int i = 0; i < dat.Length; i++)
            {
                PrintOutbox(dat[i].ToString(0));
            }
            drawWindowsList.Add(new drawWindow(generateplotmodel(dat, "FFT序列")));
            PrintOutbox("IFFT:");
            IFFT(ref dat);
            for (int i = 0; i < dat.Length; i++)
            {
                PrintOutbox(dat[i].ToString(0));
            }
            drawWindowsList.Add(new drawWindow(generateplotmodel(dat, "反变换序列")));


           for(int i=0;i<drawWindowsList.Count;i++)
            {
                drawWindowsList[i].Show();
            }

        }
        void PrintOutbox(string outstring)
        {
            this.OutBox.AppendText(outstring + "\n");
        }

        PlotModel generateplotmodel(Complex[] dat,string Tittle)
        {
            PlotModel plotmodelTemp = new PlotModel { Title = Tittle };
            List<DataPoint> datlist = new List<DataPoint>();
            for (int i = 0; i < dat.Length; i++)
            {
                datlist.Add(new DataPoint(i, dat[i].Length));
            }
            StemSeries s1 = new StemSeries
            {
                Title = "Example 1",
                ItemsSource = datlist
            };
            plotmodelTemp.Series.Add(s1);
            return plotmodelTemp;
        }
    }
}
