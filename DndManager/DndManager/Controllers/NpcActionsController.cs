using Application.NpcAction;
using Application.NpcAction.Commands.Create;
using Application.NpcAction.Commands.Delete;
using Application.NpcAction.Commands.Update;
using Application.NpcAction.Queries.Index;

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
            Guard.Against.Null(npcid);

            var request = new GetManyActionsByNpcIdQuery() { Id = npcid };
            var result = await _mediator.Send(request);

            ViewData["NpcId"] = npcid;
            return View(result);
        }

        [Route("npcs/{npcid}/actions/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NpcActionVM combatAction, [FromRoute] string npcid, [FromRoute] string id)
        {
            Guard.Against.Null(npcid);
            Guard.Against.Null(id);

            var request = new UpdateActionCommand()
            {
                Id = id,
                Name = combatAction.Name,
                Type = combatAction.Type,
                Description = combatAction.Description,
                Attack = combatAction.Attack,
                Damage = combatAction.Damage,
            };
            await _mediator.Send(request);

            TempData["Message"] = "Action updated successfully!";
            return RedirectToAction("Index", "NpcActions", new { npcid = npcid });
        }

        [Route("npcs/{npcid}/actions/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string npcid, [FromRoute] string id)
        {
            Guard.Against.Null(npcid);
            Guard.Against.Null(id);

            var request = new DeleteActionCommand() { Id = id };
            await _mediator.Send(request);

            TempData["Message"] = "Action deleted successfully!";
            return RedirectToAction("Index", "NpcActions", new { npcid = npcid });
        }


        [Route("npcs/{npcid}/actions/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NpcActionVM combatAction, [FromRoute] string npcid)
        {
            Guard.Against.Null(npcid);

            var request = new AddNewActionCommand()
            {
                NpcId = npcid,
                Name = combatAction.Name,
                Type = combatAction.Type,
                Description = combatAction.Description,
                Attack = combatAction.Attack,
                Damage = combatAction.Damage
            };
            await _mediator.Send(request);

            TempData["Message"] = "Action created successfully!";
            return RedirectToAction("Index", "NpcActions", new { npcid = npcid });
        }
    }
}
