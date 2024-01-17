using Application.Bio;
using Application.Bio.Commands.Update;
using Application.Bio.Queries.Get;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetBioByIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            return View(result.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BioVM bio)
        {
            if (!ModelState.IsValid) return View(bio);

            var request = new UpdateBioCommand() { };
            var result = await _mediator.Send(request);

            if(result.IsFailure) error = 1;

            return RedirectToAction("Edit", bio.Id);
        }
    }
}
