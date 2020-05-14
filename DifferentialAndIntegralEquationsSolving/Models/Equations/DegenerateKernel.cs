using DifferentialAndIntegralEquationsSolving.Equations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialAndIntegralEquationsSolving.Models.Equations
{
    public class DegenerateKernel : Equation
    {
        public double[] B { get; set; }
        public DegenerateKernel()
        {
            X0 = -1.0;
            X1 = 1.0;
            N = 10;
            SetupX();
            B = new double[N];
            FillB();
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

        public void Tolerance()
        {
            Diff = new double[N];
            for (int j = 0; j < X.Length; j++)
            {
                Diff[j] = Math.Abs(Ysolve(X[j]) - IntegrateForCheck(X[j]) + 2 - X[j] + 2 * X[j] * X[j]);
            }
        }

        public double IntegrateForCheck(double xj)
        {
            var sum = 0.0;
            for (int i = 1; i < X.Length; i++)
            {
                var F_ti_xj = K(xj, (X[i] + X[i - 1]) / 2) * Ysolve((X[i] + X[i - 1]) / 2);
                sum += H * F_ti_xj;
            }
            return sum;
        }
        private void FillB()
        {
            for (int i = 0; i < X.Length; i++)
            {
                B[i] = F(X[i]);
            }
        }

        public double Ysolve(double x)
        {
            var retVal = 3 * x;
            return retVal;
        }
        public double K(double x, double t)
        {
            return (x + 1) * t + x * x * (t + 1);
        }

        public double F(double x)
        {
            var retVal = -2 + x - 2 * x * x;
            return retVal;
        }

        public override void Solve()
        {
            throw new NotImplementedException();
        }
    }
}
