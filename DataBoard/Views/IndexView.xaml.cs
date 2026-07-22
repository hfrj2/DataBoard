using DataBoard.Model;
using DataBoard.Model.Provider;
using DataBoard.ViewModel;
using LiveCharts;
using LiveCharts.Wpf;
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
using static System.Windows.Forms.LinkLabel;



namespace DataBoard.Views
{
    /// <summary>
    /// IndexView.xaml 的交互逻辑
    /// </summary>
    public partial class IndexView : UserControl
    {
       
        private IProvider<History> historyProvider = new HistoryProvider();
        private IProvider<Model.Line> lineProvider = new LineProvider();
        private IProvider<SubLine> subLineProvider = new SubLineProvider();
        private IProvider<StopType> stopTypeProvider = new StopTypeProvider();
        private IndexViewModel vm = null;


        public IndexView()
        {
            InitializeComponent();
            vm = DataContext as IndexViewModel;
            DataContext = vm;
           
            Loaded += (s, e) =>
            {
                var histories = historyProvider.Select();
                var lines = lineProvider.Select();
                var sublines = subLineProvider.Select();
                var stoptypes = stopTypeProvider.Select();

                var datetime = DateTime.Now;
                var tempHistories = histories.FindAll(item => item.StartTime.Year == datetime.Year);


                // 生产线每年十二个月的停机曲线
                vm.LinesSeries.Clear();
                vm.LineAxis.Clear();
                foreach (var line in lines)
                {
                    LineSeries lineSeries = new LineSeries();
                    lineSeries.Title = line.Name;
                    lineSeries.Values = new ChartValues<double>();

                    var list = tempHistories.FindAll(t => t.LineId == line.Id);
                    for (int i = 1; i <= 12; i++)
                    {
                        double sumMintes = 0;
                        foreach (var history in list)
                        {
                            if (history.StartTime.Month == i)
                                sumMintes += history.Minutes;
                        }

                        lineSeries.Values.Add(sumMintes);
                    }
                    vm.LinesSeries.Add(lineSeries);

                }
                Axis axis = new Axis();
                axis.ShowLabels = true;
                axis.Labels = new List<string>();
                axis.Separator = new LiveCharts.Wpf.Separator() { Step = 1 };

                for (int i = 1; i <= 12; i++)
                {
                    axis.Labels.Add($"{i}月");
                }



                vm.LineAxis.Add(axis);

                //2.停机类型【饼图】
                vm.PieStopTypeSeries.Clear();
                // vm.PieStopTypeAxis.Clear();
                foreach (var stopType in stoptypes)
                {
                    PieSeries pieSeries = new PieSeries();
                    pieSeries.Values = new ChartValues<double>();
                    pieSeries.Title = stopType.Name;

                    var list = tempHistories.FindAll(t => t.StopTypeld == stopType.Id);
                    var sumMinutes = list.Sum(item => item.Minutes);
                    pieSeries.Values.Add(sumMinutes);
                    pieSeries.DataLabels = true;

                    vm.PieStopTypeSeries.Add(pieSeries);
                    //vm.PieStopTypeAxis.Add();


                }

                //3.子线
                vm.RowSubLineSeries.Clear();
                vm.RowSubLineAxis.Clear();

                Axis rowAxis = new Axis();
                axis.ShowLabels = true;
                axis.Separator = new LiveCharts.Wpf.Separator() { Step = 1 };
                axis.Labels = new List<string>();



                foreach (var line in lines)
                    {
                        var values = new ChartValues<double>();
                        foreach (var subline in sublines)
                        {
                            var list = tempHistories.FindAll(t => t.SubLineId == subline.Id&& t.LineId == line.Id);
                            var sumMinutes = list.Sum(item => item.Minutes);
                        values.Add(sumMinutes);
                    }
                    var rowSeries = new ColumnSeries
                    {
                        Title = line.Name,
                        Values = values,
                        LabelPoint = point => point.Y + "min",
                        DataLabels = true,
                    };
                    vm.RowSubLineSeries.Add(rowSeries);
                    rowAxis.Labels.Add(line.Name);
                }

                vm.RowSubLineAxis.Add(rowAxis);
                /*
                foreach (var subline in sublines)
                {
                RowSeries rowSeries = new RowSeries();
                rowSeries.Values = new ChartValues<double>();
                rowSeries.Title = subline.Name;

                var list = tempHistories.FindAll(t => t.SubLineId == subline.Id);
                var sumMinutes = list.Sum(item => item.Minutes);
                rowSeries.Values.Add(sumMinutes);
                rowSeries.DataLabels = true;

                vm.RowSubLineSeries.Add(rowSeries);

                Axis rowAxis = new Axis();
                axis.ShowLabels = true;
                axis.Labels = new List<string>();
                axis.Separator = new LiveCharts.Wpf.Separator() { Step = 1 };

                vm.RowSubLineAxis.Add(rowAxis);
                }
                */



            };
        }
    }
}
