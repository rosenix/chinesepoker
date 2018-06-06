using ChinesePoker.Core.Shuffles;
using ChinesePoker.Core.Users;
using System.Collections.Generic;

namespace ChinesePoker.Core.Deals
{
    /// <summary>
    /// 发牌策略
    /// </summary>
    public interface IDealStrategy<T> where T : IUser
    {
        /// <summary>
        /// 参与人数
        /// </summary>
        List<T> Users { get; set; }

        /// <summary>
        /// 发牌的规则，字典里面的Key代表第几次发，Value代表发几张
        /// </summary>
        IDictionary<int, int> DealRule { get; }

        /// <summary>
        /// 执行发牌的动作
        /// </summary>
        void DealingOfPerDeal(int seral, DealResult<T> result);

        /// <summary>
        /// 执行发牌的动作
        /// </summary>
        void DealedOfPerDeal(int seral, DealResult<T> result);

        /// <summary>
        /// 执行发牌的动作前的操作，如可以抽取一些牌来备用
        /// </summary>
        void Dealing(List<ShuffleResult> pokerKeys);

        /// <summary>
        /// 执行发牌的动作后的操作，如可以抽取一些牌来备用
        /// </summary>
        void Dealed(List<ShuffleResult> pokerKeys, List<DealResult<T>> results);
    }
}
