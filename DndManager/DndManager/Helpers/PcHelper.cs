using Application.Ability;
using Application.DndClass;
using Application.SpellLvlInfo;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Presentation.Helpers
{
    public class PcHelper
    {
        private static Dictionary<string, List<string>> AbilityAndSkillNames = new Dictionary<string, List<string>>() {
            { "Strength", new List<string> () { "Athletics" } },
            { "Dexterity", new List<string> () { "Acrobatics", "Stealth", "Sleight of Hand" } },
            {"Constitution", new List<string>() },
            {"Intelligence", new List<string> () { "Arcana", "History", "Investigation", "Nature", "Religion" } },
            {"Wisdom", new List<string> () { "Animal Handling", "Perception", "Medicine", "Survival", "Insight" } },
            {"Charisma", new List<string> () { "Deception", "Intimidation", "Performance", "Persuation" } }
        };

        public static decimal Mod(int value)
        {
            return Math.Floor((decimal)(value - 10) / 2);
        }

        public static int Proficiency(List<DndClassVM> dndClasses)
        {
            var value = dndClasses.Select(d => d.Lvl).Sum();

            switch (value)
            {
                case >= 1 and <= 4:
                    return 2;
                case >= 5 and <= 8:
                    return 3;
                case >= 9 and <= 12:
                    return 4;
                case >= 13 and <= 16:
                    return 5;
                case >= 17 and <= 20:
                    return 6;
            }
            return 0;
        }

        public static byte[] PhotoIntoByteImage(IFormFile Photo)
        {
            if (Photo != null && Photo.Length > 0)
            {
                using MemoryStream ms = new MemoryStream();
                Photo.CopyTo(ms);
                Console.WriteLine(Convert.ToBase64String(ms.ToArray()));
                return ms.ToArray();
            }
            return null;
        }

        public static List<SpellLvlInfoVM> GenerateSpellLvls()
        {
            List<SpellLvlInfoVM> spellLvls = new List<SpellLvlInfoVM>();
            for(int i = 0; i < 10; ++i) spellLvls.Add(new SpellLvlInfoVM() { Lvl = i });
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
            return new AbilityVM() { Name = keyValuePair.Key, Skills = GenerateSkills(keyValuePair.Value) };
        }

        private static List<SkillVM> GenerateSkills(List<string> skillNames)
        {
            var skills = new List<SkillVM>();
            foreach (var skillName in skillNames) skills.Add(new SkillVM() { Name = skillName }); 
            return skills;
        }
    }
}
