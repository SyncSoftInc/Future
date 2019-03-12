using System;

namespace SyncSoft.Future.Enum.User
{
    [Flags]
    public enum UserRoleEnum
    {
        TrustedClient = (int)ECP.Enums.User.UserRoleEnum.TrustedClient,
        PublicClient = (int)ECP.Enums.User.UserRoleEnum.PublicClient,
        Admin = (int)ECP.Enums.User.UserRoleEnum.Admin,
    }
}
