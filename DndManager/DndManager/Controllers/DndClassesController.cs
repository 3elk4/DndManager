using Application.DndClass;
using Application.DndClass.Commands.Create;
using Application.DndClass.Commands.Delete;
using Application.DndClass.Commands.Update;
using Application.DndClass.Queries.Index;
using FormHelper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        [Route("pcs/{pcid}/dndclasses")]
        [HttpGet]
        public async Task<ActionResult> Index(string pcid)
        {
            if (pcid == null) return new BadRequestResult();

            var request = new GetManyDndClasssByPcIdQuery() { PcId = pcid };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["PcId"] = pcid;
            return View(result.Value);
        }

        [Route("pcs/{pcid}/classes/create")]
        [HttpPost]
        [FormValidator]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] DndClassVM dndClassVM, [FromRoute] string pcid)
        {
            if (pcid == null) return new BadRequestResult();

            var request = new AddNewDndClassCommand()
            {
                PcId = pcid,
                Name = dndClassVM.Name,
                Lvl = dndClassVM.Lvl,
                SubclassName = dndClassVM.SubclassName
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return StatusCode(500, $"{string.Join(',', result.Errors)}");

            return RedirectToAction("Index", "DndClasses", new { errorCode = error, pcid= pcid });
        }


        [Route("pcs/{pcid}/dndclasses/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DndClassVM dndClassVM, [FromRoute] string pcid, [FromRoute] string id)
        {
            if (pcid == null || id == null) return new BadRequestResult();

            var request = new UpdateDndClassCommand()
            {
                Id = id,
                Name = dndClassVM.Name,
                Lvl = dndClassVM.Lvl,
                SubclassName = dndClassVM.SubclassName
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return StatusCode(500, $"{string.Join(',', result.Errors)}");

            return RedirectToAction("Index", "DndClasses", new { errorCode = error, pcid = pcid });
        }

        [Route("pcs/{pcid}/dndclasses/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string pcid, string id)
        {
            var request = new DeleteDndClassCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return RedirectToAction("Index", new { pcid = pcid });
        }
    }
}
