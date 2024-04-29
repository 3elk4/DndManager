using Application.NpcProficiency;
using Application.NpcProficiency.Commands.Create;
using Application.NpcProficiency.Commands.Delete;
using Application.NpcProficiency.Commands.Update;
using Application.NpcProficiency.Queries.Index;

namespace Presentation.Controllers
{
    public class NpcProficienciesController : Controller
    {
        private readonly IMediator _mediator;
        public NpcProficienciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("npcs/{npcid}/proficiencies")]
        [HttpGet]
        public async Task<ActionResult> Index(string npcid)
        {
            Guard.Against.Null(npcid);

            var request = new GetManyProficienciesByNpcIdQuery() { Id = npcid };
            var result = await _mediator.Send(request);

            ViewData["NpcId"] = npcid;
            return View(result);
        }

        [Route("npcs/{npcid}/proficiencies/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NpcProficiencyVM dndClassVM, [FromRoute] string npcid)
        {
            Guard.Against.Null(npcid);

            var request = new AddNewProficiencyCommand()
            {
                NpcId = npcid,
                Name = dndClassVM.Name,
                Type = dndClassVM.Type,
                Range = dndClassVM.Range,
            };
            await _mediator.Send(request);

            TempData["Message"] = "Proficiency created successfully!";
            return RedirectToAction("Index", "NpcProficiencies", new { npcid = npcid });
        }


        [Route("npcs/{npcid}/proficiencies/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NpcProficiencyVM dndClassVM, [FromRoute] string npcid, [FromRoute] string id)
        {
            Guard.Against.Null(npcid);
            Guard.Against.Null(id);

            var request = new UpdateProficiencyCommand()
            {
                Id = id,
                Name = dndClassVM.Name,
                Type = dndClassVM.Type,
                Range = dndClassVM.Range,
            };
            await _mediator.Send(request);

            TempData["Message"] = "Proficiency updated successfully!";
            return RedirectToAction("Index", "NpcProficiencies", new { npcid = npcid });
        }

        [Route("npcs/{npcid}/proficiencies/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string npcid, string id)
        {
            Guard.Against.Null(npcid);
            Guard.Against.Null(id);

            var request = new DeleteProficiencyCommand() { Id = id };
            await _mediator.Send(request);


            TempData["Message"] = "Proficiency deleted successfully!";
            return RedirectToAction("Index", new { npcid = npcid });
        }
    }
}
