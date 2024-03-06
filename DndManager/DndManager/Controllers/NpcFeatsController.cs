using Application.NpcFeat;
using Application.NpcFeat.Commands.Create;
using Application.NpcFeat.Commands.Delete;
using Application.NpcFeat.Commands.Update;
using Application.NpcFeat.Queries.Index;

namespace Presentation.Controllers
{
    public class NpcFeatsController : Controller
    {
        private readonly IMediator _mediator;
        public NpcFeatsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Route("npcs/{npcid}/feats")]
        [HttpGet]
        public async Task<ActionResult> Index(string npcid)
        {
            Guard.Against.Null(npcid);

            var request = new GetManyFeatsByNpcIdQuery() { NpcId = npcid };
            var result = await _mediator.Send(request);

            ViewData["NpcId"] = npcid;
            return View(result);
        }

        [Route("npcs/{npcid}/feats/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NpcFeatVM dndClassVM, [FromRoute] string npcid)
        {
            Guard.Against.Null(npcid);

            var request = new AddNewFeatCommand()
            {
                NpcId = npcid,
                Name = dndClassVM.Name,
                Description = dndClassVM.Description,
                TimeRegeneration = dndClassVM.TimeRegeneration
            };
            await _mediator.Send(request);

            TempData["Message"] = "Feat created successfully!";
            return RedirectToAction("Index", "NpcFeats", new { npcid = npcid });
        }


        [Route("npcs/{npcid}/feats/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NpcFeatVM dndClassVM, [FromRoute] string npcid, [FromRoute] string id)
        {
            Guard.Against.Null(npcid);
            Guard.Against.Null(id);

            var request = new UpdateFeatCommand()
            {
                Id = id,
                Name = dndClassVM.Name,
                Description = dndClassVM.Description,
                TimeRegeneration = dndClassVM.TimeRegeneration
            };
            await _mediator.Send(request);

            TempData["Message"] = "Feat updated successfully!";
            return RedirectToAction("Index", "NpcFeats", new { npcid = npcid });
        }

        [Route("npcs/{npcid}/feats/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string npcid, string id)
        {
            Guard.Against.Null(npcid);
            Guard.Against.Null(id);

            var request = new DeleteFeatCommand() { Id = id };
            await _mediator.Send(request);

            TempData["Message"] = "Feat deleted successfully!";
            return RedirectToAction("Index", new { npcid = npcid });
        }
    }
}
