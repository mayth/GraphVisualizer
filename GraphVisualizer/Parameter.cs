using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GraphVisualizer
{
    /// <summary>
    /// パラメータを表します。
    /// </summary>
    class Parameter : INotifyPropertyChanged
    {
        #region Properties
        char character;
        /// <summary>
        /// 文字を取得・設定します。
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">大文字・小文字のアルファベット以外の文字を設定しています。</exception>
        public char Character
        {
            get { return character; }
            set
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(value.ToString(), "[a-zA-Z]"))
                    throw new ArgumentOutOfRangeException("文字として指定できるのは大文字・小文字のアルファベットのみです。");
                if (character != value)
                {
                    character = value;
                    NotifyPropertyChanged("Character");
                }
            }
        }

        double value;
        /// <summary>
        /// 値を取得・設定します。
        /// </summary>
        public double Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    NotifyPropertyChanged("Value");
                }
            }
        }

        bool isLinked;
        /// <summary>
        /// 値が他のパラメータとリンクするかどうかを取得・設定します。
        /// </summary>
        public bool IsLinked
        {
            get { return isLinked; }
            set
            {
                if (isLinked != value)
                {
                    isLinked = value;
                    NotifyPropertyChanged("IsLinked");
                }
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Constructors
        /// <summary>
        /// 文字を指定してパラメータを初期化します。
        /// </summary>
        /// <param name="c">パラメータを表す文字</param>
        public Parameter(char c)
        {
            Character = c;
            Value = 0.0;
            IsLinked = false;
        }

        /// <summary>
        /// 文字とその値を指定してパラメータを初期化します。
        /// </summary>
        /// <param name="c">パラメータを表す文字</param>
        /// <param name="value">値</param>
        public Parameter(char c, double value)
            : this(c)
        {
            Value = value;
        }

        /// <summary>
        /// 文字とその値、他のパラメータとのリンク状態を指定してパラメータを初期化します。
        /// </summary>
        /// <param name="c">パラメータを表す文字</param>
        /// <param name="value">値</param>
        /// <param name="isLinked">他のパラメータの値とリンクするかどうか</param>
        public Parameter(char c, double value, bool isLinked)
            : this(c, value)
        {
            IsLinked = isLinked;
        }
        #endregion

        /// <summary>
        /// 変更を通知します。
        /// </summary>
        /// <param name="name">変更されたプロパティ名</param>
        void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
