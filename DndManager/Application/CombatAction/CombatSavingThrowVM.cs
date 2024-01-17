using Application.Common.Models;
using AutoMapper;

namespace Application.CombatAction
{
    public class CombatSavingThrowVM : BaseVM
    {
        public string CombatActionId { get; set; }
        public string? AbilityId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.CombatSavingThrow, CombatSavingThrowVM>();
            }
        }
    }
}
