using Application.NpcProficiency;
using Application.NpcProficiency.Commands.Create;
using Application.NpcProficiency.Commands.Delete;
using Application.NpcProficiency.Commands.Update;
using Application.NpcProficiency.Queries.Index;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            if (npcid == null) return new BadRequestResult();

            var request = new GetManyProficienciesByNpcIdQuery() { NpcId = npcid };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["NpcId"] = npcid;
            return View(result.Value);
        }

        [Route("npcs/{npcid}/proficiencies/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NpcProficiencyVM dndClassVM, [FromRoute] string npcid)
        {
            if (npcid == null) return new BadRequestResult();

            var request = new AddNewProficiencyCommand()
            {
                NpcId = npcid,
                Name = dndClassVM.Name,
                Type = dndClassVM.Type,
                Range = dndClassVM.Range,
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return StatusCode(500, $"{string.Join(',', result.Errors)}");

            return RedirectToAction("Index", "NpcProficiencies", new { npcid = npcid });
        }


        [Route("npcs/{npcid}/proficiencies/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NpcProficiencyVM dndClassVM, [FromRoute] string npcid, [FromRoute] string id)
        {
            if (npcid == null || id == null) return new BadRequestResult();

            var request = new UpdateProficiencyCommand()
            {
                Id = id,
                Name = dndClassVM.Name,
                Type = dndClassVM.Type,
                Range = dndClassVM.Range,
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return StatusCode(500, $"{string.Join(',', result.Errors)}");

            return RedirectToAction("Index", "NpcProficiencies", new { npcid = npcid });
        }

        [Route("npcs/{npcid}/proficiencies/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string npcid, string id)
        {
            var request = new DeleteProficiencyCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return new BadRequestResult();

            return RedirectToAction("Index", new { npcid = npcid });
        }
    }
}
