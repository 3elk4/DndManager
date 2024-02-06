using Application.CombatAction;
using Application.CombatAction.Command.Create;
using Application.CombatAction.Commands.Delete;
using Application.CombatAction.Commands.Update;
using Application.CombatAction.Queries.GetManyByPcId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class CombatActionsController : Controller
    {
        private readonly IMediator _mediator;
        private int error = 0;

        public CombatActionsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Route("pcs/{pcid}/actions")]
        [HttpGet]
        public async Task<IActionResult> Index(string pcid)
        {
            if (pcid == null) return new BadRequestResult();

            var request = new GetManyCombatActionsByPcIdQuery() { PcId = pcid };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["PcId"] = pcid;
            ViewData["Abilities"] = result.Value.Abilities;
            return View(result.Value);
        }

        [Route("pcs/{pcid}/actions/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CombatActionVM combatAction, [FromRoute] string pcid, [FromRoute] string id)
        {
            var request = new UpdateCombatActionCommand() {
                Id = id,
                Name = combatAction.Name,
                Type = combatAction.Type,
                CombatAttack = combatAction.CombatAttack,
                CombatDamage = combatAction.CombatDamage,
                CombatSavingThrow = combatAction.CombatSavingThrow
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return RedirectToAction("Index", "CombatActions",  new { pcid = pcid, errorCode = error });
        }

        [Route("pcs/{pcid}/actions/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string pcid, [FromRoute] string id)
        {
            var request = new DeleteCombatActionCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return RedirectToAction("Index", "CombatActions", new { pcid = pcid, errorCode = error });
        }


        [Route("pcs/{pcid}/actions/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CombatActionVM combatAction, [FromRoute] string pcid)
        {
            if (pcid == null) return new BadRequestResult();

            var request = new AddNewCombatActionCommand() {
                PcId = pcid,
                Name = combatAction.Name,
                Type = combatAction.Type,
                CombatAttack = combatAction.CombatAttack,
                CombatDamage = combatAction.CombatDamage,
                CombatSavingThrow = combatAction.CombatSavingThrow
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return RedirectToAction("Index", "CombatActions", new { pcid = pcid, errorCode = error });
        }
    }
}
