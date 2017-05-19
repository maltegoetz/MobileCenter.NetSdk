using MobileCenterSdk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk
{
    public class MobileCenterSdkClient
    {
        private AccountService _accountService;
        private BuildService _buildService;
        private string _apiKey;

        public MobileCenterSdkClient(string apiKey)
        {
            _apiKey = apiKey;
        }
        public AccountService AccountService
        {
            get { return _accountService ?? (_accountService = new AccountService(_apiKey, this)); }
        }
        public BuildService BuildService
        {
            get { return _buildService ?? (_buildService = new BuildService(_apiKey, this)); }
        }
    }
}
