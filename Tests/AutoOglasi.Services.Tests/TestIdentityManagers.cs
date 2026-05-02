namespace AutoOglasi.Services.Tests;

using AutoOglasi.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

internal static class TestIdentityManagers
{
    public static RoleManager<IdentityRole> CreateRoleManager(params IdentityRole[] roles)
    {
        var store = new InMemoryRoleStore(roles);

        return new RoleManager<IdentityRole>(
            store,
            Array.Empty<IRoleValidator<IdentityRole>>(),
            new UpperInvariantLookupNormalizer(),
            new IdentityErrorDescriber(),
            NullLogger<RoleManager<IdentityRole>>.Instance);
    }

    public static UserManager<ApplicationUser> CreateUserManager(
        IEnumerable<ApplicationUser> users,
        IDictionary<string, IEnumerable<string>>? roleAssignments = null)
    {
        var store = new InMemoryUserStore(users, roleAssignments);

        return new UserManager<ApplicationUser>(
            store,
            Options.Create(new IdentityOptions()),
            new PasswordHasher<ApplicationUser>(),
            Array.Empty<IUserValidator<ApplicationUser>>(),
            Array.Empty<IPasswordValidator<ApplicationUser>>(),
            new UpperInvariantLookupNormalizer(),
            new IdentityErrorDescriber(),
            new ServiceCollection().BuildServiceProvider(),
            NullLogger<UserManager<ApplicationUser>>.Instance);
    }

    private sealed class InMemoryRoleStore : IQueryableRoleStore<IdentityRole>
    {
        private readonly List<IdentityRole> roles;

        public InMemoryRoleStore(IEnumerable<IdentityRole> roles)
        {
            this.roles = roles.ToList();
        }

        public IQueryable<IdentityRole> Roles => this.roles.AsQueryable();

        public Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            this.roles.Add(role);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            this.roles.Remove(role);
            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
        }

        public Task<IdentityRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.roles.FirstOrDefault(role => role.Id == roleId));
        }

        public Task<IdentityRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.roles.FirstOrDefault(role => role.NormalizedName == normalizedRoleName));
        }

        public Task<string?> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task<string?> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(IdentityRole role, string? normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(IdentityRole role, string? roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }

    private sealed class InMemoryUserStore : IQueryableUserStore<ApplicationUser>, IUserRoleStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserEmailStore<ApplicationUser>
    {
        private readonly List<ApplicationUser> users;
        private readonly Dictionary<string, HashSet<string>> rolesByUserId;
        private readonly Dictionary<string, string?> passwordHashes = new();

        public InMemoryUserStore(
            IEnumerable<ApplicationUser> users,
            IDictionary<string, IEnumerable<string>>? roleAssignments)
        {
            this.users = users.ToList();
            this.rolesByUserId = roleAssignments?.ToDictionary(
                assignment => assignment.Key,
                assignment => assignment.Value.ToHashSet(StringComparer.OrdinalIgnoreCase))
                ?? new Dictionary<string, HashSet<string>>();
        }

        public IQueryable<ApplicationUser> Users => this.users.AsQueryable();

        public Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            if (!this.rolesByUserId.TryGetValue(user.Id, out var roles))
            {
                roles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                this.rolesByUserId[user.Id] = roles;
            }

            roles.Add(roleName);
            return Task.CompletedTask;
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            this.users.Add(user);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            this.users.Remove(user);
            this.rolesByUserId.Remove(user.Id);
            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
        }

        public Task<ApplicationUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.users.FirstOrDefault(user => user.Id == userId));
        }

        public Task<ApplicationUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.users.FirstOrDefault(user => user.NormalizedUserName == normalizedUserName));
        }

        public Task<string?> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            IList<string> roles = this.rolesByUserId.TryGetValue(user.Id, out var assignedRoles)
                ? assignedRoles.ToList()
                : new List<string>();

            return Task.FromResult(roles);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string?> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            IList<ApplicationUser> usersInRole = this.users
                .Where(user => this.rolesByUserId.TryGetValue(user.Id, out var roles) && roles.Contains(roleName))
                .ToList();

            return Task.FromResult(usersInRole);
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            var isInRole = this.rolesByUserId.TryGetValue(user.Id, out var roles) && roles.Contains(roleName);
            return Task.FromResult(isInRole);
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            if (this.rolesByUserId.TryGetValue(user.Id, out var roles))
            {
                roles.Remove(roleName);
            }

            return Task.CompletedTask;
        }

        public Task<string?> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            passwordHashes.TryGetValue(user.Id, out var hash);
            return Task.FromResult(hash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(passwordHashes.ContainsKey(user.Id) && passwordHashes[user.Id] != null);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string? passwordHash, CancellationToken cancellationToken)
        {
            passwordHashes[user.Id] = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string? normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(ApplicationUser user, string? userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<string?> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<ApplicationUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.users.FirstOrDefault(u => u.NormalizedEmail == normalizedEmail));
        }

        public Task SetEmailAsync(ApplicationUser user, string? email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task<string?> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string? normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
