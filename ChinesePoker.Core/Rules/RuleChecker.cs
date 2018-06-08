using ChinesePoker.Core.Pokers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChinesePoker.Core.Rules
{
    public class RuleMatchResult
    {
        public bool CheckResult { get; set; }

        public IRule Rule { get; set; }
    }

    public class RuleChecker
    {
        public static RuleMatchResult Check(IEnumerable<Poker> pokers)
        {
            if (pokers == null || !pokers.Any())
                throw new ArgumentException(nameof(pokers));

            if (pokers.Count() == 1)
            {
                return new RuleMatchResult
                {
                    CheckResult = true,
                    Rule = new SingleCard(pokers.First())
                };
            }

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
                rules.Add(new KingBombCards(pokers));
            }

            foreach (var rule in rules)
            {
                if (rule.Check())
                {
                    return new RuleMatchResult
                    {
                        CheckResult = true,
                        Rule = rule
                    };
                }
            }

            return new RuleMatchResult { CheckResult = false };
        }
    }
}
