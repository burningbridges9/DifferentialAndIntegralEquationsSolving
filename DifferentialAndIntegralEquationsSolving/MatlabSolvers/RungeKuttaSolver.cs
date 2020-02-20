using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialAndIntegralEquationsSolving.MatlabSolvers
{
    public class RungeKuttaSolver : MatlabSolver
    {
        public RungeKuttaSolver()
        {
            this.Matlab = new MLApp.MLApp();
        }
    }
}
