using MobileCenterSdk.Services;
using MobileCenterSdk.Utils;
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
        private string _username;
        private string _password;

        public MobileCenterCredentials Credentials { get; set; }

        public MobileCenterSdkClient(MobileCenterCredentials credentials)
        {
            Credentials = credentials;
        }
        public AccountService AccountService
        {
            get { return _accountService ?? (_accountService = new AccountService(Credentials, this)); }
        }
        public BuildService BuildService
        {
            get { return _buildService ?? (_buildService = new BuildService(Credentials, this)); }
        }
    }
}
