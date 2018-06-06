using System;
using System.Collections.Generic;
using System.Linq;

namespace ChinesePoker.Core.Pokers
{
    public class PokerContainer
    {
        private static readonly Lazy<List<Poker>> Pokers = new Lazy<List<Poker>>(() =>
        {
            //利用反射
            var pokers = PokerConstants.Numbers.SelectMany(x =>
            {
                if (x.Colors.Any())
                    return x.Colors.Select(y => ((Poker)x.Clone()).SetColor(y));

                return new[] { (Poker)x.Clone() };
            });

            return pokers.ToList();
        });

        public static List<Poker> GetPokers() => Pokers.Value;
    }
}
