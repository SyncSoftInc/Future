﻿using SyncSoft.App.Components;
using SyncSoft.App.Transactions;
using SyncSoft.Future.DTO.User;
using SyncSoft.Future.Passport.Command.User;
using SyncSoft.Future.Passport.DataAccess.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain.User.CreateUser
{
    public class CreateUserActivity : TccActivity
    {
        private static readonly Lazy<IUserDAL> _lazyUserDAL = ObjectContainer.LazyResolve<IUserDAL>();
        private IUserDAL _UserDAL => _lazyUserDAL.Value;

        protected override async Task RunAsync(CancellationToken? cancellationToken)
        {
            var cmd = Context.Get<CreateUserCommand>(CreateUserTransaction.Parameters_Command);
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

            var msgCode = await _UserDAL.InsertUserAsync(dto).ConfigureAwait(false);
            if (!msgCode.IsSuccess()) throw new Exception(msgCode);
        }

        protected override async Task RollbackAsync()
        {
            var cmd = Context.Get<CreateUserCommand>(CreateUserTransaction.Parameters_Command);
            var msgCode = await _UserDAL.DeleteUserAsync(cmd.ID).ConfigureAwait(false);
            if (!msgCode.IsSuccess()) throw new Exception(msgCode);
        }
    }
}
