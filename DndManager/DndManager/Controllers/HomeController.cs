using Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Presentation.ViewModels;

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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            int code = 500;
            var errorVM = new ErrorVM { StatusCode = 500, Message = "Internal server error." };

            if (exception is NotFoundException)
            {
                code = 404;
                errorVM.Message = "Link may be broken or page was not found.";
            }
            else if (exception is UnauthorizedAccessException)
            {
                code = 401;
                errorVM.Message = "Unathorized. Log in or register to see details.";
            }
            else if (exception is ForbiddenAccessException)
            {
                code = 403;
                errorVM.Message = "Forbidden.";
            }
            else if (exception is Exception)
            {
                code = 400;
                errorVM.Message = "Bad Request.";
            }

            errorVM.StatusCode = code;
            Response.StatusCode = code;

            return View(errorVM);
        }
    }
}
