using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace WindowsFormsApplication1
{
    class Slau
    {
       
        public double DET(double[,] a, int n)
        {
            int i, j, k;
            double det = 0;
            for (i = 0; i < n - 1; i++)
            {
                for (j = i + 1; j < n; j++)
                {
                    det = a[j, i] / a[i, i];
                    for (k = i; k < n; k++)
                        a[j, k] = a[j, k] - det * a[i, k];
                }
            }
            det = 1;
            for (i = 0; i < n; i++)
                det = det * a[i, i];
            return det;
        }
        public string Show(double[,]a, int n, double[] f, int i)
        {
            string[] MatrixRows=new string[n];
           
                MatrixRows[i] = "";
                for (int j=0; j<n; j++)
                {
                    MatrixRows[i] = MatrixRows[i] + Convert.ToString(Math.Round(a[i, j],2) + " ");
                }
                MatrixRows[i] = MatrixRows[i] +" | "+ Convert.ToString(Math.Round(f[i],2) );
            
            return MatrixRows[i];
        }
        public string ShowComplex(Complex[,] a, int n, double[] f, int i)
        {
            string[] MatrixRows = new string[n];

            MatrixRows[i] = "";
            for (int j = 0; j < n; j++)
            {
                MatrixRows[i] = MatrixRows[i] + Convert.ToString(a[i,j] + " ");
            }
            MatrixRows[i] = MatrixRows[i] + " | " + Convert.ToString(Math.Round(f[i], 2));

            return MatrixRows[i];
        }
        public void TriangularView (double[,] a, double[] f, int diversity )
        {
            double[,] b = new double[diversity, diversity];
            double[] g = new double[diversity];
            for (int k = 0; k < diversity; k++)
            {

                for (int i = k; i < diversity; i++)
                {
                    double diagonalitem = a[k, k];
                    if (i == k)
                    {                       
                        f[k] /= diagonalitem;
                        for (int j = k; j < diversity; j++)
                        {
                            a[k, j] /= diagonalitem;
                        }
                    }

                    else
                    {
                        double mainitem = a[i, k];
                        g[k] = f[k]/ diagonalitem ;
                        f[i] -= mainitem * g[k];
                        for (int j = k; j < diversity; j++)
                        {
                            b[k, j] = a[k, j] / diagonalitem;
                            a[i, j] -= mainitem * b[k, j];
                        }
                    }
                }
            }
        }
        public void InverseProcess(double[,]a, double[] f, int diversity, double[]x)
        {
           
            for (int i = diversity - 1; i >= 0; i--)
            {
                x[i] = f[i];
                for (int j = i + 1; j < diversity; j++)
                {
                    x[i] -= a[i, j] * x[j];
                }
            }
        }
        public void GetValues(double[,]a, double[] f, int diversity, DataGridView dtgview)
        {
            for (int i = 0; i < diversity; i++)
            {
                for (int j = 0; j < diversity; j++)
                {
                    a[i, j] = Convert.ToDouble (dtgview.Rows[i].Cells[j].Value);
                }
                f[i] = Convert.ToDouble(dtgview.Rows[i].Cells[3].Value);
            }
        }
        public double[,] UnaryMatrix(int diversity)
        {
            double[,] elements = new double[diversity, diversity];

            for (int i = 0; i < diversity; i++)
                elements[i, i] = 1;

            return elements;
        }

        public double[,] MultiplyMatricies(double[,] matrix1, double[,] matrix2, int diversity)
        {
            double[,] result = new double[diversity, diversity];

            for (int i = 0; i < diversity; i++)
            {
                for (int j = 0; j < diversity; j++)
                {
                    result[i, j] = 0;
                    for (int p = 0; p < diversity; p++)
                        result[i, j] += matrix1[i, p] * matrix2[p, j];
                }
            }

            return result;
        }
        public double[] AddVectors(double[] vector1, double[] vector2, int diversity)
        {
            double[] result = new double[diversity];

            for (int i = 0; i < diversity; i++)
            {
                result[i] = vector1[i] + vector2[i];
            }

            return result;
        }
        public double[] SubstractVectors(double[] vector1, double[] vector2, int diversity)
        {
            double[] result = new double[diversity];

            for (int i = 0; i < diversity; i++)
            {
                result[i] = vector1[i] - vector2[i];
            }

            return result;
        }
        public double[] MultipleVectorByNumber(double[] vector1, double multiplier, int diversity)
        {
            double[] result = new double[diversity];

            for (int i = 0; i < diversity; i++)
            {
                result[i] = vector1[i] *multiplier;
            }

            return result;
        }
        public double[] MultiplyMatriceByVector(double[,] matrix1, int diversity, double[] vector)
        {
            double[] result = new double[diversity];

            for (int i = 0; i < diversity; i++)
            {
                result[i] = 0;
                for (int j = 0; j < diversity; j++)
                {                  
                   
                        result[i] += matrix1[i, j] * vector[j];
                }
            }

            return result;
        }
        public double[,] AddMatricies(double[,] matrix1, double[,] matrix2, int diversity)
        {
            double[,] matrix = new double[diversity, diversity];

            for (int i = 0; i < diversity; i++)
                for (int j = 0; j < diversity; j++)
                    matrix[i, j] = matrix1[i, j] + matrix2[i, j];

            return matrix;
        }

        public double[,] SubtractMatricies(double[,] matrix1, double[,] matrix2, int diversity)
        {
            double[,] matrix = new double[diversity, diversity];

            for (int i = 0; i < diversity; i++)
                for (int j = 0; j < diversity; j++)
                    matrix[i, j] = matrix1[i, j] - matrix2[i, j];

            return matrix;
        }
        public double[,] MultiplyNumberBytMatrice(double[,] matrix1, int diversity, double multiplier)
        {
            double[,] matrix = new double[diversity, diversity];

            for (int i = 0; i < diversity; i++)
                for (int j = 0; j < diversity; j++)
                    matrix[i, j] = multiplier* matrix1[i, j];

            return matrix;
        }
        public double[,] Build_H_SM(double[,] matrix1, int diversity)
        {
            double[,] matrixH = new double[diversity, diversity];

            for (int i = 0; i < diversity; i++)
            {
                for (int j = 0; j < diversity; j++)
                {
                    if (i > j)
                    {
                        matrixH[i, j] = matrix1[i, j];
                    }
                    else { matrixH[i, j] = 0; }
                }
            }      

            return matrixH;
        }
        public double[,] Build_F_SM(double[,] matrix1, int diversity)
        {
            double[,] matrixF = new double[diversity, diversity];

            for (int i = 0; i < diversity; i++)
            {
                for (int j = 0; j < diversity; j++)
                {
                    if (i <= j)
                    {
                        matrixF[i, j] = matrix1[i, j];
                    }
                    else { matrixF[i, j] = 0; }
                }
            }

            return matrixF;
        }

        public double FirstNorm(double[,] matrix,  int diversity)
        {
            double norm = -1;
                for (int i = 0; i < diversity; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < diversity; j++)
                        sum += Math.Abs(matrix[i, j]);

                    if (norm < sum)
                        norm = sum;
                }

                return norm;            
        }
        public double FirstNormVector(double[] vector, int diversity)
        {
            double norm = -1;
            for (int i = 0; i < diversity; i++)
            {           
                if (norm < Math.Abs(vector[i]))
                    norm = Math.Abs(vector[i]);
            }
            return norm;
        }

        public double[,] Build_B_SM(double[,] matrix, int diversity, double alfa)
        {
            double[,] newMatrixB = new double[diversity, diversity];
            double[,] MatrixE = UnaryMatrix(diversity);
            double[,] MatrixATA = this.MultiplyMatricies(this.Transposed(matrix, diversity), matrix, diversity);
            newMatrixB =this.SubtractMatricies( MatrixE,this.MultiplyNumberBytMatrice(MatrixATA, diversity, alfa), diversity);

            return newMatrixB;
        }
        public double alfa (double[,] matrix, int diversity)
        {
            Random r = new Random();
            double[,] MatrixATA = this.MultiplyMatricies(this.Transposed(matrix, diversity), matrix, diversity);
            double temp = FirstNorm(MatrixATA, diversity);
            double alfa = Math.Round(r.NextDouble() * (2 / temp), 3);
            return alfa;
        }
        public double[] Build_g_SM(double[,] matrix, int diversity, double[] vector, double alfa)
        {
            double[] g = new double[diversity];     
            
           g = this.MultipleVectorByNumber(this.MultiplyMatriceByVector(this.Transposed(matrix, diversity), diversity, vector),alfa,diversity);
            return g;
        }
        public double[,] Transposed(double[,] matrix, int diversity)
        {
            double[,] transposed = new double[diversity, diversity];

            for (int i = 0; i < diversity; i++)
                for (int j = 0; j < diversity; j++)
                    transposed[i, j] = matrix[j, i];

            return transposed;
        }
        public void Build_S_forSR(double[,] a, Complex [,]s, int diversity)
        {
            for (int i = 0; i < diversity; i++)
            {
                Complex temp = 0;
                for (int k = 0; k < i ; k++)
                {
                    temp += s[k, i] * s[k, i];
                }
                s[i, i] = (a[i, i] - temp);
                s[i, i] = Complex.Sqrt(s[i, i]);

                for (int j = i + 1; j < diversity; j++)
                {
                    Complex temp2 = 0;
                    for (int k = 0; k < i ; k++)
                    {
                        temp2 += s[k, i] * s[k, j];
                    }
                    s[i, j] = (a[i, j] - temp2) / s[i, i];
                }
            }

        }
        public double[,] GetInverseMatrix(double[,] matrix, int diversity)
        { double[,]M_inverse = new double[diversity, diversity];

         double[,] M_expand=   InitExpandMatrix(matrix, diversity);          

            for (int i = 0; i < diversity; i++)
            {
                for (int j = 0; j < diversity; j++)
                {
                    M_inverse[i, j] = M_expand[i, j + diversity];
                }
            }
            return M_inverse;
        }      
       
           private double[,] InitExpandMatrix(double[,] matrix, int diversity)
        {
       
          double[,]  M_expand = new double[diversity, 2 * diversity];
            
            for (int i = 0; i < diversity; i++)
            {
                for (int j = 0; j < 2 * diversity; j++)
                {
                    if (j < diversity)
                    {
                        M_expand[i, j] = matrix[i, j];
                    }
                    else
                    {
                        if (j - i == diversity)
                        {
                            M_expand[i, j] = 1;
                        }
                        else
                        {
                            M_expand[i, j] = 0;
                        }
                    }
                }
            }
            return M_expand;
        }

    }
}

           
