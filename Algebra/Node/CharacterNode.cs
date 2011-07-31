using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    class CharacterNode : Node
    {
        private char _character;

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

        internal override double EvaluateNode(IDictionary<char, double> parameter)
        {
            if (!parameter.ContainsKey(Character))
                throw new CharacterUnresolvedException(Character);
            return parameter[Character];
        }

        public override ICollection<char> GetCharactersBase()
        {
            return new char[] { Character };
        }

        public override string ToString()
        {
            return "Char " + Character;
        }
    }
}
