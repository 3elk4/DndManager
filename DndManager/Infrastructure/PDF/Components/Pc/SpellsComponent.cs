using Application.Common.Extentions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infrastructure.PDF.Components.Pc
{
    internal class SpellsComponent : IComponent
    {
        private SpellInfo SpellInfo { get; }

        private int Proficiency { get; }

        public SpellsComponent(SpellInfo spellInfo, int proficiency)
        {
            SpellInfo = spellInfo;
            Proficiency = proficiency;
        }

        [Obsolete]
        public void Compose(IContainer container)
        {
            if (SpellInfo.Ability == null) return;

            container.Column(column =>
            {
                column.Spacing(10);

                column.Item().AlignCenter().Text("Spells").FontSize(13).Bold();

                column.Item().Grid(grid =>
                {
                    grid.VerticalSpacing(5);
                    grid.HorizontalSpacing(5);
                    grid.Columns(3);

                    ComposeSpellcasterAttributes(grid);

                    grid.Item(3).LineHorizontal((float)0.5);

                    ComposeSpellLvls(grid);
                });
            });
        }

        private void ComposeSpellLvls(GridDescriptor grid)
        {
            var spellLvls = SpellInfo.SpellLvls.Where(spellLvl => spellLvl.Max > 0 || spellLvl.Lvl == 0).OrderBy(spellLvl => spellLvl.Lvl);

            foreach (var spellLvl in spellLvls)
            {
                ComposeSpells(grid, spellLvl);
            }
        }

        private void ComposeSpells(GridDescriptor grid, SpellLvlInfo spellLvl)
        {
            grid.Item(1)
                   .Background(Colors.Orange.Lighten5)
                   .PaddingVertical(3).PaddingHorizontal(3)
                   .Column(column =>
                   {
                       column.Spacing(3);

                       column.Item().Background(Colors.Orange.Lighten4).PaddingVertical(3).PaddingHorizontal(5).Row(row =>
                       {
                           row.ConstantItem(10).Column(col => col.Item().AlignLeft().Text(spellLvl.Lvl));

                           var spellColumnHeader = spellLvl.Lvl == 0 ? "Cantrips" : $"{spellLvl.Remaining}/{spellLvl.Max}";
                           row.RelativeItem().Column(col => col.Item().AlignRight().Text(spellColumnHeader));
                       });

                       foreach (var spell in spellLvl.Spells)
                       {
                           column.Item().Row(row => row.RelativeItem().AlignLeft().Text($"- {spell.Name}"));
                       }
                   });
        }

        private void ComposeSpellcasterAttributes(GridDescriptor grid)
        {
            var attackBonus = SpellInfo.Ability.Value.Mod() + Proficiency;
            var saveDc = 8 + attackBonus;

            ComposeSpellcasterAttribute(grid, "Spell ability", SpellInfo.Ability.Name);
            ComposeSpellcasterAttribute(grid, "Save DC", saveDc.ToString());
            ComposeSpellcasterAttribute(grid, "Attack bonus", attackBonus.ToString());
        }

        private void ComposeSpellcasterAttribute(GridDescriptor grid, string title, string value)
        {
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
    }
}
