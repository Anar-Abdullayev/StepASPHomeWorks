using AutoMapper;
using CityManagerApp1.Dtos;
using CityManagerApp1.Entities;

namespace CityManagerApp1.Mappers
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<City, CityForListDto>()
                .ForMember(dest => dest.PhotoUrl, option =>
                {
                    option.MapFrom(src => src.CityImages.FirstOrDefault(city => city.IsMain).Url);
                });
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
