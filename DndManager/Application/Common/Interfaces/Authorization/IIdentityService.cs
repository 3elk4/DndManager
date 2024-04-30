namespace Application.Common.Interfaces.Authorization
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);
        Task<bool> AuthorizeRequirementAsync(string userId, object resource, string requirementName);
    }
}
