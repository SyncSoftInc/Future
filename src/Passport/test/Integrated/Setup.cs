﻿using NUnit.Framework;
using SyncSoft.App;

namespace SyncSoft.Future.Passport.IntegratedTest
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public static void Startup()
        {
            TestEngine.Init()
                .UseFutureRedis()
                .UsePassportAPI()
                .Start();
        }

        //[OneTimeTearDown]
        //public void TearDown()
        //{
        //}
    }
}
