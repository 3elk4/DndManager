using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace Infrastructure.PDF.Components.Npc
{
    internal class FeatsComponent : IComponent
    {
        private IList<NpcFeat> Feats { get; }
        public FeatsComponent(IList<NpcFeat> feats)
        {
            Feats = feats;
        }

        [Obsolete]
        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(10);

                column.Item().AlignCenter().Text("Feats").FontSize(13).Bold();

                column.Item().Grid(grid =>
                {
                    grid.VerticalSpacing(5);
                    grid.HorizontalSpacing(5);
                    grid.Columns(1);

                    foreach (var feat in Feats)
                    {
                        grid
                            .Item(1)
                            .Background(Colors.Red.Lighten5)
                            .PaddingVertical(3).PaddingHorizontal(5)
                            .Column(column =>
                            {
                                column.Spacing(5);
                                column.Item().Background(Colors.Red.Lighten4).PaddingVertical(3).PaddingHorizontal(5).Row(row =>
                                {
                                    row.RelativeItem().AlignLeft().Text(feat.Name).Bold();
                                    if (feat.TimeRegeneration != null)
                                    {
                                        row.RelativeItem().AlignRight().Text(feat.TimeRegeneration);
                                    }
                                });
                                column.Item().PaddingHorizontal(5).Row(row => row.RelativeItem().Text(feat.Description));
                            });
                    }
                });
            });
        }
    }
}
