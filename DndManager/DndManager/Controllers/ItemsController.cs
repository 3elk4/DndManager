using Application.Item;
using Application.Item.Commands.Create;
using Application.Item.Commands.Delete;
using Application.Item.Commands.UpdateMany;
using Application.Item.Queries.GetManyByPcId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IMediator _mediator;
        private int error = 0;
        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Show(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new GetManyItemsByPcIdQuery() { PcId = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["PcId"] = id;
            return View(result.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, [FromBody] List<ItemVM> items)
        {
            if (!ModelState.IsValid) error = 2;
            else
            {
                var request = new UpdateManyItemsCommand() { Items = items };
                var result = await _mediator.Send(request);

                if (result.IsFailure) error = 1;
            }

            return Json(new { redirectToUrl = Url.Action("Show", "Items", new { id = id }), error });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, [FromBody] string pcId)
        {
            var request = new DeleteItemCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return Json(new { redirectToUrl = Url.Action("Show", "Items", new { id = pcId }), error });
        }

        public async Task<IActionResult> NewItem(string id)
        {
            if (id == null) return new BadRequestResult();

            var request = new AddNewItemCommand() { PcId = id, Name = "", Quantity = 0, Weight = 0.0 };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return PartialView("_ItemEdit", result.Value);
        }
    }
}
