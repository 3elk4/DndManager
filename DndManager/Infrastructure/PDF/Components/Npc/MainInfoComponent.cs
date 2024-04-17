using Application.Common.Extentions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infrastructure.PDF.Components.Npc
{
    internal class MainInfoComponent : IComponent
    {
        private Domain.Entities.Npc Npc { get; }
        public MainInfoComponent(Domain.Entities.Npc npc)
        {
            Npc = npc;
        }

        public void Compose(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(ComposeMainInfo);
                row.ConstantColumn(20);
                row.RelativeItem().Column(ComposeDetails);
            });

        }

        private void ComposeMainInfo(ColumnDescriptor column)
        {
            column.Spacing(10);

            if (Npc.Image != null)
            {
                column.Item().Image(Npc.Image).FitWidth();
            }

            column.Item().Background(Colors.Grey.Lighten4).Row(row =>
            {
                row.RelativeItem().AlignLeft().Text(Npc.Name).Bold();
                row.RelativeItem().AlignRight().Text($"{Npc.Size}, {Npc.Type}, {Npc.Alignment}");
            });

            column.Item().LineHorizontal((float)0.5);

            column.Item().Background(Colors.Grey.Lighten4).Row(row =>
            {
                row.RelativeItem().AlignLeft().Text("AC").Bold();
                var armorType = Npc.AcType == null ? "" : $"({Npc.AcType})";
                row.RelativeItem().AlignRight().Text($"{Npc.AC}{armorType}");
            });

            column.Item().Background(Colors.Grey.Lighten4).Row(row =>
            {
                row.RelativeItem().AlignLeft().Text("HP").Bold();
                row.RelativeItem().AlignRight().Text($"{Npc.HP}({Npc.HpFormula})");
            });

            column.Item().Background(Colors.Grey.Lighten4).Row(row =>
            {
                row.RelativeItem().AlignLeft().Text("Speed").Bold();
                row.RelativeItem().AlignRight().Text(Npc.Speed);
            });

            column.Item().LineHorizontal((float)0.5);

            column.Item().Background(Colors.Grey.Lighten4).Row(row =>
            {
                row.RelativeItem().AlignLeft().Text("Proficiency Bonus").Bold();
                row.RelativeItem().AlignRight().Text(Npc.ProficiencyBonus);
            });

            column.Item().Background(Colors.Grey.Lighten4).Row(row =>
            {
                row.RelativeItem().AlignLeft().Text("Passive Perception").Bold();
                row.RelativeItem().AlignRight().Text(Npc.PassivePerception);
            });

            column.Item().Background(Colors.Grey.Lighten4).Row(row =>
            {
                row.RelativeItem().AlignLeft().Text("Challange XP").Bold();
                row.RelativeItem().AlignRight().Text($"{Npc.Challange}({Npc.ChallangeXp})");
            });

            column.Item().LineHorizontal((float)0.5);

            if (Npc.SpellInfo.Ability != null)
            {
                column.Item().Background(Colors.Red.Lighten5).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Spell Ability").Bold();
                    row.RelativeItem().AlignRight().Text(Npc.SpellInfo.Ability.Name);
                });
                column.Item().Background(Colors.Red.Lighten5).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Caster Lvl").Bold();
                    row.RelativeItem().AlignRight().Text(Npc.SpellInfo.CasterLvl);
                });
                column.Item().Background(Colors.Red.Lighten5).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Spell Save DC").Bold();
                    row.RelativeItem().AlignRight().Text(Npc.SpellInfo.SpellSaveDcMod);
                });
                column.Item().Background(Colors.Red.Lighten5).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Global Attack Mod").Bold();
                    row.RelativeItem().AlignRight().Text(Npc.SpellInfo.GlobalAttackMod);
                });
            }
        }

        [Obsolete]
        private void ComposeDetails(ColumnDescriptor column)
        {
            column.Spacing(10);

            column.Item().Grid(grid =>
            {
                grid.VerticalSpacing(5);
                grid.HorizontalSpacing(5);
                grid.AlignCenter();

                foreach (var ability in Npc.Abilities)
                {
                    grid
                        .Item(2)
                        .Background(Colors.Red.Lighten5)
                        .Column(col =>
                        {
                            col.Spacing(5);
                            col.Item().AlignCenter().Text(ability.Name.ToUpper().Substring(0, 3)).Bold();
                            col.Item().AlignCenter().Text(ability.Value).FontSize(14);
                            col.Item().AlignCenter().Text(ability.Value.Mod());
                        });
                }
            });

            var savingthrows = Npc.Abilities
                                    .Where(ability => ability.SavingThrowBonus != null)
                                    .Select(ability => $"{ability.Name.Substring(0, 3)} {ability.SavingThrowBonus}")
                                    .ToList();

            if (savingthrows.Count > 0)
            {
                column.Item().Background(Colors.Grey.Lighten4).Row(row =>
                {
                    row.ConstantItem(50).AlignLeft().Text("Saving Throws").Bold();
                    row.RelativeItem().AlignLeft().Text(string.Join(',', savingthrows));
                });
            }

            var skills = Npc.Abilities
                                .SelectMany(ability => ability.Skills).OrderBy(skill => skill.Name)
                                .ToList()
                                .Where(skill => skill.Bonus != null)
                                .Select(skill => $"{skill.Name} {skill.Bonus}")
                                .ToList();

            if (skills.Count > 0)
            {
                column.Item().Background(Colors.Grey.Lighten4).Row(row =>
                {
                    row.ConstantItem(50).AlignLeft().Text("Skills").Bold();
                    row.RelativeItem().AlignLeft().Text(string.Join(',', skills));
                });
            }

            column.Item().LineHorizontal((float)0.5);

            var dmgvulns = Npc.Proficiencies.Where(proficiency => proficiency.Type.Equals("dmgvul"));

            if (dmgvulns.Any())
            {
                column.Item().Background(Colors.Grey.Lighten4).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Damage Vulnerabilities").Bold();
                    row.RelativeItem().AlignLeft().Text(string.Join(',', dmgvulns.Select(proficiency => proficiency.Name).ToList()));
                });
            }

            var dmgres = Npc.Proficiencies.Where(proficiency => proficiency.Type.Equals("dmgres"));

            if (dmgres.Any())
            {
                column.Item().Background(Colors.Grey.Lighten4).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Damage Resistances").Bold();
                    row.RelativeItem().AlignLeft().Text(string.Join(',', dmgres.Select(proficiency => proficiency.Name).ToList()));
                });
            }

            var dmgimmun = Npc.Proficiencies.Where(proficiency => proficiency.Type.Equals("dmgimmun"));

            if (dmgimmun.Any())
            {
                column.Item().Background(Colors.Grey.Lighten4).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Damage Imunities").Bold();
                    row.RelativeItem().AlignLeft().Text(string.Join(',', dmgimmun.Select(proficiency => proficiency.Name).ToList()));
                });
            }

            var condimmun = Npc.Proficiencies.Where(proficiency => proficiency.Type.Equals("condimmun"));

            if (condimmun.Any())
            {
                column.Item().Background(Colors.Grey.Lighten4).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Condition Immunities").Bold();
                    row.RelativeItem().AlignLeft().Text(string.Join(',', condimmun.Select(proficiency => proficiency.Name).ToList()));
                });
            }

            var sense = Npc.Proficiencies.Where(proficiency => proficiency.Type.Equals("sense"));

            if (sense.Any())
            {
                column.Item().Background(Colors.Grey.Lighten4).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Senses").Bold();
                    row.RelativeItem().AlignLeft().Text(string.Join(',', sense.Select(proficiency => $"{proficiency.Name}({proficiency.Range})").ToList()));
                });
            }

            var language = Npc.Proficiencies.Where(proficiency => proficiency.Type.Equals("language"));

            if (language.Any())
            {
                column.Item().Background(Colors.Grey.Lighten4).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Languages").Bold();
                    row.RelativeItem().AlignLeft().Text(string.Join(',', language.Select(proficiency => proficiency.Name).ToList()));
                });
            }

            column.Item().LineHorizontal((float)0.5);
        }
    }
}
