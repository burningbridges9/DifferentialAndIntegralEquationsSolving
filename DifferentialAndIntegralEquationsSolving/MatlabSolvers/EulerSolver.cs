using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLApp;

namespace DifferentialAndIntegralEquationsSolving.MatlabSolvers
{
    public class EulerSolver : MatlabSolver
    {
        public EulerSolver()
        {
            this.Matlab = new MLApp.MLApp();
        }
    }
}
