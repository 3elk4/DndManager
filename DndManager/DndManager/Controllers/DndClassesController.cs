using Application.DndClass;
using Application.DndClass.Commands.Create;
using Application.DndClass.Commands.Delete;
using Application.DndClass.Commands.Update;
using Application.DndClass.Queries.GetManyByPcId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class DndClassesController : Controller
    {
        private readonly IMediator _mediator;
        private int error = 0;

        public DndClassesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetManyDndClasssByPcIdQuery() { PcId = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["PcId"] = id;
            return View(result.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(string id, [FromBody] List<DndClassVM> dndClasses)
        {
            if (!ModelState.IsValid) error = 2;
            else
            {
                var request = new UpdateManyDndClassesCommand() { DndClasses = dndClasses };
                var result = await _mediator.Send(request);

                if (result.IsFailure) error = 1;
            }

            return Json(new { redirectToUrl = Url.Action("EditAsync", "DndClasses", new { id = id }), error });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(string id, [FromBody] string pcId)
        {
            var request = new DeleteDndClassCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return Json(new { redirectToUrl = Url.Action("EditAsync", "DndClasses", new { id = pcId }), error });
        }

        public async Task<IActionResult> NewDndClassAsync(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new AddNewDndClassCommand() { PcId = id,  Name = "", Lvl = 1, SubclassName = "" };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return PartialView("_DndClassEdit", result.Value);
        }
    }
}
