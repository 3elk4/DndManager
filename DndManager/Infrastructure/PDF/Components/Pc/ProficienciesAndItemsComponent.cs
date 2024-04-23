using Infrastructure.PDF.Extentions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace Infrastructure.PDF.Components.Pc
{
    internal class ProficienciesAndItemsComponent : IComponent
    {
        private IList<Proficiency> Proficiencies { get; }
        private IList<Item> Items { get; }
        private Money Money { get; }

        public ProficienciesAndItemsComponent(IList<Proficiency> proficiencies, IList<Item> items, Money money)
        {
            Proficiencies = proficiencies;
            Items = items;
            Money = money;
        }

        [Obsolete]
        public void Compose(IContainer container)
        {
            container.Row(row =>
            {
                row.ConstantColumn(150).Column(ComposeProficiencies);
                row.ConstantColumn(20);
                row.RelativeItem().Column(ComposeItemsWithMoney);
            });
        }

        private void ComposeProficiencies(ColumnDescriptor column)
        {
            column.Spacing(10);

            column.Item().Text("Proficiencies").FontSize(13).Bold();

            foreach (var prof in Proficiencies)
            {
                column.Item().Background(Colors.Orange.Lighten5).PaddingHorizontal(3).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text(prof.Name).Bold();
                    row.RelativeItem().AlignRight().Text(prof.Type);
                });

            }
        }

        [Obsolete]
        private void ComposeItemsWithMoney(ColumnDescriptor column)
        {
            column.Spacing(10);

            ComposeMoney(column);
            ComposeItems(column);
        }

        [Obsolete]
        private void ComposeItems(ColumnDescriptor column)
        {
            column.Item().Text("Items").FontSize(13).Bold();

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(5);
                });

                table.Header(header =>
                {
                    table.Cell().LabelCell("Name");
                    table.Cell().LabelCell("Weight");
                    table.Cell().LabelCell("Quantity");
                    table.Cell().LabelCell("Notes");
                });

                double total = 0;

                foreach (var item in Items)
                {
                    table.Cell().ValueCell(item.Name);
                    table.Cell().ValueCell(item.Weight);
                    table.Cell().ValueCell(item.Quantity);
                    table.Cell().ValueCell(item.Notes);

                    total += item.Weight * item.Quantity;
                }

                table.Footer(footer =>
                {
                    footer.Cell().ColumnSpan(4).AlignRight().Text($"Total weight: {total}", TextStyle.Default.Size(12).SemiBold());
                });
            });
        }

        [Obsolete]
        private void ComposeMoney(ColumnDescriptor column)
        {
            column.Item().Text("Money").FontSize(13).Bold();

            column.Item().Grid(grid =>
            {
                grid.VerticalSpacing(5);
                grid.HorizontalSpacing(5);
                grid.AlignCenter();
                grid.Columns(5);

                grid
                     .Item(1)
                     .Background(Colors.Orange.Lighten5)
                     .Padding(5)
                     .Column(col =>
                     {
                         col.Spacing(5);
                         col.Item().Text("Copper").Bold();
                         col.Item().Text(Money.Copper);
                     });
                grid
                     .Item(1)
                     .Background(Colors.Orange.Lighten5)
                     .Padding(5)
                     .Column(col =>
                     {
                         col.Spacing(5);
                         col.Item().Text("Silver").Bold();
                         col.Item().Text(Money.Silver);
                     });
                grid
                     .Item(1)
                     .Background(Colors.Orange.Lighten5)
                     .Padding(5)
                     .Column(col =>
                     {
                         col.Spacing(5);
                         col.Item().Text("Electrum").Bold();
                         col.Item().Text(Money.Electrum);
                     });
                grid
                     .Item(1)
                     .Background(Colors.Orange.Lighten5)
                     .Padding(5)
                     .Column(col =>
                     {
                         col.Spacing(5);
                         col.Item().Text("Gold").Bold();
                         col.Item().Text(Money.Gold);
                     });
                grid
                     .Item(1)
                     .Background(Colors.Orange.Lighten5)
                     .Padding(5)
                     .Column(col =>
                     {
                         col.Spacing(5);
                         col.Item().Text("Platinum").Bold();
                         col.Item().Text(Money.Platinum);
                     });
            });
        }
    }
}
