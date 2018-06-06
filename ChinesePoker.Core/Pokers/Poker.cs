using System;
using System.Collections.Generic;

namespace ChinesePoker.Core.Pokers
{
    public class Poker
    {
        public static Poker Empty { get; } = new Poker();

        /// <summary>
        /// 权重值，数字越大代表权重值越大
        /// </summary>
        public int Weight { get; internal set; }

        /// <summary>
        /// 花色
        /// </summary>
        public PokerColor? Color { get; internal set; }

        internal Poker SetColor(PokerColor color)
        {
            Color = color;
            return this;
        }

        /// <summary>
        /// 显示的内容，如K,10
        /// </summary>
        public string Display { get; internal set; }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Key { get { return $"{Display}{(Color.HasValue ? (int)Color.Value : 0)}"; } }
    }

    public class PokerColors : Poker, ICloneable
    {
        internal List<PokerColor> Colors;

        internal PokerColors(List<PokerColor> colors)
        {
            Colors = colors ?? new List<PokerColor>();
        }

        public object Clone()
        {
            return new Poker
            {
                Weight = Weight,
                Color = Color,
                Display = Display
            };
        }
    }
}
