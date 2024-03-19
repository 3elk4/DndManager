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
                row.RelativeItem().Column(ComposeAbilities);
            });

        }

        [Obsolete]
        private void ComposeMainInfo(ColumnDescriptor column)
        {
            column.Spacing(10);

            //column.Item().Background(Colors.Grey.Lighten4).Border((float)0.2).Row(row =>
            //{
            //    row.RelativeItem().Text("Legend");
            //    row.RelativeItem().Text("Orange means proficiency in sth");
            //});

            column.Item().Image(Pc.Image).FitWidth();

            column.Item().Row(row =>
            {
                row.RelativeItem().AlignLeft().Text("Name").Bold();
                row.RelativeItem().AlignRight().Text(Pc.Name);
            });
            column.Item().Row(row =>
            {
                row.RelativeItem().AlignLeft().Text("Race").Bold();
                row.RelativeItem().AlignRight().Text(Pc.RaceName);
            });
            column.Item().Row(row =>
            {
                row.RelativeItem().AlignLeft().Text("Background").Bold();
                row.RelativeItem().AlignRight().Text(Pc.BackgroundName);
            });

            column.Item().LineHorizontal((float)0.5);

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

        [Obsolete]
        private void ComposeAbilities(ColumnDescriptor column)
        {
            column.Spacing(10);

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
                            col.Item().AlignCenter().Text(ability.Name.ToUpper().Substring(0, 3)).Bold();
                            col.Item().AlignCenter().Text(ability.Value).FontSize(14);
                            col.Item().AlignCenter().Text(ability.Value.Mod());
                        });
                }
            });

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

            column.Item().LineHorizontal((float)0.5);

            column.Item().Grid(grid =>
            {
                grid.VerticalSpacing(5);
                grid.HorizontalSpacing(5);
                grid.AlignCenter();
                grid.Columns(3);

                grid
                .Item(1)
                        .Background(Colors.Orange.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().AlignLeft().Text("AC").Bold();
                            col.Item().AlignCenter().AlignRight().Text(Pc.AC);
                        });

                grid
                .Item(1)
                        .Background(Colors.Orange.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().AlignLeft().Text("Speed").Bold();
                            col.Item().AlignCenter().AlignRight().Text(Pc.Speed);
                        });
                grid
                .Item(1)
                        .Background(Colors.Orange.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().AlignLeft().Text("HP").Bold();
                            col.Item().AlignCenter().AlignRight().Text(Pc.HP);
                        });

                grid
                .Item(1)
                        .Background(Colors.Orange.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().AlignLeft().Text("Current HP").Bold();
                            col.Item().AlignCenter().AlignRight().Text(Pc.CurrentHP);
                        });

                grid
                .Item(1)
                        .Background(Colors.Orange.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().AlignLeft().Text("Temp HP").Bold();
                            col.Item().AlignCenter().AlignRight().Text(Pc.TempHP);
                        });

                grid
                .Item(1)
                        .Background(Colors.Orange.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().AlignLeft().Text("HitDice").Bold();
                            col.Item().AlignCenter().AlignRight().Text(Pc.HitDice);
                        });
                grid
                .Item(1)
                        .Background(Colors.Orange.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().AlignLeft().Text("Proficiency").Bold();
                            col.Item().AlignCenter().AlignRight().Text(Proficiency);
                        });

                grid
                .Item(1)
                        .Background(Colors.Orange.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().AlignLeft().Text("Passive wisdom").Bold();
                            col.Item().AlignCenter().AlignRight().Text(Pc.PassiveWisdom());
                        });

                grid
                .Item(1)
                        .Background(Colors.Orange.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().AlignLeft().Text("Initiative").Bold();
                            col.Item().AlignCenter().AlignRight().Text(Pc.Initiative());
                        });


                //todo: proficiency bonus, passive wisdom, initiative
                //todo: what to do with current/temp hp?maybe changable pdf? sounds good to me
                //todo: make it prettier

            });
        }
    }
}
