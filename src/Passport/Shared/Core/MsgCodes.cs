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
        /// Update user failed.
        /// </summary>
        public const string PASS_0000000003 = nameof(PASS_0000000003);
        /// <summary>
        /// Save profile failed.
        /// </summary>
        public const string PASS_0000000004 = nameof(PASS_0000000004);
        /// <summary>
        /// Delete user failed.
        /// </summary>
        public const string PASS_0000000005 = nameof(PASS_0000000005);
        /// <summary>
        /// Old password cannot be empty.
        /// </summary>
        public const string PASS_0000000006 = nameof(PASS_0000000006);
        /// <summary>
        /// Invalid old password.
        /// </summary>
        public const string PASS_0000000007 = nameof(PASS_0000000007);
    }
}
