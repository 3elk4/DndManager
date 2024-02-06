using Application.Item;
using Application.Item.Commands.Create;
using Application.Item.Commands.Delete;
using Application.Item.Commands.Update;
using Application.Item.Queries.Index;
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



        [Route("pcs/{pcid}/items")]
        [HttpGet]
        public async Task<ActionResult> Index(string pcid)
        {
            if (pcid == null) return new BadRequestResult();

            var request = new GetManyItemsByPcIdQuery() { PcId = pcid };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return NotFound($"{string.Join(',', result.Errors)}");

            ViewData["PcId"] = pcid;
            return View(result.Value);
        }

        [Route("pcs/{pcid}/items/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemVM dndClassVM, [FromRoute] string pcid)
        {
            if (pcid == null) return new BadRequestResult();

            var request = new AddNewItemCommand()
            {
                PcId = pcid,
                Name = dndClassVM.Name,
                Quantity = dndClassVM.Quantity,
                Weight = dndClassVM.Weight,
                Notes = dndClassVM.Notes,
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return StatusCode(500, $"{string.Join(',', result.Errors)}");

            return RedirectToAction("Index", "Items", new { errorCode = error, pcid = pcid });
        }


        [Route("pcs/{pcid}/items/{id}/edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ItemVM dndClassVM, [FromRoute] string pcid, [FromRoute] string id)
        {
            if (pcid == null || id == null) return new BadRequestResult();

            var request = new UpdateItemCommand()
            {
                Id = id,
                Name = dndClassVM.Name,
                Quantity = dndClassVM.Quantity,
                Weight = dndClassVM.Weight,
                Notes = dndClassVM.Notes,
            };
            var result = await _mediator.Send(request);

            if (result.IsFailure) return StatusCode(500, $"{string.Join(',', result.Errors)}");

            return RedirectToAction("Index", "Items", new { errorCode = error, pcid = pcid });
        }

        [Route("pcs/{pcid}/items/{id}/delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string pcid, string id)
        {
            var request = new DeleteItemCommand() { Id = id };
            var result = await _mediator.Send(request);

            if (result.IsFailure) error = 1;

            return RedirectToAction("Index", new { pcid = pcid });
        }
    }
}
