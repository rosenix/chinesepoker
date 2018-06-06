using ChinesePoker.Core.Pokers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChinesePoker.Core.Shuffles
{
    /// <summary>
    /// 洗牌
    /// </summary>
    public class DefaultShuffleStrategy : IShuffleStrategy
    {
        private readonly static Random random = new Random();

        private readonly int _shuffleRepeatCount = 1;

        /// <summary>
        /// 执行洗牌
        /// 这个没有什么策略，就是随机打乱的方式
        /// 这里用的是虚拟方法
        /// </summary>
        /// <param name="pokers">扑克牌</param>
        public virtual List<ShuffleResult> Shuffle(List<Poker> pokers)
        {
            if (pokers == null)
                throw new ArgumentNullException(nameof(pokers));

            var pokerKeys = pokers.Select(x => x.Key).ToList();
            for (var i = 0; i < _shuffleRepeatCount; i++)
            {
                pokerKeys = DoPokerKeysOutofOrder(pokerKeys);
            }

            return pokerKeys.Select((x, i) => new ShuffleResult { Serial = i + 1, PokerKey = x }).ToList();
        }

        private List<string> DoPokerKeysOutofOrder(List<string> pokerKeys)
        {
            var outofOrderPokerKeys = new List<string>();
            while (pokerKeys.Count > 0)
            {
                var index = random.Next(0, pokerKeys.Count);
                outofOrderPokerKeys.Add(pokerKeys[index]);
                pokerKeys.RemoveAt(index);
            }
            return outofOrderPokerKeys;
        }
    }
}
