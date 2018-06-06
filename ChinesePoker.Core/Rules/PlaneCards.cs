using ChinesePoker.Core.Pokers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChinesePoker.Core.Rules
{
    /// <summary>
    /// 飞机
    /// </summary>
    public class PlaneCards : IRule
    {
        public PlaneCards(IEnumerable<Poker> pokers)
        {
            if (pokers == null || !pokers.Any())
                throw new ArgumentNullException(nameof(pokers));

            Pokers = pokers.ToImmutableList();
        }

        public bool Check()
        {
            if (!NumberLimitRule.Check(NumberLimit, Pokers.Count))
                return false;

            //不能包含大小王和2

            //不能有相同
            for (var i = 1; i < Pokers.Count; i++)
            {
                if (Pokers[i].Weight == Pokers.First().Weight)
                    return false;
            }

            //连续的，这里是最大的减去最小的等于一个值
            return Pokers.Max(x => x.Weight) - Pokers.Min(x => x.Weight) == Pokers.Count - 1;
        }

        private int FindMaxSamePokerWeight(IEnumerable<Poker> Pokers)
        {
            var pokerKeys = Pokers.Select(x => x.Display).Distinct().Where(x => Pokers.Count(y => y.Display == x) == 3);
            return Pokers.Where(x => pokerKeys.Contains(x.Display)).Max(x => x.Weight);
        }

        public int CompareTo(IRule other)
        {
            return FindMaxSamePokerWeight(Pokers).CompareTo(FindMaxSamePokerWeight(other.Pokers));
        }

        /// <summary>
        /// 牌
        /// </summary>
        public ImmutableList<Poker> Pokers { get; private set; }

        /// <summary>
        /// 牌数量规则
        /// </summary>
        public NumberLimitRule NumberLimit { get; private set; } = new NumberLimitRule { Count = 7, Type = NumberLimitType.GreaterThan };

        public string Description { get; } = "飞机";
    }
}
