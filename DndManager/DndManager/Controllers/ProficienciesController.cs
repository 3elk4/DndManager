using Application.Proficiency;
using Application.Proficiency.Commands.Create;
using Application.Proficiency.Commands.Delete;
using Application.Proficiency.Commands.Update;
using Application.Proficiency.Queries.Index;

namespace DndEncounter.Controllers
{
    public class ProficienciesController : Controller
    {
        private readonly IMediator _mediator;
        public ProficienciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("pcs/{pcid}/proficiencies")]
        [HttpGet]
        public async Task<ActionResult> Index(string pcid)
        {
            Guard.Against.Null(pcid);

            var request = new GetManyProficienciesByPcIdQuery() { PcId = pcid };
            var result = await _mediator.Send(request);

            ViewData["PcId"] = pcid;
            return View(result);
        }

        [Route("pcs/{pcid}/proficiencies/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProficiencyVM dndClassVM, [FromRoute] string pcid)
        {
            Guard.Against.Null(pcid);

            var request = new AddNewProficiencyCommand()
            {
                PcId = pcid,
                Name = dndClassVM.Name,
                Type = dndClassVM.Type,
            };
            var result = await _mediator.Send(request);

            TempData["Message"] = "Proficiency created successfully!";
            return RedirectToAction("Index", "Proficiencies", new { pcid = pcid });
        }


        [Route("pcs/{pcid}/proficiencies/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProficiencyVM dndClassVM, [FromRoute] string pcid, [FromRoute] string id)
        {
            Guard.Against.Null(pcid);
            Guard.Against.Null(id);

            var request = new UpdateProficiencyCommand()
            {
                Id = id,
                Name = dndClassVM.Name,
                Type = dndClassVM.Type
            };
            await _mediator.Send(request);

            TempData["Message"] = "Proficiency updated successfully!";
            return RedirectToAction("Index", "Proficiencies", new { pcid = pcid });
        }

        [Route("pcs/{pcid}/proficiencies/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string pcid, string id)
        {
            Guard.Against.Null(pcid);
            Guard.Against.Null(id);

            var request = new DeleteProficiencyCommand() { Id = id };
            await _mediator.Send(request);

            TempData["Message"] = "Proficiency deleted successfully!";
            return RedirectToAction("Index", new { pcid = pcid });
        }
    }
}
