using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace Infrastructure.PDF.Components.Pc
{
    internal class BioComponent : IComponent
    {
        private Bio Bio { get; }

        public BioComponent(Bio bio)
        {
            Bio = bio;
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
                    grid.AlignCenter();
                    grid.Columns(8);

                    foreach (var smallObject in new List<object>() { Bio.Age, Bio.Size, Bio.Weight, Bio.Height, Bio.Skin, Bio.Eyes, Bio.Hair, Bio.Alignment })
                    {

                        grid
                            .Item(1)
                            .Background(Colors.Orange.Lighten5)
                            .Column(col =>
                            {
                                col.Spacing(5);
                                col.Item().AlignCenter().Text(smallObject).FontSize(11);
                            });
                    }
                    grid.Item(8).LineHorizontal((float)0.5);

                    grid
                        .Item(2)
                        .Background(Colors.Orange.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().Text("Traits").Bold();
                            col.Item().AlignCenter().Text(Bio.Traits).FontSize(11);
                        });
                    grid
                        .Item(2)
                        .Background(Colors.Orange.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().Text("Flaws").Bold();
                            col.Item().AlignCenter().Text(Bio.Flaws).FontSize(11);
                        });

                    grid
                        .Item(2)
                        .Background(Colors.Orange.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().Text("Bonds").Bold();
                            col.Item().AlignCenter().Text(Bio.Bonds).FontSize(11);
                        });

                    grid
                        .Item(2)
                        .Background(Colors.Orange.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().Text("Ideals").Bold();
                            col.Item().AlignCenter().Text(Bio.Ideals).FontSize(11);
                        });

                    grid.Item(8).LineHorizontal((float)0.5);

                    grid
                        .Item(4)
                        .Background(Colors.Orange.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().Text("Allies").Bold();
                            col.Item().AlignCenter().Text(Bio.Allies).FontSize(11);
                        });

                    grid
                        .Item(4)
                        .Background(Colors.Orange.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().Text("Backstory").Bold();
                            col.Item().AlignCenter().Text(Bio.Backstory).FontSize(11);
                        });
                });
            });
        }
    }
}
