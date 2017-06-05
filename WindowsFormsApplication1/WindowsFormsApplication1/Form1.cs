using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSquareRoot_Click(object sender, EventArgs e)
        {
            int diversity = 0;
            diversity = dataGridView1.ColumnCount - 1;
            Slau slau = new Slau();
            double[,] a = new double[diversity, diversity];
            Complex[,] s = new Complex[diversity, diversity];
            double[] f = new double[diversity];
            Complex[] x = new Complex[diversity];
            Complex[] y = new Complex[diversity];

            slau.GetValues(a, f, diversity, dataGridView1);
            for (int i = 0; i < diversity; i++)
            {
                lblViewMatrixSR.Text += slau.Show(a, diversity, f, i) + "\n";

            }
            if (slau.DET(a, diversity) != 0)
            {
                slau.GetValues(a, f, diversity, dataGridView1);
                slau.Build_S_forSR(a, s, diversity);
                //for (int i = 0; i < diversity; i++)
                //{
                //   lbl_S_SR.Text += slau.ShowComplex(s, diversity, f, i) + "\n";
                //}
                for (int i = 0; i < diversity; i++)
                {
                    Complex temp = 0;
                    for (int k = 0; k < i; k++)
                    {
                        temp += s[k, i] * y[k];
                    }
                    y[i] = (f[i] - temp) / s[i, i];
                }
                for (int i = diversity - 1; i >= 0; i--)
                {
                    Complex temp = 0;
                    for (int k = i + 1; k < diversity; k++)
                    {
                        temp += s[i, k] * x[k];
                    }
                    x[i] = (y[i] - temp) / s[i, i];
                    lbl_S_SR.Text += "x" + i + " " + (Math.Round(x[i].Real, 2)) + "\n";
                }

            }
            else { MessageBox.Show("Given SLAU doesn't have single-valued solution"); }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int diversity = 0;

            lblTriangular.Text = "";
            lblAnswer.Text = "";
            label1.Text = "";

            diversity = dataGridView1.ColumnCount - 1;
            Slau slau = new Slau();
            double[,] a = new double[diversity, diversity];// = { { 2, -3, -5 }, { -3, 3, 3 }, { -5, 3, 8 } };
            double[] f = new double[diversity];//= { -3, -3, 3 };
            double[] x = new double[diversity];

            slau.GetValues(a, f, diversity, dataGridView1);
            for (int i = 0; i < diversity; i++)
            {
                label1.Text += slau.Show(a, diversity, f, i) + "\n";

            }
            if (slau.DET(a, diversity) != 0)
            {
                slau.GetValues(a, f, diversity, dataGridView1);
                slau.TriangularView(a, f, diversity);

                for (int i = 0; i < diversity; i++)
                {
                    lblTriangular.Text += slau.Show(a, diversity, f, i) + "\n";
                }
                slau.InverseProcess(a, f, diversity, x);
                for (int i = 0; i < diversity; i++)
                {
                    lblAnswer.Text += "x " + i + " = " + Convert.ToString(x[i]) + "\n";
                }
            }
            else { MessageBox.Show("Given SLAU doesn't have single-valued solution"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int diversity = 3;
            label6.Text = "";
            label9.Text = "";
            listBox1.Items.Clear();
            double eps = Convert.ToDouble(txbEps.Text);
            //  diversity = dataGridView1.ColumnCount - 1;
            Slau slau = new Slau();
            // double[,] a = new double[diversity, diversity];// = { { 2, -3, -5 }, { -3, 3, 3 }, { -5, 3, 8 } };
            // double[] f = new double[diversity];//= { -3, -3, 3 };
            double[] xPrev = new double[diversity];
            double[] xNew =  new double[diversity];
             double[,] a1 = { { 2, -3, -5 }, { -3, 3, 3 }, { -5, 3, 8 } };
            //double[,] a1 = { { -2, 1, 3 }, { -3, 2, 2 }, { 4, -2, 1 } };
             double[] f = { -3, -3, 3 };
            //double[] f = { 2, 1, 3 };
            if (slau.DET(a1, diversity) != 0)
            {
                 double[,] a = { { 2, -3, -5 }, { -3, 3, 3 }, { -5, 3, 8 } };
               // double[,] a = { { -2, 1, 3 }, { -3, 2, 2}, { 4, -2, 1} };
               //  double alfa = slau.alfa(a, diversity);
                double alfa = 0.001;
                label7.Text = Convert.ToString(alfa);
                double[,] b = slau.Build_B_SM(a, diversity, alfa);
                int count = 0;
                foreach (double i in b)
                {
                    if (count == diversity)
                    {
                        count = 0;
                        label6.Text += "\n";
                    }
                    label6.Text += Convert.ToString(i) + " ";
                    count++;
                }
                double[,] h = slau.Build_H_SM(b, diversity);
                double[,] F = slau.Build_F_SM(b, diversity);
                double[] g = slau.Build_g_SM(a, diversity, f, alfa);
                foreach(double i in g)
                {
                    label9.Text += Convert.ToString(i) + "\n";
                }
                
                for (int i = 0; i < diversity; i++)
                {
                    xPrev[i] = 0;
                }
                if (slau.FirstNorm(b, diversity) > 1)
                {
                    int countIter = 1;
                    double[,] E_H_inverse = slau.GetInverseMatrix(slau.SubtractMatricies(slau.UnaryMatrix(diversity), h, diversity), diversity);
                    xNew = slau.AddVectors(slau.MultiplyMatriceByVector(slau.MultiplyMatricies(E_H_inverse, F, diversity), diversity, xPrev), slau.MultiplyMatriceByVector(E_H_inverse, diversity, g), diversity);
                  double i_norm=  slau.FirstNormVector(slau.SubstractVectors(xNew, xPrev, diversity), diversity);
                    listBox1.Items.Add("--" + countIter + "---");
                    foreach (double i in xNew)
                    {
                        listBox1.Items.Add(i);
                    }
                    while (i_norm>eps)
                    {                     
                        xPrev = xNew;
                        xNew = slau.AddVectors(slau.MultiplyMatriceByVector(slau.MultiplyMatricies(E_H_inverse, F, diversity), diversity, xPrev), slau.MultiplyMatriceByVector(E_H_inverse, diversity, g), diversity);
                        i_norm = slau.FirstNormVector(slau.SubstractVectors(xNew, xPrev, diversity), diversity);
                        countIter++;
                                            
                    }
                    listBox1.Items.Add("--" + countIter + "---");
                    foreach (double i in xNew)
                    {
                        listBox1.Items.Add(i);
                    }
                }
                else
                {
                    // use criterion with constant
                    int countIter = 1;
                    double[,] E_H_inverse = slau.GetInverseMatrix(slau.SubtractMatricies(slau.UnaryMatrix(diversity), h, diversity), diversity);
                    xNew = slau.AddVectors(slau.MultiplyMatriceByVector(slau.MultiplyMatricies(E_H_inverse, F, diversity), diversity, xPrev), slau.MultiplyMatriceByVector(E_H_inverse, diversity, g), diversity);
                    double i_norm = slau.FirstNormVector(slau.SubstractVectors(xPrev, xNew, diversity), diversity);
                    while (i_norm > eps)
                    {
                        xPrev = xNew;
                        xNew = slau.AddVectors(slau.MultiplyMatriceByVector(slau.MultiplyMatricies(E_H_inverse, F, diversity), diversity, xPrev), slau.MultiplyMatriceByVector(E_H_inverse, diversity, g), diversity);
                        i_norm = slau.FirstNormVector(slau.SubstractVectors(xPrev, xNew, diversity), diversity);
                        countIter++;
                    }
                }
              
               

            }
            else { MessageBox.Show("Given SLAU have det = 0"); }
        }

    }
}
