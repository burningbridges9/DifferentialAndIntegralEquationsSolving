using DifferentialAndIntegralEquationsSolving.ViewModels.Equations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DifferentialAndIntegralEquationsSolving.Views
{
    /// <summary>
    /// Interaction logic for EulerView.xaml
    /// </summary>
    public partial class EulerView : UserControl
    {
        public static EulerVM eulerVM;
        public EulerView()
        {
            eulerVM = new EulerVM();
            InitializeComponent();
            DataContext = eulerVM;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var x0 = double.Parse(X0.Text);
            var x1 = double.Parse(X1.Text);
            var h =  double.Parse(H.Text);
            var y0 = double.Parse(Y0.Text);

            eulerVM.Euler = new Equations.Euler(x0, x1, h, y0);
        }
    }
}
