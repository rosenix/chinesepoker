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
            results.ForEach(x => x.LoadPokers(pokers));


            Console.Write("hole pokers：");
            Console.WriteLine(string.Join("，", strategy.HolePokers.Select(x => pokers.Find(x.PokerKey)).Select(x => x.Display)));

            Console.WriteLine("=============================================");

            IUser showUser = results.First(x => x.User.UserRole == DoudizhuUser.Role.Landowner).User;
            DoudizhuUser noPokersUser = null;
            while (true)
            {
                var showList = new List<ShowCardResult>();
                while (true)
                {

                    if (showList.Count >= 3 && showList.Last(x => x.Pokers.Any()).User == showUser)
                    {
                        showList.Clear();
                        break;
                    }

                    var showResult = ShowCard(showUser, results, showList.LastOrDefault());
                    showUser = showUser.GetNextUser();
                    showList.Add(showResult);
                }

                var noPokersResult = results.FirstOrDefault(x => x.PokerKeys.Count == 0);
                if (noPokersResult != null)
                {
                    noPokersUser = noPokersResult.User;
                    break;
                }
            }

            Console.WriteLine($"{noPokersUser.UserRole.ToString()} win");

            Console.ReadLine();
        }

        private static ShowCardResult ShowCard(IUser user, List<DealResult<DoudizhuUser>> results, ShowCardResult prevShowResult = null)
        {
            var pokers = results.First(x => x.User.Id == user.Id).Pokers;
            Console.WriteLine($"{user.Display}，{string.Join(",", pokers.Select(x => $"{x.Display}({x.Key})"))}");
            Console.Write($"[{(user as DoudizhuUser).UserRole.ToString()}]{user.Display}[{pokers.Count}] select pokers:");

            var readString = Console.ReadLine();
            if (string.IsNullOrEmpty(readString))
            {
                //if (prevShowResult == null)
                //{
                //    Console.WriteLine("出牌不符合规则，请重新出！");
                //    ShowCard(user, results);
                //}

                //return new ShowCardResult
                //{
                //    RuleMatcher = new RuleMatchResult { CheckResult = true },
                //    Result = ShowCardResult.ShowCardResultType.NotGiven
                //};
                readString = "P";
            }

            if (readString.ToUpper() == "P")
            {
                if (prevShowResult != null)
                {
                    prevShowResult.Result = ShowCardResult.ShowCardResultType.Pass;
                    return prevShowResult;
                }
                else
                {
                    Console.WriteLine("头家不能PASS，请重新出！");
                    return ShowCard(user, results);
                }
            }

            var selectedPokerKeys = readString.Split(",").ToList();
            var selectedPokers = pokers.Where(x => selectedPokerKeys.Any(y => y == x.Key)).ToList();
            if (selectedPokers.Count() != selectedPokerKeys.Count)
            {
                Console.WriteLine("你恐怕出了你不存在的牌吧，请重新出！");
                return ShowCard(user, results, prevShowResult);
            }

            if (prevShowResult == null)
            {
                var ruleMatcher = RuleChecker.Check(selectedPokers);
                //验证是否正确
                if (!ruleMatcher.CheckResult)
                {
                    Console.WriteLine("出牌不符合规则，请重新出！");
                    return ShowCard(user, results);
                }

                //移除用户手中的牌
                pokers.RemoveAll(x => selectedPokerKeys.Any(y => x.Key == y));
                results.First(x => x.User.Id == user.Id).PokerKeys.RemoveAll(x => selectedPokerKeys.Contains(x.PokerKey));

                return new ShowCardResult
                {
                    RuleMatcher = ruleMatcher,
                    Pokers = selectedPokers,
                    Result = ShowCardResult.ShowCardResultType.Show,
                    User = user
                };
            }

            var currentRule = RuleHelper.BuildBoomRule(selectedPokers);
            if (currentRule == null)
                currentRule = prevShowResult.RuleMatcher.Rule.New(selectedPokers);

            if (!currentRule.Check())
            {
                Console.WriteLine("出牌不符合规则，请重新出！");
                return ShowCard(user, results, prevShowResult);
            }
            if (currentRule.CompareTo(prevShowResult.RuleMatcher.Rule) <= 0)
            {
                Console.WriteLine("出牌不符合规则，请重新出！");
                return ShowCard(user, results, prevShowResult);
            }

            //移除用户手中的牌
            pokers.RemoveAll(x => selectedPokerKeys.Any(y => x.Key == y));
            results.First(x => x.User.Id == user.Id).PokerKeys.RemoveAll(x => selectedPokerKeys.Contains(x.PokerKey));

            return new ShowCardResult
            {
                RuleMatcher = new RuleMatchResult { Rule = currentRule, CheckResult = true },
                Result = ShowCardResult.ShowCardResultType.Show,
                Pokers = selectedPokers,
                User = user
            };
        }
    }


    public class ShowCardResult
    {
        public enum ShowCardResultType
        {
            /// <summary>
            /// 出牌
            /// </summary>
            Show = 1,
            //主动点的PASS
            Pass = 2,
            //要不起
            NotGiven = 3
        }

        public RuleMatchResult RuleMatcher { get; set; }

        public IEnumerable<Poker> Pokers { get; set; }

        public ShowCardResultType Result { get; set; }

        /// <summary>
        /// 上次出牌的人
        /// </summary>
        public IUser User { get; set; }
    }
}
