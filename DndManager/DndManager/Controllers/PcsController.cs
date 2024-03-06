using Application.Bio;
using Application.Common.Extentions;
using Application.DndClass;
using Application.Pc;
using Application.Pc.Commands.Create;
using Application.Pc.Commands.Delete;
using Application.Pc.Commands.Update;
using Application.Pc.Queries.Index;
using Application.Pc.Queries.Show;
using Application.SpellInfo;
using Presentation.Helpers;
using System.Collections.Generic;

namespace Presentation.Controllers
{
    //[Authorize]

    public class PcsController : Controller
    {
        private readonly IMediator _mediator;

        public PcsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("pcs")]
        [ResponseCache(CacheProfileName = "Cache5minutes")]
        [HttpGet]
        public async Task<ActionResult> Index(string searchString, int pageNumber = 1)
        {
            var request = new GetManyPcsQuery() { SearchString = searchString, PageNumber = pageNumber, PageSize = 2 };
            var result = await _mediator.Send(request);

            ViewData["CurrentFilter"] = searchString;
            return View(result);
        }

        [Route("pcs/{id}")]
        [ResponseCache(CacheProfileName = "Cache5minutes", VaryByQueryKeys = new string[] { "id" })]
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            Guard.Against.Null(id);

            var request = new GetPcByIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            return View(result);
        }

        [Route("pcs/create")]
        [HttpGet]
        public ActionResult Create()
        {
            return View(new PcCreatableVM()
            {
                Abilities = PcHelper.GenerateAbilitiesWithSkills(),
                DndClasses = new List<DndClassVM>() { new DndClassVM() },
                Bio = new BioVM()
            });
        }

        [Route("pcs/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PcCreatableVM pcVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Some errors occured during creating pc.";
                return View(pcVM);
            }

            var request = new AddNewPcCommand()
            {
                Name = pcVM.Name,
                Image = pcVM.Photo.PhotoIntoByteImage(),
                RaceName = pcVM.RaceName,
                BackgroundName = pcVM.BackgroundName,
                AC = pcVM.AC,
                Speed = pcVM.Speed,
                HP = pcVM.HP,
                HitDice = pcVM.HitDice,
                Abilities = pcVM.Abilities,
                DndClasses = pcVM.DndClasses,
                Bio = pcVM.Bio,
                SpellInfo = new SpellInfoVM() { SpellLvls = PcHelper.GenerateSpellLvls() }
            };
            var result = await _mediator.Send(request);

            TempData["Message"] = "Pc created successfully!";
            return RedirectToAction(nameof(Index));
        }

        [Route("pcs/{id}/edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Guard.Against.Null(id);

            var request = new GetPcByIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            var pc = new PcEditableVM()
            {
                Id = result.Id,
                Name = result.Name,
                RaceName = result.RaceName,
                BackgroundName = result.BackgroundName,
                Inspiration = result.Inspiration,
                AC = result.AC,
                Speed = result.Speed,
                HP = result.HP,
                CurrentHP = result.CurrentHP,
                TempHP = result.TempHP,
                HitDice = result.HitDice,
                Abilities = (IList<Application.Ability.AbilityVM>)result.Abilities
            };

            return View("Edit", pc);
        }

        [Route("pcs/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PcEditableVM pcVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Some errors occured during updating pc.";
                return View(pcVM);
            }

            var request = new UpdatePcCommand()
            {
                Id = pcVM.Id,
                Name = pcVM.Name,
                Image = pcVM.Photo.PhotoIntoByteImage(),
                RaceName = pcVM.RaceName,
                BackgroundName = pcVM.BackgroundName,
                Inspiration = pcVM.Inspiration,
                AC = pcVM.AC,
                Speed = pcVM.Speed,
                HP = pcVM.HP,
                CurrentHP = pcVM.CurrentHP,
                TempHP = pcVM.TempHP,
                HitDice = pcVM.HitDice,
                Abilities = pcVM.Abilities,
            };
            await _mediator.Send(request);

            TempData["Message"] = "Pc updated successfully!";
            return RedirectToAction("Details", new { id = pcVM.Id });
        }

        [Route("pcs/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            Guard.Against.Null(id);

            var request = new DeletePcCommand() { Id = id };
            await _mediator.Send(request);

            TempData["Message"] = "Pc deleted successfully!";
            return RedirectToAction("Index", "Pcs");
        }
    }
}
