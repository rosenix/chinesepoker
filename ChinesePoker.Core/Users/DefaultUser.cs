using System.Collections.Generic;
using System.Linq;

namespace ChinesePoker.Core.Users
{
    public class DefaultUser : IUser
    {
        public DefaultUser(string name, string id, string display)
        {
            Name = name;
            Id = id;
            Display = display;
        }

        public string Name { get; private set; }

        /// <summary>
        /// 唯一的ID
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// 显示的名称
        /// </summary>
        public string Display { get; private set; }

        /// <summary>
        /// 参数
        /// </summary>
        public IDictionary<string, object> Parameters { get; private set; }

        public DefaultUser SetParameters(IDictionary<string, object> parameters)
        {
            if (Parameters == null)
                Parameters = new Dictionary<string, object>(parameters);
            else
                parameters.Select(x =>
                {
                    Parameters.Add(x);
                    return 0;
                });

            return this;
        }

        private IUser _nextUser;

        public DefaultUser SetNextUser(IUser user)
        {
            _nextUser = user;
            return this;
        }

        /// <summary>
        /// 下一家
        /// </summary>
        public IUser GetNextUser()
        {
            return _nextUser;
        }
    }
}
