using SyncSoft.Future.Enum.User;
using System;

namespace SyncSoft.Future.Passport.Command
{
    public class CreateUserCommand : SyncSoft.App.Messaging.RequestCommand
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserStatusEnum Status { get; set; }
        public UserRoleEnum? Roles { get; set; }
        public int? PermissionLevel { get; set; }
    }
}
