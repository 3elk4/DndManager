using Application.CombatAction;
using Application.CombatAction.Command.Create;
using Application.CombatAction.Commands.Delete;
using Application.CombatAction.Commands.Update;
using Application.CombatAction.Queries.GetManyByPcId;

namespace Presentation.Controllers
{
    public class CombatActionsController : Controller
    {
        private readonly IMediator _mediator;

        public CombatActionsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Route("pcs/{pcid}/actions")]
        [HttpGet]
        public async Task<IActionResult> Index(string pcid)
        {
            Guard.Against.Null(pcid);

            var request = new GetManyCombatActionsByPcIdQuery() { Id = pcid };
            var result = await _mediator.Send(request);

            ViewData["PcId"] = pcid;
            ViewData["Abilities"] = result.Abilities;

            return View(result);
        }

        [Route("pcs/{pcid}/actions/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CombatActionVM combatAction, [FromRoute] string pcid, [FromRoute] string id)
        {
            var request = new UpdateCombatActionCommand()
            {
                Id = id,
                Name = combatAction.Name,
                Type = combatAction.Type,
                CombatAttack = combatAction.CombatAttack,
                CombatDamage = combatAction.CombatDamage,
                CombatSavingThrow = combatAction.CombatSavingThrow
            };
            await _mediator.Send(request);

            TempData["Message"] = "Action updated successfully!";
            return RedirectToAction("Index", "CombatActions", new { pcid = pcid });
        }

        [Route("pcs/{pcid}/actions/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string pcid, [FromRoute] string id)
        {
            var request = new DeleteCombatActionCommand() { Id = id };
            await _mediator.Send(request);


            TempData["Message"] = "Action deleted successfully!";
            return RedirectToAction("Index", "CombatActions", new { pcid = pcid });
        }


        [Route("pcs/{pcid}/actions/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CombatActionVM combatAction, [FromRoute] string pcid)
        {
            Guard.Against.Null(pcid);

            var request = new AddNewCombatActionCommand()
            {
                PcId = pcid,
                Name = combatAction.Name,
                Type = combatAction.Type,
                CombatAttack = combatAction.CombatAttack,
                CombatDamage = combatAction.CombatDamage,
                CombatSavingThrow = combatAction.CombatSavingThrow
            };
            var result = await _mediator.Send(request);

            TempData["Message"] = "Action created successfully!";
            return RedirectToAction("Index", "CombatActions", new { pcid = pcid });
        }
    }
}
