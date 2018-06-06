using ChinesePoker.Core.Shuffles;
using ChinesePoker.Core.Users;
using System;
using System.Collections.Generic;

namespace ChinesePoker.Core.Deals
{
    /// <summary>
    /// 现在是三人斗地主，标准的
    /// </summary>
    public class DoudizhuDealStategy : IDealStrategy<DoudizhuUser>
    {
        protected readonly static Random random = new Random();

        public DoudizhuDealStategy(IEnumerable<DoudizhuUser> users)
        {
            Users.AddRange(users);
            HolePokers = new List<ShuffleResult>(HolePokerCount);
            SetDealRuleItems();
        }

        protected int HolePokerCount { get; set; } = 3;

        /// <summary>
        /// 保留的底牌，这里默认是三张
        /// </summary>
        public List<ShuffleResult> HolePokers { get; private set; }

        /// <summary>
        /// 地主标记牌
        /// </summary>
        public string DizhuMarkPokerKey;

        /// <summary>
        /// 参与人数
        /// </summary>
        public List<DoudizhuUser> Users { get; set; } = new List<DoudizhuUser>();

        /// <summary>
        /// 发牌规则
        /// </summary>
        public IDictionary<int, int> DealRule { get; } = new Dictionary<int, int>();

        /// <summary>
        /// 设置发牌规则项目
        /// 这里总共发六次，前五次每次三张，最后一次两张
        /// </summary>
        protected virtual void SetDealRuleItems()
        {
            for (var i = 1; i <= 5; i++)
                DealRule.Add(i, 3);

            DealRule.Add(6, 2);
        }

        /// <summary>
        /// 执行发牌的动作
        /// </summary>
        public virtual void DealingOfPerDeal(int seral, DealResult<DoudizhuUser> result)
        {
            //todo
        }

        /// <summary>
        /// 执行发牌的动作
        /// </summary>
        public virtual void DealedOfPerDeal(int seral, DealResult<DoudizhuUser> result)
        {
            //todo
        }

        /// <summary>
        /// 执行发牌的动作之前的操作，这里提前抽取三张牌
        /// </summary>
        public virtual void Dealing(List<ShuffleResult> pokerKeys)
        {
            SelectDizhuMarkPoker(pokerKeys);
            SelectHolePokers(pokerKeys);
        }

        /// <summary>
        /// 选择地主标记牌
        /// </summary>
        protected virtual void SelectDizhuMarkPoker(List<ShuffleResult> pokerKeys)
        {
            var index = random.Next(HolePokerCount, pokerKeys.Count);
            DizhuMarkPokerKey = pokerKeys[index].PokerKey;
        }

        protected virtual void SelectHolePokers(List<ShuffleResult> pokerKeys)
        {
            //抽取最上面的三张（或许还有随机抽三张的算法）
            HolePokers.AddRange(pokerKeys.GetRange(0, HolePokerCount));
            pokerKeys.RemoveRange(0, HolePokerCount);
        }

        /// <summary>
        /// 执行发牌的操作的动作
        /// </summary>
        public virtual void Dealed(List<ShuffleResult> pokerKeys, List<DealResult<DoudizhuUser>> results)
        {
            //临时设置为获取到标记牌的为地主
            results.ForEach(x =>
            {
                if (x.PokerKeys.Exists(y => y.PokerKey == DizhuMarkPokerKey))
                {
                    x.User.SetRole(DoudizhuUser.Role.Landowner);
                    x.PokerKeys.AddRange(HolePokers);
                }
                else
                {
                    x.User.SetRole(DoudizhuUser.Role.Farmer);
                }
            });
        }
    }
}
