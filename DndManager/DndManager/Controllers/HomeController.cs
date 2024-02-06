
using Application.Test.Queries.GetMany;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.ViewModels;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async System.Threading.Tasks.Task<IActionResult> Index()
        {
            var request = new GetManyTestsQuery();
            var result = await _mediator.Send(request);

            return View(result.Value);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Error/{statusCode}")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
          

            return View(new ErrorVM { StatusCode = statusCode, OriginalPath = feature?.OriginalPath });
        }
    }
}
