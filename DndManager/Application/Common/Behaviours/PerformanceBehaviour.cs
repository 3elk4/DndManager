using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> //: IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        //private readonly Stopwatch _timer;
        //private readonly ILogger<TRequest> _logger;
        ////private readonly IUser _user;
        ////private readonly IIdentity _identity;

        //public PerformanceBehaviour(
        //    ILogger<TRequest> logger,
        //    IUser user,
        //    IIdentity identity)
        //{
        //    _timer = new Stopwatch();

        //    _logger = logger;
        //    //_user = user;
        //    //_identity = identity;
        //}

        //public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        //{
        //    _timer.Start();

        //    var response = await next();

        //    _timer.Stop();

        //    var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        //    if (elapsedMilliseconds > 500)
        //    {
        //        var requestName = typeof(TRequest).Name;
        //       // var userId = _user.Id ?? string.Empty;
        //        var userName = string.Empty;

        //        //if (!string.IsNullOrEmpty(userId))
        //        //{
        //        //    userName = await _identity.GetUserNameAsync(userId);
        //        //}

        //        _logger.LogWarning("CleanArchitecture Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
        //            requestName, elapsedMilliseconds, userId, userName, request);
        //    }

        //    return response;
        //}
    }
}
