using ChinesePoker.Core.Pokers;
using System.Collections.Generic;
using System.Linq;

namespace ChinesePoker.Core.Rules
{
    public class RuleHelper
    {
        public static bool IsBoomRule(IRule rule)
        {
            return rule == null ? false : (rule is BombCards || rule is KingBombCards);
        }

        public static bool IsBoomRule(IEnumerable<Poker> pokers)
        {
            if (pokers == null || pokers.Count() == 0)
                return false;

            if (pokers.Count() == 2)
                return IsKingCouple(pokers);
            else if (pokers.Count() == 4)
                return pokers.All(x => x.Display == pokers.First().Display);
            return false;
        }

        public static IRule BuildBoomRule(IEnumerable<Poker> pokers)
        {
            if (!IsBoomRule(pokers))
                return null;

            if (IsKingCouple(pokers))
                return new KingBombCards(pokers);

            return new BombCards(pokers);
        }

        public static bool IsKingCouple(IEnumerable<Poker> pokers)
        {
            if (pokers == null || pokers.Count() != 2)
                return false;

            var tempPokers = new List<Poker>(pokers);
            tempPokers.Sort((x, y) => x.Weight.CompareTo(y.Weight));
            return tempPokers.First().Key == PokerConstants.Poker_LJ.Key && tempPokers.Last().Key == PokerConstants.Poker_LJ.Key;
        }
    }
}
