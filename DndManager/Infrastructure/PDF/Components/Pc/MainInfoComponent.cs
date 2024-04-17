using Application.Common.Extentions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infrastructure.PDF.Components.Pc
{
    internal class MainInfoComponent : IComponent
    {
        private Domain.Entities.Pc Pc { get; }
        private int Proficiency { get; }

        public MainInfoComponent(Domain.Entities.Pc pc, int proficiency)
        {
            Pc = pc;
            Proficiency = proficiency;
        }

        [Obsolete]
        public void Compose(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(ComposeMainInfo);
                row.ConstantColumn(20);
                row.RelativeItem().Column(ComposeDetails);
            });

        }

        [Obsolete]
        private void ComposeMainInfo(ColumnDescriptor column)
        {
            column.Spacing(10);

            ComposeLegend(column);

            if (Pc.Image != null)
            {
                ComposeImage(column);
            }

            ComposeField(column, "Name", Pc.Name);
            ComposeField(column, "Race", Pc.RaceName);
            ComposeField(column, "Background", Pc.BackgroundName);

            column.Item().LineHorizontal((float)0.5);

            ComposeClasses(column);
        }

        private void ComposeField(ColumnDescriptor column, string title, string value)
        {
            column.Item().Row(row =>
            {
                row.RelativeItem().AlignLeft().Text(title).Bold();
                row.RelativeItem().AlignRight().Text(value);
            });
        }

        private void ComposeImage(ColumnDescriptor column)
        {
            column.Item().Image(Pc.Image).FitWidth();
        }

        [Obsolete]
        private void ComposeClasses(ColumnDescriptor column)
        {
            column.Item().Text("Classes").Bold();

            foreach (var klass in Pc.DndClasses)
            {
                column.Item().Row(row =>
                {
                    var subclass = klass.SubclassName != null ? $"({klass.SubclassName})" : "";
                    row.RelativeItem().AlignLeft().Text($"{klass.Name} {subclass}");
                    row.RelativeItem().AlignRight().Text(klass.Lvl);
                });
            }
        }

        private void ComposeLegend(ColumnDescriptor column)
        {
            column.Item().Background(Colors.Grey.Lighten4).Column(col =>
            {
                col.Spacing(5);
                col.Item().PaddingLeft(5).AlignLeft().Text("Legend").Bold();
                col.Item().PaddingLeft(5).AlignLeft().Text("* - Proficiency in saving throw.");
            });
        }

        [Obsolete]
        private void ComposeDetails(ColumnDescriptor column)
        {
            column.Spacing(10);

            ComposeAbilities(column);
            ComposeSkills(column);

            column.Item().LineHorizontal((float)0.5);

            ComposeAttributes(column);
        }

        [Obsolete]
        private void ComposeAttributes(ColumnDescriptor column)
        {
            column.Item().Grid(grid =>
            {
                grid.VerticalSpacing(5);
                grid.HorizontalSpacing(5);
                grid.AlignCenter();
                grid.Columns(3);

                ComposeAttribute(grid, "AC", Pc.AC.ToString());
                ComposeAttribute(grid, "Speed", Pc.Speed);
                ComposeAttribute(grid, "Initiative", Pc.Initiative().ToString());

                ComposeAttribute(grid, "HP", Pc.HP.ToString());
                ComposeAttribute(grid, "Current HP", Pc.CurrentHP.ToString());
                ComposeAttribute(grid, "Temporary HP", Pc.TempHP.ToString());

                ComposeAttribute(grid, "HitDice", Pc.HitDice);
                ComposeAttribute(grid, "Proficiency", Proficiency.ToString());
                ComposeAttribute(grid, "Passive wisdom", Pc.PassiveWisdom().ToString());
            });
        }

        private void ComposeAttribute(GridDescriptor grid, string title, string value)
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

        private void ComposeSkills(ColumnDescriptor column)
        {
            var Skills = Pc.Abilities.SelectMany(ability => ability.Skills).OrderBy(skill => skill.Name).ToList();

            foreach (var skill in Skills)
            {
                var abilityShort = skill.Ability.Name.ToUpper().Substring(0, 3);
                var mod = skill.Ability.Value.Mod();
                var skillValue = mod + (skill.Proficient ? Proficiency : 0);

                column
                    .Item()
                    .Background(skill.Proficient ? Colors.Orange.Lighten4 : Colors.Grey.Lighten4)
                    .Row(row =>
                    {
                        row.Spacing(5);
                        row.RelativeItem().AlignLeft().Text(skillValue);
                        row.ConstantItem(10);
                        row.RelativeItem().AlignRight().Text($"{skill.Name} ({abilityShort})");
                    });
            }
        }

        [Obsolete]
        private void ComposeAbilities(ColumnDescriptor column)
        {
            column.Item().Grid(grid =>
            {
                grid.VerticalSpacing(5);
                grid.HorizontalSpacing(5);
                grid.AlignCenter();

                foreach (var ability in Pc.Abilities)
                {
                    grid
                        .Item(2)
                        .Background(ability.SavingThrow ? Colors.Orange.Lighten4 : Colors.Grey.Lighten4)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().Row(row =>
                            {
                                var abilityName = ability.Name.ToUpper().Substring(0, 3);
                                var abilityTitle = ability.SavingThrow ? $"{abilityName}*" : abilityName;

                                row.RelativeItem().AlignCenter().Text(abilityTitle).Bold();
                            }
                            );

                            col.Item().AlignCenter().Text(ability.Value).FontSize(14);
                            col.Item().AlignCenter().Text(ability.Value.Mod());
                        });
                }
            });

        }
    }
}
