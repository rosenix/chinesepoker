using ChinesePoker.Core.Pokers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChinesePoker.Core.Rules
{
    /// <summary>
    /// 三带一
    /// </summary>
    public class ThreeViaOneCards : IRule
    {
        public ThreeViaOneCards(IEnumerable<Poker> pokers)
        {
            if (pokers == null || !pokers.Any())
                throw new ArgumentNullException(nameof(pokers));

            Pokers = pokers.ToImmutableList();
        }

        public bool Check()
        {
            if (!NumberLimitRule.Check(NumberLimit, Pokers.Count))
                return false;

            var pokerKeys = Pokers.Select(x => x.Display).Distinct();
            if (pokerKeys.Count() != 2)
                return false;

            return Pokers.Count(x => x.Display == pokerKeys.First()) == 3 || Pokers.Count(x => x.Display == pokerKeys.Last()) == 3;
        }

        public IRule New(IEnumerable<Poker> pokers)
        {
            return new ThreeViaOneCards(pokers);
        }

        private Poker FindThreeSamePoker(IEnumerable<Poker> Pokers)
        {
            var pokerKeys = Pokers.Select(x => x.Display).Distinct();
            if (Pokers.Count(x => x.Display == pokerKeys.First()) == 3)
                return Pokers.First(x => x.Display == pokerKeys.First());

            return Pokers.First(x => x.Display == pokerKeys.Last());
        }

        public int CompareTo(IRule other)
        {
            if (other is BombCards || other is KingBombCards)
                return 1;

            return FindThreeSamePoker(Pokers).Weight.CompareTo(FindThreeSamePoker(other.Pokers).Weight);
        }

        /// <summary>
        /// 牌
        /// </summary>
        public ImmutableList<Poker> Pokers { get; private set; }

        /// <summary>
        /// 牌数量规则
        /// </summary>
        public NumberLimitRule NumberLimit { get; private set; } = new NumberLimitRule { Count = 4, Type = NumberLimitType.Equal };

        public string Description { get; } = "三带一";
    }
}
