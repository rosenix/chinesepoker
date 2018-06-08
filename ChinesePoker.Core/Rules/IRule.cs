using ChinesePoker.Core.Pokers;
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
        /// 验证牌是否符合规则
        /// </summary>
        /// <returns></returns>
        IRule New(IEnumerable<Poker> pokers);

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
        protected virtual int LimitNumber { get; }

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

        public abstract IRule New(IEnumerable<Poker> pokers);

        public virtual int CompareTo(IRule other)
        {
            if (other is BombCards || other is KingBombCards)
                return 1;

            return Pokers.First().Weight.CompareTo(other.Pokers.First().Weight);
        }

        public ImmutableList<Poker> Pokers { get; private set; }

        public NumberLimitRule NumberLimit { get; private set; }
    }
}
