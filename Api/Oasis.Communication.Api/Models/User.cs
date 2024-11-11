namespace Oasis.Communication.Api.Models
{
    public class User
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
    }

    public class UserRole
    {
        public string RoleName { get; set; }
    }

    public static class UserRoles
    {
        private static readonly List<string> _roles = new List<string>();

        public static List<string> Roles
        {
            get
            {
                _roles.Add("Admin");
                _roles.Add("User");
                _roles.Add("Manager");
                return _roles;
            }
        }
    }

    public enum Roles
    {
        Admin = 1,
        User = 2,
        Manager = 3,
        Supervisor = 4
    }
}
