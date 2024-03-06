using Application.Item;
using Application.Item.Commands.Create;
using Application.Item.Commands.Delete;
using Application.Item.Commands.Update;
using Application.Item.Queries.Index;

namespace Presentation.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IMediator _mediator;
        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("pcs/{pcid}/items")]
        [HttpGet]
        public async Task<ActionResult> Index(string pcid)
        {
            Guard.Against.Null(pcid);

            var request = new GetManyItemsByPcIdQuery() { PcId = pcid };
            var result = await _mediator.Send(request);

            ViewData["PcId"] = pcid;
            return View(result);
        }

        [Route("pcs/{pcid}/items/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemVM dndClassVM, [FromRoute] string pcid)
        {
            Guard.Against.Null(pcid);

            var request = new AddNewItemCommand()
            {
                PcId = pcid,
                Name = dndClassVM.Name,
                Quantity = dndClassVM.Quantity,
                Weight = dndClassVM.Weight,
                Notes = dndClassVM.Notes,
            };
            var result = await _mediator.Send(request);

            TempData["Message"] = "Item created successfully!";
            return RedirectToAction("Index", "Items", new { pcid = pcid });
        }


        [Route("pcs/{pcid}/items/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ItemVM dndClassVM, [FromRoute] string pcid, [FromRoute] string id)
        {
            Guard.Against.Null(pcid);
            Guard.Against.Null(id);

            var request = new UpdateItemCommand()
            {
                Id = id,
                Name = dndClassVM.Name,
                Quantity = dndClassVM.Quantity,
                Weight = dndClassVM.Weight,
                Notes = dndClassVM.Notes,
            };
            await _mediator.Send(request);

            TempData["Message"] = "Item updated successfully!";
            return RedirectToAction("Index", "Items", new { pcid = pcid });
        }

        [Route("pcs/{pcid}/items/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string pcid, string id)
        {
            Guard.Against.Null(pcid);
            Guard.Against.Null(id);

            var request = new DeleteItemCommand() { Id = id };
            await _mediator.Send(request);

            TempData["Message"] = "Item deleted successfully!";
            return RedirectToAction("Index", new { pcid = pcid });
        }
    }
}
