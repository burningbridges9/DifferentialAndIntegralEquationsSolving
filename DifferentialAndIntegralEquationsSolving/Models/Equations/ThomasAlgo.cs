using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialAndIntegralEquationsSolving.Equations
{
    public class ThomasAlgo : Equation
    {
        public double YN { get; set; }
        public double[] A { get; set; }
        public double[] B { get; set; }
        public double[] C { get; set; }
        public double[] D { get; set; }
        public double[] S { get; set; }
        public double[] T { get; set; }
        public ThomasAlgo(double x0, double x1, double h, double y0, double y1)
        {
            this.X0 = x0;
            this.X1 = x1;
            this.H = h;
            this.Y0 = y0;
            this.YN = y1;
            this.SetupX();
            this.Y = new double[N];
            Y[0] = Y0;
            Y[N - 1] = YN;
            A = new double[N];
            B = new double[N];
            C = new double[N];
            D = new double[N];
            S = new double[N];
            T = new double[N];
            FillMatrixAndVectorForAlgo();
        }

        public void FillMatrixAndVectorForAlgo()
        {
            double Ai = (1 - 2 * H) / Math.Pow(H, 2);
            double Bi = 2 / Math.Pow(H, 2);
            double Ci = (1 + 2 * H) / Math.Pow(H, 2);
            double B1 = 1, Bn = 1;

            B[0] = B1;
            B[N - 1] = Bn;

            A[0] = C[N - 1] = 0;

            D[0] = Y0;
            D[N-1] = YN;

            for (int i = 1; i < N; i++)
            {
                A[i] = Ai;
            }
            for (int i = 0; i < N; i++)
            {
                C[i] = Ci;
            }
            for (int i = 1; i < N - 1; i++)
            {
                B[i] = Bi;
                var xi = X[i];
                D[i] = G(xi);
            }

            S[0] = 0;
            T[0] = 1;

            for (int i = 1; i < N; i++)
            {
                S[i] = C[i] / (B[i] - A[i] * S[i - 1]);
                T[i] = (A[i] * T[i - 1] - D[i]) / (B[i] - A[i] * S[i - 1]);
            }
        }

        public override void Solve()
        {
            for (int i = N - 2; i > 0; i--)
            {
                Y[i] = S[i] * Y[i + 1] + T[i];
            }
        }
        private double G(double xi)
        {
            var val = xi + 9;
            return val;
        }

        public void Tolerance()
        {
            var Y_exact = new double[N];
            for (int i = 0; i < N; i++)
            {
                Y_exact[i] = YExact(X[i]);
            }
            Diff = new double[N];
            for (int j = 0; j < X.Length; j++)
            {
                Diff[j] = Math.Abs(Y[j] - Y_exact[j]);
            }
        }
        public double YExact(double xi)
        {
            var retVal = (35.0 * xi) / 16.0 + (57.0 * Math.Exp(8.0 - 4.0 * xi)) / (16.0 * (Math.Exp(4) - 1.0)) - (277.0 * Math.Exp(4) - 49.0) / (64.0 * (Math.Exp(4) - 1.0)) + (xi *xi) / 8.0 - 35.0 / 64.0;
            return retVal;
        }
    }
}
