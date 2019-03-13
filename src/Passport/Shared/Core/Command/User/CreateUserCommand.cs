using SyncSoft.Future.Enum.User;
using System;

namespace SyncSoft.Future.Passport.Command.User
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
        public int Status { get; set; }
        public long? Roles { get; set; }
        public int? PermissionLevel { get; set; }
    }

    public class UpdateUserCommand : SyncSoft.App.Messaging.RequestCommand
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public long? Roles { get; set; }
        public int? PermissionLevel { get; set; }
    }

    public class UserSaveProfileCommand : SyncSoft.App.Messaging.RequestCommand
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public long? Roles { get; set; }
        public int? PermissionLevel { get; set; }
    }

    public class DeleteUserCommand : SyncSoft.App.Messaging.RequestCommand
    {
        public Guid ID { get; set; }
    }
}
