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

            A[0] = A[N - 1] = C[0] = C[N - 1] = 0;

            D[0] = Y0;
            D[N-1] = YN;

            S[0] = -C[0] / B[0];
            T[0] = D[0] / B[0];

            for (int i = 1; i < N - 1; i++)
            {
                A[i] = Ai;
                B[i] = Bi;
                C[i] = Ci;
                var xi = X[i];
                D[i] = G(xi);
            }

            for (int i = 1; i < N; i++)
            {
                S[i] = C[i] / (B[i] - A[i] * S[i - 1]);
                T[i] = (A[i] * T[i - 1] - D[i]) / (B[i] - A[i] * S[i - 1]);
            }
        }

        public override void Solve()
        {
            Y[N - 1] = T[N - 1];
            for (int i = N - 2; i >= 0; i--)
            {
                Y[i] = S[i] * Y[i + 1] + T[i];
            }
        }
        private double G(double xi)
        {
            var val = xi + 9;
            return val;
        }
    }
}
