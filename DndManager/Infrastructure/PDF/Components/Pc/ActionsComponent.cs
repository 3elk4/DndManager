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
                            ComposeAttack(table, action.CombatAttack);
                        }
                        else if (action.CombatSavingThrow.Ability != null)
                        {
                            ComposeSavingThrow(table, action.CombatSavingThrow);
                        }
                        else table.Cell().ValueCell("");

                        ComposeDamage(table, action.CombatDamage);
                    }
                });
            });
        }

        private void ComposeAttack(TableDescriptor table, CombatAttack attack)
        {
            var attackBonus = attack.Ability.Value.Mod() + attack.AdditionalBonus;
            if (attack.IsProficient) attackBonus += Proficiency;

            table.Cell().ValueCell($"+{attackBonus}");
        }

        private void ComposeDamage(TableDescriptor table, CombatDamage damage)
        {
            var damageBonus = damage.Ability == null ? 0 : damage.Ability.Value.Mod() + damage.AdditionalBonus;
            table.Cell().ValueCell($"{damage.DamageDice}+{damageBonus} {damage.DamageType}");
        }

        private void ComposeSavingThrow(TableDescriptor table, CombatSavingThrow savingThrow)
        {
            var saveDc = 8 + savingThrow.Ability.Value.Mod() + Proficiency;

            table.Cell().ValueCell($"{saveDc} DC");
        }
    }
}
