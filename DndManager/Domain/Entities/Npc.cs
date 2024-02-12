using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Npc : BaseAuditableEntity
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string Alignment { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public int AC { get; set; }
        public string AcType { get; set; }
        public int HP { get; set; }
        public string HpFormula { get; set; }
        public string Speed { get; set; }
        public int ProficiencyBonus { get; set; }
        public int PassivePerception { get; set; }
        public int Challange { get; set; }
        public int ChallangeXp { get; set; }

        public NpcSpellInfo SpellInfo { get; init; } = new NpcSpellInfo();
        public IList<NpcProficiency> Proficiencies { get; init; } = new List<NpcProficiency>();
        public IList<NpcFeat> Feats { get; private set; } = new List<NpcFeat>();
        public IList<NpcAction> Actions { get; private set; } = new List<NpcAction>();
        public IList<NpcAbility> Abilities { get; init; }
    }
}
