using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.ComponentModel;

namespace GraphVisualizer
{
    public class DrawingPenSettings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region
        Brush _axisLineBrush;
        /// <summary>
        /// 軸線の描画に使用するブラシを取得・設定します。
        /// </summary>
        public Brush AxisLineBrush
        {
            get { return _axisLineBrush; }
            set
            {
                if (_axisLineBrush != value)
                {
                    _axisLineBrush = value;
                    RaisePropertyChanged("AxisLineBrush");
                }
            }
        }

        double _axisLineThickness;
        /// <summary>
        /// 軸線の太さを取得・設定します。
        /// </summary>
        public double AxisLineThickness
        {
            get { return _axisLineThickness; }
            set
            {
                if (_axisLineThickness != value)
                {
                    _axisLineThickness = value;
                    RaisePropertyChanged("AxisLineThickness");
                }
            }
        }

        Brush _gridLineBrush;
        /// <summary>
        /// 目盛り線の描画に使用するブラシを取得・設定します。
        /// </summary>
        public Brush GridLineBrush
        {
            get { return _gridLineBrush; }
            set
            {
                if (_gridLineBrush != value)
                {
                    _gridLineBrush = value;
                    RaisePropertyChanged("GridLineBrush");
                }
            }
        }

        double _gridLineThickness;
        /// <summary>
        /// 目盛り線の太さを取得・設定します。
        /// </summary>
        public double GridLineThickness
        {
            get { return _gridLineThickness; }
            set
            {
                if (_gridLineThickness != value)
                {
                    _gridLineThickness = value;
                    RaisePropertyChanged("GridLineThickness");
                }
            }
        }

        Brush _subGridLineBrush;
        /// <summary>
        /// 補助目盛り線の描画に使用するブラシを取得・設定します。
        /// </summary>
        public Brush SubGridLineBrush
        {
            get { return _subGridLineBrush; }
            set
            {
                if (_subGridLineBrush != value)
                {
                    _subGridLineBrush = value;
                    RaisePropertyChanged("SubGridLineBrush");
                }
            }
        }

        double _subGridLineThickness;
        /// <summary>
        /// 補助目盛り線の太さを取得・設定します。
        /// </summary>
        public double SubGridLineThickness
        {
            get { return _subGridLineThickness; }
            set
            {
                if (_subGridLineThickness != value)
                {
                    _subGridLineThickness = value;
                    RaisePropertyChanged("SubGridLineThickness");
                }
            }
        }

        Brush _graphBrush;
        /// <summary>
        /// グラフの線の描画に使用するブラシを取得・設定します。
        /// </summary>
        public Brush GraphBrush
        {
            get { return _graphBrush; }
            set
            {
                if (_graphBrush != value)
                {
                    _graphBrush = value;
                    RaisePropertyChanged("GraphBrush");
                }
            }
        }

        double _graphThickness;
        /// <summary>
        /// グラフの線の太さを取得・設定します。
        /// </summary>
        public double GraphThickness
        {
            get { return _graphThickness; }
            set
            {
                if (_graphThickness != value)
                {
                    _graphThickness = value;
                    RaisePropertyChanged("GraphThickness");
                }
            }
        }
        #endregion

        public DrawingPenSettings()
        {
            AxisLineBrush = Brushes.Black;
            AxisLineThickness = 2.0;
            GridLineBrush = Brushes.Black;
            GridLineThickness = 1.5;
            SubGridLineBrush = Brushes.Black;
            SubGridLineThickness = 1.0;
            GraphBrush = Brushes.Black;
            GraphThickness = 2.0;
        }

        void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
