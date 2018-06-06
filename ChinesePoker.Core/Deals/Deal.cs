using ChinesePoker.Core.Shuffles;
using ChinesePoker.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChinesePoker.Core.Deals
{
    public class Deal
    {
        private readonly static Random random = new Random();

        public List<ShuffleResult> PokerKeys { get; private set; }

        public Deal(List<ShuffleResult> pokerKeys)
        {
            PokerKeys = pokerKeys;
        }

        /// <summary>
        /// 切牌，即改变牌的顺序
        /// </summary>
        /// <param name="index">从第几张切</param>
        /// <returns></returns>
        public string Cut(int index = -1)
        {
            if (index == -1)
            {
                index = random.Next(0, PokerKeys.Count);
            }

            var selectedPokerKey = PokerKeys[index];
            if (index != PokerKeys.Count - 1)
            {
                var afterCutPokerKeys = new List<ShuffleResult>();
                afterCutPokerKeys.AddRange(PokerKeys.Where(x => x.Serial > index + 1));
                afterCutPokerKeys.AddRange(PokerKeys.Where(x => x.Serial <= index + 1));

                //重新编排序号
                PokerKeys = afterCutPokerKeys.Select((x, i) =>
                {
                    x.Serial = i + 1;
                    return x;
                }).ToList();
            }
            return selectedPokerKey.PokerKey;
        }

        /// <summary>
        /// 执行发牌
        /// </summary>
        public List<DealResult<T>> Do<T>(IDealStrategy<T> strategy) where T : IUser
        {
            var results = strategy.Users.Select(x => new DealResult<T>
            {
                User = x,
                PokerKeys = new List<ShuffleResult>()
            }).ToList();

            //比方这里可以提前抽取几张
            strategy.Dealing(PokerKeys);
            foreach (var ruleItem in strategy.DealRule)
            {
                DoDeal(ruleItem, strategy, results);
            }
            strategy.Dealed(PokerKeys, results);
            return results;
        }

        private void DoDeal<T>(KeyValuePair<int, int> ruleItem, IDealStrategy<T> strategy, List<DealResult<T>> results) where T : IUser
        {
            foreach (var user in strategy.Users)
            {
                var result = results.First(x => x.User.Id == user.Id);
                strategy.DealingOfPerDeal(ruleItem.Key, result);
                //个对应的人发牌，如果剩余的牌不足以达到NumberOfPerDeal这个数

                result.PokerKeys.AddRange(PokerKeys.GetRange(0, ruleItem.Value));
                PokerKeys.RemoveRange(0, ruleItem.Value);

                strategy.DealedOfPerDeal(ruleItem.Key, result);
            }
        }
    }
}
