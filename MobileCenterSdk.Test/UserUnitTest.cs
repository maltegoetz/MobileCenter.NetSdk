using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using MobileCenterSdk.Models;
using MobileCenterSdk.Test.Utils;
using System.Collections.Generic;

namespace MobileCenterSdk.Test
{
    [TestClass]
    public class UserUnitTest : UnitTestBase
    {
        private McCurrentUser _user = null;
        public async Task<McCurrentUser> GetUserAsync()
        {
            return _user ?? (_user = await Client.AccountService.GetUserAsync());
        }
        
        [TestMethod]
        public async Task GetUser_AllPropertiesSet_ShouldBeTrue()
        {
            Assert.IsTrue(PropertiesSetCheck.Check(await GetUserAsync()));
        }
        [TestMethod]
        public async Task UpdateUser_IsSuccessful_ShouldBeTrue()
        {
            var fakeDisplayName = "Mr X";
            var user = await GetUserAsync();
            var displayName = user.DisplayName;
            var newUser = await user.UpdateProfileAsync(fakeDisplayName);

            //check
            Assert.AreEqual(fakeDisplayName, newUser.DisplayName);

            //clenaup
            await Client.AccountService.UpdateUserAsync(new McUserWithDisplayName() { DisplayName = displayName });
        }
        [TestMethod]
        public void AcceptAllDgInvitations_IsSuccessful_ShouldBeTrue()
        {
            Assert.Inconclusive("Not able to unit test this call, user has to be invited by another user first.");
        }
        [TestMethod]
        public void RejectOrgInvitation_IsSuccessful_ShouldBeTrue()
        {
            Assert.Inconclusive("Not able to unit test this call, user has to accept invitation via email link.");
        }
        [TestMethod]
        public void AcceptOrgInvitation_IsSuccessful_ShouldBeTrue()
        {
            Assert.Inconclusive("Not able to unit test this call, user has to accept invitation via email link.");
        }
        [TestMethod]
        public void RejectAppInvitation_IsSuccessful_ShouldBeTrue()
        {
            Assert.Inconclusive("Not able to unit test this call, user has to accept invitation via email link.");
        }
        [TestMethod]
        public void AcceptAppInvitation_IsSuccessful_ShouldBeTrue()
        {
            Assert.Inconclusive("Not able to unit test this call, user has to accept invitation via email link.");
        }
        [TestMethod]
        public async Task GetApiTokens_AllPropertiesSet_ShouldBeTrue()
        {
            var tokens = await BasicAuthClient.AccountService.GetApiTokensAsync();
            //The result must contain the token we are testing with
            if (tokens.Count < 1)
                Assert.Fail();
            Assert.IsTrue(PropertiesSetCheck.Check(tokens));
        }
        [TestMethod]
        public async Task CreateApiToken_AllPropertiesSet_ShouldBeTrue()
        {
            var token = await BasicAuthClient.AccountService.CreateApiTokenAsync(
                new McApiTokenInformation() {
                    Description = "TokenCeatedByUnitTest",
                    Scope = new List<string>() { "all" }
            });
            Assert.IsTrue(PropertiesSetCheck.Check(token));

            //cleanup
            await token.DeleteAsync();
        }
        [TestMethod]
        public async Task DeleteApiToken_IsSuccessful_ShouldBeTrue()
        {
            var token = await BasicAuthClient.AccountService.CreateApiTokenAsync(
                new McApiTokenInformation()
                {
                    Description = "TokenCeatedByUnitTest",
                    Scope = new List<string>() { "all" }
                });
            await token.DeleteAsync();
        }
    }
}
