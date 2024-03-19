using Application.Common.Extentions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
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

                column.Item().Grid(grid =>
                {
                    grid.VerticalSpacing(5);
                    grid.HorizontalSpacing(5);
                    grid.Columns(4);

                    foreach (var action in CombatActions)
                    {
                        grid.Item(1).Background(Colors.Orange.Lighten3).Column(column => column.Item().AlignLeft().Text(action.Name));
                        grid.Item(1).Background(Colors.Orange.Lighten3).Column(column => column.Item().AlignLeft().Text(action.Type));
                        if (action.CombatAttack.Ability != null)
                        {
                            var attackBonus = action.CombatAttack.Ability.Value.Mod() + action.CombatAttack.AdditionalBonus;
                            if (action.CombatAttack.IsProficient) attackBonus += Proficiency;

                            grid.Item(1).Background(Colors.Orange.Lighten4).Column(column => column.Item().AlignCenter().Text($"+{attackBonus}"));
                        }
                        else if (action.CombatSavingThrow.Ability != null)
                        {
                            var saveDc = 8 + action.CombatSavingThrow.Ability.Value.Mod() + Proficiency;

                            grid.Item(1).Background(Colors.Orange.Lighten4).Column(column => column.Item().AlignCenter().Text(saveDc));
                        }
                        else grid.Item(1).Background(Colors.Orange.Lighten4).Column(column => column.Item().AlignCenter().Text(""));

                        var damageBonus = action.CombatDamage.Ability == null ? 0 : action.CombatDamage.Ability.Value.Mod() + action.CombatDamage.AdditionalBonus;
                        grid.Item(1).Background(Colors.Orange.Lighten5).Column(column => column.Item().AlignCenter().Text($"{action.CombatDamage.DamageDice}+{damageBonus} {action.CombatDamage.DamageType}"));
                    }
                });
            });
        }
    }
}
