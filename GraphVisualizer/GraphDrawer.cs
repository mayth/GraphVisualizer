using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Algebra;
using System.ComponentModel;

namespace GraphVisualizer
{
    /// <summary>
    /// グラフ描画クラス。
    /// </summary>
    class GraphDrawer : INotifyPropertyChanged
    {
        #region Private Members
        Size originalCanvasSize;
        const double scaleMarginX = 20;
        const double scaleMarginY = 30;
        const double step = 0.005;
        double xLeftLimit;
        double xRightLimit;
        readonly Typeface timesNewRomanFace = new Typeface("Times New Roman");
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        #region Public Properties
        /// <summary>
        /// 描画するグラフの式を格納した<see cref="Calculator"/>クラスのインスタンスを取得・設定します。
        /// </summary>
        public Calculator Calculator { get; set; }

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

        double _gridLineX;
        /// <summary>
        /// 目盛り線のX間隔を取得・設定します。
        /// </summary>
        public double GridLineX
        {
            get { return _gridLineX; }
            set
            {
                if (_gridLineX != value)
                {
                    _gridLineX = value;
                    RaisePropertyChanged("GridLineX");
                }
            }
        }

        double _gridLineY;
        /// <summary>
        /// 目盛り線のY間隔を取得・設定します。
        /// </summary>
        public double GridLineY
        {
            get { return _gridLineY; }
            set
            {
                if (_gridLineY != value)
                {
                    _gridLineY = value;
                    RaisePropertyChanged("GridLineY");
                }
            }
        }

        double _subGridLineX;
        /// <summary>
        /// 補助目盛り線のX間隔を取得・設定します。
        /// </summary>
        public double SubGridLineX
        {
            get { return _subGridLineX; }
            set
            {
                if (_subGridLineX != value)
                {
                    _subGridLineX = value;
                    RaisePropertyChanged("SubGridLineX");
                }
            }
        }

        double _subGridLineY;
        /// <summary>
        /// 補助目盛り線のY間隔を取得・設定します。
        /// </summary>
        public double SubGridLineY
        {
            get { return _subGridLineY; }
            set
            {
                if (_subGridLineY != value)
                {
                    _subGridLineY = value;
                    RaisePropertyChanged("SubGridLineY");
                }
            }
        }

        int _scaleX;
        /// <summary>
        /// X軸について、数字を何目盛ごとにつけるかを取得・設定します。
        /// </summary>
        public int ScaleX
        {
            get { return _scaleX; }
            set
            {
                if (_scaleX != value)
                {
                    _scaleX = value;
                    RaisePropertyChanged("ScaleX");
                }
            }
        }

        int _scaleY;
        /// <summary>
        /// Y軸について、数字を何目盛ごとにつけるかを取得・設定します。
        /// </summary>
        public int ScaleY
        {
            get { return _scaleY; }
            set
            {
                if (_scaleY != value)
                {
                    _scaleY = value;
                    RaisePropertyChanged("ScaleY");
                }
            }
        }

        Size _canvasSize;
        /// <summary>
        /// 描画キャンバスの大きさを取得・設定します。
        /// </summary>
        public Size CanvasSize
        {
            get { return _canvasSize; }
            set
            {
                if (_canvasSize != value)
                {
                    _canvasSize = value;
                    RaisePropertyChanged("CanvasSize");
                }
            }
        }

        double _offsetX;
        /// <summary>
        /// 中心点からのX方向のずれを取得・設定します。
        /// </summary>
        public double OffsetX
        {
            get { return _offsetX; }
            set
            {
                if (_offsetX != value)
                {
                    _offsetX = value;
                    RaisePropertyChanged("OffsetX");
                }
            }
        }

        double _offsetY;
        /// <summary>
        /// 中心点からのY方向のずれを取得・設定します。
        /// </summary>
        public double OffsetY
        {
            get { return _offsetY; }
            set
            {
                if (_offsetY != value)
                {
                    _offsetY = value;
                    RaisePropertyChanged("OffsetY");
                }
            }
        }

        bool _enableGridLine;
        /// <summary>
        /// 目盛り線を有効にするかどうかを取得・設定します。
        /// </summary>
        public bool EnableGridLine
        {
            get { return _enableGridLine; }
            set
            {
                if (_enableGridLine != value)
                {
                    _enableGridLine = value;
                    RaisePropertyChanged("EnableGridLine");
                }
            }
        }

        bool _enableSubGridLine;
        /// <summary>
        /// 補助目盛り線を有効にするかどうかを取得・設定します。
        /// </summary>
        public bool EnableSubGridLine
        {
            get { return _enableSubGridLine; }
            set
            {
                if (_enableSubGridLine != value)
                    _enableSubGridLine = value;
                RaisePropertyChanged("EnableSubGridLine");
            }
        }

        bool _isDrawGraphLine;
        /// <summary>
        /// 点と点との間に線を引くかどうかを取得・設定します。
        /// </summary>
        public bool IsDrawGraphLine
        {
            get { return _isDrawGraphLine; }
            set
            {
                if (_isDrawGraphLine != value)
                {
                    _isDrawGraphLine = value;
                    RaisePropertyChanged("IsDrawGraphLine");
                }
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// 中心点座標を取得・設定します。
        /// </summary>
        Point Center { get; set; }

        /// <summary>
        /// 計算結果を取得・設定します。
        /// </summary>
        Dictionary<double, double> Result { get; set; }
        #endregion
        #endregion

        #region Constructors
        /// <summary>
        /// <see cref="GraphDrawer"/>クラスを初期化します。
        /// </summary>
        /// <param name="canvasSize">グラフ描画先のサイズ</param>
        public GraphDrawer(Size canvasSize)
        {
            AxisLineBrush = Brushes.Black;
            AxisLineThickness = 3;
            GridLineBrush = Brushes.Gray;
            GridLineThickness = 2;
            SubGridLineBrush = Brushes.LightGray;
            SubGridLineThickness = 1;
            GraphBrush = Brushes.Purple;
            GraphThickness = 2;

            GridLineX = 40;
            GridLineY = 40;
            SubGridLineX = GridLineX / 2;
            SubGridLineY = GridLineY / 2;

            ScaleX = 2;
            ScaleY = 2;

            originalCanvasSize = canvasSize;
            CanvasSize = canvasSize;
            Center = new Point(CanvasSize.Width / 2, CanvasSize.Height / 2);
            OffsetX = 0;
            OffsetY = 0;

            EnableGridLine = true;
            EnableSubGridLine = false;
            IsDrawGraphLine = true;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// グラフを描画します。
        /// </summary>
        /// <param name="dg">描画に使用する<see cref="DrawingContext"/>のインスタンス</param>
        /// <param name="isResultUpdateRequired">計算結果の更新を必要とするかどうかを指定します。</param>
        public void Draw(DrawingContext dc, bool isResultUpdateRequired)
        {
            #region Error Check
            if (GridLineX <= 0
                || GridLineY <= 0
                || (EnableSubGridLine && (SubGridLineX <= 0 || SubGridLineY <= 0)))
            {
                FormattedText ft = new FormattedText(
                        "Drawing Error",
                        System.Globalization.CultureInfo.InvariantCulture,
                        FlowDirection.LeftToRight,
                        timesNewRomanFace,
                        18,
                        Brushes.DarkRed);
                dc.DrawRectangle(Brushes.LightGray, new Pen(Brushes.DarkRed, 2.0), new Rect(CanvasSize));
                dc.DrawText(ft, new Point((CanvasSize.Width - ft.Width) / 2, (CanvasSize.Height - ft.Height) / 2));
                return;
            }
            #endregion

            double xAxis = Center.X + OffsetX;
            double yAxis = Center.Y + OffsetY;

            #region Draw Sub Grid Lines
            if (EnableSubGridLine)
            {
                Pen subGridPen = new Pen(SubGridLineBrush, SubGridLineThickness);
                subGridPen.Freeze();

                if (0 <= xAxis && xAxis <= CanvasSize.Width)
                    DrawVerticalLine(dc, xAxis, subGridPen);
                if (0 <= yAxis && yAxis <= CanvasSize.Height)
                    DrawHorizontalLine(dc, yAxis, subGridPen);

                #region Draw X Sub-Grid Line
                int subGridCountX = (int)((CanvasSize.Width - Center.X) / SubGridLineX);
                for (int i = 1; i <= subGridCountX; i++)
                {
                    double leftSide = Center.X - (SubGridLineX * i) + OffsetX;
                    if (0 <= leftSide && leftSide <= CanvasSize.Width)
                        DrawVerticalLine(dc, leftSide, subGridPen);

                    double rightSide = Center.X + (SubGridLineX * i) + OffsetX;
                    if (0 <= rightSide && rightSide <= CanvasSize.Width)
                        DrawVerticalLine(dc, rightSide, subGridPen);
                }
                #endregion

                #region Draw Y Sub-Grid Line
                int subGridCountY = (int)((CanvasSize.Height - Center.Y) / SubGridLineY);
                for (int i = 1; i <= subGridCountY; i++)
                {
                    double upSide = Center.Y - (SubGridLineY * i) + OffsetY;
                    if (0 <= upSide && upSide <= CanvasSize.Height)
                        DrawHorizontalLine(dc, upSide, subGridPen);

                    double downSide = Center.Y + (SubGridLineY * i) + OffsetY;
                    if (0 <= downSide && downSide <= CanvasSize.Height)
                        DrawHorizontalLine(dc, downSide, subGridPen);
                }
                #endregion
            }
            #endregion

            #region Draw Grid Lines
            if (EnableGridLine)
            {
                Pen gridPen = new Pen(GridLineBrush, GridLineThickness);
                gridPen.Freeze();

                #region Draw X Grid Line
                int gridCountX = (int)((CanvasSize.Width - Center.X + Math.Abs(OffsetX)) / GridLineX);
                if (0 <= xAxis && xAxis <= CanvasSize.Width)
                    DrawVerticalLine(dc, xAxis, gridPen);

                for (int i = 1; i <= gridCountX; i++)
                {
                    double leftSide = Center.X - (GridLineX * i) + OffsetX;
                    if (0 <= leftSide && leftSide <= CanvasSize.Width)
                    {
                        DrawVerticalLine(dc, leftSide, gridPen);
                        xLeftLimit = -i;
                        if (ScaleX > 0 && i % ScaleX == 0 && leftSide + scaleMarginX <= CanvasSize.Width)
                        {
                            double scalePosY;
                            if (0 <= yAxis && yAxis + scaleMarginY <= CanvasSize.Height)
                                scalePosY = yAxis;
                            else if (yAxis < 0)
                                scalePosY = 0;
                            else
                                scalePosY = CanvasSize.Height - scaleMarginY;
                            dc.DrawText(new FormattedText(
                                (-i).ToString(),
                                System.Globalization.CultureInfo.InvariantCulture,
                                FlowDirection.LeftToRight,
                                new Typeface("Times New Roman"),
                                18,
                                Brushes.DarkMagenta),
                                new Point(leftSide, scalePosY));
                        }
                    }

                    double rightSide = Center.X + (GridLineX * i) + OffsetX;
                    if (0 <= rightSide && rightSide <= CanvasSize.Width)
                    {
                        DrawVerticalLine(dc, rightSide, gridPen);
                        xRightLimit = i;
                        if (ScaleX > 0 && i % ScaleX == 0 && rightSide + scaleMarginX <= CanvasSize.Width)
                        {
                            double scalePosY;
                            if (0 <= yAxis && yAxis + scaleMarginY <= CanvasSize.Height)
                                scalePosY = yAxis;
                            else if (yAxis < 0)
                                scalePosY = 0;
                            else
                                scalePosY = CanvasSize.Height - scaleMarginY;
                            dc.DrawText(new FormattedText(
                                i.ToString(),
                                System.Globalization.CultureInfo.InvariantCulture,
                                FlowDirection.LeftToRight,
                                new Typeface("Times New Roman"),
                                18,
                                Brushes.DarkMagenta),
                                new Point(rightSide, scalePosY));
                        }
                    }
                }
                #endregion

                #region Draw Y Grid Line
                int gridCountY = (int)((CanvasSize.Height - Center.Y + Math.Abs(OffsetY)) / GridLineY);
                if (0 <= yAxis && yAxis <= CanvasSize.Height)
                    DrawHorizontalLine(dc, yAxis, gridPen);
                for (int i = 1; i <= gridCountY; i++)
                {
                    double upSide = Center.Y - (GridLineY * i) + OffsetY;
                    if (0 <= upSide && upSide <= CanvasSize.Height)
                    {
                        DrawHorizontalLine(dc, upSide, gridPen);
                        if (ScaleY > 0 && i % ScaleY == 0 && upSide + scaleMarginY <= CanvasSize.Height)
                        {
                            double scalePosX;
                            if (0 <= xAxis && xAxis + scaleMarginX <= CanvasSize.Width)
                                scalePosX = xAxis;
                            else if (xAxis < 0)
                                scalePosX = 0;
                            else
                                scalePosX = CanvasSize.Width - scaleMarginX;
                            dc.DrawText(new FormattedText(
                                i.ToString(),
                                System.Globalization.CultureInfo.InvariantCulture,
                                FlowDirection.LeftToRight,
                                new Typeface("Times New Roman"),
                                18,
                                Brushes.Brown),
                                new Point(scalePosX, upSide));
                        }
                    }

                    double downSide = Center.Y + (GridLineY * i) + OffsetY;
                    if (0 <= downSide && downSide <= CanvasSize.Height)
                    {
                        DrawHorizontalLine(dc, downSide, gridPen);
                        if (ScaleY > 0 && i % ScaleY == 0 && downSide + scaleMarginY <= CanvasSize.Height)
                        {
                            double scalePosX;
                            if (0 <= xAxis && xAxis + scaleMarginX <= CanvasSize.Width)
                                scalePosX = xAxis;
                            else if (xAxis < 0)
                                scalePosX = 0;
                            else
                                scalePosX = CanvasSize.Width - scaleMarginX;
                            dc.DrawText(new FormattedText(
                                (-i).ToString(),
                                System.Globalization.CultureInfo.InvariantCulture,
                                FlowDirection.LeftToRight,
                                new Typeface("Times New Roman"),
                                18,
                                Brushes.Brown),
                                new Point(scalePosX, downSide));
                        }
                    }
                }
                #endregion
            }
            #endregion

            #region Draw Axis Line
            Pen axisPen = new Pen(AxisLineBrush, AxisLineThickness);
            axisPen.Freeze();
            if (0 <= xAxis && xAxis <= CanvasSize.Width)
                DrawVerticalLine(dc, xAxis, axisPen);
            if (0 <= yAxis && yAxis <= CanvasSize.Height)
                DrawHorizontalLine(dc, Center.Y + OffsetY, axisPen);
            if ((0 <= xAxis && xAxis + scaleMarginX <= CanvasSize.Width) && (0 <= yAxis && yAxis + scaleMarginY <= CanvasSize.Height))
                dc.DrawText(
                    new FormattedText(
                        "O",
                        System.Globalization.CultureInfo.InvariantCulture,
                        FlowDirection.LeftToRight,
                        new Typeface("Times New Roman"),
                        18,
                        Brushes.Black),
                        new Point(xAxis, yAxis));
            #endregion

            #region Draw Graph Line
            if (isResultUpdateRequired)
                Result = new Dictionary<double, double>(Calculator.CalculateRange(new Range(xLeftLimit - 1, xRightLimit + 1, step)));
            if (IsDrawGraphLine)
            {
                PathGeometry geometry = new PathGeometry();
                for (int i = 0; i < Result.Count - 1; i++)
                {
                    KeyValuePair<double, double> r1 = Result.ElementAt(i);
                    KeyValuePair<double, double> r2 = Result.ElementAt(i + 1);
                    Point p1 = new Point((r1.Key * GridLineX) + Center.X + OffsetX, (-r1.Value * GridLineY) + Center.Y + OffsetY);
                    Point p2 = new Point((r2.Key * GridLineX) + Center.X + OffsetX, (-r2.Value * GridLineY) + Center.Y + OffsetY);
                    geometry.AddGeometry(new LineGeometry(p1, p2));
                }
                dc.DrawGeometry(null, new Pen(GraphBrush, GraphThickness), geometry);
            }
            else
            {
                foreach (var r in Result)
                {
                    double x = (r.Key * GridLineX) + Center.X + OffsetX;
                    double y = (-r.Value * GridLineY) + Center.Y + OffsetY;
                    if (0 <= x - GraphThickness && x + GraphThickness <= CanvasSize.Width && 0 <= y - GraphThickness && y + GraphThickness <= CanvasSize.Height)
                        dc.DrawEllipse(GraphBrush, null, new Point(x, y), GraphThickness, GraphThickness);
                }
            }
            #endregion

#if DEBUG
            dc.DrawRectangle(null, new Pen(Brushes.Red, 1.0), new Rect(CanvasSize));
#endif
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 垂直な線を引きます。
        /// </summary>
        /// <param name="dc">描画に使用する<see cref="DrawingContext"/>クラスのインスタンス</param>
        /// <param name="x">描画するX座標</param>
        /// <param name="pen">描画に使用するペン</param>
        void DrawVerticalLine(DrawingContext dc, double x, Pen pen)
        {
            dc.DrawLine(pen, new Point(x, 0), new Point(x, CanvasSize.Height));
        }

        /// <summary>
        /// 水平な線を引きます。
        /// </summary>
        /// <param name="dc">描画に使用する<see cref="DrawingContext"/>クラスのインスタンス</param>
        /// <param name="y">描画するY座標</param>
        /// <param name="pen">描画に使用するペン</param>
        void DrawHorizontalLine(DrawingContext dc, double y, Pen pen)
        {
            dc.DrawLine(pen, new Point(0, y), new Point(CanvasSize.Width, y));
        }

        /// <summary>
        /// <see cref="PropertyChanged"/>イベントを発生させます。
        /// </summary>
        /// <param name="name">変更されたプロパティ名</param>
        void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
