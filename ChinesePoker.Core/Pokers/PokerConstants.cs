using System.Collections.Generic;

namespace ChinesePoker.Core.Pokers
{
    public class PokerConstants
    {
        public static readonly List<PokerColor> Colors = new List<PokerColor> {
             PokerColor.Spade,
             PokerColor.Heart,
             PokerColor.Club,
             PokerColor.Diamond
        };

        public static readonly PokerColors Poker_3 = new PokerColors(Colors) { Display = "3", Weight = 3 };
        public static readonly PokerColors Poker_4 = new PokerColors(Colors) { Display = "4", Weight = 4 };
        public static readonly PokerColors Poker_5 = new PokerColors(Colors) { Display = "5", Weight = 5 };
        public static readonly PokerColors Poker_6 = new PokerColors(Colors) { Display = "6", Weight = 6 };
        public static readonly PokerColors Poker_7 = new PokerColors(Colors) { Display = "7", Weight = 7 };
        public static readonly PokerColors Poker_8 = new PokerColors(Colors) { Display = "8", Weight = 8 };
        public static readonly PokerColors Poker_9 = new PokerColors(Colors) { Display = "9", Weight = 9 };
        public static readonly PokerColors Poker_10 = new PokerColors(Colors) { Display = "10", Weight = 10 };
        public static readonly PokerColors Poker_J = new PokerColors(Colors) { Display = "J", Weight = 11 };
        public static readonly PokerColors Poker_Q = new PokerColors(Colors) { Display = "Q", Weight = 12 };
        public static readonly PokerColors Poker_K = new PokerColors(Colors) { Display = "K", Weight = 13 };
        public static readonly PokerColors Poker_A = new PokerColors(Colors) { Display = "A", Weight = 14 };
        public static readonly PokerColors Poker_2 = new PokerColors(Colors) { Display = "2", Weight = 15 };
        public static readonly PokerColors Poker_LJ = new PokerColors(null) { Display = "小王", Weight = 16 };
        public static readonly PokerColors Poker_BJ = new PokerColors(null) { Display = "大王", Weight = 17 };


        public static readonly List<PokerColors> Numbers = new List<PokerColors> {
            Poker_3,
            Poker_4,
            Poker_5,
            Poker_6,
            Poker_7,
            Poker_8,
            Poker_9,
            Poker_10,
            Poker_J,
            Poker_Q,
            Poker_K,
            Poker_A,
            Poker_2,
            Poker_LJ,
            Poker_BJ
        };
    }
}
