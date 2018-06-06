using ChinesePoker.Core.Deals;
using ChinesePoker.Core.Pokers;
using ChinesePoker.Core.Rules;
using ChinesePoker.Core.Shuffles;
using ChinesePoker.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChinesePoker.Core.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var pokers = PokerContainer.GetPokers();
            var user1 = new DoudizhuUser("郭大为", "1", "郭大为");
            var user2 = new DoudizhuUser("郭炫臻", "2", "郭炫臻");
            var user3 = new DoudizhuUser("黄莉", "3", "黄莉");

            user1.SetNextUser(user2);
            user2.SetNextUser(user3);
            user3.SetNextUser(user1);


            var pokerKeys = new DefaultShuffleStrategy().Shuffle(pokers);

            var deal = new Deal(pokerKeys);

            deal.Cut();
            var strategy = new DoudizhuDealStategy(new[] { user1, user2, user3 });
            var results = deal.Do(strategy);

            foreach (var result in results)
            {
                var currentPokers = result.PokerKeys.Select(x => pokers.Find(x.PokerKey)).ToList();
                currentPokers.Sort((x, y) => x.Weight.CompareTo(y.Weight));
                Console.Write($"[{result.User.UserRole}]{result.User.Display}：");
                Console.WriteLine(string.Join(",", currentPokers.Select(x => x.Display)));
            }



            Console.Write("hole pokers：");
            Console.WriteLine(string.Join("，", strategy.HolePokers.Select(x => pokers.Find(x.PokerKey)).Select(x => x.Display)));

            Console.WriteLine("=============================================");

            var chupaiUser = results.First(x => x.User.UserRole == DoudizhuUser.Role.Landowner).User;
            DoudizhuUser noPokersUser = null;
            while (true)
            {
                while (true)
                {
                    Chupai(chupaiUser, pokers, results);
                    break;
                }
                var noPokersResult = results.FirstOrDefault(x => x.PokerKeys.Count == 0);
                if (noPokersUser != null)
                {
                    noPokersUser = noPokersResult.User;
                    break;
                }
            }

            Console.WriteLine($"{noPokersUser.UserRole.ToString()} win");

            Console.ReadLine();
        }

        private static void Chupai(DoudizhuUser user, List<Poker> pokers, List<DealResult<DoudizhuUser>> results)
        {
            var pokerKeys = results.First(x => x.User == user).PokerKeys;
            var currentPokers = pokerKeys.Select(x => pokers.Find(x.PokerKey)).ToList();
            currentPokers.Sort((x, y) => x.Weight.CompareTo(y.Weight));

            Console.WriteLine($"{user.Display}，{string.Join(",", currentPokers.Select(x => $"{x.Display}({x.Key})"))}");
            Console.Write($"{user.Display} select pokers:");
            var readString = Console.ReadLine();
            if (string.IsNullOrEmpty(readString)) { }
            else
            {
                if (readString == "P") { }
                else
                {
                    var selectedPokerKeys = readString.Split(",");
                    var selectPokers = pokers.Where(x => selectedPokerKeys.Any(y => y == x.Key));
                    if (selectPokers.Count() != selectedPokerKeys.Length)
                    {
                        Console.WriteLine("你恐怕出了你不存在的牌吧，请重新出！");
                        Chupai(user, pokers, results);
                    }
                    //验证是否正确
                    if (!RuleChecker.Check(selectPokers))
                    {
                        Console.WriteLine("出牌不符合规则，请重新出！");
                        Chupai(user, pokers, results);
                    }
                }
            }
            Console.WriteLine(readString);
        }
    }

}
