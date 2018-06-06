using System;

namespace ChinesePoker.Core.Rules
{
    /// <summary>
    /// 出牌的数量限制规则
    /// </summary>
    public class NumberLimitRule
    {
        public int Count { get; set; }

        public NumberLimitType Type { get; set; }

        public static bool Check(NumberLimitRule rule, int count)
        {
            if (rule == null)
                throw new ArgumentNullException(nameof(rule));

            if (count <= 0 || rule.Count < 0)
                throw new ArgumentException("参数错误");

            if (rule.Type == NumberLimitType.Equal)
                return rule.Count == count;
            else if (rule.Type == NumberLimitType.GreaterThan)
                return count > rule.Count;
            else
                return count < rule.Count;
        }
    }

    public enum NumberLimitType
    {
        Equal,
        GreaterThan,
        LessThan
    }


}
