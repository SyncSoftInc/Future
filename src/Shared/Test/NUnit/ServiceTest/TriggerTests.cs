using SyncSoft.App.Components;
using SyncSoft.ECP.APIs.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncSoft.Future.NUnit.ServiceTest
{
    public abstract class TriggerTests
    {
        // *******************************************************************************************************************************
        #region -  Lazy Object(s)  -

        private static readonly Lazy<ITriggerApi> _lazyTriggerApi = ObjectContainer.LazyResolve<ITriggerApi>();
        private ITriggerApi _TriggerApi => _lazyTriggerApi.Value;

        #endregion

    }
}
