using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FFTanalyse
{
    /// <summary>
    /// 复数类 re+im*i
    /// </summary>
    class Complex
    {
        /// <summary>
        ///私有变量
        /// re 实部 
        /// im 虚部
        /// </summary>
        private  double re, im;
        /// <summary>
        /// 属性 实部 Re
        /// </summary>
        public double Re
        {
            get
            {
                return re;
            }
            set
            {
                re = value;
            }
        }
        /// <summary>
        /// 属性 虚部 Im
        /// </summary>
        public double Im
        {
            get
            {
                return im;
            }
            set
            {
                im = value;
            }
        }
        /// <summary>
        /// 弧度
        /// </summary>
        public double Theta
        {
            get { return Math.Atan2(im, re); }
            set
            {
                re = Math.Cos(value);
                im = Math.Sin(value);
            }
        }
        /// <summary>
        /// 角度
        /// </summary>
        public double Angle
        {
            get { return Math.Atan2(im, re) / Math.PI * 180; }
        }
        /// <summary>
        /// 模长
        /// </summary>
        public double Length
        {
            get
            {
                return Math.Sqrt(re * re + im * im);
            }
        }
        /// <summary>
        /// 用实部虚部生成复数
        /// </summary>
        /// <param name="re">实部</param>
        /// <param name="im">虚部</param>
         public Complex(double re,double im)
        {
            this.re = re;
            this.im = im;
        }
        /// <summary>
        /// 参数：无
        /// re = 0
        /// im = 0
        /// </summary>
        public Complex()
        {
            this.re = 0;
            this.im = 0;
        }
        /// <summary>
        /// 参数：弧度
        /// 模长：默认为 1
        /// </summary>
        /// <param name="theta">弧度</param>
        public Complex(double theta)
        {
            re = Math.Cos(theta);
            im = -Math.Sin(theta);
        }
        /// <summary>
        /// 返回共轭复数
        /// </summary>
        /// <returns>Complex Conjugate</returns>
        public Complex GetConjugate()
        {
            Complex Conjugate = new Complex(this.re, -this.im);
            return Conjugate;
        }

        /// <summary>
        /// 转换成字符串 
        /// 参数          "0"        输出 a+b*i
        /// 参数         "1"    输出 Length*e^(-j*theta)
        /// </summary>
        /// <returns></returns>
        public string ToString(int arg)
        {
            string s;
            switch (arg)
            {
                case 0:s = string.Format("{0}+{1}*i", re, im);break;
                case 1:s = string.Format("{0}*e^(-j*{1})", Length, Theta); break;
                default:s = "Non-argument";break;
            }
            return s;
        }
        /// <summary>
        /// 复数加法运算
        /// </summary>
        /// <param name="lhs">左操作数</param>
        /// <param name="rhs">右操作数</param>
        /// <returns></returns>
        public static Complex operator +(Complex lhs, Complex rhs)
        {
            Complex result = new Complex();
            /*
             (a+b*i)+(c+d*i)=(a+c)+(b+d)*i
            */
            result.re = lhs.re + rhs.re;
            result.im = lhs.im + rhs.im;
            return result;
        }
        /// <summary>
        /// 复数减法
        /// </summary>
        /// <param name="lhs">左操作数</param>
        /// <param name="rhs">右操作数</param>
        /// <returns></returns>
        public static Complex operator -(Complex lhs, Complex rhs)
        {
            Complex result = new Complex();
            /*
             (a+b*i)-(c+d*i)=(a-c)+(b-d)*i
            */
            result.re = lhs.re - rhs.re;
            result.im = lhs.im - rhs.im;
            return result;
        }
        /// <summary>
        /// 复数乘法
        /// </summary>
        /// <param name="lhs">左操作数</param>
        /// <param name="rhs">右操作数</param>
        /// <returns></returns>
        public static Complex operator *(Complex lhs, Complex rhs)
        {
            Complex result = new Complex();
            /*
            (a+b*i)*(c+d*i)=(a*c-b*d)+(a*d+b*c)*i 
            */
            result.re = lhs.re * rhs.re - lhs.im * rhs.im;
            result.im = lhs.re * rhs.im + lhs.im * rhs.re;
            return result;
        }
        /// <summary>
        /// 复数除法
        /// </summary>
        /// <param name="lhs">左操作数</param>
        /// <param name="rhs">右操作数</param>
        /// <returns></returns>
        public static Complex operator /(Complex lhs, Complex rhs)
        {
            Complex result = new Complex();
            /*
            (a+b*i)/(c+d*i)=(a+b*i)*(c-d*i) /(c*c+d*d)
            */
            double a2b2 = rhs.re * rhs.re + rhs.im * rhs.im;
            result = lhs *rhs.GetConjugate();
            result.re /= a2b2;
            result.im /= a2b2;
            return result;
        }
        public static Complex operator /(Complex lhs, double rhs)
        {
            Complex result = new Complex();
            /*
            (a+b*i)/N=(a/N)+(b/N)*i
            */
            result.re /= lhs.re / rhs;
            result.im /= lhs.im / rhs;
            return result;
        }
    }
    
}
