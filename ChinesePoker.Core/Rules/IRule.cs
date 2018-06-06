﻿using ChinesePoker.Core.Pokers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChinesePoker.Core.Rules
{
    public interface IRule : IComparable<IRule>
    {
        string Description { get; }

        /// <summary>
        /// 验证牌是否符合规则
        /// </summary>
        /// <returns></returns>
        bool Check();

        /// <summary>
        /// 牌
        /// </summary>
        ImmutableList<Poker> Pokers { get; }

        /// <summary>
        /// 牌数量规则
        /// </summary>
        NumberLimitRule NumberLimit { get; }
    }

    public abstract class SameNumberCards : IComparable<IRule>
    {
        protected int LimitNumber { get; set; }

        public SameNumberCards(IEnumerable<Poker> pokers)
        {
            if (pokers == null || !pokers.Any())
                throw new ArgumentNullException(nameof(pokers));

            Pokers = pokers.ToImmutableList();
            NumberLimit = new NumberLimitRule { Count = LimitNumber, Type = NumberLimitType.Equal };
        }

        public abstract string Description { get; } 

        public virtual bool Check()
        {
            if (!NumberLimitRule.Check(NumberLimit, Pokers.Count))
                return false;

            if (!Pokers.All(x => x.Display == Pokers.First().Display))
                return false;

            return true;
        }

        public int CompareTo(IRule other)
        {
            return other.Pokers.First().Weight.CompareTo(other.Pokers.First().Weight);
        }

        public ImmutableList<Poker> Pokers { get; private set; }

        public NumberLimitRule NumberLimit { get; private set; }
    }
}
