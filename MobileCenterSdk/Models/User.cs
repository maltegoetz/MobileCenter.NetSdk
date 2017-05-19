using MobileCenterSdk.Services;
using MobileCenterSdk.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobileCenterSdk.Models
{
    public class McUser : McSlimUser, IAccountServiceHolder
    {
        [JsonIgnore]
        AccountService IAccountServiceHolder.AccountService { get; set; }


        [JsonProperty(PropertyName = "permissions")]
        public List<string> Permissions { get; set; } = new List<string>();

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }


        public async Task<McUser> UpdateProfileAsync(string displayName, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckAccountService();
            var userWithDisplayName = new McUserWithDisplayName() { DisplayName = displayName };
            return await (this as IAccountServiceHolder).AccountService.UpdateUserAsync(userWithDisplayName, cancellationToken);
        }
        public async Task AcceptOrganizationInvitationAsync(string invitationToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckAccountService();
            await (this as IAccountServiceHolder).AccountService.AcceptOrganizationInvitationAsync(invitationToken, cancellationToken);
        }
        public async Task RejectOrganizationInvitationAsync(string invitationToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckAccountService();
            await (this as IAccountServiceHolder).AccountService.RejectOrganizationInvitationAsync(invitationToken, cancellationToken);
        }
        public async Task AcceptAppInvitationAsync(string invitationToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckAccountService();
            await (this as IAccountServiceHolder).AccountService.AcceptAppInvitationAsync(invitationToken, cancellationToken);
        }
        public async Task RejectAppInvitationAsync(string invitationToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckAccountService();
            await (this as IAccountServiceHolder).AccountService.RejectAppInvitationAsync(invitationToken, cancellationToken);
        }
        private void CheckAccountService()
        {
            if((this as IAccountServiceHolder).AccountService == null)
            {
                throw new UnauthorizedAccessException("Method can only be called on instances created by the SDK.");
            }
        }
    }
    public class McSlimUser : McUserBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty(PropertyName = "can_change_password")]
        public bool CanChangePassword { get; set; }
    }
    public class McDistributionGroupUser : McSlimUser
    {
        [JsonProperty(PropertyName = "invite_pending")]
        public bool InvitePending { get; set; }
    }
    public class McOrganizationUser : McUserBase, IUserWithRole, IAccountServiceHolder
    {
        [JsonIgnore]
        AccountService IAccountServiceHolder.AccountService { get; set; }

        [JsonIgnore]
        internal string OrganizationName { get; set; }

        [JsonProperty(PropertyName = "joined_at")]
        public DateTime JoinedAt { get; set; }

        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }

        public async Task Update(McOrganizationUserWithRole user, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckDependencies();
            await (this as IAccountServiceHolder).AccountService.UpdateOrganizationUserAsync(OrganizationName, Name, user, cancellationToken);
        }
        public async Task Delete(CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckDependencies();
            await (this as IAccountServiceHolder).AccountService.RemoveOrganizationUserAsync(OrganizationName, Name, cancellationToken);
        }
        private void CheckDependencies()
        {
            if ((this as IAccountServiceHolder).AccountService == null || string.IsNullOrEmpty(OrganizationName))
            {
                throw new UnauthorizedAccessException("Method can only be called on instances created by the SDK.");
            }
        }
    }
    public class McOrganizationUserWithRole : IUserWithRole
    {
        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }
    }
    public class McOwner : McUserBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(OwnerTypeConverter))]
        public OwnerType OwnerType { get; set; }
    }
    public enum OwnerType
    {
        Organization,
        User
    }
    public class McUserBase : McUserWithDisplayName
    {

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }
    public class McUserWithDisplayName : IUserWithDisplayName
    {
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }
    }
    public class McUserWithEmail
    {
        [JsonProperty(PropertyName = "user_email")]
        public string Email { get; set; }
    }
    public class McUsersWithEmailList
    {
        [JsonProperty(PropertyName = "user_emails")]
        public List<string> Emails { get; set; }
    }
    interface IUserWithDisplayName
    {
        string DisplayName { get; set; }
    }
    interface IUserWithRole
    {
        string Role { get; set; }
    }
}
