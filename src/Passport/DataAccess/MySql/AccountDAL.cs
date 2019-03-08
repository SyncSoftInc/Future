namespace SyncSoft.Future.Passport.MySql
{
    public class AccountDAL : SyncSoft.ECP.MySql.Account.AccountDAL
    {
        public AccountDAL(IPassportDB db) : base(db)
        {
        }
    }
}
