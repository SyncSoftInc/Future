namespace SyncSoft.Future.Logistics
{
    public static class MsgCodes
    {
        public const string SUCCESS = MsgCODES.SUCCESS;
        /// <summary>
        /// Maxmium warehouse limit reached.
        /// </summary>
        public const string WH_0000000001 = nameof(WH_0000000001);
        /// <summary>
        /// Warehouse name already exists.
        /// </summary>
        public const string WH_0000000002 = nameof(WH_0000000002);
        /// <summary>
        /// Allocate inventories failed.
        /// </summary>
        public const string WH_0000000003 = nameof(WH_0000000003);
        /// <summary>
        /// Invalid inventories data.
        /// </summary>
        public const string WH_0000000004 = nameof(WH_0000000004);
        /// <summary>
        /// Hold inventories failed.
        /// </summary>
        public const string WH_0000000005 = nameof(WH_0000000005);
        /// <summary>
        /// Unhold inventories failed.
        /// </summary>
        public const string WH_0000000006 = nameof(WH_0000000006);
        /// <summary>
        /// Understocked, inventory shortage.
        /// </summary>
        public const string WH_0000000007 = nameof(WH_0000000007);
        /// <summary>
        /// Inventory ship confirm failed.
        /// </summary>
        public const string WH_0000000008 = nameof(WH_0000000008);
        /// <summary>
        /// Inventory quatity must equal or bigger than 0.
        /// </summary>
        public const string WH_0000000009 = nameof(WH_0000000009);
        /// <summary>
        /// Safe inventory quantity cannot greater than onhand quantity.
        /// </summary>
        public const string WH_0000000010 = nameof(WH_0000000010);
    }
}
