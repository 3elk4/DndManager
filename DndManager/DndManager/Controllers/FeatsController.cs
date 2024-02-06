using Application.Feat;
using Application.Feat.Command.Create;
using Application.Feat.Command.Delete;
using Application.Feat.Command.Update;
using Application.Feat.Queries.Index;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class FeatsController : Controller
    {
        private readonly IMediator _mediator;
        private int error = 0;
        public FeatsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Route("pcs/{pcid}/feats")]
        [HttpGet]
        public async Task<ActionResult> Index(string pcid)
        {
            if (pcid == null) return new BadRequestResult();

            var request = new GetManyFeatsByPcIdQuery() { PcId = pcid };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["PcId"] = pcid;
            return View(result.Value);
        }

        [Route("pcs/{pcid}/feats/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeatVM dndClassVM, [FromRoute] string pcid)
        {
            if (pcid == null) return new BadRequestResult();

            var request = new AddNewFeatCommand()
            {
                PcId = pcid,
                Title = dndClassVM.Title,
                Source = dndClassVM.Source,
                SourceType = dndClassVM.SourceType,
                Definition = dndClassVM.Definition
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return StatusCode(500, $"{string.Join(',', result.Errors)}");

            return RedirectToAction("Index", "Feats", new { errorCode = error, pcid = pcid });
        }


        [Route("pcs/{pcid}/feats/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FeatVM dndClassVM, [FromRoute] string pcid, [FromRoute] string id)
        {
            if (pcid == null || id == null) return new BadRequestResult();

            var request = new UpdateFeatCommand()
            {
                Id = id,
                Title = dndClassVM.Title,
                Source = dndClassVM.Source,
                SourceType = dndClassVM.SourceType,
                Definition = dndClassVM.Definition
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return StatusCode(500, $"{string.Join(',', result.Errors)}");

            return RedirectToAction("Index", "Feats", new { errorCode = error, pcid = pcid });
        }

        [Route("pcs/{pcid}/feats/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string pcid, string id)
        {
            var request = new DeleteFeatCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return RedirectToAction("Index", new { pcid = pcid });
        }
    }
}
