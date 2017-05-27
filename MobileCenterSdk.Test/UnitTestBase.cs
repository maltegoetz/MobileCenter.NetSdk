using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Test
{
    public abstract class UnitTestBase
    {
        private MobileCenterSdkClient _client;
        private MobileCenterSdkClient _basicAuthClient;

        public MobileCenterSdkClient Client
        {
            get
            {
                return _client ?? (_client = new MobileCenterSdkClient(new MobileCenterCredentials(TestConfig.ApiKey)));
            }
        }
        public MobileCenterSdkClient BasicAuthClient
        {
            get
            {
                return _basicAuthClient ?? (_basicAuthClient = new MobileCenterSdkClient(new MobileCenterCredentials(TestConfig.ApiUsername, TestConfig.ApiPassword)));
            }
        }
        public async Task<McApp> CreateRandomApp(McAppOs os = McAppOs.Android, McAppPlatform platform = McAppPlatform.Java)
        {
            var guid = Guid.NewGuid();
            return await Client.AccountService.CreateAppAsync(new McAppSlim()
            {
                Description = "UnitTest test app",
                DisplayName = $"UnitTest-{guid}",
                Name = $"UnitTest-{guid}",
                OsType = os,
                PlatformType = platform
            });
        }
    }
}
