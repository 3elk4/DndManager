using Application.Common.Extentions;
using Infrastructure.PDF.Extentions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;
using Svg.Skia;

namespace Infrastructure.PDF.Components.Pc
{
    internal class MainInfoComponent : IComponent
    {
        private Domain.Entities.Pc Pc { get; }
        private int Proficiency { get; }

        private SKPicture LIGHT_BULB = new SKSvg().FromSvg("<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 352 512\"><!--!Font Awesome Free 6.5.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2024 Fonticons, Inc.--><path d=\"M96.1 454.4c0 6.3 1.9 12.5 5.4 17.7l17.1 25.7a32 32 0 0 0 26.6 14.3h61.7a32 32 0 0 0 26.6-14.3l17.1-25.7a32 32 0 0 0 5.4-17.7l0-38.4H96l.1 38.4zM0 176c0 44.4 16.5 84.9 43.6 115.8 16.5 18.9 42.4 58.2 52.2 91.5 0 .3 .1 .5 .1 .8h160.2c0-.3 .1-.5 .1-.8 9.9-33.2 35.7-72.6 52.2-91.5C335.6 260.9 352 220.4 352 176 352 78.6 272.9-.3 175.5 0 73.4 .3 0 83 0 176zm176-80c-44.1 0-80 35.9-80 80 0 8.8-7.2 16-16 16s-16-7.2-16-16c0-61.8 50.2-112 112-112 8.8 0 16 7.2 16 16s-7.2 16-16 16z\"/></svg>");

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

            column.Item().Background(Colors.Grey.Lighten4).Border((float)0.2).Column(col =>
            {
                col.Spacing(5);
                col.Item().PaddingLeft(5).AlignLeft().Text("Legend").Bold();
                col.Item().Row(row =>
                {
                    row.ConstantItem(12).PaddingLeft(5).Svg(LIGHT_BULB);
                    row.RelativeItem().PaddingRight(5).AlignRight().Text("Proficiency in saving throw.");
                });

            });

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
                            col.Item().Row(row =>
                                {
                                    row.RelativeItem().AlignCenter().Text(ability.Name.ToUpper().Substring(0, 3)).Bold();
                                    if (ability.SavingThrow)
                                    {
                                        row.ConstantItem(6).Svg(LIGHT_BULB);
                                    }
                                }
                            );

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

                grid.Item(1)
                    .Background(Colors.Orange.Lighten5)
                    .PaddingVertical(3).PaddingHorizontal(3)
                    .Column(col =>
                    {
                        col.Spacing(5);
                        col.Item().AlignLeft().Text("AC").Bold();
                        col.Item().AlignRight().Text(Pc.AC);
                    });

                grid.Item(1)
                    .Background(Colors.Orange.Lighten5)
                    .PaddingVertical(3).PaddingHorizontal(3)
                    .Column(col =>
                    {
                        col.Spacing(5);
                        col.Item().AlignLeft().Text("Speed").Bold();
                        col.Item().AlignRight().Text(Pc.Speed);
                    });
                grid.Item(1)
                    .Background(Colors.Orange.Lighten5)
                    .PaddingVertical(3).PaddingHorizontal(3)
                    .Column(col =>
                    {
                        col.Spacing(5);
                        col.Item().AlignLeft().Text("Initiative").Bold();
                        col.Item().AlignRight().Text(Pc.Initiative());
                    });

                grid.Item(1)
                    .Background(Colors.Orange.Lighten5)
                    .PaddingVertical(3).PaddingHorizontal(3)
                    .Column(col =>
                    {
                        col.Spacing(5);
                        col.Item().AlignLeft().Text("HP").Bold();
                        col.Item().AlignRight().Text(Pc.HP);
                    });

                grid.Item(1)
                    .Background(Colors.Orange.Lighten5)
                    .PaddingVertical(3).PaddingHorizontal(3)
                    .Column(col =>
                    {
                        col.Spacing(5);
                        col.Item().AlignLeft().Text("Current HP").Bold();
                        col.Item().AlignRight().Text(Pc.CurrentHP);
                    });

                grid.Item(1)
                    .Background(Colors.Orange.Lighten5)
                    .PaddingVertical(3).PaddingHorizontal(3)
                    .Column(col =>
                    {
                        col.Spacing(5);
                        col.Item().AlignLeft().Text("Temp HP").Bold();
                        col.Item().AlignRight().Text(Pc.TempHP);
                    });

                grid.Item(1)
                    .Background(Colors.Orange.Lighten5)
                    .PaddingVertical(3).PaddingHorizontal(3)
                    .Column(col =>
                    {
                        col.Spacing(5);

                        col.Item().AlignLeft().Text("HitDice").Bold();
                        col.Item().AlignRight().Text(Pc.HitDice);
                    });

                grid.Item(1)
                    .Background(Colors.Orange.Lighten5)
                    .PaddingVertical(3).PaddingHorizontal(3)
                    .Column(col =>
                    {
                        col.Spacing(5);
                        col.Item().AlignLeft().Text("Proficiency").Bold();
                        col.Item().AlignRight().Text(Proficiency);
                    });

                grid.Item(1)
                    .Background(Colors.Orange.Lighten5)
                    .PaddingVertical(3).PaddingHorizontal(3)
                    .Column(col =>
                    {
                        col.Spacing(5);
                        col.Item().AlignLeft().Text("Passive wisdom").Bold();
                        col.Item().AlignRight().Text(Pc.PassiveWisdom());
                    });
            });
        }
    }
}
