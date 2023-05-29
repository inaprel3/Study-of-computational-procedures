using System;
using System.Windows.Forms;

namespace FuzzyLog7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            calc();
        }

        private void calc()
        {
            if(float.TryParse(textBox1.Text, out float a1)&&
                float.TryParse(textBox2.Text, out float a2)&&
                float.TryParse(textBox3.Text, out float a3)&&               
                float.TryParse(textBox8.Text, out float a4) &&                
                float.TryParse(textBox7.Text, out float steps))
            {
                //float a1 = 2, a2 = 4, a3 = 5;
                //float b1 = 4, b2 = 6, b3 = 10;
                //float steps = 70;
                if (radioButton1.Checked)
                {
                    func2(a1, a2, a3, a4, steps);
                }
                else if (radioButton2.Checked)
                {
                    func5(a1, a2, a3, steps);
                }
                else if(radioButton3.Checked)
                {
                    func7(a1, a2, a3, a4, steps);
                }
                else if (radioButton4.Checked)
                {
                    func1(a1, a2, a3, steps);
                }
            }
        }

        //Регулярна трикутна функція
        private void func1(float a1, float a2, float a3, float steps)
        {
            float stepA = (a3 - a1) / steps;
           
            float lengthH = 0, lengthE = 0;

            //цикл обчислення відстані кроками
            for (float i = 0, x1 = a1; i <= steps; i++, x1 += stepA)
            {
                float y1 = 0, y2 = 0;
                if (x1 <= a1 || x1 >= a3)
                {
                    y1 = 0;
                }
                else if (x1 > a1 && x1 <= a2)
                {
                    y1 = (x1 - a1) / (a2 - a1);
                }
                else if (x1 >= a2 && x1 < a3)
                {
                    y1 = (a3 - x1) / (a3 - a2);
                }

                if (y1 < 0.5)
                {
                    y2 = 0;
                }
                else if (y1 >= 0.5)
                {
                    y2 = 1;
                }
               
                lengthH += (Math.Abs(y1 - y2));
                lengthE += (float)Math.Pow(y1 - y2, 2);

                chart1.Series[0].Points.AddXY(x1, y1);

            }

            //цикл візуалізації відстані
            float min = 1000000, max = 0;
            if (a1 < min)
                min = a1;
            if (a3 > max)
                max = a3;
           
            for (float i = min; i <= max; i += 0.01f)
            {
                float y1 = 0; //y2 = 0;
                if (i <= a1 || i >= a3)
                {
                    y1 = 0;
                }
                else if (i > a1 && i <= a2)
                {
                    y1 = (i - a1) / (a2 - a1);
                }
                else if (i >= a2 && i < a3)
                {
                    y1 = (a3 - i) / (a3 - a2);
                }

                if (y1 < 0.5)
                {
                    chart1.Series[2].Points.AddXY(i, 0);
                }
                else if(y1>=0.5)
                {
                    chart1.Series[2].Points.AddXY(i, 1);
                }
            }
            lengthH = 2 / steps * lengthH;
            lengthE = 2 / (float)Math.Sqrt(steps) * (float)Math.Sqrt(lengthE);
            label1.Text += lengthH.ToString();
            label2.Text += lengthE.ToString();
        }

        //трапецієподібна функція (подібна до трикутної)
        private void func2(float a1, float a2, float a3, float a4, float steps)
        {
            float stepA = (a4 - a1) / steps;
            float lengthH = 0, lengthE = 0;
            for (float i = 0, x1 = a1; i <= steps; i++, x1 += stepA)
            {
                float y1 = 0, y2 = 0;
                if (x1 <= a1 || x1 >= a4)
                {
                    y1 = 0;
                }
                else if (x1 > a1 && x1 <= a2)
                {
                    y1 = (x1 - a1) / (a2 - a1);
                }
                else if(x1 > a2 && x1 <= a3)
                {
                    y1 = 1;
                }
                else if (x1 > a3 && x1 < a4)
                {
                    y1 = (a4 - x1) / (a4 - a3);
                }

                if (y1 < 0.5)
                {
                    y2 = 0;
                }
                else if (y1 >= 0.5)
                {
                    y2 = 1;
                }
                
                lengthH += Math.Abs(y1 - y2);
                lengthE += (float)Math.Pow(y1 - y2, 2);
                chart1.Series[0].Points.AddXY(x1, y1);
            }

            float min = 1000000, max = 0;
            if (a1 < min)
                min = a1;
            if (a4 > max)
                max = a4;
            for (float i = min; i <= max; i += 0.01f)
            {
                float y1 = 0; //y2 = 0;
                if (i <= a1 || i >= a4)
                {
                    y1 = 0;
                }
                else if (i > a1 && i <= a2)
                {
                    y1 = (i - a1) / (a2 - a1);
                }
                else if (i > a2 && i <= a3)
                {
                    y1 = 1;
                }
                else if (i >= a3 && i < a4)
                {
                    y1 = (a4 - i) / (a4 - a3);
                }

                if (y1 < 0.5)
                {
                    chart1.Series[2].Points.AddXY(i, 0);
                }
                else if (y1 >= 0.5)
                {
                    chart1.Series[2].Points.AddXY(i, 1);
                }
            }
            lengthH = 2 / steps * lengthH;
            lengthE = 2 / (float)Math.Sqrt(steps) * (float)Math.Sqrt(lengthE);
            label1.Text += lengthH.ToString();
            label2.Text += lengthE.ToString();
        }

        //Загальна дзвоноподібна функція
        //a1 - ширина[0, +inf]; а2 - центр функц.; а3 - коефіцієнт згладжування;
        //демо-виклик: func5(4, 2, 3, 5, 6, 8, кроки);
        private void func5(float a1, float a2, float a3, float steps)
        { 
            float y2;
            float minvalA = a2, maxvalA = a2;
            for (float i = a2, j = a2; ;)
            {
                float y = (float)(1 / (1 + (Math.Pow(Math.Abs((i - a2) / a1), 2 * a3))));
                y2 = (float)(1 / (1 + (Math.Pow(Math.Abs((j - a2) / a1), 2 * a3))));
                if (y <= 0.01)
                    minvalA = i;
                else
                    i--;
                if (y2 <= 0.01)
                    maxvalA = j;
                else
                    j++;
                if (y <= 0.01 && y2 <= 0.01)
                    break;
            }
            float stepA = (maxvalA - minvalA) / steps;
            float lengthH = 0, lengthE = 0;
            for (float i = 0, x1 = minvalA; i <= steps; i++, x1 += stepA)
            {
                float y1 = (float)(1 / (1 + (Math.Pow(Math.Abs((x1 - a2) / a1), 2 * a3))));
                if (y1 < 0.5)
                {
                    y2 = 0;
                }
                else if (y1 >= 0.5)
                {
                    y2 = 1;
                }
                lengthH += Math.Abs(y1 - y2);
                lengthE += (float)Math.Pow(y1 - y2, 2);
                chart1.Series[0].Points.AddXY(x1, y1);
            }

            float min = 1000000, max = 0;
            if (minvalA < min)
                min = minvalA;
            if (maxvalA > max)
                max = maxvalA;
            for (float i = min; i <= max; i += 0.01f)
            {
                float y1 = (float)(1 / (1 + (Math.Pow(Math.Abs((i - a2) / a1), 2 * a3))));
                if (y1 < 0.5)
                {
                    chart1.Series[2].Points.AddXY(i, 0);
                }
                else if (y1 >= 0.5)
                {
                    chart1.Series[2].Points.AddXY(i, 1);
                }
            }
            lengthH = 2 / steps * lengthH;
            lengthE = 2 / (float)Math.Sqrt(steps) * (float)Math.Sqrt(lengthE);
            label1.Text += lengthH.ToString();
            label2.Text += lengthE.ToString();
        }

        //Множення сигмоїдальних функцій
        //a1 - коефіцієнт згладжування1[1; 10+-]; ; b1 - коефіцієнт згладжування2[-10+-; -1];
        //демонстраційний виклик: func7(5, 2, -5, 7, 3, 5, -7, 10, кроки);
        private void func7(float a1, float c1, float a2, float c2, float steps)
        {
            float minvalA = c1, maxvalA = c2, y2 = 0;
            for (float i = c1; ;)
            {
                float y = (1 / (1 + (float)Math.Exp(-a1 * (i - c1)))) * (1 / (1 + (float)Math.Exp(-a2 * (i - c2))));
                if (y <= 0.1)
                {
                    minvalA = i;
                    break;
                }
                else
                    i--;
            }
            for (float i = c2; ;)
            {
                float y = (1 / (1 + (float)Math.Exp(-a1 * (i - c1)))) * (1 / (1 + (float)Math.Exp(-a2 * (i - c2))));
                if (y <= 0.1)
                {
                    maxvalA = i;
                    break;
                }
                else
                    i++;
            }

            float stepA = (maxvalA - minvalA) / steps;
            float lengthH = 0, lengthE = 0;
            for (float i = 0, x1 = minvalA;  i <= steps; i++, x1 += stepA)
            {
                float y1 = (1 / (1 + (float)Math.Exp(-a1 * (i - c1)))) * (1 / (1 + (float)Math.Exp(-a2 * (i - c2))));
                if (y1 < 0.5)
                {
                    y2 = 0;
                }
                else if (y1 >= 0.5)
                {
                    y2 = 1;
                }
                lengthH += Math.Abs(y1 - y2);
                lengthE += (float)Math.Pow(y1 - y2, 2);
            }

            float min = 1000000, max = 0;
            if (minvalA < min)
                min = minvalA;
            if (maxvalA > max)
                max = maxvalA;
            for (float i = min; i <= max; i += 0.01f)
            {
                float y1 = (1 / (1 + (float)Math.Exp(-a1 * (i - c1)))) * (1 / (1 + (float)Math.Exp(-a2 * (i - c2))));
                chart1.Series[0].Points.AddXY(i, y1);
                if (y1 < 0.5)
                {
                    chart1.Series[2].Points.AddXY(i, 0);
                }
                else if (y1 >= 0.5)
                {
                    chart1.Series[2].Points.AddXY(i, 1);
                }
            }
            lengthH = 2 / steps * lengthH;
            lengthE = 2 / (float)Math.Sqrt(steps) * (float)Math.Sqrt(lengthE);
            label1.Text += lengthH.ToString();
            label2.Text += lengthE.ToString();
        }

        //Кнопка «Розрахувати».
        private void button1_Click(object sender, EventArgs e)
        {
            clear();
            calc();
        }

        private void clear()
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            
            chart1.Refresh();
            label1.Text = "Лінійний індекс: ";
            label2.Text = "Квадратичний індекс: ";
        }

        private void textBox8_TextChanged(object sender, EventArgs e) { }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                textBox8.Visible = true;
                textBox1.Text = "2";
                textBox2.Text = "4";
                textBox3.Text = "6";
                textBox8.Text = "8";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox8.Visible = false;
                textBox1.Text = "4";
                textBox2.Text = "2";
                textBox3.Text = "3";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                textBox8.Visible = true;
                textBox1.Text = "5";
                textBox2.Text = "2";
                textBox3.Text = "-5";
                textBox8.Text = "7";
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                textBox8.Visible = false;
                textBox1.Text = "1";
                textBox2.Text = "3";
                textBox3.Text = "5";
            }
        }
    }
}