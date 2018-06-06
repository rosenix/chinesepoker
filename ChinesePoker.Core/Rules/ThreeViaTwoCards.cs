using ChinesePoker.Core.Pokers;
using System.Collections.Generic;

namespace ChinesePoker.Core.Rules
{
    /// <summary>
    /// 三带二
    /// </summary>
    public class ThreeViaTwoCards : ThreeViaOneCards, IRule
    {
        public ThreeViaTwoCards(IEnumerable<Poker> pokers) : base(pokers)
        {
        }
        /// <summary>
        /// 牌数量规则
        /// </summary>
        public new NumberLimitRule NumberLimit { get; private set; } = new NumberLimitRule { Count = 5, Type = NumberLimitType.Equal };

        public new string Description { get; } = "三带二";
    }
}
