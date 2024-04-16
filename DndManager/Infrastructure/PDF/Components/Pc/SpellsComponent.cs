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

                    var attackBonus = SpellInfo.Ability.Value.Mod() + Proficiency;
                    var saveDc = 8 + attackBonus;

                    grid.Item(1)
                        .Background(Colors.Orange.Lighten5)
                        .PaddingVertical(3).PaddingHorizontal(3)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignLeft().Text("Spell ability").Bold();
                            col.Item().AlignRight().Text(SpellInfo.Ability.Name);
                        });

                    grid.Item(1)
                        .Background(Colors.Orange.Lighten5)
                        .PaddingVertical(3).PaddingHorizontal(3)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignLeft().Text("Save DC").Bold();
                            col.Item().AlignRight().Text(saveDc);
                        });

                    grid.Item(1)
                        .Background(Colors.Orange.Lighten5)
                        .PaddingVertical(3).PaddingHorizontal(3)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignLeft().Text("Attack bonus").Bold();
                            col.Item().AlignRight().Text(attackBonus);
                        });

                    grid.Item(3).LineHorizontal((float)0.5);

                    foreach (var spellLvl in SpellInfo.SpellLvls.Where(spellLvl => spellLvl.Max > 0 || spellLvl.Lvl == 0).OrderBy(spellLvl => spellLvl.Lvl))
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
                                    row.RelativeItem().Column(col => col.Item().AlignRight().Text(spellLvl.Lvl == 0 ? "Cantrips" : $"{spellLvl.Remaining}/{spellLvl.Max}"));
                                });
                                foreach (var spell in spellLvl.Spells)
                                {
                                    column.Item().Row(row => row.RelativeItem().AlignLeft().Text($"- {spell.Name}"));
                                }
                            });
                    }
                });
            });
        }
    }
}
