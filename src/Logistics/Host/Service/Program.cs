﻿using SyncSoft.ECP.AspNetCore.Hosting;

namespace SyncSoft.Future.Logistics.Service
{
    public class Program
    {
        public static void Main(string[] args) => ECPHost.Run<Startup>(args);
    }
}
