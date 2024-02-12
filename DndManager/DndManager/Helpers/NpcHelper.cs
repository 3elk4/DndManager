using Application.NpcAbility;
using System.Collections.Generic;

namespace Presentation.Helpers
{
    public class NpcHelper
    {
        private static Dictionary<string, List<string>> AbilityAndSkillNames = new Dictionary<string, List<string>>() {
            { "Strength", new List<string> () { "Athletics" } },
            { "Dexterity", new List<string> () { "Acrobatics", "Stealth", "Sleight of Hand" } },
            { "Constitution", new List<string>() },
            { "Intelligence", new List<string> () { "Arcana", "History", "Investigation", "Nature", "Religion" } },
            { "Wisdom", new List<string> () { "Animal Handling", "Perception", "Medicine", "Survival", "Insight" } },
            { "Charisma", new List<string> () { "Deception", "Intimidation", "Performance", "Persuation" } }
        };

        public static List<NpcAbilityVM> GenerateAbilitiesWithSkills()
        {
            var result = new List<NpcAbilityVM>();
            foreach (var keyvalue in AbilityAndSkillNames) result.Add(GenerateAbility(keyvalue));
            return result;
        }

        private static NpcAbilityVM GenerateAbility(KeyValuePair<string, List<string>> keyValuePair)
        {
            return new NpcAbilityVM() { Name = keyValuePair.Key, Skills = GenerateSkills(keyValuePair.Value) };
        }

        private static List<NpcSkillVM> GenerateSkills(List<string> skillNames)
        {
            var skills = new List<NpcSkillVM>();
            foreach (var skillName in skillNames) skills.Add(new NpcSkillVM() { Name = skillName });
            return skills;
        }
    }
}
