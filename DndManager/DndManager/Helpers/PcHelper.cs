using Application.Ability;
using Application.SpellLvlInfo;
using System.Collections.Generic;

namespace Presentation.Helpers
{
    public class PcHelper
    {
        private static Dictionary<string, List<string>> AbilityAndSkillNames = new Dictionary<string, List<string>>() {
            { "STR", new List<string> () { "Athletics" } },
            { "DEX", new List<string> () { "Acrobatics", "Stealth", "Sleight of Hand" } },
            {"CON", new List<string>() },
            {"INT", new List<string> () { "Arcana", "History", "Investigation", "Nature", "Religion" } },
            {"WIS", new List<string> () { "Animal Handling", "Perception", "Medicine", "Survival", "Insight" } },
            {"CHA", new List<string> () { "Deception", "Intimidation", "Performance", "Persuation" } }
        };

        public static List<SpellLvlInfoVM> GenerateSpellLvls()
        {
            List<SpellLvlInfoVM> spellLvls = new List<SpellLvlInfoVM>();
            for (int i = 0; i < 10; ++i) spellLvls.Add(new SpellLvlInfoVM() { Lvl = i });
            return spellLvls;
        }

        public static List<AbilityVM> GenerateAbilitiesWithSkills()
        {
            var result = new List<AbilityVM>();
            foreach (var keyvalue in AbilityAndSkillNames) result.Add(GenerateAbility(keyvalue));
            return result;
        }

        private static AbilityVM GenerateAbility(KeyValuePair<string, List<string>> keyValuePair)
        {
            return new AbilityVM() { Id = Guid.NewGuid().ToString(), Name = keyValuePair.Key, Skills = GenerateSkills(keyValuePair.Value) };
        }

        private static List<SkillVM> GenerateSkills(List<string> skillNames)
        {
            var skills = new List<SkillVM>();
            foreach (var skillName in skillNames) skills.Add(new SkillVM() { Name = skillName });
            return skills;
        }
    }
}
