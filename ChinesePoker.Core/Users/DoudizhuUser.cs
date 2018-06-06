namespace ChinesePoker.Core.Users
{
    public class DoudizhuUser : DefaultUser
    {
        public enum Role
        {
            None,
            Landowner,
            Farmer
        }

        public DoudizhuUser(string name, string id, string display) : base(name, id, display)
        {
        }

        public Role UserRole { get; private set; }

        public DoudizhuUser SetRole(Role userRole)
        {
            UserRole = userRole;
            return this;
        }
    }
}
