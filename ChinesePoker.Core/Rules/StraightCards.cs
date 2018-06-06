using ChinesePoker.Core.Pokers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChinesePoker.Core.Rules
{
    /// <summary>
    /// 顺子
    /// </summary>
    public class StraightCards : IRule
    {
        public StraightCards(IEnumerable<Poker> pokers)
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

        public int CompareTo(IRule other)
        {
            return Pokers.Max(x => x.Weight).CompareTo(other.Pokers.Max(x => x.Weight));
        }

        /// <summary>
        /// 牌
        /// </summary>
        public ImmutableList<Poker> Pokers { get; private set; }

        /// <summary>
        /// 牌数量规则
        /// </summary>
        public NumberLimitRule NumberLimit { get; private set; } = new NumberLimitRule { Count = 4, Type = NumberLimitType.GreaterThan };

        public string Description { get; } = "顺子";
    }
}
