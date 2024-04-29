using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Security.Claims;

namespace Infrastructure.Identity.Requirements
{
    public class OwnRecordRequirement<T> : IAuthorizationRequirement where T : BaseAuditableEntity
    {
    }

    public class OwnRecordHandler<T> : AuthorizationHandler<OwnRecordRequirement<T>, Dictionary<string, object>> where T : BaseAuditableEntity
    {
        private readonly IDbContext dbContext;

        public OwnRecordHandler(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnRecordRequirement<T> requirement, Dictionary<string, object> resource)
        {
            var userId = GetUserId(context);
            var recordId = GetRecordId(resource);
            var record = GetRecordByUser(userId, recordId);

            if (record != null)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }

        private string GetUserId(AuthorizationHandlerContext context)
        {
            return context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private string GetRecordId(Dictionary<string, object> resource)
        {
            return resource["Id"].ToString();
        }

        private object GetRecordByUser(string userId, string recordId)
        {
            return dbContext.GetSet<T>().Where(record => record.Id.Equals(recordId) && record.CreatedBy.Equals(userId)).FirstOrDefault();
        }
    }
}
