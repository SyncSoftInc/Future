using System;

namespace SyncSoft.Future.Enum.User
{
    [Flags]
    public enum UserStatusEnum
    {
        Active = (int)ECP.Enums.User.UserStatusEnum.Active,
        Inactive = (int)ECP.Enums.User.UserStatusEnum.Inactive,
    }
}
