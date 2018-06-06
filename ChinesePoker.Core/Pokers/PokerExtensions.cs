using System.Collections.Generic;
using System.Linq;

namespace ChinesePoker.Core.Pokers
{
    public static class PokerExtensions
    {
        public static Poker Find(this IEnumerable<Poker> pokers, string key)
        {
            if (pokers == null || !pokers.Any())
                return null;

            return pokers.FirstOrDefault(x => x.Key == key);
        }
    }
}
