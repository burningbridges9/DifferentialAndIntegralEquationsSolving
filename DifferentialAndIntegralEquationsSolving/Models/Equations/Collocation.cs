using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialAndIntegralEquationsSolving.Equations
{
    public class Collocation : Equation
    {
        public double[,] A { get; set; }

        public double[] C { get; set; }
        public double[] RightF { get; set; }

        public double[] Diff { get; set; }

        public Collocation()
        {
            X0 = 0;
            X1 = Math.PI / 2.0;
            N = 10;
            A = new double[N, N];
            SetupX();
            C = new double[N];
            RightF = new double[N];
            for (int i = 0; i < X.Length; i++)
            {
                RightF[i] = F(X[i]);
            }
        }

        protected override void SetupX()
        {
            X = new double[N];
            H = (X1 - X0) / N;
            for (int i = 0; i < N; i++)
            {
                X[i] = X0 + H * i;
            }
        }

        public override void Solve()
        {
            FillMatrix();
            FindC();
            Y = new double[N];
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = FindY(X[i]);
            }
        }

        public void FindC()
        {
            C = Gauss(A, RightF);
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
            return Math.Pow(x, n-1);
        }

        public double Integrate(double xi, int j)
        {
            var sum = 0.0;
            for (int i = 1; i < X.Length; i++)
            {
                var F_xi_sj = K(xi, (X[i] + X[i - 1]) / 2) * Phi((X[i] + X[i - 1]) / 2, j);
                sum += (X[i] - X[i - 1]) * F_xi_sj;
            }
            return sum;
        }

        public double IntegrateForCheck(double xj)
        {
            var sum = 0.0;
            for (int i = 1; i < X.Length; i++)
            {
                var F_ti_xj = K(xj, (X[i] + X[i - 1]) / 2) * Y[i];
                sum += (X[i] - X[i - 1]) * F_ti_xj;
            }
            return sum;
        }

        public void Tolerance()
        {
            Diff = new double[N];
            for (int j = 0; j < X.Length; j++)
            {
                Diff[j] = Math.Abs(Y[j] - IntegrateForCheck(X[j]) - 2 + Math.PI - 2 * X[j]);
            }
        }

        public void FillMatrix()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    var integral = Integrate(X[i], j);
                    A[i, j] = Phi(X[i], j) - integral;
                }
            }
        }

        public double FindY(double x)
        {
            var y = 0.0;
            for (int j = 0; j < N; j++)
            {
                y += C[j] * Phi(x, j);
            }
            return y;
        }

        #region SLAE solve method

        public double[] Gauss(double[,] a, double[] y)
        {
            double[] x = new double[N];
            double max, temp;
            int k, index;
            const double eps = 0.00001;  // точность
            k = 0;
            while (k < N)
            {
                // Поиск строки с максимальным a[i][k]
                max = Math.Abs(a[k,k]);
                index = k;
                for (int i = k + 1; i < N; i++)
                {
                    if (Math.Abs(a[i,k]) > max)
                    {
                        max = Math.Abs(a[i,k]);
                        index = i;
                    }
                }
                // Перестановка строк
                if (max < eps)
                {
                }
                for (int j = 0; j < N; j++)
                {
                    temp = a[k,j];
                    a[k,j] = a[index,j];
                    a[index,j] = temp;
                }
                temp = y[k];
                y[k] = y[index];
                y[index] = temp;
                // Нормализация уравнений
                for (int i = k; i < N; i++)
                {
                    temp = a[i,k];
                    if (Math.Abs(temp) < eps) continue; // для нулевого коэффициента пропустить
                    for (int j = 0; j < N; j++)
                        a[i, j] = a[i, j] / temp;
                    y[i] = y[i] / temp;
                    if (i == k) continue; // уравнение не вычитать само из себя
                    for (int j = 0; j < N; j++)
                        a[i, j] = a[i, j] - a[k, j];
                    y[i] = y[i] - y[k];
                }
                k++;
            }
            // обратная подстановка
            for (k = N - 1; k >= 0; k--)
            {
                x[k] = y[k];
                for (int i = 0; i < k; i++)
                    y[i] = y[i] - a[i, k] * x[k];
            }
            return x;
        }
        #endregion

    }
}
