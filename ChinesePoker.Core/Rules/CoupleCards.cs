using ChinesePoker.Core.Pokers;
using System.Collections.Generic;

namespace ChinesePoker.Core.Rules
{
    /// <summary>
    /// 一对
    /// </summary>
    public class CoupleCards : SameNumberCards, IRule
    {
        public CoupleCards(IEnumerable<Poker> pokers) : base(pokers)
        {
            LimitNumber = 2;
        }

        public override string Description { get; } = "一对";

        public override bool Check()
        {
            if (!base.Check())
                return false;

            //不能等于对王
            

            return true;
        }
    }
}
