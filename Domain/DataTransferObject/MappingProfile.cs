using AutoMapper;
using Entities.Entity.NewsEnt;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<News, NewsGetDTO>().ReverseMap(); 
            //CreateMap<News, NewsCategoryRssSourceDTO>().ReverseMap(); 

            CreateMap<News, NewsModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<RssSource, RssSourceDto>().ReverseMap();
            CreateMap<RssSource, RssSourceModel>().ReverseMap();

            CreateMap<User, MemberDto>()
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(s => s.Login))
              .ForMember(dest => dest.Age, opt => opt.MapFrom(s => s.DayOfBirth.CalculateAge()));

            CreateMap<RssSource, RssSourceModel>()
                .ForMember(x => x.Id = Guid.NewGuid(), opt => opt.MapFrom(s => s.Id=Guid.NewGuid()))


                .ForMember(x => x.Id = Guid.NewGuid());
        }
    }
}
