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
            await app.ConfigureRespository(TestConfig.Repository);
            var branches = await app.GetBranchesAsync();

            if (branches.Count < 1)
                Assert.Fail();

            Assert.IsTrue(PropertiesSetCheck.Check(branches));

            //cleanup
            await app.DeleteAsync();
        }

        [TestMethod]
        public async Task CreateBuild_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp(TestConfig.AppOsOfRepo, TestConfig.AppPlatformOfRepo);
            var sampleBranchStatus = await GetConfiguredSampleBranchAsync(app);
            var build = await GetSampleBuild(sampleBranchStatus);
            Assert.IsTrue(PropertiesSetCheck.Check(build));

            //cleanup
            await app.DeleteAsync();
        }
        [TestMethod]
        public async Task GetBuilds_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp(TestConfig.AppOsOfRepo, TestConfig.AppPlatformOfRepo);
            var sampleBranchStatus = await GetConfiguredSampleBranchAsync(app);
            var build = await GetSampleBuild(sampleBranchStatus);

            var builds = await sampleBranchStatus.Branch.GetBuildsAsync();

            if (builds.Count < 1)
                Assert.Fail();


            Assert.IsTrue(PropertiesSetCheck.Check(builds));

            //cleanup
            await app.DeleteAsync();
        }

        [TestMethod]
        public async Task ConfigureBranch_AllPropertiesSet_ShouldBeTrue()
        {
            var app = await CreateRandomApp(TestConfig.AppOsOfRepo, TestConfig.AppPlatformOfRepo);
            var sampleBranchStatus = await GetConfiguredSampleBranchAsync(app);
            var build = await GetSampleBuild(sampleBranchStatus);
            var branchStatus = (await app.GetBranchesAsync()).First();
            var configInfo = await GetToolsetProjectsAsync(app, branchStatus);
            var sampleConfig = SampleToolsetProjectConfig(configInfo);
            await branchStatus.Branch.ConfigureForBuildAsync(sampleConfig);

            var config = await branchStatus.Branch.GetConfigurationAsync();

            var isSlnPathCorrect = config.Toolsets.Xamarin.SlnPath == sampleConfig.Toolsets.Xamarin.SolutionPath;
            var isSimBuildCorrect = config.Toolsets.Xamarin.IsSimBuild == sampleConfig.Toolsets.Xamarin.IsSimBuild;
            Assert.IsTrue(isSlnPathCorrect && isSimBuildCorrect);

            //cleanup
            await app.DeleteAsync();
        }

        #endregion
        private async Task<McBuild> GetSampleBuild(McBranchStatus branchStatus)
        {
            return await branchStatus.Branch.CreateBuildAsync(
                new McBuildParams()
                {
                    IsDebug = false,
                    SourceVersion = ""
                });
        }
        private async Task<McBranchStatus> GetConfiguredSampleBranchAsync(McApp app)
        {
            await app.ConfigureRespository(TestConfig.Repository);
            var branchStatus = (await app.GetBranchesAsync()).First();
            var configInfo = await GetToolsetProjectsAsync(app, branchStatus);
            await branchStatus.Branch.ConfigureForBuildAsync(SampleToolsetProjectConfig(configInfo));
            return branchStatus;
        }
        private async Task<McToolsetProjects> GetToolsetProjectsAsync(McApp app, McBranchStatus branchStatus)
        {
            return await branchStatus.Branch.GetProjectsAsync(app.OsType, app.PlatformType);
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
