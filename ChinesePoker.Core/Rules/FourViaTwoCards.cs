using ChinesePoker.Core.Pokers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChinesePoker.Core.Rules
{
    /// <summary>
    /// 四带二
    /// </summary>
    public class FourViaTwoCards : IRule
    {
        public FourViaTwoCards(IEnumerable<Poker> pokers)
        {
            if (pokers == null || !pokers.Any())
                throw new ArgumentNullException(nameof(pokers));

            Pokers = pokers.ToImmutableList();
        }

        public bool Check()
        {
            if (!NumberLimitRule.Check(NumberLimit, Pokers.Count))
                return false;

            //是否可以带两个王


            var pokerKeys = Pokers.Select(x => x.Display).Distinct();
            if (pokerKeys.Count() != 2)
                return false;

            return Pokers.Count(x => x.Display == pokerKeys.First()) == 4 || Pokers.Count(x => x.Display == pokerKeys.Last()) == 4;
        }

        private Poker FindFourSamePoker(IEnumerable<Poker> Pokers)
        {
            var pokerKeys = Pokers.Select(x => x.Display).Distinct();
            if (Pokers.Count(x => x.Display == pokerKeys.First()) == 4)
                return Pokers.First(x => x.Display == pokerKeys.First());

            return Pokers.First(x => x.Display == pokerKeys.Last());
        }

        public int CompareTo(IRule other)
        {
            return FindFourSamePoker(Pokers).Weight.CompareTo(FindFourSamePoker(other.Pokers).Weight);
        }

        /// <summary>
        /// 牌
        /// </summary>
        public ImmutableList<Poker> Pokers { get; private set; }

        /// <summary>
        /// 牌数量规则
        /// </summary>
        public NumberLimitRule NumberLimit { get; private set; } = new NumberLimitRule { Count = 6, Type = NumberLimitType.Equal };

        public string Description { get; } = "四带二";

    }
}
