using Application.Feat;
using Application.Feat.Command.Create;
using Application.Feat.Command.Delete;
using Application.Feat.Command.Update;
using Application.Feat.Queries.Index;

namespace Presentation.Controllers
{
    public class FeatsController : Controller
    {
        private readonly IMediator _mediator;
        public FeatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("pcs/{pcid}/feats")]
        [HttpGet]
        public async Task<ActionResult> Index(string pcid)
        {
            Guard.Against.Null(pcid);

            var request = new GetManyFeatsByPcIdQuery() { Id = pcid };
            var result = await _mediator.Send(request);

            ViewData["PcId"] = pcid;
            return View(result);
        }

        [Route("pcs/{pcid}/feats/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeatVM dndClassVM, [FromRoute] string pcid)
        {
            Guard.Against.Null(pcid);

            var request = new AddNewFeatCommand()
            {
                PcId = pcid,
                Title = dndClassVM.Title,
                Source = dndClassVM.Source,
                SourceType = dndClassVM.SourceType,
                Definition = dndClassVM.Definition
            };
            var result = await _mediator.Send(request);

            TempData["Message"] = "Feat created successfully!";
            return RedirectToAction("Index", "Feats", new { pcid = pcid });
        }


        [Route("pcs/{pcid}/feats/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FeatVM dndClassVM, [FromRoute] string pcid, [FromRoute] string id)
        {
            Guard.Against.Null(pcid);
            Guard.Against.Null(id);

            var request = new UpdateFeatCommand()
            {
                Id = id,
                Title = dndClassVM.Title,
                Source = dndClassVM.Source,
                SourceType = dndClassVM.SourceType,
                Definition = dndClassVM.Definition
            };
            await _mediator.Send(request);

            TempData["Message"] = "Feat updated successfully!";
            return RedirectToAction("Index", "Feats", new { pcid = pcid });
        }

        [Route("pcs/{pcid}/feats/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string pcid, string id)
        {
            Guard.Against.Null(pcid);
            Guard.Against.Null(id);

            var request = new DeleteFeatCommand() { Id = id };
            await _mediator.Send(request);

            TempData["Message"] = "Feat deleted successfully!";
            return RedirectToAction("Index", new { pcid = pcid });
        }
    }
}
