using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialAndIntegralEquationsSolving.Equations
{
    public class Euler : Equation
    {
        public Euler(int x0, int x1, double h, double y0)
        {
            this.X0 = x0;
            this.X1 = x1;
            this.H = h;
            this.Y0 = y0;
            this.SetupX();
            this.Y = new double[N];
            Y[0] = Y0;
        }
        public override void Solve()
        {
            for (int i = 1; i < X.Length; i++)
            {
                Y[i] = X[i-1] - Y[i - 1] + 2;
            }
        }
    }
}
