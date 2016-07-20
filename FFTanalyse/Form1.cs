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
using System.IO;
using Oraycn.MPlayer;
using Oraycn.MCapture;
namespace FFTanalyse
{
    public partial class Form1 : Form
    {
        //音频接口
        private IAudioPlayer audioPlayer;
        private IMicrophoneCapturer microphoneCapturer;
        public Form1()
        {
            InitializeComponent();
            for(int i=0;i<voicefft.Length;i++)
            {
                voicefft[i] = new Complex();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        ArrayList datarraylist = new ArrayList();
        /// <summary>
        /// 快速傅里叶变换
        /// </summary>
        /// <param name="dat"></param>
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
        /// <summary>
        /// 傅里叶反变换
        /// </summary>
        /// <param name="dat"></param>
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
        /// <summary>
        /// 数据倒序
        /// </summary>
        /// <param name="dat">数据数组引用</param>
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
        //绘图窗口数组
        private List<drawWindow> drawWindowsList = new List<drawWindow>();
        /// <summary>
        /// Button响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            List<string> readDataList = ReadData();
            int N = int.Parse(FFTcount.Text) ;
            Complex[] dat = new Complex[N];
            for (int i = 0; i < readDataList.Count; i++)
                dat[i] = new Complex(double.Parse(readDataList[i]), 0);
            for(int i= readDataList.Count;i<dat.Length;i++)
            {
                dat[i] = new Complex();
            }
            for (int i = 0; i < dat.Length; i++)
            {
                PrintOutbox(dat[i].ToString(0));
            }
            drawWindowsList.Add(new drawWindow(generateplotmodel(dat,"原始序列")));

            FFT(ref dat);
            PrintOutbox("FFT:");
            for (int i = 0; i < dat.Length; i++)
            {
                PrintOutbox(dat[i].ToString(0));
            }
            drawWindowsList.Add(new drawWindow(generateplotmodel(dat, "FFT序列")));

            HighPassFilter(ref dat);//滤波
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
        /// <summary>
        /// 从输出框输出信息
        /// </summary>
        /// <param name="outstring">string</param>
        void PrintOutbox(string outstring)
        {
            this.OutBox.AppendText(outstring + "\n");
        }
        /// <summary>
        /// 生成绘图模型
        /// </summary>
        /// <param name="dat">数据</param>
        /// <param name="Tittle">数据标题</param>
        /// <returns></returns>
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
            ScatterSeries s2 = new ScatterSeries { MarkerType = MarkerType.Circle , Title = "Example 1" };
            
            for (int i = 0; i < dat.Length; i++)
            {
                s2.Points.Add(new ScatterPoint(i, dat[i].Length,10,500));
            }
            LineSeries lineSeries = new LineSeries { Title = "Example 1",ItemsSource= datlist };

            plotmodelTemp.Series.Add(s1); 
           // plotmodelTemp.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
            return plotmodelTemp;
        }
        List<string> ReadData()
        {
            string dataFileName="";
            List<string> dataList = new List<string>();
            OpenFileDialog openfile = new OpenFileDialog();
            if(openfile.ShowDialog ()==DialogResult.OK)
            {
                dataFileName = openfile.FileName;
                try
                {
                    FileStream file = new FileStream(@dataFileName, FileMode.Open);
                    StreamReader reader = new StreamReader(file);
                    string temp = reader.ReadLine(); ;
                    while(temp!=null)
                    {
                        dataList.Add(temp);
                        temp = reader.ReadLine();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("未选择文件");
            }
             return dataList;
        }
        void HighPassFilter(ref Complex[] dat)
        {
            for(int i=63;i<65;i++)
            {
                dat[i] = new Complex(7,0);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for(int i=0;i<drawWindowsList.Count;i++)
            {
                drawWindowsList[i].Close();
            }
            drawWindowsList.Clear();
            OutBox.Clear();
        }
        List<DataPoint> datlist = new List<DataPoint>();
        drawWindow audiowindow;
        PlotModel plotmodelTemp;
        LineSeries lineSeries;
        private void button3_Click(object sender, EventArgs e)
        {
            if(audiolabel.Text == "AudioOff")
            {
                try
                {
                    this.microphoneCapturer = CapturerFactory.CreateMicrophoneCapturer(0);
                    int sm = this.microphoneCapturer.SampleRate;
                    this.microphoneCapturer.AudioCaptured += new ESBasic.CbGeneric<byte[]>(microphoneCapturer_AudioCaptured);
                    this.audioPlayer = PlayerFactory.CreateAudioPlayer(0, 16000, 1, 16, 2);
                    this.microphoneCapturer.Start();
                    audiolabel.Text = "AudioOn";
                    audioButton.Text = "Stop";
                    //this.label_msg.Text = "正在采集麦克风，并播放 . . .";
                    //this.label_msg.Visible = true;
                    //this.button_wav.Enabled = false;
                    //this.button_mic.Enabled = false;
                    //this.button_stop.Enabled = true;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

                plotmodelTemp = new PlotModel { Title = "声音" };
                lineSeries = new LineSeries { Title = "Example 1", ItemsSource = datlist };
                StemSeries s1 = new StemSeries { Title = "Example 1", ItemsSource = datlist };
                plotmodelTemp.Series.Add(s1);
                audiowindow= new drawWindow(plotmodelTemp);
                audiowindow.Show();
            }
            else
            {
                if (this.audioPlayer == null)
                {
                    return;
                }

                if (this.microphoneCapturer != null)
                {
                    this.microphoneCapturer.Stop();
                    this.microphoneCapturer.Dispose();
                    this.microphoneCapturer = null;
                }

                this.audioPlayer.Clear();
                this.audioPlayer.Dispose();
                this.audioPlayer = null;
                audiolabel.Text = "AudioOff";
                audioButton.Text = "Audio";
                //this.label_msg.Visible = false;
                //this.button_wav.Enabled = true;
                //this.button_mic.Enabled = true;
                //this.button_stop.Enabled = false;
            }
            
        }
        Complex[] voicefft = new Complex[1024];
        void microphoneCapturer_AudioCaptured(byte[] audioData)
        {
            
            for (int i=0;i<audioData.Length;i++)
            {
                voicefft[i].Re = (float)audioData[i];
                voicefft[i].Im = 0;
            }
            for(int i= audioData.Length;i<1024;i++)
            {
                voicefft[i].Re = 0;
                voicefft[i].Im = 0;
            }
            FFT(ref voicefft);
            if (this.audioPlayer != null)
            {
                //this.audioPlayer.Play(audioData);
                datlist.Clear();
               /* for(int i=0;i<audioData.Length;i++)
                {
                    datlist.Add(new DataPoint(i, audioData[i]));
                }
                plotmodelTemp.InvalidatePlot(true);*/
                for(int i=0;i<voicefft.Length;i++)
                {
                    datlist.Add(new DataPoint(i, voicefft[i].Length));
                }
                plotmodelTemp.InvalidatePlot(true);
            }
        }
    }
}
