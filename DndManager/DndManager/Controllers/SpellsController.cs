using Application.Spell;
using Application.Spell.Commands.Create;
using Application.Spell.Commands.Delete;
using Application.Spell.Commands.Update;
using Application.SpellInfo;
using Application.SpellInfo.Commands.Update;
using Application.SpellInfo.Queries.Get;
using Application.SpellLvlInfo;
using Application.SpellLvlInfo.Commands.Update;
using Application.SpellLvlInfo.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentantion.Controllers
{
    public class SpellsController : Controller
    {
        private readonly IMediator _mediator;
        private int error = 0;

        public SpellsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Route("spells/{id}")]
        [HttpGet]
        public async Task<ActionResult> Show(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetSpellInfoWithAbilitiesByPcIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["PcId"] = result.Value.SpellInfo.PcId;
            return View(result.Value);
        }

        [Route("spells/{id}/ability")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAbility(SpellInfoAndAbilitiesVM siaVM, [FromRoute] string id)
        {
            var request = new UpdateSpellInfoCommand() { Id = siaVM.SpellInfo.Id, AbilityId = siaVM.SpellInfo.AbilityId, };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return RedirectToAction("Show", new { id = id, errorCode = error });
        }


        [Route("spells/{spellinfoid}/lvl/{id}")]
        [HttpGet]
        public async Task<ActionResult> ShowSpellLvl(string spellinfoid, string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetSpellLvlInfoQuery() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["SpellInfoId"] = spellinfoid;
            ViewData["SpellLvlInfoId"] = id;
            return View(result.Value);
        }

        [Route("spells/{spellinfoid}/lvl/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSpellLvl(SpellLvlInfoVM sliVM, [FromRoute] string spellinfoid, [FromRoute] string id)
        {
            var request = new UpdateSpellLvlInfoCommand() { Id = id, Max = sliVM.Max, Remaining = sliVM.Remaining };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return RedirectToAction("ShowSpellLvl", new { spellinfoid = spellinfoid, id = id, errorCode = error });
        }

        [Route("spells/{spellinfoid}/lvl/{lvlid}/newspell")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewSpell(SpellVM spell, [FromRoute] string spellinfoid, [FromRoute] string lvlid)
        {
            if (lvlid == null) return new BadRequestResult();

            var request = new AddNewSpellCommand() { SpellLvlInfoId = lvlid, Name = spell.Name, CastingRange = spell.CastingRange, CastingTime = spell.CastingTime, Components = spell.Components, Duration = spell.Duration, Description = spell.Description };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return RedirectToAction("ShowSpellLvl", new { spellinfoid = spellinfoid, id = lvlid, errorCode = error });
        }

        [Route("spells/{spellinfoid}/lvl/{lvlid}/spell/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSpell(SpellVM spell, [FromRoute] string id, [FromRoute] string lvlid, [FromRoute] string spellinfoid)
        {
            var request = new UpdateSpellCommand() { Id = id, Name = spell.Name, CastingRange = spell.CastingRange, CastingTime = spell.CastingTime, Components = spell.Components, Duration = spell.Duration, Description = spell.Description };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return RedirectToAction("ShowSpellLvl", new { spellinfoid = spellinfoid, id = lvlid, errorCode = error });
        }

        [Route("spells/{spellinfoid}/lvl/{lvlid}/spell/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSpell(string id, [FromRoute] string lvlid, [FromRoute] string spellinfoid)
        {
            var request = new DeleteSpellCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return RedirectToAction("ShowSpellLvl", new { spellinfoid = spellinfoid, id = lvlid, errorCode = error });
        }
    }
}
