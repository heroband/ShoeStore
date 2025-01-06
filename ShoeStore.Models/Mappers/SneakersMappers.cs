using System.Runtime.CompilerServices;
using ShoeStore.Models.Entities;
using ShoeStore.Models.ViewModels;

namespace ShoeStore.Models.Mappers
{
    public static class SneakersMappers
    {
        public static Sneakers ToSneakersFromDto(this SneakersViewModel sneakersDto)
        {
            return new Sneakers
            {
                Name = sneakersDto.Name,
                Description = sneakersDto.Description,
                Size = sneakersDto.Size,
                Price = sneakersDto.Price,
                ImageUrl = sneakersDto.ImageUrl,
            };
        }

        public static SneakersViewModel ToSneakersDto(this Sneakers sneakers)
        {
            return new SneakersViewModel
            {
                Name = sneakers.Name,
                Description = sneakers.Description,
                Size = sneakers.Size,
                Price = sneakers.Price,
                ImageUrl = sneakers.ImageUrl,
            };
        }
    }
}
