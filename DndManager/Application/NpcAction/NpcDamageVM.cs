using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.NpcAction
{
    public class NpcDamageVM : BaseVM
    {
        [MaxLength(50, ErrorMessage = "Damage dice should be min 0 and max 50 length")]
        public string DamageDice { get; set; }

        [MaxLength(50, ErrorMessage = "Damage type should be min 0 and max 50 length")]
        public string DamageType { get; set; }

        public string ActionId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.NpcDamage, NpcDamageVM>();
                CreateMap<NpcDamageVM, Domain.Entities.NpcDamage>();
            }
        }
    }
}
