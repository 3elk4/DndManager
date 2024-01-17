using Application.Spell.Commands.Create;
using Application.Spell.Commands.Delete;
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

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetSpellInfoWithAbilitiesByPcIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["PcId"] = result.Value.SpellInfo.PcId;
            return View(result.Value);
        }

        public async Task<ActionResult> EditSpellLvlAsync(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetSpellLvlInfoQuery() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["SpellInfoId"] = result.Value.SpellInfoId;
            return View("_SpellLvlEdit", result.Value);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSpellLvlAsync([FromBody] SpellLvlInfoVM sliVM)
        {
            if (!ModelState.IsValid) error = 2;
            else
            {
                var request = new UpdateSpellLvlInfoCommand() {  };
                var result = await _mediator.Send(request);

                if (result.IsFailure) error = 1;
            }

            return Json(new { redirectToUrl = Url.Action("EditSpellLvl", "Spells", new { id = sliVM.Id }), error });
        }

        public async Task<ActionResult> EditAbilityAsync(string id)
        {
            var request = new GetSpellInfoWithAbilitiesByPcIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            return PartialView("_EditSpellAbility", result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAbilityAsync(SpellInfoAndAbilitiesVM siaVM)
        {

            if (!ModelState.IsValid) error = 2;
            else
            {
                var request = new UpdateSpellInfoCommand() { Id = siaVM.SpellInfo.Id, AbilityId = siaVM.SpellInfo.AbilityId, };
                var result = await _mediator.Send(request);

                if (result.IsFailure) error = 1;
            }

            return RedirectToAction("ShowAsync", new { id = siaVM.SpellInfo.PcId, errorCode = error });
        }

        public async Task<IActionResult> NewSpellAsync(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new AddNewSpellCommand() { SpellLvlInfoId = id, Name = "", CastingRange = "", CastingTime = "", Components = "", Duration = "", Description = "" };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return PartialView("_SpellEdit", result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSpellAsync(string id, [FromBody] string spelllvlInfoId)
        {
            var request = new DeleteSpellCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return Json(new { redirectToUrl = Url.Action("EditSpellLvlAsync", "Spells", new { id = spelllvlInfoId }), error });
        }
    }
}
