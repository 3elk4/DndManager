using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.DndClass
{
    public class DndClassVM : BaseVM
    {
        [Required(AllowEmptyStrings = true, ErrorMessage = "Please provide dndclass name")]
        [MaxLength(50, ErrorMessage = "DndClass name should be min 1 and max 500 length")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide dndclass lvl")]
        [Range(1, 20, ErrorMessage = "Lvl should be between 1 and 20")]
        public int Lvl { get; set; }

        [MaxLength(50, ErrorMessage = "Subclass name should be max 50 length")]
        public string SubclassName { get; set; }

        //[Required]
        public string PcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.DndClass, DndClassVM>();
                CreateMap<DndClassVM, Domain.Entities.DndClass>();
            }
        }
    }
}
