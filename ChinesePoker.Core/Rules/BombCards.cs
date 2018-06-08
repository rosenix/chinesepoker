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
        }

        protected override int LimitNumber { get; } = 4;

        public override string Description { get; } = "炸弹";

        public override int CompareTo(IRule other)
        {
            if (other is KingBombCards)
                return 1;

            return Pokers.First().Weight.CompareTo(other.Pokers.First().Weight);
        }

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
        public KingBombCards(IEnumerable<Poker> pokers)
        {
            if (pokers == null)
                throw new ArgumentNullException(nameof(pokers));

            Pokers = pokers.ToImmutableList();
        }

        public bool Check() => RuleHelper.IsKingCouple(Pokers);

        public IRule New(IEnumerable<Poker> pokers)
        {
            return new KingBombCards(pokers);
        }

        public int CompareTo(IRule other)
        {
            return 1;
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
