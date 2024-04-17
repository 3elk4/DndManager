using Application.Common.Extentions;
using Application.Npc;
using Application.Npc.Commands.Create;
using Application.Npc.Commands.Delete;
using Application.Npc.Commands.Update;
using Application.Npc.Queries.GeneratePdf;
using Application.Npc.Queries.Index;
using Application.Npc.Queries.Show;
using Presentation.Helpers;
using System.Collections.Generic;

namespace Presentation.Controllers
{
    public class NpcsController : Controller
    {
        private readonly IMediator _mediator;

        public NpcsController(IMediator mediator)
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

            ViewData["CurrentFilter"] = searchString;
            return View(result);
        }

        [Route("npcs/{id}")]
        [ResponseCache(CacheProfileName = "Cache5minutes", VaryByQueryKeys = new string[] { "id" })]
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            Guard.Against.Null(id);

            var request = new GetNpcByIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            return View(result);
        }

        [Route("npcs/{id}/pdf")]
        [HttpGet]
        public async Task<ActionResult> SaveAsPdfAsync(string id)
        {
            Guard.Against.Null(id);

            var request = new GenerateNpcPdfQuery() { Id = id };
            var result = await _mediator.Send(request);

            return new FileStreamResult(result.MemoryStream, "application/pdf") { FileDownloadName = result.Filename };
        }


        [Route("npcs/create")]
        [HttpGet]
        public ActionResult Create()
        {
            return View(new NpcEditableVM()
            {
                Abilities = NpcHelper.GenerateAbilitiesWithSkills(),
                SpellInfo = new Application.NpcSpellInfo.NpcSpellInfoVM()
            });
        }

        [Route("npcs/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NpcEditableVM npcVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Some errors occured during creating npc.";
                return View(npcVM);
            }

            var request = new AddNewNpcCommand()
            {
                Name = npcVM.Name,
                Image = npcVM.Photo.PhotoIntoByteImage(),
                Type = npcVM.Type,
                Size = npcVM.Size,
                Alignment = npcVM.Alignment,
                AC = npcVM.AC,
                AcType = npcVM.AcType,
                HP = npcVM.HP,
                HpFormula = npcVM.HpFormula,
                Speed = npcVM.Speed,
                ProficiencyBonus = npcVM.ProficiencyBonus,
                PassivePerception = npcVM.PassivePerception,
                Challange = npcVM.Challange,
                ChallangeXp = npcVM.ChallangeXp,
                Abilities = npcVM.Abilities,
                SpellInfo = npcVM.SpellInfo
            };
            await _mediator.Send(request);

            TempData["Message"] = "Npc created successfully!";
            return RedirectToAction(nameof(Index));
        }

        [Route("npcs/{id}/edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Guard.Against.Null(id);

            var request = new GetNpcByIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            var npc = new NpcEditableVM()
            {
                Id = result.Id,
                Name = result.Name,
                Type = result.Type,
                Size = result.Size,
                Alignment = result.Alignment,
                AC = result.AC,
                AcType = result.AcType,
                HP = result.HP,
                HpFormula = result.HpFormula,
                Speed = result.Speed,
                ProficiencyBonus = result.ProficiencyBonus,
                PassivePerception = result.PassivePerception,
                Challange = result.Challange,
                ChallangeXp = result.ChallangeXp,
                Abilities = (IList<Application.NpcAbility.NpcAbilityVM>)result.Abilities,
                SpellInfo = result.SpellInfo
            };

            return View("Edit", npc);
        }

        [Route("npcs/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NpcEditableVM npcVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Some errors occured during updating npc.";
                return View(npcVM);
            }

            var request = new UpdateNpcCommand()
            {
                Id = npcVM.Id,
                Name = npcVM.Name,
                Image = npcVM.Photo.PhotoIntoByteImage(),
                Type = npcVM.Type,
                Size = npcVM.Size,
                Alignment = npcVM.Alignment,
                AC = npcVM.AC,
                AcType = npcVM.AcType,
                HP = npcVM.HP,
                HpFormula = npcVM.HpFormula,
                Speed = npcVM.Speed,
                ProficiencyBonus = npcVM.ProficiencyBonus,
                PassivePerception = npcVM.PassivePerception,
                Challange = npcVM.Challange,
                ChallangeXp = npcVM.ChallangeXp,
                Abilities = npcVM.Abilities,
                SpellInfo = npcVM.SpellInfo
            };
            await _mediator.Send(request);

            TempData["Message"] = "Npc updated successfully!";
            return RedirectToAction("Details", new { id = npcVM.Id });
        }

        [Route("npcs/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            Guard.Against.Null(id);

            var request = new DeleteNpcCommand() { Id = id };
            await _mediator.Send(request);

            TempData["Message"] = "Npc deleted successfully!";
            return RedirectToAction("Index", "Npcs");
        }
    }
}
