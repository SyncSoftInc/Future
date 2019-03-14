namespace SyncSoft.Future.Passport
{
    public static class MsgCodes
    {
        public const string SUCCESS = MsgCODES.SUCCESS;

        /// <summary>
        /// Username already exists.
        /// </summary>
        public const string PASS_0000000001 = nameof(PASS_0000000001);
        /// <summary>
        /// Create user failed.
        /// </summary>
        public const string PASS_0000000002 = nameof(PASS_0000000002);
        /// <summary>
        /// Old password cannot be empty.
        /// </summary>
        public const string PASS_0000000003 = nameof(PASS_0000000003);
        /// <summary>
        /// Invalid Old password.
        /// </summary>
        public const string PASS_0000000004 = nameof(PASS_0000000004);
    }
}
