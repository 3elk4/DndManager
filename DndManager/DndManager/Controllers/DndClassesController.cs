using Application.DndClass;
using Application.DndClass.Commands.Create;
using Application.DndClass.Commands.Delete;
using Application.DndClass.Commands.Update;
using Application.DndClass.Queries.Index;

namespace Presentation.Controllers
{
    public class DndClassesController : Controller
    {
        private readonly IMediator _mediator;

        public DndClassesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("pcs/{pcid}/dndclasses")]
        [HttpGet]
        public async Task<ActionResult> Index(string pcid)
        {
            Guard.Against.Null(pcid);

            var request = new GetManyDndClasssByPcIdQuery() { PcId = pcid };
            var result = await _mediator.Send(request);

            ViewData["PcId"] = pcid;
            return View(result);
        }

        [Route("pcs/{pcid}/classes/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DndClassVM dndClassVM, [FromRoute] string pcid)
        {
            Guard.Against.Null(pcid);

            var request = new AddNewDndClassCommand()
            {
                PcId = pcid,
                Name = dndClassVM.Name,
                Lvl = dndClassVM.Lvl,
                SubclassName = dndClassVM.SubclassName
            };
            await _mediator.Send(request);

            TempData["Message"] = "Class created successfully!";
            return RedirectToAction("Index", "DndClasses", new { pcid = pcid });
        }


        [Route("pcs/{pcid}/dndclasses/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DndClassVM dndClassVM, [FromRoute] string pcid, [FromRoute] string id)
        {
            Guard.Against.Null(pcid);
            Guard.Against.Null(id);

            var request = new UpdateDndClassCommand()
            {
                Id = id,
                Name = dndClassVM.Name,
                Lvl = dndClassVM.Lvl,
                SubclassName = dndClassVM.SubclassName
            };
            await _mediator.Send(request);

            TempData["Message"] = "Class edited successfully!";
            return RedirectToAction("Index", "DndClasses", new { pcid = pcid });
        }

        [Route("pcs/{pcid}/dndclasses/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string pcid, string id)
        {
            Guard.Against.Null(pcid);
            Guard.Against.Null(id);

            var request = new DeleteDndClassCommand() { Id = id };
            await _mediator.Send(request);

            TempData["Message"] = "Class deleted successfully!";
            return RedirectToAction("Index", new { pcid = pcid });
        }
    }
}
