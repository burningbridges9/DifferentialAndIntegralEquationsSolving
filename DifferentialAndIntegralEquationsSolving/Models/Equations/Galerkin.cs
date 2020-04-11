using DifferentialAndIntegralEquationsSolving.Equations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialAndIntegralEquationsSolving.Models.Equations
{
    public class Galerkin : Equation
    {
        public double[,] C { get; set; }
        public double[] B { get; set; }

        public double[] Diff { get; set; }

        public Galerkin()
        {
            X0 = -1.0;
            X1 = 1.0;
            N = 10;
            C = new double[N, N];
            SetupX();
            B = new double[N];
        }

        private void FillB()
        {
            for (int i = 0; i < X.Length; i++)
            {
                B[i] = F(X[i]);
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
            FillB();
            Y = Gauss(C, B);
        }

        private void FillMatrix()
        {
            for (int i = 0; i < X.Length; i++)
            {
                for (int j = 0; j < X.Length; j++)
                {
                    C[i, j] = FindC(i, j);
                }
            }
        }

        public double F(double x)
        {
            var retVal = Math.Pow(Math.E, -Math.Pow(x, 2)) * x - Math.Sqrt(Math.PI) * x / 2.0;
            return retVal;
        }
        public double K(double x, double t)
        {
            return x * (t - x);
        }

        public double IntegrateForCheck(double xj)
        {
            var sum = 0.0;
            for (int i = 1; i < X.Length; i++)
            {
                var f = K(xj, (X[i] + X[i - 1]) / 2) * Y[i];
                sum += (X[i] - X[i - 1]) * f;
            }
            return sum;
        }

        public void Tolerance()
        {
            Diff = new double[N];
            for (int j = 0; j < X.Length; j++)
            {
                Diff[j] = Math.Abs(Y[j] - IntegrateForCheck(X[j]) - B[j]);
            }
        }

        public double FindC(int i, int j)
        {
            var c_ij = FirstC(i,i) - SecondC(i,j);
            return c_ij;
        }

        public double FirstC(int i, int j)
        {
            var sum = 0.0;
            for (int k = 1; k < X.Length; k++)
            {
                var h1 = Hermit((X[k] + X[k - 1]) / 2, i);
                var h2 = Hermit((X[k] + X[k - 1]) / 2, j);
                var f = h1 * h2;
                sum += (X[k] - X[k - 1]) * f;
            }
            return sum;
        }

        public double SecondC(int i, int j)
        {
            var sumOutter = 0.0;
            for (int n = 1; n < X.Length; n++)
            {
                var h1 = Hermit((X[n] + X[n - 1]) / 2, i);
                var sumInner = 0.0;
                for (int m = 1; m < X.Length; m++)
                {
                    var k = K((X[n] + X[n - 1]) / 2, (X[m] + X[m - 1]) / 2);
                    var h2 = Hermit((X[m] + X[m - 1]) / 2, j);
                    var fInner = h1 * h2;
                    sumInner += (X[m] - X[m - 1]) * fInner;
                }
                var fOutter = h1 * sumInner;
                sumOutter += (X[n] - X[n - 1]) * fOutter;
            }
            return sumOutter;
        }

        public double FindB(int i)
        {
            var sum = 0.0;
            for (int k = 1; k < X.Length; k++)
            {
                var f = F((X[k] + X[k - 1]) / 2);
                var h = Hermit((X[k] + X[k - 1]) / 2, i);
                sum += (X[k] - X[k - 1]) * f * h;
            }
            return sum;
        }

        public double Hermit(double x, int n)
        {
            var sum = 0.0;
            for (int i = 0; i <= n / 2; i++)
            {
                var factorials = Factorial(n) / (Factorial(i) * Factorial(n - 2 * i));
                sum += Math.Pow(-1, i) * (2 * Math.Pow(x, n - 2 * i)) * factorials;
            }
            return sum;
        }

        public int Factorial(int x)
        {
            if (x == 0)
            {
                return 1;
            }
            if (x == 1)
            {
                return 1;
            }
            else
            {
                int res = 1;
                for (int i = 1; i <= x; i++)
                {
                    res *= i;
                }
                return res;
            }
        }

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
                max = Math.Abs(a[k, k]);
                index = k;
                for (int i = k + 1; i < N; i++)
                {
                    if (Math.Abs(a[i, k]) > max)
                    {
                        max = Math.Abs(a[i, k]);
                        index = i;
                    }
                }
                // Перестановка строк
                if (max < eps)
                {
                }
                for (int j = 0; j < N; j++)
                {
                    temp = a[k, j];
                    a[k, j] = a[index, j];
                    a[index, j] = temp;
                }
                temp = y[k];
                y[k] = y[index];
                y[index] = temp;
                // Нормализация уравнений
                for (int i = k; i < N; i++)
                {
                    temp = a[i, k];
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

    }
}
