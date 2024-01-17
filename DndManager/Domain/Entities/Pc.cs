using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pc : BaseAuditableEntity
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }

        public string RaceName { get; set; }
        public string BackgroundName { get; set; }

        public bool Inspiration { get; set; }
        public int AC { get; set; }
        public string Speed { get; set; }
        public int HP { get; set; }
        public int CurrentHP { get; set; }
        public int TempHP { get; set; }
        public string HitDice { get; set; }
        public int Proficiency { get; set; } = 2;


        public Bio Bio { get; set; } = new Bio();
        public SpellInfo SpellInfo { get; set; } = new SpellInfo();

        public IList<DndClass> DndClasses { get; init; } 
        public IList<Item> Items { get; private set; } = new List<Item>();
        public IList<Feat> Feats { get; private set; } = new List<Feat>();
        public IList<Proficiency> Proficiencies { get; private set; } = new List<Proficiency>();
        public IList<CombatAction> CombatsActions { get; set; } = new List<CombatAction>();
        public IList<Ability> Abilities { get; init; }
    }
}
