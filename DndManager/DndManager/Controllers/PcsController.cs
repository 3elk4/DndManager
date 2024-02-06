using Application.Bio;
using Application.DndClass;
using Application.Pc;
using Application.Pc.Commands.Create;
using Application.Pc.Commands.Delete;
using Application.Pc.Commands.Update;
using Application.Pc.Queries.Show;
using Application.Pc.Queries.Index;
using Application.SpellInfo;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Extentions;

namespace Presentation.Controllers
{
    //[Authorize]

    public class PcsController : Controller
    {
        private readonly IMediator _mediator;
        private int error = 0;
        public PcsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("pcs")]
        [ResponseCache(CacheProfileName = "Cache5minutes")]
        [HttpGet]
        public async Task<ActionResult> Index(string searchString, int pageNumber = 1)
        {
            var request = new GetManyPcsQuery() { SearchString = searchString, PageNumber = pageNumber, PageSize = 2};
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["CurrentFilter"] = searchString;
            return View(result.Value);
        }

        [Route("pcs/{id}")]
        [ResponseCache(CacheProfileName = "Cache5minutes", VaryByQueryKeys = new string[] { "id" })]
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetPcByIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            return View(result.Value);
        }

        [Route("pcs/create")]
        [HttpGet]
        public ActionResult Create()
        {
            return View(new PcCreatableVM() {
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
            if (!ModelState.IsValid) return View(pcVM);
            else
            {
                var request = new AddNewPcCommand() { 
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

                if (result.IsFailure) return StatusCode(500, $"{string.Join(',', result.Errors)}");
            }

            return RedirectToAction(nameof(Index), new { errorCode = error } );
        }

        [Route("pcs/{id}/edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetPcByIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");
            var pc = new PcEditableVM()
            {
                Id = result.Value.Id,
                Name = result.Value.Name,
                RaceName = result.Value.RaceName,
                BackgroundName = result.Value.BackgroundName,
                Inspiration = result.Value.Inspiration,
                AC = result.Value.AC,
                Speed = result.Value.Speed,
                HP = result.Value.HP,
                CurrentHP = result.Value.CurrentHP,
                TempHP = result.Value.TempHP,
                HitDice = result.Value.HitDice,
                Abilities = (IList<Application.Ability.AbilityVM>)result.Value.Abilities
            };

            return View("Edit", pc);
        }

        [Route("pcs/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PcEditableVM pcVM)
        {
            //if (!ModelState.IsValid) error = 2;
            //else
            //{

            //}
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
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return RedirectToAction("Details", new { id = pcVM.Id, errorCode = error });
        }

        [Route("pcs/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            var request = new DeletePcCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return RedirectToAction("Index", "Pcs");
        }
    }
}
