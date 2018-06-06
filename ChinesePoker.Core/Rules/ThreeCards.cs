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
            LimitNumber = 3;
        }

        public override string Description { get; } = "三个";
    }
}

