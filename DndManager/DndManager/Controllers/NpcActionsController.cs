using Application.NpcAction;
using Application.NpcAction.Commands.Create;
using Application.NpcAction.Commands.Delete;
using Application.NpcAction.Commands.Update;
using Application.NpcAction.Queries.Index;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class NpcActionsController : Controller
    {
        private readonly IMediator _mediator;

        public NpcActionsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Route("npcs/{npcid}/actions")]
        [HttpGet]
        public async Task<IActionResult> Index(string npcid)
        {
            if (npcid == null) return new BadRequestResult();

            var request = new GetManyActionsByNpcIdQuery() { NpcId = npcid };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["NpcId"] = npcid;
            return View(result.Value);
        }

        [Route("npcs/{npcid}/actions/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NpcActionVM combatAction, [FromRoute] string npcid, [FromRoute] string id)
        {
            var request = new UpdateActionCommand()
            {
                Id = id,
                Name = combatAction.Name,
                Type = combatAction.Type,
                Description = combatAction.Description,
                Attack = combatAction.Attack,
                Damage = combatAction.Damage,
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return new BadRequestResult();

            return RedirectToAction("Index", "NpcActions", new { npcid = npcid });
        }

        [Route("npcs/{npcid}/actions/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string npcid, [FromRoute] string id)
        {
            var request = new DeleteActionCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return new BadRequestResult();

            return RedirectToAction("Index", "NpcActions", new { npcid = npcid });
        }


        [Route("npcs/{npcid}/actions/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NpcActionVM combatAction, [FromRoute] string npcid)
        {
            if (npcid == null) return new BadRequestResult();

            var request = new AddNewActionCommand()
            {
                NpcId = npcid,
                Name = combatAction.Name,
                Type = combatAction.Type,
                Description = combatAction.Description,
                Attack = combatAction.Attack,
                Damage = combatAction.Damage
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return new BadRequestResult();

            return RedirectToAction("Index", "NpcActions", new { npcid = npcid });
        }
    }
}
