using Application.Common.Models;
using Application.NpcAbility;
using Application.NpcSpellInfo;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Application.Npc
{
    public class NpcEditableVM : BaseVM
    {
        public string Name { get; init; }
        public string Type { get; init; }
        public string Size { get; set; }
        public int AC { get; set; }
        public string AcType { get; set; }
        public int HP { get; set; }
        public string HpFormula { get; set; }
        public string Speed { get; set; }
        public int ProficiencyBonus { get; set; }
        public int PassivePerception { get; set; }
        public string Alignment { get; init; }
        public int Challange { get; init; }//todo: double
        public int ChallangeXp { get; init; }

        public IFormFile Photo { get; set; }

        public IList<NpcAbilityVM> Abilities { get; init; }

        public NpcSpellInfoVM SpellInfo { get; set; }
    }
}
