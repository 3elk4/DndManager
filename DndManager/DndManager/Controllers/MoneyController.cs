using Application.Money;
using Application.Money.Commands.Update;
using Application.Money.Queries.Show;

namespace Presentation.Controllers
{
    public class MoneyController : Controller
    {
        private readonly IMediator _mediator;
        public MoneyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("money/{id}")]
        [HttpGet]
        public async Task<ActionResult> Show(string id)
        {
            Guard.Against.Null(id);

            var request = new GetMoneyByIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            return View(result);
        }


        [Route("money/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MoneyVM moneyVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Some errors occured during updating money.";
                return View(moneyVM);
            }

            var request = new UpdateMoneyCommand()
            {
                Id = moneyVM.Id,
                Copper = moneyVM.Copper,
                Silver = moneyVM.Silver,
                Electrum = moneyVM.Electrum,
                Gold = moneyVM.Gold,
                Platinum = moneyVM.Platinum
            };
            await _mediator.Send(request);

            TempData["Message"] = "Money updated successfully!";
            return RedirectToAction("Show", "Money", new { id = moneyVM.Id });
        }
    }
}
