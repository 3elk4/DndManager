using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace Infrastructure.PDF.Components.Npc
{
    internal class ActionsComponent : IComponent
    {
        private IList<NpcAction> Actions { get; }
        public ActionsComponent(IList<NpcAction> actions)
        {
            Actions = actions;
        }

        [Obsolete]
        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(10);

                var actions = Actions.Where(action => action.Type.Equals("action")).ToList();
                var bonusActions = Actions.Where(action => action.Type.Equals("bonus action")).ToList();
                var reactions = Actions.Where(action => action.Type.Equals("reaction")).ToList();
                var legendaryActions = Actions.Where(action => action.Type.Equals("legendary action")).ToList();
                var mythicActions = Actions.Where(action => action.Type.Equals("mythic action")).ToList();

                var allActions = new Dictionary<string, List<NpcAction>>() {
                    { "Actions", actions },
                    { "Bonus actions", bonusActions },
                    { "Reactions", reactions },
                    { "Legendary actions", legendaryActions },
                    { "Mythic actions", mythicActions }
                };

                foreach (var titleActions in allActions)
                {
                    if (titleActions.Value.Count() == 0) continue;

                    column.Item().Text(titleActions.Key).Bold();

                    column.Item().Grid(grid =>
                    {
                        grid.VerticalSpacing(5);
                        grid.HorizontalSpacing(5);
                        grid.AlignLeft();
                        grid.Columns(1);

                        foreach (var action in titleActions.Value)
                        {
                            grid
                               .Item(1)
                               .Background(Colors.Red.Lighten5)
                               .PaddingVertical(3).PaddingHorizontal(5)
                               .Column(col =>
                               {
                                   col.Spacing(5);
                                   col.Item().Background(Colors.Red.Lighten3).PaddingVertical(3).PaddingHorizontal(5).Row(row =>
                                   {
                                       row.RelativeItem().AlignLeft().Text(action.Name).Bold();
                                   });
                                   col.Item().Background(Colors.Red.Lighten4).PaddingVertical(3).PaddingHorizontal(5).Column(column =>
                                   {
                                       if (action.Attack.Type != null) column.Item().AlignLeft().Text($"{action.Attack.Type} Attack, +{action.Attack.ToHit} to hit, reach {action.Attack.Range}, {action.Attack.Target}");
                                       if (action.Damage.DamageDice != null) column.Item().AlignLeft().Text($"Hit: {action.Damage.DamageDice} {action.Damage.DamageType}");
                                   });

                                   if (action.Description != null)
                                   {
                                       col.Item().AlignLeft().Text(action.Description);
                                   }
                               });
                        }
                    });
                }
            });
        }
    }
}
