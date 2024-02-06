using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.Item
{
    public class ItemVM : BaseVM
    {
        [Required(AllowEmptyStrings = true, ErrorMessage = "Please provide item's name")]
        [MaxLength(100, ErrorMessage = "Item's name should be min 1 and max 100 length")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide item's quantity")]
        [Range(0, 1000, ErrorMessage = "Item's quantity shouldn't be negative")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please provide item's weight")]
        [Range(0, 1000, ErrorMessage = "Item's weight shouldn't be negative")]
        public double Weight { get; set; }

        [MaxLength(500, ErrorMessage = "Item's notes should be between 0 and 500")]
        public string Notes { get; set; }
        public string PcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Item, ItemVM>();
            }
        }
    }
}
