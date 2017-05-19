using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using MobileCenterSdk.Test.Utils;
using MobileCenterSdk.Models;
using System.Linq;
using System.Collections.Generic;

namespace MobileCenterSdk.Test
{
    [TestClass]
    public class BuildUnitTest : UnitTestBase
    {
        #region Branches
        [TestMethod]
        public async Task GetBranches_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp(TestConfig.AppOsOfRepo, TestConfig.AppPlatformOfRepo);
            await Client.BuildService.ConfigureRepoForBuild(app.Owner.Name, app.Name, TestConfig.Repository);
            var branches = await Client.BuildService.GetBranchesAsync(app.Owner.Name, app.Name);

            if (branches.Count < 1)
                Assert.Fail();

            Assert.IsTrue(PropertiesSetCheck.Check(branches));

            //cleanup
            await Client.AccountService.DeleteAppAsync(app.Owner.Name, app.Name);
        }

        [TestMethod]
        public async Task CreateBuild_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp(TestConfig.AppOsOfRepo, TestConfig.AppPlatformOfRepo);
            var sampleBranchStatus = await GetConfiguredSampleBranchAsync(app);
            var build = await GetSampleBuild(app, sampleBranchStatus);
            Assert.IsTrue(PropertiesSetCheck.Check(build));

            //cleanup
            await Client.AccountService.DeleteAppAsync(app.Owner.Name, app.Name);
        }
        [TestMethod]
        public async Task GetBuilds_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp(TestConfig.AppOsOfRepo, TestConfig.AppPlatformOfRepo);
            var sampleBranchStatus = await GetConfiguredSampleBranchAsync(app);
            var build = await GetSampleBuild(app, sampleBranchStatus);

            var builds = await Client.BuildService.GetBranchBuildsAsync(app.Owner.Name, app.Name, sampleBranchStatus.Branch.Name);

            if (builds.Count < 1)
                Assert.Fail();


            Assert.IsTrue(PropertiesSetCheck.Check(builds));

            //cleanup
            await Client.AccountService.DeleteAppAsync(app.Owner.Name, app.Name);
        }

        [TestMethod]
        public async Task ConfigureBranch_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp(TestConfig.AppOsOfRepo, TestConfig.AppPlatformOfRepo);
            var sampleBranchStatus = await GetConfiguredSampleBranchAsync(app);
            var build = await GetSampleBuild(app, sampleBranchStatus);
            var branch = (await GetBranchesAsync(app)).First();
            var configInfo = await GetToolsetProjectsAsync(app, branch);
            var sampleConfig = SampleToolsetProjectConfig(configInfo);
            await Client.BuildService.ConfigureBranchAsync(app.Owner.Name, app.Name, branch.Branch.Name, sampleConfig);

            var config = await Client.BuildService.GetBranchConfigurationAsync(app.Owner.Name, app.Name, branch.Branch.Name);

            var isSlnPathCorrect = config.Toolsets.Xamarin.SlnPath == sampleConfig.Toolsets.Xamarin.SolutionPath;
            var isSimBuildCorrect = config.Toolsets.Xamarin.IsSimBuild == sampleConfig.Toolsets.Xamarin.IsSimBuild;
            Assert.IsTrue(isSlnPathCorrect && isSimBuildCorrect);

            //cleanup
            await Client.AccountService.DeleteAppAsync(app.Owner.Name, app.Name);
        }

        #endregion
        private async Task<McBuild> GetSampleBuild(McApp app, McBranchStatus branchStatus)
        {

            return await Client.BuildService.CreateBranchBuildAsync(app.Owner.Name, app.Name,
                branchStatus.Branch.Name, new McBuildParams()
                {
                    IsDebug = false,
                    SourceVersion = ""
                });
        }
        private async Task<McBranchStatus> GetConfiguredSampleBranchAsync(McApp app)
        {
            await Client.BuildService.ConfigureRepoForBuild(app.Owner.Name, app.Name, TestConfig.Repository);
            var branch = (await GetBranchesAsync(app)).First();
            var configInfo = await GetToolsetProjectsAsync(app, branch);
            await Client.BuildService.ConfigureBranchAsync(app.Owner.Name, app.Name, branch.Branch.Name, SampleToolsetProjectConfig(configInfo));
            return branch;
        }
        private async Task<List<McBranchStatus>> GetBranchesAsync(McApp app)
        {
            return await Client.BuildService.GetBranchesAsync(app.Owner.Name, app.Name);
        }
        private async Task<McToolsetProjects> GetToolsetProjectsAsync(McApp app, McBranchStatus branchStatus)
        {
            //configure branch before
            return await Client.BuildService.GetProjectsInRepoForBranchAsync(app.Owner.Name, app.Name, branchStatus.Branch.Name, app.PlatformType, app.OsType);
        }
        private McToolsetProjectConfiguration SampleToolsetProjectConfig(McToolsetProjects toolsetProjects)
        {
            return new McToolsetProjectConfiguration()
            {
                Toolsets = new McToolsets()
                {
                    Xamarin = new McXamarinToolsetConfig()
                    {
                        Configuration = toolsetProjects.Xamarin.XamarinSolutions.First().Configurations.First(),
                        IsSimBuild = false,
                        SolutionPath = toolsetProjects.Xamarin.XamarinSolutions.First().Path
                    }
                },
                Trigger = "manual",
                EnvironmentVariables = new List<string>(),
                TestsEnabled = false
            };
        }
    }
}
