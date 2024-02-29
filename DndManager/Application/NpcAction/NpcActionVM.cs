using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.NpcAction
{
    public class NpcActionVM : BaseVM
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide action name")]
        [MaxLength(100, ErrorMessage = "Action name should be min 1 and max 100 length")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide action type")]
        [MaxLength(50, ErrorMessage = "Action type should be min 1 and max 50 length")]
        public string Type { get; set; }

        [MaxLength(500, ErrorMessage = "Action type should be min 1 and max 500 length")]
        public string Description { get; set; }

        public NpcAttackVM Attack { get; set; }
        public NpcDamageVM Damage { get; set; }

        [Required]
        public string NpcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.NpcAction, NpcActionVM>();
            }
        }
    }
}
