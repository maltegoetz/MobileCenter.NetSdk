using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using MobileCenterSdk.Test.Utils;
using MobileCenterSdk.Models;
using System.Collections.Generic;
using System.Linq;

namespace MobileCenterSdk.Test
{
    [TestClass]
    public class AppUnitTest : UnitTestBase
    {
        #region App Basic
        private List<McApp> _apps;
        [TestMethod]
        public async Task GetApps_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            Assert.IsTrue(PropertiesSetCheck.Check(await GetAppsAsync()));

            //cleanup
            await app.DeleteAsync();
        }
        [TestMethod]
        public async Task GetSingleApp_AllPropertiesSet_ShouldBeTrue()
        {
            var newapp = await CreateRandomApp();
            var appInfo = (await GetAppsAsync()).FirstOrDefault();
            var app = await Client.AccountService.GetAppAsync(appInfo.Owner.Name, appInfo.Name);
            Assert.IsTrue(PropertiesSetCheck.Check(app));

            //cleanup
            await newapp.DeleteAsync();
        }
        [TestMethod]
        public async Task CreateApp_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            Assert.IsTrue(PropertiesSetCheck.Check(app));
            //cleanup
            await app.DeleteAsync();

        }

        [TestMethod]
        public async Task DeleteApp_Successful_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            await app.DeleteAsync();
        }
        [TestMethod]
        public async Task UpdateApp_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            var guid = Guid.NewGuid();
            var newapp = await app.UpdateAsync(new McAppBase()
            {
                DisplayName = $"UnitTest-{guid}",
                Name = $"UnitTest-{guid}",
                Description = "test"
            });
            Assert.IsTrue(PropertiesSetCheck.Check(newapp));
            //cleanup
            await app.DeleteAsync();

        }
        #endregion
        #region Distribution Group
        [TestMethod]
        public async Task CreateDistributionGroup_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            var dg = await CreateRandomDistributionGroup(app);

            Assert.IsTrue(PropertiesSetCheck.Check(dg));
            //cleanup
            await app.DeleteAsync();
        }
        [TestMethod]
        public async Task GetDistributionGroups_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            var dg = await CreateRandomDistributionGroup(app);
            var dgs = await app.GetDistributionGroupsAsync();
            if (dgs.Count < 1)
                Assert.Fail();
            Assert.IsTrue(PropertiesSetCheck.Check(dgs));
            //cleanup
            await app.DeleteAsync();
        }
        [TestMethod]
        public async Task GetSingleDistributionGroup_PropertiesAreEqual_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            var dg = await CreateRandomDistributionGroup(app);
            var singleDg = await app.GetDistributionGroupByNameAsync(dg.Name);

            Assert.IsTrue(dg.Id.Equals(singleDg.Id) && dg.Name.Equals(singleDg.Name) &&
                dg.Origin.Equals(singleDg.Origin) && dg.OriginType.Equals(singleDg.OriginType));
            //cleanup
            await app.DeleteAsync();
        }
        [TestMethod]
        public async Task UpdateDistributionGroup_PropertiesAreEqual_ShouldBeTrue()
        {
            var updatedName = "UpdatedDG";
            var app = await CreateRandomApp();
            var dg = await CreateRandomDistributionGroup(app);
            var newDg = await dg.UpdateAsync(updatedName);
            
            Assert.IsTrue(dg.Id.Equals(newDg.Id) && newDg.Name.Equals(updatedName) &&
                dg.Origin.Equals(newDg.Origin) && dg.OriginType.Equals(newDg.OriginType));
            //cleanup
            await app.DeleteAsync();
        }
        [TestMethod]
        public async Task DeleteDistributionGroup_Successful_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            var dg = await CreateRandomDistributionGroup(app);
            await dg.DeleteAsync();
            //cleanup
            await app.DeleteAsync();
        }
        [TestMethod]
        public async Task AddDistributionGroupMembers_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            var dg = await CreateRandomDistributionGroup(app);
            var invites = await dg.InviteMembersAsync(new McUsersWithEmailList()
            {
                Emails = new List<string>(){
                    TestConfig.SecondUserEmailAdress
                }
            });
            if (invites.Count < 1)
                Assert.Fail();
            Assert.IsTrue(PropertiesSetCheck.Check(invites));
            //cleanup
            await app.DeleteAsync();
        }
        [TestMethod]
        public async Task GetDistributionGroupMembers_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            var dg = await CreateRandomDistributionGroup(app);
            var invites = await dg.InviteMembersAsync(new McUsersWithEmailList()
            {
                Emails = new List<string>(){
                    TestConfig.SecondUserEmailAdress
                }
            });
            var members = await dg.GetMembersAsync();
            Assert.IsTrue(PropertiesSetCheck.Check(members));
            //cleanup
            await app.DeleteAsync();
        }

        [TestMethod]
        public async Task RemoveDistributionGroupMembers_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            var dg = await CreateRandomDistributionGroup(app);
            var users = new McUsersWithEmailList()
            {
                Emails = new List<string>(){
                    TestConfig.SecondUserEmailAdress
                }
            };
            var invites = await dg.InviteMembersAsync(users);
            var delmembers = await dg.DeleteMembersAsync(users);
            if (delmembers.Count < 1)
                Assert.Fail();
            Assert.IsTrue(PropertiesSetCheck.Check(delmembers));
            //cleanup
            await app.DeleteAsync();

        }
        #endregion
        #region App Invitation
        [TestMethod]
        public async Task InviteUserToApp_IsSuccessful_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            await app.InviteUserAsync(TestConfig.SecondUserEmailAdress);
            
            //cleanup
            await app.DeleteAsync();
        }
        [TestMethod]
        public async Task GetAppInvitations_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            await app.InviteUserAsync(TestConfig.SecondUserEmailAdress);
            var invitations = await app.GetPendingInvitationsAsync();
            if (invitations.Count < 1 || invitations[0].Email != TestConfig.SecondUserEmailAdress)
                Assert.Fail();

            Assert.IsTrue(PropertiesSetCheck.Check(invitations));
            //cleanup
            await app.DeleteAsync();
        }
        [TestMethod]
        public async Task UpdateAppInvitation_IsSuccessful_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            await app.InviteUserAsync(TestConfig.SecondUserEmailAdress);
            var permissions = new McPermissionData()
            {
                Permissions = new List<string>() { "manager" }
            };
            var invitations = await app.GetPendingInvitationsAsync();
            var invite = invitations.Single(i => i.Email == TestConfig.SecondUserEmailAdress);
            await invite.UpdateAsync(permissions);
            
            //cleanup
            await app.DeleteAsync();
        }
        [TestMethod]
        public async Task DeleteAppInvitation_IsSuccessful_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            await app.InviteUserAsync(TestConfig.SecondUserEmailAdress);
            var invitations = await app.GetPendingInvitationsAsync();
            var invite = invitations.Single(i => i.Email == TestConfig.SecondUserEmailAdress);
            await invite.DeleteAsync();

            //cleanup
            await Client.AccountService.DeleteAppAsync(app.Owner.Name, app.Name);
        }
        [TestMethod]
        public async Task GetAppTesters_IsSuccessful_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            var testers = await app.GetTestersAsync();

            if (testers.Count < 1) //the owner can not be removed
                Assert.Fail();
            Assert.IsTrue(PropertiesSetCheck.Check(testers));
            //cleanup
            await app.DeleteAsync();
        }
        [TestMethod]
        public async Task GetAppUsers_IsSuccessful_ShouldBeTrue()
        {
            var app = await CreateRandomApp();
            var users = await app.GetUsersAsync();

            if (users.Count < 1) //the owner can not be removed
                Assert.Fail();
            Assert.IsTrue(PropertiesSetCheck.Check(users));
            //cleanup
            await app.DeleteAsync();
        }
        [TestMethod]
        public void UpdateAppUser_IsSuccessful_ShouldBeTrue()
        {
            Assert.Inconclusive("Not able to unit test this call, user has to accept invitation via email link.");
        }
        [TestMethod]
        public void DeleteAppUser_IsSuccessful_ShouldBeTrue()
        {
            Assert.Inconclusive("Not able to unit test this call, user has to accept invitation via email link.");
        }
        #endregion
        #region Helpers
        private async Task<List<McApp>> GetAppsAsync()
        {
            return _apps ?? (_apps = await Client.AccountService.GetAppsAsync());
        }
        private async Task<McDistributionGroup> CreateRandomDistributionGroup(McApp app)
        {
            var guid = Guid.NewGuid();
            return await app.CreateDistributionGroupAsync($"UnitTestDG-{guid}");
        }
        #endregion
    }
}
