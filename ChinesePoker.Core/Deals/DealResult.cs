using ChinesePoker.Core.Shuffles;
using ChinesePoker.Core.Users;
using System.Collections.Generic;

namespace ChinesePoker.Core.Deals
{
    public class DealResult<T> where T : IUser
    {
        public T User { get; set; }

        public List<ShuffleResult> PokerKeys { get; set; }
    }
}
