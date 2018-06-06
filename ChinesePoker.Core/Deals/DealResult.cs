using ChinesePoker.Core.Pokers;
using ChinesePoker.Core.Shuffles;
using ChinesePoker.Core.Users;
using System.Collections.Generic;
using System.Linq;

namespace ChinesePoker.Core.Deals
{
    public class DealResult<T> where T : IUser
    {
        public T User { get; set; }

        public List<ShuffleResult> PokerKeys { get; set; }

        public List<Poker> Pokers { get; } = new List<Poker>();

        public DealResult<T> LoadPokers(IEnumerable<Poker> pokers)
        {
            var selectedPokers = pokers.Where(x => PokerKeys.Exists(y => y.PokerKey == x.Key));
            //按照weight从大到小排序，相同的weight按照黑、红、樱、方排序
            var dictionary = new SortedDictionary<int, List<Poker>>();
            foreach (var poker in selectedPokers)
            {
                var key = 0 - poker.Weight;
                if (dictionary.ContainsKey(key))
                    dictionary[key].Add(poker);
                else
                    dictionary.Add(key, new List<Poker> { poker });
            }


            foreach (var item in dictionary)
            {
                item.Value.Sort((x, y) =>
                {
                    if (x.Color.HasValue && y.Color.HasValue)
                        return x.Color.Value.CompareTo(y.Color.Value);

                    return 0;
                });
            }

            Pokers.AddRange(dictionary.Values.SelectMany(x => x));

            return this;
        }
    }
}
