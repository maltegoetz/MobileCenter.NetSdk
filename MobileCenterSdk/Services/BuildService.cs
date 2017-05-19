using MobileCenterSdk.Models;
using MobileCenterSdk.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobileCenterSdk.Services
{
    public class BuildService : ServiceBase
    {
        public BuildService(string apiKey, MobileCenterSdkClient mcsc) : base(apiKey, mcsc) { }
        public async Task<List<McBranchStatus>> GetBranchesAsync(string ownerName, string appName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchesEndpoint, ownerName, appName),
                HttpMethod.Get);
            return await SendRequest<List<McBranchStatus>>(request, cancellationToken);
        }
        public async Task<List<McBuild>> GetBranchBuildsAsync(string ownerName, string appName, string branchName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchBuildsEndpoint, ownerName, appName, branchName),
                HttpMethod.Get);
            return await SendRequest<List<McBuild>>(request, cancellationToken);
        }
        public async Task<McBuild> CreateBranchBuildAsync(string ownerName, string appName, string branchName, McBuildParams buildParams, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchBuildsEndpoint, ownerName, appName, branchName),
                HttpMethod.Post,
                body: buildParams);
            return await SendRequest<McBuild>(request, cancellationToken);
        }
        public async Task<McBranchConfiguration> ReconfigureBranchAsync(string ownerName, string appName, string branchName, McToolsetProjectConfiguration toolsetConfig, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchConfigEndpoint, ownerName, appName, branchName),
                HttpMethod.Put,
                body: toolsetConfig);
            return await SendRequest<McBranchConfiguration>(request, cancellationToken);
        }
        public async Task<McBranchConfiguration> ConfigureBranchAsync(string ownerName, string appName, string branchName, McToolsetProjectConfiguration toolsetConfig, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchConfigEndpoint, ownerName, appName, branchName),
                HttpMethod.Post,
                body: toolsetConfig);
            return await SendRequest<McBranchConfiguration>(request, cancellationToken);
        }
        public async Task<McBranchConfiguration> GetBranchConfigurationAsync(string ownerName, string appName, string branchName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchConfigEndpoint, ownerName, appName, branchName),
                HttpMethod.Get);
            return await SendRequest<McBranchConfiguration>(request, cancellationToken);
        }
        public async Task<McBranchConfiguration> DeleteBranchConfigurationAsync(string ownerName, string appName, string branchName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchConfigEndpoint, ownerName, appName, branchName),
                HttpMethod.Delete);
            return await SendRequest<McBranchConfiguration>(request, cancellationToken);
        }
        public async Task<McToolsetProjects> GetProjectsInRepoForBranchAsync(string ownerName, string appName, string branchName, AppPlatform platform, AppOs os, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchToolsetEndpoint, ownerName, appName, branchName),
                HttpMethod.Get,
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("os", StringToOsTypeConverter.ConvertBack(os)),
                    new KeyValuePair<string, string>("platform", StringToPlatformTypeConverter.ConvertBack(platform))
                });
            return await SendRequest<McToolsetProjects>(request, cancellationToken);
        }
        public async Task<McBuildServiceStatusResponse> GetBuildServiceStatusAsync(string ownerName, string appName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBuildServiceStatusEndpoint, ownerName, appName),
                HttpMethod.Get);
            return await SendRequest<McBuildServiceStatusResponse>(request, cancellationToken);
        }
        public async Task<McBuild> CancelBuildAsync(string ownerName, string appName, string buildId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBuildEndpoint, ownerName, appName, buildId),
                ApiSettings.HttpMethodPatch);
            return await SendRequest<McBuild>(request, cancellationToken);
        }
        public async Task<McBuild> GetBuildAsync(string ownerName, string appName, string buildId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBuildEndpoint, ownerName, appName, buildId),
                HttpMethod.Get);
            return await SendRequest<McBuild>(request, cancellationToken);
        }
        public async Task<McDistributionResponse> DistributeBuild(string ownerName, string appName, string buildId, McDistributionInformation distributionInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBuildDistributeEndpoint, ownerName, appName, buildId),
                HttpMethod.Post,
                body: distributionInfo);
            return await SendRequest<McDistributionResponse>(request, cancellationToken);
        }
        public async Task<McDownloadContainer> GetBuildDownloadInformation(string ownerName, string appName, string buildId, DownloadType downloadType, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBuildDownloadsEndpoint, ownerName, appName, buildId, StringToDownloadTypeConverter.ConvertBack(downloadType)),
                HttpMethod.Get);
            return await SendRequest<McDownloadContainer>(request, cancellationToken);
        }
        public async Task<McBuildLog> GetBuildLogs(string ownerName, string appName, string buildId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBuildLogEndpoint, ownerName, appName, buildId),
                HttpMethod.Get);
            return await SendRequest<McBuildLog>(request, cancellationToken);
        }
        public async Task<McCommitDetail> GetCommitInformationForShas(string ownerName, string appName, List<string> shaList, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppCommitsEndpoint, ownerName, appName),
                HttpMethod.Get,
                body: shaList);
            return await SendRequest<McCommitDetail>(request, cancellationToken);
        }
        public async Task<McSuccessMessage> ConfigureRepoForBuild(string ownerName, string appName, string repoUrl, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppRepoConfigEndpoint, ownerName, appName),
                HttpMethod.Post,
                body: new McRepositoryInfo() { RepoUrl = repoUrl });
            return await SendRequest<McSuccessMessage>(request, cancellationToken);
        }
        public async Task<List<McRepositoryConfig>> GetRepoBuildConfiguration(string ownerName, string appName, bool includeInactive, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppRepoConfigEndpoint, ownerName, appName),
                HttpMethod.Get,
                new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("includeInactive", includeInactive.ToString().ToLower())
                });
            return await SendRequest<List<McRepositoryConfig>>(request, cancellationToken);
        }
        public async Task<McSuccessMessage> RemoveRepoBuildConfiguration(string ownerName, string appName, string repoUrl, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppRepoConfigEndpoint, ownerName, appName),
                HttpMethod.Delete);
            return await SendRequest<McSuccessMessage>(request, cancellationToken);
        }
        public async Task<List<McSourceRepository>> GetReposAvailableFromSourceControl(string ownerName, string appName, SourceHost sourceHost, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppSourceHostsEndpoint, ownerName, appName, StringToSourceHostConverter.ConvertBack(sourceHost)),
                HttpMethod.Get);
            return await SendRequest<List<McSourceRepository>>(request, cancellationToken);
        }
        public async Task<List<McXcodeVersion>> GetAvailableXcodeVersions(string ownerName, string appName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppXcodeVersionEndpoint, ownerName, appName),
                HttpMethod.Get);
            return await SendRequest<List<McXcodeVersion>>(request, cancellationToken);
        }
    }
}
