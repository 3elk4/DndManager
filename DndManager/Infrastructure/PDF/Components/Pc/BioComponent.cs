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

                    var attributesDict = new Dictionary<string, object>() {
                            { "Age", Bio.Age },
                            { "Size", Bio.Size },
                            { "Weight", Bio.Weight },
                            { "Height", Bio.Height},
                            { "Skin", Bio.Skin },
                            { "Eyes", Bio.Eyes },
                            { "Hair", Bio.Hair },
                            { "Alignment", Bio.Alignment }
                        };
                    foreach (var attribute in attributesDict)
                    {
                        ComposeAttribute(grid, attribute);
                    }

                    grid.Item(8).LineHorizontal((float)0.5);

                    ComposeCharacteristic(grid, 2, "Traits", Bio.Traits);
                    ComposeCharacteristic(grid, 2, "Flaws", Bio.Flaws);
                    ComposeCharacteristic(grid, 2, "Bonds", Bio.Bonds);
                    ComposeCharacteristic(grid, 2, "Ideals", Bio.Ideals);

                    grid.Item(8).LineHorizontal((float)0.5);

                    ComposeCharacteristic(grid, 8, "Allies", Bio.Allies);
                    ComposeCharacteristic(grid, 8, "Backstory", Bio.Backstory);
                });
            });
        }

        [Obsolete]
        private void ComposeAttribute(GridDescriptor grid, KeyValuePair<string, object> attribute)
        {
            var title = attribute.Key;
            var value = attribute.Value;

            grid.Item(1)
                .Background(Colors.Orange.Lighten5)
                .PaddingVertical(3).PaddingHorizontal(3)
                .Column(col =>
                {
                    col.Spacing(5);
                    col.Item().AlignLeft().Text(title).Bold();
                    col.Item().AlignRight().Text(value);
                });
        }

        private void ComposeCharacteristic(GridDescriptor grid, int columnSize, string title, string value)
        {

            grid
                .Item(columnSize)
                .Background(Colors.Orange.Lighten5)
                .PaddingVertical(3).PaddingHorizontal(3)
                .Column(col =>
                {
                    col.Spacing(5);
                    col.Item().AlignCenter().Text(title).Bold();
                    col.Item().AlignCenter().Text(value ?? "").FontSize(11);
                });
        }
    }
}
