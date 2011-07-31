using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    class CharacterNode : Node
    {
        private char _character;

        /// <summary>
        /// このノードの文字を取得します。
        /// </summary>
        public char Character
        {
            get { return _character; }
        }

        /// <summary>
        /// 文字ノードを初期化します。
        /// </summary>
        /// <param name="character">このノードの文字</param>
        /// <exception cref="ArgumentException">文字ノードに使用できる文字は大文字か小文字のアルファベットに限られます。</exception>
        public CharacterNode(char character)
        {
            if (!char.IsLower(character) && !char.IsUpper(character))
                throw new ArgumentException("文字ノードに使用できる文字は大文字か小文字のアルファベットに限られます。");
            _character = character;
        }

        /// <summary>
        /// このノードの値を計算します。
        /// </summary>
        /// <param name="parameter">計算に使用するパラメータの辞書</param>
        /// <returns>計算結果</returns>
        internal override double EvaluateNode(IDictionary<char, double> parameter)
        {
            if (!parameter.ContainsKey(Character))
                throw new CharacterUnresolvedException(Character);
            return parameter[Character];
        }

        /// <summary>
        /// このノードの式を計算するのに必要な文字を取得します。
        /// </summary>
        /// <returns>このノードの式を計算するのに必要な文字のリスト。文字が必要でない場合は空のコレクションを返します。</returns>
        public override ICollection<char> GetCharactersBase()
        {
            return new char[] { Character };
        }

        /// <summary>
        /// このノードの文字列形式を取得します。
        /// </summary>
        /// <returns>このノードの文字列形式</returns>
        public override string ToString()
        {
            return "Char " + Character;
        }
    }
}
