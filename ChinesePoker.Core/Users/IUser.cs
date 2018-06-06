using System.Collections.Generic;

namespace ChinesePoker.Core.Users
{
    public interface IUser
    {
        string Name { get; }

        /// <summary>
        /// 唯一的ID
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 显示的名称
        /// </summary>
        string Display { get; }

        /// <summary>
        /// 参数
        /// </summary>
        IDictionary<string, object> Parameters { get; }

        /// <summary>
        /// 下一家
        /// </summary>
        IUser GetNextUser();
    }
}
