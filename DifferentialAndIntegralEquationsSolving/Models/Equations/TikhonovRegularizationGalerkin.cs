using DifferentialAndIntegralEquationsSolving.Equations;
using System;

namespace DifferentialAndIntegralEquationsSolving.Models.Equations
{
    public class TikhonovRegularizationGalerkin : Equation
    {
        public double[,] C { get; set; }
        public double[] B { get; set; }

        public double[] Diff { get; set; }

        public double[] AlphaY { get; set; }

        public double XC { get; set; }
        public double XD { get; set; }
        public double[] XCD { get; set; }
        public TikhonovRegularizationGalerkin()
        {
            X0 = -1.0;
            X1 = 1.0;

            XC  = -1.0;
            XD = 1.0;

            N = 10;
            C = new double[N, N];
            SetupX();
            B = new double[N];
            AlphaY = new double[N];
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

            XCD = new double[N];
            var HCD = (XD - XC) / N;
            for (int i = 0; i < N; i++)
            {
                XCD[i] = XC + HCD * i;
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

        #region Functions

        public double F(double x)
        {
            var retVal = Math.Pow(Math.E, -Math.Pow(x, 2)) * x - Math.Sqrt(Math.PI) * x / 2.0;
            return retVal;
        }

        public double WaveF(double x)
        {
            var sum = 0.0;
            for (int m = 1; m < XCD.Length; m++)
            {
                var k = K((XCD[m] + XCD[m - 1]) / 2, x);
                var f = k * F((XCD[m] + XCD[m - 1]) / 2);
                sum += (XCD[m] - XCD[m - 1]) * f;
            }
            return sum;
        }

        public double K(double x, double t)
        {
            return x * (t - x);
        }

        public double WaveK(double x, double t)
        {
            var sum = 0.0;
            for (int m = 1; m < XCD.Length; m++)
            {
                var k1 = K((XCD[m] + XCD[m - 1]) / 2, x);
                var k2 = K((XCD[m] + XCD[m - 1]) / 2, t);
                var f = k1 * k2;
                sum += (XCD[m] - XCD[m - 1]) * f;
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

        public double Legendre(double x, int n)
        {
            if (n == 0)
                return 1.0;
            else if (n == 1)
                return x;
            else
            {
                var pn2 = 1.0;
                var pn1 = x;
                var pCurrent = 0.0;
                for (int i = 2; i <= n; i++)
                {
                    pCurrent = ((2 * i + 1) / (i + 1)) * x * pn1 - ((double)n) / ((double)(n - 1)) * pn2;
                    pn1 = pn2;
                    pn2 = pCurrent;
                }
                return pCurrent;
            }
        }

        #endregion

        #region Tolerance
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
        #endregion

        #region Find C matrix
        public double FindC(int i, int j)
        {
            var c_ij = FirstC(i, i) - SecondC(i, j);
            return c_ij;
        }

        public double FirstC(int i, int j)
        {
            var sum = 0.0;
            for (int k = 1; k < X.Length; k++)
            {
                var h1 = Legendre((X[k] + X[k - 1]) / 2, i);
                var h2 = Legendre((X[k] + X[k - 1]) / 2, j);
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
                var h1 = Legendre((X[n] + X[n - 1]) / 2, i);
                var sumInner = 0.0;
                for (int m = 1; m < X.Length; m++)
                {
                    var k = WaveK((X[n] + X[n - 1]) / 2, (X[m] + X[m - 1]) / 2);
                    var h2 = Legendre((X[m] + X[m - 1]) / 2, j);
                    var fInner = h1 * h2;
                    sumInner += (X[m] - X[m - 1]) * fInner;
                }
                var fOutter = h1 * sumInner;
                sumOutter += (X[n] - X[n - 1]) * fOutter;
            }
            return sumOutter;
        }
        #endregion

        #region FindB
        public double FindB(int i)
        {
            var sum = 0.0;
            for (int k = 1; k < X.Length; k++)
            {
                var f = WaveF((X[k] + X[k - 1]) / 2);
                var h = Legendre((X[k] + X[k - 1]) / 2, i);
                sum += (X[k] - X[k - 1]) * f * h;
            }
            return sum;
        }
        #endregion

        #region Gauss
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
        #endregion
    }
}
