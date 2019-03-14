﻿using SyncSoft.App.Components;
using SyncSoft.App.Transactions;
using SyncSoft.ECP.DALs.Account;
using SyncSoft.ECP.DTOs.Account;
using SyncSoft.ECP.Securities;
using SyncSoft.Future.Passport.Command.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSoft.Future.Passport.Domain.User.UserSaveProfile
{
    public class ChangePasswordActivity : RrTransactionActivity
    {
        private static readonly Lazy<IAccountDAL> _lazyAccountDAL = ObjectContainer.LazyResolve<IAccountDAL>();
        private IAccountDAL _AccountDAL => _lazyAccountDAL.Value;

        private static readonly Lazy<IPasswordEncryptor> _lazyPasswordEncryptor = ObjectContainer.LazyResolve<IPasswordEncryptor>();
        private IPasswordEncryptor _PasswordEncryptor => _lazyPasswordEncryptor.Value;

        public ChangePasswordActivity(RrTransactionContext context) : base(context)
        {
        }

        protected override async Task RunAsync(CancellationToken? cancellationToken)
        {
            var cmd = (UserSaveProfileCommand)Context.Items[UserSaveProfileTransaction.Parameters_Command];
            if (cmd.Password.IsMissing()) return;
            // ^^^^^^^^^^
            if (cmd.OldPassword.IsMissing()) throw new Exception(MsgCodes.PASS_0000000003);
            // ^^^^^^^^^^

            var oldDto = await _AccountDAL.GetAccountAsync(cmd.ID).ConfigureAwait(false);
            if (oldDto.IsNull()) throw new Exception(MsgCODES.FUT_0000000002);

            // Validate old password
            var oldPassword = _PasswordEncryptor.HashEncodePassword(cmd.OldPassword, oldDto.PasswordSalt);
            if (oldDto.Password != oldPassword) throw new Exception(MsgCodes.PASS_0000000004);
            // ^^^^^^^^^^

            Context.Items.Add(UserSaveProfileTransaction.Parameters_Account_Backup, oldDto);

            var dto = new AccountDTO
            {
                ID = cmd.ID,
                LoginFailedCount = 0,
                UpdatedOnUtc = DateTime.UtcNow
            };
            dto.PasswordSalt = _PasswordEncryptor.GeneratePasswordSalt();
            dto.Password = _PasswordEncryptor.HashEncodePassword(cmd.Password, dto.PasswordSalt);

            var msgCode = await _AccountDAL.UpdatePasswordAsync(dto).ConfigureAwait(false);
            if (!msgCode.IsSuccess()) throw new Exception(msgCode);
        }

        protected override async Task RollbackAsync()
        {
            var dto = (AccountDTO)Context.Items[UserSaveProfileTransaction.Parameters_Account_Backup];
            var msgCode = await _AccountDAL.UpdatePasswordAsync(dto).ConfigureAwait(false);
            if (!msgCode.IsSuccess()) throw new Exception(msgCode);
        }
    }
}
