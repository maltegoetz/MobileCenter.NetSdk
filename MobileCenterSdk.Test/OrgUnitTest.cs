using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileCenterSdk.Models;
using MobileCenterSdk.Test.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Test
{
    [TestClass]
    public class OrgUnitTest : UnitTestBase
    {
        #region Org Basic 
        [TestMethod]
        public async Task GetOrgs_AllPropertiesSet_ShouldBeTrue()
        {
            var org = await CreateRandomOrg();
            var orgs = await Client.AccountService.GetOrganizationsAsync();

            Assert.IsTrue(PropertiesSetCheck.Check(orgs));
            //cleanup
            await Client.AccountService.DeleteOrganizationAsync(org.Name);
        }
        [TestMethod]
        public async Task CreateOrg_AllPropertiesSet_ShouldBeTrue()
        {
            var org = await CreateRandomOrg();
            Assert.IsTrue(PropertiesSetCheck.Check(org));
            //cleanup
            await Client.AccountService.DeleteOrganizationAsync(org.Name);
        }
        [TestMethod]
        public async Task GetOrg_AllPropertiesSet_ShouldBeTrue()
        {
            var org = await CreateRandomOrg();
            org = await Client.AccountService.GetOrganizationAsync(org.Name);
            Assert.IsTrue(PropertiesSetCheck.Check(org));
            //cleanup
            await Client.AccountService.DeleteOrganizationAsync(org.Name);
        }
        [TestMethod]
        public async Task UpdateOrg_AllPropertiesSet_ShouldBeTrue()
        {
            var org = await CreateRandomOrg();
            var changes = new BaseOrganization()
            {
                DisplayName = "UnitTestOrg-Changed",
                Name = "UnitTestOrg-Changed"
            };
            org = await Client.AccountService.UpdateOrganizationAsync(org.Name, changes);
            if (org.Name != changes.Name || org.DisplayName != changes.DisplayName)
                Assert.Fail();

            Assert.IsTrue(PropertiesSetCheck.Check(org));
            //cleanup
            await Client.AccountService.DeleteOrganizationAsync(org.Name);
        }
        [TestMethod]
        public async Task DeleteOrg_IsSuccessful_ShouldBeTrue()
        {
            var org = await CreateRandomOrg();
            await Client.AccountService.DeleteOrganizationAsync(org.Name);
        }
        #endregion
        #region App in Org
        [TestMethod]
        public async Task CreateAppInOrg_AllPropertiesSet_ShouldBeTrue()
        {
            var org = await CreateRandomOrg();
            var app = await CreateRandomAppInOrg(org.Name);

            Assert.IsTrue(PropertiesSetCheck.Check(app));

            //cleanup
            await Client.AccountService.DeleteOrganizationAsync(org.Name);
        }
        [TestMethod]
        public async Task GetAppsInOrg_AllPropertiesSet_ShouldBeTrue()
        {
            var org = await CreateRandomOrg();
            var app = await CreateRandomAppInOrg(org.Name);
            var apps = await Client.AccountService.GetAppsOfOrganizationAsync(org.Name);
            Assert.IsTrue(PropertiesSetCheck.Check(apps));

            //cleanup
            await Client.AccountService.DeleteOrganizationAsync(org.Name);
        }
        #endregion
        #region Users in Org
        [TestMethod]
        public async Task InviteUserToOrg_IsSuccessful_ShouldBeTrue()
        {
            var org = await CreateRandomOrg();
            await Client.AccountService.InviteUserToOrganizationAsync(org.Name, TestConfig.SecondUserEmailAdress);

            //cleanup
            await Client.AccountService.DeleteOrganizationAsync(org.Name);
        }
        [TestMethod]
        public async Task InvitationsToOrg_AllPropertiesSet_ShouldBeTrue()
        {
            var org = await CreateRandomOrg();
            await Client.AccountService.InviteUserToOrganizationAsync(org.Name, TestConfig.SecondUserEmailAdress);
            var invitations = await Client.AccountService.GetPendingInvitationsOfOrganizationAsync(org.Name);
            if (invitations.Count < 1)
                Assert.Fail();

            Assert.IsTrue(PropertiesSetCheck.Check(invitations));

            //cleanup
            await Client.AccountService.DeleteOrganizationAsync(org.Name);
        }
        [TestMethod]
        public async Task RemoveInvitationsToOrg_IsSuccessful_ShouldBeTrue()
        {
            var org = await CreateRandomOrg();
            await Client.AccountService.InviteUserToOrganizationAsync(org.Name, TestConfig.SecondUserEmailAdress);
            await Client.AccountService.RemoveInvitationToOrganization(org.Name, TestConfig.SecondUserEmailAdress);

            //cleanup
            await Client.AccountService.DeleteOrganizationAsync(org.Name);
        }
        [TestMethod]
        public async Task ResendInvitationsToOrg_IsSuccessful_ShouldBeTrue()
        {
            var org = await CreateRandomOrg();
            await Client.AccountService.InviteUserToOrganizationAsync(org.Name, TestConfig.SecondUserEmailAdress);
            await Client.AccountService.ResendInvitationToOrganizationAsync(org.Name, TestConfig.SecondUserEmailAdress);

            //cleanup
            await Client.AccountService.DeleteOrganizationAsync(org.Name);
        }
        [TestMethod]
        public async Task GetOrgUsers_AllPropertiesSet_ShouldBeTrue()
        {
            var org = await CreateRandomOrg();
            var users = await Client.AccountService.GetOrganizationUsersAsync(org.Name);
            //owner is always a user
            if (users.Count < 1)
                Assert.Fail();

            Assert.IsTrue(PropertiesSetCheck.Check(users));
            //cleanup
            await Client.AccountService.DeleteOrganizationAsync(org.Name);
        }

        [TestMethod]
        public void UpdateOrgUser_AllPropertiesSet_ShouldBeTrue()
        {
            Assert.Inconclusive("Not able to unit test this call, user has to accept invitation via email link.");
        }
        [TestMethod]
        public void DeleteOrgUser_AllPropertiesSet_ShouldBeTrue()
        {
            Assert.Inconclusive("Not able to unit test this call, user has to accept invitation via email link.");
        }
        #endregion
        #region Helpers
        private async Task<McOrganization> CreateRandomOrg()
        {
            var guid = Guid.NewGuid();
            return await Client.AccountService.CreateOrganizationAsync(new BaseOrganization()
            {
                DisplayName = $"UnitTestOrg-{guid}",
                Name = $"UnitTestOrg-{guid}"
            });
        }
        private async Task<McApp> CreateRandomAppInOrg(string orgName)
        {
            var guid = Guid.NewGuid();
            return await Client.AccountService.CreateAppInOrganizationAsync(orgName, new McAppSlim()
            {
                Description = "UnitTest test app",
                DisplayName = $"UnitTest-{guid}",
                Name = $"UnitTest-{guid}",
                OsType = AppOs.IOs,
                PlatformType = AppPlatform.ObjectiveCSwift
            });
        }
        #endregion
    }
}
