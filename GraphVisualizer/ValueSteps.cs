using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GraphVisualizer
{
    class ValueSteps : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private double valueStep;
        /// <summary>
        /// 値の増減幅を取得・設定します。
        /// </summary>
        /// <remarks>
        /// 負の値に設定しようとした場合、実際にはその絶対値が設定されます。
        /// </remarks>
        public double ValueStep
        {
            get { return valueStep; }
            set
            {
                double d = Math.Abs(value);
                if (valueStep != d)
                {
                    valueStep = d;
                    RaisePropertyChanged("ValueStep");
                }
            }
        }

        private double graphMoveStep;
        /// <summary>
        /// グラフ移動幅を取得・設定します。
        /// </summary>
        /// <remarks>
        /// 負の値に設定しようとした場合、実際にはその絶対値が設定されます。
        /// </remarks>
        public double GraphMoveStep
        {
            get { return graphMoveStep; }
            set
            {
                double d = Math.Abs(value);
                if (graphMoveStep != d)
                {
                    graphMoveStep = d;
                    RaisePropertyChanged("GraphMoveStep");
                }
            }
        }

        void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
