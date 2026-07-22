using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBoard.ViewModel
{
    public class IndexViewModel : ViewModelBase
    {

        public SeriesCollection LinesSeries {  get; set; }=new SeriesCollection();
        public AxesCollection LineAxis { get; set; } = new AxesCollection();


        //

        public SeriesCollection PieStopTypeSeries { get; set; } = new SeriesCollection();

        //public AxesCollection PieStopTypeAxis { get; set; } = new AxesCollection();


        //

        public SeriesCollection RowSubLineSeries { get; set; } = new SeriesCollection();
        public AxesCollection RowSubLineAxis { get; set; } = new AxesCollection();

        //子线饼图
        public SeriesCollection PieSubLineSeries { get; set; } = new SeriesCollection();


    }
}
