using ChinesePoker.Core.Pokers;
using System.Collections.Generic;
using System.Linq;

namespace ChinesePoker.Core.Rules
{
    /// <summary>
    /// 单牌
    /// </summary>
    public class SingleCard : SameNumberCards, IRule
    {
        public SingleCard(Poker poker) : this(new List<Poker> { poker })
        {
        }

        public SingleCard(List<Poker> pokers) : base(pokers)
        {
            Current = pokers.First();
            LimitNumber = 1;
        }

        public Poker Current { get; }

        public override string Description { get; } = "单牌";
    }
}
