﻿using DifferentialAndIntegralEquationsSolving.Equations;
using System;

namespace DifferentialAndIntegralEquationsSolving.Models.Equations
{
    public class TikhonovRegularizationGalerkin : Equation
    {
        public double[,] C { get; set; }
        public double[] B { get; set; }

        public double[] Diff { get; set; }

        public double[] WeightsE { get; set; }

        public TikhonovRegularizationGalerkin()
        {
            N = 10;
            C = new double[N, N];
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
            X[0]= 0.137793470540;
            X[1]= 0.729454549503;
            X[2]= 1.808342901740;
            X[3]= 3.401433697855;
            X[4]= 5.552496140064;
            X[5]= 8.330152746764;
            X[6]= 11.843785837900;
            X[7]= 16.279257831378;
            X[8]= 21.996585811981;
            X[9]= 29.920697012274;


            WeightsE = new double[N];
            WeightsE[0] = 0.308441115765;
            WeightsE[1] = 0.401119929155;
            WeightsE[2] = 0.218068287612;
            WeightsE[3] = 0.00620874560987;
            WeightsE[4] = 0.000950151697518;
            WeightsE[5] = 0.0000753008388588;
            WeightsE[6] = 0.000002822592334960;
            WeightsE[7] = 0.0000000424931398496;
            WeightsE[8] = 0.000000000183956482398;
            WeightsE[9] = 0.0000000000000991182721961;
        }

        public override void Solve()
        {
            FillMatrix();
            FillB();
            var A = Gauss(C, B);
            Y = new double[N];
            var alpha = 1.0;
            for (int i = 0; i < N; i++)
            {
                var sum = 0.0;
                for (int j = 0; j < N; j++)
                {
                    sum += A[j] * Laguerre(X[i], j);
                }
                Y[i] += sum/alpha;
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

        #region Functions

        public double F(double x)
        {
            var retVal = 2 - x;
            return retVal;
        }

        public double WaveF(double x)
        {
            var sum = 0.0;
            for (int i = 0; i < X.Length; i++)
            {
                var wi = WeightsE[i];
                var si = X[i];

                var k = K(si, x);
                var f = F(si);
                sum += wi * k * f;
            }
            return sum;
        }

        public double K(double x, double t)
        {
            return t * (t - x);
        }

        public double WaveK(double x, double t)
        {
            var sum = 0.0;
            for (int i = 0; i < X.Length; i++)
            {
                var wi = WeightsE[i];
                var si = X[i];

                var k1 = K(si, x);
                var k2 = K(si, t);

                sum += wi * k1 * k2;
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

        public double Laguerre(double x, int n)
        {
            var Ln = 0.0;
            for (int k = 0; k <= n; k++)
            {
                var binom_nk = Factorial(n) / (Factorial(k) * Factorial(n - k));
                var r = Math.Pow(-1, k) * Math.Pow(x, k) / Factorial(k);
                Ln += binom_nk * r;
            }
            return Ln;
        }
        
        #endregion

        #region Tolerance
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
            for (int k = 0; k < X.Length; k++)
            {
                var xk = X[k];
                var wk = WeightsE[k];
                var h1 = Laguerre(xk, i);
                var h2 = Laguerre(xk, j);

                var f = h1 * h2;
                sum += wk * f;
            }
            return sum;
        }

        public double SecondC(int i, int j)
        {
            var sumOutter = 0.0;
            for (int n = 0; n < X.Length; n++)
            {
                var xn = X[n];
                var wn = WeightsE[n];

                var h1 = Laguerre(xn, i);
                var sumInner = 0.0;
                for (int m = 0; m < X.Length; m++)
                {
                    var xm = X[m];
                    var wm = WeightsE[m];

                    var k = WaveK(xn, xm);
                    var h2 = Laguerre(xm, j);
                    var fInner = k * h2;
                    sumInner += wm * fInner;
                }
                var fOutter = h1 * sumInner;
                sumOutter += wn * fOutter;
            }
            return sumOutter;
        }
        #endregion
        public double FindB(int i)
        {
            var sum = 0.0;
            for (int k = 0; k < X.Length; k++)
            {
                var wi = WeightsE[k];
                var f = WaveF(X[k]);
                var h = Laguerre(X[k], i);
                sum += wi * f * h;
            }
            return sum;
        }

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
