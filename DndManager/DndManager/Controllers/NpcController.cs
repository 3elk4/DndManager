using Application.Common.Extentions;
using Application.Npc;
using Application.Npc.Commands.Create;
using Application.Npc.Commands.Delete;
using Application.Npc.Commands.Update;
using Application.Npc.Queries.Index;
using Application.Npc.Queries.Show;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class NpcController : Controller
    {
        private readonly IMediator _mediator;

        public NpcController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("npcs")]
        [ResponseCache(CacheProfileName = "Cache5minutes")]
        [HttpGet]
        public async Task<ActionResult> Index(string searchString, int pageNumber = 1)
        {
            var request = new GetManyNpcsQuery() { SearchString = searchString, PageNumber = pageNumber, PageSize = 2 };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["CurrentFilter"] = searchString;
            return View(result.Value);
        }

        [Route("npcs/{id}")]
        [ResponseCache(CacheProfileName = "Cache5minutes", VaryByQueryKeys = new string[] { "id" })]
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetNpcByIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            return View(result.Value);
        }

        [Route("npcs/create")]
        [HttpGet]
        public ActionResult Create()
        {
            return View(new NpcEditableVM()
            {
                Abilities = NpcHelper.GenerateAbilitiesWithSkills()
            });
        }

        [Route("npcs/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NpcEditableVM pcVM)
        {
            if (!ModelState.IsValid) return View(pcVM);
            else
            {
                var request = new AddNewNpcCommand()
                {
                    Name = pcVM.Name,
                    Image = pcVM.Photo.PhotoIntoByteImage(),
                    Type = pcVM.Type,
                    Size = pcVM.Size,
                    Alignment =pcVM.Alignment,
                    AC = pcVM.AC,
                    AcType = pcVM.AcType,
                    HP = pcVM.HP,
                    HpFormula = pcVM.HpFormula,
                    Speed = pcVM.Speed,
                    ProficiencyBonus = pcVM.ProficiencyBonus,
                    PassivePerception = pcVM.PassivePerception,
                    Challange = pcVM.Challange,
                    ChallangeExp = pcVM.ChallangeExp,
                    Abilities = pcVM.Abilities
                };
                var result = await _mediator.Send(request);

                if (result.IsFailure) return StatusCode(500, $"{string.Join(',', result.Errors)}");
            }

            return RedirectToAction(nameof(Index));
        }

        [Route("pcs/{id}/edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetNpcByIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");
            var npc = new NpcEditableVM()
            {
                Id = result.Value.Id,
                Name = result.Value.Name,
                Type = result.Value.Type,
                Size = result.Value.Size,
                Alignment = result.Value.Alignment,
                AC = result.Value.AC,
                AcType = result.Value.AcType,
                HP = result.Value.HP,
                HpFormula = result.Value.HpFormula,
                Speed = result.Value.Speed,
                ProficiencyBonus = result.Value.ProficiencyBonus,
                PassivePerception = result.Value.PassivePerception,
                Challange = result.Value.Challange,
                ChallangeExp = result.Value.ChallangeExp,
                Abilities = (IList<Application.NpcAbility.NpcAbilityVM>)result.Value.Abilities,
            };

            return View("Edit", npc);
        }

        [Route("pcs/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NpcEditableVM pcVM)
        {
            var request = new UpdateNpcCommand()
            {
                Id = pcVM.Id,
                Name = pcVM.Name,
                Image = pcVM.Photo.PhotoIntoByteImage(),
                Type = pcVM.Type,
                Size = pcVM.Size,
                Alignment = pcVM.Alignment,
                AC = pcVM.AC,
                AcType = pcVM.AcType,
                HP = pcVM.HP,
                HpFormula = pcVM.HpFormula,
                Speed = pcVM.Speed,
                ProficiencyBonus = pcVM.ProficiencyBonus,
                PassivePerception = pcVM.PassivePerception,
                Challange = pcVM.Challange,
                ChallangeExp = pcVM.ChallangeExp,
                Abilities = pcVM.Abilities
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return BadRequest();

            return RedirectToAction("Details", new { id = pcVM.Id });
        }

        [Route("pcs/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            var request = new DeleteNpcCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return BadRequest();

            return RedirectToAction("Index", "Pcs");
        }
    }
}
