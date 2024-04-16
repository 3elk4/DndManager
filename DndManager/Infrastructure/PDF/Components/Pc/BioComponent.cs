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

                    var fieldsDict = new Dictionary<string, object>() {
                            { "Age", Bio.Age },
                            { "Size", Bio.Size },
                            { "Weight", Bio.Weight },
                            { "Height", Bio.Height},
                            { "Skin", Bio.Skin },
                            { "Eyes", Bio.Eyes },
                            { "Hair", Bio.Hair },
                            { "Alignment", Bio.Alignment }
                        };
                    foreach (var field in fieldsDict)
                    {

                        grid.Item(1)
                            .Background(Colors.Orange.Lighten5)
                            .PaddingVertical(3).PaddingHorizontal(3)
                            .Column(col =>
                            {
                                col.Spacing(5);
                                col.Item().AlignLeft().Text(field.Key).Bold();
                                col.Item().AlignRight().Text(field.Value);
                            });
                    }
                    grid.Item(8).LineHorizontal((float)0.5);

                    grid
                        .Item(2)
                        .Background(Colors.Orange.Lighten5)
                        .PaddingVertical(3).PaddingHorizontal(3)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().Text("Traits").Bold();
                            col.Item().AlignCenter().Text(Bio.Traits).FontSize(11);
                        });
                    grid
                        .Item(2)
                        .Background(Colors.Orange.Lighten5)
                        .PaddingVertical(3).PaddingHorizontal(3)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().Text("Flaws").Bold();
                            col.Item().AlignCenter().Text(Bio.Flaws).FontSize(11);
                        });

                    grid
                        .Item(2)
                        .Background(Colors.Orange.Lighten5)
                        .PaddingVertical(3).PaddingHorizontal(3)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().Text("Bonds").Bold();
                            col.Item().AlignCenter().Text(Bio.Bonds).FontSize(11);
                        });

                    grid
                        .Item(2)
                        .Background(Colors.Orange.Lighten5)
                        .PaddingVertical(3).PaddingHorizontal(3)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().Text("Ideals").Bold();
                            col.Item().AlignCenter().Text(Bio.Ideals).FontSize(11);
                        });

                    grid.Item(8).LineHorizontal((float)0.5);

                    grid
                        .Item(8)
                        .Background(Colors.Orange.Lighten5)
                        .PaddingVertical(3).PaddingHorizontal(3)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().Text("Allies").Bold();
                            col.Item().AlignCenter().Text(Bio.Allies ?? "").FontSize(11);
                        });

                    grid
                        .Item(8)
                        .Background(Colors.Orange.Lighten5)
                        .PaddingVertical(3).PaddingHorizontal(3)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().Text("Backstory").Bold();
                            col.Item().AlignCenter().Text(Bio.Backstory ?? "").FontSize(11);
                        });
                });
            });
        }
    }
}
