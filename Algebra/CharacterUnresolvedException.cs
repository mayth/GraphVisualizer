using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algebra
{
    /// <summary>
    /// 式中の文字が解決できなかった際にスローされる例外。
    /// </summary>
    [Serializable]
    public class CharacterUnresolvedException : Exception
    {
        /// <summary>
        /// 解決できなかった文字を取得します。
        /// </summary>
        public char UnresolvedCharacter { get; private set; }

        /// <summary>
        /// 解決できなかった文字を指定して<see cref="CharacterUnresolvedException"/>クラスを初期化します。
        /// </summary>
        /// <param name="character">解決できなかった文字</param>
        public CharacterUnresolvedException(char character)
            : base("Character unresolved: '" + character.ToString() + "'")
        {
            UnresolvedCharacter = character;
        }

        /// <summary>
        /// 解決できなかった文字と例外を通知するメッセージを指定して<see cref="CharacterUnresolvedException"/>クラスを初期化します。
        /// </summary>
        /// <param name="character">解決できなかった文字</param>
        /// <param name="message">例外を通知するメッセージ</param>
        public CharacterUnresolvedException(char character, string message)
            : base(message)
        {
            UnresolvedCharacter = character;
        }

        /// <summary>
        /// <see cref="CharacterUnresolvedException"/>クラスを初期化します。
        /// </summary>
        /// <param name="info">シリアライズ情報</param>
        /// <param name="context">コンテキスト</param>
        protected CharacterUnresolvedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
