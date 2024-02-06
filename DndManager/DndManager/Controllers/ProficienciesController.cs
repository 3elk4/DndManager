using Application.Proficiency;
using Application.Proficiency.Commands.Create;
using Application.Proficiency.Commands.Delete;
using Application.Proficiency.Commands.Update;
using Application.Proficiency.Queries.Index;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DndEncounter.Controllers
{
    public class ProficienciesController : Controller
    {
        private readonly IMediator _mediator;
        private int error = 0;
        public ProficienciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("pcs/{pcid}/proficiencies")]
        [HttpGet]
        public async Task<ActionResult> Index(string pcid)
        {
            if (pcid == null) return new BadRequestResult();

            var request = new GetManyProficienciesByPcIdQuery() { PcId = pcid };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["PcId"] = pcid;
            return View(result.Value);
        }

        [Route("pcs/{pcid}/proficiencies/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProficiencyVM dndClassVM, [FromRoute] string pcid)
        {
            if (pcid == null) return new BadRequestResult();

            var request = new AddNewProficiencyCommand()
            {
                PcId = pcid,
                Name = dndClassVM.Name,
                Type = dndClassVM.Type,
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return StatusCode(500, $"{string.Join(',', result.Errors)}");

            return RedirectToAction("Index", "Proficiencies", new { errorCode = error, pcid = pcid });
        }


        [Route("pcs/{pcid}/proficiencies/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProficiencyVM dndClassVM, [FromRoute] string pcid, [FromRoute] string id)
        {
            if (pcid == null || id == null) return new BadRequestResult();

            var request = new UpdateProficiencyCommand()
            {
                Id = id,
                Name = dndClassVM.Name,
                Type = dndClassVM.Type
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return StatusCode(500, $"{string.Join(',', result.Errors)}");

            return RedirectToAction("Index", "Proficiencies", new { errorCode = error, pcid = pcid });
        }

        [Route("pcs/{pcid}/proficiencies/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string pcid, string id)
        {
            var request = new DeleteProficiencyCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return RedirectToAction("Index", new { pcid = pcid });
        }
    }
}
