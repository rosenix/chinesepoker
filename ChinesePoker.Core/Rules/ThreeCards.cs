using ChinesePoker.Core.Pokers;
using System.Collections.Generic;

namespace ChinesePoker.Core.Rules
{
    /// <summary>
    /// 三张
    /// </summary>
    public class ThreeCards : SameNumberCards, IRule
    {
        public ThreeCards(IEnumerable<Poker> pokers) : base(pokers)
        {
        }

        protected override int LimitNumber { get; } = 3;

        public override IRule New(IEnumerable<Poker> pokers)
        {
            return new ThreeCards(pokers);
        }

        public override string Description { get; } = "三个";
    }
}

