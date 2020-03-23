using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialAndIntegralEquationsSolving.Equations
{
    public class Collocation
    {
        public double[,] A { get; set; }
        public double X0 { get; set; } = 0;
        public double X1 { get; set; } = Math.PI / 2.0;
        public int N { get; set; } = 100;
        public double H { get; set; }
        public double[] XArray { get; set; }
        public double[] C { get; set; }
        public double[] RightF { get; set; }

        public Collocation()
        {
            A = new double[N, N];
            XArray = new double[N];
            H = (X1 - X0) / N;
            for (int i = 0; i < N; i++)
            {
                XArray[i] = X0 + X1 * i;
            }
            C = new double[N];
            RightF = new double[N];
            for (int i = 0; i < XArray.Length; i++)
            {
                RightF[i] = F(XArray[i]);
            }
        }

        public void Solve()
        {
            FillMatrix();
            FindC();
            FindY();
        }

        public void FindC()
        {
            GaussSeidel(A, RightF, C);
        }

        public double F(double x)
        {
            var retVal = 2 - Math.PI + 2 * x;
            return retVal;
        }
        public double K(double x, double t)
        {
            return Math.Sin(t) * (t - x);
        }

        public double Phi(double x, int n)
        {
            //double P;
            //if (n == 0)
            //    P = 1;
            //else if (n == 1)
            //    P = x;
            //else
            //    P = ((2 * n - 1) / n) * x * this.Phi(x, n - 1) - ((n - 1) / n) * this.Phi(x, n - 2);
            //return P;

            var retVal = 0.0;

            if (n == 0)
                retVal = 1;
            else if (n == 1)
                retVal = x;
            else
                retVal = retVal = Math.Pow(x, n - 1);
            return retVal;
        }

        public double Integrate(double xi, int j)
        {
            double[] x = this.XArray;
            var sum = 0.0;
            for (int i = 1; i < x.Length; i++)
            {
                var F_xi_sj = K(xi, (x[i] + x[i - 1]) / 2) * Phi((x[i] + x[i - 1]) / 2, j);
                sum += (x[i] - x[i - 1]) * F_xi_sj;
            }
            return sum;
        }

        public void FillMatrix()
        {
            double[] x = this.XArray;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    var integral = Integrate(x[i], j);
                    A[i, j] = Phi(x[i], j) - integral;
                }
            }
        }

        public double FindY()
        {
            var y = 0.0;
            double[] x = this.XArray;
            for (int j = 0; j < N; j++)
            {
                for (int i = 0; i < N; i++)
                {
                    y += C[j] * Phi(x[i], j);
                }
            }
            return y;
        }

        #region SLAE solve method
        public void GaussSeidel(double[,] A, double[] B, double[] X)
        {
            double[] prev = new double[N];
            do
            {
                prev = X;
                for (int i = 0; i < X.Length; i++)
                {
                    double var = 0;
                    for (int j = 0; j < i; j++)
                        var += (A[i,j] * X[j]);
                    for (int j = i + 1; j < X.Length; j++)
                        var += (A[i,j] * prev[j]);
                    X[i] = (B[i] - var) / A[i,i];
                }
            }
            while (!Converge(X, prev));
        }

        private bool Converge(double[] xk, double[] xkp)
        {
            double eps = 0.000001;
            double norm = 0;
            for (int i = 0; i < xk.Length; i++)
                norm += (xk[i] - xkp[i]) * (xk[i] - xkp[i]);
            return (Math.Sqrt(norm) < eps);
        }
        #endregion

    }
}
