using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkUI.Forms;

namespace VitchMat_Metodi_4
{
    public partial class Form1 : DarkForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void darkTextBox5_KeyPress(object sender, KeyPressEventArgs e) //запрет на ввод
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 45 && number != 44) 
            {
                e.Handled = true;
            }
        }
        int NatchZnachLeft = -100;
        private void darkButton1_Click(object sender, EventArgs e) //кнопка запуска
        {
            if (chek() == false)
            {
                MessageBox.Show("Ошибка!");
            }
            else
            {
                NatchZnachLeft = -100;
                if (otbor(NatchZnachLeft) == false)
                {
                    MessageBox.Show("Корней нет!");
                    NatchZnachLeft = -100;
                }
                else if (rb_pol.Checked == true)
                {
                    metod_pol();
                }
                else if (rb_hord.Checked == true)
                {
                    metod_hord();
                }
                else if (rb_kas.Checked == true)
                {
                    metod_kas();
                }
                else if (rb_komb.Checked == true)
                {
                    metod_komb();
                }
            }
            darkButton2.Visible = true;
        }
        void metod_pol() // метод пол дел
        {
            double E = Convert.ToDouble("0," + t_e.Text);
            int n = 0;
            double a, b, c;
            a = Convert.ToDouble(t_ab1.Text);
            b = Convert.ToDouble(t_ab2.Text);
            while ((Math.Abs(b - a)) / 2 > E)
            {
                c = (a + b) / 2;
                if (yr(c) * yr(a) < 0)
                {
                    b = c;
                }
                else if (yr(c) * yr(b) < 0)
                {
                    a = c;
                }
                n++;
            }
            t_x.Text = Convert.ToString((a + b) / 2);
            t_n.Text = Convert.ToString(n);
        }
        void metod_hord() // метод хорд
        {
            double E = Convert.ToDouble("0," + t_e.Text);
            int n = 1;
            double a, b, xn;
            a = Convert.ToDouble(t_ab1.Text);
            b = Convert.ToDouble(t_ab2.Text);
            xn = b - yr(b) * (b - a) / (yr(b) - yr(a));
            while (Math.Abs(xn - b) > E)
            {
                b = xn;
                xn = b - yr(b) * (b - a) / (yr(b) - yr(a));
                n++;
            }
            t_x.Text = Convert.ToString(xn);
            t_n.Text = Convert.ToString(n);
        }
        void metod_kas() //метод кас
        {
            double E = Convert.ToDouble("0," + t_e.Text);
            int n = 1;
            double a, xn;
            a = Convert.ToDouble(t_ab1.Text);
            xn = a - (yr(a) / yr_proizvod(a));
            while (Math.Abs(xn - a) > E)
            {
                a = xn;
                xn = a - (yr(a)/yr_proizvod(a));
                n++;
            }
            t_x.Text = Convert.ToString(xn);
            t_n.Text = Convert.ToString(n);
        }
        void metod_komb() //метод комб
        {
            double E = Convert.ToDouble("0," + t_e.Text);
            int n = 1;
            double a, b, an, bn;
            a = Convert.ToDouble(t_ab1.Text);
            b = Convert.ToDouble(t_ab2.Text);
            an = a - (yr(a) / yr_proizvod(a));
            bn = b - yr(b) * (b - a) / (yr(b) - yr(a));
            while ((Math.Abs(bn - an)) / 2 > E)
            {
                a = an;
                b = bn;
                an = a - (yr(a) / yr_proizvod(a));
                bn = b - yr(b) * (b - a) / (yr(b) - yr(a));
                n++;
            }
            t_x.Text = Convert.ToString((bn + an) / 2);
            t_n.Text = Convert.ToString(n);
        }
        double yr(double x) //уравнение 
        {
            return Convert.ToDouble(t_d1.Text) * (x * x * x) + Convert.ToDouble(t_d2.Text) * (x * x) + Convert.ToDouble(t_d3.Text) * x + Convert.ToDouble(t_d4.Text);
        }
        double yr_proizvod(double x) // производная
        {
            return 3 * Convert.ToDouble(t_d1.Text) * (x * x) + 2 * Convert.ToDouble(t_d2.Text) * x + Convert.ToDouble(t_d3.Text);
        }
        bool otbor(int j) //нахождение интервала
        {
            int h = 1;
            bool error = true;
            for (int i = j; i <= 100; i += h)
            {
                if (yr(i) == 0)
                {
                    error = false;
                    break;
                }
                if (yr(i) * yr(i + h) < 0)
                {
                    t_ab1.Text = Convert.ToString(i);
                    t_ab2.Text = Convert.ToString(i + h);
                    NatchZnachLeft = i + h;
                    error = false;
                    break;
                }
            }
            if (error == true)
            {
                return false;
            }
            return true;
        }
        bool chek() // проверка на правильный ввод
        {
            double d;
            if (Double.TryParse(t_e.Text, out d) == false)
            {
                return false;
            }
            else if (Double.TryParse(t_d1.Text, out d) == false || Double.TryParse(t_d2.Text, out d) == false || Double.TryParse(t_d3.Text, out d) == false || Double.TryParse(t_d4.Text, out d) == false )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void darkButton2_Click(object sender, EventArgs e) // кнопка след корня
        {
            if (otbor(NatchZnachLeft) == false)
            {
                MessageBox.Show("Корней больше нет!");
                darkButton2.Visible = false;
                NatchZnachLeft = -100;
            }
            else if (rb_pol.Checked == true)
            {
                metod_pol();
            }
            else if (rb_hord.Checked == true)
            {
                metod_hord();
            }
            else if (rb_kas.Checked == true)
            {
                metod_kas();
            }
            else if (rb_komb.Checked == true)
            {
                metod_komb();
            }
        }

        private void rb_pol_CheckedChanged(object sender, EventArgs e) 
        {
            darkButton2.Visible = false;
            NatchZnachLeft = -100;
        }
    }
}
