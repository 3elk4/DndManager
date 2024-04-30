using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Authorization;
using Application.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Application.Common.Behaviours
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IUser _user;
        private readonly IIdentityService _identity;

        public AuthorizationBehavior(
            IUser user,
            IIdentityService identity)
        {
            _user = user;
            _identity = identity;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (_user.Id == null)
                {
                    throw new UnauthorizedAccessException();
                }

                // Role-based authorization
                var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

                if (authorizeAttributesWithRoles.Any())
                {
                    var authorized = false;

                    foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                    {
                        foreach (var role in roles)
                        {
                            var isInRole = await _identity.IsInRoleAsync(_user.Id, role.Trim());
                            if (isInRole)
                            {
                                authorized = true;
                                break;
                            }
                        }
                    }

                    // Must be a member of at least one role in roles
                    if (!authorized)
                    {
                        throw new ForbiddenAccessException();
                    }
                }

                // Policy-based authorization
                var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
                if (authorizeAttributesWithPolicies.Any())
                {
                    foreach (var policyWithPropertyNames in authorizeAttributesWithPolicies.Select(a => (a.Policy, a.ProperiesNames)))
                    {
                        var authorized = false;

                        if (policyWithPropertyNames.ProperiesNames.Any())
                        {
                            Dictionary<string, object> resource = new Dictionary<string, object>();
                            foreach (var propertyName in policyWithPropertyNames.ProperiesNames)
                            {
                                resource[propertyName] = request.GetType().GetRuntimeProperty(propertyName).GetValue(request);
                            }

                            authorized = await _identity.AuthorizeRequirementAsync(_user.Id, resource, policyWithPropertyNames.Policy);
                        }
                        else
                        {
                            authorized = await _identity.AuthorizeAsync(_user.Id, policyWithPropertyNames.Policy);
                        }

                        if (!authorized)
                        {
                            throw new ForbiddenAccessException();
                        }
                    }
                }
            }

            // User is authorized / authorization not required
            return await next();
        }
    }
}
