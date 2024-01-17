using Application.Proficiency;
using Application.Proficiency.Commands.Create;
using Application.Proficiency.Commands.Delete;
using Application.Proficiency.Commands.UpdateMany;
using Application.Proficiency.Queries.GetManyByPcId;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetManyProficienciesByPcIdQuery() { PcId = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["PcId"] = id;
            return View(result.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(string id, [FromBody] List<ProficiencyVM> proficiencies)
        {
            if (!ModelState.IsValid) error = 2;
            else
            {
                var request = new UpdateManyProficienciesCommand() { Proficiencies = proficiencies };
                var result = await _mediator.Send(request);

                if (result.IsFailure) error = 1;
            }

            return Json(new { redirectToUrl = Url.Action("EditAsync", "Proficiencies", new { id = id }), error });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(string id, [FromBody] string pcId)
        {
            var request = new DeleteProficiencyCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return Json(new { redirectToUrl = Url.Action("EditAsync", "Proficienciess", new { id = pcId }), error });
        }

        public async Task<IActionResult> NewProficiencyAsync(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new AddNewProficiencyCommand() { PcId = id, Name = "", Type = "other" };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return PartialView("_ProficiencyEdit", result.Value);
        }
    }
}
