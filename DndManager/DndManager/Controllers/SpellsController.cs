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

namespace Presentantion.Controllers
{
    public class SpellsController : Controller
    {
        private readonly IMediator _mediator;

        public SpellsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Route("spells/{id}")]
        [HttpGet]
        public async Task<ActionResult> Show(string id)
        {
            Guard.Against.Null(id);

            var request = new GetSpellInfoWithAbilitiesByPcIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            ViewData["PcId"] = result.SpellInfo.PcId;
            return View(result);
        }

        [Route("spells/{id}/ability")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAbility(SpellInfoAndAbilitiesVM siaVM, [FromRoute] string id)
        {
            Guard.Against.Null(id);

            var request = new UpdateSpellInfoCommand() { Id = siaVM.SpellInfo.Id, AbilityId = siaVM.SpellInfo.AbilityId, };
            await _mediator.Send(request);

            TempData["Message"] = "Spell ability updated successfully!";
            return RedirectToAction("Show", new { id = id });
        }


        [Route("spells/{spellinfoid}/lvl/{id}")]
        [HttpGet]
        public async Task<ActionResult> ShowSpellLvl(string spellinfoid, string id)
        {
            Guard.Against.Null(id);

            var request = new GetSpellLvlInfoQuery() { Id = id };
            var result = await _mediator.Send(request);

            ViewData["SpellInfoId"] = spellinfoid;
            ViewData["SpellLvlInfoId"] = id;
            return View(result);
        }

        [Route("spells/{spellinfoid}/lvl/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSpellLvl(SpellLvlInfoVM sliVM, [FromRoute] string spellinfoid, [FromRoute] string id)
        {
            Guard.Against.Null(spellinfoid);
            Guard.Against.Null(id);

            var request = new UpdateSpellLvlInfoCommand() { Id = id, Max = sliVM.Max, Remaining = sliVM.Remaining };
            await _mediator.Send(request);

            TempData["Message"] = "Spell lvl updated successfully!";
            return RedirectToAction("ShowSpellLvl", new { spellinfoid = spellinfoid, id = id });
        }

        [Route("spells/{spellinfoid}/lvl/{lvlid}/newspell")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewSpell(SpellVM spell, [FromRoute] string spellinfoid, [FromRoute] string lvlid)
        {
            Guard.Against.Null(spellinfoid);
            Guard.Against.Null(lvlid);

            var request = new AddNewSpellCommand()
            {
                SpellLvlInfoId = lvlid,
                Name = spell.Name,
                CastingRange = spell.CastingRange,
                CastingTime = spell.CastingTime,
                Components = spell.Components,
                Duration = spell.Duration,
                Description = spell.Description,
                Target = spell.Target,
                School = spell.School,
                Concentration = spell.Concentration,
                Ritual = spell.Ritual,
                HigherLvl = spell.HigherLvl
            };
            var result = await _mediator.Send(request);

            TempData["Message"] = "Spell created successfully!";
            return RedirectToAction("ShowSpellLvl", new { spellinfoid = spellinfoid, id = lvlid });
        }

        [Route("spells/{spellinfoid}/lvl/{lvlid}/spell/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSpell(SpellVM spell, [FromRoute] string id, [FromRoute] string lvlid, [FromRoute] string spellinfoid)
        {
            Guard.Against.Null(id);
            Guard.Against.Null(lvlid);
            Guard.Against.Null(spellinfoid);

            var request = new UpdateSpellCommand()
            {
                Id = id,
                Name = spell.Name,
                CastingRange = spell.CastingRange,
                CastingTime = spell.CastingTime,
                Components = spell.Components,
                Duration = spell.Duration,
                Description = spell.Description,
                Target = spell.Target,
                School = spell.School,
                Concentration = spell.Concentration,
                Ritual = spell.Ritual,
                HigherLvl = spell.HigherLvl
            };
            await _mediator.Send(request);

            TempData["Message"] = "Spell updated successfully!";
            return RedirectToAction("ShowSpellLvl", new { spellinfoid = spellinfoid, id = lvlid });
        }

        [Route("spells/{spellinfoid}/lvl/{lvlid}/spell/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSpell(string id, [FromRoute] string lvlid, [FromRoute] string spellinfoid)
        {
            Guard.Against.Null(id);
            Guard.Against.Null(lvlid);
            Guard.Against.Null(spellinfoid);

            var request = new DeleteSpellCommand() { Id = id };
            await _mediator.Send(request);

            TempData["Message"] = "Spell deleted successfully!";
            return RedirectToAction("ShowSpellLvl", new { spellinfoid = spellinfoid, id = lvlid });
        }
    }
}
