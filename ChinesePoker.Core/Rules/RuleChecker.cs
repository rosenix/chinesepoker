using ChinesePoker.Core.Pokers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChinesePoker.Core.Rules
{
    public class RuleChecker
    {
        public static bool Check(IEnumerable<Poker> pokers)
        {
            if (pokers == null || !pokers.Any())
                throw new ArgumentException(nameof(pokers));

            if (pokers.Count() == 1)
                return true;

            var rules = new List<IRule>();
            //大于等于5
            if (pokers.Count() >= 5)
            {
                rules.Add(new StraightCards(pokers));
                rules.Add(new PlaneCards(pokers));
                rules.Add(new ContinuousCoupleCards(pokers));
                rules.Add(new FourViaTwoCards(pokers));
                rules.Add(new ThreeViaTwoCards(pokers));
            }
            else if (pokers.Count() == 4)
            {
                rules.Add(new ThreeViaOneCards(pokers));
                rules.Add(new BombCards(pokers));
            }
            else if (pokers.Count() == 3)
            {
                rules.Add(new ThreeCards(pokers));
            }
            else if (pokers.Count() == 2)
            {
                rules.Add(new CoupleCards(pokers));
                rules.Add(new KingBombCards(pokers.FirstOrDefault(x => x.Display == PokerConstants.Poker_BJ.Display) ?? Poker.Empty,
                    pokers.FirstOrDefault(x => x.Display == PokerConstants.Poker_LJ.Display) ?? Poker.Empty));
            }

            foreach (var rule in rules)
            {
                if (rule.Check())
                    return true;
            }

            return false;
        }
    }
}
