using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.NpcAction
{
    public class NpcAttackVM : BaseVM
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide action to hit")]
        [Range(0, 5000, ErrorMessage = "Attack additonal bonus should be min 0 and max 5000")]
        public int ToHit { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide action type")]
        [MaxLength(50, ErrorMessage = "Attack type should be min 1 and max 50 length")]
        public string Type { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide action target")]
        [MaxLength(50, ErrorMessage = "Attack target should be min 1 and max 50 length")]
        public string Target { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide action range")]
        [MaxLength(50, ErrorMessage = "Attack range should be min 1 and max 50 length")]
        public string Range { get; set; }

        public string ActionId { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.NpcAttack, NpcAttackVM>();
                CreateMap<NpcAttackVM, Domain.Entities.NpcAttack>();
            }
        }
    }
}