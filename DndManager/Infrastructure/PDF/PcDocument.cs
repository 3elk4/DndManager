using Application.Common.Extentions;
using Infrastructure.PDF.Components.Pc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infrastructure.PDF
{
    public class PcDocument : IDocument
    {
        private Pc Model { get; }

        public PcDocument(Pc model)
        {
            Model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(16).ExtraLight().Italic().FontColor(Colors.Orange.Accent4);

            container.Row(row =>
            {
                row.RelativeItem().AlignRight().Text(Model.Name).Style(titleStyle);
            });
        }

        [Obsolete]
        void ComposeContent(IContainer container)
        {
            var proficiency = Model.DndClasses.Proficiency();

            container.PaddingVertical(10).Stack(stack =>
            {
                stack.Item().Row(row => row.RelativeItem().Component(new MainInfoComponent(Model, proficiency)));
                stack.Item().PageBreak();
                stack.Item().Row(row => row.RelativeItem().Component(new BioComponent(Model.Bio)));
                stack.Item().PageBreak();
                stack.Item().Row(row => row.RelativeItem().Component(new FeatsComponent(Model.Feats)));
                stack.Item().PageBreak();
                stack.Item().Row(row => row.RelativeItem().Component(new ProficienciesAndItemsComponent(Model.Proficiencies, Model.Items)));
                stack.Item().PageBreak();
                stack.Item().Row(row => row.RelativeItem().Component(new SpellsComponent(Model.SpellInfo, proficiency)));
                stack.Item().PageBreak();
                stack.Item().Row(row => row.RelativeItem().Component(new ActionsComponent(Model.CombatActions, proficiency)));
            });
        }
    }
}


