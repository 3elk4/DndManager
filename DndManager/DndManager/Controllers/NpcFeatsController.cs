using Application.NpcFeat;
using Application.NpcFeat.Commands.Create;
using Application.NpcFeat.Commands.Delete;
using Application.NpcFeat.Commands.Update;
using Application.NpcFeat.Queries.Index;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            if (npcid == null) return new BadRequestResult();

            var request = new GetManyFeatsByNpcIdQuery() { NpcId = npcid };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["NpcId"] = npcid;
            return View(result.Value);
        }

        [Route("npcs/{npcid}/feats/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NpcFeatVM dndClassVM, [FromRoute] string npcid)
        {
            if (npcid == null) return new BadRequestResult();

            var request = new AddNewFeatCommand()
            {
                NpcId = npcid,
                Name = dndClassVM.Name,
                Description = dndClassVM.Description,
                TimeRegeneration = dndClassVM.TimeRegeneration
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return StatusCode(500, $"{string.Join(',', result.Errors)}");

            return RedirectToAction("Index", "NpcFeats", new { npcid = npcid });
        }


        [Route("npcs/{npcid}/feats/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NpcFeatVM dndClassVM, [FromRoute] string npcid, [FromRoute] string id)
        {
            if (npcid == null || id == null) return new BadRequestResult();

            var request = new UpdateFeatCommand()
            {
                Id = id,
                Name = dndClassVM.Name,
                Description = dndClassVM.Description,
                TimeRegeneration = dndClassVM.TimeRegeneration
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return StatusCode(500, $"{string.Join(',', result.Errors)}");

            return RedirectToAction("Index", "NpcFeats", new { npcid = npcid });
        }

        [Route("npcs/{npcid}/feats/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string npcid, string id)
        {
            var request = new DeleteFeatCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return BadRequest();

            return RedirectToAction("Index", new { npcid = npcid });
        }
    }
}
