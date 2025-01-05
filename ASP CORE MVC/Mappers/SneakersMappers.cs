using System.Runtime.CompilerServices;
using ASP_CORE_MVC.Models;
using ASP_CORE_MVC.Models.Dto;

namespace ASP_CORE_MVC.Mappers
{
    public static class SneakersMappers
    {
        public static Sneakers ToSneakersFromDto(this SneakersDto sneakersDto)
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

        public static SneakersDto ToSneakersDto(this Sneakers sneakers)
        {
            return new SneakersDto
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
