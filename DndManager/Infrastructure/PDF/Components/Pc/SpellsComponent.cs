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

                column.Item().Grid(grid =>
                {
                    grid.VerticalSpacing(5);
                    grid.HorizontalSpacing(5);
                    grid.AlignCenter();
                    grid.Columns(3);

                    var attackBonus = SpellInfo.Ability.Value.Mod() + Proficiency;
                    var saveDc = 8 + attackBonus;

                    grid.Item(1).Background(Colors.Orange.Lighten4).Column(column => column.Item().Text("Spell ability"));
                    grid.Item(1).Background(Colors.Orange.Lighten4).Column(column => column.Item().Text("Save DC"));
                    grid.Item(1).Background(Colors.Orange.Lighten4).Column(column => column.Item().Text("AttackBonus"));

                    grid.Item(1).Background(Colors.Orange.Lighten5).Column(column => column.Item().Text(SpellInfo.Ability.Name));
                    grid.Item(1).Background(Colors.Orange.Lighten5).Column(column => column.Item().Text(saveDc));
                    grid.Item(1).Background(Colors.Orange.Lighten5).Column(column => column.Item().Text(attackBonus));

                    grid.Item(3).LineHorizontal((float)0.5);

                    foreach (var spellLvl in SpellInfo.SpellLvls.OrderBy(spellLvl => spellLvl.Lvl))
                    {
                        grid
                        .Item(1)
                        .Padding(5)
                        .Background(Colors.Orange.Lighten5)
                        .Column(column =>
                        {
                            column.Spacing(3);
                            column.Item().Background(Colors.Orange.Lighten4).Row(row =>
                            {
                                row.ConstantItem(10).Column(col => col.Item().AlignLeft().Text(spellLvl.Lvl));
                                row.RelativeItem().Column(col => col.Item().AlignRight().Text(spellLvl.Lvl == 0 ? "Cantrips" : $"{spellLvl.Remaining}/{spellLvl.Max}"));
                            });
                            foreach (var spell in spellLvl.Spells)
                            {
                                column.Item().Row(row => row.RelativeItem().AlignLeft().Text(spell.Name));
                            }
                        });
                    }
                });
            });
        }
    }
}
