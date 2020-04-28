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

        public double[] WeightsE { get; set; }
        public Galerkin()
        {
            X0 = -1.0;
            X1 = 1.0;
            N = 10;
            C = new double[N, N];
            WeightsE = new double[N];
            SetupX();
            B = new double[N];
        }

        private void FillB()
        {
            for (int i = 0; i < X.Length; i++)
            {
                B[i] = FindB(i);
            }
        }

        protected override void SetupX()
        {
            X = new double[N];
            X[0] = -3.436159118837738;
            X[1] = -2.532731674232790;
            X[2] = -1.756683649299882;
            X[3] = -1.036610829789514;
            X[4] = -0.342901327223705;
            X[5] = 0.342901327223705;
            X[6] = 1.036610829789514;
            X[7] = 1.756683649299882;
            X[8] = 2.532731674232790;
            X[9] = 3.436159118837738;

            WeightsE[0] = 1.0254516913657;
            WeightsE[1] = 0.8206661264048;
            WeightsE[2] = 0.7414419319436;
            WeightsE[3] = 0.7032963231049;
            WeightsE[4] = 0.6870818539513;
            WeightsE[5] = 0.6870818539513;
            WeightsE[6] = 0.7032963231049;
            WeightsE[7] = 0.7414419319436;
            WeightsE[8] = 0.8206661264048;
            WeightsE[9] = 1.0254516913657;

            #region bllsht
            #endregion
        }

        public override void Solve()
        {
            FillMatrix();
            FillB();
            var A = new double[N];
            A = Gauss(C, B);
            Y = new double[N];
            for (int i = 0; i < N; i++)
            {
                var sum = 0.0;
                for (int j = 0; j < N; j++)
                {
                    sum += A[j] * Hermit(X[i], j);
                }
                Y[i] += sum;
            }
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
            for (int i = 0; i < X.Length; i++)
            {
                var wi = WeightsE[i];
                var f = K(xj, X[i]) * Y[i];
                sum += wi * f;
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
            var c_ij = FirstC(i,j) - SecondC(i,j);
            
            return c_ij;
        }

        public double Weight(int n, double xi)
        {
            //n = 10;
            var t = Math.Pow(2, n - 1) * Factorial(n) * Math.Sqrt(Math.PI);
            var b = Math.Pow(n,2) * Math.Pow(Hermit(xi, n - 1), 2);
            var wi = t / b;
            return wi;
        }

        public double FirstC(int i, int j)
        {
            var sum = 0.0;
            for (int k = 0; k < X.Length; k++)
            {
                var wi = WeightsE[k];
                var h1 = Hermit(X[k], i);
                var h2 = Hermit(X[k], j);
                var f = h1 * h2;
                sum += wi * f;
            }
            return sum;
        }

        public double SecondC(int i, int j)
        {
            var sumOutter = 0.0;
            for (int n = 0; n < X.Length; n++)
            {
                var h1 = Hermit(X[n], i);
                var wi = WeightsE[n];
                var sumInner = 0.0;
                for (int m = 0; m < X.Length; m++)
                {
                    var k = K(X[n], X[m]);
                    var h2 = Hermit(X[m], j);
                    var fInner = k * h2;
                    var wj = WeightsE[m];
                    sumInner += wj * fInner;
                }
                var fOutter = h1 * sumInner;
                sumOutter += wi * fOutter;
            }
            return sumOutter;
        }

        public double FindB(int i)
        {
            var sum = 0.0;
            for (int k = 0; k < X.Length; k++)
            {
                var wi = WeightsE[k];
                var f = F(X[k]);
                var h = Hermit(X[k], i);
                sum += wi * f * h;
            }
            return sum;
        }

        public double Hermit(double x, int n = 10)
        {
            var sum = 0.0;
            var add = n / 2 == 0 ? 0 : 1;
            for (int i = 0; i <= n / 2 + add; i++)
            {
                var factorials = Factorial(n) / (Factorial(i) * Factorial(n - 2 * i));
                sum += Math.Pow(-1, i) * Math.Pow(2 * x, n - 2 * i) * factorials;
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
