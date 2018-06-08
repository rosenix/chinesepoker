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
        }

        protected override int LimitNumber { get; } = 1;

        public override IRule New(IEnumerable<Poker> pokers)
        {            
            return new SingleCard(pokers.First());
        }

        public Poker Current { get; }

        public override string Description { get; } = "单牌";
    }
}
