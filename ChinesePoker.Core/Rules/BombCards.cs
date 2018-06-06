using ChinesePoker.Core.Pokers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChinesePoker.Core.Rules
{
    /// <summary>
    /// 炸弹
    /// </summary>
    public class BombCards : SameNumberCards, IRule
    {
        public BombCards(IEnumerable<Poker> pokers) : base(pokers)
        {
            LimitNumber = 4;
        }

        public override string Description { get; } = "炸弹";

        public override IRule New(IEnumerable<Poker> pokers)
        {
            return new BombCards(pokers);
        }
    }

    /// <summary>
    /// 王炸
    /// </summary>
    public class KingBombCards : IRule
    {
        private readonly Poker _bking;
        private readonly Poker _pking;

        public KingBombCards(Poker bking, Poker pking)
        {
            _bking = bking ?? throw new ArgumentNullException(nameof(bking));
            _pking = pking ?? throw new ArgumentNullException(nameof(pking));

            Pokers = new Poker[] { _bking, _pking }.ToImmutableList();
        }

        public bool Check()
        {
            return _bking.Display == PokerConstants.Poker_BJ.Display && _pking.Display == PokerConstants.Poker_LJ.Display;
        }

        public IRule New(IEnumerable<Poker> pokers)
        {
            return new KingBombCards(pokers.First(), pokers.Last());
        }

        public int CompareTo(IRule other)
        {
            return -1;
        }

        /// <summary>
        /// 牌
        /// </summary>
        public ImmutableList<Poker> Pokers { get; private set; }

        /// <summary>
        /// 牌数量规则
        /// </summary>
        public NumberLimitRule NumberLimit { get; private set; } = new NumberLimitRule { Count = 2, Type = NumberLimitType.Equal };

        public string Description { get; } = "王炸";
    }
}
