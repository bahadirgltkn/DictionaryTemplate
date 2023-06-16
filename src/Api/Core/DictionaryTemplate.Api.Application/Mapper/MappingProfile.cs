using AutoMapper;
using DictinoaryTemplate.Common.Models.Queries;
using DictionaryTemplate.Api.Domain.Models;

namespace DictionaryTemplate.Api.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>().ReverseMap();
        }
    }
}
