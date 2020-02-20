using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialAndIntegralEquationsSolving.MatlabSolvers
{
    public class MatlabSolver
    {
        public MLApp.MLApp Matlab { get; set; }
        public string ScriptPath { get; set; }
            = @"C:\Users\Rustam\Documents\Visual Studio 2017\Projects\DifferentialAndIntegralEquationsSolving\DifferentialAndIntegralEquationsSolving\MatlabScripts";
        public double[] X { get; set; }
        public double[] Y { get; set; }
        public virtual (double[,], double[,]) Execute(string name, params double[] p)
        {
            Matlab.Execute(ScriptPath);

            object result = null;
            if (p.Length == 4)
                Matlab.Feval(name, 2, out result, p[0], p[1], p[2], p[3]);
            if (p.Length == 5)
                Matlab.Feval(name, 2, out result, p[0], p[1], p[2], p[3], p[4]);

            object[] res = result as object[];
            double[,] X = res[0] as double[,];
            double[,] Y = res[1] as double[,];
            this.X = new double[X.GetUpperBound(0) + 1];
            this.Y = new double[Y.GetUpperBound(0) + 1];

            for (int i = 0; i <= X.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= X.GetUpperBound(1); j++)
                {
                    this.X[i] = X[i, j];
                    this.Y[i] = Y[i, j];
                }
            }
            return (X, Y);
        }
    }
}
