﻿namespace Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> //: IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        //private readonly ILogger _logger;
        //private readonly IUser _user;
        //private readonly IIdentity _identity;

        //public LoggingBehaviour(ILogger<TRequest> logger, IUser user, IIdentity identity)
        //{
        //    _logger = logger;
        //    _user = user;
        //    _identity = identity;
        //}

        //public async Task Process(TRequest request, CancellationToken cancellationToken)
        //{
        //    var requestName = typeof(TRequest).Name;
        //    var userId = _user.Id ?? string.Empty;
        //    string? userName = string.Empty;

        //    if (!string.IsNullOrEmpty(userId))
        //    {
        //        userName = await _identity.GetUserNameAsync(userId);
        //    }

        //    _logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}", requestName, userId, userName, request);
        //}
    }
}
