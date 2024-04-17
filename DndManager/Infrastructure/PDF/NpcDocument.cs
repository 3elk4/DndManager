using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infrastructure.PDF
{
    internal class NpcDocument : IDocument
    {
        private Npc Model { get; }

        public NpcDocument(Npc model)
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
            var titleStyle = TextStyle.Default.FontSize(16).ExtraLight().Italic().FontColor(Colors.Red.Accent4);

            container.Row(row =>
            {
                row.RelativeItem().AlignRight().Text(Model.Name).Style(titleStyle);
            });
        }

        [Obsolete]
        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(15).Stack(stack =>
            {
                stack.Item().Row(row => row.RelativeItem().Component(new Components.Npc.MainInfoComponent(Model)));

                if (Model.Feats.Count > 0)
                {
                    stack.Item().PageBreak();
                    stack.Item().Row(row => row.RelativeItem().Component(new Components.Npc.FeatsComponent(Model.Feats)));
                }

                if (Model.Actions.Count > 0)
                {
                    stack.Item().PageBreak();
                    stack.Item().Row(row => row.RelativeItem().Component(new Components.Npc.ActionsComponent(Model.Actions)));
                }
            });
        }
    }
}
