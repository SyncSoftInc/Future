﻿using SyncSoft.App.Components;
using SyncSoft.App.Transactions;
using SyncSoft.Future.DTO.User;
using SyncSoft.Future.Passport.Command.User;
using SyncSoft.Future.Passport.DataAccess.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain.User.UpdateUser
{
    public class UpdateUserActivity : TccActivity
    {
        private static readonly Lazy<IUserDAL> _lazyUserDAL = ObjectContainer.LazyResolve<IUserDAL>();
        private IUserDAL _UserDAL => _lazyUserDAL.Value;

        protected override async Task RunAsync(CancellationToken? cancellationToken)
        {
            var cmd = Context.Get<UpdateUserCommand>(UpdateUserTransaction.Parameters_Command);

            var oldDto = await _UserDAL.GetUserAsync(cmd.ID).ConfigureAwait(false);
            if (oldDto.IsNull()) throw new Exception(MsgCODES.FUT_0000000002);
            Context.Set(UpdateUserTransaction.Parameters_User_Backup, oldDto);

            var dto = new UserDTO
            {
                ID = cmd.ID,
                FirstName = cmd.FirstName,
                MiddleName = cmd.MiddleName,
                LastName = cmd.LastName,
                Email = cmd.Email,
                Status = (ECP.Enums.User.UserStatusEnum)cmd.Status,
                Roles = cmd.Roles,
                PermissionLevel = cmd.PermissionLevel,
            };

            var msgCode = await _UserDAL.UpdateUserAsync(dto).ConfigureAwait(false);
            if (!msgCode.IsSuccess()) throw new Exception(msgCode);
        }

        protected override async Task RollbackAsync()
        {
            var dto = Context.Get<UserDTO>(UpdateUserTransaction.Parameters_User_Backup);
            var msgCode = await _UserDAL.UpdateUserAsync(dto).ConfigureAwait(false);
            if (!msgCode.IsSuccess()) throw new Exception(msgCode);
        }
    }
}
