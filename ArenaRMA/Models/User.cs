using ArenaRMA.Models;

namespace ArenaRMA.Models
{
    public class User
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public bool IsActive { get; set; }
        public Role Role { get; set; }
    }
}