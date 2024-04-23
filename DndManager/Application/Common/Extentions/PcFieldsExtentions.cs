using Application.Ability;
using Application.DndClass;
using Application.Pc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Application.Common.Extentions
{
    public static class PcFieldsExtentions
    {
        public static string ConvertToBase64String(this byte[] image)
        {
            return image == null ? "" : "data:image/jpg;base64," + Convert.ToBase64String(image, 0, image.Length);
        }

        public static int Initiative(this PcDetailsVM pc)
        {
            return pc.Abilities.First(a => a.Name.Equals("DEX")).Value.Mod();
        }

        public static int Initiative(this Domain.Entities.Pc pc)
        {
            return pc.Abilities.First(a => a.Name.Equals("DEX")).Value.Mod();
        }

        public static int PassiveWisdom(this PcDetailsVM pc)
        {
            var proficiency = pc.DndClasses.Proficiency();
            AbilityVM wis = pc.Abilities.First(a => a.Name.Equals("WIS"));
            SkillVM perc = wis.Skills.First(s => s.Name.Equals("Perception"));
            return 10 + wis.Value.Mod() + (perc.Proficient ? proficiency : 0);
        }

        public static int PassiveWisdom(this Domain.Entities.Pc pc)
        {
            var proficiency = pc.DndClasses.Proficiency();
            Domain.Entities.Ability wis = pc.Abilities.First(a => a.Name.Equals("WIS"));
            Domain.Entities.Skill perc = wis.Skills.First(s => s.Name.Equals("Perception"));
            return 10 + wis.Value.Mod() + (perc.Proficient ? proficiency : 0);
        }

        public static int Mod(this int value)
        {
            return (int)Math.Floor((decimal)(value - 10) / 2);
        }

        public static int Proficiency(this IEnumerable<object> dndClasses)
        {
            var value = dndClasses.Sum(d =>
            {
                if (d is DndClassVM dvm) return dvm.Lvl;
                else if (d is Domain.Entities.DndClass de) return de.Lvl;
                else return 0;
            });

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

        public static byte[] PhotoIntoByteImage(this IFormFile Photo)
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

    }
}
