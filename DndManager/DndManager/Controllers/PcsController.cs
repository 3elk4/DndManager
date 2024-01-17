using Application.Bio;
using Application.DndClass;
using Application.Pc;
using Application.Pc.Commands.Create;
using Application.Pc.Commands.Delete;
using Application.Pc.Commands.Update;
using Application.Pc.Queries.Get;
using Application.Pc.Queries.GetMany;
using Application.SpellInfo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        // GET: PcsController
        public async Task<ActionResult> Index()
        {
            var request = new GetManyPcsQuery();
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            return View(result.Value);
        }

        // GET: PcsController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetPcByIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            return View(result.Value);
        }

        //// GET: PcsController/Create
        public ActionResult Create()
        {
            return View(new PcVM() {
                Abilities = PcHelper.GenerateAbilitiesWithSkills(),
                DndClasses = new List<DndClassVM>() { new DndClassVM() },
                Bio = new BioVM(),
                SpellInfo = new SpellInfoVM() { SpellLvls = PcHelper.GenerateSpellLvls() } //todo: in formual choose cast ability
            });
        }

        // POST: PcsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PcVM pcVM)
        {
            if (!ModelState.IsValid) error = 2;
            else
            {
                var request = new AddNewPcCommand() { 
                    Name = pcVM.Name,
                    //Image = PcHelper.PhotoIntoByteImage(pcVM.Image),
                    RaceName = pcVM.RaceName,
                    BackgroundName = pcVM.BackgroundName,
                    AC = pcVM.AC,
                    Speed = pcVM.Speed,
                    HP = pcVM.HP,
                    HitDice = pcVM.HitDice,
                    Abilities = pcVM.Abilities,
                    DndClasses = pcVM.DndClasses,
                    Bio = pcVM.Bio,
                    SpellInfo = pcVM.SpellInfo
                };
                var result = await _mediator.Send(request);

                if (result.IsFailure) error = 1;
            }

            return RedirectToAction(nameof(Index), new { error = error } );
        }

        // GET: PcsController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetPcByIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            return PartialView("_Edit", result.Value);
        }

        // POST: PcsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PcVM pcVM)
        {
            if (!ModelState.IsValid) error = 2;
            else
            {
                var request = new UpdatePcCommand()
                {
                    Name = pcVM.Name,
                    //Image = PcHelper.PhotoIntoByteImage(pcVM.Image),
                    RaceName = pcVM.RaceName,
                    BackgroundName = pcVM.BackgroundName
                };
                var result = await _mediator.Send(request);

                if (result.IsFailure) error = 1;
            }

            return RedirectToAction("Details", new { id = pcVM.Id, errorCode = error });
        }

        //public ActionResult EditInfo(string id)
        //{
        //    if (id == null) return new BadRequestResult();

        //    var info = service.GetInfoById(id);
        //    if (info == null) return NotFound();

        //    return PartialView("_EditInfo", EditInfoConfigToViewModel().CreateMapper().Map<Info, InfoDetailsViewModel>(info));
        //}

        //// POST: PcsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditInfo(InfoDetailsViewModel infoDVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var info = EditInfoViewModelConfigToModel().CreateMapper().Map<InfoDetailsViewModel, Info>(infoDVM);
        //            service.EditInfo(info);
        //        }
        //        catch (Exception)
        //        {
        //            error = 1;
        //        }
        //    }
        //    else
        //    {
        //        error = 2;
        //    }
        //    return RedirectToAction("Details", new { id = infoDVM.PcId, errorCode = error });
        //}

        //public ActionResult EditSkillsAndAbilities(string id)
        //{
        //    if (id == null) return new BadRequestResult();

        //    var pcIdAndAbilities = service.GetAbilitiesByInfoId(id);
        //    if (pcIdAndAbilities.Abilities == null) return NotFound();
        //    var abilities = EditAbilitiesConfigToViewModel().CreateMapper()
        //        .Map<List<Ability>, List<ViewModels.Pc.PartialEdit.AbilityViewModel>>(pcIdAndAbilities.Abilities);
        //    abilities.ForEach(ab => ab.PcId = pcIdAndAbilities.PcId);

        //    return PartialView("_EditSkillsAndAbilities", abilities);
        //}

        //// POST: PcsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditSkillsAndAbilities(List<ViewModels.Pc.PartialEdit.AbilityViewModel> abilitiesVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var abilities = EditAbilitiesViewModelConfigToModel()
        //                .CreateMapper()
        //                .Map< List<ViewModels.Pc.PartialEdit.AbilityViewModel>, List<Ability>>(abilitiesVM);
        //            service.EditAbilities(abilities);
        //        }
        //        catch (Exception)
        //        {
        //            error = 1;
        //        }
        //    }
        //    else
        //    {
        //        error = 2;
        //    }
        //    return RedirectToAction("Details", new { id = abilitiesVM.First().PcId, errorCode = error });
        //}



        // POST: PcsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            var request = new DeletePcCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return RedirectToAction(nameof(Index), new { error = error });
        }
    }
}
