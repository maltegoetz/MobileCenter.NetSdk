using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Test.Utils
{
    public static class PropertiesSetCheck
    {
        public static bool Check(McUser user)
        {
            var isIdSet = !string.IsNullOrWhiteSpace(user.Id);
            var isNameSet = !string.IsNullOrWhiteSpace(user.Name);
            var isDisplayNameSet = !string.IsNullOrEmpty(user.DisplayName);
            var isEmailSet = !string.IsNullOrWhiteSpace(user.Email);
            var isCreatedAtSet = user.CreatedAt != null && user.CreatedAt != default(DateTime);
            return (isIdSet && isNameSet && isDisplayNameSet && isEmailSet && isCreatedAtSet);
        }

        public static bool Check(List<McBranchStatus> branches)
        {
            foreach (var branch in branches)
            {
                if (!Check(branch))
                    return false;
            }
            return true;
        }

        public static bool Check(McBranchStatus branchStatus, bool checkForBuild = false)
        {
            var isBranchSet = Check(branchStatus.Branch);
            var isLastBuildSet = checkForBuild ? Check(branchStatus.LastBuild) : true;
            var isTriggerSet = checkForBuild ? !string.IsNullOrWhiteSpace(branchStatus.Trigger) : true;

            return (isBranchSet && isLastBuildSet && isTriggerSet);
        }

        public static bool Check(McBuild build)
        {
            var isBuildNumberSet = !string.IsNullOrWhiteSpace(build.BuildNumber);
            var isQueueTimeSet = build.QueueTime != null && build.QueueTime != default(DateTime);
            var isLastChangedDateSet = build.LastChangedDate != null && build.LastChangedDate != default(DateTime);
            var isStatusSet = !string.IsNullOrWhiteSpace(build.Status);
            var isReasonSet = !string.IsNullOrWhiteSpace(build.Reason);
            var isSourceBranchSet = !string.IsNullOrWhiteSpace(build.SourceBranch);

            return (isBuildNumberSet && isQueueTimeSet &&
                isLastChangedDateSet && isStatusSet && isReasonSet && isSourceBranchSet);
        }

        internal static bool Check(List<McBuild> builds)
        {
            foreach (var build in builds)
            {
                if (!Check(build))
                    return false;
            }
            return true;
        }

        public static bool Check(List<McApiTokenBase> tokens)
        {
            foreach (var token in tokens)
            {
                if (!Check(token))
                    return false;
            }
            return true;
        }

        public static bool Check(McApiTokenBase token)
        {
            var isCreatedAtSet = token.CreatedAt != null && token.CreatedAt != default(DateTime);
            var isIdSet = !string.IsNullOrWhiteSpace(token.Id);
            var isScopeSet = token.Scope.Count > 0;
            return (isCreatedAtSet && isIdSet && isScopeSet);
        }

        public static bool Check(McApiToken token)
        {
            var isTokenBaseSet = Check(token as McApiTokenBase);
            var isTokenSet = !string.IsNullOrWhiteSpace(token.Token);
            return (isTokenBaseSet && isTokenSet);
        }

        private static bool Check(McBranch branch)
        {
            var isNameSet = !string.IsNullOrWhiteSpace(branch.Name);
            var isCommitSet = Check(branch.Commit);
            return isNameSet;
        }

        private static bool Check(McCommitBase commit)
        {
            var isShaSet = !string.IsNullOrWhiteSpace(commit.Sha);
            var isUrlSet = !string.IsNullOrWhiteSpace(commit.Url);
            return (isShaSet && isUrlSet);
        }

        public static bool Check(List<McOrganization> orgs)
        {
            foreach (var org in orgs)
            {
                if (!Check(org))
                    return false;
            }
            return true;
        }

        public static bool Check(McOrganization org)
        {
            var isDisplayNameSet = !string.IsNullOrWhiteSpace(org.DisplayName);
            var isNameSet = !string.IsNullOrWhiteSpace(org.Name);
            var isCollabRoleSet = !string.IsNullOrWhiteSpace(org.CollaboratorRole);
            var isOriginSet = !string.IsNullOrWhiteSpace(org.Origin);
            var isOriginTypeSet = org.OriginType != McOrigin.Unknown;
            var isIdSet = !string.IsNullOrWhiteSpace(org.Id);
            return (isDisplayNameSet && isNameSet && isCollabRoleSet && isOriginSet &&
                isOriginTypeSet && isIdSet);
        }

        public static bool Check(List<McApp> appList)
        {
            foreach(var app in appList)
            {
                if (!Check(app))
                    return false;
            }
            return true;
        }

        public static bool Check(McApp app)
        {
            var isIdSet = !string.IsNullOrWhiteSpace(app.Id);
            var isAppSecretSet = !string.IsNullOrWhiteSpace(app.AppSecret);
            var isDisplayNameSet = !string.IsNullOrWhiteSpace(app.DisplayName);
            var isNameSet = !string.IsNullOrWhiteSpace(app.Name);
            var isOsSet = !string.IsNullOrWhiteSpace(app.Os);
            var isOsTypeSet = app.OsType != McAppOs.Unknown;
            var isOwnerSet = Check(app.Owner);
            var isPlatformSet = !string.IsNullOrWhiteSpace(app.Platform);
            var isPlatformTypeSet = app.PlatformType != McAppPlatform.Unknown;
            var isOriginSet = !string.IsNullOrWhiteSpace(app.Origin);
            var isOriginTypeSet = app.OriginType != McOrigin.Unknown;
            return (isIdSet && isAppSecretSet && isDisplayNameSet && isNameSet && isOsSet && isOwnerSet && isPlatformSet && isPlatformTypeSet && isOriginSet && isOriginTypeSet);
        }

        public static bool Check(McOwner owner)
        {
            var isIdSet = !string.IsNullOrWhiteSpace(owner.Id);
            var isDisplayNameSet = !string.IsNullOrWhiteSpace(owner.DisplayName);
            var isNameSet = !string.IsNullOrWhiteSpace(owner.Name);
            return (isIdSet && isDisplayNameSet && isNameSet);
        }

        public static bool Check(McDistributionGroup dg)
        {
            var isIdSet = !string.IsNullOrWhiteSpace(dg.Id);
            var isNameSet = !string.IsNullOrWhiteSpace(dg.Name);
            var isOriginSet = !string.IsNullOrWhiteSpace(dg.Origin);
            var isOriginTypeSet = dg.OriginType != McOrigin.Unknown;
            return (isIdSet && isNameSet && isOriginSet && isOriginTypeSet);
        }

        public static bool Check(List<McDistributionGroup> dgs)
        {
            foreach (var dg in dgs)
            {
                if (!Check(dg))
                    return false;
            }
            return true;
        }

        public static bool Check(List<McDistributionGroupInvitation> invites)
        {
            foreach (var invite in invites)
            {
                if (!Check(invite))
                    return false;
            }
            return true;
        }

        public static bool Check(List<McOrganizationInvitation> invitations)
        {
            foreach (var invite in invitations)
            {
                if (!Check(invite))
                    return false;
            }
            return true;
        }

        public static bool Check(McOrganizationInvitation invite)
        {
            var isEmailSet = !string.IsNullOrWhiteSpace(invite.Email);
            var isIdSet = !string.IsNullOrWhiteSpace(invite.Id);
            return (isEmailSet && isIdSet);
        }

        public static bool Check(McDistributionGroupInvitation invite)
        {
            var isStatusSet = invite.Status != default(int);
            var isUserEmailSet = !string.IsNullOrWhiteSpace(invite.UserEmail);
            return (isStatusSet && isUserEmailSet);
        }

        public static bool Check(List<McDistributionGroupUser> members)
        {
            foreach (var member in members)
            {
                if (!Check(member))
                    return false;
            }
            return true;
        }

        public static bool Check(McDistributionGroupUser member)
        {
            var isEmailSet = !string.IsNullOrWhiteSpace(member.Email);
            return isEmailSet;
        }

        public static bool Check(List<McAppInvitation> invitations)
        {
            foreach (var invitation in invitations)
            {
                if (!Check(invitation))
                    return false;
            }
            return true;
        }

        public static bool Check(List<McOrganizationUser> users)
        {
            foreach (var user in users)
            {
                if (!Check(user))
                    return false;
            }
            return true;
        }

        public static bool Check(McOrganizationUser user)
        {
            var isDislplayNameSet = !string.IsNullOrWhiteSpace(user.DisplayName);
            var isEmailSet = !string.IsNullOrWhiteSpace(user.Email);
            var isJoinedAtSet = user.JoinedAt != default(DateTime);
            var isNameSet = !string.IsNullOrWhiteSpace(user.Name);
            var isRoleSet = !string.IsNullOrWhiteSpace(user.Role);

            return (isDislplayNameSet && isEmailSet && isJoinedAtSet && isNameSet && isRoleSet);
        }

        public static bool Check(McAppInvitation invitation)
        {
            var isIdSet = !string.IsNullOrWhiteSpace(invitation.Id);
            var isEMailSet = !string.IsNullOrWhiteSpace(invitation.Email);
            var isPermissionsSet = invitation.Permissions != null;
            return (isIdSet && isEMailSet && isPermissionsSet);
        }

        public static bool Check(List<McUser> users)
        {
            foreach (var user in users)
            {
                if (!Check(user))
                    return false;
            }
            return true;
        }

        internal static bool Check(List<McAppUser> users)
        {
            foreach (var user in users)
            {
                if (!Check(user))
                    return false;
            }
            return true;
        }
    }
}
