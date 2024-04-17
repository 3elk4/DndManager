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
                ComposeImage(column);
            }

            ComposeName(column);

            column.Item().LineHorizontal((float)0.5);

            ComposeNpcAttribute(column, "AC", Npc.AcType == null ? Npc.AC.ToString() : $"{Npc.AC}({Npc.AcType})");
            ComposeNpcAttribute(column, "HP", $"{Npc.HP}({Npc.HpFormula})");
            ComposeNpcAttribute(column, "Speed", Npc.Speed.ToString());

            column.Item().LineHorizontal((float)0.5);

            ComposeNpcAttribute(column, "Proficiency Bonus", Npc.ProficiencyBonus.ToString());
            ComposeNpcAttribute(column, "Passive Perception", Npc.PassivePerception.ToString());
            ComposeNpcAttribute(column, "Challange XP", $"{Npc.Challange}({Npc.ChallangeXp})");

            column.Item().LineHorizontal((float)0.5);

            if (Npc.SpellInfo.Ability != null)
            {
                ComposeSpellInfo(column);
            }
        }

        private void ComposeNpcAttribute(ColumnDescriptor column, string title, string value)
        {
            column.Item().Background(Colors.Grey.Lighten4).Row(row =>
            {
                row.RelativeItem().AlignLeft().Text(title).Bold();
                row.RelativeItem().AlignRight().Text(value);
            });
        }

        private void ComposeName(ColumnDescriptor column)
        {
            column.Item().Background(Colors.Grey.Lighten4).Row(row =>
            {
                row.RelativeItem().AlignLeft().Text(Npc.Name).Bold();
                row.RelativeItem().AlignRight().Text($"{Npc.Size}, {Npc.Type}, {Npc.Alignment}");
            });
        }

        private void ComposeImage(ColumnDescriptor column)
        {
            column.Item().Image(Npc.Image).FitWidth();
        }

        private void ComposeSpellInfo(ColumnDescriptor column)
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

        [Obsolete]
        private void ComposeDetails(ColumnDescriptor column)
        {
            column.Spacing(10);

            ComposeAbilities(column);
            ComposeSavingThrows(column);
            ComposeSkills(column);

            column.Item().LineHorizontal((float)0.5);

            ComposeDamageVulnerabilities(column);
            ComposeDamageResistances(column);
            ComposeDamageImmunities(column);
            ComposeConditionImmunities(column);

            ComposeSenses(column);
            ComposeLanguages(column);

            column.Item().LineHorizontal((float)0.5);
        }

        private void ComposeLanguages(ColumnDescriptor column)
        {
            var languages = Npc.Proficiencies.Where(proficiency => proficiency.Type.Equals("language"));

            if (languages.Any())
            {
                column.Item().Background(Colors.Grey.Lighten4).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Languages").Bold();
                    row.RelativeItem().AlignLeft().Text(string.Join(',', languages.Select(proficiency => proficiency.Name).ToList()));
                });
            }
        }

        private void ComposeSenses(ColumnDescriptor column)
        {
            var senses = Npc.Proficiencies.Where(proficiency => proficiency.Type.Equals("sense"));

            if (senses.Any())
            {
                column.Item().Background(Colors.Grey.Lighten4).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Senses").Bold();
                    row.RelativeItem().AlignLeft().Text(string.Join(',', senses.Select(proficiency => $"{proficiency.Name}({proficiency.Range})").ToList()));
                });
            }
        }

        private void ComposeConditionImmunities(ColumnDescriptor column)
        {
            var conditionImmunities = Npc.Proficiencies.Where(proficiency => proficiency.Type.Equals("condimmun"));

            if (conditionImmunities.Any())
            {
                column.Item().Background(Colors.Grey.Lighten4).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Condition Immunities").Bold();
                    row.RelativeItem().AlignLeft().Text(string.Join(',', conditionImmunities.Select(proficiency => proficiency.Name).ToList()));
                });
            }
        }

        private void ComposeDamageImmunities(ColumnDescriptor column)
        {
            var damageImmunities = Npc.Proficiencies.Where(proficiency => proficiency.Type.Equals("dmgimmun"));

            if (damageImmunities.Any())
            {
                column.Item().Background(Colors.Grey.Lighten4).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Damage Imunities").Bold();
                    row.RelativeItem().AlignLeft().Text(string.Join(',', damageImmunities.Select(proficiency => proficiency.Name).ToList()));
                });
            }
        }

        private void ComposeDamageResistances(ColumnDescriptor column)
        {
            var damageResistances = Npc.Proficiencies.Where(proficiency => proficiency.Type.Equals("dmgres"));

            if (damageResistances.Any())
            {
                column.Item().Background(Colors.Grey.Lighten4).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Damage Resistances").Bold();
                    row.RelativeItem().AlignLeft().Text(string.Join(',', damageResistances.Select(proficiency => proficiency.Name).ToList()));
                });
            }
        }

        private void ComposeDamageVulnerabilities(ColumnDescriptor column)
        {
            var damageVulnerabilities = Npc.Proficiencies.Where(proficiency => proficiency.Type.Equals("dmgvul"));

            if (damageVulnerabilities.Any())
            {
                column.Item().Background(Colors.Grey.Lighten4).Row(row =>
                {
                    row.RelativeItem().AlignLeft().Text("Damage Vulnerabilities").Bold();
                    row.RelativeItem().AlignLeft().Text(string.Join(',', damageVulnerabilities.Select(proficiency => proficiency.Name).ToList()));
                });
            }
        }

        private void ComposeSkills(ColumnDescriptor column)
        {
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
        }

        private void ComposeSavingThrows(ColumnDescriptor column)
        {
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
        }

        [Obsolete]
        private void ComposeAbilities(ColumnDescriptor column)
        {
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
        }
    }
}
