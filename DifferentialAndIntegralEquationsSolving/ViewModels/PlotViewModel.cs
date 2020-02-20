using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DifferentialAndIntegralEquationsSolving.ViewModels
{
    public class PlotViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public PlotViewModel()
        {
            this.MyModel = new PlotModel();
            var s1 = new LineSeries
            {
                Color = OxyColors.SkyBlue,
                MarkerType = MarkerType.None,
                //MarkerSize = 3,
                //MarkerStroke = OxyColors.White,
                //MarkerFill = OxyColors.SkyBlue,
                MarkerStrokeThickness = 1.5
            };
            var s3 = new LineSeries
            {
                Color = OxyColors.Black,
                //MarkerType = MarkerType.Cross,
                //MarkerSize = 3,
                //MarkerStroke = OxyColors.White,
                //MarkerFill = OxyColors.SkyBlue,
                MarkerStrokeThickness = 1.5
            };
            var s2 = new LineSeries
            {
                Color = OxyColors.AliceBlue,
                MarkerType = MarkerType.Cross,
                MarkerSize = 1.5,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.AliceBlue,
                MarkerStrokeThickness = 1.5
            };

            MyModel.Series.Add(s1);

            MyModel.Series.Add(s2);
            MyModel.Series.Add(s3);
            MyModel.InvalidatePlot(true);
        }

        public void PlotXY(double[] x, double[] y)
        {
            MyModel.Series.Clear();
            MyModel.InvalidatePlot(true);
            var model = new PlotModel { LegendSymbolLength = 24 };
            model.Series.Add(new LineSeries
            {
                Color = OxyColors.SkyBlue,
                MarkerType = MarkerType.None,
                MarkerStrokeThickness = 1.5
            });
            foreach (var xy in x.Zip(y, Tuple.Create))
            {
                (model.Series[0] as LineSeries).Points.Add(new DataPoint(xy.Item1, xy.Item2));
            }
            MyModel = model;
        }

        public void PlotXYXY(double[] x1, double[] y1, double[] x2, double[] y2)
        {
            MyModel.Series.Clear();
            MyModel.InvalidatePlot(true);
            var model = new PlotModel { LegendSymbolLength = 24 };
            model.Series.Add(new LineSeries
            {
                Color = OxyColors.DarkBlue,
                MarkerType = MarkerType.Plus,
                MarkerStrokeThickness = 0.75                
            });
            foreach (var xy in x1.Zip(y1, Tuple.Create))
            {
                (model.Series[0] as LineSeries).Points.Add(new DataPoint(xy.Item1, xy.Item2));
            }

            model.Series.Add(new LineSeries
            {
                Color = OxyColors.Red,
                MarkerType = MarkerType.Cross,
                MarkerStrokeThickness = 0.5
            });
            foreach (var xy in x2.Zip(y2, Tuple.Create))
            {
                (model.Series[1] as LineSeries).Points.Add(new DataPoint(xy.Item1, xy.Item2));
            }
            MyModel = model;
        }

        private PlotModel myModel { get; set; }
        public PlotModel MyModel
        {
            get { return myModel; }
            set
            {
                myModel = value;
                OnPropertyChanged("MyModel");
            }
        }
    }
}