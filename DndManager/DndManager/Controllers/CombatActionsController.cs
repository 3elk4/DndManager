using Application.CombatAction;
using Application.CombatAction.Command.Create;
using Application.CombatAction.Commands.Delete;
using Application.CombatAction.Commands.Update;
using Application.CombatAction.Queries.Get;
using Application.CombatAction.Queries.GetManyByPcId;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IActionResult> Show(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetManyCombatActionsByPcIdQuery() { PcId = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["PcId"] = id;
            return View(result.Value);
        }

        //public IActionResult Edit(string id)
        //{
        //    if (id == null) return new BadRequestResult();

        //    var action = service.GetByIdWithInfo(id);

        //    ViewData["Abilities"] = FromModelToViewModel().CreateMapper().Map<ICollection<Ability>, List<AbilityVM>>(action.Pc.Info.Abilities);
        //    return PartialView("_CombatActionEdit", FromModelToViewModel().CreateMapper().Map<CombatAction, CombatActionVM>(action));
        
        //    return NotFound();

        //var request = new GetCombatActionByIdQuery() { Id = id };
        //var result = await _mediator.Send(request);

        //    if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

        //var vm = new PcCombatActionsVM() { PcId = id, CombatActions = result.Value.AsQueryable().ProjectTo<CombatActionVM>(_mapper.ConfigurationProvider).ToList() };
        //    return View(vm);
        //}

    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CombatActionVM CombatAction)
        { 
            if (!ModelState.IsValid) error = 2;
            else
            {
                var request = new UpdateCombatActionCommand() { };
                var result = await _mediator.Send(request);

                if (result.IsFailure) error = 1;
            }

            return RedirectToAction("Showasync", new { id = CombatAction.PcId, errorCode = error });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, [FromBody] string pcId)
        {
            var request = new DeleteCombatActionCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return Json(new { redirectToUrl = Url.Action("Show", "CombatActions", new { id = pcId }), error });
        }

        public async Task<IActionResult> NewCombatAction(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new AddNewCombatActionCommand() {
                PcId = id,
                Name = "",
                Type = "action",
                CombatAttack = new CombatAttackVM() { AdditionalBonus = 0, IsProficient = false, Range = "" },
                CombatDamage = new CombatDamageVM() { AdditionalBonus = 0, DamageDice = "", DamageType = "" },
                CombatSavingThrow = new CombatSavingThrowVM()
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            //ViewData["Abilities"] = FromModelToViewModel().CreateMapper().Map<ICollection<Ability>, List<AbilityVM>>(service.GetInfoByPcId(id).Abilities);
            return PartialView("_CombatActionEdit", result.Value);
        }
    }
}
