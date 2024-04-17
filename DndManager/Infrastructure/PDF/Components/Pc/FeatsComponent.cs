using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace Infrastructure.PDF.Components.Pc
{
    internal class FeatsComponent : IComponent
    {
        private IList<Feat> Feats { get; }

        public FeatsComponent(IList<Feat> feats)
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
                            .Background(Colors.Orange.Lighten5)
                            .PaddingVertical(3).PaddingHorizontal(5)
                            .Column(column =>
                            {
                                column.Spacing(5);
                                column.Item().Background(Colors.Orange.Lighten4).PaddingVertical(3).PaddingHorizontal(5).Row(row =>
                                {
                                    row.RelativeItem().AlignLeft().Text(feat.Title).Bold();
                                    row.RelativeItem().AlignRight().Text($"{feat.Source}({feat.SourceType})");
                                });
                                column.Item().PaddingHorizontal(5).Row(row => row.RelativeItem().Text(feat.Definition));
                            });
                    }
                });
            });
        }
    }
}
