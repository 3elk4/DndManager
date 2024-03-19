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

                column.Item().Grid(grid =>
                {
                    grid.VerticalSpacing(5);
                    grid.HorizontalSpacing(5);
                    grid.Columns(3);

                    foreach (var feat in Feats)
                    {
                        grid
                            .Item(1)
                            .Background(Colors.Orange.Lighten5)
                            .Column(column =>
                            {
                                column.Spacing(10);
                                column.Item().Row(row => row.RelativeItem().AlignLeft().Text(feat.Title).Bold());
                                column.Item().Row(row => row.RelativeItem().AlignLeft().Text($"{feat.Source}({feat.SourceType})"));
                                column.Item().Row(row => row.RelativeItem().Text(feat.Definition));
                            });
                    }
                });
            });
        }
    }
}
