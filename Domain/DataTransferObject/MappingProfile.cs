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
            CreateMap<RssSource, RssSourceModel>().ReverseMap();
        }
    }
}
