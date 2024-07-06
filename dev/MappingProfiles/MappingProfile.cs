using AutoMapper;
using dev.Models;
using dev.ViewModels;

namespace dev.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
        }
    }
}
