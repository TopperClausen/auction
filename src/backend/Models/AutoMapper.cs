using AutoMapper;
using backend.Models;
using backend.Models.DTO;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Car, CarDTO>();
    }
}