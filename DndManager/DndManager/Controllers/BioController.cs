using Application.Bio;
using Application.Bio.Commands.Update;
using Application.Bio.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class BioController : Controller
    {
        private readonly IMediator _mediator;
        private int error = 0;

        public BioController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Route("bio/{id}")]
        [HttpGet]
        public async Task<ActionResult> Show(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetBioByIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            return View(result.Value);
        }

        [Route("bio/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BioVM bio)
        {
            if (!ModelState.IsValid) return View("Show", bio);

            var request = new UpdateBioCommand() { Bio = bio };
            var result = await _mediator.Send(request);

            if(result.IsFailure) error = 1;

            return RedirectToAction("Show", "Bio", new RouteValueDictionary() { { "id", bio.Id } });
        }
    }
}
