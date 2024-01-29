using Application.Feat;
using Application.Feat.Command.Create;
using Application.Feat.Command.Delete;
using Application.Feat.Command.UpdateMany;
using Application.Feat.Queries.GetManyByPcId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public async Task<ActionResult> Show(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetManyFeatsByPcIdQuery() { PcId = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["PcId"] = id;
            return View(result.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, [FromBody] List<FeatVM> feats)
        {
            if (!ModelState.IsValid) error = 2;
            else
            {
                var request = new UpdateManyFeatsCommand() { Feats = feats };
                var result = await _mediator.Send(request);

                if (result.IsFailure) error = 1;
            }

            return Json(new { redirectToUrl = Url.Action("Show", "Feats", new { id = id }), error });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, [FromBody] string pcId)
        {
            var request = new DeleteFeatCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return Json(new { redirectToUrl = Url.Action("Show", "Feats", new { id = pcId }), error });
        }

        public async Task<IActionResult> NewFeat(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new AddNewFeatCommand() { PcId = id, Title = "", Source = "other", SourceType = "", Definition = "" };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return PartialView("_FeatEdit", result.Value);
        }
    }
}
