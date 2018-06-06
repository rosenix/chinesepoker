using ChinesePoker.Core.Pokers;
using System.Collections.Generic;

namespace ChinesePoker.Core.Shuffles
{
    public interface IShuffleStrategy
    {
        /// <summary>
        /// 执行洗牌
        /// </summary>
        /// <param name="pokers">扑克牌</param>
        List<ShuffleResult> Shuffle(List<Poker> pokers);
    }
}
