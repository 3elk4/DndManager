﻿using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace Infrastructure.PDF.Components.Pc
{
    internal class ProficienciesAndItemsComponent : IComponent
    {
        private IList<Proficiency> Proficiencies { get; }
        private IList<Item> Items { get; }

        public ProficienciesAndItemsComponent(IList<Proficiency> proficiencies, IList<Item> items)
        {
            Proficiencies = proficiencies;
            Items = items;
        }

        [Obsolete]
        public void Compose(IContainer container)
        {
            container.Row(row =>
            {
                row.ConstantColumn(150).Column(ComposeProficiencies);
                row.ConstantColumn(20);
                row.RelativeItem().Column(ComposeItems);
            });
        }

        private void ComposeProficiencies(ColumnDescriptor column)
        {
            column.Spacing(10);

            column.Item().Text("Proficiencies");

            foreach (var prof in Proficiencies)
            {
                column.Item().Background(Colors.Orange.Lighten5).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text(prof.Name).Bold();
                    row.RelativeItem().AlignRight().Text(prof.Type);
                });

            }
        }

        [Obsolete]
        private void ComposeItems(ColumnDescriptor column)
        {
            column.Spacing(10);

            column.Item().Text("Items");

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(5);
                });

                table.Cell().LabelCell("Name");
                table.Cell().LabelCell("Weight");
                table.Cell().LabelCell("Quantity");
                table.Cell().LabelCell("Notes");

                foreach (var item in Items)
                {
                    table.Cell().ValueCell(item.Name);
                    table.Cell().ValueCell(item.Weight);
                    table.Cell().ValueCell(item.Quantity);
                    table.Cell().ValueCell(item.Notes);
                }
            });
        }
    }
}
