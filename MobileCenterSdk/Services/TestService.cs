using MobileCenterSdk.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Services
{
    public class TestService : ServiceBase
    {
        public TestService(MobileCenterCredentials credentials, MobileCenterSdkClient mcsc) : base(credentials, mcsc){}

    }
}
