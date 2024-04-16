using Application.Common.Extentions;
using Infrastructure.PDF.Extentions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace Infrastructure.PDF.Components.Pc
{
    internal class ActionsComponent : IComponent
    {
        private IList<CombatAction> CombatActions { get; }
        private int Proficiency { get; }

        public ActionsComponent(IList<CombatAction> combatActions, int proficiency)
        {
            CombatActions = combatActions;
            Proficiency = proficiency;
        }

        [Obsolete]
        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(10);

                column.Item().AlignCenter().Text("Actions").FontSize(13).Bold();

                if (CombatActions.Count() == 0) return;

                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(4);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(3);
                    });

                    table.Header(header =>
                    {
                        table.Cell().LabelCell("Name");
                        table.Cell().LabelCell("Type");
                        table.Cell().LabelCell("Attack/Spell DC");
                        table.Cell().LabelCell("Damage");
                    });

                    foreach (var action in CombatActions)
                    {

                        table.Cell().ValueCell(action.Name);
                        table.Cell().ValueCell(action.Type);
                        if (action.CombatAttack.Ability != null)
                        {
                            var attackBonus = action.CombatAttack.Ability.Value.Mod() + action.CombatAttack.AdditionalBonus;
                            if (action.CombatAttack.IsProficient) attackBonus += Proficiency;

                            table.Cell().ValueCell($"+{attackBonus}");
                        }
                        else if (action.CombatSavingThrow.Ability != null)
                        {
                            var saveDc = 8 + action.CombatSavingThrow.Ability.Value.Mod() + Proficiency;

                            table.Cell().ValueCell($"{saveDc} DC");
                        }
                        else table.Cell().ValueCell("");

                        var damageBonus = action.CombatDamage.Ability == null ? 0 : action.CombatDamage.Ability.Value.Mod() + action.CombatDamage.AdditionalBonus;
                        table.Cell().ValueCell($"{action.CombatDamage.DamageDice}+{damageBonus} {action.CombatDamage.DamageType}");
                    }
                });
            });
        }
    }
}
