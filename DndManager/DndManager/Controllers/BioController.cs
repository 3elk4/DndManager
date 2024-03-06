using Application.Bio;
using Application.Bio.Commands.Update;
using Application.Bio.Queries.Get;

namespace Presentation.Controllers
{
    public class BioController : Controller
    {
        private readonly IMediator _mediator;

        public BioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("bio/{id}")]
        [HttpGet]
        public async Task<ActionResult> Show(string id)
        {
            Guard.Against.Null(id);

            var request = new GetBioByIdQuery() { Id = id };
            var result = await _mediator.Send(request);

            return View(result);
        }

        [Route("bio/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BioVM bio)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Some errors occured during updating bio.";
                return View("Show", bio);
            }

            var request = new UpdateBioCommand()
            {
                Id = bio.Id,
                Age = bio.Age,
                Size = bio.Size,
                Weight = bio.Weight,
                Height = bio.Height,
                Skin = bio.Skin,
                Eyes = bio.Eyes,
                Hair = bio.Hair,
                Alignment = bio.Alignment,
                Traits = bio.Traits,
                Flaws = bio.Flaws,
                Bonds = bio.Bonds,
                Ideals = bio.Ideals,
                Allies = bio.Allies,
                Backstory = bio.Backstory
            };
            await _mediator.Send(request);

            TempData["Message"] = "Bio updated successfully!";
            return RedirectToAction("Show", "Bio", new { id = bio.Id });
        }
    }
}
