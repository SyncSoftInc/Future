using SyncSoft.App.MySql;

namespace SyncSoft.Future.Passport.MySql
{
    public class PassportDB : MySqlDatabase, IPassportDB
    {
        public PassportDB(string connStrName) : base(connStrName)
        {
        }
    }
}
